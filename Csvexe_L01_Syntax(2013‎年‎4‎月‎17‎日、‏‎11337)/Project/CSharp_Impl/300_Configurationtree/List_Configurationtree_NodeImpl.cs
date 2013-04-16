using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// 主に、子要素を入れるのに使う。
    /// </summary>
    public class List_Configurationtree_NodeImpl : List_Configurationtree_Node
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public List_Configurationtree_NodeImpl(Configurationtree_Node owner_Conf)
        {
            this.owner = owner_Conf;
            this.list_Configurationtree_Node = new List<Configurationtree_Node>();
        }

        //────────────────────────────────────────

        /// <summary>
        /// リストを空にする。
        /// </summary>
        /// <param name="log_Reports"></param>
        public void Clear(
            Log_Reports log_Reports
            )
        {
            this.list_Configurationtree_Node.Clear();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public virtual void ToText_Content(Log_TextIndented s)
        {
            s.Increment();


            //
            // 子要素
            foreach (Configuration_Node cur_Conf in this.list_Configurationtree_Node)
            {
                cur_Conf.ToText_Content(s);
            }

            // 子要素しかありません。

            s.Decrement();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 追加。
        /// </summary>
        public void Add(
            Configurationtree_Node cur_Conf,
            Log_Reports log_Reports
            )
        {
            this.list_Configurationtree_Node.Add(cur_Conf);
        }

        //────────────────────────────────────────

        /// <summary>
        /// @Deprecated
        /// 一覧。
        /// </summary>
        public List<Configurationtree_Node> SelectList(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            return list_Configurationtree_Node;
        }

        //────────────────────────────────────────

        /// <summary>
        /// ノードを、リストのindexで指定して、取得します。
        /// 該当がなければヌルを返します。
        /// </summary>
        /// <param select="index">リストのインデックス</param>
        /// <param select="bRequired">該当するデータがない場合、エラー</param>
        /// <param select="log_Reports">警告メッセージ</param>
        /// <returns></returns>
        public Configurationtree_Node GetNodeByIndex(
            int nIndex,
            bool bRequired,
            Log_Reports log_Reports
            )
        {

            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "GetNodeByIndex",log_Reports);
            //
            //
            //
            //

            Configurationtree_Node gcav_FoundItem;

            if (0 <= nIndex && nIndex < this.list_Configurationtree_Node.Count)
            {
                gcav_FoundItem = this.list_Configurationtree_Node[nIndex];
            }
            else
            {
                gcav_FoundItem = null;

                if (bRequired)
                {
                    // エラーとして扱います。
                    goto gt_Error_BadIndex;
                }
            }

            goto gt_EndMethod;
        //
        //
        #region 異常系
        //────────────────────────────────────────
        gt_Error_BadIndex:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー097！!", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("指定されたノードは存在しませんでした。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("リストのインデックス=[");
                sb.Append(nIndex);
                sb.Append("]");
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
            return gcav_FoundItem;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private Configurationtree_Node owner;

        public Configurationtree_Node Owner
        {
            get
            {
                return owner;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// ＜f-●●＞要素のリスト。
        /// </summary>
        private List<Configurationtree_Node> list_Configurationtree_Node;

        public void ForEach(DELEGATE_Configurationtree_Nodes dlgt1)
        {
            bool bBreak = false;
            foreach (Configurationtree_Node cur_Conf in this.list_Configurationtree_Node)
            {
                dlgt1(cur_Conf, ref bBreak);

                if (bBreak)
                {
                    break;
                }
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 個数。
        /// </summary>
        public int Count
        {
            get
            {
                return list_Configurationtree_Node.Count;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
