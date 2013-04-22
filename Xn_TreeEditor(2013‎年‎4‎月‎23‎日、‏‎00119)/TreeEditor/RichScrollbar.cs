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
        }

        public void OnHScroll(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;

            // 現在行の最初の文字（0 から数えて）
            int firstChar = rtb.GetFirstCharIndexOfCurrentLine();
            System.Console.WriteLine("OnHScroll　firstChar=" + firstChar);

            int firstCharX = rtb.GetPositionFromCharIndex(firstChar).X;
            System.Console.WriteLine("OnHScroll　firstCharX=" + firstCharX);

            int headX = -firstCharX;//ひっくり返す

            this.Head = new Point(headX, this.Head.Y);


            ////━━━━━
            ////左上端（テキストエリアの最上行）
            ////━━━━━
            //// 表示されている[0]行目は、実際の何行目か？
            //int topChar = rtb.GetCharIndexFromPosition(new Point(1, 1));
            //System.Console.WriteLine("OnHScroll　topChar=" + topChar);

            //━━━━━
            //現在行（カーソルがある行）
            //━━━━━



            //// カーソル位置にある文字（0 から数えて何文字目か？）
            //int curChar = rtb.SelectionStart;
            //System.Console.WriteLine("OnHScroll　curChar=" + curChar);

            //// カーソル行（0から数えて）
            //int curLine = rtb.GetLineFromCharIndex(curChar);
            //System.Console.WriteLine("OnHScroll　現行=（" + curLine + "）");


            // カーソル座標
            //Point curPos = rtb.GetPositionFromCharIndex(curChar);


            //System.Console.WriteLine("OnHScroll　curChar=" + curFirstChar);


            //// 現行の画面左端の文字は、0 から数えて何文字目か？
            //int leftCharIndex;
            //{
            //    int headY = 0;
            //    for (int i = 1; i < curLine; i++)
            //    {
            //        int lineHeight = rtb.GetPositionFromCharIndex(rtb.GetFirstCharIndexFromLine(i)).Y - rtb.GetPositionFromCharIndex(rtb.GetFirstCharIndexFromLine(i - 1)).Y;
            //        headY += lineHeight;
            //    }

            //    // 現行の画面左端の文字は、0 から数えて何文字目か？
            //    leftCharIndex = rtb.GetCharIndexFromPosition(
            //        new Point(1, headY)
            //        );
            //}



            //for (int i = curFirstChar + 1; i < curChar; i++)
            //{
            //    int fontWidth = rtb.GetPositionFromCharIndex(i).X - rtb.GetPositionFromCharIndex(i - 1).X;
            //    //ystem.Console.WriteLine("OnHScroll　（" + rtb.GetPositionFromCharIndex(i).X + "）－（" + rtb.GetPositionFromCharIndex(i - 1).X + "）＝　[" + i + "].fontWidth=" + fontWidth);
            //    headX += fontWidth;
            //}
            //System.Console.WriteLine("OnHScroll　headX=" + headX);

        }

    }
}
