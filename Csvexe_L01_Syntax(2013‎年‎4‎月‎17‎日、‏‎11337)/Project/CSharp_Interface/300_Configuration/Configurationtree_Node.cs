using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    public delegate void DELEGATE_Configurationtree_Nodes( Configurationtree_Node expr_Child, ref bool bBreak);

    /// <summary>
    /// 記述ファイル、記述要素をたどれる仕組み。
    /// 
    /// これはツリー構造なので、テーブルには向かない。
    /// 
    /// 読み取り専用。
    /// </summary>
    public interface Configurationtree_Node : Configuration_Node
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// new された直後の内容に戻します。
        /// </summary>
        void Clear(string sName, Configurationtree_Node parent_OrNull, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// ノード名を指定して、直近の子ノードを取得します。
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="bRequired">偽を指定した時は、要素数0のリストを返す。</param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        List<Configurationtree_Node> GetChildrenByNodename(string sName, bool bRequired, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 属性＝””。
        /// </summary>
        Dictionary_Configurationtree_String Dictionary_Attribute
        {
            get;
        }

        /// <summary>
        /// 子のリスト。
        /// </summary>
        List_Configurationtree_Node List_Child
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
