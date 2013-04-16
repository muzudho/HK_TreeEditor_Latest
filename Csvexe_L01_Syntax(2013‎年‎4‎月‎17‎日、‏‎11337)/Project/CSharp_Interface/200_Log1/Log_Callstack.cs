using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{



    /// <summary>
    /// コールスタックです。
    /// </summary>
    public interface Log_Callstack
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// メソッドの場所情報を追加します。
        /// </summary>
        /// <param name="log_Method"></param>
        void Push(Log_Method log_Method);

        /// <summary>
        /// メソッドの場所情報を追加します。更にメソッド中のどこかという情報も付記します。
        /// </summary>
        /// <param name="log_Method"></param>
        /// <param name="sComment_Statement"></param>
        void Push(Log_Method log_Method, string sComment_Statement);

        /// <summary>
        /// メソッドの場所情報を追加します。
        /// </summary>
        /// <param name="log_Method"></param>
        void Pop(Log_Method log_Method);

        /// <summary>
        /// メソッドの場所情報を追加します。更にメソッド中のどこかという情報も付記します。
        /// </summary>
        /// <param name="log_Method"></param>
        /// <param name="sComment_Statement"></param>
        void Pop(Log_Method log_Method, string sComment_Statement);

        //────────────────────────────────────────
        #endregion



    }
}
