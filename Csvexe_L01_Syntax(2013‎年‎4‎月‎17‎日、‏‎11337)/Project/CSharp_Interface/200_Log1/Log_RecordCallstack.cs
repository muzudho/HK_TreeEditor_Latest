using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{



    /// <summary>
    /// コールスタックの要素。
    /// </summary>
    public interface Log_RecordCallstack
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// コールスタックの表示。
        /// 
        /// 実行パス・パンくずリスト。
        /// </summary>
        /// <returns></returns>
        string ToText_Pathbreadcrumbs();

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// メソッドの場所情報。
        /// </summary>
        Log_Method Log_Method
        {
            get;
            set;
        }

        /// <summary>
        /// メソッドのどこか、という細かな場所情報。指定がないこともある。
        /// </summary>
        string Comment_Statement
        {
            get;
            set;
        }

        //────────────────────────────────────────
        #endregion



    }
}
