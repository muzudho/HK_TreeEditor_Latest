using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{



    /// <summary>
    /// ストップウォッチ。
    /// </summary>
    public interface Log_Stopwatch
    {



        #region 開始と終了
        //────────────────────────────────────────

        /// <summary>
        /// 計測開始。
        /// </summary>
        void Begin();

        /// <summary>
        /// 計測終了。
        /// </summary>
        void End(Log_Reports log_reports);

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        /// <summary>
        /// 計測中なら真。
        /// </summary>
        bool IsRunning
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// タイマー表示。
        /// </summary>
        string Message
        {
            set;
        }

        //────────────────────────────────────────
        #endregion



    }
}
