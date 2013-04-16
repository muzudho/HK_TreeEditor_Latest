using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Table
{

    /// <summary>
    /// テーブルのフィールドを指定するものです。
    /// </summary>
    public class Fieldkey
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public Fieldkey()
        {
            this.name = "";
            this.name_Type = "";
            this.description = "";
        }

        //────────────────────────────────────────

        public Fieldkey(string name, string name_Type, string description)
        {
            this.name = name;
            this.name_Type = name_Type;
            this.description = description;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private string name;

        /// <summary>
        /// フィールド名。「NAME」など。
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        //────────────────────────────────────────

        private string name_Type;

        /// <summary>
        /// フィールド値の型。「int」など。
        /// </summary>
        public string Name_Type
        {
            get
            {
                return name_Type;
            }
            set
            {
                name_Type = value;
            }
        }

        //────────────────────────────────────────

        private string description;

        /// <summary>
        /// フィールドの説明。
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
