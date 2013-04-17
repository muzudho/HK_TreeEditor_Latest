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
            this.FitSize();
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

        }
    }
}
