using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;//WarningReports


namespace Xenon.Table
{

    /// <summary>
    /// 旧名：String_HumaninputImpl
    /// </summary>
    public class StringCellImpl : AbstractCellImpl
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="sourceHintName"></param>
        public StringCellImpl(String nodeConfigtree)
            : base(nodeConfigtree)
        {

        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public override void ToText_Content(Log_TextIndented txt)
        {
            txt.Increment();


            txt.AppendI(0, "<");
            txt.Append(this.GetType().Name);
            txt.Append("クラス");

            txt.AppendI(1, "humanInputString=[");
            txt.Append(this.Text);
            txt.Append("]");

            txt.AppendI(0, ">");
            txt.Newline();


            txt.Decrement();
        }

        //────────────────────────────────────────

        public bool TryGet(out string result)
        {
            result = this.Text;
            return true;
        }

        public string GetString()
        {
            return this.Text;
        }

        public void SetString(string value)
        {
            this.Text = value;
            isSpaced = ("" == this.Text.Trim());
        }

        //────────────────────────────────────────

        static public bool TryParse(
            object data,
            out string s_Out,
            string sDebugConfigStack_Table,
            string sDebugConfigStack_Field,
            Log_Method log_Method,
            Log_Reports log_Reports)
        {
            bool bResult;

            if (data is StringCellImpl)
            {
                s_Out = ((StringCellImpl)data).GetString();
                bResult = true;
            }
            else if (data is DBNull)
            {
                //
                // 空欄は空文字列に。
                s_Out = "";
                bResult = true;
            }
            else if (null == data)
            {
                //
                // エラー
                goto gt_Error_Null;
            }
            else if (!(data is Cell))
            {
                //
                // エラー
                goto gt_Error_NotCellData;
            }
            else
            {
                //
                // エラー
                goto gt_Error_AnotherType;
            }

            // 正常
            goto gt_EndMethod;
            //
        // エラー。
        //────────────────────────────────────────
        gt_Error_Null:
            s_Out = "";//ゴミ値
            bResult = false;
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー241！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("指定の引数dataに、StringCellData型の値を指定してください。空っぽ(null)でした。");

                s.Append(Environment.NewLine);
                s.Append("debugHintName=[");
                s.Append(sDebugConfigStack_Table);
                s.Append(".");
                s.Append(sDebugConfigStack_Field);
                s.Append("]");

                s.Append(Environment.NewLine);
                s.Append("debugRunningHintName=[");
                s.Append(log_Method.Fullname);
                s.Append("]");

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_NotCellData:
            s_Out = "";//ゴミ値
            bResult = false;
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー243！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("　指定の引数dataに、CellData型の値を指定してください。");
                s.Append(Environment.NewLine);
                s.Append("　別の型[" + data.GetType().Name + "でした。");
                s.Append(Environment.NewLine);

                s.Append(Environment.NewLine);
                s.Append("debugHintName=[");
                s.Append(sDebugConfigStack_Table);
                s.Append(".");
                s.Append(sDebugConfigStack_Field);
                s.Append("]");

                s.Append(Environment.NewLine);
                s.Append("debugRunningHintName=[");
                s.Append(log_Method.Fullname);
                s.Append("]");

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_AnotherType:
            s_Out = "";//ゴミ値
            bResult = false;
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー244！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("指定の引数の値[");
                s.Append(((Cell)data).Text);
                s.Append("]は、StringCellData型ではありませんでした。");

                s.Append(Environment.NewLine);
                s.Append("debugHintName=[");
                s.Append(sDebugConfigStack_Table);
                s.Append(".");
                s.Append(sDebugConfigStack_Field);
                s.Append("]");

                s.Append(Environment.NewLine);
                s.Append("debugRunningHintName=[");
                s.Append(log_Method.Fullname);
                s.Append("]");

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        //
        //
        gt_EndMethod:
            return bResult;
        }

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        public override bool Equals(System.Object obj)
        {
            // 引数がヌルの場合は、偽です。
            if (obj == null)
            {
                return false;
            }

            // 型が違えば偽です。
            StringCellImpl stringH = obj as StringCellImpl;
            if (null != stringH)
            {
                // 文字列の比較。
                return this.Text == stringH.Text;
            }

            string str = obj as string;
            if (null != str)
            {
                // 文字列の比較。
                return this.Text == str;
            }

            return false;
        }

        //────────────────────────────────────────

        static public bool IsSpaces(object data)
        {
            if (data is StringCellImpl)
            {
                return ((StringCellImpl)data).isSpaced;
            }

            throw new System.ArgumentException("指定の引数の値[" + ((Cell)data).Text + "]は、string型ではありませんでした。");
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 入力データそのままの形。
        /// </summary>
        public override string Text
        {
            set
            {
                if ("" == value.Trim())
                {
                    isSpaced = true;
                }
                else
                {
                    isSpaced = false;
                }

                // 常に真。
                isValidated = true;

                this.text = value;
            }
        }

        //────────────────────────────────────────

        public override int GetHashCode()
        {
            return this.Text.GetHashCode();
        }

        //────────────────────────────────────────
        #endregion



    }
}
