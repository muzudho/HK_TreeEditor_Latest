using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// 
    /// </summary>
    public interface ConfigurationtreeToFunction
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s_Action"></param>
        /// <param name="bRequired"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        Expression_Node_Function Translate(
            Configurationtree_Node systemFunction_Conf,
            bool bRequired,
            Log_Reports log_Reports
        );

        //────────────────────────────────────────
        #endregion



    }
}
