using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{



    /// <summary>
    /// メソッドの場所（ライブラリ＋クラス名＋メソッド名）を、デバッグ表示するための情報。
    /// </summary>
    public interface Log_Method
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// コンソールに文字出力。
        /// </summary>
        /// <param name="sMessage"></param>
        void WriteDebug_ToConsole(string sMessage);

        /// <summary>
        /// コンソールに文字出力。「エラー」と付記される。
        /// </summary>
        /// <param name="sMessage"></param>
        void WriteError_ToConsole(string sMessage);

        /// <summary>
        /// コンソールに文字出力。「警告」と付記される。
        /// </summary>
        /// <param name="sMessage"></param>
        void WriteWarning_ToConsole(string sMessage);

        /// <summary>
        /// コンソールに文字出力。「情報」と付記される。
        /// </summary>
        /// <param name="sMessage"></param>
        void WriteInfo_ToConsole(string sMessage);

        //────────────────────────────────────────
        #endregion



        #region 開始と終了
        //────────────────────────────────────────

        /// <summary>
        /// メソッド本文が始まる前に置く。
        /// </summary>
        /// <param name="log_Reports"></param>
        void BeginMethod(string sName_Library, object thisClass, string sName_Method, Log_Reports log_Reports);

        void BeginMethod(string sName_Library, string sName_StaticClass, string sName_Method, Log_Reports log_Reports);

        /// <summary>
        /// メソッド本文が始まる前に置く。
        /// </summary>
        /// <param name="log_Reports"></param>
        void BeginMethod(string sName_Library, object thisClass, string sName_Method, out Log_Reports log_Reports);

        void BeginMethod(string sName_Library, string sName_StaticClass, string sName_Method, out Log_Reports log_Reports);

        /// <summary>
        /// メソッドを抜ける前に置く。
        /// </summary>
        /// <param name="log_Reports"></param>
        void EndMethod(Log_Reports log_Reports);
        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        bool Equals(Log_Method log_Method);

        /// <summary>
        /// 「エラー」表示を作成するレベル設定なら真。「エラー」は、深刻さが１番めのメッセージ。
        /// </summary>
        /// <returns></returns>
        bool CanError();

        /// <summary>
        /// 「警告」表示を作成するレベル設定なら真。「警告」は、深刻さが２番めのメッセージ。
        /// </summary>
        /// <returns></returns>
        bool CanWarning();

        /// <summary>
        /// 「デバッグ」表示を作成するレベル設定なら真。「デバッグ」は、深刻さが３番めのメッセージ。
        /// </summary>
        /// <returns></returns>
        bool CanDebug(int nCodeblockDebugLevel);

        /// <summary>
        /// 「情報」表示を作成するレベル設定なら真。「情報」は、深刻さが４番めのメッセージ。
        /// </summary>
        /// <returns></returns>
        bool CanInfo();

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// メソッド内での、デバッグ出力をする閾値。
        /// </summary>
        int Level_DebugMethod
        {
            get;
            set;
        }

        /// <summary>
        /// ストップウォッチ。処理時間を計測するのに使う。
        /// </summary>
        Log_Stopwatch Log_Stopwatch
        {
            get;
            set;
        }

        /// <summary>
        /// メソッドの場所表示。「ライブラリ＋クラス名＋メソッド名」
        /// </summary>
        string Fullname
        {
            get;
        }

        /// <summary>
        /// ライブラリ名。
        /// </summary>
        string Name_Library
        {
            get;
        }

        /// <summary>
        /// クラス名。
        /// </summary>
        string Name_Class
        {
            get;
        }

        /// <summary>
        /// staticメソッドなら真。
        /// </summary>
        bool IsStatic
        {
            get;
        }

        /// <summary>
        /// メソッド名。
        /// </summary>
        string Name_Method
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
