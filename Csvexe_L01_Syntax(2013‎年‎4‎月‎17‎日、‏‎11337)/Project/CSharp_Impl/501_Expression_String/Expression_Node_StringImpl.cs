using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// 
    /// </summary>
    public class Expression_Node_StringImpl : Expression_Node_String
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="parent_Expression"></param>
        /// <param name="cur_Conf">生成時に指定できないものもある。</param>
        public Expression_Node_StringImpl(Expression_Node_String parent_Expression, Configuration_Node cur_Conf)
        {
            this.parent_Expression = parent_Expression;
            this.cur_Configuration = cur_Conf;

            enumHitcount = EnumHitcount.Unconstraint;
            this.ecList_Child = new List_Expression_Node_StringImpl(this);
            this.dictionary_Expression_Attribute = new Dictionary_Expression_Node_StringImpl(this.Cur_Configuration);
        }

        /// <summary>
        /// コンストラクター。
        /// node_Configurationtree を後で InitializeBeforeuse を使って指定する必要がある。
        /// </summary>
        /// <param name="parent_Expression"></param>
        public Expression_Node_StringImpl(Expression_Node_String parent_Expression) : this(parent_Expression, null)
        {
        }

        //────────────────────────────────────────

        /// <summary>
        /// コンストラクターで指定していれば、必要ない。
        /// </summary>
        /// <param name="cur_Cf"></param>
        public void InitializeBeforeuse(Configurationtree_Node cur_Cf)
        {
            this.cur_Configuration = cur_Cf;
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        public void ToText_Snapshot(Log_TextIndented s)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            Log_Reports log_Reports_ForSnapshot = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "ToText_Snapshot",log_Reports_ForSnapshot);

            log_Reports_ForSnapshot.BeginCreateReport(EnumReport.Dammy);
            s.Increment();


            // ノード名
            s.AppendI(0,"「E■[");
            s.Append(this.Cur_Configuration.Name);
            s.Append("]　");

            // クラス名
            s.Append("[");
            s.Append(this.GetType().Name);
            s.Append("]クラス　");

            s.Append("子値＝[");
            s.Append(this.Execute4_OnExpressionString(EnumHitcount.Unconstraint, log_Reports_ForSnapshot));
            s.Append("]");

            s.Append("」");
            s.Newline();

            // 属性リスト
            this.Dictionary_Expression_Attribute.ToText_Snapshot(s);

            // 子リスト
            if (this.List_Expression_Child.Count < 1)
            {
                s.AppendI(0, "子無し");
                s.Newline();
            }
            else
            {
                s.AppendI(0, "┌────────┐子数＝[");
                s.Append(this.List_Expression_Child.Count);
                s.Append("]");
                s.Newline();
                this.List_Expression_Child.ForEach(delegate(Expression_Node_String e_Child, ref bool bRemove, ref bool bBreak)
                {
                    e_Child.ToText_Snapshot(s);
                });
                s.AppendI(0, "└────────┘");
                s.Newline();
            }

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            s.Decrement();
            log_Reports_ForSnapshot.EndCreateReport();
            log_Method.EndMethod(log_Reports_ForSnapshot);
        }

        //────────────────────────────────────────

        /// <summary>
        /// E_Elm属性。
        /// </summary>
        /// <param name="out_E_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        public bool TrySelectAttribute_ExpressionFilepath(
            out Expression_Node_Filepath ec_Result_Out,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            )
        {
            return this.Dictionary_Expression_Attribute.TrySelect_ExpressionFilepath(
                out ec_Result_Out, sName, hits, log_Reports);
        }

        /// <summary>
        /// E_Elm属性。
        /// </summary>
        /// <param name="out_E_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        public bool TrySelectAttribute(
            out Expression_Node_String ec_Result_Out,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            )
        {
            return this.Dictionary_Expression_Attribute.TrySelect(
                out ec_Result_Out, sName, hits, log_Reports);
        }

        public bool TrySelectAttribute(
            out string sResult_Out,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            )
        {

            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "TrySelectAttribute", log_Reports);

            //
            //

            bool bResult = this.Dictionary_Expression_Attribute.TrySelect(out sResult_Out, sName, hits, log_Reports);

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return bResult;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 文字列を、子要素として追加。
        /// </summary>
        /// <param name="sHumanInput"></param>
        /// <param name="s_ParentNode"></param>
        /// <param name="log_Reports"></param>
        public void AppendTextNode(
            string sHumanInput,
            Configuration_Node parent_Conf,
            Log_Reports log_Reports
            )
        {
            Expression_Leaf_StringImpl ec_Child = new Expression_Leaf_StringImpl(null, parent_Conf);
            ec_Child.SetString(sHumanInput, log_Reports);

            this.List_Expression_Child.Add(ec_Child, log_Reports);
        }

        //────────────────────────────────────────

        public virtual string Execute5_Main(
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "E_ExecuteMain",log_Reports);

            //
            //

            StringBuilder sb_Result = new StringBuilder();

            List<Expression_Node_String> ecList_Child = this.List_Expression_Child.SelectList(EnumHitcount.Unconstraint, log_Reports);

            switch (this.enumHitcount)
            {
                case EnumHitcount.First_Exist:
                    {
                        //
                        // 最初の１件のみ。存在しない場合エラー。
                        //
                        if (0 < ecList_Child.Count)
                        {
                            Expression_Node_String ec_Child = ecList_Child[0];
                            string str1 = ec_Child.Execute4_OnExpressionString(this.enumHitcount, log_Reports);

                            sb_Result.Append(str1);
                        }
                        else
                        {
                            //
                            // エラー
                            goto gt_Error_NotFoundOne;
                        }
                    }
                    break;

                case EnumHitcount.First_Exist_Or_Zero:
                    {
                        //
                        // 最初の１件のみ。存在しない場合、空文字列。
                        //
                        if (0 < ecList_Child.Count)
                        {
                            Expression_Node_String ec_Child = ecList_Child[0];
                            string str1 = ec_Child.Execute4_OnExpressionString(this.enumHitcount, log_Reports);

                            sb_Result.Append(str1);
                        }
                        else
                        {
                            //
                            // 存在しないので、空文字列。
                            //

                            // そのままスルー。
                        }
                    }
                    break;

                case EnumHitcount.Unconstraint:
                    {
                        //
                        // 制限なし
                        //

                        foreach (Expression_Node_String ec_Child in ecList_Child)
                        {
                            string s1 = ec_Child.Execute4_OnExpressionString(this.enumHitcount, log_Reports);

                            sb_Result.Append(s1);
                        }

                    }
                    break;

                default:
                    {
                        //
                        // エラー
                        goto gt_Error_UndefinedEnum;
                    }
            }

            goto gt_EndMethod;
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NotFoundOne:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー111！", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("必ず、最初の１件を取得する指定でしたが、１件も存在しませんでした。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                // ヒント

                r.Message = sb_Result.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_UndefinedEnum:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー113！", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("this.requestItems.VolumeConstraintEnum=[");
                sb.Append(this.enumHitcount.ToString());
                sb.Append("]には、プログラム側でまだ未対応です。");

                // ヒント

                r.Message = sb_Result.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return sb_Result.ToString();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 内容を文字列型で返します。
        /// </summary>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public virtual string Execute4_OnExpressionString(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Execute4_OnExpressionString①", log_Reports);
            //
            //

            string sResult;
            sResult = this.Execute5_Main(log_Reports);

            //
            //
            //
            //

            // もとに戻す
            this.enumHitcount = EnumHitcount.Unconstraint;

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return sResult;
        }

        //────────────────────────────────────────

        /// <summary>
        /// このデータは、ファイルパス型だ、と想定して、ファイルパスを取得します。
        /// </summary>
        /// <returns></returns>
        public Expression_Node_Filepath Execute4_OnExpressionString_AsFilepath(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            return Expression_Node_StringImpl.Execute4_OnExpressionString_AsFilepath_Impl(this, request, log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// このデータは、ファイルパス型だ、と想定して、ファイルパスを取得します。
        /// </summary>
        /// <returns></returns>
        public static Expression_Node_Filepath Execute4_OnExpressionString_AsFilepath_Impl(
            Expression_Node_String ec_Caller,
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, ec_Caller, "Execute4_OnExpressionString_AsFilepath_Impl", log_Reports);
            //
            //
            //
            //

            Expression_Node_Filepath ec_Fpath_result;

            //
            // ファイルパス。
            string sFpath = ec_Caller.Execute5_Main(log_Reports);
            {
                Configurationtree_NodeFilepath cf_Fpath = new Configurationtree_NodeFilepathImpl("ファイルパス出典未指定L01_1", ec_Caller.Cur_Configuration);
                cf_Fpath.InitPath(sFpath, log_Reports);
                if (!log_Reports.Successful)
                {
                    // 既エラー。
                    ec_Fpath_result = null;
                    goto gt_EndMethod;
                }

                ec_Fpath_result = new Expression_Node_FilepathImpl(cf_Fpath);
            }

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return ec_Fpath_result;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 親E_Stringを遡って検索。一致するものがなければヌル。
        /// </summary>
        /// <param name="sName_Node"></param>
        /// <returns></returns>
        public Expression_Node_String GetParentExpressionOrNull(string sName_Node)
        {
            return Expression_Node_StringImpl.GetParentEOrNull_(this, sName_Node);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 親E_Stringを遡って検索。一致するものがなければヌル。
        /// </summary>
        /// <param name="sName_Node"></param>
        /// <returns></returns>
        public static Expression_Node_String GetParentEOrNull_(Expression_Node_String ec_Me, string sName_Node)
        {
            Expression_Node_String result;


            if (ec_Me.Parent_Expression == null)
            {
                result = null;
            }
            else if (ec_Me.Parent_Expression.Cur_Configuration.Name == sName_Node)
            {
                result = ec_Me.Parent_Expression;
            }
            else
            {
                result = ec_Me.Parent_Expression.GetParentExpressionOrNull(sName_Node);
            }

            return result;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 例えば　"data"　と指定すれば、
        /// 直下の子要素の中で　＜ｄａｔａ　＞ といったノード名を持つものはヒットする。
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sExpectedValue"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public List<Expression_Node_String> SelectDirectchildByNodename(
            string sExpectedNodeName, bool bRemove, EnumHitcount request, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Divide3Blocks",log_Reports);

            List<Expression_Node_String> result = new List<Expression_Node_String>();

            this.List_Expression_Child.ForEach(delegate(Expression_Node_String ec_Child, ref bool bRemove2, ref bool bBreak2)
            {
                if (log_Reports.Successful)
                {
                    if (ec_Child.Cur_Configuration.Name == sExpectedNodeName)
                    {
                        result.Add(ec_Child);

                        if (bRemove)
                        {
                            // 削除要求1があるとき、削除要求2を出します。
                            bRemove2 = true;
                        }


                        if (EnumHitcount.First_Exist == request ||
                            EnumHitcount.First_Exist_Or_Zero == request)
                        {
                            // 最初の１件で削除は終了。複数件ヒットするかどうかは判定しない。
                            bBreak2 = true;
                        }
                    }
                }
            });

            if (EnumHitcount.One == request)
            {
                // 必ず１件だけヒットする想定。

                if (result.Count != 1)
                {
                    goto gt_errorNotOne;
                }
            }
            else if (EnumHitcount.First_Exist == request)
            {
                // 必ずヒットする。複数件あれば、最初の１件だけ取得。

                if (0 == result.Count)
                {
                    goto gt_errorNoHit;
                }
                else if (1 < result.Count)
                {
                    result.RemoveRange(1, result.Count - 1);
                }
            }
            else if (EnumHitcount.First_Exist_Or_Zero == request)
            {
                // ヒットすれば最初の１件だけ、ヒットしなければ０件の想定。

                if (1 < result.Count)
                {
                    result.RemoveRange(1, result.Count - 1);
                }
            }
            else
            {
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_errorNoHit:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー102！", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("必ず、１件以上ヒットする指定でしたが、[");
                sb.Append(result.Count);
                sb.Append("]件ヒットしました。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                // ヒント

                r.Message = sb.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //
        //
        gt_errorNotOne:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー101！", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("必ず、１件のみ取得する指定でしたが、[");
                sb.Append(result.Count);
                sb.Append("]件取得しました。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                // ヒント

                r.Message = sb.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 例えば　"data"　と指定すれば、
        /// 直下の子要素の中で　＜ｄａｔａ　＞ といったノード名を持つものはヒットする。
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sExpectedValue"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public static List<Expression_Node_String> SelectItemByNodeName(
            List<Expression_Node_String> listExpression, string sName_ExpectedNode, bool bRemove, EnumHitcount request, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, "Expression_Node_StringImpl", "SelectItemByNodeName",log_Reports);

            List<Expression_Node_String> result = new List<Expression_Node_String>();


            for (int nI = 0; nI < listExpression.Count; nI++)
            {
                Expression_Node_String ec_Item = listExpression[nI];

                if (log_Reports.Successful)
                {
                    if (ec_Item.Cur_Configuration.Name == sName_ExpectedNode)
                    {
                        result.Add(ec_Item);

                        if (bRemove)
                        {
                            // 削除を要求します。
                            listExpression.RemoveAt(nI);
                            nI--;
                        }


                        if (EnumHitcount.First_Exist == request ||
                            EnumHitcount.First_Exist_Or_Zero == request)
                        {
                            // 最初の１件で終了。複数件ヒットするかどうかは判定しない。
                            break;
                        }
                    }
                }
            }


            if (EnumHitcount.One == request)
            {
                // 必ず１件だけヒットする想定。

                if (result.Count != 1)
                {
                    goto gt_errorNotOne;
                }
            }
            else if (EnumHitcount.First_Exist == request)
            {
                // 必ずヒットする。複数件あれば、最初の１件だけ取得。

                if (0 == result.Count)
                {
                    goto gt_errorNoHit;
                }
                else if (1 < result.Count)
                {
                    result.RemoveRange(1, result.Count - 1);
                }
            }
            else if (EnumHitcount.First_Exist_Or_Zero == request)
            {
                // ヒットすれば最初の１件だけ、ヒットしなければ０件の想定。

                if (1 < result.Count)
                {
                    result.RemoveRange(1, result.Count - 1);
                }
            }
            else
            {
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_errorNoHit:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー102！", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("必ず、１件以上ヒットする指定でしたが、[");
                sb.Append(result.Count);
                sb.Append("]件ヒットしました。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                // ヒント

                r.Message = sb.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //
        //
        gt_errorNotOne:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー101！", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("必ず、１件のみ取得する指定でしたが、[");
                sb.Append(result.Count);
                sb.Append("]件取得しました。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                // ヒント

                r.Message = sb.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private Configuration_Node cur_Configuration;

        /// <summary>
        /// 設定場所のヒント。
        /// </summary>
        public Configuration_Node Cur_Configuration
        {
            get
            {
                return this.cur_Configuration;
            }
            set
            {
                this.cur_Configuration = value;
            }
        }

        //────────────────────────────────────────

        private Expression_Node_String parent_Expression;

        /// <summary>
        /// 設定場所のヒント。
        /// </summary>
        public Expression_Node_String Parent_Expression
        {
            get
            {
                return this.parent_Expression;
            }
            set
            {
                this.parent_Expression = value;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// どういう結果が欲しいかの指定。
        /// </summary>
        private EnumHitcount enumHitcount;

        protected EnumHitcount EnumHitcount
        {
            get
            {
                return this.enumHitcount;
            }
        }

        /// <summary>
        /// どういう結果が欲しいかの指定。
        /// </summary>
        /// <param name="request"></param>
        public void SetEnumHitcount(
            EnumHitcount request
            )
        {
            enumHitcount = request;
        }

        //────────────────────────────────────────

        private List_Expression_Node_String ecList_Child;

        /// <summary>
        /// 子＜●●＞要素リスト。
        /// </summary>
        public List_Expression_Node_String List_Expression_Child
        {
            get
            {
                return ecList_Child;
            }
        }

        //────────────────────────────────────────

        private Dictionary_Expression_Node_String dictionary_Expression_Attribute;

        /// <summary>
        /// 属性="" マップ。
        /// </summary>
        public Dictionary_Expression_Node_String Dictionary_Expression_Attribute
        {
            get
            {
                return dictionary_Expression_Attribute;
            }
        }

        /// <summary>
        /// 属性を上書きします。
        /// </summary>
        /// <param name="name_Attribute"></param>
        /// <param name="expr_Attribute"></param>
        /// <param name="log_Reports"></param>
        public void SetAttribute(
            string name_Attribute,
            Expression_Node_String expr_Attribute,
            Log_Reports log_Reports
            )
        {
            this.Dictionary_Expression_Attribute.Set(name_Attribute, expr_Attribute, log_Reports);
        }

        //────────────────────────────────────────
        #endregion



    }
}
