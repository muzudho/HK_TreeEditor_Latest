using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{


    /// <summary>
    /// フィールド定義。
    /// 旧名：Fielddef
    /// </summary>
    public interface Fielddef
    {



        #region アクション
        //────────────────────────────────────────

        Cell NewField(string nodeConfigtree, Log_Reports log_Reports);

        //────────────────────────────────────────

        /// <summary>
        /// string,int,boolを返します。未該当の時は空文字列を返します。
        /// </summary>
        /// <returns></returns>
        string ToString_Type();

        /// <summary>
        /// セルの型を返します。未該当の時はヌルを返します。
        /// </summary>
        /// <returns></returns>
        Type ToType_Field();

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// フィールドの名前。入力したままの文字列。
        /// </summary>
        string Name_Humaninput
        {
            get;
            set;
        }

        /// <summary>
        /// フィールドの名前。トリムして英字大文字に加工（アッパー）した文字列。読み取り専用。
        /// </summary>
        string Name_Trimupper
        {
            get;
        }

        /// <summary>
        /// フィールドの型。
        /// </summary>
        EnumTypeFielddef Type_Field
        {
            get;
            set;
        }

        /// <summary>
        /// フィールドについてのコメント。
        /// </summary>
        string Comment
        {
            set;
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
