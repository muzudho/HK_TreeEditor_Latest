using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    /// <summary>
    /// 「-7~-5」「-3~1」「3」といった記述で数字を記述できる。
    /// </summary>
    public interface IntegerRange
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 「1」「2」「3」といった数字が、指定されたリストに追加されます。
        /// </summary>
        /// <param name="numbers">リストに、数字が追加されます。</param>
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
        /// 始値。
        /// </summary>
        int Number_First
        {
            set;
            get;
        }

        /// <summary>
        /// 終値。
        /// </summary>
        int Number_Last
        {
            set;
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
