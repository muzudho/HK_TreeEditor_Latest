using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{
    public interface RecordSet
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// List＜DataRow＞ → Dictionary
        /// </summary>
        /// <param name="list_Row"></param>
        /// <param name="log_Reports"></param>
        void AddList(List<DataRow> list_Row, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        List<Dictionary<string, Cell>> List_Field
        {
            get;
            set;
        }

        //────────────────────────────────────────
        #endregion



    }
}
