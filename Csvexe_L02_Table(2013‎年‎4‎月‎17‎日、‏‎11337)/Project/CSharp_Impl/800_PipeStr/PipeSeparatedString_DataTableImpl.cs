using System;
using System.Collections.Generic;
using System.Data;//DataRowView,DataTable
using System.Linq;
using System.Text;

using Xenon.Syntax;//HumanInputFilePath,WarningReports

namespace Xenon.Table
{
    /// <summary>
    /// 縦棒「 | 」区切り文字列。
    /// 
    /// DataTableを使った実装。
    /// 
    /// １つ目のトークンに「%1%」といった引数があると、
    /// ２つ目のトークンをフィールド名と見なして、レコードから値を取り出して入れます。
    /// </summary>
    public class PipeSeparatedStringImpl : PipeSeparatedString
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 例えば、次の２つの物を与えると、
        /// ●ID＝10、EXPL＝赤、と入っている行。
        /// ●「%1%:%2%|ID|EXPL」という文字列。
        /// 
        /// すると、次の文字列が返ってくる。
        /// ●「10:EXPL」
        /// 
        /// %1%はID、%2%はEXPLに当たる。
        /// </summary>
        /// <param name="sFormat"></param>
        /// <param name="dataRowView"></param>
        /// <param name="xenonTable"></param>
        /// <param name="sErrorMsg"></param>
        /// <returns></returns>
        public string Perform(
            string sFormat,
            DataRowView dataRowView,
            Table_Humaninput xenonTable,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "Perform",log_Reports);


            string result;

            if ( null==xenonTable)
            {
                // エラー
                // テーブルが未指定の場合

                goto gt_Error_NullTable;
            }


            CsvTo_DataTableImpl reader = new CsvTo_DataTableImpl();
            reader.CharSeparator = '|';
            DataTable scriptParameters = reader.Read(
                sFormat
                );


            if (scriptParameters.Rows.Count<1)
            {
                // 警告
                // 項目の表示書式が指定されていない場合
                //
                result = "(【Er:301;】表示書式未指定、レイアウト設定で)";

                // エラーにはしない。
                if (log_Method.CanWarning())
                {
                    log_Method.WriteWarning_ToConsole("(【Er:301;】表示書式未指定、レイアウト設定で)");
                }

                goto gt_EndMethod;
            }

            DataRow dataRow = scriptParameters.Rows[0];

            object[] recordFields = dataRow.ItemArray;// ItemArrayは1回の呼び出しが重い。



            Builder_TexttemplateP1pImpl formatString = new Builder_TexttemplateP1pImpl();
            formatString.Text = recordFields[0].ToString();//例："%1%:%2%"
            //.Console.WriteLine(this.GetType().Name + "#CreateText: recordFields[0].ToString()=[" + recordFields[0].ToString() + "]");

            FieldToParameters fieldToParameters = new FieldToParameters();
            Configurationtree_Node parent_Configurationtree_Node = new Configurationtree_NodeImpl("!ハードコーディング_RecordFormatStringImpl#CreateText",null);
            // フィールド名のリストが続く。
            for (int nIndex = 1; nIndex < recordFields.Length; nIndex++)
            {
                //.Console.WriteLine(this.GetType().Name + "#CreateText: index=[" + index + "] recordFields[index].ToString()=[" + recordFields[index].ToString() + "]");

                string sFieldName = recordFields[nIndex].ToString();
                fieldToParameters.AddField(
                    sFieldName,
                    xenonTable,
                    log_Reports
                    );

                if (!log_Reports.Successful)
                {
                    // 既エラー。
                    result = "（エラー）";
                    goto gt_EndMethod;
                }
            }

            fieldToParameters.Perform(ref formatString, dataRowView, xenonTable, log_Reports);
            if (!log_Reports.Successful)
            {
                // 既エラー。
                result = "（エラー）";
                goto gt_EndMethod;
            }

            result = formatString.Perform(log_Reports);

            
            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NullTable:
            result = "(テーブルが未指定です)";
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー131！", log_Method);
                r.Message = "(エラー。テーブルが未指定です）";
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
        #endregion



    }
}
