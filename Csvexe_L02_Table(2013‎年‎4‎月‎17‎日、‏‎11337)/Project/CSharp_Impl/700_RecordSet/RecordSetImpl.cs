using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{
    public class RecordSetImpl : RecordSet
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public RecordSetImpl(Table_Humaninput xenonTable)
        {
            this.xenonTable = xenonTable;

            this.list_Field = new List<Dictionary<string, Cell>>();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// DataRow → Dictionary
        /// </summary>
        /// <param name="row"></param>
        /// <param name="log_Reports"></param>
        public void Add(DataRow row, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "Add",log_Reports);

            Dictionary<string, Cell> record = new Dictionary<string, Cell>();

            int nFieldCount = row.ItemArray.Length;
            for (int nFieldIndex = 0; nFieldIndex < nFieldCount; nFieldIndex++)
            {
                // フィールド名
                string sFieldName = xenonTable.RecordFielddef.ValueAt(nFieldIndex).Name_Trimupper;

                // 値
                Cell oValue;
                if (row[nFieldIndex] is DBNull)
                {
                    //// デバッグ
                    //if (true)
                    //{
                    //Log_TextIndented txt = new Log_TextIndentedImpl();

                    //    txt.Append(InfxenonTable.LibraryName + ":" + this.GetType().Name + "#Add:【ヌル】");
                    //    txt.Append("　field＝[" + sFieldName + "]");

                    //    ystem.Console.WriteLine(txt.ToString());
                    //}

                    String sConfigStack = xenonTable.Expression_Filepath_ConfigStack.Execute4_OnExpressionString(
                        EnumHitcount.Unconstraint, log_Reports);
                    if (!log_Reports.Successful)
                    {
                        // 既エラー。
                        goto gt_EndMethod;
                    }

                    EnumTypeFielddef typeField = xenonTable.RecordFielddef.ValueAt(nFieldIndex).Type_Field;
                    switch (typeField)
                    {
                        case EnumTypeFielddef.String:
                            oValue = new StringCellImpl(sConfigStack);
                            break;
                        case EnumTypeFielddef.Int:
                            oValue = new IntCellImpl(sConfigStack);
                            break;
                        case EnumTypeFielddef.Bool:
                            oValue = new BoolCellImpl(sConfigStack);
                            break;
                        default:
                            // エラー。
                            goto gt_Error_UndefinedType;
                    }
                }
                else
                {
                    oValue = (Cell)row[nFieldIndex];

                    //// デバッグ
                    //if (true)
                    //{
                    //Log_TextIndented txt = new Log_TextIndentedImpl();

                    //    txt.Append(InfxenonTable.LibraryName + ":" + this.GetType().Name + "#Add:【○】");
                    //    txt.Append("　値＝[" + oValue.HumanInputString + "]");

                    //    ystem.Console.WriteLine(txt.ToString());
                    //}
                }

                record.Add(sFieldName, oValue);
            }

            this.List_Field.Add(record);

            // 正常
            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_UndefinedType:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー293！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("　未定義の型です。プログラムのミスの可能性があります。");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

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

        /// <summary>
        /// List＜DataRow＞ → Dictionary
        /// </summary>
        /// <param name="row"></param>
        /// <param name="log_Reports"></param>
        public void AddList(List<DataRow> list_Row, Log_Reports log_Reports)
        {
            foreach (DataRow row in list_Row)
            {
                this.Add(row, log_Reports);
                if (!log_Reports.Successful)
                {
                    // 既エラー。
                    goto gt_EndMethod;
                }

            }

            // 正常
            goto gt_EndMethod;

            //
        //
        //
        //
        gt_EndMethod:
            return;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private List<Dictionary<string, Cell>> list_Field;

        public List<Dictionary<string, Cell>> List_Field
        {
            get
            {
                return list_Field;
            }
            set
            {
                list_Field = value;
            }
        }

        //────────────────────────────────────────

        private Table_Humaninput xenonTable;

        public Table_Humaninput XenonTable
        {
            get
            {
                return xenonTable;
            }
            set
            {
                xenonTable = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
