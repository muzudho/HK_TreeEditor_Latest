using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Syntax
{

    /// <summary>
    /// Expression系クラスのユーティリティーです。
    /// </summary>
    public abstract class Utility_Expression_Node_String
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 未実装の場合。
        /// </summary>
        /// <param name="parent_Conf"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public static List<Expression_Node_String> NoImpl_Expression_GetChildList(
            Configurationtree_Node parent_Conf,
            Log_Reports log_Reports
            )
        {
            //
            // エラー。

            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, "Util_Expression_Node_String", "NoImpl_Expression_GetChildList", log_Reports);
            //
            //
            //
            //

            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー361！", log_Method);

                StringBuilder sb = new StringBuilder();

                sb.Append("Expression_GetChildList メソッドは、未実装です。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                // ヒント
                sb.Append(r.Message_Configuration(parent_Conf));

                r.Message = sb.ToString();
                log_Reports.EndCreateReport();
            }

            //
            //
            //
            //
            log_Method.EndMethod(log_Reports);
            return null;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 未実装の場合。
        /// </summary>
        /// <param name="parent_Conf"></param>
        /// <param name="log_Reports"></param>
        static public void NoImpl_Expression_AddChild(
            Configurationtree_Node parent_Conf,
            Log_Reports log_Reports
            )
        {
            //
            // エラー。

            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, "Util_Expression_Node_String", "NoImpl_Expression_AddChild",log_Reports);

            //
            //
            //
            //

            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー361！", log_Method);

                StringBuilder sb = new StringBuilder();

                sb.Append("Expression_AddChild メソッドは、未実装です。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                // ヒント
                sb.Append(r.Message_Configuration(parent_Conf));

                r.Message = sb.ToString();
                log_Reports.EndCreateReport();
            }

            //
            //
            //
            //
            log_Method.EndMethod(log_Reports);
        }
        //────────────────────────────────────────
        #endregion



    }
}
