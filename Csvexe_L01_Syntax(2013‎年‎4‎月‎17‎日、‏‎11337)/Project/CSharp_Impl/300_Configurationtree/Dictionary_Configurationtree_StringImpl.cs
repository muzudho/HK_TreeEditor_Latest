using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    public delegate void DELEGATE_StringAttributes(string sKey, string sValue, ref bool bBreak);


    public class Dictionary_Configurationtree_StringImpl : Dictionary_Configurationtree_String
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public Dictionary_Configurationtree_StringImpl(Configurationtree_Node owner_Conf)
        {
            this.owner = owner_Conf;
            this.dictionary_Attribute = new Dictionary<string, string>();
        }

        //────────────────────────────────────────

        /// <summary>
        /// new された直後の内容に戻します。
        /// </summary>
        public void Clear(Configurationtree_Node owner_Conf, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Clear", log_Reports);

            //
            //

            this.owner = null;
            this.dictionary_Attribute.Clear();

            //
            //
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// attr系要素の追加。
        /// 
        /// 既に追加されている要素は、追加できない。
        /// </summary>
        public void Add(
            string sKey,
            string sValue,
            Configuration_Node conf_Value,
            bool bRequired,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Add",log_Reports);

            //
            //

            if (!this.dictionary_Attribute.ContainsKey(sKey))
            {
                this.dictionary_Attribute.Add(sKey, sValue);
            }
            else
            {
                if (bRequired)
                {
                    // エラー
                    goto gt_Error_Duplicate;
                }
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Duplicate:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー345！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();

                s.Append("要素<");
                s.Append(this.owner.Name);
                s.Append(">に、同じ名前の属性が重複していました。");
                s.Newline();

                s.Append("入れようとした要素の名前=[");
                s.Append(sKey);
                s.Append("]");
                s.Newline();

                // ヒント
                s.Append(r.Message_Configuration(conf_Value));

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
        }

        //────────────────────────────────────────

        /// <summary>
        /// attr系要素の追加。
        /// </summary>
        public void Set(
            string sKey,
            string sValue,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Set",log_Reports);

            //
            //
            //
            //

            this.dictionary_Attribute[sKey] = sValue;

            //
            //
            //
            //
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 空白は、無いのと同じに扱う。
        /// </summary>
        /// <param name="sKey"></param>
        /// <param name="sResult"></param>
        /// <param name="bRequired"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public bool TryGetValue(
            PmName pmName,//Pmオブジェクトにしたい。
            out string sResult,
            bool bRequired,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "TryGetValue",log_Reports);
            //


            bool bHit = this.dictionary_Attribute.TryGetValue(pmName.Name_Pm, out sResult);
            if (!bHit || "" == sResult)
            {
                if (bRequired)
                {
                    goto gt_Error_NoHit;
                }
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NoHit:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("Er:004;", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();


                s.Append("name=\"");
                s.Append(pmName.Name_Attribute);
                s.Append("\" 属性か、または <arg name=\"");
                s.Append(pmName.Name_Pm);
                s.Append("\" ～> 要素のどちらかが必要でしたが、違う方を書いたか、記述されていないか、空文字列でした。");
                s.Newline();
                s.Newline();

                if (null != this.owner)
                {
                    //ヒント
                    s.Append(r.Message_Configuration(this.owner));
                }
                else
                {
                    s.Append("どの要素かは不明。");
                    s.Newline();
                }

                // ヒント

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
            return bHit;
        }

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        public bool ContainsKey(string sKey)
        {
            return this.dictionary_Attribute.ContainsKey(sKey);
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// このオブジェクトを所有しているオブジェクト。
        /// </summary>
        private Configurationtree_Node owner;

        //────────────────────────────────────────

        private Dictionary<string, string> dictionary_Attribute;

        public void ForEach(DELEGATE_StringAttributes dlgt1)
        {
            bool bBreak = false;
            foreach (KeyValuePair<string, string> kvp in this.dictionary_Attribute)
            {
                dlgt1(kvp.Key, kvp.Value, ref bBreak);

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
                return this.dictionary_Attribute.Count;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
