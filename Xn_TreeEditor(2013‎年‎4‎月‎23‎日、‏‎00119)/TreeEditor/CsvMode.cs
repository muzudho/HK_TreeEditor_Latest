using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Xenon.Syntax;
using Xenon.Table;

namespace TreeEditor
{

    /// <summary>
    /// 画像ファイル参照モード
    /// </summary>
    public class CsvMode
    {

        /// <summary>
        /// 右マウスボタンを放したところのピクチャーボックス。
        /// </summary>
        private PictBox mouseupPictBox;
        public PictBox MouseupPictBox
        {
            get
            {
                return this.mouseupPictBox;
            }
            set
            {
                this.mouseupPictBox = value;
            }
        }

        /// <summary>
        /// ドラッグしているピクチャーボックス。なければヌル。
        /// </summary>
        private PictBox draggingPictBox;
        public PictBox DraggingPictBox
        {
            get
            {
                return draggingPictBox;
            }
            set
            {
                draggingPictBox = value;
            }
        }


        /// <summary>
        /// マウスボタンが押されたときの、コントロール内でのマウス押下座標。
        /// </summary>
        private Point mouseDownErrFromCenter;


        /// <summary>
        /// スクロールバー
        /// </summary>
        private RichScrollbar richScrollbar;
        public RichScrollbar RichScrollbar
        {
            get
            {
                return richScrollbar;
            }
            set
            {
                richScrollbar = value;
            }
        }



        public CsvMode()
        {
            this.mouseDownErrFromCenter = Point.Empty;
            this.richScrollbar = new RichScrollbar();
        }


        public void Load(UiTextside uiTextside1)
        {

            //━━━━━
            // ドラッグ＆ドロップ
            //━━━━━
            uiTextside1.RichTextBox1.AllowDrop = true;


            //━━━━━
            // スクロールバー
            //━━━━━
            this.richScrollbar.OnVScrollAction = (object sender2, EventArgs e2, int headY) =>//int pos, int movement
            {
                //━━━━━
                // ※ここでは、コントロールのプロパティーへの変更が正常には利きません。
                //━━━━━
            };
            this.richScrollbar.OnHScrollAction = (object sender2, EventArgs e2, int headX) =>//int pos, int movement
            {
                //━━━━━
                // ※ここでは、コントロールのプロパティーへの変更が正常には利きません。
                //━━━━━
            };
        }


        public void Timer1_Tick(UiTextside uiTextside1, object sender, EventArgs e)
        {
            //━━━━━
            // TODO:画像の表示位置再修正
            //━━━━━
            foreach (Control c in uiTextside1.RichTextBox1.Controls)
            {
                if (c is PictBox)
                {
                    PictBox pic = (PictBox)c;


                    //ystem.Console.WriteLine("★UiTextside 垂直 pos=" + pos + " movement=" + movement + " pic.Location（" + pic.Location.X + "、" + pic.Location.Y + "）");
                    // リッチテキストエリア内での、ピクチャーボックスの左上座標
                    pic.ReJustLocation(this.RichScrollbar.Head);
                    //ystem.Console.WriteLine("★UiTextside 垂直2 pic.Location（" + pic.Location.X + "、" + pic.Location.Y + "）");
                    pic.Refresh();
                }
            }

            ////━━━━━
            //// スクロールバーの移動量を、全画像に逆方向に加算
            ////━━━━━
            //if (this.ImageMovement.X != 0 || this.ImageMovement.Y != 0)
            //{
            //    //━━━━━
            //    // 全画像のYに移動量を逆方向に加算
            //    //━━━━━
            //    foreach (Control c in uiTextside1.RichTextBox1.Controls)
            //    {
            //        if (c is PictBox)
            //        {
            //            PictBox pic = (PictBox)c;


            //            //ystem.Console.WriteLine("★UiTextside 垂直 pos=" + pos + " movement=" + movement + " pic.Location（" + pic.Location.X + "、" + pic.Location.Y + "）");
            //            // リッチテキストエリア内での、ピクチャーボックスの左上座標
            //            pic.Location = new Point(
            //                pic.Location.X + this.ImageMovement.X,
            //                pic.Location.Y + this.ImageMovement.Y
            //                );
            //            //ystem.Console.WriteLine("★UiTextside 垂直2 pic.Location（" + pic.Location.X + "、" + pic.Location.Y + "）");
            //            pic.Refresh();
            //        }
            //    }

            //    this.ImageMovement = new Point();
            //}
        }


        public void Pic1_MouseDown(Form1 form1, object sender, MouseEventArgs e)
        {
            if (form1.UiMain1.UiTextside1.IsImageCsvMode)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        {
                            System.Console.WriteLine("★pic1_MouseDown　左ボタン");
                            this.DraggingPictBox = (PictBox)sender;
                            this.DraggingPictBox.BorderStyle = BorderStyle.FixedSingle;

                            // ピクチャーボックス内での、マウスボタン押下座標。
                            System.Console.WriteLine("e.Location=(" + e.Location.X + "、" + e.Location.Y + ")");

                            // 中心位置からのずれ
                            this.mouseDownErrFromCenter = new Point(
                                e.Location.X - this.DraggingPictBox.Width / 2,
                                e.Location.Y - this.DraggingPictBox.Height / 2
                                );
                        }
                        break;
                    case MouseButtons.Right:
                        {
                            System.Console.WriteLine("★pic1_MouseDown　右ボタン");
                        }
                        break;
                }
            }
        }

        public void Pic1_MouseUp(Form1 form1, object sender, MouseEventArgs e)
        {
            if (form1.UiMain1.UiTextside1.IsImageCsvMode)
            {
                PictBox pic1 = (PictBox)sender;

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        System.Console.WriteLine("★pic1_MouseUp　左ボタン");
                        if (pic1 == this.DraggingPictBox)
                        {
                            // ドラッグ中のピクチャーボックスだった場合
                            this.DraggingPictBox.BorderStyle = BorderStyle.None;
                            this.DraggingPictBox = null;
                        }
                        break;
                    case MouseButtons.Right:
                        {
                            System.Console.WriteLine("★pic1_MouseUp　右ボタン");

                            Point p1 = pic1.PointToScreen(e.Location);

                            this.MouseupPictBox = pic1;

                            form1.UiMain1.UiTextside1.ContextMenuStrip1.Show(
                                new Point(
                                    p1.X,
                                    p1.Y - form1.UiMain1.UiTextside1.ContextMenuStrip1.Height/2
                                    )
                                );

                        }
                        break;
                }
            }
        }

        public void Pic1_MouseLeave(Form1 form1, object sender, EventArgs e)
        {
            if (null != this.DraggingPictBox)
            {
                this.DraggingPictBox.BorderStyle = BorderStyle.None;
                this.DraggingPictBox.Refresh();
                this.DraggingPictBox = null;
            }
        }

        public void Pic1_MouseMove(Form1 form1, object sender, MouseEventArgs e)
        {
            if (form1.UiMain1.UiTextside1.IsImageCsvMode)
            {
                if (null != this.DraggingPictBox)
                {

                    // テキストボックス内でのマウス座標
                    Point p1 = form1.UiMain1.UiTextside1.RichTextBox1.PointToClient(Cursor.Position);
                    //ystem.Console.WriteLine("Pic1_MouseMove　ヘッド（" + this.RichScrollbar.Head.X + "、" + this.RichScrollbar.Head.Y + "）");
                    //ystem.Console.WriteLine("Pic1_MouseMove　Cursor（" + Cursor.Position.X + "、" + Cursor.Position.Y + "）");
                    //ystem.Console.WriteLine("Pic1_MouseMove　Client（" + p1.X + "、" + p1.Y + "）");

                    // 画像の中心座標のまま指定
                    this.DraggingPictBox.LocationCInAll = new Point(
                        this.RichScrollbar.Head.X + p1.X - this.mouseDownErrFromCenter.X,
                        this.RichScrollbar.Head.Y + p1.Y - this.mouseDownErrFromCenter.Y
                        );

                    form1.UiMain1.Contents.IsChangedCsv = true;
                    form1.UiMain1.RefreshTitleBar();
                    this.DraggingPictBox.Refresh();
                }
            }
        }


        public void RichTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            RichTextBox richTextBox1 = (RichTextBox)sender;

            //int index = this.richTextBox1.GetCharIndexFromPosition(e.Location);
            int index = richTextBox1.GetCharIndexFromPosition(new Point(1, 1));
            int line = richTextBox1.GetLineFromCharIndex(index);
            int column = index - richTextBox1.GetFirstCharIndexFromLine(line);
            Console.WriteLine("{0}行 {1}桁", line, column);

            //System.Console.WriteLine("★DisplayRectangle（" + this.richTextBox1.DisplayRectangle.X + "、" + this.richTextBox1.DisplayRectangle.Y + "、" + this.richTextBox1.DisplayRectangle.Width + "、" + this.richTextBox1.DisplayRectangle.Height + "）");
            //Point p = this.richTextBox1.GetPositionFromCharIndex(
            //    this.richTextBox1.GetCharIndexFromPosition(new Point(0,0))
            //    );
            //System.Console.WriteLine("★this.richTextBox1.GetCharIndexFromPosition(new Point(0,0))＝（" + this.richTextBox1.GetCharIndexFromPosition(new Point(0, 0)) + "）");
            //System.Console.WriteLine("★p＝（" + p.X + "、" + p.Y + "）");

            if (null != this.DraggingPictBox)
            {
                this.DraggingPictBox.BorderStyle = BorderStyle.None;
                this.DraggingPictBox = null;
            }
        }

        public void RichTextBox1_VScroll(object sender, EventArgs e)
        {
            //━━━━━
            // ここでは、コントロールのプロパティーへの変更が正常には利きません。
            //━━━━━
            this.RichScrollbar.OnVScroll(sender, e);
        }

        public void RichTextBox1_HScroll(object sender, EventArgs e)
        {
            //━━━━━
            // ここでは、コントロールのプロパティーへの変更が正常には利きません。
            //━━━━━
            this.RichScrollbar.OnHScroll(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeleteImage(Form1 form1, object sender, EventArgs e)
        {
            UiTextside uiTextside1 = form1.UiMain1.UiTextside1;
            PictBox pic1 = this.MouseupPictBox;


            //ystem.Console.WriteLine("画像削除ToolStripMenuItem_Click　pic1=" + pic1.ToString());

            uiTextside1.RichTextBox1.SuspendLayout();
            uiTextside1.RichTextBox1.Controls.Remove(pic1);
            form1.UiMain1.Contents.IsChangedCsv = true;
            form1.UiMain1.RefreshTitleBar();
            uiTextside1.RichTextBox1.ResumeLayout();

            //foreach (Control c in uiTextside1.RichTextBox1.Controls)
            //{
            //    System.Console.WriteLine("画像削除ToolStripMenuItem_Click　c=" + c.ToString());
            //}

            uiTextside1.RichTextBox1.Refresh();
        }


        /// <summary>
        /// 
        /// </summary>
        public void DuplicateImage(Form1 form1, object sender, EventArgs e)
        {
            UiTextside uiTextside1 = form1.UiMain1.UiTextside1;
            PictBox pic1 = this.MouseupPictBox;

            // 左上座標で指定します。ずらして作成します。
            uiTextside1.AddPictBox(
                pic1.ImageLocation,
                new Point(
                    pic1.Location.X + pic1.Width / 2,
                    pic1.Location.Y + pic1.Height / 2
                    ),
                    true
                );
        }
    }
}
