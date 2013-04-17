using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TreeEditor
{
    public partial class UiTextside : UserControl
    {


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
        private Point mouseDownOffsetLocation = Point.Empty;



        /// <summary>
        /// テキストの丸ごと履歴。
        /// </summary>
        private RtfHistory textHistory;
        public RtfHistory TextHistory
        {
            get
            {
                return textHistory;
            }
            set
            {
                textHistory = value;
            }
        }



        public void Undo()
        {
            string old;
            if (this.TextHistory.TryUndo(out old))
            {
                this.isAutoinputTextarea = true;
                this.TextareaRtf = old;
                this.isAutoinputTextarea = false;
            }
        }
        public void Redo()
        {
            string next;
            if (this.TextHistory.TryRedo(out next))
            {
                this.isAutoinputTextarea = true;
                this.TextareaRtf = next;
                this.isAutoinputTextarea = false;
            }
        }
        /// <summary>
        /// テキストエリアへの自動入力。
        /// </summary>
        private bool isAutoinputTextarea;
        public bool IsAutoinputTextarea
        {
            get
            {
                return isAutoinputTextarea;
            }
            set
            {
                isAutoinputTextarea = value;
            }
        }


        /// <summary>
        /// スクロールバー
        /// </summary>
        private Scrollbar scrollbar;
        public Scrollbar Scrollbar
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

        public RichTextBox RichTextBox1
        {
            get
            {
                return this.richTextBox1;
            }
        }


        public void Clear()
        {
            //━━━━━
            //テキスト
            //━━━━━
            this.NodeNameTxt1.Text = "";

            this.IsAutoinputTextarea = true;
            this.TextareaRtf = "";
            this.IsAutoinputTextarea = false;

            //━━━━━
            //画像
            //━━━━━
            List<Control> remove1 = new List<Control>();
            foreach (Control c in this.RichTextBox1.Controls)
            {
                if (c is PictureBox)
                {
                    remove1.Add(c);
                }
            }

            foreach (Control c in remove1)
            {
                this.RichTextBox1.SuspendLayout();
                this.RichTextBox1.Controls.Remove(c);
                this.RichTextBox1.ResumeLayout();
                this.RichTextBox1.Refresh();
                this.Refresh();
            }
        }

        public UiTextside()
        {
            this.imageMovement = new Point();

            InitializeComponent();
            this.textHistory = new RtfHistory();

            this.scrollbar = new Scrollbar();
        }

        private void FitSize()
        {
            this.toolStripContainer1.Location = new Point();
            this.toolStripContainer1.Width = this.ClientSize.Width;
            this.toolStripContainer1.Height = this.ClientSize.Height;

            this.richTextBox1.Location = new Point();
            this.richTextBox1.Width = this.toolStripContainer1.ContentPanel.Width;
            this.richTextBox1.Height = this.toolStripContainer1.ContentPanel.Height;
        }

        private void UiTextside_Load(object sender, EventArgs e)
        {
            this.FitSize();


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


            //━━━━━
            // ドラッグ＆ドロップ
            //━━━━━
            this.richTextBox1.AllowDrop = true;


            //━━━━━
            // フォント・ファミリー
            //━━━━━
            this.toolStripComboBox1.Items.Clear();
            //InstalledFontCollectionオブジェクトの取得
            System.Drawing.Text.InstalledFontCollection ifc = new System.Drawing.Text.InstalledFontCollection();
            //インストールされているすべてのフォントファミリアを取得
            FontFamily[] ffs = ifc.Families;
            foreach (FontFamily ff in ffs)
            {
                //ここではスタイルにRegularが使用できるフォントのみを表示
                if (ff.IsStyleAvailable(FontStyle.Regular))
                {
                    this.toolStripComboBox1.Items.Add(ff.Name);
                }
            }
            this.toolStripComboBox1.SelectedIndex = 0;

            //━━━━━
            // フォントサイズ
            //━━━━━
            this.toolStripComboBox2.Items.Clear();
            this.toolStripComboBox2.Items.AddRange(new string[] {"9","10","11" });
            this.toolStripComboBox2.SelectedIndex = 0;


            //━━━━━
            // タイマー
            //━━━━━
            this.timer1.Start();
        }

        private void UiTextside_Resize(object sender, EventArgs e)
        {
            this.FitSize();
        }

        private void toolStrip2_Resize(object sender, EventArgs e)
        {
            this.toolStripTextBox1.AutoSize = false;
            this.toolStripTextBox1.TextBox.Width = this.toolStrip2.ClientSize.Width - 20;
        }

        /// <summary>
        /// ノード名を入れるテキストボックスです。
        /// </summary>
        public ToolStripTextBox NodeNameTxt1
        {
            get
            {
                return this.toolStripTextBox1;
            }
        }

        public string TextareaRtf
        {
            get
            {
                return this.richTextBox1.Rtf;
            }
            set
            {
                this.richTextBox1.Rtf = value;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            if (!this.isAutoinputTextarea)
            {
                UiMain uiMain = ((Form1)this.ParentForm).UiMain1;
                this.TextHistory.Add(this.TextareaRtf);
                uiMain.Contents.IsChangedRtf = uiMain.IsChangeRtf();
                uiMain.RefreshTitleBar();
            }

        }

        private void richTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            // ドラッグされてきたデータの形式を一覧します。
            StringBuilder sb = new StringBuilder();
            sb.Append("ドラッグされてきたデータの形式の個数=[");
            sb.Append(e.Data.GetFormats().Length);
            sb.Append("]\r\n");
            foreach (string format in e.Data.GetFormats())
            {
                sb.Append(format);
                sb.Append("\r\n");
            }
            System.Console.WriteLine( sb.ToString());

            // ドラッグされてきたデータの形式を調べます。

            // ファイルドロップ
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                System.Console.WriteLine("ファイル名のようなものがパネルの上にドラッグされてきています。");

                // ドロップした時の効果を Copy として見えるようにします。
                e.Effect = DragDropEffects.Copy;
                System.Console.WriteLine("コピーの受け入れ態勢をとります。");
            }
            // URL
            else if (
                e.Data.GetDataPresent("UniformResourceLocator") ||
                e.Data.GetDataPresent("UniformResourceLocatorW")
                )
            {
                System.Console.WriteLine("URLのようなものがパネルの上にドラッグされてきています。");

                // URLであれば、ドロップした時の効果を Copy として見えるようにします。
                e.Effect = DragDropEffects.Copy;
                System.Console.WriteLine("コピーの受け入れ態勢をとります。");
            }
            // 文字列
            else if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                System.Console.WriteLine("文字列のようなものがパネルの上にドラッグされてきています。");

                // 文字列であれば、ドロップした時の効果を Copy として見えるようにします。
                e.Effect = DragDropEffects.None;
                System.Console.WriteLine("受け入れない態勢をとります。");
            }
            else
            {
                System.Console.WriteLine("何かがパネルの上にドラッグされてきています。");

                // 文字列でも画像でもなければ、ドロップできないように見えるようにします。
                e.Effect = DragDropEffects.None;
                System.Console.WriteLine("受け入れない態勢をとります。");
            }
        }

        public PictureBox NewPictureBox(string fileName, Point p2, bool andAdd)
        {
            Form1 form1 = ((Form1)this.ParentForm);

            Image droppedBitmap = new Bitmap(fileName);
            //ystem.Console.WriteLine("droppedBitmap:" + droppedBitmap.ToString());

            PictureBox pic1 = null;

            if (andAdd)
            {
                Clipboard.SetImage(droppedBitmap);
                if (Clipboard.ContainsImage())
                {
                    this.richTextBox1.Paste();
                }
            }

            return pic1;
        }

        private void richTextBox1_DragDrop(object sender, DragEventArgs e)
        {

            // ドロップされたデータの形式を一覧します。
            StringBuilder text = new StringBuilder();
            text.Append("ドロップされたデータの形式の個数=[");
            text.Append(e.Data.GetFormats().Length);
            text.Append("]\r\n");
            foreach (string format in e.Data.GetFormats())
            {
                text.Append(format);
                text.Append("\r\n");
            }
            System.Console.WriteLine(text.ToString());

            //ドロップされたデータの形式を調べます。

            // ファイルドロップ
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // ファイルであれば。
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                // ドロップされたファイル名を一覧します。
                StringBuilder fileNamesText = new StringBuilder();
                fileNamesText.Append("ドロップされたファイル名の個数=[");
                fileNamesText.Append(fileNames.Length);
                fileNamesText.Append("]\r\n");

                foreach (string fileName in fileNames)
                {
                    try
                    {
                        // ファイル名が画像を指していれば画像に、
                        // そうでなければ例外を返します。

                        Point p2 = this.richTextBox1.PointToClient(new Point(e.X, e.Y));

                        this.NewPictureBox(fileName, p2, true);

                        foreach (Control c in this.richTextBox1.Controls)
                        {
                            System.Console.WriteLine("コントロール："+c.ToString());
                            if (c is PictureBox)
                            {
                                PictureBox p = (PictureBox)c;

                                System.Console.WriteLine("コントロール： 座標(" + p.Location.X + "," + p.Location.Y + ") サイズ("+ p.Width + "," + p.Height +")");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // 画像ではなかった場合。

                        // 無視します。
                    }

                    // ファイル名を取得していきます。
                    fileNamesText.Append(fileName);
                    fileNamesText.Append("\r\n");
                }
                System.Console.WriteLine("fileNamesText.ToString():" +  fileNamesText.ToString());
                System.Console.WriteLine("ファイル名のようなものがパネルの上に落とされました。");
            }
            // URL
            else if (
                e.Data.GetDataPresent("UniformResourceLocator") ||
                e.Data.GetDataPresent("UniformResourceLocatorW")
                )
            {
                // 現在、プログラムの処理は　このコードまで到達しません。

                MessageBox.Show(e.Data.GetData("UniformResourceLocator").ToString(), "URI");

                // URLとして読み取れる形式のデータがドロップされた場合、
                // テキストボックスに、そのURLを表示します。
                string droppedUri = e.Data.GetData("UniformResourceLocator").ToString();
                if ("" == droppedUri)
                {
                    droppedUri = e.Data.GetData("UniformResourceLocatorW").ToString();
                }

                System.Console.WriteLine("droppedUri:" + droppedUri);
                System.Console.WriteLine("URLがパネルの上に落とされました。");
            }
            // 文字列
            else if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                // 文字列として読み取れる形式のデータがドロップされた場合、
                // テキストボックスに、その文字列データを表示します。
                string droppedText = (string)e.Data.GetData(typeof(string));

                System.Console.WriteLine("droppedText:" + droppedText);
                System.Console.WriteLine("文字列がパネルの上に落とされました。");
            }
            else
            {
                System.Console.WriteLine("何かパネルの上に落とされましたが、文字列としても画像としても読み取れませんでした。");
            }
            // 文字列または画像のどちらにも読み取れないデータは無視します。

        }

        void pic1_MouseUp(object sender, MouseEventArgs e)
        {

            switch(e.Button)
            {
                case MouseButtons.Left:
                    System.Console.WriteLine("★pic1_MouseUp　左ボタン");
                    if ((PictureBox)sender == this.DraggingPictureBox)
                    {
                        // ドラッグ中のピクチャーボックスだった場合
                        this.DraggingPictureBox.BorderStyle = BorderStyle.None;
                        this.DraggingPictureBox = null;
                    }
                    break;
                case MouseButtons.Right:
                    {
                        System.Console.WriteLine("★pic1_MouseUp　右ボタン");
                    }
                    break;
            }
        }

        void pic1_MouseDown(object sender, MouseEventArgs e)
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

        void pic1_MouseMove(object sender, MouseEventArgs e)
        {
            //System.Console.WriteLine("★pic1_MouseMove");

            //if (null != this.DraggingPictureBox)
            //{
            //    Form1 form1 = (Form1)this.ParentForm;

            //    // マウス移動量。
            //    Point mv = new Point(
            //            e.X - this.mouseDownOffsetLocation.X,
            //            e.Y - this.mouseDownOffsetLocation.Y
            //        );

            //    // リッチテキストエリア内での、ピクチャーボックスの左上座標
            //    this.DraggingPictureBox.Location = new Point(
            //        this.DraggingPictureBox.Location.X + mv.X,
            //        this.DraggingPictureBox.Location.Y + mv.Y
            //        );
            //    form1.UiMain1.Contents.IsChangedResource = true;
            //    form1.UiMain1.RefreshTitleBar();
            //    this.DraggingPictureBox.Refresh();
            //}
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //int index = this.richTextBox1.GetCharIndexFromPosition(e.Location);
            int index = this.richTextBox1.GetCharIndexFromPosition(new Point(1,1));
            int line = this.richTextBox1.GetLineFromCharIndex(index);
            int column = index - this.richTextBox1.GetFirstCharIndexFromLine(line);
            Console.WriteLine("{0}行 {1}桁", line, column);

            //System.Console.WriteLine("★DisplayRectangle（" + this.richTextBox1.DisplayRectangle.X + "、" + this.richTextBox1.DisplayRectangle.Y + "、" + this.richTextBox1.DisplayRectangle.Width + "、" + this.richTextBox1.DisplayRectangle.Height + "）");
            //Point p = this.richTextBox1.GetPositionFromCharIndex(
            //    this.richTextBox1.GetCharIndexFromPosition(new Point(0,0))
            //    );
            //System.Console.WriteLine("★this.richTextBox1.GetCharIndexFromPosition(new Point(0,0))＝（" + this.richTextBox1.GetCharIndexFromPosition(new Point(0, 0)) + "）");
            //System.Console.WriteLine("★p＝（" + p.X + "、" + p.Y + "）");

            if (null!=this.DraggingPictureBox)
            {
                this.DraggingPictureBox.BorderStyle = BorderStyle.None;
                this.DraggingPictureBox = null;
            }
        }



        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            //━━━━━
            // ここでは、コントロールのプロパティーへの変更が正常には利きません。
            //━━━━━

            this.Scrollbar.OnVScroll(sender, e);
        }

        private void richTextBox1_HScroll(object sender, EventArgs e)
        {
            //━━━━━
            // ここでは、コントロールのプロパティーへの変更が正常には利きません。
            //━━━━━

            this.Scrollbar.OnHScroll(sender, e);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //━━━━━
            // スクロールバーの移動量を、全画像に逆方向に加算
            //━━━━━
            if (this.ImageMovement.X != 0 || this.ImageMovement.Y != 0)
            {
                //━━━━━
                // 全画像のYに移動量を逆方向に加算
                //━━━━━
                foreach (Control c in this.richTextBox1.Controls)
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

        /// <summary>
        /// 画像を右クリックしたときの[画像削除]。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 画像削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form1 form1 = (Form1)this.ParentForm;

            //System.Console.WriteLine("画像削除ToolStripMenuItem_Click　sender=" + sender.ToString()+"　"+sender.GetType());

            //if (sender is ToolStripMenuItem)
            //{
            //    ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;

            //    System.Console.WriteLine("画像削除ToolStripMenuItem_Click　tsmi.Owner=" + tsmi.Owner.ToString());

            //    if (tsmi.Owner is ContextMenuStrip)
            //    {
            //        ContextMenuStrip cms = (ContextMenuStrip)tsmi.Owner;


            //        System.Console.WriteLine("画像削除ToolStripMenuItem_Click　cms.SourceControl=" + cms.SourceControl.ToString());

            //        if (cms.SourceControl is PictureBox)
            //        {
            //            PictureBox pic1 = (PictureBox)cms.SourceControl;



            //            System.Console.WriteLine("画像削除ToolStripMenuItem_Click　pic1=" + pic1.ToString());

            //            this.RichTextBox1.SuspendLayout();
            //            this.RichTextBox1.Controls.Remove(pic1);
            //            form1.UiMain1.Contents.IsChangedResource = true;
            //            form1.UiMain1.RefreshTitleBar();
            //            this.RichTextBox1.ResumeLayout();

            //            foreach (Control c in this.RichTextBox1.Controls)
            //            {
            //                System.Console.WriteLine("画像削除ToolStripMenuItem_Click　c=" + c.ToString());
            //            }

            //            this.RichTextBox1.Refresh();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 画像を右クリックしたときの[画像複製]。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 画像複製ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form1 form1 = (Form1)this.ParentForm;

            //System.Console.WriteLine("画像複製　sender=" + sender.ToString() + "　" + sender.GetType());

            //if (sender is ToolStripMenuItem)
            //{
            //    ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;

            //    System.Console.WriteLine("画像複製　tsmi.Owner=" + tsmi.Owner.ToString());

            //    if (tsmi.Owner is ContextMenuStrip)
            //    {
            //        ContextMenuStrip cms = (ContextMenuStrip)tsmi.Owner;


            //        System.Console.WriteLine("画像複製　cms.SourceControl=" + cms.SourceControl.ToString());

            //        if (cms.SourceControl is PictureBox)
            //        {
            //            PictureBox pic1 = (PictureBox)cms.SourceControl;

            //            // 中心座標で指定します。
            //            PictureBox pic2 = this.NewPictureBox(
            //                pic1.ImageLocation,
            //                new Point(
            //                    pic1.Location.X + (pic1.Width / 2) + (pic1.Width / 2),
            //                    pic1.Location.Y + (pic1.Height / 2) + (pic1.Width / 2)
            //                    ),
            //                true);
            //            if (null != pic2)
            //            {
            //                this.RichTextBox1.SuspendLayout();
            //                this.RichTextBox1.Controls.Add(pic2);
            //                form1.UiMain1.Contents.IsChangedResource = true;
            //                form1.UiMain1.RefreshTitleBar();
            //                this.RichTextBox1.ResumeLayout();

            //                this.RichTextBox1.Refresh();
            //            }

            //        }
            //    }
            //}
        }

    }
}
