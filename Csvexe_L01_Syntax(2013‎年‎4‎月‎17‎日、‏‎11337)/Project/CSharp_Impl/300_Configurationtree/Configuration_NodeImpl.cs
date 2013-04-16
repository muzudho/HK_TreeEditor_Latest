using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    public abstract class Configuration_NodeImpl : Configuration_Node
    {


        #region 生成と破棄
        //────────────────────────────────────────

        public Configuration_NodeImpl(string name_Node, Configuration_Node parent_Conf_OrNull)
        {
            this.name = name_Node;
            this.parent = parent_Conf_OrNull;
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        abstract public void ToText_Content(Log_TextIndented s);

        //────────────────────────────────────────

        /// <summary>
        /// 問題箇所ヒント。
        /// 
        /// 問題が起こったときに、設定ファイル等の修正箇所を示す説明などに利用。
        /// </summary>
        public virtual void ToText_Locationbreadcrumbs(Log_TextIndented s)
        {
            s.Increment();

            // 親のノード名を追加。
            if (null != this.Parent)
            {
                this.Parent.ToText_Locationbreadcrumbs(s);
                s.Append("/");
            }
            else
            {
                // このクラスがトップ・ノードだった場合。
                s.Append("問題個所ヒント（トップノード）：");
            }

            // 自分のノード名を追加。
            s.Append(this.Name);

            s.Decrement();
        }

        //────────────────────────────────────────

        /// <summary>
        /// ノード名を指定して、直近の親ノードを取得したい。
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="enumConf">「Configuration_Node」「Configurationtree_Node」のいずれか。</param>
        /// <param name="bRequired">偽を指定した時は、不一致の時ヌルを返す。</param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public virtual Configuration_Node GetParentByNodename(
            string sName, EnumConfiguration enumConf, bool bRequired, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "GetParentByNodename", log_Reports);
            //
            //
            Configuration_Node result;

            Configuration_Node err_Parent_Conf;
            if (log_Reports.Successful)
            {
                if (null != this.Parent)
                {
                    // 親要素があるとき

                    if (sName == this.Parent.Name)
                    {
                        // ノード名が一致
                        result = this.Parent;
                    }
                    else
                    {
                        // ノード名が一致しないとき
                        result = this.Parent.GetParentByNodename(sName, enumConf, bRequired, log_Reports);
                    }
                }
                else
                {
                    // 親要素がないとき

                    result = null;
                    err_Parent_Conf = null;
                    goto gt_Error_NotFoundParent;
                }
            }
            else
            {
                // 既にエラーが出ているとき

                result = null;
            }

            if (enumConf == EnumConfiguration.Tree)
            {
                if (!(result is Configurationtree_Node))
                {
                    //エラー
                    goto gt_Error_AnotherClass;
                }
            }
            else if (enumConf == EnumConfiguration.Unknown)
            {
                if (!(result is Configuration_Node))
                {
                    //エラー
                    goto gt_Error_AnotherClass;
                }
            }
            else
            {
                //エラー
                goto gt_Error_UnsupportedConfigurationType;
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NotFoundParent:
            if (log_Reports.CanCreateReport)
            {
                if (bRequired)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー501！", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append("親要素の取得に失敗しました。");
                    s.Newline();


                    s.Append("指定ノード名[");
                    s.Append(sName);
                    s.Append("]");
                    s.Newline();

                    s.Append("親要素はヌルです。");
                    s.Newline();

                    if (null != err_Parent_Conf)
                    {
                        s.Append("親要素ノード名[");
                        s.Append(err_Parent_Conf.Name);
                        s.Append("]");
                        s.Newline();
                    }

                    // ヒント
                    s.Append(r.Message_Configuration(this));

                    r.Message = s.ToString();
                    log_Reports.EndCreateReport();
                }
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_AnotherClass:
            if (log_Reports.CanCreateReport)
            {
                if (bRequired)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー502！", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append("（内部プログラム・エラー）取得した親要素は、指定のクラスとは異なりました。");
                    s.Newline();

                    s.Append("指定コンフィグ形[");
                    s.Append(enumConf);
                    s.Append("]");
                    s.Newline();

                    s.Append("取得した親要素のクラス名[");
                    s.Append(result.GetType().Name);
                    s.Append("]");
                    s.Newline();

                    // ヒント
                    s.Append(r.Message_Configuration(this));

                    r.Message = s.ToString();
                    log_Reports.EndCreateReport();
                }
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_UnsupportedConfigurationType:
            if (log_Reports.CanCreateReport)
            {
                if (bRequired)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー503！", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append("（内部プログラム・エラー）コンフィグ・クラスの指定に対応できませんでした。");
                    s.Newline();

                    s.Append("指定コンフィグ形[");
                    s.Append(enumConf);
                    s.Append("]");
                    s.Newline();

                    //s.Append("取得した親要素のクラス名[");
                    //s.Append(result.GetType().Name);
                    //s.Append("]");
                    //s.Newline();

                    // ヒント
                    s.Append(r.Message_Configuration(this));

                    r.Message = s.ToString();
                    log_Reports.EndCreateReport();
                }
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────
        #endregion




        #region プロパティー
        //────────────────────────────────────────

        private string name;

        /// <summary>
        /// ノード（要素、属性）の名前。fncや arg など。
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        //────────────────────────────────────────

        private Configuration_Node parent;

        /// <summary>
        /// 親要素。なければヌル。
        /// </summary>
        public Configuration_Node Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        //────────────────────────────────────────
        #endregion


    }
}
