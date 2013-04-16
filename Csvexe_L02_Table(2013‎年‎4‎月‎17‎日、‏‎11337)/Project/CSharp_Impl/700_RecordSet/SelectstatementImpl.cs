using System;
using System.Collections.Generic;
using System.Data;//DataRow
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{

    /// <summary>
    /// セレクト文。
    /// </summary>
    public class SelectstatementImpl : Selectstatement
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public SelectstatementImpl(Expression_Node_String parent_Expr, Configuration_Node parent_Conf)
        {
            this.parent = parent_Conf;
            this.List_SName_SelectField = new List<string>();
            this.Expression_From = new Expression_Node_StringImpl(parent_Expr, parent_Conf);
            this.Expression_Into = new Expression_Node_StringImpl(parent_Expr, parent_Conf);
            this.Expression_Where_RecordSetLoadFrom = new Expression_Node_StringImpl(parent_Expr, parent_Conf);
            this.EnumWherelogic = EnumLogic.And;//ｗｈｅｒｅのlogic属性のデフォルト。
            this.list_Recordcondition = new List<Recordcondition>();
            this.Required = "";
            this.Storage = "";
            this.Description = "";
        }

        //────────────────────────────────────────
        #endregion

        

        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// ｓｅｌｅｃｔ属性。「id,name」といったカンマ区切り文字列。
        /// </summary>
        /// <returns></returns>
        public string ToSelectFieldNameListCsv()
        {
            StringBuilder sb = new StringBuilder();
            int nCount = this.list_SName_SelectField.Count();
            for (int nI = 0; nI < nCount; nI++)
            {
                if (0 != nI)
                {
                    sb.Append(",");
                }
                sb.Append(this.list_SName_SelectField[nI]);
            }

            return sb.ToString();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private Expression_Node_String expression_Where_RecordSetLoadFrom;

        /// <summary>
        /// 「E■ｗｈｅｒｅ」要素に指定されている「ｒｅｃｏｒｄ－ｓｅｔ－ｌｏａｄ－ｆｒｏｍ」属性。
        /// 
        /// 一時記憶から、レコードセットのロードをするか否か。
        /// </summary>
        public Expression_Node_String Expression_Where_RecordSetLoadFrom
        {
            get
            {
                return expression_Where_RecordSetLoadFrom;
            }
            set
            {
                expression_Where_RecordSetLoadFrom = value;
            }
        }

        //────────────────────────────────────────

        private List<string> list_SName_SelectField;

        /// <summary>
        /// ｓｅｌｅｃｔ属性。
        /// </summary>
        public List<string> List_SName_SelectField
        {
            get
            {
                return list_SName_SelectField;
            }
            set
            {
                list_SName_SelectField = value;
            }
        }

        //────────────────────────────────────────

        private Expression_Node_String expression_Into;

        /// <summary>
        /// ｉｎｔｏ属性。
        /// </summary>
        public Expression_Node_String Expression_Into
        {
            get
            {
                return expression_Into;
            }
            set
            {
                expression_Into = value;
            }
        }

        //────────────────────────────────────────

        private List<Recordcondition> list_Recordcondition;

        /// <summary>
        /// 条件。
        /// </summary>
        public List<Recordcondition> List_Recordcondition
        {
            get
            {
                return list_Recordcondition;
            }
            set
            {
                list_Recordcondition = value;
            }
        }

        //────────────────────────────────────────

        private EnumLogic enumWhereLogic;

        /// <summary>
        /// 「E■ｗｈｅｒｅ」要素に指定されている「ｌｏｇｉｃ」属性。
        /// </summary>
        public EnumLogic EnumWherelogic
        {
            get
            {
                return enumWhereLogic;
            }
            set
            {
                enumWhereLogic = value;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// required="" 属性。
        /// 
        /// SELECTの検索結果が 0 件でも良いか否か。
        /// </summary>
        private string sRequired;

        public string Required
        {
            get
            {
                return sRequired;
            }
            set
            {
                sRequired = value;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// ｆｒｏｍ="" 属性。
        /// </summary>
        private Expression_Node_String expression_From;

        public Expression_Node_String Expression_From
        {
            get
            {
                return expression_From;
            }
            set
            {
                expression_From = value;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// storage="" 属性。
        /// </summary>
        private string sStorage;

        public string Storage
        {
            get
            {
                return sStorage;
            }
            set
            {
                sStorage = value;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 属性。
        /// </summary>
        private string sDescription;

        public string Description
        {
            get
            {
                return sDescription;
            }
            set
            {
                sDescription = value;
            }
        }

        //────────────────────────────────────────

        private Configuration_Node parent;

        /// <summary>
        /// 問題箇所ヒント。
        /// </summary>
        public Configuration_Node Parent
        {
            get
            {
                return this.parent;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
