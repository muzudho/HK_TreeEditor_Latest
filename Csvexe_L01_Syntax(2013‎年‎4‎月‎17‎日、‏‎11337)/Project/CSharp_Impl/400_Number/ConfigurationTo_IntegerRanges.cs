using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{
    public class ConfigurationTo_IntegerRanges
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// S→O。
        /// 
        /// 文字列「-7~-5|-3~1|3|5~7|9|10|13~24」といった記述を入れると、
        /// O_IntegerRangesオブジェクトを返します。
        /// </summary>
        /// <param name="sText">「-7~-5|-3~1|3|5~7|9|10|13~24」といった記述。</param>
        /// <param name="out_O_Ranges">O_IntegerRangesオブジェクト。</param>
        /// <param name="ref_sInfoMsg">正常時：変化なし、情報・警告有り時：空文字以外の文字列が追加。</param>
        /// <param name="out_sErrorMsg">正常終了：空文字列、エラー時：空文字以外の文字列。</param>
        /// <returns>正常終了：真、異常終了：偽</returns>
        public bool ConfigurationTo(
            string sText,
            out IntegerRanges o_Ranges_Out,
            ref StringBuilder sb_InfoMsg_Ref,
            out string sErrorMsg_Out
            )
        {
            string[] sRanges = sText.Split('|');

            bool bParsedSuccessful = true;//解析が失敗していなければ真。

            o_Ranges_Out = new IntegerRangesImpl();

            string err_sRange;
            Exception err_Excp;
            string err_sFirst;
            string err_sLast;
            foreach (string sRange in sRanges)
            {
                string[] sFirstLast = sRange.Split('~');

                if (1==sFirstLast.Length)
                {
                    //
                    // 「1」など

                    string sSingle = sFirstLast[0];
                    int nSingle;
                    if (!int.TryParse(sSingle, out nSingle))
                    {
                        // エラー
                        err_sRange = sRange;
                        err_Excp = null;
                        goto gt_Error_MissParse;
                    }

                    // 解析が失敗していなければ。
                    IntegerRange o_Range = new IntegerRangeImpl(nSingle);
                    o_Ranges_Out.List_Item.Add(o_Range);
                }
                else if (2 == sFirstLast.Length)
                {
                    //
                    // 「1~10」など

                    string sFirst = sFirstLast[0];
                    string sLast = sFirstLast[1];

                    int nFirst;
                    if (!int.TryParse(sFirst, out nFirst))
                    {
                        // エラー
                        err_sRange = sRange;
                        err_Excp = null;
                        err_sFirst = sFirst;
                        goto gt_Error_MissParseFirst;
                    }

                    int nLast;
                    if (!int.TryParse(sLast, out nLast))
                    {
                        // エラー
                        err_sRange = sRange;
                        err_Excp = null;
                        err_sLast = sLast;
                        goto gt_Error_MissParseLast;
                    }

                    // 解析が失敗していなければ。
                    IntegerRange o_Range = new IntegerRangeImpl(nFirst,nLast);
                    o_Ranges_Out.List_Item.Add(o_Range);
                }
                else
                {
                    // エラー
                    err_sRange = sRange;
                    goto gt_Error_MissFormat01;
                }
            }

            sErrorMsg_Out = "";
            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_MissParseFirst:
            {
                bParsedSuccessful = false;

                StringBuilder sb = new StringBuilder();
                sb.Append("[Error:1084]["+Info_Syntax.Name_Library+":"+this.GetType().Name+"#Parse]");
                sb.Append("整数範囲の記述[");
                sb.Append(err_sRange);
                sb.Append("]の始値[" + err_sFirst + "]は、解析できませんでした。");
                sb.Append(Environment.NewLine);

                // ヒント
                sb.Append("[ErrorMessage:" + err_Excp.Message+ "]");

                sErrorMsg_Out = sb.ToString();
            }
            goto gt_EndMethod;

        gt_Error_MissParseLast:
            {
                bParsedSuccessful = false;

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("[Error:1085][" + Info_Syntax.Name_Library + ":" + this.GetType().Name + "#Parse]");
                s.Append("整数範囲の記述[");
                s.Append(err_sRange);
                s.Append("]の終値[" + err_sLast + "]は、解析できませんでした。");
                s.Append(Environment.NewLine);

                // ヒント
                s.Append("[ErrorMessage:" + err_Excp.Message + "]");

                sErrorMsg_Out = s.ToString();
            }
            goto gt_EndMethod;

        gt_Error_MissParse:
            {
                bParsedSuccessful = false;

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("[Error:1086][" + Info_Syntax.Name_Library + ":" + this.GetType().Name + "#Parse]");
                s.Append("整数範囲の記述[");
                s.Append(err_sRange);
                s.Append("]は、解析できませんでした。");
                s.Append(Environment.NewLine);

                // ヒント
                if (null != err_Excp)
                {
                    s.Append("[ErrorMessage:" + err_Excp.Message + "]");
                }

                sErrorMsg_Out = s.ToString();
            }
            goto gt_EndMethod;

        gt_Error_MissFormat01:
            {
                bParsedSuccessful = false;

                StringBuilder sb = new StringBuilder();
                sb.Append("[Error:1087][" + Info_Syntax.Name_Library + ":" + this.GetType().Name + "#Parse]");
                sb.Append("整数範囲の記述[");
                sb.Append(err_sRange);
                sb.Append("]は、解析できませんでした。");

                sErrorMsg_Out = sb.ToString();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
            //
        gt_EndMethod:
            return bParsedSuccessful;
        }
        //────────────────────────────────────────
        #endregion



    }
}
