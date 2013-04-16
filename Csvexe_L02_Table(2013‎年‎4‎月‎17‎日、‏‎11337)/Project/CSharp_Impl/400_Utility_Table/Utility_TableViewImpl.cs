using System;
using System.Collections.Generic;
using System.Data;//DataTable
using System.Linq;
using System.Text;
using System.Windows.Forms;//ListView

using Xenon.Syntax;//WarningReports

namespace Xenon.Table
{
    /// <summary>
    /// テーブル・ビュー・ユーティリティー。
    /// </summary>
    public class Utility_TableviewImpl
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// リスト・ビュー1の内容を、リスト・ビュー2へ、コピーします。
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="listView"></param>
        public void CopyTo(ListView listView1, ListView listView2, Log_Reports log_Reports)
        {
            // リスト・ビュー2を空にします。
            listView2.Clear();

            // 編集テーブルを、並び順変更先テーブルにコピーします。
            foreach (ColumnHeader columnHeader in listView1.Columns)
            {
                listView2.Columns.Add(columnHeader.Text);
            }

            foreach (ListViewItem listViewItem in listView1.Items)
            {
                // [0]列目を初期値として設定します。
                ListViewItem newItem = new ListViewItem(listViewItem.Text);

                // 最初の[0]列目は既に追加済みなので、[1]列目以降から追加します。
                for (int nIndex = 1; nIndex < listViewItem.SubItems.Count; nIndex++)
                {
                    newItem.SubItems.Add(listViewItem.SubItems[nIndex]);
                }
                listView2.Items.Add(newItem);
            }

            goto gt_EndMethod;
        //
        gt_EndMethod:
            ;
        }

        //────────────────────────────────────────

        /// <summary>
        /// リスト・ビューに、テーブルをセットします。
        /// </summary>
        public void SetDataSourceToListView(
            Table_Humaninput xenonTable, ListView listView, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "SetDataSourceToListView", log_Reports);

            DataTable dataTable = xenonTable.DataTable;

            listView.Clear();

            // リスト・ビューにフィールドを追加します。
            xenonTable.RecordFielddef.ForEach(delegate(Fielddef fielddefinition, ref bool isBreak2, Log_Reports log_Reports2)
            {
                // 列を追加します。見出しと幅も設定します。
                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append(fielddefinition.Name_Humaninput);

                if (this.IsVisibled_Fieldtype)
                {
                    // デバッグ用に、フィールドの型もヘッダーに表示する場合。
                    s.Append(":");
                    s.Append(fielddefinition.ToString_Type());
                }

                listView.Columns.Add(s.ToString(), 90);
            }, log_Reports);


            for (int index_Row = 0; index_Row < dataTable.Rows.Count; index_Row++)
            {
                DataRow row = dataTable.Rows[index_Row];

                ListViewItem item = null;

                object[] recordFields = row.ItemArray;//ItemArrayは1回の呼び出しが重い。
                for (int indexColumn = 0; indexColumn < recordFields.Length; indexColumn++)
                {
                    object columnObject = recordFields[indexColumn];

                    if (columnObject is Cell)
                    {
                        Cell valueH = (Cell)columnObject;

                        string valueField = valueH.Text;

                        // レコードを作成します。
                        if (0 == indexColumn)
                        {
                            // 最初の列の場合は、行追加になります。

                            // 文字列を追加。
                            item = new ListViewItem(valueField);
                            listView.Items.Add(item);
                        }
                        else
                        {
                            // 最初の列より後ろは、列追加になります。

                            // 文字列を追加。
                            item.SubItems.Add(valueField);
                        }
                    }
                    else if (columnObject is DBNull)
                    {
                        // 空欄、または列データを未設定。
                        goto gt_Error_DBNull;
                    }
                    else
                    {
                        //エラー
                        goto gt_Error_UnknownType;
                    }


                }
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_DBNull:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー201！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Newline();
                s.Append("列が未設定（DBNull）。テーブル名=[" + xenonTable.Name + "]");

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_UnknownType:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー202！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Newline();
                s.Append("未定義の型の列。テーブル名=[" + xenonTable.Name + "]");

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private bool isVisibled_Fieldtype;

        /// <summary>
        /// デバッグ用に、フィールドの型もヘッダーに表示するなら真。
        /// </summary>
        public bool IsVisibled_Fieldtype
        {
            set
            {
                isVisibled_Fieldtype = value;
            }
            get
            {
                return isVisibled_Fieldtype;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
