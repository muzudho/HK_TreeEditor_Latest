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

        /// <summary>
        /// 新規作成用なら真。
        /// </summary>
        private bool isNew;
        public bool IsNew
        {
            get
            {
                return isNew;
            }
            set
            {
                isNew = value;
            }
        }



        public UiNewProject()
        {
            InitializeComponent();
        }

        private void UiNewProject_Load(object sender, EventArgs e)
        {

            Form1 form1 = (Form1)((NewProjectDialog)this.ParentForm).Owner;

            //━━━━━
            //今使用中のフォルダー
            //━━━━━
            this.currentTxt.Text = form1.UiMain1.Contents.ProjectFolder;

            if (this.IsNew)
            {
                //this.createDammyBtn.Enabled = true;

                this.openBtn.Text = "空っぽのフォルダーを開く";
                this.defaultPrjBtn.Visible = false;
            }
            else
            {
                this.createDammyBtn.Visible = false;
            }

            try
            {
                //━━━━━
                //ディレクトリー一覧
                //━━━━━
                this.listBox1.Items.Clear();

                string[] dirs = Directory.GetDirectories(form1.UiMain1.Contents.ProjectFolder);
                string head = form1.UiMain1.Contents.ProjectFolder + @"\";

                foreach (string dir in dirs)
                {
                    System.Console.WriteLine("ディレクトリー：" + dir);

                    string dir2 = dir;
                    if (dir2.StartsWith(head))
                    {
                        dir2 = dir2.Substring(head.Length, dir2.Length - head.Length);
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
                string dir = this.newProjectFolderTxt1.Text;
                System.Console.WriteLine("まだ無いディレクトリー名の入力：" + dir);

                string[] files = Directory.GetFileSystemEntries(dir);
                if (null != files && 0<files.Length)
                {
                    MessageBox.Show("何かフォルダーの中に入っています。\n空のフォルダーを選んでください。\n" + dir);
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

        public TextBox NewProjectFolderTxt1
        {
            get
            {
                return this.newProjectFolderTxt1;
            }
        }

        /// <summary>
        /// 開くボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            DialogResult reslut = dlg.ShowDialog();

            if (reslut == DialogResult.OK)
            {
                this.newProjectFolderTxt1.Text = dlg.SelectedPath;
                if (this.IsNew)
                {
                    //新規作成ダイアログの場合
                    this.createDammyBtn.Enabled = true;
                }
                else
                {
                    //フォルダー・オープン用ダイアログの場合。
                    ((NewProjectDialog)this.ParentForm).Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.IsNew)
            {
                if ("" != this.newProjectFolderTxt1.Text)
                {
                    this.createDammyBtn.Enabled = true;
                }
                else
                {
                    this.createDammyBtn.Enabled = false;
                }
            }
        }

        /// <summary>
        /// デフォルト・プロジェクトを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.newProjectFolderTxt1.Text = Application.StartupPath + @"\save\default";

            if (this.IsNew)
            {
            }
            else
            {
                //オープン・フォルダー時。
                ((NewProjectDialog)this.ParentForm).Close();
            }
        }
    }
}
