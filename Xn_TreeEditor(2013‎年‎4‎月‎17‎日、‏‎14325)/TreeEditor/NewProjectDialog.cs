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
    public partial class NewProjectDialog : Form
    {
        public NewProjectDialog()
        {
            InitializeComponent();
        }

        private void FitSize()
        {
            this.uiNewProject1.Location = new Point(0,0);
            this.uiNewProject1.Width = this.ClientSize.Width;
            this.uiNewProject1.Height = this.ClientSize.Height;
        }

        private void NewProjectDialog_Load(object sender, EventArgs e)
        {
            this.FitSize();
        }

        private void NewProjectDialog_Resize(object sender, EventArgs e)
        {
            this.FitSize();
        }

        public UiNewProject UiNewProject
        {
            get
            {
                return this.uiNewProject1;
            }
        }

    }
}
