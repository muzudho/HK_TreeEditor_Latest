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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Xenon.Syntax.Log_ReportsImpl.BDebugmode_Form = true;
            Xenon.Syntax.Log_ReportsImpl.BDebugmode_Static = true;
        }


        private void FitSize()
        {
            this.uiMain1.Location = new Point(0, this.menuStrip1.Height);
            this.uiMain1.Width = this.ClientSize.Width;
            this.uiMain1.Height = this.ClientSize.Height - this.menuStrip1.Height;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //ApplicationExitイベントハンドラを追加
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            this.FitSize();
        }

        //ApplicationExitイベントハンドラ
        public void Application_ApplicationExit(object sender, EventArgs e)
        {
            Actions.ApplicationExit(this, sender, e);
        }



        private void Form1_Resize(object sender, EventArgs e)
        {
            this.FitSize();
        }

        private void 新規作成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.New(this);
        }

        public UiMain UiMain1
        {
            get
            {
                return this.uiMain1;
            }
        }

        private void エディットを元に戻すToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.Undo(this);
        }

        private void エディットを元に戻すのをやり直すToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.Redo(this);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && Keys.S == e.KeyCode)
            {
                System.Console.WriteLine("セーブしたい。");
                Actions.Save(this);
            }
            else if (e.Control && Keys.Z == e.KeyCode)
            {
                System.Console.WriteLine("アンドゥしたい。");
                Actions.Undo(this);
            }
            else if (e.Control && Keys.Y == e.KeyCode)
            {
                System.Console.WriteLine("リドゥしたい。");
                Actions.Redo(this);
            }
            else if (e.Control && Keys.C == e.KeyCode)
            {
                System.Console.WriteLine("コピーしたい。");

                if (this.UiMain1.UiTextside1.RichTextBox1.Focused)
                {
                    System.Console.WriteLine("リッチテキストボックスのものを、コピーしたい。");

                    this.UiMain1.Contents.AnotherPageCopy = 1;
                }

            }
            else if (e.Control && Keys.V == e.KeyCode)
            {
                System.Console.WriteLine("ペーストしたい。");


                //──────────
                //
                //　・違うページの内容を貼り付けようとしたとき、貼り付けられないことがある。
                //　　かといって、ペーストを自作すると、
                //　　もともとあるペースト機能と重複して２度貼り付けてしまう。
                //
                //
                //
                //──────────

                if (this.UiMain1.Contents.AnotherPageCopy==2 && this.UiMain1.UiTextside1.RichTextBox1.Focused)
                {
                    System.Console.WriteLine("リッチテキストボックスへ、ペーストしたい。");

                    IDataObject data = Clipboard.GetDataObject();
                    if (data != null &&
                        (data.GetDataPresent(DataFormats.Text) || data.GetDataPresent(DataFormats.Rtf))
                        )
                    {
                        
                        if (data.GetDataPresent(DataFormats.Rtf))
                        {
                            //━━━━━
                            //RTFをペーストするように優先
                            //━━━━━

                            //ystem.Console.WriteLine("リッチテキストボックスへ、RTF[" + data.GetData(DataFormats.Rtf) + "]をペーストしたい。");

                            this.UiMain1.UiTextside1.RichTextBox1.Paste(DataFormats.GetFormat(DataFormats.Rtf));
                        }
                        else if (data.GetDataPresent(DataFormats.Text))
                        {
                            //━━━━━
                            //RTFをペーストするように優先
                            //━━━━━

                            //ystem.Console.WriteLine("リッチテキストボックスへ、テキスト[" + data.GetData(DataFormats.Text) + "]をペーストしたい。");

                            this.UiMain1.UiTextside1.RichTextBox1.Paste(DataFormats.GetFormat(DataFormats.Text));
                        }
                        else
                        {
                            //内部実装任せ
                            this.UiMain1.UiTextside1.RichTextBox1.Paste();
                        }

                    }
                }
            }
            else if (e.Control && Keys.Up == e.KeyCode)
            {
                if (this.UiMain1.TreeView1.Focused)
                {
                    System.Console.WriteLine("ノードを上に移動したい。");
                    Actions.MoveNodeInSiblings(this.UiMain1, true);
                }
            }
            else if (e.Control && Keys.Down == e.KeyCode)
            {
                if (this.UiMain1.TreeView1.Focused)
                {
                    System.Console.WriteLine("ノードを下に移動したい。");
                    Actions.MoveNodeInSiblings(this.UiMain1, false);
                }
            }
            else if (e.Control && Keys.Left == e.KeyCode)
            {
                if (this.UiMain1.TreeView1.Focused)
                {
                    System.Console.WriteLine("ノードを左に移動したい。");
                    Actions.MoveNodeToParent(this.UiMain1);
                }
            }
            else if (e.Control && Keys.Right == e.KeyCode)
            {
                if (this.UiMain1.TreeView1.Focused)
                {
                    System.Console.WriteLine("ノードを子に移動したい。");
                    Actions.MoveNodeToChild(this.UiMain1);
                }
            }

        }

        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.OpenTef(this);
        }

        private void 上書き保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.Save(this);
        }

        private void 同じ階層に追加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.AddNodeToSibling(this.UiMain1, null);
        }

        private void 下の階層に追加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.AppendChildNode(this.UiMain1);
        }

        private void 上へ移動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.MoveNodeInSiblings(this.UiMain1, true);
        }

        private void 下へ移動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.MoveNodeInSiblings(this.UiMain1, false);
        }

        private void 親へ移動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.MoveNodeToParent(this.UiMain1);
        }

        private void 色の変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.SetNodeProperty(this.UiMain1);
        }

        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.DeleteNode(this.UiMain1);
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.ApplicationExit(this, sender, e);

            Application.Exit();
        }

        private void バージョン情報ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AboutDialog dlg = new AboutDialog();
            dlg.ShowDialog(this);
        }

        private void 階層付きテキストを読み込みToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.ImportStoryEditor(this.UiMain1);
        }

        private void 子へ移動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.MoveNodeToChild(this.UiMain1);
        }

        /// <summary>
        /// フォームを閉じる前。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //━━━━━
            //画像ファイルを削除するために、使用している画像を解放します。
            //━━━━━
            this.UiMain1.Clear();

            //essageBox.Show("Form1_FormClosing");
        }
    }
}
