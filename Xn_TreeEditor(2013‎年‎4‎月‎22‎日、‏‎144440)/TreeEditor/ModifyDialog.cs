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
    public partial class ModifyDialog : Form
    {
        public ModifyDialog()
        {
            InitializeComponent();
        }

        private int Movement
        {
            get
            {
                string s = this.movementTxt.Text;
                int n;

                if (int.TryParse(s, out n))
                {

                }

                return n;
            }
        }

        private void MoveAllImages(int moveX, int moveY)
        {
            if (0 != moveX || 0 != moveY)
            {
                Form1 form1 = (Form1)this.Owner;

                bool isChangeResource = false;

                foreach (Control c in form1.UiMain1.UiTextside1.RichTextBox1.Controls)
                {
                    PictBox p = (PictBox)c;

                    p.Location = new Point(p.Location.X + moveX, p.Location.Y + moveY);
                    isChangeResource = true;
                    p.Refresh();
                }

                if (isChangeResource)
                {
                    form1.UiMain1.Contents.IsChangedCsv= true;
                    form1.UiMain1.RefreshTitleBar();
                }
            }
        }

        /// <summary>
        /// 上ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upBtn_Click(object sender, EventArgs e)
        {
            this.MoveAllImages(0, -this.Movement);
        }

        /// <summary>
        /// 右ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rightBtn_Click(object sender, EventArgs e)
        {
            this.MoveAllImages(this.Movement, 0);
        }

        /// <summary>
        /// 下ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downBtn_Click(object sender, EventArgs e)
        {
            this.MoveAllImages(0, this.Movement);
        }

        /// <summary>
        /// 左ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void leftBtn_Click(object sender, EventArgs e)
        {
            this.MoveAllImages(-this.Movement, 0);
        }


    }
}
