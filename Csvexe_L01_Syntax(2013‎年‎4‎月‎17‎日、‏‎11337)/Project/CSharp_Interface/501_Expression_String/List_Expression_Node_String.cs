using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{
    public interface List_Expression_Node_String
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// new された直後の内容に戻します。
        /// </summary>
        /// <param name="log_Reports"></param>
        void Clear(Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        void ToText_Snapshot(Log_TextIndented s);

        /// <summary>
        /// 要素の追加。
        /// </summary>
        /// <param name="nItems"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        void Add(
            Expression_Node_String ec_Item,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 要素の追加。
        /// </summary>
        /// <param name="eItems"></param>
        /// <param name="log_Reports"></param>
        void AddList(
            List<Expression_Node_String> listExpression_Item,
            Log_Reports log_Reports
            );

        /// <summary>
        /// リストの上書き。
        /// </summary>
        /// <param name="eItems"></param>
        /// <param name="log_Reports"></param>
        void SetList(
            List<Expression_Node_String> listExpression_Item,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 子要素のリスト。
        /// </summary>
        List<Expression_Node_String> SelectList(
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



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// それぞれの要素。
        /// </summary>
        /// <param name="dlgt1"></param>
        void ForEach(DELEGATE_Expression_Nodes dlgt1);

        /// <summary>
        /// 要素の個数。
        /// </summary>
        int Count
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
