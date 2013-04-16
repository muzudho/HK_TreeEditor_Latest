using System;
using System.Collections.Generic;
using System.Data;//DataRowView,DataTable
using System.Linq;
using System.Text;

using Xenon.Syntax;//HumanInputFilePath,WarningReports

namespace Xenon.Table
{

    /// <summary>
    /// 縦棒「 | 」区切り文字列。
    /// </summary>
    public interface PipeSeparatedString
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 例えば、次の２つの物を与えると、
        /// ●ID＝10、EXPL＝赤、と入っている行。
        /// ●「%1%:%2%|ID|EXPL」という文字列。
        /// 
        /// すると、次の文字列が返ってくる。
        /// ●「10:EXPL」
        /// 
        /// %1%はID、%2%はEXPLに当たる。
        /// </summary>
        /// <param name="sFormat"></param>
        /// <param name="dataRowView"></param>
        /// <param name="xenonTable"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        string Perform(
            string sFormat,
            DataRowView dataRowView,
            Table_Humaninput xenonTable,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────
        #endregion



    }
}
