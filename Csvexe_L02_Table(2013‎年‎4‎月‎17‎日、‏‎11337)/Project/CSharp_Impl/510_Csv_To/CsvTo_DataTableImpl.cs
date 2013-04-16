using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;//DataTable


namespace Xenon.Table
{
    ///
    /// CSVファイル関係ライブラリ。
    ///
    /// 備考：CSVの先頭データが「ID」の場合、ExcelだとSYLKファイルと認識されて開けません。
    ///
    public class CsvTo_DataTableImpl
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public CsvTo_DataTableImpl()
        {
            this.charSeparator = ',';
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// DataTableを作成します。
        /// 
        /// セルのデータ型は全て string です。
        /// </summary>
        /// <param name="csvText"></param>
        /// <returns></returns>
        public DataTable Read(
            string string_Csv
            )
        {
            // テーブルを作成します。
            DataTable dataTable = new DataTable();

            System.IO.StringReader reader = new System.IO.StringReader(string_Csv);

            //
            // CSVを解析して、テーブル形式で格納。
            //

            int index_Row = 0;
            string[] array_Field;
            DataRow datarow;
            CsvLineParserImpl csvParser = new CsvLineParserImpl();

            if (-1 < reader.Peek())
            {
                // 1行ずつ読み取ります。

                //
                // 0 行目の読取。　列名データが入っている行です。
                //

                // 読み取った返却値を、変数に入れ直さずにスプリット。
                array_Field = csvParser.UnescapeLineToFieldList(reader.ReadLine(), this.CharSeparator).ToArray();

                // 行を作成します。
                datarow = dataTable.NewRow();

                int indexColumn = 0;
                while (indexColumn < array_Field.Length)
                {
                    // 列情報を追加します。 型は文字列型とします。
                    dataTable.Columns.Add(array_Field[indexColumn], typeof(string));

                    // データとしても早速格納します。
                    datarow[indexColumn] = array_Field[indexColumn];

                    indexColumn++;
                }

                dataTable.Rows.Add(datarow);
                index_Row++;



                //
                // 1行目以降の読取。
                //
                while (-1 < reader.Peek())
                {
                    // 1行ずつ読み取ります。

                    // 読み取った返却値を、変数に入れ直さずにスプリット。
                    array_Field = reader.ReadLine().Split(this.CharSeparator);

                    // 行を作成します。
                    datarow = dataTable.NewRow();

                    //
                    // 追加する列数
                    //
                    object[] itemArray = datarow.ItemArray;//ItemArrayは1回の呼び出しが重い。
                    int count_AddsColumns = array_Field.Length - itemArray.Length;
                    for (int count = 0; count < count_AddsColumns; count++)
                    {
                        // 0行目で数えた列数より多い場合。

                        // 列を追加します。
                        // 列定義を追加しています。型は文字列型、名前は空文字列です。
                        dataTable.Columns.Add("", typeof(string));
                    }

                    int indexColumn3 = 0;
                    while (indexColumn3 < array_Field.Length)
                    {
                        datarow[indexColumn3] = array_Field[indexColumn3];
                        indexColumn3++;
                    }

                    dataTable.Rows.Add(datarow);
                    index_Row++;
                }
            }



            // ストリームを閉じます。
            reader.Close();

            return dataTable;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private char charSeparator;

        /// <summary>
        /// 区切り文字。初期値は「,」
        /// </summary>
        public char CharSeparator
        {
            get
            {
                return charSeparator;
            }
            set
            {
                charSeparator = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
