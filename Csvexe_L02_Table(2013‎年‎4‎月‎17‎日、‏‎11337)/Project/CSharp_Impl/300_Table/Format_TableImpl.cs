using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Table
{

    /// <summary>
    /// テーブルの内容保存方法などの設定。
    /// 
    /// 旧名：Format_Table_HumaninputImpl
    /// </summary>
    public class Format_TableImpl : Format_Table
    {



        #region プロパティー
        //────────────────────────────────────────

        private bool isRowColRev;

        /// <summary>
        /// 行と列がひっくり返っている（左から右へレコードが並んでいる）なら真。
        /// 
        /// 通常（上から下へレコードが並んでいる）なら偽。
        /// </summary>
        public bool IsRowcolumnreverse
        {
            get
            {
                return isRowColRev;
            }
            set
            {
                isRowColRev = value;
            }
        }

        //────────────────────────────────────────

        private bool isAllIntFields;

        /// <summary>
        /// 型定義のレコード（intやboolやstringが書いてあるところ）がなく、全フィールドがint型のテーブルの場合、真。
        /// </summary>
        public bool IsAllintfieldsActivated
        {
            get
            {
                return isAllIntFields;
            }
            set
            {
                isAllIntFields = value;
            }
        }

        //────────────────────────────────────────

        private bool isCommaEnding;

        /// <summary>
        /// 行の末尾を「,」で終える場合、真。
        /// </summary>
        public bool IsCommaending
        {
            get
            {
                return isCommaEnding;
            }
            set
            {
                isCommaEnding = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
