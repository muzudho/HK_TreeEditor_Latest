using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{


    /// <summary>
    /// 報告が「エラー」なのか「警告」なのかの区別。
    /// </summary>
    public enum EnumReport
    {



        #region 用意
        //────────────────────────────────────────

        /// <summary>
        /// プログラムを異常停止させるメッセージ。
        /// </summary>
        Error,

        /// <summary>
        /// プログラムを正常続行させるが、問題点があることを知らせるメッセージ。
        /// </summary>
        Warning,

        /// <summary>
        /// 内容は使われず、捨てられるもの。
        /// </summary>
        Dammy

        //────────────────────────────────────────
        #endregion



    }
}
