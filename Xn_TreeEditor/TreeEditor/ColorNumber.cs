using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeEditor
{

    /// <summary>
    /// 色
    /// </summary>
    public class ColorNumber
    {
        /// <summary>
        /// 赤
        /// </summary>
        private int red;
        public int Red
        {
            get
            {
                return red;
            }
            set
            {
                red = value;
            }
        }

        /// <summary>
        /// 緑
        /// </summary>
        private int green;
        public int Green
        {
            get
            {
                return green;
            }
            set
            {
                green = value;
            }
        }

        /// <summary>
        /// 青
        /// </summary>
        private int blue;
        public int Blue
        {
            get
            {
                return blue;
            }
            set
            {
                blue = value;
            }
        }

        /// <summary>
        /// 前景色のWebカラー表記
        /// </summary>
        public string Web
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("#");
                sb.Append(this.Red.ToString("X2"));
                sb.Append(this.Green.ToString("X2"));
                sb.Append(this.Blue.ToString("X2"));

                return sb.ToString();
            }
            set
            {
                string s = value;

                if (!s.StartsWith("#"))
                {

                }
                else if(s.Length==7)
                {
                    string r = s.Substring(1, 2);
                    string g = s.Substring(3, 2);
                    string b = s.Substring(5, 2);
                    this.Red = Convert.ToInt32(r, 16);
                    this.Green = Convert.ToInt32(g, 16);
                    this.Blue = Convert.ToInt32(b, 16);
                }
                else if (s.Length == 4)
                {
                    string r = s.Substring(1, 1);
                    string g = s.Substring(2, 1);
                    string b = s.Substring(3, 1);
                    this.Red = Convert.ToInt32(r, 16);
                    this.Green = Convert.ToInt32(g, 16);
                    this.Blue = Convert.ToInt32(b, 16);
                }
            }
        }

    }

}
