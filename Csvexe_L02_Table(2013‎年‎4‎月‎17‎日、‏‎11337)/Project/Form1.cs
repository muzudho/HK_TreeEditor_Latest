using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xenon.Table
{
    public partial class Form_CsvTest : Form
    {
        public Form_CsvTest()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CsvLineParserImpl csvParser = new CsvLineParserImpl();

            List<string> list = csvParser.UnescapeLineToFieldList("\"    if ( $s4 == \"\",\"\" ){\"", ',');

            int num = 0;
            foreach (string field in list)
            {
                System.Console.Write("("+num+") field=["+field+"]");
                num++;
            }
        }
    }
}
