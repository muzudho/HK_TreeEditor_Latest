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
    public partial class TextPropertyDialog : Form
    {

        public UiTextProperty UiTextProperty1
        {
            get
            {
                return this.uiTextProperty1;
            }
        }

        public TextPropertyDialog()
        {
            InitializeComponent();
        }
    }
}
