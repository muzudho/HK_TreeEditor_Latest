//
// Cushion
//
// アプリケーションを作るうえで、よく使うことになるもの。
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    /// <summary>
    /// ログ機能。
    /// </summary>
    public interface Log_Reports
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// ダミーの新規レポート。
        /// 
        /// このスレッドには追加されていないものを返します。
        /// </summary>
        /// <returns></returns>
        Log_RecordReports CreateDammyReport();

        /// <summary>
        /// メッセージを全て消去します。
        /// </summary>
        void Clear();

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 指定したログの全てのメッセージを、追加します。
        /// </summary>
        /// <param name="log_Reports"></param>
        void AddRange(Log_Reports log_Reports);

        /// <summary>
        /// 警告が出ていれば、そのテキスト。
        /// </summary>
        /// <returns></returns>
        string ToText(string sGroupTag);

        /// <summary>
        /// 警告が出ていれば、そのテキスト。
        /// </summary>
        /// <returns></returns>
        string ToText();

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        /// <summary>
        /// デバッグ処理中にデバッグ処理を行うと、無限ループになることがある。
        /// そこでデバッグ処理はこのフラグで囲い、デバッグ処理に入ったら偽にしておくことで、
        /// 子プログラムでデバッグ処理を行わないようにする。これで無限ループを防止する。
        /// </summary>
        bool CanCreateReport
        {
            get;
        }

        /// <summary>
        /// ストップウォッチを使用する設定になっていれば真。
        /// </summary>
        bool CanStopwatch
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



        #region 開始と終了
        //────────────────────────────────────────

        /// <summary>
        /// 新規レポート。
        /// 
        /// このスレッドに既に追加されたレポートを返します。
        /// </summary>
        /// <returns>新しいレポート。</returns>
        Log_RecordReports BeginCreateReport(EnumReport enumReport);

        /// <summary>
        /// レポート作成終了。
        /// </summary>
        void EndCreateReport();

        /// <summary>
        /// ログ取りの終了時。
        /// </summary>
        void EndLogging(Log_Method log_Method);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// このロガーが new されているメソッドを特定する情報。
        /// </summary>
        Log_Method Log_Method_CreationMe
        {
            get;
            //set;
        }

        /// <summary>
        /// このロガーを new したイベントの説明。
        /// </summary>
        string Comment_EventCreationMe
        {
            get;
            set;
        }

        //────────────────────────────────────────

        /// <summary>
        /// メッセージの件数。
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// レポートのリスト。
        /// </summary>
        List<Log_RecordReports> List_Record
        {
            get;
        }

        /// <summary>
        /// デバッグ用情報。実行経路ヒント。
        /// </summary>
        Log_Callstack Log_Callstack
        {
            get;
            set;
        }

        /// <summary>
        /// 問題が発生していなければ真。
        /// </summary>
        /// <returns></returns>
        bool Successful
        {
            get;
        }

        /// <summary>
        /// デバッグモード（実行時間計測）なら真。
        /// </summary>
        bool Debugmode_Stopwatch
        {
            set;
        }

        //────────────────────────────────────────
        #endregion



    }
}
