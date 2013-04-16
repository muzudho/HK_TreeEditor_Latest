using System;
using System.Collections.Generic;
using System.Data;//DataTable
using System.Linq;
using System.Text;

using Xenon.Syntax;


namespace Xenon.Table
{


    public class ToCsv_Table_Humaninput_RowColRegularImpl
    {



        #region 用意
        //────────────────────────────────────────

        public const string S_EOF = "EOF";

        public const string S_EOL = "EOL";
        public const string S_END = "END";

        //────────────────────────────────────────
        #endregion



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public ToCsv_Table_Humaninput_RowColRegularImpl()
        {
            this.exceptedFields = new ExceptedFields();
            this.charSeparator = ',';
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public string ToCsvText(
            Table_Humaninput tableH,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "ToCsvText",log_Reports);

            Log_TextIndented result = new Log_TextIndentedImpl();

            RecordFielddef error_RecordFielddef;
            Exception err_Excep;
            int error_IndexColumn;
            Fielddef error_Fielddef;
            object error_Item;

            if (null == tableH)
            {
                // エラー
                goto gt_Error_NullTable;
            }

            CsvLineParserImpl csvParser = new CsvLineParserImpl();

            // フィールド名をカンマ区切りで出力します。最後にENDを付加します。

            // フィールド定義部
            if (tableH.RecordFielddef.Count < 1)
            {
                //エラー。
                error_RecordFielddef = tableH.RecordFielddef;
                goto gt_Error_FieldZero;
            }


            // フィールド定義部：名前
            tableH.RecordFielddef.ForEach(delegate(Fielddef fielddefinition, ref bool isBreak, Log_Reports log_Reports2)
            {
                if (this.ExceptedFields.TryExceptedField(fielddefinition.Name_Trimupper))
                {
                    // 出力しないフィールドの場合、無視します。
                }
                else
                {
                    result.Append(csvParser.EscapeCell(fielddefinition.Name_Humaninput));
                    result.Append(",");
                }
            }, log_Reports);
            result.Append(
                ToCsv_Table_Humaninput_RowColRegularImpl.S_EOL //ToCsv_Table_Humaninput_RowColRegularImpl.S_END
                );
            result.Append(Environment.NewLine);//改行

            // フィールド定義部：型
            tableH.RecordFielddef.ForEach(delegate(Fielddef fielddefinition, ref bool isBreak, Log_Reports log_Reports2)
            {
                if (this.ExceptedFields.TryExceptedField(fielddefinition.Name_Trimupper))
                {
                    // 出力しないフィールドの場合、無視します。
                }
                else
                {
                    switch(fielddefinition.Type_Field)
                    {
                        case EnumTypeFielddef.String:
                            {
                                result.Append(FielddefImpl.S_STRING);
                            }
                            break;
                        case EnumTypeFielddef.Int:
                            {
                                result.Append(FielddefImpl.S_INT);
                            }
                            break;
                        case EnumTypeFielddef.Bool:
                            {
                                result.Append(FielddefImpl.S_BOOL);
                            }
                            break;
                        default:
                            {
                                // TODO エラー対応。

                                // 未定義の型があった場合、そのまま出力します。
                                // C#のメッセージになるかと思います。
                                result.Append(fielddefinition.ToString_Type());
                            }
                            break;
                    }

                    result.Append(",");
                }
            }, log_Reports);
            result.Append(
                ToCsv_Table_Humaninput_RowColRegularImpl.S_EOL
                //ToCsv_Table_Humaninput_RowColRegularImpl.S_END
                );
            result.Append(Environment.NewLine);//改行

            // フィールド定義部：コメント
            tableH.RecordFielddef.ForEach(delegate(Fielddef fielddefinition, ref bool isBreak, Log_Reports log_Reports2)
            {
                if (this.ExceptedFields.TryExceptedField(fielddefinition.Name_Trimupper))
                {
                    // 出力しないフィールドの場合、無視します。
                }
                else
                {
                    result.Append(csvParser.EscapeCell(fielddefinition.Comment));
                    result.Append(",");
                }
            }, log_Reports);
            result.Append(
                ToCsv_Table_Humaninput_RowColRegularImpl.S_EOL
                //ToCsv_Table_Humaninput_RowColRegularImpl.S_END
                );
            result.Append(Environment.NewLine);//改行

            // 0行目から数えて3行目以降はデータ・テーブル部。

            // データ・テーブル部
            DataTable dataTable = tableH.DataTable;

            // 各行について
            for (int nRowIndex = 0; nRowIndex < dataTable.Rows.Count; nRowIndex++)
            {
                DataRow dataRow = dataTable.Rows[nRowIndex];

                //
                // 各フィールドについて
                //
                object[] itemArray = dataRow.ItemArray;// ItemArrayは1回の呼び出しが重い。
                for (int indexColumn = 0; indexColumn < itemArray.Length; indexColumn++)
                {

                    // TODO:範囲 リストサイズが0の時がある←プログラムミス？
                    Fielddef fielddefinition;
                    try
                    {
                        fielddefinition = tableH.RecordFielddef.ValueAt(indexColumn);
                    }
                    catch (Exception e)
                    {
                        // エラー。
                        err_Excep = e;
                        error_RecordFielddef = tableH.RecordFielddef;
                        error_IndexColumn = indexColumn;
                        goto gt_Error_OutOfIndex;
                    }

                    if (this.ExceptedFields.TryExceptedField(fielddefinition.Name_Trimupper))
                    {
                        // 出力しないフィールドの場合、無視します。
                    }
                    else
                    {
                        string value_Cell;
                        object item = itemArray[indexColumn];

                        if (item is Cell)
                        {
                            value_Cell = ((Cell)item).Text;
                        }
                        else if (item is string)
                        {
                            //フィールド定義部など。
                            value_Cell = (string)item;
                        }
                        else if (item is DBNull)
                        {
                            //空欄。
                            value_Cell = "";
                        }
                        else
                        {
                            // エラー
                            error_Item = item;
                            error_Fielddef = fielddefinition;
                            goto gt_Error_UndefinedFieldType;
                        }

                        result.Append(csvParser.EscapeCell(value_Cell));
                        result.Append(this.charSeparator);
                    }
                }
                result.Append(
                    ToCsv_Table_Humaninput_RowColRegularImpl.S_EOL
                    //ToCsv_Table_Humaninput_RowColRegularImpl.S_END
                    );
                result.Append(Environment.NewLine);//改行
            }
            result.Append(ToCsv_Table_Humaninput_RowColRegularImpl.S_EOF);
            // 最後に一応、改行を付けておきます。
            result.Append(Environment.NewLine);//改行

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_FieldZero:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー854！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();

                s.Append("（プログラム内部エラー）テーブルの列定義が０件です。 error_RecordFielddef.Count[");
                s.Append(error_RecordFielddef.Count);
                s.Append("] テーブル名＝[");
                s.Append(tableH.Name);
                s.Append("]");
                s.Newline();

                // ヒント

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_OutOfIndex:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー853！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();

                s.Append("（プログラム内部エラー）err_NColIndex=[");
                s.Append(error_IndexColumn);
                s.Append("] error_RecordFielddef.Count[");
                s.Append(error_RecordFielddef.Count);
                s.Append("]");
                s.Newline();

                // ヒント
                s.Append(r.Message_SException(err_Excep));

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_UndefinedFieldType:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー855！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();

                s.Append("（プログラム内部エラー）CSVを出力しようとしたとき、未定義のフィールド型=[");
                s.Append(error_Fielddef.ToString_Type());
                s.Append("]がありました。");
                s.Newline();

                s.Append("型名=[");
                s.Append(error_Item.GetType().Name);
                s.Append("]");
                s.Newline();

                s.Append("型は[");
                s.Append(typeof(StringCellImpl));
                s.Append("],[");
                s.Append(typeof(IntCellImpl));
                s.Append("],[");
                s.Append(typeof(BoolCellImpl));
                s.Append("]が使えます。");
                s.Newline();

                // ヒント

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_NullTable:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー852！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();

                s.Append("（プログラム内部エラー）tableがヌルでした。");
                s.Newline();

                // ヒント

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result.ToString();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private ExceptedFields exceptedFields;

        /// <summary>
        /// 出力しないフィールド名のリスト。
        /// </summary>
        public ExceptedFields ExceptedFields
        {
            get
            {
                return exceptedFields;
            }
            set
            {
                exceptedFields = value;
            }
        }

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
