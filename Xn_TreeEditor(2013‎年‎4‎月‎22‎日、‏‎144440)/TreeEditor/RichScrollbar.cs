using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Runtime.InteropServices;//DllImport
using System.Windows.Forms;

namespace TreeEditor
{

    /// <summary>
    /// リッチテキストボックス用の、スクロールバー。
    /// </summary>
    public class RichScrollbar
    {

        /// <summary>
        /// 先頭座標
        /// </summary>
        private Point head;
        public Point Head
        {
            get
            {
                return head;
            }
            set
            {
                head = value;
            }
        }


        /// <summary>
        /// 前回のx位置。
        /// </summary>
        private int preX;


        /// <summary>
        /// 前回のy位置。
        /// </summary>
        private int preY;



        ///// <summary>
        ///// スクロールバーのために。
        ///// </summary>
        ///// <param name="hWnd"></param>
        ///// <param name="nBar"></param>
        ///// <returns></returns>
        //[DllImport("user32")]
        //public static extern int GetScrollPos(IntPtr hWnd, int nBar);

        ///// <summary>
        ///// スクロールバーのために。
        ///// </summary>
        ///// <param name="hWnd"></param>
        ///// <param name="wMsg"></param>
        ///// <param name="wParam"></param>
        ///// <param name="lParam"></param>
        ///// <returns></returns>
        //[DllImport("user32", EntryPoint = "SendMessageA")]
        //public static extern int SendMessage(
        //    IntPtr hWnd, int wMsg, int wParam, IntPtr lParam);


        public delegate void ScrollAction(object sender, EventArgs e, int headXorY);//int pos, int movement
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

        public RichScrollbar()
        {
            this.head = new Point();
        }

        public void OnVScroll(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;

            // 表示されている[0]行目は、実際の何行目か？
            int charIndex = rtb.GetCharIndexFromPosition(new Point(1,1));
            //ystem.Console.WriteLine("OnVScroll　charIndex=" + charIndex);

            // 表示されている左上隅の文字
            //ystem.Console.WriteLine("OnVScroll　rtb.Text[charIndex]=「" + rtb.Text[charIndex] + "」");

            Point p2 = rtb.GetPositionFromCharIndex(charIndex);
            //ystem.Console.WriteLine("OnVScroll　rtb.GetPositionFromCharIndex=（" + p2.X + "、" + p2.Y + "）");

            // 先頭を 0 とする行番号。
            int lineNumber = rtb.GetLineFromCharIndex(charIndex);
            //ystem.Console.WriteLine("OnVScroll　lineNumber=（" + lineNumber + "）");

            int headY = 0;
            for (int i = 1; i < lineNumber; i++)
            {
                int lineHeight = rtb.GetPositionFromCharIndex(rtb.GetFirstCharIndexFromLine(i)).Y - rtb.GetPositionFromCharIndex(rtb.GetFirstCharIndexFromLine(i-1)).Y;
                headY += lineHeight;
            }

            // 表示されている先頭行は、全体の何yか。
            //System.Console.WriteLine("OnVScroll　headY=（" + headY + "）");


            //int line0Height = rtb.GetPositionFromCharIndex(rtb.GetFirstCharIndexFromLine(1)).Y - rtb.GetPositionFromCharIndex(rtb.GetFirstCharIndexFromLine(0)).Y;
            //int line1Height = rtb.GetPositionFromCharIndex(rtb.GetFirstCharIndexFromLine(2)).Y - rtb.GetPositionFromCharIndex(rtb.GetFirstCharIndexFromLine(1)).Y;
            //ystem.Console.WriteLine("OnVScroll　1行目の行の高さ=" + line0Height);

            this.Head = new Point(this.Head.X, headY);
            this.OnVScrollAction(sender, e, headY);// pos, mv


            //try
            //{
            //    const int SB_THUMBPOSITION = 4;
            //    const int SB_VERT = 1;
            //    const int WM_VSCROLL = 0x0115;
            //    int pos = GetScrollPos(rtb.Handle, SB_VERT);
            //    SendMessage(
            //        rtb.Handle,//tb.Handle,
            //        WM_VSCROLL, (pos << 16) | SB_THUMBPOSITION,
            //        IntPtr.Zero);

            //    //移動量
            //    int mv = pos - this.preY;
            //    this.OnVScrollAction(sender, e, pos, mv);

            //    this.preY = pos;

            //    ((Form1)((UiTextside)rtb.Parent.Parent.Parent).ParentForm).UiMain1.Contents.ScrollY += mv;
            //    //ystem.Console.WriteLine("★OnVScroll y=" + ((Form1)((UiTextside)rtb.Parent.Parent.Parent).ParentForm).UiMain1.Contents.ScrollY + " pos=" + pos);

            //    //System.Console.WriteLine("★richTextBox1_VScroll e.ToString()=" + e.ToString() + " pos=" + pos + " sender=" + sender.ToString());
            //    //System.Console.WriteLine("★richTextBox1_VScroll tb.AutoScrollOffset=（" + tb.Scroll.AutoScrollOffset.X + "、" + tb.AutoScrollOffset.Y + "）");
            //    //System.Console.WriteLine("★richTextBox1_VScroll tb.AutoScrollOffset=（" + tb.AutoScrollOffset.X + "、" + tb.AutoScrollOffset.Y + "）");
            //}
            //catch(Exception e2)
            //{
            //    MessageBox.Show(e2.Message, "強制終了を回避！　OnVScroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        public void OnHScroll(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;

            //// 表示されている[0]行目は、実際の何行目か？
            //int charIndex = rtb.GetCharIndexFromPosition(new Point(1, 1));
            ////ystem.Console.WriteLine("OnVScroll　charIndex=" + charIndex);

            //// 表示されている左上隅の文字
            ////ystem.Console.WriteLine("OnVScroll　rtb.Text[charIndex]=「" + rtb.Text[charIndex] + "」");

            //Point p2 = rtb.GetPositionFromCharIndex(charIndex);
            ////ystem.Console.WriteLine("OnVScroll　rtb.GetPositionFromCharIndex=（" + p2.X + "、" + p2.Y + "）");

            //// 先頭を 0 とする行番号。
            //int lineNumber = rtb.GetLineFromCharIndex(charIndex);
            ////ystem.Console.WriteLine("OnVScroll　lineNumber=（" + lineNumber + "）");

            //int headY = 0;
            //for (int i = 1; i < lineNumber; i++)
            //{
            //    int lineHeight = rtb.GetPositionFromCharIndex(rtb.GetFirstCharIndexFromLine(i)).Y - rtb.GetPositionFromCharIndex(rtb.GetFirstCharIndexFromLine(i - 1)).Y;
            //    headY += lineHeight;
            //}

            int headY = 0;
            this.Head = new Point(this.Head.X, headY);

            //try
            //{
            //    const int SB_THUMBPOSITION = 4;
            //    const int SB_HORZ = 0;
            //    const int WM_HSCROLL = 0x0114;
            //    int pos = GetScrollPos(rtb.Handle, SB_HORZ);
            //    SendMessage(
            //        rtb.Handle,//tb.Handle,
            //        WM_HSCROLL,
            //        (pos << 16) |
            //        SB_THUMBPOSITION //絶対位置へスクロール
            //        ,
            //        IntPtr.Zero);

            //    //移動量
            //    int mv = pos - this.preX;
            //    this.OnHScrollAction(sender, e, pos, mv);

            //    this.preX = pos;

            //    ((Form1)((UiTextside)rtb.Parent.Parent.Parent).ParentForm).UiMain1.Contents.ScrollX += mv;
            //    //ystem.Console.WriteLine("★OnHScroll x=" + ((Form1)((UiTextside)rtb.Parent.Parent.Parent).ParentForm).UiMain1.Contents.ScrollY + " pos=" + pos);
            //}
            //catch (Exception e2)
            //{
            //    MessageBox.Show(e2.Message, "強制終了を回避！　OnHScroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

    }
}
