using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

namespace TreeEditor
{

    /// <summary>
    /// ピクチャーボックスに属性追加。
    /// </summary>
    public class PictBox : PictureBox
    {


        /// <summary>
        /// 全体の中での位置。画像の中心座標。
        /// </summary>
        private Point locationCInAll;
        public Point LocationCInAll
        {
            get
            {
                return locationCInAll;
            }
            set
            {
                locationCInAll = value;
            }
        }



        public PictBox()
        {
            this.LocationCInAll = new Point();
        }

        public void ReJustLocation(Point head)
        {
            // 中心座標を、左上座標に変換。
            this.Location = new Point(
                this.LocationCInAll.X - this.Width/2 - head.X,
                this.LocationCInAll.Y - this.Height/2 - head.Y
                );
        }

    }


}
