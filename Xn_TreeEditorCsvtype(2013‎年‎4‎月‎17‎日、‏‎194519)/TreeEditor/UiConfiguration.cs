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
    public partial class UiConfiguration : UserControl
    {

        public bool IsRtf
        {
            get
            {
                return this.radioButton2.Checked;
            }
            set
            {
                this.radioButton2.Checked = value;
            }
        }

        public UiConfiguration()
        {
            InitializeComponent();
        }
    }
}
