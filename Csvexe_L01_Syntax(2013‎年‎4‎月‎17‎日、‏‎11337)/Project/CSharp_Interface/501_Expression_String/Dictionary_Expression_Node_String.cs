using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{


    /// <summary>
    /// 連想配列。
    /// </summary>
    public interface Dictionary_Expression_Node_String
    {


        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 内容を確認するのに使います。
        /// </summary>
        void ToText_Snapshot(Log_TextIndented s);

        /// <summary>
        /// 要素を上書きします。
        /// </summary>
        /// <param name="nItems"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        void Set(
            string sName,
            Expression_Node_String ec_Item,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 項目を取得します。
        /// </summary>
        /// <param name="out_E_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        bool TrySelect_ExpressionFilepath(
            out Expression_Node_Filepath out_Expression_Result,
            string sName,
            EnumHitcount request,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 項目を取得します。
        /// </summary>
        /// <param name="out_E_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        bool TrySelect(
            out Expression_Node_String out_Expression_Result,
            string sName,
            EnumHitcount request,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 項目を取得します。
        /// </summary>
        /// <param name="out_SResult"></param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        bool TrySelect(
            out string out_SResult,
            string sName,
            EnumHitcount request,
            Log_Reports log_Reports
            );

        /// <summary>
        /// デバッグするのに使う内容を取得します。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="log_Reports"></param>
        void ToText_Debug(Log_TextIndented s, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        /// <summary>
        /// 要素の有無を確認します。
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        bool ContainsKey(string sName);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 要素のキー一覧。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        Dictionary<string, Expression_Node_String>.KeyCollection Keys(
            Log_Reports log_Reports
            );

        /// <summary>
        /// 要素の値一覧。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        Dictionary<string, Expression_Node_String>.ValueCollection Values(
            Log_Reports log_Reports
            );

        /// <summary>
        /// 要素数。
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// それぞれの要素。
        /// </summary>
        /// <param name="dlgt1"></param>
        void ForEach(DELEGATE_Expression_Attributes dlgt1);

        //────────────────────────────────────────
        #endregion



    }
}
