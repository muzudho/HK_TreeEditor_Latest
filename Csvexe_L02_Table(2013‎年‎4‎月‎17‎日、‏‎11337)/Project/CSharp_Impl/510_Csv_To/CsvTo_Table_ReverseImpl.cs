using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Xenon.Syntax;



namespace Xenon.Table
{

    /// <summary>
    /// 旧名：CsvTo_Table_Humaninput_ReverseImpl
    /// </summary>
    public class CsvTo_Table_ReverseImpl
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public CsvTo_Table_ReverseImpl()
        {
            this.charSeparator = ',';
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// TODO:「,」「"」に対応したい。
        /// 
        /// 
        /// 縦と横が逆のテーブル。
        /// 
        /// CSVを読取り、テーブルにして返します。
        /// 
        /// 
        /// SRS仕様の実装状況
        /// ここでは、先頭行を[0]行目と数えるものとします。
        /// (1)CSVの[0]行目は列名です。
        /// (2)CSVの[1]行目は型名です。
        /// (3)CSVの[2]行目はコメントです。
        /// 
        /// (4)データ・テーブル部で、0列目に「EOF」と入っていれば終了。大文字・小文字は区別せず。
        ///    それ以降に、コメントのようなデータが入力されていることがあるが、フィールドの型に一致しないことがあるので無視。
        ///    TODO EOF以降の行も、コメントとして残したい。
        /// 
        /// (5)列名にENDがある場合、その手前までの列が有効データです。
        ///    END以降の列は無視します。
        ///    TODO END以降の行も、コメントとして残したい。
        /// 
        /// (6)int型として指定されているフィールドのデータ・テーブル部に空欄があった場合、DBNull（データベース用のヌル）とします。
        /// </summary>
        /// <param name="csvText"></param>
        /// <returns>列名情報も含むテーブル。列の型は文字列型とします。</returns>
        public Table_Humaninput Read(
            string string_Csv,
            Request_ReadsTable forTable_Request,
            Format_Table forTable_Format,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "Read",log_Reports);

            //
            //
            //
            //
            CsvLineParserImpl csvParser = new CsvLineParserImpl();


            Table_Humaninput xenonTable = new Table_HumaninputImpl(
                forTable_Request.Name_PutToTable, forTable_Request.Expression_Filepath, forTable_Request.Expression_Filepath.Cur_Configuration );
            xenonTable.Tableunit = forTable_Request.Tableunit;
            xenonTable.Typedata = forTable_Request.Typedata;
            xenonTable.IsDatebackupActivated = forTable_Request.IsDatebackupActivated;
            xenonTable.Format_Table_Humaninput = forTable_Format;


            //
            // 一旦、テーブルを全て読み込みます。
            //
            List<List<string>> lines = new List<List<string>>();

            {
                // CSVテキストを読み込み、型とデータのバッファーを作成します。
                System.IO.StringReader reader = new System.IO.StringReader(string_Csv);


                while (-1 < reader.Peek())
                {
                    string sLine = reader.ReadLine();
                    List<string> tokens = new List<string>();

                    string[] sFields;
                    sFields = csvParser.UnescapeLineToFieldList(sLine, this.charSeparator).ToArray();

                    int nColumnIndex = 0;
                    foreach (string sToken in sFields)
                    {
                        if (nColumnIndex == 0 &&
                            (
                            ToCsv_Table_Humaninput_RowColRegularImpl.S_EOL == sToken.Trim().ToUpper() ||
                            ToCsv_Table_Humaninput_RowColRegularImpl.S_END == sToken.Trim().ToUpper()
                            )                            
                            )
                        {
                            // 1列目にENDがある場合、その手前までの列が有効データです。
                            // END以降の行は無視します。
                            goto row_end;
                        }


                        tokens.Add(sToken);

                        nColumnIndex++;
                    }
                    lines.Add(tokens);
                }
            row_end:

                // ストリームを閉じます。
                reader.Close();
            }





            //
            // 型定義部
            //
            // （※NO,ID,EXPL,NAME など、フィールドの定義を持つテーブル）
            //
            RecordFielddef recordFielddef = new RecordFielddefImpl();

            //
            // データ・テーブル部
            //
            List<List<string>> rows = new List<List<string>>();

            //
            // まず、0列目、1列目、2列目のデータを読み取ります。
            //
            int nRowIndex=0;
            foreach (List<string> tokens in lines)
            {
                Fielddef fieldDefinition = null;



                int nColumnIndex = 0;
                foreach(string sToken in tokens)
                {

                    if(0==nColumnIndex)
                    {
                        //
                        // 0列目は、フィールド名です。
                        //
                        string sFieldName = sToken;//.Trim().ToUpper();

                        // テーブルのフィールドを追加します。型の既定値は文字列型とします。
                        fieldDefinition = new FielddefImpl(sFieldName, EnumTypeFielddef.String);
                        recordFielddef.Add(fieldDefinition);
                    }
                    else if(1==nColumnIndex)
                    {
                        //
                        // 1列目は、フィールドの型名です。
                        //
                        nColumnIndex = 1;
                        string sFieldTypeNameLower = sToken.Trim().ToLower();

                        // テーブルのフィールドを追加します。型の既定値は文字列型とします。
                        // TODO int型とboolean型にも対応したい。
                        if (FielddefImpl.S_STRING.Equals(sFieldTypeNameLower))
                        {
                            fieldDefinition.Type_Field = EnumTypeFielddef.String;
                        }
                        else if (FielddefImpl.S_INT.Equals(sFieldTypeNameLower))
                        {
                            fieldDefinition.Type_Field = EnumTypeFielddef.Int;
                        }
                        else if (FielddefImpl.S_BOOL.Equals(sFieldTypeNameLower))
                        {
                            fieldDefinition.Type_Field = EnumTypeFielddef.Bool;
                        }
                        else
                        {
                            // 型が未定義の列は、文字列型として読み取ります。

                            // TODO: 警告。（エラーではない）

                            Log_TextIndented t = new Log_TextIndentedImpl();
                            t.Append("▲エラー45！(" + Info_Table.Name_Library + ")");
                            t.Newline();

                            t.Append("型の名前を記入してください。");
                            t.Append(Environment.NewLine);
                            t.Append(Environment.NewLine);
                            t.Append("※縦と横がひっくり返っているテーブルと指定されています。");
                            t.Append(Environment.NewLine);
                            t.Append(Environment.NewLine);
                            t.Append("1列目（先頭を0とする）に、型の名前は必須です。");
                            t.Append(Environment.NewLine);
                            t.Append(Environment.NewLine);
                            t.Append("[");

                            t.Append(fieldDefinition.Name_Humaninput);

                            t.Append("]フィールド　（[");
                            t.Append(nRowIndex);
                            t.Append("]行目）に、");
                            t.Append(Environment.NewLine);
                            t.Append("型名が[");
                            t.Append(sFieldTypeNameLower);
                            t.Append("]と入っています。この名前には、未対応です。");
                            t.Append(Environment.NewLine);
                            t.Append(Environment.NewLine);
                            t.Append("文字列型として続行します。");
                            t.Append(Environment.NewLine);
                            t.Append(Environment.NewLine);
                            t.Append("テーブル名＝[");
                            t.Append(forTable_Request.Name_PutToTable);
                            t.Append("]");
                            t.Append(Environment.NewLine);
                            t.Append("ファイル・パス＝[");
                            t.Append(forTable_Request.Expression_Filepath.Humaninput);
                            t.Append("]");
                            t.Append(Environment.NewLine);
                            t.Append(Environment.NewLine);

                            string sWarning = t.ToString();

                            MessageBox.Show(sWarning, "▲警告！（L02）");

                            fieldDefinition.Type_Field = EnumTypeFielddef.String;
                        }
                    }
                    else if(2==nColumnIndex)
                    {
                        //
                        // 2列目は、フィールドのコメントとします。
                        //
                        nColumnIndex = 2;
                        {
                            fieldDefinition.Comment = sToken;
                        }

                    }
                    else
                    {
                        //
                        // 3列目から右側は、データ・テーブル部。
                        //

                        if(0==nRowIndex)
                        {
                            //
                            // 先頭行
                            //

                            //
                            // 「EOF」というトークンが出てくるまで。
                            //
                            if(ToCsv_Table_Humaninput_RowColRegularImpl.S_EOF==sToken.Trim().ToUpper())
                            {
                                goto column_end;
                            }

                            List<string> record = new List<string>();

                            // 1番目のフィールド_データを追加。
                            record.Add( sToken);

                            rows.Add(record);
                        }
                        else
                        {
                            //
                            // 2番目以降のフィールド_データを追加。
                            //

                            //
                            // 先頭の3つのレコード分、切り詰めます。
                            //
                            int nDataIndex = nColumnIndex - 3;
                            if (nDataIndex < rows.Count)
                            {
                                List<string> record = rows[nDataIndex];

                                record.Add(sToken);
                            }
                            else
                            {
                                // 無視
                            }
                        }
                    }


                    nColumnIndex ++;
                }//c
            column_end:


                nRowIndex++;
            }





            //essageBox.Show("CSV読取終わり1 rows.Count=[" + rows.Count + "]", "TableCsvLibデバッグ");

            // テーブル作成。テーブルのフィールド型定義と、データ本体をセットします。
            xenonTable.CreateTable(recordFielddef, log_Reports);
            if (log_Reports.Successful)
            {
                xenonTable.AddRecordList(rows, recordFielddef, log_Reports);
                //essageBox.Show("CSV読取後のテーブル作成終わり", "TableCsvLibデバッグ");
            }

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return xenonTable;
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
