using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    /// <summary>
    /// 「-7~-5」「-3~1」「3」といった記述で数字を記述できる。
    /// </summary>
    public class IntegerRangeImpl : IntegerRange
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="nSingle">数値</param>
        public IntegerRangeImpl(int nSingle)
        {
            this.number_First = nSingle;
            this.number_Last = nSingle;
        }

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="nFirst">始値</param>
        /// <param name="nLast">終値</param>
        public IntegerRangeImpl(int nFirst, int nLast)
        {
            this.number_First = nFirst;
            this.number_Last = nLast;
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        // 説明はインターフェース参照。
        public void ToNumbers(ref List<int> listN)
        {
            for (int nI = this.number_First; nI <= this.number_Last; nI++)
            {
                listN.Add(nI);
            }
        }

        //────────────────────────────────────────

        // 説明はインターフェース参照。
        public string ToCsv()
        {
            StringBuilder sb = new StringBuilder();

            for (int nI = this.number_First; nI <= this.number_Last; nI++)
            {
                sb.Append(nI);

                if (nI + 1 <= this.number_Last)
                {
                    sb.Append(",");
                }
            }

            return sb.ToString();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 「1」や、「3~5」といった文字列を返す。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (this.number_First == this.number_Last)
            {
                sb.Append(this.number_First);
            }
            else
            {
                sb.Append(this.number_First);
                sb.Append("~");
                sb.Append(this.number_Last);
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
            if (this.number_First <= nValue && nValue <= this.number_Last)
            {
                return true;
            }

            return false;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private int number_First;

        // 説明はインターフェース参照。
        public int Number_First
        {
            set
            {
                number_First = value;
            }
            get
            {
                return number_First;
            }
        }

        //────────────────────────────────────────

        private int number_Last;

        // 説明はインターフェース参照。
        public int Number_Last
        {
            set
            {
                number_Last = value;
            }
            get
            {
                return number_Last;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
