using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    public class Log_MethodImpl : Log_Method
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public Log_MethodImpl()
        {
            this.level_DebugMethod = 0;
            this.mode_DebugMaster = false;
            this.name_Library = "";
            this.name_Class = "";
            this.name_Method = "";
            this.log_Stopwatch = new Log_StopwatchImpl(this);
        }

        public Log_MethodImpl(int nDebugLevel_Method)
        {
            this.level_DebugMethod = nDebugLevel_Method;
            this.mode_DebugMaster = true;
            this.name_Library = "";
            this.name_Class = "";
            this.name_Method = "";
            this.log_Stopwatch = new Log_StopwatchImpl(this);
        }

        public Log_MethodImpl(int nLevel_MethodDebug, bool bDebugMode_Master)
        {
            this.level_DebugMethod = nLevel_MethodDebug;
            this.mode_DebugMaster = bDebugMode_Master;
            this.name_Library = "";
            this.name_Class = "";
            this.name_Method = "";
            this.log_Stopwatch = new Log_StopwatchImpl(this);
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        [Obsolete("ライブラリ名、クラス名、メソッド名も同時にセットするBeginMethodを使うこと。")]
        public void SetPath(string sName_Library, object thisClass, string sName_Method)
        {
            this.name_Library = sName_Library;
            this.name_Class = thisClass.GetType().Name;
            this.isStatic = false;
            this.name_Method = sName_Method;
        }

        [Obsolete("ライブラリ名、クラス名、メソッド名も同時にセットするBeginMethodを使うこと。")]
        public void SetPath(string sName_Library, string sName_StaticClass, string sName_Method)
        {
            this.name_Library = sName_Library;
            this.name_Class = sName_StaticClass;
            this.isStatic = true;
            this.name_Method = sName_Method;
        }

        //────────────────────────────────────────

        public void WriteDebug_ToConsole(string sMessage)
        {
            // #出力
            System.Console.WriteLine(this.Fullname + ":　" + sMessage);
        }

        public void WriteError_ToConsole(string sMessage)
        {
            // #出力
            System.Console.WriteLine(this.Fullname + ":エラー　" + sMessage);
        }

        public void WriteWarning_ToConsole(string sMessage)
        {
            // #出力
            System.Console.WriteLine(this.Fullname + ":警告　" + sMessage);
        }

        public void WriteInfo_ToConsole(string sMessage)
        {
            // #出力
            System.Console.WriteLine(this.Fullname + ":情報　" + sMessage);
        }

        //────────────────────────────────────────
        #endregion



        #region 開始と終了
        //────────────────────────────────────────

        [Obsolete("ライブラリ名、クラス名、メソッド名も同時にセットするBeginMethodを使うこと。")]
        public void BeginMethod(Log_Reports log_Reports)
        {
            // デバッグを行うなら、コールスタックにこのメソッドパスを追加。
            if (Log_ReportsImpl.BDebugmode_Static)
            {
                log_Reports.Log_Callstack.Push(this);
            }

            this.IsActive = true;
        }

        public void BeginMethod(string sName_Library, object thisClass, string sName_Method, Log_Reports log_Reports)
        {
            this.name_Library = sName_Library;
            this.name_Class = thisClass.GetType().Name;
            this.isStatic = false;
            this.name_Method = sName_Method;

            // デバッグを行うなら、コールスタックにこのメソッドパスを追加。
            if (Log_ReportsImpl.BDebugmode_Static)
            {
                log_Reports.Log_Callstack.Push(this);
            }

            this.IsActive = true;
        }

        public void BeginMethod(string sName_Library, string sName_StaticClass, string sName_Method, Log_Reports log_Reports)
        {
            this.name_Library = sName_Library;
            this.name_Class = sName_StaticClass;
            this.isStatic = true;
            this.name_Method = sName_Method;

            // デバッグを行うなら、コールスタックにこのメソッドパスを追加。
            if (Log_ReportsImpl.BDebugmode_Static)
            {
                log_Reports.Log_Callstack.Push(this);
            }

            this.IsActive = true;
        }

        [Obsolete("ライブラリ名、クラス名、メソッド名も同時にセットするBeginMethodを使うこと。")]
        public void BeginMethod(out Log_Reports log_Reports)
        {
            log_Reports = new Log_ReportsImpl(this);

            // デバッグを行うなら、コールスタックにこのメソッドパスを追加。
            if (Log_ReportsImpl.BDebugmode_Static)
            {
                log_Reports.Log_Callstack.Push(this);
            }

            this.IsActive = true;
        }

        public void BeginMethod(string sName_Library, object thisClass, string sName_Method, out Log_Reports log_Reports)
        {
            this.name_Library = sName_Library;
            this.name_Class = thisClass.GetType().Name;
            this.isStatic = false;
            this.name_Method = sName_Method;

            log_Reports = new Log_ReportsImpl(this);

            // デバッグを行うなら、コールスタックにこのメソッドパスを追加。
            if (Log_ReportsImpl.BDebugmode_Static)
            {
                log_Reports.Log_Callstack.Push(this);
            }

            this.IsActive = true;
        }

        public void BeginMethod(string sName_Library, string sName_StaticClass, string sName_Method, out Log_Reports log_Reports)
        {
            this.name_Library = sName_Library;
            this.name_Class = sName_StaticClass;
            this.isStatic = true;
            this.name_Method = sName_Method;

            log_Reports = new Log_ReportsImpl(this);

            // デバッグを行うなら、コールスタックにこのメソッドパスを追加。
            if (Log_ReportsImpl.BDebugmode_Static)
            {
                log_Reports.Log_Callstack.Push(this);
            }

            this.IsActive = true;
        }

        public void EndMethod(Log_Reports log_Reports)
        {
            if (this.IsActive)
            {
                if (Log_ReportsImpl.BDebugmode_Static)
                {
                    if (this.Log_Stopwatch.IsRunning && log_Reports.CanStopwatch)
                    {
                        this.Log_Stopwatch.End(log_Reports);
                    }

                    log_Reports.Log_Callstack.Pop(this);
                }

                this.IsActive = false;
            }
            else
            {
                //エラー
                goto gt_Error_NoActiveYet;
            }


            goto gt_EndMethod;
        //
            #region 異常系
            //────────────────────────────────────────
        gt_Error_NoActiveYet:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー31！", this);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("BeginMethodしていないのにEndMethodしました。対応の数は合っていますか？");
                s.Newline();

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
            //────────────────────────────────────────
            #endregion
            //
        gt_EndMethod:
            ;
        }

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        public bool Equals(Log_Method log_Method)
        {
            bool bEquals;

            if(
                log_Method.Name_Library == this.Name_Library &&
                log_Method.Name_Class == this.Name_Class &&
                log_Method.IsStatic == this.IsStatic &&
                log_Method.Name_Method == this.Name_Method
                )
            {
                bEquals = true;
            }
            else
            {
                bEquals = false;
            }

            return bEquals;
        }

        //────────────────────────────────────────

        public bool CanError()
        {
            bool bErrorMode = true;

            return bErrorMode;
        }

        public bool CanWarning()
        {
            bool bWarningMode = true;

            return bWarningMode;
        }

        public bool CanDebug(int nDebugLevel_Codeblock)
        {
            bool bDebugMode;

            if (this.mode_DebugMaster && nDebugLevel_Codeblock <= this.level_DebugMethod)
            {
                bDebugMode = true;
            }
            else
            {
                bDebugMode = false;
            }

            return bDebugMode;
        }

        public bool CanInfo()
        {
            bool bInfoMode;

            if (this.mode_DebugMaster)
            {
                bInfoMode = true;
            }
            else
            {
                bInfoMode = false;
            }

            return bInfoMode;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private bool isActive;

        /// <summary>
        /// 最初、偽。BeginMethodすると真。EndMethodすると偽。
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive =value;
            }
        }

        //────────────────────────────────────────

        public string Fullname
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(this.Name_Library);
                sb.Append(":");
                sb.Append(this.Name_Class);
                if (this.IsStatic)
                {
                    sb.Append(".");
                }
                else
                {
                    sb.Append("#");
                }
                sb.Append(this.Name_Method);

                return sb.ToString();
            }
        }

        //────────────────────────────────────────

        private int level_DebugMethod;

        /// <summary>
        /// メソッド内での、デバッグ出力をする閾値。
        /// </summary>
        public int Level_DebugMethod
        {
            get
            {
                return this.level_DebugMethod;
            }
            set
            {
                this.level_DebugMethod = value;
            }
        }

        //────────────────────────────────────────

        private bool mode_DebugMaster;

        //────────────────────────────────────────

        private Log_Stopwatch log_Stopwatch;

        /// <summary>
        /// ストップウォッチ。
        /// </summary>
        public Log_Stopwatch Log_Stopwatch
        {
            get
            {
                return log_Stopwatch;
            }
            set
            {
                log_Stopwatch = value;
            }
        }

        //────────────────────────────────────────
        
        private string name_Library;

        public string Name_Library
        {
            get
            {
                return name_Library;
            }
        }

        //────────────────────────────────────────

        private string name_Class;

        public string Name_Class
        {
            get
            {
                return name_Class;
            }
        }

        //────────────────────────────────────────

        private bool isStatic;

        public bool IsStatic
        {
            get
            {
                return isStatic;
            }
        }

        //────────────────────────────────────────

        private string name_Method;

        public string Name_Method
        {
            get
            {
                return name_Method;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
