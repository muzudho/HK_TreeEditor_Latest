using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Table
{

    /// <summary>
    /// エラー入力があった場合に取る操作の、指定。
    /// 
    /// 旧名：EnumValueErrorSupport
    /// </summary>
    public enum EnumOperationIfErrorvalue
    {



        #region 用意
        //────────────────────────────────────────

        /// <summary>
        /// エラー入力は、エラー。
        /// </summary>
        Error,

        /// <summary>
        /// エラー入力は、エラー。
        /// ただし、空白は、指定の値で置き換えます。
        /// </summary>
        Spaces_To_Alt_Value

        //────────────────────────────────────────
        #endregion



    }
}
