using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    /// <summary>
    /// 「-7~-5|-3~1|3|5~7|9|10|13~24」といった記述で数字を記述できる。
    /// </summary>
    public interface IntegerRanges
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 「1」「2」「3」といった数字が、指定されたリストに追加されます。
        /// </summary>
        /// <returns>リストに、数字が追加されます。</returns>
        void ToNumbers(ref List<int> listN);

        /// <summary>
        /// 「1,2,3」といった書式で返します。
        /// </summary>
        /// <returns>「1,2,3」といった書式の文字列。</returns>
        string ToCsv();

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        /// <summary>
        /// 指定の数値を含むか否かを判定。
        /// </summary>
        /// <param name="nValue"></param>
        /// <returns></returns>
        bool Contains(int nValue);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 数値の範囲のリスト。
        /// </summary>
        List<IntegerRange> List_Item
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
