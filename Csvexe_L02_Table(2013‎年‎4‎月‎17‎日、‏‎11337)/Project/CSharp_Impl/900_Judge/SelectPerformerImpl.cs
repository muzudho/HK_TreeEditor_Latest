using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Xenon.Syntax;

namespace Xenon.Table
{
    public class SelectPerformerImpl
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 無条件で、全てのレコードを返す。
        /// </summary>
        /// <param name="dst_Row"></param>
        /// <param name="isRequired_ExpectedValue"></param>
        /// <param name="dataTable"></param>
        /// <param name="s_ParentNode_query"></param>
        /// <param name="log_Reports"></param>
        public void Select(
            out List<DataRow> out_List_DstRow,
            bool isRequired_ExpectedValue,
            DataTable dataTable,
            Configuration_Node parent_Conf,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "Select",log_Reports);

            //
            //
            //
            //

            out_List_DstRow = new List<DataRow>();


            foreach (DataRow row in dataTable.Rows)
            {
                out_List_DstRow.Add(row);
            }

            goto gt_EndMethod;

            //
        //
        //
        //

        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 「フィールド名＝値」という条件１つで検索。該当するレコード０～Ｎ件を返す。
        /// </summary>
        /// <param name="dst_Row"></param>
        /// <param name="name_KeyField"></param>
        /// <param name="value_Expected"></param>
        /// <param name="isRequired_ExpectedValue"></param>
        /// <param name="fielddefinition_Key"></param>
        /// <param name="dataTable"></param>
        /// <param name="s_ParentNode_query"></param>
        /// <param name="log_Reports"></param>
        public void Select(
            out List<DataRow> out_List_DstRow,
            string name_KeyField,
            string value_Expected,
            bool isRequired_ExpectedValue,
            Fielddef fielddefinition_Key,
            DataTable dataTable,
            Configuration_Node parent_Query,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "Select",log_Reports);

            //
            //
            //
            //

            out_List_DstRow = new List<DataRow>();

            Judge_FieldBoolImpl judgeB = new Judge_FieldBoolImpl();
            Judge_FieldIntImpl judgeI = new Judge_FieldIntImpl();
            Judge_FieldStringImpl judgeS = new Judge_FieldStringImpl();


            foreach (DataRow row in dataTable.Rows)
            {
                bool bJudge;

                switch (fielddefinition_Key.Type_Field)
                {
                    case EnumTypeFielddef.String:
                        {
                            // string型フィールドなら
                            judgeS.Judge(
                                out bJudge,
                                name_KeyField,
                                value_Expected,
                                isRequired_ExpectedValue,
                                row,
                                parent_Query,
                                log_Reports
                            );
                        }
                        break;
                    case EnumTypeFielddef.Int:
                        {
                            // int型フィールドなら
                            judgeI.Judge(
                                out bJudge,
                                name_KeyField,
                                value_Expected,
                                isRequired_ExpectedValue,
                                row,
                                parent_Query,
                                log_Reports
                            );
                        }
                        break;
                    case EnumTypeFielddef.Bool:
                        {
                            // bool型フィールドなら
                            judgeB.Judge(
                                out bJudge,
                                name_KeyField,
                                value_Expected,
                                isRequired_ExpectedValue,
                                row,
                                parent_Query,
                                log_Reports
                            );
                        }
                        break;
                    default:
                        {
                            // エラー。
                            goto gt_Error_UndefinedClass;
                        }
                        break;
                }

                if (!log_Reports.Successful)
                {
                    // 既エラー。
                    goto gt_EndMethod;
                }

                if (bJudge)
                {
                    out_List_DstRow.Add(row);
                }
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_UndefinedClass:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー899", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();

                s.AppendI(0, "<NFuncCellUpdaterImplクラス>");
                s.Append(Environment.NewLine);

                s.AppendI(1, "予期しない型です。");
                s.Append(Environment.NewLine);

                s.AppendI(1, "keyFldDefinition.Type=[");
                s.Append(fielddefinition_Key.ToString_Type());
                s.Append("]");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

                // ヒント
                s.AppendI(1, r.Message_Configuration(parent_Query));

                s.AppendI(0, "</NFuncCellUpdaterImplクラス>");

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
        }

        //────────────────────────────────────────
        #endregion



    }
}
