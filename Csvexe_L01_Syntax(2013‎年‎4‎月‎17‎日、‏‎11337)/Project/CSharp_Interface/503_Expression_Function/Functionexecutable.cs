using System;
using System.Collections.Generic;
using System.Drawing;//Point,Brush,Image
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xenon.Syntax
{
    /// <summary>
    /// イベントに合わせて、関数の実行の仕方を変えます。イベントハンドラーの関数シグニチャーの違いを吸収します。
    /// 
    /// ２種類あります。
    /// ①コントロールに登録されるもの。 Xenon.Controls.Functionwrapper_FormImpl 他。
    /// ②関数として実行されるもの。 Functionwrapper_OnFunction。
    /// 
    /// FunctionwrapperAbstract を継承して使う。
    /// </summary>
    public interface Functionexecutable
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// ドラッグ＆ドロップ　アクション実行。
        /// 
        /// System.EventHandlerの引数と同じにしてある。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Execute4_OnDnD(
            object sender,
            GiveFeedbackEventArgs e
            );

        /// <summary>
        /// 画像ドロップ　アクション実行。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="parentLocation"></param>
        /// <param name="debugMessage1"></param>
        /// <param name="debugStatusResultMessage"></param>
        /// <param name="log_Reports"></param>
        void Execute4_OnImgDrop(
            object sender,
            DragEventArgs e,
            Point parentLocation,
            string sMessage_Debug1,
            string sMessage_DebugStatusResult,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 画像ドロップ　アクション実行。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="fileName"></param>
        /// <param name="droppedBitmap"></param>
        void Execute4_OnImgDropB(
            object sender,
            DragEventArgs e,
            Point parentLocation,
            string sFpatha_Image,
            Bitmap droppedBitmap,
            Log_Reports log_Reports
            );

        /// <summary>
        /// リストボックス用アクション実行。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        string Execute4_OnLstBox(
            object sender,
            object itemValue,
            Log_Reports log_Reports
            );

        /// <summary>
        /// マウス　アクション実行。
        /// 
        /// System.EventHandlerの引数と同じにしてある。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Execute4_OnMouse(
            object sender,
            MouseEventArgs e
            );

        /// <summary>
        /// アクション実行。
        /// 
        /// 引数が object と EventArgs の場合。
        /// 
        /// System.EventHandlerの引数と同じにしてある。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Execute4_OnOEa(
            object sender,
            EventArgs e
            );

        /// <summary>
        /// エディター切替アクション実行。
        /// </summary>
        /// <param name="st_selectedProjectElm"></param>
        /// <param name="projectValid">エディターの読み込みに成功していれば真。</param>
        /// <param name="log_Reports"></param>
        void Execute4_OnEditorSelected(
            object sender,
            object st_SelectedEditorElm,
            bool bEditorValid,
            Log_Reports log_Reports
            );

        /// <summary>
        /// ドラッグ＆ドロップ用アクション実行。
        /// 
        /// System.EventHandlerの引数と同じにしてある。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Execute4_OnQueryContinueDragEventArgs(
            object sender,
            QueryContinueDragEventArgs e
            );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_Reports"></param>
        void Execute4_OnLr(
            object sender,
            Log_Reports log_Reports
        );

        //────────────────────────────────────────
        #endregion



    }
}
