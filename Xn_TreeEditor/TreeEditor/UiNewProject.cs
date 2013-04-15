using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace TreeEditor
{
    public partial class UiNewProject : UserControl
    {
        public UiNewProject()
        {
            InitializeComponent();
        }

        private void UiNewProject_Load(object sender, EventArgs e)
        {
            try
            {
                //━━━━━
                //ディレクトリー一覧
                //━━━━━
                this.listBox1.Items.Clear();

                string[] dirs = Directory.GetDirectories("save");

                foreach (string dir in dirs)
                {
                    System.Console.WriteLine("ディレクトリー：" + dir);

                    string dir2 = dir;
                    if (dir2.StartsWith(@"save\"))
                    {
                        dir2 = dir2.Substring(@"save\".Length, dir2.Length - @"save\".Length);
                    }

                    this.listBox1.Items.Add(dir2);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 作成ボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //━━━━━
            //まだ無いディレクトリー名の入力
            //━━━━━
            bool isClose = false;
            {
                string dir = this.textBox1.Text;
                System.Console.WriteLine("まだ無いディレクトリー名の入力：" + dir);

                if(Directory.Exists(@"save\"+dir))
                {
                    MessageBox.Show("もうあります。\n"+dir);
                }
                else
                {
                    isClose = true;
                }
            }

            //━━━━━
            //ダイアログボックスを閉じる
            //━━━━━
            if (isClose)
            {
                this.ParentForm.Close();
            }
        }

        public TextBox TextBox1
        {
            get
            {
                return this.textBox1;
            }
        }
    }
}
