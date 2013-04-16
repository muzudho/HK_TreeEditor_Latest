using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    public class Log_CallstackRecordImpl : Log_RecordCallstack
    {



        #region アクション
        //────────────────────────────────────────

        public string ToText_Pathbreadcrumbs()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.Log_Method.Fullname);

            if ("" != this.Comment_Statement)
            {
                sb.Append(" ");
                sb.Append(this.Comment_Statement);
            }

            sb.Append(" > ");

            return sb.ToString();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private Log_Method log_Method;

        public Log_Method Log_Method
        {
            get
            {
                return log_Method;
            }
            set
            {
                log_Method = value;
            }
        }

        //────────────────────────────────────────

        private string comment_Statement;

        public string Comment_Statement
        {
            get
            {
                return comment_Statement;
            }
            set
            {
                comment_Statement = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
