using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{


    /// <summary>
    /// (関数名：RecCond)
    /// (record condition)
    /// </summary>
    public interface Recordcondition
    {



        #region プロパティー
        // ──────────────────────────────

        /// <summary>
        /// logic="" 属性。
        /// </summary>
        EnumLogic EnumLogic
        {
            get;
            //set;
        }

        // ──────────────────────────────

        /// <summary>
        /// 子＜ｒｅｃ－ｃｏｎｄ＞要素のリスト。
        /// </summary>
        List<Recordcondition> List_Child
        {
            get;
            set;
        }

        // ──────────────────────────────

        /// <summary>
        /// キーフィールド名。field="" 属性。
        /// </summary>
        string Name_Field
        {
            get;
            //set;
        }


        // ──────────────────────────────

        /// <summary>
        /// ope="" 属性。
        /// </summary>
        EnumOpe EnumOpe
        {
            get;
            set;
        }

        // ──────────────────────────────

        /// <summary>
        /// value="" 属性。
        /// </summary>
        string Value
        {
            get;
            set;
        }

        //──────────────────────────────

        /// <summary>
        /// 属性。
        /// </summary>
        Expression_Node_String Expression_Description
        {
            get;
            set;
        }

        //──────────────────────────────

        /// <summary>
        /// 問題箇所ヒント。
        /// </summary>
        Configuration_Node Parent
        {
            get;
        }

        //──────────────────────────────
        #endregion



    }
}
