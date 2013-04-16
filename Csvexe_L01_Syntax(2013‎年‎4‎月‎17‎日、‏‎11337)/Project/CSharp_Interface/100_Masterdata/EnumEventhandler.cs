using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    /// <summary>
    /// 呼び出されたイベントハンドラーの種類。
    /// 実行メソッドPerformの引数の形です。
    /// </summary>
    public enum EnumEventhandler
    {



        #region 用意
        //────────────────────────────────────────

        /// <summary>
        /// 未知です。
        /// </summary>
        Unknown,

        /// <summary>
        /// 画像ドロップ用
        /// 
        /// object, DragEventArgs, Point, string, Bitmap, Log_Reports
        /// </summary>
        O_Dea_P_S_B_Lr,

        /// <summary>
        /// 画像ドロップ用
        /// 
        /// object, DragEventArgs, Point, string, string, Log_Reports
        /// </summary>
        O_Dea_P_S_S_Lr,

        /// <summary>
        /// よく使うアクション object, EventArgs
        /// </summary>
        O_Ea,

        /// <summary>
        /// ドラッグ＆ドロップ用
        /// 
        /// object, GiveFeedbackEventArgs
        /// </summary>
        O_Gfea,

        /// <summary>
        /// キー操作用
        /// 
        /// object, KeyEventArgs
        /// </summary>
        O_Kea,
        
        /// <summary>
        /// マウス用
        /// 
        /// object, MouseEventArgs
        /// </summary>
        O_Mea,

        /// <summary>
        /// ドラッグ＆ドロップ用
        /// 
        /// object, QueryContinueDragEventArgs
        /// </summary>
        O_Qcdea,

        /// <summary>
        /// よく使うメソッド。（旧：リストボックス用）
        /// 
        /// object, Log_Reports
        /// 
        /// 旧：Lr_Rhn もここ。
        /// </summary>
        O_Lr,
        
        /// <summary>
        /// エディター読取用
        /// 
        /// 旧：Tp_B_Lr_Rhn（ TcProject, bool, Log_Reports, Rhn）
        /// </summary>
        Editor_B_Lr,

        //────────────────────────────────────────
        #endregion



    }
}
