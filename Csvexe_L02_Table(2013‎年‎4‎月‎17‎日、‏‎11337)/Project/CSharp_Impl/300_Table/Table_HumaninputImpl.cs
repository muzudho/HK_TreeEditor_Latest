using System;
using System.Data;//DataTable,DataRow
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;//ManualFilePath,WarningReports


namespace Xenon.Table
{
    /// <summary>
    /// テーブルです。
    /// 各セルが「Humaninput_～」型になっており、int型の列にも空白文字列などを入力可能になっています。
    /// 
    /// フィールドの型定義と、0～複数件のレコードを持ちます。
    /// </summary>
    public class Table_HumaninputImpl : Configurationtree_NodeImpl, Table_Humaninput
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="e_Fpath_ConfigStack"></param>
        public Table_HumaninputImpl(
            string name_Table,
            Expression_Node_Filepath filepath_Nodeconfigtree_Expr,
            Configuration_Node cur_Conf
            )
            : base(name_Table, cur_Conf)//"ノード名未指定"
        {
            this.expression_Filepath_ConfigStack = filepath_Nodeconfigtree_Expr;

            this.dataTable = new DataTable();
            this.name_Table = name_Table;
            this.format_Table_Humaninput = new Format_TableImpl();
            this.recordFielddef_ = new RecordFielddefImpl();//暫定
        }

        //────────────────────────────────────────

        /// <summary>
        /// 設定された型リストで、テーブルの構造を作成します。
        /// </summary>
        public void CreateTable(RecordFielddef recordFielddef_New, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "CreateTable",log_Reports);

            //

            this.dataTable.Clear();
            this.recordFielddef_ = recordFielddef_New;

            Exception error_Excp;
            recordFielddef_New.ForEach(delegate(Fielddef fielddefinition_New, ref bool isBreak2, Log_Reports log_Reports2)
            {
                // 列の型を決めます。
                try
                {
                    this.dataTable.Columns.Add(fielddefinition_New.Name_Trimupper, fielddefinition_New.ToType_Field());
                }
                catch (DuplicateNameException e)
                {
                    error_Excp = e;
                    goto gt_Error_Duplicated;
                }

                goto gt_EndInnermethod;
                //
                #region 異常系
            //────────────────────────────────────────
            gt_Error_Duplicated:
                if (log_Reports.CanCreateReport)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー111！", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();

                    s.Append("列の名前が重複しています。");
                    s.Append(error_Excp.Message);
                    s.Append(Environment.NewLine);
                    s.Append("テーブル名＝[");
                    s.Append(this.dataTable.TableName);
                    s.Append("]");

                    r.Message = s.ToString();
                    log_Reports.EndCreateReport();
                }
            goto gt_EndInnermethod;
            //────────────────────────────────────────
                #endregion
            //
            gt_EndInnermethod:
                ;
            }, log_Reports);

            goto gt_EndMethod;
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────
        //
        // テーブル作成
        //

        /// <summary>
        /// NOフィールドを 0からの連番に振りなおします。（-1が入っている場所はスキップされます）
        /// 
        /// NOフィールド値は、プログラム中で主キーとして使うことがあるので、
        /// 変更するのであれば、ファイルを読み込んだ直後にするものとします。
        /// </summary>
        public void RenumberingNoField()
        {
            if (this.DataTable.Columns.Contains("NO"))
            {
                // NOフィールドがあれば。
                int value_No = 0;// NOフィールド値。
                foreach (DataRow dataRow in this.DataTable.Rows)
                {
                    IntCellImpl intH = (IntCellImpl)dataRow["NO"];

                    int number;
                    intH.TryGet(out number);

                    if (-1 != number)
                    {
                        // NOフィールド値をセット
                        intH.SetInt(value_No);

                        value_No++;
                    }
                    else
                    {
                        // -1 が入っている場合、飛ばします。【2012-10-12 追加】
                    }

                }
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 「END」フィールドの左に、新しいフィールドを追加します。
        /// 同名の列が既に追加されている場合は無視されます。
        /// </summary>
        /// <param name="fielddefinition_New"></param>
        /// <param name="isRequired">追加に失敗したときエラーにするなら真。ただし、既に同名の列が追加されている場合は除く。</param>
        /// <param name="log_Reports"></param>
        /// <returns>追加に成功した場合、真を返します。</returns>
        public bool AddField(Fielddef fielddefinition_New, bool isRequired, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "AddField", log_Reports);

            bool isResult = false;

            if (this.DataTable.Columns.Contains(fielddefinition_New.Name_Trimupper))
            {
                //既に同名の列があれば。
                //無視。
                goto gt_EndMethod;
            }


            // 「EOLフィールド」の列index。（最新バージョン）
            // なければ、「ENDフィールド」の列index。（旧バージョンとの互換性）
            int index_FieldEol = this.RecordFielddef.ColumnIndexOf_Trimupper(ToCsv_Table_Humaninput_RowColRegularImpl.S_EOL);
            if (-1 == index_FieldEol)
            {
                index_FieldEol = this.RecordFielddef.ColumnIndexOf_Trimupper(ToCsv_Table_Humaninput_RowColRegularImpl.S_END);
            }
            int index_Insert;

            if (-1 == index_FieldEol)
            {
                index_Insert = this.RecordFielddef.Count;
            }
            else
            {
                //「END」フィールドがある場合。
                index_Insert = index_FieldEol;
            }

            isResult = this.InsertField(index_Insert, fielddefinition_New, isRequired, log_Reports);

            goto gt_EndMethod;
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return isResult;
        }

        /// <summary>
        /// フィールドを追加します。
        /// 同名の列が既に追加されている場合は無視されます。
        /// </summary>
        /// <param name="fielddefinition_New"></param>
        /// <param name="isRequired">追加に失敗したときエラーにするなら真。ただし、既に同名の列が追加されている場合は除く。</param>
        /// <param name="log_Reports"></param>
        /// <returns>追加に成功した場合、真を返します。</returns>
        public bool InsertField(int columnIndex, Fielddef fielddefinition_New, bool isRequired, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "InsertField", log_Reports);

            bool isResult = false;

            if (this.DataTable.Columns.Contains(fielddefinition_New.Name_Trimupper))
            {
                //既に同名の列があれば。
                //無視。
                goto gt_EndMethod;
            }

            // 定義部に挿入します。
            this.RecordFielddef.Insert(columnIndex, fielddefinition_New);

            // データ部テーブルの最後尾に、ダミーの列を１個追加します。
            this.DataTable.Columns.Add(
                fielddefinition_New.Name_Humaninput,// "<ADD>",//フィールド名はダミー。被らない名前が好ましい。
                fielddefinition_New.ToType_Field()
                );
            this.ForEach_Datapart(delegate(Record_Humaninput recordH, ref bool isBreak2, Log_Reports log_Reports2)
            {
                // todo:型もチェンジしてる？
                recordH.Insert(columnIndex, fielddefinition_New.NewField(log_Method.Fullname,log_Reports2), log_Reports2);


            },log_Reports);

            goto gt_EndMethod;
            //
        //
            #region 異常系
        //────────────────────────────────────────
        //gt_Error_Exists:
        //    if (log_Reports.CanCreateReport)
        //    {
        //        Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
        //        r.SetTitle("▲エラー463！", log_Method);

        //        Log_TextIndented s = new Log_TextIndentedImpl();

        //        s.Append("列定義の個数より　フィールド数の少ない入力テーブルが指定されました。");
        //        s.Newline();

        //        s.Append("実データのこの行の列数[");
        //        s.Append(err_SList_Column.Count);
        //        s.Append("] 指定した列インデックス=[");
        //        s.Append(err_NCIx);
        //        s.Append("] フィールド定義の個数=[");
        //        s.Append(recordFielddef.Count);
        //        s.Append("]");
        //        s.Newline();

        //        s.Append("──────────────────────────────テーブルに存在する列名");
        //        s.Newline();
        //        foreach (DataColumn col in err_DataRow.Table.Columns)
        //        {
        //            s.Append("実列名＝[" + col.ColumnName + "]");
        //            s.Newline();
        //        }
        //        s.Append("──────────────────────────────");
        //        s.Newline();

        //        s.Append("──────────────────────────────定義に存在する列名");
        //        s.Newline();
        //        s.Append("定義列名＝[" + recordFielddef.ToString_DebugDump() + "]");
        //        s.Newline();
        //        s.Append("──────────────────────────────");
        //        s.Newline();

        //        // ヒント

        //        r.Message = s.ToString();
        //        log_Reports.EndCreateReport();
        //    }
        //    goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return isResult;
        }

        //────────────────────────────────────────
        //
        //
        //

        public override void ToText_Content(Log_TextIndented s)
        {
            Log_Method log_Method = new Log_MethodImpl();
            Log_Reports log_Reports_ThisMethod = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "ToText_Content", log_Reports_ThisMethod);

            s.Increment();
            s.AppendI(0, "[table]");
            s.Append(Environment.NewLine);

            s.Increment();
            s.AppendI(0, "[プログラム]");
            s.Append(log_Method.Fullname);
            s.Append(Environment.NewLine);

            this.Expression_Filepath_ConfigStack.Cur_Configuration.ToText_Content(s);

            s.AppendI(0, "[/プログラム]");
            s.Append(log_Method.Fullname);
            s.Append(Environment.NewLine);
            s.Decrement();

            s.AppendI(0, "[/table]");
            s.Append(Environment.NewLine);
            s.Decrement();

            log_Method.EndMethod(log_Reports_ThisMethod);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 行を追加します。
        /// </summary>
        /// <param name="record"></param>
        public void AddRecord(Record_Humaninput recordH)
        {
            recordH.AddTo(this);
            //this.DataTable.Rows.Add(recordH.DataRow);
        }

        /// <summary>
        /// 空行を作成します。
        /// </summary>
        /// <returns></returns>
        public Record_Humaninput CreateNewRecord(string config, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "CreateNewRecord", log_Reports);

            // 新しいレコードを1つ作ります。
            DataRow newDataRow = this.DataTable.NewRow();
            // 全ての列は現在、DBNullになっています。

            // ひとまず、全ての列に有効なインスタンスを設定します。

            int indexColumn = 0;
            this.RecordFielddef.ForEach(delegate(Fielddef fielddefinition, ref bool isBreak2, Log_Reports log_Reports2)
            {
                // 列の型を調べます。

                // セルのソースヒント名
                string nodeConfigtree_NameOfCell;
                if ("" == fielddefinition.Name_Trimupper)
                {
                    // 名無しフィールド

                    // フィールド名がないので、インデックスで指定します。
                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append("(");
                    s.Append(indexColumn);
                    s.Append(")番フィールド");
                    nodeConfigtree_NameOfCell = s.ToString();
                }
                else
                {
                    nodeConfigtree_NameOfCell = fielddefinition.Name_Humaninput;
                }

                switch (fielddefinition.Type_Field)
                {
                    case EnumTypeFielddef.String:
                        {
                            StringCellImpl stringH = new StringCellImpl(nodeConfigtree_NameOfCell);
                            stringH.Text = "";

                            if ("" == fielddefinition.Name_Trimupper)
                            {
                                // 名無しフィールド
                                // フィールド名がないので、インデックスで指定します。
                                newDataRow[indexColumn] = stringH;
                            }
                            else
                            {
                                newDataRow[fielddefinition.Name_Trimupper] = stringH;
                            }
                        }
                        break;
                    case EnumTypeFielddef.Int:
                        {
                            IntCellImpl intH = new IntCellImpl(nodeConfigtree_NameOfCell);
                            intH.Text = "";

                            if ("" == fielddefinition.Name_Trimupper)
                            {
                                // 名無しフィールド
                                // フィールド名がないので、インデックスで指定します。
                                newDataRow[indexColumn] = intH;
                            }
                            else
                            {
                                newDataRow[fielddefinition.Name_Trimupper] = intH;
                            }
                        }
                        break;
                    case EnumTypeFielddef.Bool:
                        {
                            BoolCellImpl boolH = new BoolCellImpl(nodeConfigtree_NameOfCell);
                            boolH.Text = "";

                            if ("" == fielddefinition.Name_Trimupper)
                            {
                                // 名無しフィールド
                                // フィールド名がないので、インデックスで指定します。
                                newDataRow[indexColumn] = boolH;
                            }
                            else
                            {
                                newDataRow[fielddefinition.Name_Trimupper] = boolH;
                            }
                        }
                        break;
                    default:
                        {
                            // 正常（警告は出したい）

                            if (log_Reports.CanCreateReport)
                            {
                                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Warning);
                                r.SetTitle("▲エラー431！", log_Method);

                                Log_TextIndented s = new Log_TextIndentedImpl();
                                s.Newline();
                                s.Append("この列は、未定義の型です。[" + fielddefinition.ToString_Type() + "]");
                                r.Message = s.ToString();

                                log_Reports.EndCreateReport();
                            }

                            // 文字列型を入れる。
                            StringCellImpl stringH = new StringCellImpl(nodeConfigtree_NameOfCell);
                            stringH.Text = "";

                            if ("" == fielddefinition.Name_Trimupper)
                            {
                                // 名無しフィールド
                                // フィールド名がないので、インデックスで指定します。
                                newDataRow[indexColumn] = stringH;
                            }
                            else
                            {
                                newDataRow[fielddefinition.Name_Trimupper] = stringH;
                            }
                        }
                        break;
                }

                indexColumn++;
            }, log_Reports);

            goto gt_EndMethod;
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
        return new Record_HumaninputImpl(config, newDataRow, this);
        }

        /// <summary>
        /// データを渡すことで、テーブルを作成します。
        /// テーブルの型定義と、データを渡します。
        /// 
        /// TODO:データテーブルによって新行を作成するので、データテーブルの列定義と、列定義リストは合わせて置かなければならない。
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="fldDefList">列定義は絞りこまれている場合もあります。</param>
        /// <param name="d_Logging_OrNull"></param>
        public void AddRecordList(
            List<List<string>> rows, RecordFielddef recordFielddef, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "AddRecordList",log_Reports);
            //

            string error_NameColumn_TrimUpper;
            Exception error_Excep;
            DataRow error_DataRow;
            List<string> error_List_NameColumn;
            int error_indexColumn;

            // テーブルデータを作成します。
            for (int indexRow = 0; indexRow < rows.Count; indexRow++)
            {
                List<string> list_NameColumn = rows[indexRow];

                // 行オブジェクトを作成。
                DataRow dataRow = this.dataTable.NewRow();

                // TODO:これで合ってる？　入力テーブルの行数と、列定義の列数、小さい方に合わせます。（2012-02-11/仕様変更）
                int indexEndover;
                if (list_NameColumn.Count < recordFielddef.Count)
                {
                    indexEndover = list_NameColumn.Count;
                }
                else
                {
                    indexEndover = recordFielddef.Count;
                }

                // 行の列数ではなく、列定義の列数でループを回します。
                // 絞りこまれていることがあるからです。
                for (int indexColumn = 0; indexColumn < indexEndover; indexColumn++)
                {
                    // 引き渡されたデータを、行オブジェクトにセット
                    string nameColumn_TrimUpper = recordFielddef.ValueAt(indexColumn).Name_Trimupper;
                    if ("" == nameColumn_TrimUpper)
                    {
                        // 列定義になく、データ領域に溢れていたので追加された列か、
                        // 列名なしの列。

                        if (recordFielddef.Count <= indexColumn)
                        {
                            // フィールドを追加。
                            // 列名：　空文字列
                            // 値の型：OValue_StringImpl
                            this.dataTable.Columns.Add("", typeof(StringCellImpl));
                        }

                        // セルのソースヒント名
                        string nodeConfigtreeOfCell;
                        {
                            // フィールド名がないので、インデックスで指定します。
                            Log_TextIndented s = new Log_TextIndentedImpl();
                            s.Append("(");
                            s.Append(indexColumn);
                            s.Append(")番フィールド");
                            nodeConfigtreeOfCell = s.ToString();
                        }

                        // 列名がないので、列インデックスで指定して、データを追加。
                        // 値の型：OValue_StringImpl
                        StringCellImpl stringH = new StringCellImpl(nodeConfigtreeOfCell);
                        stringH.Text = list_NameColumn[indexColumn];
                        dataRow[indexColumn] = stringH;
                    }
                    else
                    {
                        if (list_NameColumn.Count <= indexColumn)
                        {
                            // エラー
                            error_DataRow = dataRow;
                            error_List_NameColumn = list_NameColumn;
                            error_indexColumn = indexColumn;
                            goto gt_Error_ColumnIndexOver;
                        }

                        // 値を格納。
                        Cell valueH = Utility_Row.ConfigurationTo_Field(//TODO:
                            indexColumn,
                            list_NameColumn[indexColumn],
                            recordFielddef,
                            log_Reports
                            );

                        try
                        {
                            dataRow[nameColumn_TrimUpper] = valueH;
                        }
                        catch (ArgumentException e)
                        {
                            error_DataRow = dataRow;
                            error_NameColumn_TrimUpper = nameColumn_TrimUpper;
                            error_Excep = e;
                            goto gt_Error_Field;
                        }
                    }
                }

                // テーブルに行オブジェクトをセット
                this.dataTable.Rows.Add(dataRow);
            }

            goto gt_EndMethod;
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_ColumnIndexOver:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー463！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();

                s.Append("列定義の個数より　フィールド数の少ない入力テーブルが指定されました。");
                s.Newline();

                s.Append("実データのこの行の列数[");
                s.Append(error_List_NameColumn.Count);
                s.Append("] 指定した列インデックス=[");
                s.Append(error_indexColumn);
                s.Append("] フィールド定義の個数=[");
                s.Append(recordFielddef.Count);
                s.Append("]");
                s.Newline();

                s.Append("──────────────────────────────テーブルに存在する列名");
                s.Newline();
                foreach (DataColumn col in error_DataRow.Table.Columns)
                {
                    s.Append("実列名＝[" + col.ColumnName + "]");
                    s.Newline();
                }
                s.Append("──────────────────────────────");
                s.Newline();

                s.Append("──────────────────────────────定義に存在する列名");
                s.Newline();
                s.Append("定義列名＝[" + recordFielddef.ToString_DebugDump() + "]");
                s.Newline();
                s.Append("──────────────────────────────");
                s.Newline();

                // ヒント

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Field:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー462！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();

                s.Append("フィールド名[" + error_NameColumn_TrimUpper + "]が指定されましたが、ありません。");
                s.Newline();

                s.Append("──────────────────────────────テーブルに存在する列名");
                s.Newline();
                foreach (DataColumn col in error_DataRow.Table.Columns)
                {
                    s.Append("実列名＝[" + col.ColumnName + "]");
                    s.Newline();
                }
                s.Append("──────────────────────────────");
                s.Newline();

                s.Append("──────────────────────────────定義に存在する列名");
                s.Newline();
                s.Append("定義列名＝[" + recordFielddef.ToString_DebugDump() + "]");
                s.Newline();
                s.Append("──────────────────────────────");
                s.Newline();

                // ヒント
                s.Append(r.Message_SException(error_Excep));

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
        }

        /// <summary>
        /// 行の削除。
        /// </summary>
        /// <param name="record"></param>
        public void Remove(Record_Humaninput recordH)
        {
            recordH.RemoveFrom(this);
            //this.DataTable.Rows.Remove(recordH.DataRow);
        }

        /// <summary>
        /// 指定したレコードの並び順を１つ上に上げます。
        /// </summary>
        /// <param name="ttbwIndex"></param>
        public void MoveRecordToUpByIndex(int nRowIndex)
        {
            if (0 < nRowIndex)
            {
                // 新規空行を作成
                DataRow newRow = this.DataTable.NewRow();

                // 移動させたい行の内容を、新規空行にコピー。
                newRow.ItemArray = this.DataTable.Rows[nRowIndex].ItemArray;

                // 移動させたい行を、テーブルから除外。（重複挿入防止機能を回避するため）
                this.DataTable.Rows.RemoveAt(nRowIndex);

                // 移動させたい行のコピー内容を持つ新規行を挿入。
                this.DataTable.Rows.InsertAt(newRow, nRowIndex - 1);
            }
        }

        /// <summary>
        /// 指定したレコードの並び順を１つ下に下げます。
        /// </summary>
        /// <param name="ttbwIndex"></param>
        public void MoveRecordToDownByIndex(int nRowIndex)
        {
            if (nRowIndex < this.DataTable.Rows.Count - 1)
            {
                // 新規空行を作成
                DataRow newRow = this.DataTable.NewRow();

                // 移動させたい行の内容を、新規空行にコピー。
                newRow.ItemArray = this.DataTable.Rows[nRowIndex].ItemArray;

                // 移動させたい行を、テーブルから除外。（重複挿入防止機能を回避するため）
                this.DataTable.Rows.RemoveAt(nRowIndex);

                // 移動させたい行のコピー内容を持つ新規行を挿入。
                this.DataTable.Rows.InsertAt(newRow, nRowIndex + 1);
            }
        }

        /// <summary>
        /// 指定項目A（1～複数件）を、指定項目Bの下に移動させます。
        /// </summary>
        /// <param name="sourceIndices">移動待ち要素のリスト。インデックスの昇順に並んでいる必要があります。</param>
        /// <param name="destinationIndex"></param>
        public void MoveItemsBefore(int[] indices_Source, int index_Destination)
        {
            if (index_Destination < 0 && this.DataTable.Rows.Count <= index_Destination)
            {
                // 無視。
                // テーブルの行数と同数の数字を指定しても、現実装では無視。

                return;
            }

            // ビューの不活性化(Enabled=false)は、このメソッドの外側で行ってください。

            // 位置調整に使うカウンター。
            int offset = 0;

            for (int index_SourceArray = 0; index_SourceArray < indices_Source.Length; index_SourceArray++)
            {
                int index_Source = indices_Source[index_SourceArray];

                // 要素が動いた後の、移動待ちの全要素のインデックスを見直します。
                if (index_Destination == index_Source)
                {
                    // 移動元要素が動かなかったら

                    // 無視します。
                }
                else if (index_Destination < index_Source)
                {
                    // 移動元要素が、上に移動したのなら

                    // 移動元要素が移動後に　その後ろに来る要素で、
                    // もともと移動元要素より上にあった要素は、
                    // 位置が 1つ繰り下がり（+1）ます。
                    for (int index2_Array = index_SourceArray + 1; index2_Array < indices_Source.Length; index2_Array++)
                    {
                        if (index_Destination <= indices_Source[index2_Array] && indices_Source[index2_Array] < index_Source)
                        {
                            indices_Source[index2_Array]++;
                        }
                    }

                    // TODO ・テーブルには、そのテーブルに入っている行を、また同じテーブルに挿入するということができない？
                    // TODO ・テーブルから行を抜いてしまうと、その行はもう使えなくなってしまうので、削除は避けたい。

                    // TODO ・データソースとなるテーブルの並び順を替えたなら、そのテーブルのセルをデータソースにしているコントロールは全て作り直しにする？

                    // 移動元要素を、リストから一旦抜いた後で、移動先の要素の上に挿入します。
                    DataRow newSourceItem = this.DataTable.NewRow();
                    DataRow sourceItem = this.DataTable.Rows[index_Source];
                    newSourceItem.ItemArray = sourceItem.ItemArray;//コピー
                    this.DataTable.Rows.RemoveAt(index_Source);

                    // Insertメソッドは、0から始まる数字を指定します。
                    // 指定したインデックスの上に挿入されます。
                    //
                    // 2つ目以降の要素が追加されるときは、
                    // 「先に追加した要素と同じインデックスにInsertする＝どんどん上に追加される」
                    // ことになるので、逆順になります。
                    // それを防ぐために offset で調整します。
                    this.DataTable.Rows.InsertAt(newSourceItem, index_Destination + offset);
                    offset++;
                }
                else
                {
                    // 移動元要素が、下に移動したのなら

                    // 移動元要素より下にあった要素で、
                    // 移動元要素が移動後に、その前に来る要素は、
                    // 位置が 1つ繰り上がり（-1）ます。
                    for (int index2_Array = index_SourceArray + 1; index2_Array < indices_Source.Length; index2_Array++)
                    {
                        if (indices_Source[index2_Array] <= index_Destination && index_Source < indices_Source[index2_Array])
                        {
                            indices_Source[index2_Array]--;
                        }
                    }

                    // TODO テーブルから行を抜いてしまうと、その行はもう使えなくなってしまうので、削除は避けたい。

                    // 移動元要素を、一旦リストから抜いて、移動先の要素の上に挿入します。
                    DataRow newSourceItem = this.DataTable.NewRow();
                    DataRow sourceItem = this.DataTable.Rows[index_Source];
                    newSourceItem.ItemArray = sourceItem.ItemArray;//コピー
                    this.DataTable.Rows.RemoveAt(index_Source);

                    // Insertメソッドは、0から始まる数字を指定します。
                    // 指定したインデックスの上に挿入されます。

                    // 移動元の要素が抜かれているので、移動先は1つ繰り下がって(+1)ずれこんでいます。
                    // -1 して、ずれこみを解消します。
                    this.DataTable.Rows.InsertAt(newSourceItem, index_Destination - 1);
                    // 2つ目以降の要素も　同じインデックスに追加されますが、
                    // 自分が削除されている瞬間に　移動先の要素は上に移動しているので、
                    // その空いたスペースに　自分が入ることになります。
                    // これで、正順に並ぶことになります。
                }
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// フィールドの定義を取得します。
        /// 
        /// フィールド名の英字大文字、小文字は無視します。
        /// </summary>
        /// <param name="expectedFieldName"></param>
        /// <param name="isRequired">該当なしの時に例外を投げるなら真。</param>
        /// <returns>該当なし、エラーの場合偽。</returns>
        public bool TryGetFieldDefinitionByName(
            out RecordFielddef out_RecordFielddef3,
            List<string> list_NameField_Expected,
            bool isRequired,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "TryGetFieldDefinitionByName",log_Reports);

            bool isResult = true;
            RecordFielddef recordFielddef1 = new RecordFielddefImpl();

            if (list_NameField_Expected.Count < 1)
            {
                // エラー。
                goto gt_Error_ParamNothingField;
            }


            string error_NameField_Expected;
            int count = 0;
            foreach (string nameField_Expected in list_NameField_Expected)
            {
                //
                // TODO:現状、「ID,NAME」などのカンマ区切りに対応できていない？
                //

                string name_Field_ExpectedUpper = nameField_Expected.ToUpper();

                bool isHit2 = false;
                this.RecordFielddef.ForEach(delegate(Fielddef fielddefinition,ref bool isBreak2, Log_Reports log_Reports2)
                {
                    if (fielddefinition.Name_Trimupper == name_Field_ExpectedUpper)
                    {
                        //ヒット
                        isHit2 = true;
                        recordFielddef1.Add(fielddefinition);
                        count++;
                        isBreak2 = true;
                        goto gt_NextField;
                    }

                gt_NextField:
                    ;
                }, log_Reports);

                if (!isHit2)
                {
                    // 一致するものが無かった場合。
                    recordFielddef1.Add(new FielddefImpl("<null>", EnumTypeFielddef.String));//桁合わせ。
                    isResult = false;

                    if (isRequired)
                    {
                        // エラー。
                        error_NameField_Expected = nameField_Expected;
                        goto gt_Error_NothingField1;
                    }

                    // 正常
                    goto gt_EndMethod;
                }
            }
            // 正常

            if (count < 1)
            {
                isResult = false;

                if (isRequired)
                {
                    // エラー。
                    StringBuilder s = new StringBuilder();
                    foreach (string sFld in list_NameField_Expected)
                    {
                        s.Append("[");
                        s.Append(sFld);
                        s.Append("]");
                    }
                    error_NameField_Expected = s.ToString();
                    goto gt_Error_NothingField2;
                }

            }

            // 正常
            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_ParamNothingField:
            {
                isResult = false;

                if (log_Reports.CanCreateReport)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー121！", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append("[");
                    s.Append(this.Name);
                    s.Append("]テーブルの列定義を調べようとしましたが、列名が指定されていません。sExpectedFieldNameList.Count＝[");
                    s.Append(list_NameField_Expected.Count);
                    s.Append("]");

                    // ヒント

                    r.Message = s.ToString();
                    log_Reports.EndCreateReport();
                }
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_NothingField1:
            isResult = false;
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー131！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("[");
                s.Append(this.Name);
                s.Append("]テーブルに、[");

                s.Append(error_NameField_Expected);
                s.Append("]フィールドは存在しませんでした。");
                s.Newline();
                s.Newline();

                // ヒント
                s.Append("──────────定義されている列のリスト");
                s.Newline();
                s.Append(recordFielddef1.ToString_DebugDump());
                s.Newline();
                s.Append("──────────");
                s.Newline();

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_NothingField2:
            isResult = false;
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー132！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("[");
                s.Append(this.Name);
                s.Append("]テーブルに、[");

                s.Append(error_NameField_Expected);
                s.Append("]フィールドは存在しませんでした。");
                s.Newline();
                s.Newline();

                // ヒント
                s.Append("──────────定義されている列のリスト");
                s.Newline();
                s.Append(recordFielddef1.ToString_DebugDump());
                s.Newline();
                s.Append("──────────");
                s.Newline();

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            out_RecordFielddef3 = recordFielddef1;
            log_Method.EndMethod(log_Reports);
            return isResult;
        }

        //────────────────────────────────────────

        /// <summary>
        /// NOフィールドの値で指定したレコードを返します。なければヌルです。
        /// </summary>
        /// <param name="exceptedNo"></param>
        /// <returns></returns>
        public Record_Humaninput SelectByNo(
            IntCellImpl valueint_No,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "SelectByNo",log_Reports);
            //


            Record_Humaninput result;

            if (valueint_No.IsSpaces())
            {
                // 空白なら中断。
                result = null;
                goto gt_Error_Spaces;
            }
            else if (!valueint_No.IsValidated)
            {
                // エラーデータなら中断。
                result = null;
                goto gt_Error_Invalid;
            }

            int nExpectedNo;
            bool bParsed = IntCellImpl.TryParse(
                valueint_No,
                out nExpectedNo,
                EnumOperationIfErrorvalue.Error,
                null,
                log_Reports
                );
            if (!log_Reports.Successful)
            {
                // 既エラー
                result = null;
                goto gt_EndMethod;
            }

            if (bParsed)
            {

                foreach (DataRow dataRow in this.DataTable.Rows)
                {
                    if (dataRow.Table.Columns.Contains("NO"))
                    {
                        // NO列があれば

                        Cell valueH = (Cell)dataRow["NO"];

                        if (valueH is IntCellImpl)
                        {
                            IntCellImpl intH = (IntCellImpl)valueH;

                            if (intH.IsSpaces())
                            {
                                // 空白なら無視
                            }
                            else if (!intH.IsValidated)
                            {
                                // エラーデータなら無視
                            }
                            else
                            {
                                int int_No;

                                bool isParsed2 = IntCellImpl.TryParse(
                                    intH,
                                    out int_No,
                                    EnumOperationIfErrorvalue.Error,
                                    null,
                                    log_Reports
                                    );
                                if (!log_Reports.Successful)
                                {
                                    // 既エラー
                                    result = null;
                                    goto gt_EndMethod;
                                }

                                if (isParsed2)
                                {
                                    if (nExpectedNo == int_No)
                                    {
                                        // 一致すれば。
                                        result = new Record_HumaninputImpl( int_No.ToString(), dataRow, this);
                                        goto gt_EndMethod;
                                    }
                                }
                            }
                        }
                    }
                }

            }

            result = null;
            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Spaces:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー641！", log_Method);
                r.Message = "（空白なので中断）";
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Invalid:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー642！", log_Method);
                r.Message = "（エラーデータなので中断）";
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 指定のInt型フィールドの値で指定したレコードを返します。なければヌルです。
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="expectedInt"></param>
        /// <returns>一致しなければヌル。</returns>
        public Record_Humaninput SelectByInt(
            string name_Field,
            IntCellImpl expectedIntParam,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "SelectByInt",log_Reports);

            //
            //
            //
            //

            Record_Humaninput result;

            if (expectedIntParam.IsSpaces())
            {
                // 空白なら中断。
                result = null;
                goto gt_Error_Spaces;
            }
            else if (!expectedIntParam.IsValidated)
            {
                // エラーデータなら中断。
                result = null;
                goto gt_Error_Invalid;
            }

            int int_Expected;

            bool isParsed = IntCellImpl.TryParse(
                expectedIntParam,
                out int_Expected,
                EnumOperationIfErrorvalue.Error,
                null,
                log_Reports
                );
            if (!log_Reports.Successful || !isParsed)
            {
                // 既エラー
                result = null;
                goto gt_EndMethod;
            }


            Exception err_Excp;
            try
            {
                foreach (DataRow dataRow in this.DataTable.Rows)
                {

                    Cell valueH = (Cell)dataRow[name_Field];

                    if (valueH is IntCellImpl)
                    {
                        IntCellImpl intH = (IntCellImpl)valueH;

                        if (intH.IsSpaces())
                        {
                            // 空白なら無視
                        }
                        else if (!intH.IsValidated)
                        {
                            // エラーデータなら無視
                        }
                        else
                        {
                            int int_Exists;

                            bool isParsed2 = IntCellImpl.TryParse(
                                intH,
                                out int_Exists,
                                EnumOperationIfErrorvalue.Error,
                                null,
                                log_Reports
                                );
                            if (!log_Reports.Successful)
                            {
                                // 既エラー
                                result = null;
                                goto gt_EndMethod;
                            }

                            if (isParsed2)
                            {
                                if (int_Expected == int_Exists)
                                {
                                    // 一致すれば。
                                    result = new Record_HumaninputImpl( log_Method.Fullname, dataRow, this);
                                    // 正常
                                    goto gt_EndMethod;
                                }
                            }
                        }
                    }
                    //}
                }
            }
            catch (Exception e)
            {
                // ArgumentException: 指定した名前の列がなかった場合など。
                err_Excp = e;
                result = null;
                goto gt_Error_Exception;
            }


            result = null;
            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Spaces:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー651！", log_Method);
                r.Message = "＜空白で中断＞";
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Invalid:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー652！", log_Method);
                r.Message = "＜エラーデータで中断＞";
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Exception:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー653！", log_Method);
                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append(r.Message_SException(err_Excp));

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
            return result;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 指定のstring型フィールドの値で指定したレコードを返します。なければヌルです。
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="expectedInt"></param>
        /// <returns>一致しなければヌル。</returns>
        public List<Record_Humaninput> SelectByString(
            string name_Field,
            StringCellImpl valuestring_Expected,
            EnumHitcount hits,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "SelectByString", log_Reports);
            //

            List<Record_Humaninput> list_Result = new List<Record_Humaninput>();

            string string_Expected;

            bool isParsed = StringCellImpl.TryParse(
                valuestring_Expected,
                out string_Expected,
                "",
                "",
                log_Method,
                log_Reports
                );
            if (!log_Reports.Successful || !isParsed)
            {
                // 既エラー
                goto gt_EndMethod;
            }


            Exception error_Excp;
            try
            {                
                foreach (DataRow dataRow in this.DataTable.Rows)
                {

                    Cell valueH = (Cell)dataRow[name_Field];

                    if (valueH is StringCellImpl)
                    {
                        StringCellImpl stringH = (StringCellImpl)valueH;

                        if (!stringH.IsValidated)
                        {
                            // エラーデータなら無視
                        }
                        else
                        {
                            string exists;

                            bool isParsed2 = StringCellImpl.TryParse(
                                stringH,
                                out exists,
                                "",
                                "",
                                log_Method,
                                log_Reports
                                );
                            if (!log_Reports.Successful)
                            {
                                // 既エラー
                                goto gt_EndMethod;
                            }

                            if (isParsed2)
                            {
                                if (string_Expected == exists)
                                {
                                    // 一致すれば。
                                    list_Result.Add(new Record_HumaninputImpl( log_Method.Fullname, dataRow, this));

                                    if (hits == EnumHitcount.First_Exist)
                                    {
                                        // 正常
                                        goto gt_EndMethod;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // ArgumentException: 指定した名前の列がなかった場合など。
                error_Excp = e;
                goto gt_Error_Exception;
            }

            goto gt_EndMethod;
            //
            //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Exception:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー653！", log_Method);
                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append(r.Message_SException(error_Excp));

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
            //
            //
        gt_EndMethod:
            if (hits == EnumHitcount.First_Exist && list_Result.Count != 1)
            {
                // 必ず存在する最初の１件を返さなければなりませんが、そうではありませんでした。
                if (log_Reports.CanCreateReport)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー654！", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append("必ず存在する最初の１件を返さなければなりませんが、そうではありませんでした。");
                    s.Newline();
                    s.Append("count=[");
                    s.Append(list_Result.Count);
                    s.Append("]");

                    r.Message = s.ToString();
                    log_Reports.EndCreateReport();
                }
            }
            log_Method.EndMethod(log_Reports);
            return list_Result;
        }

        //────────────────────────────────────────

        public void ForEach_Datapart(DELEGATE_Records delegate_Records, Log_Reports log_Reports )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "ForEach_Datapart", log_Reports);
            //

            bool isBreak = false;

            foreach (DataRow dataRow in this.DataTable.Rows)
            {
                delegate_Records(new Record_HumaninputImpl( "レコードをForEach("+log_Method.Fullname+")。ソース不明。", dataRow, this), ref isBreak, log_Reports);

                if (isBreak)
                {
                    break;
                }
            }

            //
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        /// <summary>
        /// 【2012-08-25 追加】
        /// </summary>
        /// <param name="name_Field"></param>
        /// <param name="isRequired"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public bool ContainsField(string name_Field, bool isRequired, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "ContainsField",log_Reports);

            bool isResult;

            if (this.DataTable.Columns.Contains(name_Field))
            {
                isResult = true;
            }
            else
            {
                isResult = false;

                if (isRequired)
                {
                    // エラー
                    goto gt_Error_NotFoundField;
                }
            }


            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NotFoundField:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー902！", log_Method);

                StringBuilder sb = new StringBuilder();

                sb.Append("[" + name_Field + "]フィールドは、[" + this.Name + "]には存在しませんでした。");
                sb.Append(Environment.NewLine);
                sb.Append("テーブル名＝[" + this.Name + "]");
                sb.Append(Environment.NewLine);
                sb.Append("データ・タイプ＝[" + this.Typedata + "]");

                r.Message = sb.ToString();
                log_Reports.EndCreateReport();
            }
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return isResult;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private RecordFielddef recordFielddef_;

        public RecordFielddef RecordFielddef
        {
            get
            {
                return this.recordFielddef_;
            }
        }

        //────────────────────────────────────────

        private Format_Table format_Table_Humaninput;

        /// <summary>
        /// テーブルの内容保存方法などの設定。
        /// </summary>
        public Format_Table Format_Table_Humaninput
        {
            get
            {
                return format_Table_Humaninput;
            }
            set
            {
                format_Table_Humaninput = value;
            }
        }

        //────────────────────────────────────────

        private DataTable dataTable;

        /// <summary>
        /// テーブルのデータ部。
        /// </summary>
        public DataTable DataTable
        {
            get
            {
                return dataTable;
            }
            set
            {
                dataTable = value;
            }
        }

        //────────────────────────────────────────

        private string name_Table;

        /// <summary>
        /// このテーブルの名前。なければ空文字列。
        /// </summary>
        public string Name_Table
        {
            get
            {
                return name_Table;
            }
            set
            {
                name_Table = value;

                // データテーブルに、テーブル名を上書きします。
                this.DataTable.TableName = name_Table;
            }
        }

        //────────────────────────────────────────

        private string tableunit;

        /// <summary>
        /// このテーブルの「テーブル_ユニット名」。なければ空文字列。
        /// </summary>
        public string Tableunit
        {
            get
            {
                return tableunit;
            }
            set
            {
                tableunit = value;
            }
        }

        //────────────────────────────────────────

        private string typedata;

        /// <summary>
        /// 「TYPE_DATA」フィールド値。
        /// 「T:～;」
        /// </summary>
        public string Typedata
        {
            get
            {
                return typedata;
            }
            set
            {
                typedata = value;
            }
        }

        //────────────────────────────────────────

        private bool isDatebackupActivated;

        /// <summary>
        /// 「日別バックアップ」を行うなら真。
        /// </summary>
        public bool IsDatebackupActivated
        {
            get
            {
                return isDatebackupActivated;
            }
            set
            {
                isDatebackupActivated = value;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// あれば、テーブルのファイル・パスなど。
        /// なければヌル可？
        /// </summary>
        private Expression_Node_Filepath expression_Filepath_ConfigStack;

        public Expression_Node_Filepath Expression_Filepath_ConfigStack
        {
            get
            {
                return expression_Filepath_ConfigStack;
            }
        }

        //────────────────────────────────────────
        #endregion



    }



}
