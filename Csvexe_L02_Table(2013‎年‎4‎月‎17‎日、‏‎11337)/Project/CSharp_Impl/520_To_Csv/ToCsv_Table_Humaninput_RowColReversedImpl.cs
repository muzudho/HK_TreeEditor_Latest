using System;
using System.Collections.Generic;
using System.Data;//DataTable
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Xenon.Syntax;//WarningReports

namespace Xenon.Table
{

    //
    // 行と列が逆になっているテーブル。
    //
    public class ToCsv_Table_Humaninput_RowColReversedImpl
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public ToCsv_Table_Humaninput_RowColReversedImpl()
        {
            this.o_ExceptedFields = new ExceptedFields();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 「"」や「,」には対応していない。
        /// </summary>
        /// <param name="table"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public string ToCsvText(
            Table_Humaninput table,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "ToCsvText",log_Reports);

            string sResult = "";


            //
            //
            // （０）
            //
            //

            if (null == table)
            {
                // エラー
                goto gt_Error_NullTable;
            }


            //
            //
            // テーブルは、次の処理が一番重い。
            //
            // object[] dataRowItems = dataRow.ItemArray;
            //
            // 行から列一覧を取得する処理は、最大で、行数と同じ値までにしたい。
            //
            //

            //
            //
            // （１）
            //
            //


            List<List<string>> rsltTable = this.ToModel(table, log_Reports);
            if (!log_Reports.Successful)
            {
                // 既エラー。
                goto gt_EndMethod;
            }

            sResult = this.ToText(rsltTable, table);

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NullTable:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー452！", log_Method);

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
            return sResult;
        }

        //────────────────────────────────────────

        private List<List<string>> ToModel(
            Table_Humaninput table,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "ToModel",log_Reports);

            Fielddef err_FldDef;

            // 「フィールド定義部」「データ部」両方含むテーブル。
            List<List<string>> rsltTable = new List<List<string>>();

            // フィールド名をカンマ区切りで出力します。最後にENDを付加します。

            // フィールド定義部
            //List<Fielddef> list_FieldDefinition = table.List_Fielddef;

            // データ・テーブル部
            DataTable dataTable = table.DataTable;





            // 「END,END,END...」行を除く、行数。
            int nHorizontalCountExceptEnd = table.RecordFielddef.Count;
            //essageBox.Show("「END,END,END...」行を除く、行数=[" + horizontalCountExceptEnd + "]", this.GetType().Name + "#Textize_rowColReversed: (Table)");



            bool bAllIntFields = table.Format_Table_Humaninput.IsAllintfieldsActivated;

            DataRow dataRow;

            string sCellValue;



            //
            // フィールド名の行
            //
            {
                List<string> sList_FieldNameRow = new List<string>();

                table.RecordFielddef.ForEach(delegate(Fielddef fielddefinition, ref bool isBreak2, Log_Reports log_Reports2)
                {
                    if (this.O_ExceptedFields.TryExceptedField(fielddefinition.Name_Trimupper))
                    {
                        // 出力しないフィールドの場合、無視します。
                    }
                    else
                    {
                        sList_FieldNameRow.Add(fielddefinition.Name_Humaninput);
                    }
                }, log_Reports);

                rsltTable.Add(sList_FieldNameRow);
            }



            //
            // 型名の行
            //
            if (bAllIntFields)
            {
                //
                // 全部 int型フィールドなので、型名を記述しない場合。
                //
            }
            else
            {
                //
                // 型名を記述する場合。
                //

                List<string> sList_FieldTypeRow = new List<string>();

                table.RecordFielddef.ForEach(delegate(Fielddef fielddefinition, ref bool isBreak2, Log_Reports log_Reports2)
                {
                    if (this.O_ExceptedFields.TryExceptedField(fielddefinition.Name_Trimupper))
                    {
                        // 出力しないフィールドの場合、無視します。
                    }
                    else
                    {

                        switch(fielddefinition.Type_Field)
                        {
                            case EnumTypeFielddef.String:
                                {
                                    sList_FieldTypeRow.Add(FielddefImpl.S_STRING);
                                }
                                break;
                            case EnumTypeFielddef.Int:
                                {
                                    sList_FieldTypeRow.Add(FielddefImpl.S_INT);
                                }
                                break;
                            case EnumTypeFielddef.Bool:
                                {
                                    sList_FieldTypeRow.Add(FielddefImpl.S_BOOL);
                                }
                                break;
                            default:
                                {
                                    // TODO エラー対応。

                                    // 未定義の型があった場合、そのまま出力します。
                                    // C#のメッセージになるかと思います。
                                    sList_FieldTypeRow.Add(fielddefinition.ToString_Type());
                                }
                                break;
                        }

                    }
                }, log_Reports);
            }



            //
            // コメント行
            //
            {
                List<string> sList_FieldNameRow = new List<string>();
                table.RecordFielddef.ForEach(delegate(Fielddef fielddefinition, ref bool isBreak2, Log_Reports log_Reports2)
                {
                    if (this.O_ExceptedFields.TryExceptedField(fielddefinition.Name_Trimupper))
                    {
                        // 出力しないフィールドの場合、無視します。
                    }
                    else
                    {
                        sList_FieldNameRow.Add(fielddefinition.Comment);
                    }
                }, log_Reports);
                rsltTable.Add(sList_FieldNameRow);
            }



            //
            // データ部（フィールド定義部の次の行から始まるテーブル）
            //
            {
                // 「列定義」「EOF」列を除く、「データ部」だけの列数。
                int nDataCount = dataTable.Rows.Count;

                for (int nR = 0; nR < nDataCount; nR++)
                {
                    dataRow = dataTable.Rows[nR];
                    object[] itemArray = dataRow.ItemArray;//ItemArrayは1回の呼び出しが重い。



                    List<string> sList_DtRow = new List<string>();
                    int indexColumn = 0;
                    table.RecordFielddef.ForEach(delegate(Fielddef fielddefinition, ref bool isBreak2, Log_Reports log_Reports2)
                    {
                        if (this.O_ExceptedFields.TryExceptedField(fielddefinition.Name_Trimupper))
                        {
                            // 出力しないフィールドの場合、無視します。
                        }
                        else
                        {

                            if (itemArray[indexColumn] is Cell)
                            {
                                sCellValue = ((Cell)itemArray[indexColumn]).Text;
                            }
                            else
                            {
                                // （１１）エラー
                                err_FldDef = fielddefinition;
                                isBreak2 = true;
                                goto gt_Error_UndefinedFieldType;
                            }

                            sList_DtRow.Add(sCellValue);
                        }

                        goto gt_EndInnermethod;
                        //
                        #region 異常系
                    //────────────────────────────────────────
                    gt_Error_UndefinedFieldType:
                        if (log_Reports.CanCreateReport)
                        {
                            Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                            r.SetTitle("▲エラー455！", log_Method);

                            Log_TextIndented s = new Log_TextIndentedImpl();

                            s.Append("（プログラム内部エラー）未定義のフィールド型=[");
                            s.Append(err_FldDef.ToString_Type());
                            s.Append("]");
                            s.Newline();

                            // ヒント

                            r.Message = s.ToString();
                            log_Reports.EndCreateReport();
                        }
                    goto gt_EndInnermethod;
                        //────────────────────────────────────────
                        #endregion
                        //
                    gt_EndInnermethod:
                        indexColumn++;
                    }, log_Reports);

                    rsltTable.Add(sList_DtRow);
                }
            }

            goto gt_EndMethod;
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return rsltTable;
        }


        private string ToText(
            List<List<string>> rsltTable,
            Table_Humaninput table
            )
        {
            bool bCommaEnding = table.Format_Table_Humaninput.IsCommaending;


            //
            // 行列をひっくり返して保存します。
            //
            List<StringBuilder> lineWriters = new List<StringBuilder>();

            for (int nR = 0; nR < rsltTable.Count; nR++)
            {
                List<string> row = rsltTable[nR];

                for (int nC = 0; nC < row.Count; nC++)
                {
                    if (lineWriters.Count <= nC)
                    {
                        lineWriters.Add(new StringBuilder());
                    }

                    lineWriters[nC].Append(row[nC]);

                    // 行列がひっくり返っているので分かりづらいが、
                    // 次の行があれば、カンマを付ける。
                    if (nR + 1 < rsltTable.Count)
                    {
                        lineWriters[nC].Append(",");
                    }
                }
            }


            StringBuilder sb_Result = new StringBuilder();

            for (int nL = 0; nL < lineWriters.Count; nL++)
            {
                sb_Result.Append(lineWriters[nL].ToString());

                //
                // EOF
                //
                if (0 == nL)
                {
                    sb_Result.Append(",");
                    sb_Result.Append(ToCsv_Table_Humaninput_RowColRegularImpl.S_EOF);
                }

                if (bCommaEnding)
                {
                    //
                    // 行の末尾を「,」で終わる場合。
                    //
                    sb_Result.Append(",");
                }

                sb_Result.Append(Environment.NewLine);
            }

            //
            // 最終行：EOF列の手前まで、「END」で埋めます。
            //
            {
                // 行数と同じ数の1個手前まで。
                for (int nC = 0; nC < rsltTable.Count - 1; nC++)
                {
                    sb_Result.Append(
                        ToCsv_Table_Humaninput_RowColRegularImpl.S_EOL
                        //ToCsv_Table_Humaninput_RowColRegularImpl.S_END
                        );
                    sb_Result.Append(",");
                }
                // 行数と同じ数に。
                sb_Result.Append(
                    ToCsv_Table_Humaninput_RowColRegularImpl.S_EOL
                    //ToCsv_Table_Humaninput_RowColRegularImpl.S_END
                    );

                // EOF列には END は付けません。

                if (bCommaEnding)
                {
                    //
                    // 行の末尾を「,」で終わる場合。
                    //
                    sb_Result.Append(",");
                }
            }

            // 最後に一応、改行を付けておきます。
            sb_Result.Append(Environment.NewLine);//改行

            return sb_Result.ToString();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private ExceptedFields o_ExceptedFields;

        /// <summary>
        /// 出力しないフィールド名のリスト。
        /// </summary>
        public ExceptedFields O_ExceptedFields
        {
            get
            {
                return o_ExceptedFields;
            }
            set
            {
                o_ExceptedFields = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
