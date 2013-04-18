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
    public partial class ConfigurationDialog : Form
    {

        public UiConfiguration UiConfiguration1
        {
            get
            {
                return this.uiConfiguration1;
            }
        }

        public ConfigurationDialog()
        {
            InitializeComponent();
        }
    }
}
