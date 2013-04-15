using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;//DllImport
using System.Windows.Forms;

namespace TreeEditor
{

    /// <summary>
    /// スクロールバー。
    /// </summary>
    public class Scrollbar
    {

        /// <summary>
        /// 前回のx位置。
        /// </summary>
        private int preX;
        //public int PreX
        //{
        //    get
        //    {
        //        return preX;
        //    }
        //    set
        //    {
        //        preX = value;
        //    }
        //}


        /// <summary>
        /// 前回のy位置。
        /// </summary>
        private int preY;
        //public int PreY
        //{
        //    get
        //    {
        //        return preY;
        //    }
        //    set
        //    {
        //        preY = value;
        //    }
        //}



        /// <summary>
        /// スクロールバーのために。
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nBar"></param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern int GetScrollPos(IntPtr hWnd, int nBar);

        /// <summary>
        /// スクロールバーのために。
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(
            IntPtr hWnd, int wMsg, int wParam, IntPtr lParam);


        public delegate void ScrollAction(object sender, EventArgs e, int pos, int movement);
        private ScrollAction onVScrollAction;
        public ScrollAction OnVScrollAction
        {
            get
            {
                return this.onVScrollAction;
            }
            set
            {
                this.onVScrollAction = value;
            }
        }

        private ScrollAction onHScrollAction;
        public ScrollAction OnHScrollAction
        {
            get
            {
                return this.onHScrollAction;
            }
            set
            {
                this.onHScrollAction = value;
            }
        }

        public void OnVScroll(object sender, EventArgs e)
        {
            //RichTextBox tb = (RichTextBox)sender;

            const int SB_THUMBPOSITION = 4;
            const int SB_VERT = 1;
            const int WM_VSCROLL = 0x0115;
            int pos = GetScrollPos(((Control)sender).Handle, SB_VERT);
            SendMessage(
                ((Control)sender).Handle,//tb.Handle,
                WM_VSCROLL, (pos << 16) | SB_THUMBPOSITION,
                IntPtr.Zero);

            this.OnVScrollAction(sender, e, pos, pos-this.preY);

            this.preY = pos;
            //System.Console.WriteLine("★richTextBox1_VScroll e.ToString()=" + e.ToString() + " pos=" + pos + " sender=" + sender.ToString());
            //System.Console.WriteLine("★richTextBox1_VScroll tb.AutoScrollOffset=（" + tb.Scroll.AutoScrollOffset.X + "、" + tb.AutoScrollOffset.Y + "）");
            //System.Console.WriteLine("★richTextBox1_VScroll tb.AutoScrollOffset=（" + tb.AutoScrollOffset.X + "、" + tb.AutoScrollOffset.Y + "）");
        }

        public void OnHScroll(object sender, EventArgs e)
        {
            const int SB_THUMBPOSITION = 4;
            const int SB_HORZ = 0;
            const int WM_HSCROLL = 0x0114;
            int pos = GetScrollPos(((Control)sender).Handle, SB_HORZ);
            SendMessage(
                ((Control)sender).Handle,//tb.Handle,
                WM_HSCROLL, (pos << 16) | SB_THUMBPOSITION,
                IntPtr.Zero);

            this.OnHScrollAction(sender, e, pos, pos - this.preX);

            this.preX = pos;
        }

    }
}
