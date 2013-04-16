using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{



    #region 用意
    //────────────────────────────────────────

    public delegate void DELEGATE_Fielddefs(Fielddef fielddefinition, ref bool isBreak, Log_Reports log_Reports);

    //────────────────────────────────────────
    #endregion



    public interface RecordFielddef
    {



        #region アクション
        //────────────────────────────────────────

        void Add(Fielddef fielddefinition);

        void Insert(int indexColumn, Fielddef fielddefinition);

        Fielddef ValueAt(int index);

        //────────────────────────────────────────

        void ForEach(DELEGATE_Fielddefs delegate_Fielddefs, Log_Reports log_Reports);

        //────────────────────────────────────────

        /// <summary>
        /// デバッグ用に内容をダンプします。
        /// </summary>
        /// <returns></returns>
        string ToString_DebugDump();

        //────────────────────────────────────────

        /// <summary>
        /// 主に、データベースのフィールド名のindexを調べるのに利用されます。
        /// </summary>
        /// <param name="expected"></param>
        /// <returns>該当がなければ -1。</returns>
        int ColumnIndexOf_Trimupper(string expected);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        int Count
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
