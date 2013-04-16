using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    public class Utility_Hitcount
    {

        /// <summary>
        /// ヒットした件数がなかったとき、エラーになるか否か。
        /// </summary>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public static bool IsError_IfNoHit(EnumHitcount hits, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, "Utility_Hitcount", "IsError_IfNoHit", log_Reports);

            bool result;

            switch(hits)
            {
                case EnumHitcount.Exists:
                    result = true;//エラーになる。
                    break;
                case EnumHitcount.First_Exist:
                    result = true;//エラーになる。
                    break;
                case EnumHitcount.First_Exist_Or_Zero:
                    result = false;//セーフ。
                    break;
                case EnumHitcount.One:
                    result = true;//エラーになる。
                    break;
                case EnumHitcount.One_Or_Zero:
                    result = false;//セーフ。
                    break;
                case EnumHitcount.Unconstraint:
                    result = false;//セーフ。
                    break;
                default:
                    //エラー
                    result = true;//意味が変わるが、エラーにする。
                    goto gt_Error_Default;
            }

            goto gt_EndMethod;
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Default:
            {
                if (log_Reports.CanCreateReport)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー031！", log_Method);

                    Log_TextIndented t = new Log_TextIndentedImpl();
                    t.Append("Enum型の対応していない値[");
                    t.Append(hits.ToString());
                    t.Append("]");
                    t.Newline();

                    r.Message = t.ToString();
                    log_Reports.EndCreateReport();
                }
            }
            goto gt_EndMethod;
            //────────────────────────────────────────
            #endregion
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result;
        }

    }
}
