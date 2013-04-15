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
        private TextHistory textHistory;
        public TextHistory TextHistory
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
                this.isAutoinputTextareaText = true;
                this.TextareaText = old;
                this.isAutoinputTextareaText = false;
            }
        }
        public void Redo()
        {
            string next;
            if (this.TextHistory.TryRedo(out next))
            {
                this.isAutoinputTextareaText = true;
                this.TextareaText = next;
                this.isAutoinputTextareaText = false;
            }
        }
        /// <summary>
        /// テキストエリアへの自動入力。
        /// </summary>
        private bool isAutoinputTextareaText;
        public bool IsAutoinputTextareaText
        {
            get
            {
                return isAutoinputTextareaText;
            }
            set
            {
                isAutoinputTextareaText = value;
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
            this.ToolStripTextBox1.Text = "";
            this.TextareaText = "";

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
            this.textHistory = new TextHistory();

            this.scrollbar = new Scrollbar();
            this.scrollbar.OnVScrollAction = (object sender, EventArgs e, int pos, int movement) =>
            {
                //━━━━━
                // ここでは、コントロールのプロパティーへの変更が正常には利きません。
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
            this.scrollbar.OnHScrollAction = (object sender, EventArgs e, int pos, int movement) =>
            {
                //━━━━━
                // ここでは、コントロールのプロパティーへの変更が正常には利きません。
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
        public ToolStripTextBox ToolStripTextBox1
        {
            get
            {
                return this.toolStripTextBox1;
            }
        }

        public string TextareaText
        {
            get
            {
                return this.richTextBox1.Text;
            }
            set
            {
                this.richTextBox1.Text = value;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!this.isAutoinputTextareaText)
            {
                this.TextHistory.Add(this.richTextBox1.Text);
            }

            UiMain uiMain = ((Form1)this.ParentForm).UiMain1;
            uiMain.TestChangeText();

            // 改行コードが違っても、文字が同じなら、変更なしと判定します。
            string text1 = uiMain.FileText.Replace("\r", "").Replace("\n", "");
            string text2 = this.richTextBox1.Text.Replace("\r", "").Replace("\n", "");

            uiMain.IsChangedText = text1.CompareTo(text2)!=0;
            //uiMain.IsChangedText = uiMain.FileText.CompareTo(this.richTextBox1.Text) != 0;

            //System.Console.WriteLine("★uiMain.FileText==this.richTextBox1.Text");
            //System.Console.WriteLine(uiMain.FileText==this.richTextBox1.Text);
            //System.Console.WriteLine("★uiMain.FileText.CompareTo(this.richTextBox1.Text)");
            //System.Console.WriteLine(uiMain.FileText.CompareTo(this.richTextBox1.Text));
            //System.Console.WriteLine("★uiMain.IsChangedText");
            //System.Console.WriteLine(uiMain.IsChangedText);
            //System.Console.WriteLine("★ファイルテキスト");
            //System.Console.WriteLine(uiMain.FileText);
            //System.Console.WriteLine("★テキストエリアテキスト");
            //System.Console.WriteLine(this.richTextBox1.Text);

            uiMain.RefreshTitleBar();
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

        public PictureBox NewPictureBox(string fileName, Point p2)
        {
            Image droppedBitmap = new Bitmap(fileName);
            System.Console.WriteLine("droppedBitmap:" + droppedBitmap.ToString());

            PictureBox pic1 = new PictureBox();
            pic1.Visible = true;
            pic1.ImageLocation = fileName;
            pic1.Image = droppedBitmap;
            pic1.Location = new Point(p2.X - droppedBitmap.Width / 2, p2.Y - droppedBitmap.Height / 2);
            pic1.Size = new Size(droppedBitmap.Width, droppedBitmap.Height);
            pic1.MouseMove += pic1_MouseMove;
            pic1.MouseDown += pic1_MouseDown;
            pic1.MouseUp += pic1_MouseUp;

            //Clipboard.SetImage(droppedBitmap);
            //if(Clipboard.ContainsImage())
            //{
            //    this.richTextBox1.Paste();
            //}

            this.richTextBox1.SuspendLayout();
            this.richTextBox1.Controls.Add(pic1);
            this.richTextBox1.ResumeLayout();
            this.richTextBox1.Refresh();
            this.Refresh();

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

                        this.NewPictureBox(fileName, p2);

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
            System.Console.WriteLine("★pic1_MouseUp");

            if (null != this.DraggingPictureBox)
            {
                this.DraggingPictureBox.BorderStyle = BorderStyle.None;
                this.DraggingPictureBox = null;
            }
        }

        void pic1_MouseDown(object sender, MouseEventArgs e)
        {
            System.Console.WriteLine("★pic1_MouseDown");

            this.DraggingPictureBox = (PictureBox)sender;
            this.DraggingPictureBox.BorderStyle = BorderStyle.FixedSingle;

            // ピクチャーボックス内での、マウスボタン押下座標。
            System.Console.WriteLine("e.Location=(" + e.Location.X + "、" + e.Location.Y + ")");
            this.mouseDownOffsetLocation = new Point(e.Location.X, e.Location.Y);
        }

        void pic1_MouseMove(object sender, MouseEventArgs e)
        {
            //System.Console.WriteLine("★pic1_MouseMove");

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
                this.DraggingPictureBox.Refresh();
            }
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

        private void richTextBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(this.ImageMovement.X != 0 || this.ImageMovement.Y != 0)
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
    }
}
