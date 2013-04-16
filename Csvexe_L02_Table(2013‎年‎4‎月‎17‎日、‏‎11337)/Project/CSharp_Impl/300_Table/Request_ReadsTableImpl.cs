using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;//WarningReports, HumanInputFilePath

namespace Xenon.Table
{


    /// <summary>
    /// 
    /// </summary>
    public class Request_ReadsTableImpl : Request_ReadsTable
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Request_ReadsTableImpl()
        {
            this.name_PutToTable = "";
            this.use = "";

            {
                Configurationtree_Node s_ParentNode = new Configurationtree_NodeImpl("!ハードコーディング_Request_TableReadsImpl#<init>", null);
                Configurationtree_NodeFilepath s_fpath = new Configurationtree_NodeFilepathImpl("ファイルパス出典未指定L02_1", s_ParentNode);
                this.expression_Filepath = new Expression_Node_FilepathImpl(s_fpath);
            }
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private string name_PutToTable;

        /// <summary>
        /// テーブルに付けたい名前。
        /// </summary>
        public string Name_PutToTable
        {
            get
            {
                return name_PutToTable;
            }
            set
            {
                name_PutToTable = value;
            }
        }

        //────────────────────────────────────────

        private string tableunit;

        /// <summary>
        /// テーブル_ユニット名。
        /// </summary>
        public string Tableunit
        {
            get
            {
                return tableunit;
            }
            set
            {
                tableunit = value;
            }
        }

        //────────────────────────────────────────

        private string typedata;

        /// <summary>
        /// 「TYPE_DATA」フィールド値。
        /// 「T:～;」
        /// </summary>
        public string Typedata
        {
            get
            {
                return typedata;
            }
            set
            {
                typedata = value;
            }
        }

        //────────────────────────────────────────

        private Expression_Node_Filepath expression_Filepath;

        /// <summary>
        /// CSVファイル等へのパス。
        /// </summary>
        public Expression_Node_Filepath Expression_Filepath
        {
            get
            {
                return expression_Filepath;
            }
            set
            {
                expression_Filepath = value;
            }
        }

        //────────────────────────────────────────

        private bool isDatebackupActivated;

        /// <summary>
        /// 「日別バックアップ」するなら真。
        /// </summary>
        public bool IsDatebackupActivated
        {
            get
            {
                return isDatebackupActivated;
            }
            set
            {
                isDatebackupActivated = value;
            }
        }

        //────────────────────────────────────────

        private string use;

        /// <summary>
        /// 用途。／「」指定なし。／「WriteOnly」データの読取を行わない。ログ出力先を登録しているだけなど。
        /// </summary>
        public string Use
        {
            get
            {
                return use;
            }
            set
            {
                use = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
