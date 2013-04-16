using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Xenon.Syntax;

namespace Xenon.Table
{
    public class Record_HumaninputImpl : Configuration_NodeImpl, Record_Humaninput
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public Record_HumaninputImpl(string config, DataRow dataRow, Configuration_Node parent_ConfigurationNode)
            : base(config, parent_ConfigurationNode)
        {
            this.dataRow = dataRow;
            //this.configuration_Node = configuration_Node;
        }

        public Record_HumaninputImpl(string config, Table_Humaninput owner_TableH)
            : base(config, owner_TableH)
        {
            this.dataRow = owner_TableH.DataTable.NewRow();
            //this.configuration_Node = owner_TableH;
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────
        //
        // テーブル改造
        //

        /// <summary>
        /// 指定のフィールドから左を、全て右に１列分ずらします。一番右の列は無くなります。
        /// 開いた列には null が入ります。
        /// </summary>
        /// <param name="columnIndex"></param>
        public void Insert(int columnIndex, Cell valueH, Log_Reports log_Reports)
        {
            object[] itemArray = this.DataRow.ItemArray;//ItemArrayを取得する処理は重い。
            int length = itemArray.Length;
            int index_Destination = length - 1;

            //右から順に左へ。
            for (int index_Source = index_Destination - 1; columnIndex <= index_Source; index_Source-- )
            {
                //上書きコピー
                itemArray[index_Source + 1] = itemArray[index_Source];

                //todo:型チェンジは？　値を移動したら型も一緒に移動している。
            }

            //挿入するはずの列は空に。
            itemArray[columnIndex] = valueH;
        }

        //────────────────────────────────────────
        //
        //
        //

        public void ForEach(DELEGATE_Fields delegate_Fields, Log_Reports log_Reports)
        {
            bool isBreak = false;

            foreach (object valueField in this.DataRow.ItemArray)
            {
                delegate_Fields((Cell)valueField, ref isBreak, log_Reports);

                if (isBreak)
                {
                    break;
                }
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 配列の要素を取得します。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Cell ValueAt(int index)
        {
            return (Cell)this.DataRow[index];
        }

        /// <summary>
        /// 配列の要素を取得します。
        /// </summary>
        /// <param name="name_Field"></param>
        /// <returns></returns>
        public Cell ValueAt(string name_Field)
        {
            return (Cell)this.DataRow[name_Field];
        }

        /// <summary>
        /// 配列の要素をテキストとして取得します。
        /// </summary>
        /// <param name="name_Field"></param>
        /// <returns></returns>
        public string TextAt(string name_Field)
        {
            string text;

            if (!this.DataRow.Table.Columns.Contains(name_Field))
            {
                text = "";
            }
            else if (this.DataRow[name_Field] is Cell)
            {
                Cell valueH = (Cell)this.DataRow[name_Field];
                if (null != valueH)
                {
                    text = valueH.Text;
                }
                else
                {
                    text = "";
                }
            }
            else
            {
                // DBNull クラスだった場合など。
                text = "";
            }

            return text;
        }

        /// <summary>
        /// 配列の要素を取得します。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void SetValueAt(int index, Cell valueH, Log_Reports log_Reports)
        {
            this.DataRow[index] = valueH;
        }

        /// <summary>
        /// 配列の要素を取得します。
        /// </summary>
        /// <param name="name_Field"></param>
        /// <returns></returns>
        public void SetValueAt(string name_Field, Cell valueH, Log_Reports log_Reports)
        {
            this.DataRow[name_Field] = valueH;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <returns>該当がなければ -1。</returns>
        public int ColumnIndexOf_Trimupper(string expected)
        {
            int result = -1;

            int cur_IndexColumn = 0;
            foreach(object obj in this.DataRow.ItemArray)
            {
                Cell valueH = (Cell)obj;

                if (expected == valueH.Text.Trim().ToUpper())
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

        /// <summary>
        /// デバッグ用に内容をダンプします。
        /// </summary>
        /// <returns></returns>
        public string ToString_DebugDump()
        {
            StringBuilder s = new StringBuilder();

            int cur_IndexColumn = 0;
            foreach (object obj in this.DataRow.ItemArray)
            {
                Cell valueH = (Cell)obj;

                if ("" != valueH.Text)
                {
                    s.Append("[");
                    s.Append(cur_IndexColumn);
                    s.Append("]");
                    s.Append(valueH.Text);
                }

                cur_IndexColumn++;
            }

            return s.ToString();
        }

        //────────────────────────────────────────

        public override void ToText_Content(Log_TextIndented s)
        {
            s.Increment();

            int cur_IndexColumn = 0;
            foreach (object obj in this.DataRow.ItemArray)
            {
                Cell valueH = (Cell)obj;

                if ("" != valueH.Text)
                {
                    s.Append("[");
                    s.Append(cur_IndexColumn);
                    s.Append("]");
                    s.Append(valueH.Text);
                }

                cur_IndexColumn++;
            }

            s.Decrement();
        }

        //────────────────────────────────────────

        public void AddTo(Table_Humaninput tableH)
        {
            tableH.DataTable.Rows.Add(this.DataRow);
        }

        public void RemoveFrom(Table_Humaninput tableH)
        {
            tableH.DataTable.Rows.Remove(this.DataRow);
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        ////────────────────────────────────────────

        //private Configuration_Node configuration_Node;

        ///// <summary>
        ///// テーブルのコンフィグ記述場所情報。
        ///// </summary>
        //public Configuration_Node Configuration_Node
        //{
        //    get
        //    {
        //        return this.configuration_Node;
        //    }
        //    set
        //    {
        //        this.configuration_Node = value;
        //    }
        //}

        //────────────────────────────────────────

        private DataRow dataRow;

        private DataRow DataRow
        {
            get
            {
                return this.dataRow;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
