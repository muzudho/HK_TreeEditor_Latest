using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// "p1p"、"p2p"、"p3p"といった名前かどうかをテストします。
    /// </summary>
    public abstract class Utility_TexttemplateP1p
    {



        #region アクション
        //────────────────────────────────────────

        public static bool TryParseName(string sName, out int nResult)
        {
            nResult = 0;

            int nParamNameMatchedCount = 0;
            {
                //正規表現
                System.Text.RegularExpressions.Regex regexp =
                    new System.Text.RegularExpressions.Regex(
                        @"p([0-9]+)p",
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase
                        );

                //文字列検索を1回する。
                System.Text.RegularExpressions.Match match = regexp.Match(sName);

                while (match.Success)
                {
                    string sP1pNumber = match.Groups[1].Value;

                    
                    bool bParseSuccessful = int.TryParse(sP1pNumber, out nResult);
                    if (!bParseSuccessful)
                    {
                        return false;
                    }

                    match = match.NextMatch();
                    nParamNameMatchedCount++;
                }
            }

            return 1 == nParamNameMatchedCount;
        }

        //────────────────────────────────────────
        #endregion



    }
}
