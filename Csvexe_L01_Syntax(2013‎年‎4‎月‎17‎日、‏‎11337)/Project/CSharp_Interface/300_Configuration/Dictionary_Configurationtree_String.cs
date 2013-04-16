using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{


    /// <summary>
    /// 属性の連想配列。
    /// </summary>
    public interface Dictionary_Configurationtree_String
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// new された直後の内容に戻します。
        /// </summary>
        void Clear(Configurationtree_Node owner_Conf, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 属性を追加します。
        /// </summary>
        /// <param name="sKey"></param>
        /// <param name="sValue"></param>
        /// <param name="cf_Value_ConfigSource"></param>
        /// <param name="bRequired"></param>
        /// <param name="log_Reports"></param>
        void Add(
            string sKey,
            string sValue,
            Configuration_Node conf_Value,
            bool bRequired,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 属性を上書きします。
        /// </summary>
        void Set(
            string sKey,
            string sValue,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 属性を取得します。
        /// </summary>
        /// <param name="pmName"></param>
        /// <param name="sResult"></param>
        /// <param name="bRequired"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        bool TryGetValue(
            PmName pmName,
            out string sResult,
            bool bRequired,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        /// <summary>
        /// 属性の有無を確認します。
        /// </summary>
        /// <param name="sKey"></param>
        /// <returns></returns>
        bool ContainsKey(string sKey);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 属性数。
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// それぞれの属性。
        /// </summary>
        /// <param name="dlgt1"></param>
        void ForEach(DELEGATE_StringAttributes dlgt1);

        //────────────────────────────────────────
        #endregion



    }
}
