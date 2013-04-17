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
        /// ファイル名。
        /// </summary>
        private string file;
        public string File
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
            }
        }

        public static string RtfToCsv(string rtfRel)
        {
            string csvRel;

            if (rtfRel.ToLower().EndsWith(".rtf"))
            {
                csvRel = rtfRel.Substring(0, rtfRel.Length - ".rtf".Length) + ".csv";
            }
            else
            {
                csvRel = "";
            }

            return csvRel;
        }

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

        private void Clear()
        {
            // タグはクリアしていません。

            this.File = "";

            this.Back = new ColorNumber();
            this.Back.Red = 255;
            this.Back.Green = 255;
            this.Back.Blue = 255;

            this.Fore = new ColorNumber();
        }

        public override object Clone()
        {
            TreeNode2 tn = (TreeNode2)base.Clone();

            tn.Back.Red = this.Back.Red;
            tn.Back.Green = this.Back.Green;
            tn.Back.Blue = this.Back.Blue;

            tn.File = this.File;

            tn.Fore.Red = this.Fore.Red;
            tn.Fore.Green = this.Fore.Green;
            tn.Fore.Blue = this.Fore.Blue;



            return tn;
        }

        /// <summary>
        /// Clone用。
        /// </summary>
        public TreeNode2()
        {
            this.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag">アプリケーション起動中、重複しない値を入れる。</param>
        public TreeNode2(long tag)
        {
            this.Clear();
            this.Tag = tag;
        }

    }


}
