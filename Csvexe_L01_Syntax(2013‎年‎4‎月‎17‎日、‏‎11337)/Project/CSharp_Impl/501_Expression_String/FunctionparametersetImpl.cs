using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

namespace Xenon.Syntax
{

    /// <summary>
    /// Expression_Functionの引数？
    /// </summary>
    public class FunctionparametersetImpl : Functionparameterset
    {



        #region プロパティー
        //────────────────────────────────────────

        private object sender;

        /// <summary>
        /// イベントハンドラー引数。
        /// object sender
        /// </summary>
        public object Sender
        {
            get
            {
                return sender;
            }
            set
            {
                sender = value;
            }
        }

        //────────────────────────────────────────

        private GiveFeedbackEventArgs giveFeedbackEventArgs;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_DnD_Main で利用。
        /// </summary>
        public GiveFeedbackEventArgs GiveFeedbackEventArgs
        {
            get
            {
                return giveFeedbackEventArgs;
            }
            set
            {
                giveFeedbackEventArgs = value;
            }
        }

        //────────────────────────────────────────

        private DragEventArgs dragEventArgs;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_ImgDrop_Main, Perform_ImgDropB_Main で利用。
        /// </summary>
        public DragEventArgs DragEventArgs
        {
            get
            {
                return dragEventArgs;
            }
            set
            {
                dragEventArgs = value;
            }
        }

        //────────────────────────────────────────

        private Point parentLocation;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_ImgDrop_Main, Perform_ImgDropB_Main で利用。
        /// </summary>
        public Point ParentLocation
        {
            get
            {
                return parentLocation;
            }
            set
            {
                parentLocation = value;
            }
        }

        //────────────────────────────────────────

        private string sMessage_Debug1;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_ImgDrop_Main で利用。
        /// </summary>
        public string Message_Debug1
        {
            get
            {
                return sMessage_Debug1;
            }
            set
            {
                sMessage_Debug1 = value;
            }
        }

        //────────────────────────────────────────

        private string sMessage_DebugStatusResult;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_ImgDrop_Main で利用。
        /// </summary>
        public string Message_DebugStatusResult
        {
            get
            {
                return sMessage_DebugStatusResult;
            }
            set
            {
                sMessage_DebugStatusResult = value;
            }
        }

        //────────────────────────────────────────

        private string sFpatha_Image;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_ImgDropB_Main で利用。
        /// </summary>
        public string Filepathabsolute_Image
        {
            get
            {
                return sFpatha_Image;
            }
            set
            {
                sFpatha_Image = value;
            }
        }

        //────────────────────────────────────────

        private Bitmap droppedBitmap;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_ImgDropB_Main で利用。
        /// </summary>
        public Bitmap DroppedBitmap
        {
            get
            {
                return droppedBitmap;
            }
            set
            {
                droppedBitmap = value;
            }
        }

        //────────────────────────────────────────

        private object itemValue;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_LstBox_Main で利用。
        /// </summary>
        public object ItemValue
        {
            get
            {
                return itemValue;
            }
            set
            {
                itemValue = value;
            }
        }

        //────────────────────────────────────────

        private KeyEventArgs keyEventArgs;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_Key_Main で利用。
        /// </summary>
        public KeyEventArgs KeyEventArgs
        {
            get
            {
                return keyEventArgs;
            }
            set
            {
                keyEventArgs = value;
            }
        }

        //────────────────────────────────────────

        private MouseEventArgs mouseEventArgs;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_Mouse_Main で利用。
        /// </summary>
        public MouseEventArgs MouseEventArgs
        {
            get
            {
                return mouseEventArgs;
            }
            set
            {
                mouseEventArgs = value;
            }
        }

        //────────────────────────────────────────

        private EventArgs eventArgs;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_OEa_Main で利用。
        /// </summary>
        public EventArgs EventArgs
        {
            get
            {
                return eventArgs;
            }
            set
            {
                eventArgs = value;
            }
        }

        //────────────────────────────────────────

        private object st_SelectedProjectElm;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_PrjSelected_Main で利用。
        /// </summary>
        public object SelectedProjectElement_Configurationtree
        {
            get
            {
                return st_SelectedProjectElm;
            }
            set
            {
                st_SelectedProjectElm = value;
            }
        }

        //────────────────────────────────────────

        private bool isProjectValid;

        /// <summary>
        /// Perform_PrjSelected_Main で利用。
        /// 
        /// プロジェクトの読み込みに成功していれば真。
        /// </summary>
        public bool IsProjectValid
        {
            get
            {
                return isProjectValid;
            }
            set
            {
                isProjectValid = value;
            }
        }

        //────────────────────────────────────────

        private QueryContinueDragEventArgs queryContinueDragEventArgs;

        /// <summary>
        /// イベントハンドラー引数。
        /// Perform_QueryContinueDragEventArgs_Main で利用。
        /// </summary>
        public QueryContinueDragEventArgs QueryContinueDragEventArgs
        {
            get
            {
                return queryContinueDragEventArgs;
            }
            set
            {
                queryContinueDragEventArgs = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
