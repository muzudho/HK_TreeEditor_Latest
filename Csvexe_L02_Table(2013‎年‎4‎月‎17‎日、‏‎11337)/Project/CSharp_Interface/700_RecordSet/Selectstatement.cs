using System;
using System.Collections.Generic;
using System.Data;//DataRow
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{

    /// <summary>
    /// セレクト文。
    /// (select statement)
    /// </summary>
    public interface Selectstatement
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// ｓｅｌｅｃｔ属性。「id,name」といったカンマ区切り文字列。
        /// </summary>
        /// <returns></returns>
        string ToSelectFieldNameListCsv();

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// ｓｅｌｅｃｔ属性。
        /// </summary>
        List<string> List_SName_SelectField
        {
            get;
            set;
        }

        //────────────────────────────────────────

        /// <summary>
        /// ｉｎｔｏ属性。
        /// </summary>
        Expression_Node_String Expression_Into
        {
            get;
            set;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 一時記憶から、レコードセットのロードをするか否か。
        /// </summary>
        Expression_Node_String Expression_Where_RecordSetLoadFrom
        {
            get;
            set;
        }

        //────────────────────────────────────────

        /// <summary>
        /// ＜ｗｈｅｒｅ　ｌｏｇｉｃ＝”☆”＞
        /// </summary>
        EnumLogic EnumWherelogic
        {
            get;
            set;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 条件。
        /// </summary>
        List<Recordcondition> List_Recordcondition
        {
            get;
            set;
        }

        //────────────────────────────────────────

        /// <summary>
        /// required="" 属性。
        /// </summary>
        string Required
        {
            get;
            set;
        }

        //────────────────────────────────────────

        /// <summary>
        /// from="" 属性。
        /// </summary>
        Expression_Node_String Expression_From
        {
            get;
            set;
        }

        //────────────────────────────────────────

        /// <summary>
        /// storage="" 属性。
        /// </summary>
        string Storage
        {
            get;
            set;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 属性。
        /// </summary>
        string Description
        {
            get;
            set;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 問題箇所ヒント。
        /// </summary>
        Configuration_Node Parent
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
