using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{

    /// <summary>
    /// 
    /// </summary>
    public class List_Expression_Node_StringImpl : List_Expression_Node_String
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public List_Expression_Node_StringImpl(Expression_Node_String owner)
        {
            this.owner_Expression = owner;
            this.listExpression_Item = new List<Expression_Node_String>();
        }

        //────────────────────────────────────────

        public void Clear(Log_Reports log_Reports)
        {
            this.listExpression_Item.Clear();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        public void ToText_Snapshot(Log_TextIndented txt)
        {
            txt.Increment();


            txt.Append("<" + this.GetType().Name + "クラス>");
            txt.Newline();

            foreach (Expression_Node_String ec_Item in this.listExpression_Item)
            {
                ec_Item.Cur_Configuration.ToText_Content(txt);
            }

            txt.Append("</" + this.GetType().Name + "クラス>");
            txt.Newline();


            txt.Decrement();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 追加。
        /// </summary>
        /// <param name="nItems"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        public void Add(
            Expression_Node_String ec_Child,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Add",log_Reports);


            if (ec_Child is Expression_Node_StringImpl)
            {
                ((Expression_Node_StringImpl)ec_Child).Parent_Expression = this.owner_Expression;
            }
            else if (ec_Child is Expression_Leaf_StringImpl)
            {
                ((Expression_Leaf_StringImpl)ec_Child).Parent_Expression = this.owner_Expression;
            }
            else if (ec_Child is Expression_TexttemplateP1pImpl)
            {
                ((Expression_TexttemplateP1pImpl)ec_Child).Parent_Expression = this.owner_Expression;
            }
            else
            {
                log_Method.WriteWarning_ToConsole(" 想定外のクラス=[" + ec_Child.GetType().Name + "]");
            }
            this.listExpression_Item.Add(ec_Child);

            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        public void SetList(
            List<Expression_Node_String> ecList_Item,
            Log_Reports log_Reports
            )
        {
            this.listExpression_Item = ecList_Item;
        }

        //────────────────────────────────────────

        public void AddList(
            List<Expression_Node_String> ecList_Item,
            Log_Reports log_Reports
            )
        {
            this.listExpression_Item.AddRange(ecList_Item);
        }

        //────────────────────────────────────────

        /// <summary>
        /// デバッグするのに使う内容を取得します。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="log_Reports"></param>
        public void ToText_Debug(Log_TextIndented s, Log_Reports log_Reports)
        {
            s.Append(this.GetType().Name + "#DebugWrite:E_String項目数＝[" + this.listExpression_Item.Count + "]");
            s.Newline();
            s.Append(this.GetType().Name + "#DebugWrite:──────────ここから");
            s.Newline();
            foreach (Expression_Node_String e_Str in this.listExpression_Item)
            {
                s.Append("E_String=[" + e_Str.Execute4_OnExpressionString(EnumHitcount.Unconstraint, log_Reports) + "]");
                s.Newline();
            }
            s.Append(this.GetType().Name + "#DebugWrite:──────────ここまで");
            s.Newline();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 親要素。
        /// </summary>
        private Expression_Node_String owner_Expression;

        //────────────────────────────────────────

        /// <summary>
        /// 子＜●●＞要素のリスト。
        /// </summary>
        private List<Expression_Node_String> listExpression_Item;

        public List<Expression_Node_String> SelectList(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            return this.listExpression_Item;
        }

        //────────────────────────────────────────

        public void ForEach(DELEGATE_Expression_Nodes dlgt1)
        {
            bool bBreak = false;
            bool bRemove = false;
            for (int nI = 0; nI < this.listExpression_Item.Count; nI++)
            {
                Expression_Node_String cur_Expression = this.listExpression_Item[nI];

                dlgt1(cur_Expression, ref bRemove, ref bBreak);

                if (bRemove)
                {
                    this.listExpression_Item.RemoveAt(nI);
                    nI--;
                    bRemove = false;
                }

                if (bBreak)
                {
                    break;
                }
            }
        }

        //────────────────────────────────────────

        public int Count
        {
            get
            {
                return this.listExpression_Item.Count;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
