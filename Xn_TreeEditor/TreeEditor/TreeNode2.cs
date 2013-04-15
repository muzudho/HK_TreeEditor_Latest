using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeEditor
{


    /// <summary>
    /// 追加情報付きツリーノード。
    /// </summary>
    public class TreeNode2 : System.Windows.Forms.TreeNode
    {

        /// <summary>
        /// 前景色
        /// </summary>
        private ColorNumber fore;
        public ColorNumber Fore
        {
            get
            {
                return fore;
            }
            set
            {
                fore = value;
            }
        }

        /// <summary>
        /// 後景色
        /// </summary>
        private ColorNumber back;
        public ColorNumber Back
        {
            get
            {
                return back;
            }
            set
            {
                back = value;
            }
        }


        public TreeNode2()
        {
            this.Fore = new ColorNumber();

            this.Back = new ColorNumber();
            this.Back.Red = 255;
            this.Back.Green = 255;
            this.Back.Blue = 255;
        }

    }


}
