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
        private PictureBox mouseupPicturebox;
        public PictureBox MouseupPicturebox
        {
            get
            {
                return this.mouseupPicturebox;
            }
            set
            {
                this.mouseupPicturebox = value;
            }
        }

        /// <summary>
        /// ドラッグしているピクチャーボックス。なければヌル。
        /// </summary>
        private PictureBox draggingPictureBox;
        public PictureBox DraggingPictureBox
        {
            get
            {
                return draggingPictureBox;
            }
            set
            {
                draggingPictureBox = value;
            }
        }


        /// <summary>
        /// マウスボタンが押されたときの、コントロール内でのマウス押下座標。
        /// </summary>
        private Point mouseDownOffsetLocation;


        /// <summary>
        /// スクロールバー
        /// </summary>
        private RichScrollbar scrollbar;
        public RichScrollbar Scrollbar
        {
            get
            {
                return scrollbar;
            }
            set
            {
                scrollbar = value;
            }
        }
        /// <summary>
        /// 画像移動量
        /// </summary>
        private Point imageMovement;
        public Point ImageMovement
        {
            get
            {
                return imageMovement;
            }
            set
            {
                imageMovement = value;
            }
        }



        public CsvMode()
        {
            this.mouseDownOffsetLocation = Point.Empty;
            this.imageMovement = new Point();
            this.scrollbar = new RichScrollbar();
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
            this.scrollbar.OnVScrollAction = (object sender2, EventArgs e2, int pos, int movement) =>
            {
                //━━━━━
                // ※ここでは、コントロールのプロパティーへの変更が正常には利きません。
                //━━━━━

                if (0 != movement)
                {
                    //ystem.Console.WriteLine("★UiTextside 垂直1 movement=" + movement + " this.ImageMovement（" + this.ImageMovement.X + "、" + this.ImageMovement.Y + "）");
                    this.ImageMovement = new Point(
                        this.ImageMovement.X,
                        this.ImageMovement.Y - movement
                        );
                    //ystem.Console.WriteLine("★UiTextside 垂直2 this.ImageMovement（" + this.ImageMovement.X + "、" + this.ImageMovement.Y + "）");
                }
            };
            this.scrollbar.OnHScrollAction = (object sender2, EventArgs e2, int pos, int movement) =>
            {
                //━━━━━
                // ※ここでは、コントロールのプロパティーへの変更が正常には利きません。
                //━━━━━

                if (0 != movement)
                {
                    this.ImageMovement = new Point(
                        this.ImageMovement.X - movement,
                        this.ImageMovement.Y
                        );
                }
            };
        }


        public void Timer1_Tick(UiTextside uiTextside1, object sender, EventArgs e)
        {
            //━━━━━
            // スクロールバーの移動量を、全画像に逆方向に加算
            //━━━━━
            if (this.ImageMovement.X != 0 || this.ImageMovement.Y != 0)
            {
                //━━━━━
                // 全画像のYに移動量を逆方向に加算
                //━━━━━
                foreach (Control c in uiTextside1.RichTextBox1.Controls)
                {
                    if (c is PictureBox)
                    {
                        PictureBox pic = (PictureBox)c;


                        //ystem.Console.WriteLine("★UiTextside 垂直 pos=" + pos + " movement=" + movement + " pic.Location（" + pic.Location.X + "、" + pic.Location.Y + "）");
                        // リッチテキストエリア内での、ピクチャーボックスの左上座標
                        pic.Location = new Point(
                            pic.Location.X + this.ImageMovement.X,
                            pic.Location.Y + this.ImageMovement.Y
                            );
                        //ystem.Console.WriteLine("★UiTextside 垂直2 pic.Location（" + pic.Location.X + "、" + pic.Location.Y + "）");
                        pic.Refresh();
                    }
                }

                this.ImageMovement = new Point();
            }
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
                            this.DraggingPictureBox = (PictureBox)sender;
                            this.DraggingPictureBox.BorderStyle = BorderStyle.FixedSingle;

                            // ピクチャーボックス内での、マウスボタン押下座標。
                            System.Console.WriteLine("e.Location=(" + e.Location.X + "、" + e.Location.Y + ")");
                            this.mouseDownOffsetLocation = new Point(e.Location.X, e.Location.Y);
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
                PictureBox pic1 = (PictureBox)sender;

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        System.Console.WriteLine("★pic1_MouseUp　左ボタン");
                        if (pic1 == this.DraggingPictureBox)
                        {
                            // ドラッグ中のピクチャーボックスだった場合
                            this.DraggingPictureBox.BorderStyle = BorderStyle.None;
                            this.DraggingPictureBox = null;
                        }
                        break;
                    case MouseButtons.Right:
                        {
                            System.Console.WriteLine("★pic1_MouseUp　右ボタン");

                            Point p1 = pic1.PointToScreen(e.Location);

                            this.MouseupPicturebox = pic1;

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

        public void Pic1_MouseMove(Form1 form1, object sender, MouseEventArgs e)
        {
            if (form1.UiMain1.UiTextside1.IsImageCsvMode)
            {
                if (null != this.DraggingPictureBox)
                {
                    // マウス移動量。
                    Point mv = new Point(
                            e.X - this.mouseDownOffsetLocation.X,
                            e.Y - this.mouseDownOffsetLocation.Y
                        );

                    // リッチテキストエリア内での、ピクチャーボックスの左上座標
                    this.DraggingPictureBox.Location = new Point(
                        this.DraggingPictureBox.Location.X + mv.X,
                        this.DraggingPictureBox.Location.Y + mv.Y
                        );
                    form1.UiMain1.Contents.IsChangedCsv = true;
                    form1.UiMain1.RefreshTitleBar();
                    this.DraggingPictureBox.Refresh();
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

            if (null != this.DraggingPictureBox)
            {
                this.DraggingPictureBox.BorderStyle = BorderStyle.None;
                this.DraggingPictureBox = null;
            }
        }

        public void RichTextBox1_VScroll(object sender, EventArgs e)
        {
            //━━━━━
            // ここでは、コントロールのプロパティーへの変更が正常には利きません。
            //━━━━━
            this.Scrollbar.OnVScroll(sender, e);
        }

        public void RichTextBox1_HScroll(object sender, EventArgs e)
        {
            //━━━━━
            // ここでは、コントロールのプロパティーへの変更が正常には利きません。
            //━━━━━
            this.Scrollbar.OnHScroll(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeleteImage(Form1 form1, object sender, EventArgs e)
        {
            UiTextside uiTextside1 = form1.UiMain1.UiTextside1;
            PictureBox pic1 = this.MouseupPicturebox;


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
            PictureBox pic1 = this.MouseupPicturebox;

            // 左上座標で指定します。ずらして作成します。
            uiTextside1.AddPicture(
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
