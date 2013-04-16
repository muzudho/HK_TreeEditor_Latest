using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace Xenon.Syntax
{

    /// <summary>
    /// スレッド１つにつき、これを１つ宛てがうように使う。
    /// 
    /// プログラムの中でエラーがあれば、これに報告する。
    /// </summary>
    public class Log_ReportsImpl : Log_Reports
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Log_ReportsImpl(Log_Method log_Method_CreationMe)
        {
            this.bDebugEnable = true;

            this.bNotInfiniteLoop = true;
            this.list_Record = new List<Log_RecordReports>();

            this.log_Callstack = new Log_CallstackImpl();

            this.bSuccessful = true;
            this.log_Method_CreationMe = log_Method_CreationMe;
            this.comment_EventCreationMe = "";
        }

        public Log_RecordReports CreateDammyReport()
        {
            return new Log_RecordReportsImpl(this);
        }

        //────────────────────────────────────────

        /// <summary>
        /// エラーメッセージ、警告メッセージを全て消去します。
        /// </summary>
        public void Clear()
        {
            if (0 < this.list_Record.Count)
            {
                // メッセージがあったのなら。
                this.list_Record.Clear();
                this.bSuccessful = true;
            }
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 警告メッセージを追加します。
        /// </summary>
        /// <param name="warningReport"></param>
        private void Add(Log_RecordReports log_RecordReport)
        {
            this.list_Record.Add(log_RecordReport);

            if (log_RecordReport.EnumReport == EnumReport.Error)
            {
                this.bSuccessful = false;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 指定したログの全てのメッセージを、追加します。
        /// </summary>
        /// <param name="log_Reports"></param>
        public void AddRange(Log_Reports log_Reports)
        {
            this.list_Record.AddRange(log_Reports.List_Record);
            this.bSuccessful = (0 == this.list_Record.Count);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 警告が出ていれば、そのテキスト。
        /// </summary>
        /// <returns></returns>
        public string ToText(string sGroupTag)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            Log_Reports log_Reports_ThisMethod = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "ToText", log_Reports_ThisMethod);

            Log_TextIndented s = new Log_TextIndentedImpl();



            s.Append("ログ出力：");
            s.Newline();

            int nErrorCount = 0;

            
            List<Log_RecordReports> listCopy = new List<Log_RecordReports>(this.list_Record);
            foreach (Log_RecordReports log_RecordReport in listCopy)//todo:bug:ここで リストが変更されているときに例外を出してしまう。
            {
                // グループ・タグが指定されていれば、
                // グループ・タグが一致するメッセージだけを出力します。

                if (log_RecordReport.EnumReport == EnumReport.Error)
                {
                    if ("" == sGroupTag || sGroupTag == log_RecordReport.Tag_Group)
                    {
                        s.Append("(No.");
                        s.Append(nErrorCount);
                        s.Append(") ");

                        // タイトル
                        s.Append(log_RecordReport.ErrorSymbol);

                        if ("" != log_RecordReport.Tag_Group)
                        {
                            // グループ・タグ
                            s.Append(log_RecordReport.Tag_Group);
                        }

                        s.Newline();
                        s.Newline();

                        if ("" != log_RecordReport.Logstack)
                        {
                            s.Append("エラー発生元データの推測ヒント：");
                            s.Append(log_RecordReport.Logstack);
                            s.Newline();
                            s.Newline();
                        }

                        s.Append(log_RecordReport.GetMessage(this));
                        s.Newline();
                        s.Newline();

                        if ("" != log_RecordReport.Logstack)
                        {
                            s.Append("プログラム実行経路推測ヒント：");
                            s.Append(this.Log_Callstack.ToString());
                            s.Newline();
                            s.Newline();
                        }

                        s.Newline();
                        s.Newline();

                        // エラーが発生したメソッド。
                        s.Append(log_RecordReport.FullnameMethod);

                        s.Newline();
                    }

                    // カウンターは、読み飛ばしたエラーもきちんとカウント。
                    nErrorCount++;
                }
            }

            {
                s.Append(Log_RecordReportsImpl.ToText_Separator());
            }

            {
                if ("" != this.Comment_EventCreationMe)
                {
                    s.Append("ロガーの作成に関するコメント：");
                    s.Append(this.Comment_EventCreationMe);
                    s.Newline();
                }
            }

            {
                if (null != this.log_Method_CreationMe)
                {
                    s.Append("ロガー生成場所：");
                    s.Append(this.Log_Method_CreationMe.Fullname);
                    s.Newline();
                }
                else
                {
                    s.Append("ロガー生成場所：ヌル");
                    s.Newline();
                }
            }

            {
                s.Append("このエラーメッセージを作っているロガー：");
                s.Append(log_Method.Fullname);
                s.Newline();
            }

            if (!Log_ReportsImpl.BDebugmode_Static)
            {
                s.Newline();
                s.Newline();
                s.Append("このデバッグ情報は、DebugModeフラグが立っていない状態でのものです。");
                s.Newline();
                s.Append("DDebuggerImpl.DebugModeフラグを立てると、今より詳細な情報が出力されるかもしれません。");
                s.Newline();
            }


            log_Method.EndMethod(log_Reports_ThisMethod);
            log_Reports_ThisMethod.EndLogging(log_Method);
            return s.ToString();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 警告が出ていれば、そのテキスト。
        /// </summary>
        /// <returns></returns>
        public string ToText()
        {
            // タグ指定なし。
            return this.ToText("");
        }

        //────────────────────────────────────────
        #endregion



        #region 開始と終了
        //────────────────────────────────────────

        /// <summary>
        /// デバッグの開始。
        /// </summary>
        public void BeginDebug()
        {
            this.bDebugEnable = false;
        }

        /// <summary>
        /// デバッグの終了。
        /// </summary>
        public void EndDebug()
        {
            this.bDebugEnable = true;
        }

        /// <summary>
        /// このオブジェクトの生存の終了時。
        /// </summary>
        public void EndLogging(Log_Method log_Method)
        {
            if (!this.Successful)
            {
                // エラー
                goto gt_Error_NotSuccessful;
            }
            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NotSuccessful:
            //必ず実行。
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("▲エラー！（L01 EndLogging）");

                string strMessage = this.ToText();
                // \n を、実際に改行する命令に変換。
                strMessage = strMessage.Replace("\\n", System.Environment.NewLine);

                //一定数を超えた文字列は削ります。（ダイアログボックスが表示できなくなるので）
                if (1000<strMessage.Length)
                {
                    strMessage = strMessage.Substring(0, 999);
                }

                MessageBox.Show(strMessage, sb.ToString());
            }
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            ;
        }

        //────────────────────────────────────────

        /// <summary>
        /// デバッグ報告開始。
        /// </summary>
        /// <param name="d_EnumReport"></param>
        /// <returns>新しいレポート。</returns>
        public Log_RecordReports BeginCreateReport(EnumReport enumReport)
        {
            this.bNotInfiniteLoop = false;

            Log_RecordReportsImpl r;
            r = new Log_RecordReportsImpl(this);

            r.EnumReport = enumReport;

            // ダミーレポートでない場合、レポートを記録します。
            if (EnumReport.Dammy != enumReport)
            {
                r.ToText_Pathbreadcrumbs(this.Log_Callstack);
                this.Add(r);
            }

            return r;
        }

        /// <summary>
        /// デバッグ報告終了。
        /// </summary>
        public void EndCreateReport()
        {
            this.bNotInfiniteLoop = true;
        }

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        /// <summary>
        /// デバッグ処理中にデバッグ処理を行うと、無限ループになることがある。
        /// そこでデバッグ処理はこのフラグで囲い、デバッグ処理に入ったら偽にしておくことで、
        /// 子プログラムでデバッグ処理を行わないようにする。これで無限ループを防止する。
        /// </summary>
        public bool CanCreateReport
        {
            get
            {
                return this.bNotInfiniteLoop;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// デバッグして良い場合なら真。
        /// </summary>
        public bool CanDebug
        {
            get
            {
                return bDebugEnable;
            }
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private Log_Method log_Method_CreationMe;

        /// <summary>
        /// このロガーが new されているメソッドを特定する情報。
        /// </summary>
        public Log_Method Log_Method_CreationMe
        {
            get
            {
                return this.log_Method_CreationMe;
            }
        }

        //────────────────────────────────────────

        private string comment_EventCreationMe;
        /// <summary>
        /// このロガーを new したイベントの説明。
        /// </summary>
        public string Comment_EventCreationMe
        {
            get
            {
                return this.comment_EventCreationMe;
            }
            set
            {
                this.comment_EventCreationMe = value;
            }
        }

        //────────────────────────────────────────

        private Log_Callstack log_Callstack;

        /// <summary>
        /// @Deprecated
        /// コールスタック。
        /// </summary>
        public Log_Callstack Log_Callstack
        {
            get
            {
                return log_Callstack;
            }
            set
            {
                log_Callstack = value;
            }
        }

        //────────────────────────────────────────

        private bool bNotInfiniteLoop;

        //────────────────────────────────────────

        private List<Log_RecordReports> list_Record;

        /// <summary>
        /// 警告メッセージの一覧。
        /// </summary>
        public List<Log_RecordReports> List_Record
        {
            get
            {
                return this.list_Record;
            }
        }

        //────────────────────────────────────────

        private bool bSuccessful;

        /// <summary>
        /// プログラムを停止させるべき問題が発生していなければ真。（エラーメッセージが 0 件なら真）
        /// </summary>
        public bool Successful
        {
            get
            {
                return bSuccessful;
            }
        }

        //────────────────────────────────────────

        private static bool bDebugmode_Static;

        /// <summary>
        ///
        /// </summary>
        public static bool BDebugmode_Static
        {
            get
            {
                return bDebugmode_Static;
            }
            set
            {
                bDebugmode_Static = value;
            }
        }

        //────────────────────────────────────────

        private bool bDebugEnable;

        // ──────────────────────────────

        private static bool bDebugmode_Form;

        /// <summary>
        /// デバッグモードのON/OFF。画面レイアウト用。
        /// </summary>
        public static bool BDebugmode_Form
        {
            get
            {
                return bDebugmode_Form;
            }
            set
            {
                bDebugmode_Form = value;
            }
        }

        // ──────────────────────────────

        static private bool bDebugmode_Stopwatch_Static;

        public bool CanStopwatch
        {
            get
            {
                return Log_ReportsImpl.BDebugmode_Static && bDebugmode_Stopwatch_Static;
            }
        }

        /// <summary>
        /// デバッグモード（実行時間計測）なら真。
        /// </summary>
        public bool Debugmode_Stopwatch
        {
            set
            {
                bDebugmode_Stopwatch_Static = value;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 警告の件数を返します。
        /// </summary>
        public int Count
        {
            get
            {
                return this.list_Record.Count;
            }
        }

        //────────────────────────────────────────
        #endregion


        
    }
}
