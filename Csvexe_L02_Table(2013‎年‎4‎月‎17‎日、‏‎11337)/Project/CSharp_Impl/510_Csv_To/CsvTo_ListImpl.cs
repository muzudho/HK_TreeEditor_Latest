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
    public class CsvTo_ListImpl
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public CsvTo_ListImpl()
        {
            this.charSeparator = ',';
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// Listを作成します。
        /// 
        /// セルのデータ型は全て string です。
        /// 
        /// 【仕様変更 2011-03-01】空行、スペースだけの行は、トークンに入れません。
        /// 【仕様変更 2011-03-01】行の最後が「,」で終わる場合、最後のトークンは空白が入っているのではなく、追加しません。
        /// </summary>
        /// <param name="csvText"></param>
        /// <returns></returns>
        public List<string> Read(
            string string_Csv
            )
        {
            // テーブルを作成します。
            List<string> list_String = new List<string>();

            System.IO.StringReader reader = new System.IO.StringReader(string_Csv);
            CsvLineParserImpl csvParser = new CsvLineParserImpl();

            // CSVを解析して、テーブル形式で格納。
            {
                string[] fields;
                while (-1 < reader.Peek())
                {
                    string sLine = reader.ReadLine();
                    // 空行、スペースだけの行を拾うこともある。

                    if ("" != sLine.Trim())
                    {
                        //
                        // 「空行、スペースだけの行」ではない場合。

                        fields = csvParser.UnescapeLineToFieldList(sLine, this.CharSeparator).ToArray();

                        //essageBox.Show("ttbwIndex=[" + ttbwIndex + "]行目ループ", "デバッグ2");

                        for (int nColumnIndex = 0; nColumnIndex < fields.Length; nColumnIndex++)
                        {
                            if (nColumnIndex + 1 <= fields.Length && "" == fields[nColumnIndex].Trim())
                            {
                                // 行の最後が「,」で終わる場合、最後のトークンは空白が入っているのではなく、追加しません。
                                break;
                            }
                            list_String.Add(fields[nColumnIndex]);
                        }
                    }

                }
            }

            // ストリームを閉じます。
            reader.Close();

            return list_String;
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
