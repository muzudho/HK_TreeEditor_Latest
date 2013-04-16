using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{


    /// <summary>
    /// todo:使ってる？
    /// </summary>
    public interface Expressionv_Elem99 : Expression_Node_String
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// E_Executeの引数。
        /// </summary>
        /// <param name="request"></param>
        void SetDataRow(
            DataRow dataRow
            );

        //────────────────────────────────────────
        #endregion



    }


}
