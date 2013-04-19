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
    public partial class UiPictureProperty : UserControl
    {

        public string File
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
            }
        }

        /// <summary>
        /// ×ボタンを押して終了すると空文字列。
        /// </summary>
        private string dialogResult;
        public string DialogResult
        {
            get
            {
                return this.dialogResult;
            }
            set
            {
                this.dialogResult = value;
            }
        }

        public UiPictureProperty()
        {
            InitializeComponent();
            this.dialogResult = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = "OK";
            this.ParentForm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = "Cancel";
            this.ParentForm.Close();
        }
    }
}
