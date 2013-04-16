using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;//WarningReports

namespace Xenon.Table
{
    /// <summary>
    /// テーブルに付けたい名前や、ファイルパスの要求。
    /// 旧名：ORequest_TableReads
    /// </summary>
    public interface Request_ReadsTable
    {



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 読み込んだテーブルに付けたい名前。
        /// (string of name,put to table)
        /// </summary>
        string Name_PutToTable
        {
            get;
            set;
        }

        /// <summary>
        /// テーブル_ユニット名。使ってる？
        /// (table unit)
        /// </summary>
        string Tableunit
        {
            get;
            set;
        }

        /// <summary>
        /// 「T:～;」といった文字列が入る。
        /// (フィールド名：TYPE_DATA)
        /// </summary>
        string Typedata
        {
            get;
            set;
        }

        /// <summary>
        /// CSVファイル等へのパス。
        /// </summary>
        Expression_Node_Filepath Expression_Filepath
        {
            get;
            set;
        }

        /// <summary>
        /// 「日別バックアップ」するなら真。
        /// (date backup)
        /// </summary>
        bool IsDatebackupActivated
        {
            get;
            set;
        }

        /// <summary>
        /// 用途。／「」指定なし。／「WriteOnly」データの読取を行わない。ログ出力先を登録しているだけなど。
        /// </summary>
        string Use
        {
            get;
            set;
        }

        //────────────────────────────────────────
        #endregion



    }
}
