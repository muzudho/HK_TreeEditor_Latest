using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TreeEditor
{
    public partial class PicturePropertyDlg : Form
    {


        public UiPictureProperty UiPictureProperty1
        {
            get
            {
                return this.uiPictureProperty1;
            }
        }

        public PicturePropertyDlg()
        {
            InitializeComponent();
        }
    }
}
