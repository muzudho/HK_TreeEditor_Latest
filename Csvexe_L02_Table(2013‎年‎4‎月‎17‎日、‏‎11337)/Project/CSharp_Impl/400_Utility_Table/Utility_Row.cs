using System;
using System.Collections.Generic;
using System.Data;//DataRowView
using System.Linq;
using System.Text;

using Xenon.Syntax;


namespace Xenon.Table
{

    /// <summary>
    /// 行ユーティリティー
    /// </summary>
    public class Utility_Row
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// O_TableImpl#AddRecordListで使います。
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="value"></param>
        /// <param name="oTable"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public static Cell ConfigurationTo_Field(
            int index_Column,
            string value,
            RecordFielddef recordFielddef,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, "Utility_Row", "ConfigurationTo_Field", log_Reports);
            //

            //
            // セルのソースヒント名
            string nodeConfigtree;
            try
            {
                nodeConfigtree = recordFielddef.ValueAt(index_Column).Name_Humaninput;
            }
            catch (ArgumentOutOfRangeException)
            {
                // エラー
                goto gt_Error_Index;
            }

            Cell result;

            // 型毎に処理を分けます。
            switch (recordFielddef.ValueAt(index_Column).Type_Field)
            {
                case EnumTypeFielddef.Int:
                    {
                        // 空白データも自動処理
                        IntCellImpl cellData = new IntCellImpl(nodeConfigtree);
                        cellData.Text = value;
                        result = cellData;
                    }
                    break;
                case EnumTypeFielddef.Bool:
                    {
                        // 空白データも自動処理
                        BoolCellImpl cellData = new BoolCellImpl(nodeConfigtree);
                        cellData.Text = value;
                        result = cellData;
                    }
                    break;
                default:
                    {
                        StringCellImpl cellData = new StringCellImpl(nodeConfigtree);
                        cellData.Text = value;
                        result = cellData;
                    }
                    break;
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Index:
            result = null;
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー461！", log_Method);

                Log_TextIndented t = new Log_TextIndentedImpl();

                t.Append("列インデックス[" + index_Column + "]（0スタート）が指定されましたが、");
                t.Newline();
                t.Append("列は[" + recordFielddef.Count + "]個しかありません。（列定義リストは、絞りこまれている場合もあります）");
                t.Newline();

                // ヒント

                r.Message = t.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────

        /// <summary>
        /// フィールドがなかった場合に、警告を作成してくれます。
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="c"></param>
        /// <param name="log_Reports"></param>
        /// <param name="dataSourceHintName"></param>
        /// <returns></returns>
        public static Cell GetFieldvalue(
            string name_Field,
            DataRow row,
            bool isNoHitIsError,
            Log_Reports log_Reports,
            string nodeConfigtree_Datasource
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, "Utility_Row", "GetFieldvalue", log_Reports);

            Cell result;

            // ArgumentException予防
            if (!row.Table.Columns.Contains(name_Field))
            {
                // 該当なしの場合

                result = null;


                if (isNoHitIsError)
                {
                    // 指定のフィールドが、該当なしの場合エラーになる設定なら
                    goto gt_Error_NotFoundField;
                }

                // 該当なしの場合エラーにならない設定なら、フィールドがなくても無視します。
                // 正常
                goto gt_EndMethod;
            }

            Exception err_Excp;
            try
            {
                // bug: 列名には、大文字・小文字の区別はないようです。
                result = (Cell)row[name_Field];
            }
            catch (Exception e)
            {
                result = null;
                err_Excp = e;
                goto gt_Error_Exception;
            }

            if (isNoHitIsError && null==result)
            {
                goto gt_Error_Null;
            }

            // 正常
            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NotFoundField:
            // エラー。指定のフィールドが見つからなかった。
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー603！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("指定のフィールド[");
                s.Append(name_Field);
                s.Append("]は、データソース[");
                s.Append(nodeConfigtree_Datasource);
                s.Append("]にはありませんでした。");
                s.Append(Environment.NewLine);

                s.Append("テーブル名＝[");
                s.Append(row.Table.TableName);
                s.Append("]");
                s.Append(Environment.NewLine);

                //sB.Append("エラー型：");
                //sB.Append(err_Excp.GetType().Name);
                //sB.Append(Environment.NewLine);
                //sB.Append("エラーメッセージ：");
                //sB.Append(err_Excp.Message);

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Exception:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー601！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("指定のフィールド[");
                s.Append(name_Field);
                s.Append("]の読取りに失敗しました。");
                s.Append(Environment.NewLine);

                s.Append("テーブル名＝[");
                s.Append(row.Table.TableName);
                s.Append("]");
                s.Append(Environment.NewLine);

                s.Append("データソースヒント名：");
                s.Append(nodeConfigtree_Datasource);
                s.Append(Environment.NewLine);

                s.Append("メッセージ：");
                s.Append(err_Excp.Message);

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Null:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー602！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("▲エラー4101！(" + Info_Table.Name_Library + ")");
                s.Newline();
                s.Append("指定のフィールド[");
                s.Append(name_Field);
                s.Append("]は、ヌルでした。");
                s.Append(Environment.NewLine);
                s.Append("データソースヒント名：");
                s.Append(nodeConfigtree_Datasource);
                s.Append(Environment.NewLine);

                s.Append("テーブル名＝[");
                s.Append(row.Table.TableName);
                s.Append("]");
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
            return result;
        }

        //────────────────────────────────────────
        #endregion



    }
}
