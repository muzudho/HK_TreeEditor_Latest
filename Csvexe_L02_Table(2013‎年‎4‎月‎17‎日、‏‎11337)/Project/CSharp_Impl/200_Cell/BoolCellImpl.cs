using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;//WarningReports


namespace Xenon.Table
{

    /// <summary>
    /// 旧名：Bool_HumaninputImpl
    /// </summary>
    public class BoolCellImpl : AbstractCellImpl
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="sourceHintName"></param>
        public BoolCellImpl(String sConfigStack)
            : base(sConfigStack)
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

        public bool TryGet(out bool bResult)
        {
            bool bSuccess;

            if (this.IsValidated)
            {
                bResult = this.bValue_Bool;
                bSuccess = true;
            }
            else
            {
                if (bool.TryParse(this.Text, out this.bValue_Bool))
                {
                    bResult = this.bValue_Bool;
                    bSuccess = true;
                }
                else
                {
                    bResult = false;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }

        //────────────────────────────────────────

        static public bool TryParse(
            object data,
            out bool bValue_Out,
            EnumOperationIfErrorvalue enumCellDataErrorSupport,
            object altValue,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, "XenonValue_BoolImpl", "TryParse", log_Reports);

            bool bResult;

            BoolCellImpl err_BoolCellData;
            if (data is Boolean)
            {
                bValue_Out = (bool)data;

                bResult = true;
            }
            else if (data is BoolCellImpl)
            {
                BoolCellImpl boolCellData = (BoolCellImpl)data;

                if (boolCellData.IsSpaces())
                {
                    // 空白の場合

                    if (EnumOperationIfErrorvalue.Spaces_To_Alt_Value == enumCellDataErrorSupport)
                    {
                        if (altValue is bool)
                        {
                            bValue_Out = (bool)altValue;

                            bResult = true;
                        }
                        else
                        {
                            // エラー
                            bValue_Out = false;//ゴミ値
                            bResult = false;
                            err_BoolCellData = boolCellData;
                            goto gt_Error_AnotherType;
                        }
                    }
                    else
                    {
                        // エラー
                        bValue_Out = false;//ゴミ値
                        bResult = false;
                        err_BoolCellData = boolCellData;
                        goto gt_Error_EmptyString;
                    }

                }
                else if (!boolCellData.isValidated)
                {
                    // エラー（変換に失敗した場合）
                    bValue_Out = false;//ゴミ値
                    bResult = false;
                    err_BoolCellData = boolCellData;
                    goto gt_Error_Invalid;
                }
                else
                {
                    bValue_Out = boolCellData.GetBool();

                    bResult = true;
                }
            }
            else if (null == data)
            {
                // エラー
                bValue_Out = false;//ゴミ値
                bResult = false;
                goto gt_Error_Null;
            }
            else if (!(data is Cell))
            {
                // エラー
                bValue_Out = false;//ゴミ値
                bResult = false;
                goto gt_Error_AnotherTypeData;
            }
            else
            {
                // エラー
                bValue_Out = false;//ゴミ値
                bResult = false;
                goto gt_Error_Class;
            }

            // 正常
            goto gt_EndMethod;
            //
            //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_AnotherType:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー543！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("　altValue引数には、bool型の値を指定してください。");
                s.Append(Environment.NewLine);
                s.Append("　　boolセル値=[");
                s.Append(err_BoolCellData.Text);
                s.Append("]");

                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);
                s.Append("　　問題箇所ヒント：");
                err_BoolCellData.ToText_Locationbreadcrumbs(s);

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_EmptyString:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー531！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("　セルに、bool型の値を入れてください。空欄にしないでください。");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);
                s.Append("　　boolセル値=[");
                s.Append(err_BoolCellData.Text);
                s.Append("]");

                //
                // ヒント
                err_BoolCellData.ToText_Locationbreadcrumbs(s);

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Invalid:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー112！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("　bool型に変換できませんでした。[");
                s.Append(err_BoolCellData.Text);
                s.Append("]");

                //
                // ヒント
                err_BoolCellData.ToText_Locationbreadcrumbs(s);

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Null:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー231！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("　指定の引数dataに、BoolCellData型の値を指定してください。空っぽでした。");

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_AnotherTypeData:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー332！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("　bool型のデータを入れるところで、");
                s.Append(Environment.NewLine);
                s.Append("　別の型[" + data.GetType().Name + "]でした。");

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Class:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー233！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("指定の引数の値[");
                s.Append(((Cell)data).Text);
                s.Append("]は、BoolCellData型ではありませんでした。");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

                s.Append("　型＝[");
                s.Append(data.GetType().Name);
                s.Append("]");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

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
            return bResult;
        }

        static public bool IsSpaces(object data)
        {
            if (data is BoolCellImpl)
            {
                return ((BoolCellImpl)data).isSpaced;
            }

            throw new System.ArgumentException("指定の引数の値[" + ((Cell)data).Text + "]は、bool型ではありませんでした。");
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
            BoolCellImpl obj2 = obj as BoolCellImpl;
            if (null != obj2)
            {
                // 空欄同士なら真です。
                if (this.IsSpaces() && obj2.IsSpaces())
                {
                    return true;
                }

                if (this.IsValidated && obj2.IsValidated)
                {
                    // お互いがブール値なら

                    return this.bValue_Bool == obj2.bValue_Bool;
                }
                else
                {
                    // どちらか片方でも非ブール値なら

                    return this.Text == obj2.Text;
                }
            }

            if (obj is bool)
            {
                bool bValue = (bool)obj;

                // このオブジェクトが空欄なら偽。
                if (this.IsSpaces())
                {
                    return false;
                }

                // このオブジェクトが非bool値なら偽。
                if (!this.IsValidated)
                {
                    return false;
                }

                // bool値で比較
                return this.bValue_Bool == bValue;
            }

            return false;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// bool型のデータ。
        /// </summary>
        private bool bValue_Bool;

        public bool GetBool()
        {
            if (!isValidated)
            {
                bool bSuccessful = this.IsValidated;
                if (!bSuccessful)
                {
                    // 変換に失敗した場合。
                    throw new System.InvalidOperationException("bool型に変換できませんでした。[" + this.Text + "]");
                }
            }
            return bValue_Bool;
        }

        public void SetBool(bool bValue)
        {
            this.bValue_Bool = bValue;
            this.Text = bValue.ToString();
        }

        //────────────────────────────────────────

        //static public string ParseString(object data)
        //{
        //    string sResult;

        //    if (data is Bool_HumaninputImpl)
        //    {
        //        sResult = ((Bool_HumaninputImpl)data).Text;
        //        goto gt_EndMethod;
        //    }

        //    //
        //    // エラー
        //    //
        //    Log_TextIndented t = new Log_TextIndentedImpl();
        //    t.Append("指定の引数の値[");
        //    t.Append(((Value_Humaninput)data).Text);
        //    t.Append("]は、bool型ではありませんでした。");
        //    t.Append(Environment.NewLine);

        //    throw new System.ArgumentException(t.ToString());

        //    //
        ////
        ////
        ////
        //gt_EndMethod:
        //    return sResult;
        //}

        //────────────────────────────────────────

        /// <summary>
        /// 入力データそのままの形。
        /// </summary>
        public override string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if ("" == value.Trim())
                {
                    isSpaced = true;
                    isValidated = true;
                }
                else
                {
                    isSpaced = false;

                    isValidated = bool.TryParse(value, out bValue_Bool);
                }

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
