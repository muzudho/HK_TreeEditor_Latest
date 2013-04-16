using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{

    public delegate void DELEGATE_Expression_Nodes(Expression_Node_String expr_Node, ref bool bRemove, ref bool bBreak);
    public delegate void DELEGATE_Expression_Attributes(string sAttrName, Expression_Node_String ec_Attr, ref bool bBreak);

    /// <summary>
    /// ツリー構造の葉以外の節。
    /// 
    /// サブクラスは
    /// ・L01_Cushion:Ev_Elem、
    /// ・L03_Opyopyo:Ev_4ASelectRecord
    /// 
    /// 【方針】読み取り専用にしたい。
    /// </summary>
    public interface Expression_Node_String
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 内容を確認するのに使います。
        /// </summary>
        void ToText_Snapshot(Log_TextIndented s);

        //────────────────────────────────────────

        /// <summary>
        /// 属性を取得します。
        /// </summary>
        /// <param name="out_Expression_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        bool TrySelectAttribute_ExpressionFilepath(
            out Expression_Node_Filepath out_Expression_Result,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 属性を取得します。
        /// </summary>
        /// <param name="out_Expression_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        bool TrySelectAttribute(
            out Expression_Node_String out_Expression_Result,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 属性を取得します。
        /// </summary>
        /// <param name="out_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        bool TrySelectAttribute(
            out string out_SResult,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────

        /// <summary>
        /// 内部実装用。
        /// </summary>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        string Execute5_Main(
            Log_Reports log_Reports
            );

        /// <summary>
        /// ユーザー定義プログラムを実行。
        /// 
        /// ・「Execute_」系は、デバッグ出力のために使ってはいけません。
        /// </summary>
        /// <param name="request">どういう結果が欲しいかの指定。</param>
        /// <param name="log_Reports"></param>
        /// <returns>処理結果の結合文字列。</returns>
        string Execute4_OnExpressionString(
            EnumHitcount request,
            Log_Reports log_Reports
            );

        Expression_Node_Filepath Execute4_OnExpressionString_AsFilepath(
            EnumHitcount request,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 親E_Stringを遡って検索。一致するものがなければヌル。
        /// </summary>
        /// <returns></returns>
        Expression_Node_String GetParentExpressionOrNull(string sName_Node);

        /// <summary>
        /// 例えば　"data"　と指定すれば、
        /// 直下の子要素の中で　＜ｄａｔａ　＞ といったノード名を持つものはヒットする。
        /// 
        /// 読み取った要素をリストから削除するなら、bRemove=true とします。
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sExpectedValue"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        List<Expression_Node_String> SelectDirectchildByNodename(
            string sName_ExpectedNode, bool bRemove, EnumHitcount request, Log_Reports log_Reports);

        /// <summary>
        /// 文字列を、子要素として追加。
        /// </summary>
        /// <param name="sHumaninput"></param>
        /// <param name="parent_Conf"></param>
        /// <param name="log_Reports"></param>
        void AppendTextNode(
            string sHumaninput,
            Configuration_Node parent_Conf,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 属性="" マップ。
        /// </summary>
        Dictionary_Expression_Node_String Dictionary_Expression_Attribute
        {
            get;
        }

        /// <summary>
        /// 属性を上書きします。
        /// </summary>
        /// <param name="name_Attribute"></param>
        /// <param name="expr_Attribute"></param>
        /// <param name="log_Reports"></param>
        void SetAttribute(
            string name_Attribute,
            Expression_Node_String expr_Attribute,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────

        /// <summary>
        /// 親要素。
        /// 
        /// コンストラクターだけではなく、タイミングを遅らせて、後付けで設定されることもあります。
        /// </summary>
        Expression_Node_String Parent_Expression
        {
            get;
            set;
        }

        /// <summary>
        /// 設定場所のヒント。
        /// 
        /// コンストラクターだけではなく、タイミングを遅らせて、後付けで設定されることもあります。
        /// </summary>
        Configuration_Node Cur_Configuration
        {
            get;
            set;
        }

        /// <summary>
        /// 子要素リスト。
        /// </summary>
        List_Expression_Node_String List_Expression_Child
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }


}
