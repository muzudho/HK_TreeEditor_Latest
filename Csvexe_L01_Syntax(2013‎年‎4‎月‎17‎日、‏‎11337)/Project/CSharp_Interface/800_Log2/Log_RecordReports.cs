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
    /// 警告データ
    /// </summary>
    public interface Log_RecordReports
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 警告タイトル。
        /// </summary>
        void SetTitle(string sErrorNumber, Log_Method log_Method);

        /// <summary>
        /// 警告メッセージの定型部分を簡単に作成します。
        /// </summary>
        /// <returns></returns>
        string Message_Configuration(
            Configuration_Node cur_Conf
            );

        /// <summary>
        /// 警告メッセージの定型部分を簡単に作成します。
        /// </summary>
        /// <returns></returns>
        string Message_SException(
            Exception ex
            );

        /// <summary>
        /// 警告メッセージの定型部分を簡単に作成します。
        /// </summary>
        /// <returns></returns>
        string Message_SSeparator();

        /// <summary>
        /// 「%1%」形式で文章中で使える文字列。
        /// </summary>
        /// <param name="number">「%1%」で使う数字。1から始まる連番。</param>
        /// <param name="sMessage">「%1%」に対応する文字列。</param>
        void SetParameter_P1p(int nNumber, object sMessage, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// プログラムを停止させるか、続行させるかの区別。
        /// </summary>
        EnumReport EnumReport
        {
            get;
            set;
        }

        /// <summary>
        /// 警告メッセージのテンプレート。
        /// </summary>
        string Message
        {
            set;
        }

        /// <summary>
        /// 警告メッセージ
        /// </summary>
        string GetMessage(Log_Reports d_Logging_orNull);

        /// <summary>
        /// エラー記号。
        /// </summary>
        string ErrorSymbol
        {
            get;
        }

        /// <summary>
        /// 問題が発生したメソッド。
        /// </summary>
        string FullnameMethod
        {
            get;
        }

        /// <summary>
        /// 人間オペレーターが修正するべき箇所を特定する情報など。
        /// </summary>
        string Logstack
        {
            set;
            get;
        }

        /// <summary>
        /// グループ・タグ。情報を見たい人が、表示する情報を絞り込むために使われます。
        /// </summary>
        string Tag_Group
        {
            set;
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
