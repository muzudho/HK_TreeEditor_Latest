using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{

    /// <summary>
    /// セル値。型付き。
    /// (Value)
    /// 旧名：Value_Humaninput
    /// </summary>
    public interface Cell : Configurationtree_Node
    {



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 文字列データを int型や bool型などに変換済みなら真、
        /// できていないなら偽。
        /// 空白は真。
        /// </summary>
        bool IsValidated
        {
            get;
        }

        /// <summary>
        /// 入力データそのままの形。
        /// </summary>
        string Text
        {
            get;
            set;
        }

        //────────────────────────────────────────
        #endregion



    }
}
