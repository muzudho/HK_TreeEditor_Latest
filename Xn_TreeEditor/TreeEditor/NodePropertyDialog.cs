using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public string SampleText
        {
            get
            {
                return this.sampleLbl.Text;
            }
            set
            {
                this.sampleLbl.Text = value;
            }
        }

        /// <summary>
        /// 変更前の前景の赤
        /// </summary>
        private int preForeRed;
        public int PreForeRed
        {
            get
            {
                int n = preForeRed;

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
                preForeRed = value;
            }
        }

        /// <summary>
        /// 変更前の前景の緑
        /// </summary>
        private int preForeGreen;
        public int PreForeGreen
        {
            get
            {
                int n = preForeGreen;

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
                preForeGreen = value;
            }
        }

        /// <summary>
        /// 変更前の前景の青
        /// </summary>
        private int preForeBlue;
        public int PreForeBlue
        {
            get
            {
                int n = preForeBlue;

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
                preForeBlue = value;
            }
        }

        /// <summary>
        /// 変更前の後景の赤
        /// </summary>
        private int preBackRed;
        public int PreBackRed
        {
            get
            {
                int n = preBackRed;

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
                preBackRed = value;
            }
        }

        /// <summary>
        /// 変更前の後景の緑
        /// </summary>
        private int preBackGreen;
        public int PreBackGreen
        {
            get
            {
                int n = preBackGreen;

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
                preBackGreen = value;
            }
        }

        /// <summary>
        /// 変更前の後景の青
        /// </summary>
        private int preBackBlue;
        public int PreBackBlue
        {
            get
            {
                int n = preBackBlue;

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
                preBackBlue = value;
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
        }

        private void NodeColorDialog_Load(object sender, EventArgs e)
        {
            this.PreForeRed = this.ForeRed;
            this.PreForeGreen = this.ForeGreen;
            this.PreForeBlue = this.ForeBlue;
            this.PreBackRed = this.BackRed;
            this.PreBackGreen = this.BackGreen;
            this.PreBackBlue = this.BackBlue;
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
            this.ForeRed = this.PreForeRed;
            this.ForeGreen = this.PreForeGreen;
            this.ForeBlue = this.PreForeBlue;
            this.BackRed = this.PreBackRed;
            this.BackGreen = this.PreBackGreen;
            this.BackBlue = this.PreBackBlue;
            this.SelectedImageIndex = this.PreSelectedImageIndex;
        }

        private void RefreshSample()
        {
            this.sampleLbl.ForeColor = Color.FromArgb(255, this.ForeRed, this.ForeGreen, this.ForeBlue);
            this.sampleLbl.BackColor = Color.FromArgb(255, this.BackRed, this.BackGreen, this.BackBlue);
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
                string s = this.ForeWeb;

                if (!s.StartsWith("#"))
                {

                }
                else if(s.Length==7)
                {
                    string r = s.Substring(1, 2);
                    string g = s.Substring(3, 2);
                    string b = s.Substring(5, 2);
                    this.ForeRed = Convert.ToInt32(r, 16);
                    this.ForeGreen = Convert.ToInt32(g, 16);
                    this.ForeBlue = Convert.ToInt32(b, 16);
                }
                else if (s.Length == 4)
                {
                    string r = s.Substring(1, 1);
                    string g = s.Substring(2, 1);
                    string b = s.Substring(3, 1);
                    this.ForeRed = Convert.ToInt32(r, 16);
                    this.ForeGreen = Convert.ToInt32(g, 16);
                    this.ForeBlue = Convert.ToInt32(b, 16);
                }
            }

            //後景色
            {
                string s = this.BackWeb;

                if (!s.StartsWith("#"))
                {

                }
                else if (s.Length == 7)
                {
                    string r = s.Substring(1, 2);
                    string g = s.Substring(3, 2);
                    string b = s.Substring(5, 2);
                    this.BackRed = Convert.ToInt32(r, 16);
                    this.BackGreen = Convert.ToInt32(g, 16);
                    this.BackBlue = Convert.ToInt32(b, 16);
                }
                else if (s.Length == 4)
                {
                    string r = s.Substring(1, 1);
                    string g = s.Substring(2, 1);
                    string b = s.Substring(3, 1);
                    this.BackRed = Convert.ToInt32(r, 16);
                    this.BackGreen = Convert.ToInt32(g, 16);
                    this.BackBlue = Convert.ToInt32(b, 16);
                }
            }
        }

        private void foreRedTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputRgb)
            {
                this.autoInputWeb = true;
                this.RgbToWeb();
                this.autoInputWeb = false;

                this.RefreshSample();
            }
        }

        private void backRedTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputRgb)
            {
                this.autoInputWeb = true;
                this.RgbToWeb();
                this.autoInputWeb = false;

                this.RefreshSample();
            }
        }

        private void foreGreenTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputRgb)
            {
                this.autoInputWeb = true;
                this.RgbToWeb();
                this.autoInputWeb = false;

                this.RefreshSample();
            }
        }

        private void backGreenTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputRgb)
            {
                this.autoInputWeb = true;
                this.RgbToWeb();
                this.autoInputWeb = false;

                this.RefreshSample();
            }
        }

        private void foreBlueTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputRgb)
            {
                this.autoInputWeb = true;
                this.RgbToWeb();
                this.autoInputWeb = false;

                this.RefreshSample();
            }
        }

        private void backBlueTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputRgb)
            {
                this.autoInputWeb = true;
                this.RgbToWeb();
                this.autoInputWeb = false;

                this.RefreshSample();
            }
        }

        private void foreWebTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputWeb)
            {
                this.autoInputRgb = true;
                this.WebToRgb();
                this.autoInputRgb = false;

                this.RefreshSample();
            }
        }

        private void backWebTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputWeb)
            {
                this.autoInputRgb = true;
                this.WebToRgb();
                this.autoInputRgb = false;

                this.RefreshSample();
            }
        }

        private void imageIndexLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Owner is Form1)
            {
                Form1 form1 = (Form1)this.Owner;

                ListBox lst = (ListBox)sender;
                this.SelectedImageIndex = (int)lst.SelectedItem;

                string file = form1.UiMain1.GetListiconFile(form1.UiMain1.ProjectName, form1.UiMain1.UiTextside1.NodeNameTxt1.Text, this.SelectedImageIndex);
                System.Console.WriteLine("★imageIndexLst_SelectedIndexChanged　this.SelectedImageIndex=[" + this.SelectedImageIndex + "]　file=[" + file + "]");
                this.pictureBox1.ImageLocation = file;
                this.pictureBox1.Refresh();
            }
        }
    }
}
