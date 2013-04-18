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
        }

        /// <summary>
        /// 太字。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 斜体。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 下線。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 取り消し線。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 太字。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            System.Console.WriteLine("太字にする。");
            this.richTextBox1.SelectAll();

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

            this.richTextBox1.Refresh();
        }

        /// <summary>
        /// 斜体。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            this.richTextBox1.SelectAll();

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

            this.richTextBox1.Refresh();
        }

        /// <summary>
        /// 下線。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.richTextBox1.SelectAll();

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

            this.richTextBox1.Refresh();
        }

        /// <summary>
        /// 取り消し線。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            this.richTextBox1.SelectAll();

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

            this.richTextBox1.Refresh();
        }

        private void UiTextProperty_Load(object sender, EventArgs e)
        {
            this.dialogResult = "";
            this.InitRtf = this.Rtf;


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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
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
        }

    }
}
