using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{
    /// <summary>
    /// 条件。
    /// (関数：RecCond)
    /// </summary>
    public class RecordconditionImpl : Recordcondition
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="s_ParentNode"></param>
        private RecordconditionImpl(Configuration_Node parent_Conf)
        {
            this.parent = parent_Conf;

            this.list_Child = new List<Recordcondition>();
            this.enumLogic = EnumLogic.None;
            this.sField = "(▲未初期化102！)";
            this.enumOpe = EnumOpe.Eq;
            this.sValue = "";
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// インスタンスを作ります。
        /// </summary>
        /// <param name="out_recCond"></param>
        /// <param name="logic"></param>
        /// <param name="sField"></param>
        /// <param name="s_ParentNode"></param>
        /// <returns></returns>
        public static bool TryBuild(
            out Recordcondition out_RecCond,
            EnumLogic enumLogic,
            string sField,
            Configuration_Node parent_Conf,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, "RecCondImpl", "TryBuild",log_Reports);

            bool bSuccessful;

            sField = sField.Trim();

            if (EnumLogic.None == enumLogic && "" != sField)
            {
                // 条件式

                RecordconditionImpl rc = new RecordconditionImpl(parent_Conf);
                rc.sField = sField;
                out_RecCond = rc;
                bSuccessful = true;
            }
            else if (EnumLogic.None != enumLogic && "" == sField)
            {
                // グループ

                RecordconditionImpl rc = new RecordconditionImpl(parent_Conf);
                rc.sField = "(▲グループにフィールド属性無し103！[" + enumLogic + "])";
                rc.enumLogic = enumLogic;
                out_RecCond = rc;
                bSuccessful = true;
            }
            else
            {
                out_RecCond = null;
                bSuccessful = false;
                goto gt_Error_Attribute;
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Attribute:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー101！", log_Method);

                StringBuilder s = new StringBuilder();
                s.Append("＜ｒｅｃ－ｃｏｎｄ＞インスタンスを作成する引数にエラー。");
                s.Append(Environment.NewLine);
                s.Append("logic=[");
                s.Append(enumLogic);
                s.Append("] sField=[");
                s.Append(sField);
                s.Append("]");
                s.Append(Environment.NewLine);

                s.Append(r.Message_Configuration(parent_Conf));

                r.Message = s.ToString();
                log_Reports.EndCreateReport();

                //throw new Exception(s.ToString());
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return bSuccessful;
        }

        //────────────────────────────────────────

        public override string ToString()
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            Log_Reports log_Reports_ForString = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "ToString",log_Reports_ForString);

            log_Reports_ForString.BeginCreateReport(EnumReport.Dammy);
            //
            //

            StringBuilder sb = new StringBuilder();

            if (EnumLogic.None != this.EnumLogic)
            {
                sb.Append("グループ ");
                sb.Append(Utility_Table.LogicSymbolToString(this.EnumLogic));
                sb.Append(" ");
                sb.Append("子数");
                sb.Append(this.List_Child.Count);
            }
            else
            {
                sb.Append("条件 ");
                sb.Append(this.Name_Field);
                sb.Append(Utility_Table.OpeSymbolToString(this.EnumOpe));
                sb.Append(this.Value);
                sb.Append(" 子数");
                sb.Append(this.List_Child.Count);
            }

            goto gt_EndMethod;
            //
            //
        gt_EndMethod:
            log_Reports_ForString.EndCreateReport();
            log_Method.EndMethod(log_Reports_ForString);
            return sb.ToString();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private EnumLogic enumLogic;

        /// <summary>
        /// logic="" 属性。
        /// </summary>
        public EnumLogic EnumLogic
        {
            get
            {
                return enumLogic;
            }
            set
            {
                enumLogic = value;
            }
        }

        //────────────────────────────────────────

        private List<Recordcondition> list_Child;

        /// <summary>
        /// 子＜ｒｅｃ－ｃｏｎｄ＞要素のリスト。
        /// </summary>
        public List<Recordcondition> List_Child
        {
            get
            {
                return list_Child;
            }
            set
            {
                list_Child = value;
            }
        }

        //────────────────────────────────────────

        private string sField;

        /// <summary>
        /// キーフィールド名。 field="" 属性。
        /// </summary>
        public string Name_Field
        {
            get
            {
                return sField;
            }
            set
            {
                sField = value;
            }
        }

        //────────────────────────────────────────

        private EnumOpe enumOpe;

        /// <summary>
        /// ope="" 属性。
        /// </summary>
        public EnumOpe EnumOpe
        {
            get
            {
                return enumOpe;
            }
            set
            {
                enumOpe = value;
            }
        }

        //────────────────────────────────────────

        private string sValue;

        /// <summary>
        /// value="" 属性。
        /// （旧 ｌｏｏｋｕｐ－ｖａｌｕｅ="" 属性）
        /// </summary>
        public string Value
        {
            get
            {
                return sValue;
            }
            set
            {
                sValue = value;
            }
        }

        //────────────────────────────────────────

        private Expression_Node_String expression_Description;

        /// <summary>
        /// 属性。
        /// </summary>
        public Expression_Node_String Expression_Description
        {
            get
            {
                return expression_Description;
            }
            set
            {
                expression_Description = value;
            }
        }

        //────────────────────────────────────────

        private Configuration_Node parent;

        /// <summary>
        /// 問題箇所ヒント。
        /// </summary>
        public Configuration_Node Parent
        {
            get
            {
                return this.parent;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
