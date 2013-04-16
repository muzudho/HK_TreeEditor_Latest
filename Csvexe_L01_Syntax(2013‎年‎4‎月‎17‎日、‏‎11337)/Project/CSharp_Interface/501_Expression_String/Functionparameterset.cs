using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

namespace Xenon.Syntax
{


    /// <summary>
    /// Expression_CommonFunctionの引数？
    /// 
    /// 旧名：ExpressionfncPrmset
    /// </summary>
    public interface Functionparameterset
    {



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// イベントハンドラー引数。
        /// object sender
        /// </summary>
        object Sender
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_DnD_Main で利用。
        /// </summary>
        GiveFeedbackEventArgs GiveFeedbackEventArgs
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_ImgDrop_Main, Perform_ImgDropB_Main で利用。
        /// </summary>
        DragEventArgs DragEventArgs
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_ImgDrop_Main, Perform_ImgDropB_Main で利用。
        /// </summary>
        Point ParentLocation
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_ImgDrop_Main で利用。
        /// </summary>
        string Message_Debug1
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_ImgDrop_Main で利用。
        /// </summary>
        string Message_DebugStatusResult
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_ImgDropB_Main で利用。
        /// </summary>
        string Filepathabsolute_Image
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_ImgDropB_Main で利用。
        /// </summary>
        Bitmap DroppedBitmap
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_LstBox_Main で利用。
        /// </summary>
        object ItemValue
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_Key_Main で利用。
        /// </summary>
        KeyEventArgs KeyEventArgs
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_Mouse_Main で利用。
        /// </summary>
        MouseEventArgs MouseEventArgs
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_OEa_Main で利用。
        /// </summary>
        EventArgs EventArgs
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_PrjSelected_Main で利用。
        /// </summary>
        object SelectedProjectElement_Configurationtree
        {
            get;
            set;
        }

        /// <summary>
        /// Perform_PrjSelected_Main で利用。
        /// 
        /// プロジェクトの読み込みに成功していれば真。
        /// </summary>
        bool IsProjectValid
        {
            get;
            set;
        }

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_QueryContinueDragEventArgs_Main で利用。
        /// </summary>
        QueryContinueDragEventArgs QueryContinueDragEventArgs
        {
            get;
            set;
        }

        //────────────────────────────────────────
        #endregion



    }
}
