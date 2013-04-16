using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{


    /// <summary>
    /// 「%1%」や、「%2%」といったシンボルを含むテキストの Expression を作成するものです。
    /// 
    /// 旧名：TextP1p
    /// </summary>
    public interface Builder_TexttemplateP1p
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// Expression系オブジェクトを作成します。
        /// </summary>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        Expression_Node_String Compile(
            Log_Reports log_Reports
            );

        /// <summary>
        /// 実行します。
        /// </summary>
        /// <returns></returns>
        String Perform(Log_Reports log_Reports);

        /// <summary>
        /// 登録されている「%1%」、「%2%」といった記号の数字を一覧します。
        /// リストに「1」、「2」といった数字に置き換えて返します。
        /// </summary>
        /// <returns></returns>
        List<int> ExistsP1pNumbers(
            Dictionary_Expression_Node_String dic_Expr_Attr,
            Log_Reports log_Reports
            );

        /// <summary>
        /// [1]=101
        /// [2]=赤
        /// といったディクショナリー。
        /// 
        /// キーは %1%や、%2%といった名前の中の数字。[1]から始める。
        /// Xn_L05_E:E_FtextTemplate#E_ExecuteでAddされます。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="log_Reports"></param>
        void SetParameter(int key, string value, Log_Reports log_Reports);

        void TryGetParameter(out string out_Value, int key, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 「%1%:%2%」といった文字列（テキスト_テンプレートと呼ぶ）。
        /// </summary>
        string Text
        {
            get;
            set;
        }

        ///// <summary>
        ///// [1]=101
        ///// [2]=赤
        ///// といったディクショナリー。
        ///// 
        ///// キーは %1%や、%2%といった名前の中の数字。[1]から始める。
        ///// Xn_L01_Syntax:E_FtextTemplate#E_ExecuteでAddされます。
        ///// </summary>
        //Dictionary<int, string> Dictionary_NumberAndValue_Parameter
        //{
        //    get;
        //}

        //────────────────────────────────────────
        #endregion



    }
}
