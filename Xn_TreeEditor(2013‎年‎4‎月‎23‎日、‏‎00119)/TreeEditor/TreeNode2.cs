﻿using System;
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
        /// RTFファイル名。プロジェクトからの相対パス。
        /// </summary>
        private string rtfRel;
        public string RtfRel
        {
            get
            {
                return rtfRel;
            }
            set
            {
                rtfRel = value;
            }
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

            this.RtfRel = "";

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

            tn.RtfRel = this.RtfRel;

            tn.Fore.Red = this.Fore.Red;
            tn.Fore.Green = this.Fore.Green;
            tn.Fore.Blue = this.Fore.Blue;

            //タグはクローンしていません。

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
