using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Table
{

    /// <summary>
    /// 出力しないフィールド名のリスト。
    /// </summary>
    public class ExceptedFields
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public ExceptedFields()
        {
            this.list_SExceptedFields_Upper = new List<string>();
            this.list_SExceptedFields_Starts_Upper = new List<string>();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 出力しないフィールドの場合、真。
        /// </summary>
        /// <param name="fieldName_trimUpper"></param>
        /// <returns></returns>
        public bool TryExceptedField(string sName_Field_TrimUpper)
        {

            if (this.List_SExceptedFields_Upper.Contains(sName_Field_TrimUpper))
            {
                //
                // 出力しないフィールドの場合。（完全一致）
                //
                return true;
            }



            for (int nIndex = this.List_SExceptedFields_Starts_Upper.Count - 1; 0 <= nIndex; nIndex--)
            {
                string sStarts = this.List_SExceptedFields_Starts_Upper[nIndex];

                if (sName_Field_TrimUpper.StartsWith(sStarts))
                {
                    //
                    // 出力しないフィールドの場合。（前方一致）
                    //
                    return true;
                }
            }

            //f oreach (string starts in this.ExcFields_starts_listUpper)
            //{
            //    if (fieldName_trimUpper.StartsWith(starts))
            //    {
            //        //
            //        // 出力しないフィールドの場合。
            //        //
            //        return true;
            //    }
            //}

            return false;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private List<string> list_SExceptedFields_Upper;

        /// <summary>
        /// 出力しないフィールド名のリスト。
        /// 
        /// 英字は大文字にして入れてください。
        /// </summary>
        public List<string> List_SExceptedFields_Upper
        {
            set
            {
                list_SExceptedFields_Upper = value;
            }
            get
            {
                return list_SExceptedFields_Upper;
            }
        }

        //────────────────────────────────────────

        private List<string> list_SExceptedFields_Starts_Upper;

        /// <summary>
        /// 出力しないフィールド名の「開始文字列」リスト。
        /// 
        /// 英字は大文字にして入れてください。
        /// </summary>
        public List<string> List_SExceptedFields_Starts_Upper
        {
            set
            {
                list_SExceptedFields_Starts_Upper = value;
            }
            get
            {
                return list_SExceptedFields_Starts_Upper;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
