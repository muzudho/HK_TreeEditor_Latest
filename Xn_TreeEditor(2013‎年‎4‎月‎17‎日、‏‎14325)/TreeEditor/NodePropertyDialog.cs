using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Xenon.Syntax;

namespace TreeEditor
{
    public partial class NodePropertyDialog : Form
    {

        /// <summary>
        /// RGB自動入力中。
        /// </summary>
        private bool autoInputRgb;

        /// <summary>
        /// Webカラー自動入力中。
        /// </summary>
        private bool autoInputWeb;

        /// <summary>
        /// 色サンプル
        /// </summary>
        public string NodeNameText
        {
            get
            {
                return this.nodeNameTxt.Text;
            }
            set
            {
                this.nodeNameTxt.Text = value;
            }
        }

        /// <summary>
        /// 変更前の前景色
        /// </summary>
        private ColorNumber preFore;
        public ColorNumber PreFore
        {
            get
            {
                return preFore;
            }
            set
            {
                preFore = value;
            }
        }

        /// <summary>
        /// 変更前の後景色
        /// </summary>
        private ColorNumber preBack;
        public ColorNumber PreBack
        {
            get
            {
                return preBack;
            }
            set
            {
                preBack = value;
            }
        }



        /// <summary>
        /// 前景の赤
        /// </summary>
        public int ForeRed
        {
            get
            {
                int n;
                int.TryParse(this.foreRedTxt.Text, out n);

                if (n < 0)
                {
                    n = 0;
                }
                else if (255 < n)
                {
                    n = 255;
                }

                return n;
            }
            set
            {
                this.foreRedTxt.Text = value.ToString();
            }
        }

        /// <summary>
        /// 前景の緑
        /// </summary>
        public int ForeGreen
        {
            get
            {
                int n;
                int.TryParse(this.foreGreenTxt.Text, out n);

                if (n < 0)
                {
                    n = 0;
                }
                else if (255 < n)
                {
                    n = 255;
                }

                return n;
            }
            set
            {
                this.foreGreenTxt.Text = value.ToString();
            }
        }

        /// <summary>
        /// 前景の青
        /// </summary>
        public int ForeBlue
        {
            get
            {
                int n;
                int.TryParse(this.foreBlueTxt.Text, out n);

                if (n < 0)
                {
                    n = 0;
                }
                else if (255 < n)
                {
                    n = 255;
                }

                return n;
            }
            set
            {
                this.foreBlueTxt.Text = value.ToString();
            }
        }

        /// <summary>
        /// 前景のWebカラー
        /// </summary>
        public string ForeWeb
        {
            get
            {
                return this.foreWebTxt.Text;
            }
            set
            {
                this.foreWebTxt.Text = value;
            }
        }


        /// <summary>
        /// 後景の赤
        /// </summary>
        public int BackRed
        {
            get
            {
                int n;
                int.TryParse(this.backRedTxt.Text, out n);

                if (n < 0)
                {
                    n = 0;
                }
                else if (255 < n)
                {
                    n = 255;
                }

                return n;
            }
            set
            {
                this.backRedTxt.Text = value.ToString();
            }
        }

        /// <summary>
        /// 後景の緑
        /// </summary>
        public int BackGreen
        {
            get
            {
                int n;
                int.TryParse(this.backGreenTxt.Text, out n);

                if (n < 0)
                {
                    n = 0;
                }
                else if (255 < n)
                {
                    n = 255;
                }

                return n;
            }
            set
            {
                this.backGreenTxt.Text = value.ToString();
            }
        }

        /// <summary>
        /// 後景の青
        /// </summary>
        public int BackBlue
        {
            get
            {
                int n;
                int.TryParse(this.backBlueTxt.Text, out n);

                if (n < 0)
                {
                    n = 0;
                }
                else if (255 < n)
                {
                    n = 255;
                }

                return n;
            }
            set
            {
                this.backBlueTxt.Text = value.ToString();
            }
        }

        /// <summary>
        /// 前景のWebカラー
        /// </summary>
        public string BackWeb
        {
            get
            {
                return this.backWebTxt.Text;
            }
            set
            {
                this.backWebTxt.Text = value;
            }
        }

        /// <summary>
        /// 変更前のリストアイコンのインデックス。0～。
        /// </summary>
        private int preSelectedImageIndex;
        public int PreSelectedImageIndex
        {
            get
            {
                return preSelectedImageIndex;
            }
            set
            {
                preSelectedImageIndex = value;
            }
        }

        /// <summary>
        /// リストアイコンのインデックス。0～。
        /// </summary>
        private int selectedImageIndex;
        public int SelectedImageIndex
        {
            get
            {
                return selectedImageIndex;
            }
            set
            {
                selectedImageIndex = value;
            }
        }



        public NodePropertyDialog()
        {
            InitializeComponent();

            this.PreFore = new ColorNumber();
            this.PreBack = new ColorNumber();
        }

        private void NodeColorDialog_Load(object sender, EventArgs e)
        {
            this.PreFore.Red = this.ForeRed;
            this.PreFore.Green = this.ForeGreen;
            this.PreFore.Blue = this.ForeBlue;
            this.PreBack.Red = this.BackRed;
            this.PreBack.Green = this.BackGreen;
            this.PreBack.Blue = this.BackBlue;
            this.PreSelectedImageIndex = this.SelectedImageIndex;
            //ystem.Console.WriteLine("★NodeColorDialog_Load　this.ForeBlue=" + this.ForeBlue + "　this.BackBlue=" + this.BackBlue);

            NodePropertyDialog dlg = (NodePropertyDialog)sender;
            if (dlg.Owner is Form1)
            {
                Form1 form1 = (Form1)dlg.Owner;
                int cnt = form1.UiMain1.ImageList1.Images.Count;
                this.imageIndexLst.Items.Clear();
                for (int n = 0; n < cnt; n++)
                {
                    this.imageIndexLst.Items.Add(n);
                }

                if (0 <= this.selectedImageIndex && this.selectedImageIndex < this.imageIndexLst.Items.Count)
                {
                    this.imageIndexLst.SelectedIndex = this.SelectedImageIndex;
                }
            }
        }

        /// <summary>
        /// 元に戻すボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.ForeRed = this.PreFore.Red;
            this.ForeGreen = this.PreFore.Green;
            this.ForeBlue = this.PreFore.Blue;
            this.BackRed = this.PreBack.Red;
            this.BackGreen = this.PreBack.Green;
            this.BackBlue = this.PreBack.Blue;
            this.SelectedImageIndex = this.PreSelectedImageIndex;
        }

        private void RefreshNodeName()
        {
            this.nodeNameTxt.ForeColor = Color.FromArgb(255, this.ForeRed, this.ForeGreen, this.ForeBlue);
            this.nodeNameTxt.BackColor = Color.FromArgb(255, this.BackRed, this.BackGreen, this.BackBlue);
        }

        private void RgbToWeb()
        {
            //前景色
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("#");
                sb.Append(this.ForeRed.ToString("X2"));
                sb.Append(this.ForeGreen.ToString("X2"));
                sb.Append(this.ForeBlue.ToString("X2"));

                this.foreWebTxt.Text = sb.ToString();
            }

            //後景色
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("#");
                sb.Append(this.BackRed.ToString("X2"));
                sb.Append(this.BackGreen.ToString("X2"));
                sb.Append(this.BackBlue.ToString("X2"));

                this.backWebTxt.Text = sb.ToString();
            }
        }

        private void WebToRgb()
        {
            //前景色
            {
                ColorNumber cn = new ColorNumber();
                cn.Web = this.ForeWeb;
                this.ForeRed = cn.Red;
                this.ForeGreen = cn.Green;
                this.ForeBlue = cn.Blue;
            }

            //後景色
            {
                ColorNumber cn = new ColorNumber();
                cn.Web = this.BackWeb;
                this.BackRed = cn.Red;
                this.BackGreen = cn.Green;
                this.BackBlue = cn.Blue;
            }
        }

        private void foreRedTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputRgb)
            {
                this.autoInputWeb = true;
                this.RgbToWeb();
                this.autoInputWeb = false;

                this.RefreshNodeName();
            }
        }

        private void backRedTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputRgb)
            {
                this.autoInputWeb = true;
                this.RgbToWeb();
                this.autoInputWeb = false;

                this.RefreshNodeName();
            }
        }

        private void foreGreenTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputRgb)
            {
                this.autoInputWeb = true;
                this.RgbToWeb();
                this.autoInputWeb = false;

                this.RefreshNodeName();
            }
        }

        private void backGreenTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputRgb)
            {
                this.autoInputWeb = true;
                this.RgbToWeb();
                this.autoInputWeb = false;

                this.RefreshNodeName();
            }
        }

        private void foreBlueTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputRgb)
            {
                this.autoInputWeb = true;
                this.RgbToWeb();
                this.autoInputWeb = false;

                this.RefreshNodeName();
            }
        }

        private void backBlueTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputRgb)
            {
                this.autoInputWeb = true;
                this.RgbToWeb();
                this.autoInputWeb = false;

                this.RefreshNodeName();
            }
        }

        private void foreWebTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputWeb)
            {
                this.autoInputRgb = true;
                this.WebToRgb();
                this.autoInputRgb = false;

                this.RefreshNodeName();
            }
        }

        private void backWebTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputWeb)
            {
                this.autoInputRgb = true;
                this.WebToRgb();
                this.autoInputRgb = false;

                this.RefreshNodeName();
            }
        }

        private void imageIndexLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Owner is Form1)
            {
                Form1 form1 = (Form1)this.Owner;

                ListBox lst = (ListBox)sender;
                this.SelectedImageIndex = (int)lst.SelectedItem;

                string file = form1.UiMain1.GetListiconFile(form1.UiMain1.Contents.ProjectFolder, form1.UiMain1.UiTextside1.NodeNameTxt1.Text, this.SelectedImageIndex);
                System.Console.WriteLine("★imageIndexLst_SelectedIndexChanged　this.SelectedImageIndex=[" + this.SelectedImageIndex + "]　file=[" + file + "]");
                this.pictureBox1.ImageLocation = file;
                this.pictureBox1.Refresh();
            }
        }

        private void nodeNameTxt_TextChanged(object sender, EventArgs e)
        {
            if (this.Owner is Form1)
            {
                Form1 form1 = (Form1)this.Owner;

                bool duplicate = false;
                TextBox txt = (TextBox)sender;


                try
                {
                    System.IO.Path.GetFullPath(txt.Text);
                }
                catch (Exception)
                {
                    // ファイル名に使えない文字や、文字数が長すぎた時に例外が投げられます。
                    MessageBox.Show(form1, "ファイル名に使えない名前？\n["+txt.Text+"]", "何らかのエラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


                foreach (TreeNode tn in form1.UiMain1.TreeView1.Nodes)
                {
                    if (tn.Text == txt.Text)
                    {
                        //重複する場合。
                        duplicate = true;
                    }
                }

                if (duplicate)
                {
                    this.nameCommentLbl.ForeColor = Color.Black;
                    this.nameCommentLbl.Text = "既にあるノードの名前です。";
                }
                else
                {
                    this.nameCommentLbl.ForeColor = Color.Red;
                    this.nameCommentLbl.Text = "ノード名を変更します。";
                }
            }

        }
    }
}
