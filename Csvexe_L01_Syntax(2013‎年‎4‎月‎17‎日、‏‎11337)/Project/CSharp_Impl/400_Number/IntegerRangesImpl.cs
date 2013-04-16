using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    /// <summary>
    /// 「-7~-5|-3~1|3|5~7|9|10|13~24」といった記述で数字を記述できる。
    /// </summary>
    public class IntegerRangesImpl : IntegerRanges
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public IntegerRangesImpl()
        {
            this.list_Item = new List<IntegerRange>();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        // 説明はインターフェース参照。
        public void ToNumbers(ref List<int> listN)
        {
            for (int nI = 0; nI < this.List_Item.Count; nI++)
            {
                IntegerRange o_Range = this.List_Item[nI];

                o_Range.ToNumbers(ref listN);
            }
        }

        //────────────────────────────────────────

        // 説明はインターフェース参照。
        public string ToCsv()
        {
            StringBuilder sb = new StringBuilder();

            for (int nI = 0; nI < this.List_Item.Count; nI++)
            {
                IntegerRange o_Range = this.List_Item[nI];

                sb.Append(o_Range.ToCsv());

                if (nI + 1 < this.List_Item.Count)
                {
                    sb.Append(",");
                }
            }

            return sb.ToString();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 「-7~-5|-3~1|3|5~7|9|10|13~24」といった書式で返します。
        /// </summary>
        /// <returns>「-7~-5|-3~1|3|5~7|9|10|13~24」といった書式の文字列。</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int nI = 0; nI < this.List_Item.Count; nI++)
            {
                IntegerRange o_Range = this.List_Item[nI];

                sb.Append(o_Range.ToString());

                if (nI + 1 < this.List_Item.Count)
                {
                    sb.Append("|");
                }
            }

            return sb.ToString();
        }

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        // 説明はインターフェース参照。
        public bool Contains(int nValue)
        {
            foreach (IntegerRange o_Range in this.list_Item)
            {
                if (o_Range.Number_First <= nValue && nValue <= o_Range.Number_Last)
                {
                    return true;
                }
            }

            return false;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private List<IntegerRange> list_Item;

        // 説明はインターフェース参照。
        public List<IntegerRange> List_Item
        {
            get
            {
                return list_Item;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
