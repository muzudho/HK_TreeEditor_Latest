using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;//DataTable,DataRow

using Xenon.Syntax;

namespace Xenon.Table
{
    public class Utility_Table
    {



        #region 用意
        //────────────────────────────────────────

        public const string S_AND = "and";

        public const string S_OR = "or";

        public const string S_SYMBOL_EQ = "＝";

        public const string S_SYMBOL_GT = "＞";

        public const string S_SYMBOL_GTEQ = "≧";

        public const string S_SYMBOL_LT = "＜";

        public const string S_SYMBOL_LTEQ = "≦";

        public const string S_SYMBOL_NEQ = "≠";

        public const string S_FIELD_NO = "NO";

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public static EnumLogic LogicStringToEnum(string sLogic)
        {
            EnumLogic logic;

            switch (sLogic.Trim())
            {
                case Utility_Table.S_AND:
                    logic = EnumLogic.And;
                    break;

                case Utility_Table.S_OR:
                    logic = EnumLogic.Or;
                    break;

                default:
                    logic = EnumLogic.None;
                    break;
            }

            return logic;
        }

        //────────────────────────────────────────

        public static string LogicSymbolToString(EnumLogic logic)
        {
            string sLogic;

            switch (logic)
            {
                case EnumLogic.And:
                    sLogic = Utility_Table.S_AND;
                    break;

                case EnumLogic.Or:
                    sLogic = Utility_Table.S_OR;
                    break;

                case EnumLogic.None:
                    sLogic = "";
                    break;

                default:
                    sLogic = "(エラー101)";
                    break;
            }

            return sLogic;
        }

        //────────────────────────────────────────

        public static string OpeSymbolToString(EnumOpe ope)
        {
            string sOpe;

            switch (ope)
            {
                case EnumOpe.Eq:
                    sOpe = Utility_Table.S_SYMBOL_EQ;
                    break;

                case EnumOpe.Gt:
                    sOpe = Utility_Table.S_SYMBOL_GT;
                    break;

                case EnumOpe.Gteq:
                    sOpe = Utility_Table.S_SYMBOL_GTEQ;
                    break;

                case EnumOpe.Lt:
                    sOpe = Utility_Table.S_SYMBOL_LT;
                    break;

                case EnumOpe.Lteq:
                    sOpe = Utility_Table.S_SYMBOL_LTEQ;
                    break;

                case EnumOpe.Neq:
                    sOpe = Utility_Table.S_SYMBOL_NEQ;
                    break;

                default:
                    sOpe = "(エラー102)";
                    break;
            }

            return sOpe;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 指定のフィールド名だけに絞り込んだサブテーブルを作って返します。
        /// </summary>
        /// <param name="sFieldNameList"></param>
        /// <returns></returns>
        public static Table_Humaninput CreateSubTableBySelect(
            string name_NewTable,
            List<string> list_Src_SNewFieldName,
            Expression_Node_Filepath expr_Fpath_NewTable,
            EnumLogic enumWhereLogic,
            List<Recordcondition> list_Reccond,
            Table_Humaninput src_XenonTable,
            Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, "Util_Table", "CreateSubTableBySelect",log_Reports);

            //
            //
            //
            //



            Table_Humaninput tableH_New = new Table_HumaninputImpl(
                name_NewTable, expr_Fpath_NewTable, expr_Fpath_NewTable.Cur_Configuration);

            RecordFielddef recordFielddefinigion_New;
            List<List<string>> sFieldListList;
            Utility_Table.SelectFieldListList(
                out sFieldListList,
                out recordFielddefinigion_New,
                enumWhereLogic,
                list_Src_SNewFieldName,
                list_Reccond,
                src_XenonTable,
                log_Reports
                );




            //
            // 新しいテーブルを作成します。（列定義の追加）
            //
            tableH_New.CreateTable(recordFielddefinigion_New, log_Reports);

            if (tableH_New.DataTable.Columns.Count < 1)
            {
                // エラー。
                goto gt_Error_ZeroField;
            }
            else if (tableH_New.RecordFielddef.Count < 1)
            {
                // エラー。
                goto gt_Error_ZeroFieldDef;
            }



            // 不要なレコードを除去して絞り込んだ後で、
            // レコード追加。
            {
                tableH_New.AddRecordList(sFieldListList, recordFielddefinigion_New, log_Reports);
            }


            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_ZeroField:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー471！", log_Method);

                Log_TextIndented t = new Log_TextIndentedImpl();

                t.Append("　フィールドが０件のテーブルを作ることはできません。newFldDefList=[");
                t.Append(recordFielddefinigion_New.Count);
                t.Append("]");

                t.Newline();
                t.Newline();

                // ヒント

                r.Message = t.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_ZeroFieldDef:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー472！", log_Method);

                Log_TextIndented t = new Log_TextIndentedImpl();

                t.Append("　フィールド定義が０件のテーブルを作ることはできません。o_NewTable.FieldDefinitions.Count=[");
                t.Append(tableH_New.RecordFielddef.Count);
                t.Append("]");

                t.Newline();
                t.Newline();

                // ヒント

                r.Message = t.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return tableH_New;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        /// <param name="out_sFieldListList"></param>
        /// <param name="out_O_NewFldDefList"></param>
        /// <param name="src_sNewFieldNameList"></param>
        /// <param name="e_Where"></param>
        /// <param name="tableH_Source"></param>
        /// <param name="log_Reports"></param>
        public static void SelectFieldListList(
            out List<List<string>> listList_SField_Out,
            out RecordFielddef out_RecordFielddef_New,
            EnumLogic enumWhereLogic,
            List<string> list_SName_NewField_Src,
            List<Recordcondition> list_Reccond,
            Table_Humaninput tableH_Source,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, "Util_Table", "SelectFieldListList",log_Reports);

            //

            RecordFielddef recordFielddef_New = new RecordFielddefImpl();
            List<int> list_indexField = new List<int>();

            //
            // 新しい、列定義リストを作成します。
            //
            {
                // 最初の列は「NO」とします。
                list_SName_NewField_Src.Insert(0, Utility_Table.S_FIELD_NO);

                //fieldIndex
                int nFIx = 0;
                tableH_Source.RecordFielddef.ForEach(delegate(Fielddef fielddefinition, ref bool isBreak, Log_Reports log_Reports2)
                {
                    if (list_SName_NewField_Src.Contains(fielddefinition.Name_Humaninput))
                    {
                        // 選出されたフィールドだけでリストを作ります。
                        recordFielddef_New.Add(fielddefinition);
                        list_indexField.Add(nFIx);
                    }

                    nFIx++;
                }, log_Reports);
            }



            //
            // テーブルに列定義を設定した後で。
            // 移し替えたいデータ値の配列を作ります。
            listList_SField_Out = new List<List<string>>();
            //
            {
                int nEndover = list_indexField.Count;
                foreach (DataRow srcRow in tableH_Source.DataTable.Rows)
                {
                    List<string> sList_NewField = new List<string>();


                    //
                    // 「E■＠ｗｈｅｒｅ」属性を解析します。
                    //
                    // 該当しないレコードは除去していきます。
                    //
                    // 「E■＠ｗｈｅｒｅ」に logic属性が無い場合は logic="and" とします。
                    if (EnumLogic.None == enumWhereLogic)
                    {
                        enumWhereLogic = EnumLogic.And;
                    }
                    bool bHit = Utility_Table.ApplyReccond(srcRow, tableH_Source, enumWhereLogic, list_Reccond, 0, log_Reports);
                    //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.SelectFieldListList: (結果) [" + bHit + "]");

                    if (bHit)
                    {
                        for (int nA = 0; nA < nEndover; nA++)
                        {
                            // TODO:指定のフィールド・インデックスだけをピックアップしたい。
                            int nB = list_indexField[nA];
                            Cell o_Value = (Cell)srcRow[nB];

                            sList_NewField.Add(o_Value.Text);
                        }

                        listList_SField_Out.Add(sList_NewField);
                    }
                    //hit

                }

            }


            goto gt_EndMethod;

            //
            //
            //
            //
        gt_EndMethod:
            out_RecordFielddef_New = recordFielddef_New;
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 「E■＠ｗｈｅｒｅ」属性を解析します。
        /// 
        /// 該当しないレコードは除去していきます。
        /// </summary>
        /// <param name="srcRow"></param>
        /// <param name="tableH_Source"></param>
        /// <param name="groupLogic"></param>
        /// <param name="reccondList"></param>
        /// <param name="log_Reports"></param>
        /// <returns>ロジックの真偽。</returns>
        private static bool ApplyReccond(
            DataRow srcRow,
            Table_Humaninput tableH_Source,
            EnumLogic parent_EnumLogic,
            List<Recordcondition> list_Reccond,//「E■@ｗｈｅｒｅ」または「E■rec-cond」。子要素を持たないか、子要素に「E■rec-cond」を持つものとする。
            int nCount_Recursive_Debug,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, "Util_Table", "ApplyReccond",log_Reports);

            //
            //
            //
            //

            bool bResult;
            string err_SField;
            int nDebug_ChildIndex = 0;

            if (EnumLogic.Or == parent_EnumLogic)
            {
                // １つも真がなければ、偽。
                bResult = false;
            }
            else if (EnumLogic.And == parent_EnumLogic)
            {
                // １つも偽がなければ、真。
                bResult = true;
            }
            else
            {
                // 条件による。条件が１つもなければ真。
                bResult = true;
            }

            foreach (Recordcondition childReccond in list_Reccond)
            {

                //
                // "and" と有れば、条件に合わなかった場合 false。
                // "or" と有れば、
                //

                if (EnumLogic.None != childReccond.EnumLogic)
                {
                    // andグループ、orグループなら。

                    bool bChildHit = Utility_Table.ApplyReccond(srcRow, tableH_Source, childReccond.EnumLogic, childReccond.List_Child, nCount_Recursive_Debug + 1, log_Reports);

                    if (EnumLogic.And == parent_EnumLogic)
                    {
                        if (bChildHit)
                        {
                            // そのまま。
                            //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件内のグループで真 [" + bChildHit + "→真なのでそのまま " + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "] 子が真なので変化なし。");
                        }
                        else
                        {
                            // １つでも偽があれば、偽。
                            //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件内のグループで偽 [" + bChildHit + "→偽なので確定偽に " + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "] 子に偽があるので偽。");
                            bResult = false;
                            goto gt_EndMethod;
                        }
                    }
                    else if (EnumLogic.Or == parent_EnumLogic)
                    {
                        if (bChildHit)
                        {
                            // １つでも真があれば、真。
                            //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") OR条件内のグループで真 [" + bChildHit + "→真なので確定真に " + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "] 子に真があるので真。");
                            bResult = true;
                            goto gt_EndMethod;
                        }
                        else
                        {
                            // そのまま。
                            //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") OR条件内のグループで偽 [" + bChildHit + "→偽なのでそのまま "+ bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "] 子が偽なので変化なし。");
                        }
                    }
                    else
                    {
                        // #TODO:エラー
                        System.Console.WriteLine(Info_Table.Name_Library + ":Util_Table.ApplyＷｈｅｒｅ:　不明ロジック[" + parent_EnumLogic + "]");
                    }


                }
                else
                {
                    // 条件なら。



                    // このレコードについて判定。
                    if (!tableH_Source.DataTable.Columns.Contains(childReccond.Name_Field))
                    {
                        // エラー
                        err_SField = childReccond.Name_Field;
                        goto gt_Error_MissField;
                    }

                    int nFieldIx = tableH_Source.DataTable.Columns.IndexOf(childReccond.Name_Field);
                    Fielddef o_FldDef = tableH_Source.RecordFielddef.ValueAt(nFieldIx);
                    Cell o_Value = (Cell)srcRow[nFieldIx];


                    // 型に合わせて値取得。
                    if (o_Value is IntCellImpl)
                    {
                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyＷｈｅｒｅ:　intフィールド　[" + sLogic + " " + sField + " " + sOpe + " " + sValue + "]");

                        int nFieldInt;
                        {
                            IntCellImpl.TryParse(
                                o_Value,
                                out nFieldInt,
                                EnumOperationIfErrorvalue.Spaces_To_Alt_Value,
                                -1,
                                log_Reports
                                );
                        }

                        int nExpectedInt;
                        {
                            bool bHit2 = int.TryParse(childReccond.Value, out nExpectedInt);
                            if (!bHit2 && log_Reports.CanCreateReport)
                            {
                                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                                r.SetTitle("▲エラー502！", log_Method);
                                r.Message = "int型に変換できませんでした。sValue=[" + childReccond.Value + "]";
                                log_Reports.EndCreateReport();
                            }
                        }


                        if (EnumLogic.Or == parent_EnumLogic)
                        {
                            switch (childReccond.EnumOpe)
                            {
                                case EnumOpe.Gteq:
                                    // 「＞＝」

                                    if (nFieldInt >= nExpectedInt)
                                    {
                                        // セーフ

                                        // １つでも真が確定なら、真。
                                        bResult = true;
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ")  OR条件[" + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "]　１つでも真なら真。");
                                        goto gt_EndMethod;
                                    }
                                    else
                                    {
                                        // アウト

                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ")  OR条件[" + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "]");
                                    }
                                    break;

                                case EnumOpe.Gt:
                                    // 「＞」

                                    if (nFieldInt > nExpectedInt)
                                    {
                                        // セーフ

                                        // １つでも真が確定なら、真。
                                        bResult = true;
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ")  OR条件[" + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "]　１つでも真なら真。");
                                        goto gt_EndMethod;
                                    }
                                    else
                                    {
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ")  OR条件[" + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "]");
                                    }
                                    break;

                                case EnumOpe.Lteq:
                                    // 「＜＝」

                                    if (nFieldInt <= nExpectedInt)
                                    {
                                        // セーフ

                                        // １つでも真が確定なら、真。
                                        bResult = true;
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ")  OR条件[" + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "]　１つでも真なら真。");
                                        goto gt_EndMethod;
                                    }
                                    else
                                    {
                                        // アウト

                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ")  OR条件[" + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "]");
                                    }
                                    break;

                                case EnumOpe.Lt:
                                    // 「＜」

                                    if (nFieldInt < nExpectedInt)
                                    {
                                        // セーフ

                                        // １つでも真が確定なら、真。
                                        bResult = true;
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ")  OR条件[" + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "]　１つでも真なら真。");
                                        goto gt_EndMethod;
                                    }
                                    else
                                    {
                                        // アウト

                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ")  OR条件[" + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "]");
                                    }
                                    break;

                                case EnumOpe.Neq:
                                    // 「！＝」

                                    if (nFieldInt != nExpectedInt)
                                    {
                                        // セーフ

                                        // １つでも真が確定なら、真。
                                        bResult = true;
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ")  OR条件[" + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "]　１つでも真なら真。");
                                        goto gt_EndMethod;
                                    }
                                    else
                                    {
                                        // アウト

                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ")  OR条件[" + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "]");
                                    }
                                    break;

                                case EnumOpe.Eq:
                                    // ""、"eq"、は eq扱い。

                                    // 「＝」
                                    if (nFieldInt == nExpectedInt)
                                    {
                                        // セーフ

                                        // １つでも真が確定なら、真。
                                        bResult = true;
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ")  OR条件[" + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "]　１つでも真なら真。");
                                        goto gt_EndMethod;
                                    }
                                    else
                                    {
                                        // アウト

                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ")  OR条件[" + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "]");
                                    }
                                    break;
                            }

                        }
                        else if (EnumLogic.And == parent_EnumLogic)
                        {
                            switch (childReccond.EnumOpe)
                            {
                                case EnumOpe.Gteq:
                                    // 「＞＝」

                                    if (nFieldInt >= nExpectedInt)
                                    {
                                        // セーフ

                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件[" + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "]");
                                    }
                                    else
                                    {
                                        // アウト

                                        // １つでも偽が確定なら、偽。
                                        bResult = false;
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件[" + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "]　１つでも偽なら偽。");
                                        goto gt_EndMethod;
                                    }
                                    break;

                                case EnumOpe.Gt:
                                    // 「＞」

                                    if (nFieldInt > nExpectedInt)
                                    {
                                        // セーフ

                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件[" + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "]");
                                    }
                                    else
                                    {
                                        // アウト

                                        // １つでも偽が確定なら、偽。
                                        bResult = false;
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件[" + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "]　１つでも偽なら偽。");
                                        goto gt_EndMethod;
                                    }
                                    break;

                                case EnumOpe.Lteq:
                                    // 「＜＝」

                                    if (nFieldInt <= nExpectedInt)
                                    {
                                        // セーフ

                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件[" + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "]");
                                    }
                                    else
                                    {
                                        // アウト

                                        // １つでも偽が確定なら、偽。
                                        bResult = false;
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件[" + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "]　１つでも偽なら偽。");
                                        goto gt_EndMethod;
                                    }
                                    break;

                                case EnumOpe.Lt:
                                    // 「＜」

                                    if (nFieldInt < nExpectedInt)
                                    {
                                        // セーフ

                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件[" + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "]");
                                    }
                                    else
                                    {
                                        // アウト

                                        // １つでも偽が確定なら、偽。
                                        bResult = false;
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件[" + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "]　１つでも偽なら偽。");
                                        goto gt_EndMethod;
                                    }
                                    break;

                                case EnumOpe.Neq:
                                    // 「！＝」

                                    if (nFieldInt != nExpectedInt)
                                    {
                                        // セーフ

                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件[" + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "]");
                                    }
                                    else
                                    {
                                        // アウト

                                        // １つでも偽が確定なら、偽。
                                        bResult = false;
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件[" + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "]　１つでも偽なら偽。");
                                        goto gt_EndMethod;
                                    }
                                    break;

                                case EnumOpe.Eq:
                                    // ""、"eq"、は eq扱い。

                                    // 「＝」
                                    if (nFieldInt == nExpectedInt)
                                    {
                                        // セーフ

                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件[" + bResult + "=Ｔｒｕｅ] Reccond=[" + childReccond.ToString() + "]");
                                    }
                                    else
                                    {
                                        // アウト

                                        // １つでも偽が確定なら、偽。
                                        bResult = false;
                                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":Util_Table.ApplyRecord: (" + debug_RecursiveCount + "." + debug_ChildIndex + ") AND条件[" + bResult + "=Ｆａｌｓｅ] Reccond=[" + childReccond.ToString() + "]　１つでも偽なら偽。");
                                        goto gt_EndMethod;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            // #TODO:エラー
                            System.Console.WriteLine(Info_Table.Name_Library + ":Util_Table.ApplyＷｈｅｒｅ:　不明ロジック[" + parent_EnumLogic + "] nFieldIx=[" + nFieldIx + "] フィールド名=[" + o_FldDef.Name_Humaninput + "]　[" + parent_EnumLogic + "][" + childReccond.Name_Field + " " + childReccond.EnumOpe + " " + childReccond.Value + "] objValueの型＝[" + o_Value.GetType().Name + "]");
                        }


                    }
                    else
                    {
                        // #TODO:エラー
                        System.Console.WriteLine(Info_Table.Name_Library + ":Util_Table.ApplyＷｈｅｒｅ:　不明フィールド　nFieldIx=[" + nFieldIx + "] フィールド名=[" + o_FldDef.Name_Humaninput + "]　[" + parent_EnumLogic + "][" + childReccond.Name_Field + " " + childReccond.EnumOpe + " " + childReccond.Value + "] objValueの型＝[" + o_Value.GetType().Name + "]");

                    }


                }//or,and,条件


                nDebug_ChildIndex++;
            }//for


            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_MissField:
            if(log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー501！", log_Method);

                StringBuilder s = new StringBuilder();
                s.Append("＜ｒｅｃ－ｃｏｎｄ＞要素のfield属性エラー");
                s.Append(Environment.NewLine);
                s.Append("ｆｉｅｌｄ=[");
                s.Append(err_SField);
                s.Append("]");

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
            return bResult;
        }

        //────────────────────────────────────────
        #endregion



    }
}
