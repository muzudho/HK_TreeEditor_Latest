using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Xenon.Syntax;



namespace Xenon.Table
{

    /// <summary>
    /// 旧名：CsvTo_Table_Humaninput_ReverseAllIntsImpl
    /// </summary>
    public class CsvTo_Table_ReverseAllIntsImpl
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public CsvTo_Table_ReverseAllIntsImpl()
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
        /// 縦、横がひっくり返っていて、
        /// 型定義レコードがないCSVテーブルの読取。
        /// </summary>
        /// <param name="csvText"></param>
        /// <returns>列名情報も含むテーブル。</returns>
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
                forTable_Request.Name_PutToTable, forTable_Request.Expression_Filepath, forTable_Request.Expression_Filepath.Cur_Configuration);
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


                string[] sFields;
                while (-1 < reader.Peek())
                {
                    string sLine = reader.ReadLine();
                    List<string> tokens = new List<string>();

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
            // まず、0列目、1列目のデータを読み取ります。
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

                        // テーブルのフィールドを追加します。フィールドの型は、intに固定です。
                        fieldDefinition = new FielddefImpl(sFieldName, EnumTypeFielddef.Int);
                        recordFielddef.Add(fieldDefinition);
                    }
                    else if(1==nColumnIndex)
                    {
                        //
                        // 1列目は、フィールドのコメントとします。
                        //
                        nColumnIndex = 1;
                        {
                            fieldDefinition.Comment = sToken;
                        }

                    }
                    else
                    {
                        //
                        // 2列目から右側は、データ・テーブル部。
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
                            // 先頭の2つのレコード分、切り詰めます。
                            //
                            int nDataIndex = nColumnIndex - 2;
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
            xenonTable.CreateTable(recordFielddef,log_Reports);
            if( log_Reports.Successful)
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
