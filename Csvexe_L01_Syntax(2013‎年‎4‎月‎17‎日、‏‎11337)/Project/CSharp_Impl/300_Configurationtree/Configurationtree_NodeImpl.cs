using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// 設定ファイル等から読み込んだデータを保持するオブジェクトは、これを継承することになる。
    /// </summary>
    public class Configurationtree_NodeImpl : Configuration_NodeImpl, Configurationtree_Node
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="name_Node"></param>
        /// <param name="parent_Conf_OrNull"></param>
        public Configurationtree_NodeImpl(string name_Node, Configuration_Node parent_Conf_OrNull)
            :base(name_Node, parent_Conf_OrNull)
        {
            this.list_Child = new List_Configurationtree_NodeImpl(this);

            this.dictionary_Attribute = new Dictionary_Configurationtree_StringImpl(this);
        }

        //────────────────────────────────────────

        /// <summary>
        /// new された直後の内容に戻します。
        /// </summary>
        public void Clear( string sName, Configurationtree_Node parent_Conf_OrNull, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Clear", log_Reports);

            //
            //
            //
            // 親
            //
            //
            //
            this.Parent = parent_Conf_OrNull;


            //
            //
            //
            // 自
            //
            //
            //
            this.Name = sName;


            //
            //
            //
            // 属性
            //
            //
            //
            this.Dictionary_Attribute.Clear( this, log_Reports);


            //
            //
            //
            // 子
            //
            //
            //
            this.list_Child.Clear(log_Reports);


            //
            //
            //
            // 親への連結は維持。
            //
            //
            //

            //
            //
            log_Method.EndMethod(log_Reports);
        }
        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public override void ToText_Content(Log_TextIndented s)
        {
            s.Increment();

            // ノード名
            s.AppendI(0, "<");
            s.Append(this.Name);
            s.Append(" ");

            //
            // 属性
            //
            this.Dictionary_Attribute.ForEach(delegate(string sKey, string sValue, ref bool bBreak)
            {
                s.Append(sKey);
                s.Append("=[");
                s.Append(sValue);
                s.Append("] ");
            });


            if (0 < this.list_Child.Count)
            {
                s.Append(">");
                s.Newline();

                // 子要素
                this.list_Child.ToText_Content(s);

                s.AppendI(0, "</");
                s.Append(this.Name);
                s.Append(">");
                s.Newline();
            }
            else
            {
                s.Append("/>");
                s.Newline();
            }


            s.Decrement();
        }

        //────────────────────────────────────────

        /// <summary>
        /// ノード名を指定して、直近の子ノードを取得したい。
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="bRequired">偽を指定した時は、要素数0のリストを返す。</param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public List<Configurationtree_Node> GetChildrenByNodename(string sName, bool bRequired, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "GetChildrenByNodename", log_Reports);
            //
            //
            List<Configurationtree_Node> result = new List<Configurationtree_Node>();

            if (log_Reports.Successful)
            {
                this.list_Child.ForEach(delegate(Configurationtree_Node child_Conf, ref bool bBreak)
                {
                    if (sName == child_Conf.Name)
                    {
                        // ノード名が一致
                        result.Add(child_Conf);
                    }
                    else
                    {
                        // ノード名が一致しないとき

                    }
                });
            }
            else
            {
                // 既にエラーが出ているとき
                goto gt_EndMethod;
            }

            if (result.Count < 1 && bRequired)
            {
                if (bRequired)
                {
                    goto gt_Error_EmptyHitChild;
                }
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
            //────────────────────────────────────────
        gt_Error_EmptyHitChild:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー502！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("該当した子要素がありませんでした。");
                s.Newline();


                s.Append("指定ノード名[");
                s.Append(sName);
                s.Append("]");
                s.Newline();

                // ヒント
                s.Append(r.Message_Configuration(this));

                r.Message = s.ToString();
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

        private List_Configurationtree_Node list_Child;

        /// <summary>
        /// 子要素のリスト。（格納順序を保つこと）
        /// </summary>
        public List_Configurationtree_Node List_Child
        {
            get
            {
                return list_Child;
            }
        }

        //────────────────────────────────────────

        private Dictionary_Configurationtree_String dictionary_Attribute;

        public Dictionary_Configurationtree_String Dictionary_Attribute
        {
            get
            {
                return this.dictionary_Attribute;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
