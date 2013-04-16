using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// 「属性名」「Pm:属性名;」
    /// の２つの表記を持つ属性名。
    /// </summary>
    public interface PmName
    {



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 属性名。書式「Pm:☆;」。
        /// </summary>
        string Name_Pm
        {
            get;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 属性名の部分だけ。
        /// </summary>
        string Name_Attribute
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
