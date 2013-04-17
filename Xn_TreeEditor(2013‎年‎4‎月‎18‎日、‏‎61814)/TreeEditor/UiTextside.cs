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
        /// 画像ファイル参照モード
        /// </summary>
        private CsvMode csvMode;
        public CsvMode CsvMode
        {
            get
            {
                return csvMode;
            }
            set
            {
                csvMode = value;
            }
        }




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



        public RichTextBox RichTextBox1
        {
            get
            {
                return this.richTextBox1;
            }
        }

        /// <summary>
        /// 画像モード。CSVモードなら真、RTFモードなら偽。
        /// </summary>
        public bool IsImageCsvMode
        {
            get
            {
                return this.toolStripComboBox3.SelectedIndex == 1;
            }
            set
            {
                if (value)
                {
                    this.toolStripComboBox3.SelectedIndex = 1;
                }
                else
                {
                    this.toolStripComboBox3.SelectedIndex = 0;
                }
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
            InitializeComponent();
            this.textHistory = new RtfHistory();
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
            // コンボボックス
            //━━━━━
            this.toolStripComboBox3.SelectedIndex = 0;//RTFモード


            //━━━━━
            // CSVモード
            //━━━━━
            this.CsvMode = new CsvMode();
            this.CsvMode.Load(this);


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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="p2"></param>
        /// <param name="isLeftTop">左上座標で指定した場合は真。</param>
        public void AddPicture(string fileName, Point p2, bool isLeftTop)
        {
            Form1 form1 = ((Form1)this.ParentForm);

            Image droppedBitmap = new Bitmap(fileName);
            //ystem.Console.WriteLine("droppedBitmap:" + droppedBitmap.ToString());


            if (this.IsImageCsvMode)
            {
                //━━━━━
                //CSVモード
                //━━━━━

                PictureBox pic1 = new PictureBox();
                pic1.Visible = true;
                pic1.ImageLocation = fileName;
                pic1.Image = droppedBitmap;

                if (isLeftTop)
                {
                    //左上座標のままセットします。

                    pic1.Location = new Point(p2.X, p2.Y);
                }
                else
                {
                    //中心座標→左上座標に変換します。
                    pic1.Location = new Point(
                        pic1.Location.X - pic1.Width / 2,
                        pic1.Location.Y - pic1.Height / 2
                    );
                }

                pic1.Size = new Size(droppedBitmap.Width, droppedBitmap.Height);
                pic1.MouseMove += pic1_MouseMove;
                pic1.MouseDown += pic1_MouseDown;
                pic1.MouseUp += pic1_MouseUp;
                pic1.ContextMenuStrip = this.contextMenuStrip1;

                this.RichTextBox1.SuspendLayout();
                this.RichTextBox1.Controls.Add(pic1);
                form1.UiMain1.Contents.IsChangedCsv = true;
                form1.UiMain1.RefreshTitleBar();
                this.RichTextBox1.ResumeLayout();

                this.RichTextBox1.Refresh();
            }
            else
            {
                //━━━━━
                //RTFモード
                //━━━━━

                Clipboard.SetImage(droppedBitmap);
                if (Clipboard.ContainsImage())
                {
                    this.richTextBox1.Paste();
                }
            }

            //foreach (Control c in this.richTextBox1.Controls)
            //{
            //    System.Console.WriteLine("コントロール：" + c.ToString());
            //    if (c is PictureBox)
            //    {
            //        PictureBox p = (PictureBox)c;

            //        System.Console.WriteLine("コントロール： 座標(" + p.Location.X + "," + p.Location.Y + ") サイズ(" + p.Width + "," + p.Height + ")");
            //    }
            //}

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

                        // 中心座標で指定します。
                        this.AddPicture(fileName, p2, false);
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
            this.CsvMode.Pic1_MouseUp(sender, e);
        }

        void pic1_MouseDown(object sender, MouseEventArgs e)
        {
            this.CsvMode.Pic1_MouseDown(sender, e);
        }

        void pic1_MouseMove(object sender, MouseEventArgs e)
        {
            Form1 form1 = (Form1)this.ParentForm;
            this.CsvMode.Pic1_MouseMove(form1, sender, e);
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.CsvMode.RichTextBox1_MouseUp(sender, e);
        }



        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            this.CsvMode.RichTextBox1_VScroll(sender, e);
        }

        private void richTextBox1_HScroll(object sender, EventArgs e)
        {
            this.CsvMode.RichTextBox1_HScroll(sender, e);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.CsvMode.Timer1_Tick(this, sender, e);
        }

        /// <summary>
        /// 画像を右クリックしたときの[画像削除]。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 画像削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.ParentForm;

            this.CsvMode.DeleteImage(form1, sender, e);
        }

        /// <summary>
        /// 画像を右クリックしたときの[画像複製]。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 画像複製ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.ParentForm;
            this.CsvMode.DuplicateImage(form1, sender, e);


        }

    }
}
