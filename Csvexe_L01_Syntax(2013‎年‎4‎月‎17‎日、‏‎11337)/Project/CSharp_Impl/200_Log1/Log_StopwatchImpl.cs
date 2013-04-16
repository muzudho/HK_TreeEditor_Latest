using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;//Stopwatch


namespace Xenon.Syntax
{

    public class Log_StopwatchImpl : Log_Stopwatch
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Log_StopwatchImpl(Log_Method owner_Log_Method)
        {
            this.owner_Log_Method = owner_Log_Method;
            this.message = "";
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────


        public void Begin()
        {
            if (null == this.stopwatch)
            {
                this.stopwatch = new Stopwatch();
            }

            this.stopwatch.Start();
            this.MilliSeconds_Start = this.stopwatch.ElapsedMilliseconds;
        }

        public void End(Log_Reports log_reports)
        {
            if (this.IsRunning)
            {
                Log_Method log_Method = new Log_MethodImpl(0);
                log_Method.BeginMethod(Info_Syntax.Name_Library, this, "End", log_reports);

                this.MilliSeconds_End = stopwatch.ElapsedMilliseconds;


                StringBuilder sb = new StringBuilder();

                sb.Append(Info_Syntax.Name_Library);
                sb.Append(":");
                sb.Append(this.GetType().Name);
                sb.Append("#End: 計測 ");
                sb.Append(this.ToString());
                sb.Append(Environment.NewLine);

                log_Method.WriteInfo_ToConsole(sb.ToString());
                log_Method.EndMethod(log_reports);
            }
        }

        //────────────────────────────────────────

        public override string ToString()
        {
            long nMilliSeconds = this.MilliSeconds_End - this.MilliSeconds_Start;

            StringBuilder sb = new StringBuilder();

            if (this.IsRunning)
            {
                //
                // メソッド名
                //
                sb.Append("Stopwatch ");
                sb.Append(this.Owner_Log_Method.Fullname);
                sb.Append(":");

                sb.Append(this.Message);


                TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)nMilliSeconds);  // 値を TimeSpan 型へ変換する

                sb.Append(String.Format("　処理時間[{0}Days {1:D2}:{2:D2}.{3:D2}'{4:D3}]", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, nMilliSeconds%1000 ));
                sb.Append("　(");
                sb.Append(nMilliSeconds);
                sb.Append(")ミリ秒");

            }
            else
            {
                //
                // メソッド名
                //
                sb.Append("Stopwatch ");
                sb.Append(this.Owner_Log_Method.Fullname);
                sb.Append(":");

                sb.Append(this.Message);

                sb.Append(" 未起動。");
            }

            return sb.ToString();
        }

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        public bool IsRunning
        {
            get
            {
                bool bResult;
                if (null == this.stopwatch)
                {
                    bResult = false;
                }
                else
                {
                    bResult = this.stopwatch.IsRunning;
                }

                return bResult;
            }
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// C#で用意されているストップウォッチ。
        /// </summary>
        private Stopwatch stopwatch;

        //────────────────────────────────────────

        private Log_Method owner_Log_Method;

        private Log_Method Owner_Log_Method
        {
            get
            {
                return this.owner_Log_Method;
            }
            set
            {
                this.owner_Log_Method = value;
            }
        }

        //────────────────────────────────────────

        private string message;

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }

        //────────────────────────────────────────

        private long milliSeconds_Start;

        public long MilliSeconds_Start
        {
            get
            {
                return milliSeconds_Start;
            }
            set
            {
                milliSeconds_Start = value;
            }
        }

        //────────────────────────────────────────

        private long milliSeconds_End;

        public long MilliSeconds_End
        {
            get
            {
                return milliSeconds_End;
            }
            set
            {
                milliSeconds_End = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }

}
