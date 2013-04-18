using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TreeEditor
{
    public partial class UiTextProperty : UserControl
    {


        /// <summary>
        /// RGB自動入力中。
        /// </summary>
        private bool autoInputForeRgb;
        private bool autoInputBackRgb;

        /// <summary>
        /// Webカラー自動入力中。
        /// </summary>
        private bool autoInputForeWeb;
        private bool autoInputBackWeb;


        /// <summary>
        /// 変更前のフォントカラー
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



        private string dialogResult;
        public string DialogResult
        {
            get
            {
                return dialogResult;
            }
            set
            {
                dialogResult = value;
            }
        }

        private string initRtf;
        public string InitRtf
        {
            get
            {
                return initRtf;
            }
            set
            {
                initRtf = value;
            }
        }



        public string Rtf
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



        public UiTextProperty()
        {
            InitializeComponent();

            this.PreFore = new ColorNumber();
        }


        private void RefreshSample()
        {
            this.richTextBox1.SelectionColor = Color.FromArgb(255, this.ForeRed, this.ForeGreen, this.ForeBlue);
            this.richTextBox1.SelectionBackColor = Color.FromArgb(255, this.BackRed, this.BackGreen, this.BackBlue);
        }




        private string RgbToWeb(int r, int g, int b)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("#");
            sb.Append(r.ToString("X2"));
            sb.Append(g.ToString("X2"));
            sb.Append(b.ToString("X2"));

            return sb.ToString();
        }



        /// <summary>
        /// 太字。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.richTextBox1.SelectAll();

            if (null != this.richTextBox1.SelectionFont)//選択できない場合がある？
            {
                CheckBox cb = (CheckBox)sender;
                Font f;

                if (cb.Checked)
                {
                    f = new Font(
                        this.richTextBox1.SelectionFont.FontFamily,
                        this.richTextBox1.SelectionFont.Size,
                        this.richTextBox1.SelectionFont.Style | FontStyle.Bold
                        );
                }
                else
                {
                    f = new Font(
                        this.richTextBox1.SelectionFont.FontFamily,
                        this.richTextBox1.SelectionFont.Size,
                        this.richTextBox1.SelectionFont.Style & (0xf - FontStyle.Bold)
                        );
                }

                this.richTextBox1.SelectionFont = f;
                f.Dispose();

                //this.richTextBox1.Refresh();
            }
        }

        /// <summary>
        /// 斜体。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            this.richTextBox1.SelectAll();

            if (null != this.richTextBox1.SelectionFont)//選択できない場合がある？
            {
                CheckBox cb = (CheckBox)sender;
                Font f;

                if (cb.Checked)
                {
                    f = new Font(
                        this.richTextBox1.SelectionFont.FontFamily,
                        this.richTextBox1.SelectionFont.Size,
                        this.richTextBox1.SelectionFont.Style | FontStyle.Italic
                    );
                }
                else
                {
                    f = new Font(
                        this.richTextBox1.SelectionFont.FontFamily,
                        this.richTextBox1.SelectionFont.Size,
                        this.richTextBox1.SelectionFont.Style & (0xf - FontStyle.Italic)
                        );
                }

                this.richTextBox1.SelectionFont = f;
                f.Dispose();

                //this.richTextBox1.Refresh();
            }
        }

        /// <summary>
        /// 下線。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.richTextBox1.SelectAll();

            if (null != this.richTextBox1.SelectionFont)//選択できない場合がある？
            {
                CheckBox cb = (CheckBox)sender;
                Font f;

                if (cb.Checked)
                {
                    f = new Font(
                        this.richTextBox1.SelectionFont.FontFamily,
                        this.richTextBox1.SelectionFont.Size,
                        this.richTextBox1.SelectionFont.Style | FontStyle.Underline
                    );
                }
                else
                {
                    f = new Font(
                        this.richTextBox1.SelectionFont.FontFamily,
                        this.richTextBox1.SelectionFont.Size,
                        this.richTextBox1.SelectionFont.Style & (0xf - FontStyle.Underline)
                        );
                }

                this.richTextBox1.SelectionFont = f;
                f.Dispose();

                //this.richTextBox1.Refresh();
            }
        }

        /// <summary>
        /// 取り消し線。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            this.richTextBox1.SelectAll();

            if (null != this.richTextBox1.SelectionFont)//選択できない場合がある？
            {
                CheckBox cb = (CheckBox)sender;
                Font f;

                if (cb.Checked)
                {
                    f = new Font(
                        this.richTextBox1.SelectionFont.FontFamily,
                        this.richTextBox1.SelectionFont.Size,
                        this.richTextBox1.SelectionFont.Style | FontStyle.Strikeout
                    );
                }
                else
                {
                    f = new Font(
                        this.richTextBox1.SelectionFont.FontFamily,
                        this.richTextBox1.SelectionFont.Size,
                        this.richTextBox1.SelectionFont.Style & (0xf - FontStyle.Strikeout)
                        );
                }

                this.richTextBox1.SelectionFont = f;
                f.Dispose();

//                this.richTextBox1.Refresh();
            }
        }

        private void UiTextProperty_Load(object sender, EventArgs e)
        {
            //━━━━━
            // 設定
            //━━━━━
            this.dialogResult = "";
            this.InitRtf = this.Rtf;

            {
                RichTextBox rtb = new RichTextBox();
                rtb.Rtf = this.Rtf;
                rtb.SelectAll();

                this.PreFore = new ColorNumber();
                this.PreFore.Red = this.ForeRed = rtb.SelectionColor.R;
                this.PreFore.Green = this.ForeGreen = rtb.SelectionColor.G;
                this.PreFore.Blue = this.ForeBlue = rtb.SelectionColor.B;

                this.PreBack = new ColorNumber();
                this.PreBack.Red = this.BackRed = rtb.SelectionBackColor.R;
                this.PreBack.Green = this.BackGreen = rtb.SelectionBackColor.G;
                this.PreBack.Blue = this.BackBlue = rtb.SelectionBackColor.B;
            }


            //━━━━━
            // フォント・ファミリー
            //━━━━━
            this.comboBox1.Items.Clear();
            //InstalledFontCollectionオブジェクトの取得
            System.Drawing.Text.InstalledFontCollection ifc = new System.Drawing.Text.InstalledFontCollection();
            //インストールされているすべてのフォントファミリアを取得
            FontFamily[] ffs = ifc.Families;
            foreach (FontFamily ff in ffs)
            {
                //ここではスタイルにRegularが使用できるフォントのみを表示
                if (ff.IsStyleAvailable(FontStyle.Regular))
                {
                    this.comboBox1.Items.Add(ff.Name);
                }
            }
            this.comboBox1.SelectedIndex = 0;



            //━━━━━
            // フォントサイズ
            //━━━━━
            this.comboBox2.Items.Clear();
            this.comboBox2.Items.Add("");
            for (int i = 6; i < 128; i++)
            {
                // 文字列として追加。
                this.comboBox2.Items.Add(i.ToString());
            }
            this.comboBox2.SelectedIndex = 0;

            this.richTextBox1.SelectAll();


        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (null != this.richTextBox1.SelectionFont)
                {
                    //━━━━━
                    //フォント
                    //━━━━━
                    {
                        string s = this.richTextBox1.SelectionFont.FontFamily.Name;

                        int i = 0;
                        foreach (string s2 in this.comboBox1.Items)
                        {
                            if (s2 == s)
                            {
                                this.comboBox1.SelectedIndex = i;
                                break;
                            }

                            i++;
                        }
                    }


                    //━━━━━
                    // フォントサイズ
                    //━━━━━
                    {
                        string s = ((int)this.richTextBox1.SelectionFont.Size).ToString();

                        int i = 0;
                        foreach (string s2 in this.comboBox2.Items)
                        {
                            if (s2 == s)
                            {
                                this.comboBox2.SelectedIndex = i;
                                break;
                            }

                            i++;
                        }
                    }
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(this.ParentForm, e2.Message, "強制終了を回避！　richTextBox1_SelectionChanged", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.richTextBox1.SelectAll();

                if (null != this.richTextBox1.SelectionFont)//選択できていない場合がある？
                {
                    ComboBox cb = (ComboBox)sender;

                    string s = (string)cb.Items[cb.SelectedIndex];

                    if (s != "")
                    {
                        Font f = new Font(
                            s,
                            this.richTextBox1.SelectionFont.Size,
                            this.richTextBox1.SelectionFont.Style
                            );

                        this.richTextBox1.SelectionFont = f;
                        f.Dispose();
                    }
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(this.ParentForm, e2.Message, "強制終了を回避！　comboBox1_SelectedIndexChanged", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.richTextBox1.SelectAll();

                if (null != this.richTextBox1.SelectionFont)//選択できていない場合がある？
                {
                    ComboBox cb = (ComboBox)sender;

                    string s = (string)cb.Items[cb.SelectedIndex];

                    int n;

                    if (int.TryParse(s, out n))
                    {
                        Font f = new Font(
                            this.richTextBox1.SelectionFont.FontFamily,
                            n,
                            this.richTextBox1.SelectionFont.Style
                            );

                        this.richTextBox1.SelectionFont = f;
                        f.Dispose();
                    }
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(this.ParentForm, e2.Message, "強制終了を回避！　comboBox2_SelectedIndexChanged", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// OKボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = "OK";
            this.ParentForm.Close();
        }

        /// <summary>
        /// キャンセルボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = "Cancel";
            this.ParentForm.Close();
        }

        /// <summary>
        /// 元に戻すボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Rtf = this.InitRtf;

            this.ForeRed = this.PreFore.Red;
            this.ForeGreen = this.PreFore.Green;
            this.ForeBlue = this.PreFore.Blue;
        }

        private void foreRedTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputForeRgb)
            {
                this.autoInputForeWeb = true;
                this.foreWebTxt.Text = this.RgbToWeb(this.ForeRed, this.ForeGreen, this.ForeBlue);
                this.autoInputForeWeb = false;

                this.RefreshSample();
            }
        }

        private void foreGreenTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputForeRgb)
            {
                this.autoInputForeWeb = true;
                this.foreWebTxt.Text = this.RgbToWeb(this.ForeRed, this.ForeGreen, this.ForeBlue);
                this.autoInputForeWeb = false;

                this.RefreshSample();
            }
        }

        private void foreBlueTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputForeRgb)
            {
                this.autoInputForeWeb = true;
                this.foreWebTxt.Text = this.RgbToWeb(this.ForeRed, this.ForeGreen, this.ForeBlue);
                this.autoInputForeWeb = false;

                this.RefreshSample();
            }
        }

        private void foreWebTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputForeWeb)
            {
                this.autoInputForeRgb = true;

                ColorNumber cn = new ColorNumber();
                cn.Web = this.ForeWeb;
                this.ForeRed = cn.Red;
                this.ForeGreen = cn.Green;
                this.ForeBlue = cn.Blue;

                this.autoInputForeRgb = false;

                this.RefreshSample();
            }
        }

        private void backRedTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputBackRgb)
            {
                this.autoInputBackWeb = true;
                this.backWebTxt.Text = this.RgbToWeb(this.BackRed, this.BackGreen, this.BackBlue);
                this.autoInputBackWeb = false;

                this.RefreshSample();
            }
        }

        private void backGreenTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputBackRgb)
            {
                this.autoInputBackWeb = true;
                this.backWebTxt.Text = this.RgbToWeb(this.BackRed, this.BackGreen, this.BackBlue);
                this.autoInputBackWeb = false;

                this.RefreshSample();
            }
        }

        private void backBlueTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputBackRgb)
            {
                this.autoInputBackWeb = true;
                this.backWebTxt.Text = this.RgbToWeb(this.BackRed, this.BackGreen, this.BackBlue);
                this.autoInputBackWeb = false;

                this.RefreshSample();
            }
        }

        private void backWebTxt_TextChanged(object sender, EventArgs e)
        {
            if (!this.autoInputBackWeb)
            {
                this.autoInputBackRgb = true;

                ColorNumber cn = new ColorNumber();
                cn.Web = this.BackWeb;
                this.BackRed = cn.Red;
                this.BackGreen = cn.Green;
                this.BackBlue = cn.Blue;

                this.autoInputBackRgb = false;

                this.RefreshSample();
            }
        }

    }
}
