using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{
    public class RecordFielddefImpl : RecordFielddef
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public RecordFielddefImpl()
        {
            this.list_Fielddef = new List<Fielddef>();
        }

        public RecordFielddefImpl(List<Fielddef> list_Fielddef)
        {
            this.list_Fielddef = list_Fielddef;
        }

        //────────────────────────────────────────
        #endregion

        

        #region アクション
        //────────────────────────────────────────

        public void Add(Fielddef fielddefinition)
        {
            this.List_Fielddef.Add(fielddefinition);
        }

        public void Insert(int indexColumn, Fielddef fielddefinition)
        {
            this.List_Fielddef.Insert(indexColumn, fielddefinition);
        }

        public void ForEach(DELEGATE_Fielddefs delegate_Fielddefs, Log_Reports log_Reports)
        {
            bool isBreak = false;

            foreach (Fielddef fielddefinition in this.List_Fielddef)
            {
                delegate_Fielddefs(fielddefinition, ref isBreak, log_Reports);

                if (isBreak)
                {
                    break;
                }
            }
        }

        //────────────────────────────────────────

        public Fielddef ValueAt(int index)
        {
            return this.List_Fielddef[index];
        }

        //────────────────────────────────────────

        /// <summary>
        /// デバッグ用に内容をダンプします。
        /// </summary>
        /// <returns></returns>
        public string ToString_DebugDump()
        {
            StringBuilder s = new StringBuilder();

            int cur_IndexColumn = 0;
            foreach (Fielddef fielddefinition in this.List_Fielddef)
            {
                s.Append("[");
                s.Append(cur_IndexColumn);
                s.Append("](");
                s.Append(fielddefinition.Name_Humaninput);
                s.Append(":");
                s.Append(fielddefinition.ToString_Type());
                s.Append(")");

                cur_IndexColumn++;
            }

            return s.ToString();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 主に、データベースのフィールド名のindexを調べるのに利用されます。
        /// </summary>
        /// <param name="expected"></param>
        /// <returns>該当がなければ -1。</returns>
        public int ColumnIndexOf_Trimupper(string expected)
        {
            int result = -1;

            int cur_IndexColumn = 0;
            foreach (Fielddef fielddefinition in this.List_Fielddef)
            {
                if (expected == fielddefinition.Name_Trimupper)
                {
                    result = cur_IndexColumn;
                    break;
                }
                else
                {
                }

                cur_IndexColumn++;
            }

            return result;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        public int Count
        {
            get
            {
                return this.List_Fielddef.Count;
            }
        }

        //────────────────────────────────────────

        private List<Fielddef> list_Fielddef;

        /// <summary>
        /// フィールドの型定義。
        /// </summary>
        private List<Fielddef> List_Fielddef
        {
            get
            {
                return list_Fielddef;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
