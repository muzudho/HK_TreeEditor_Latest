using System;
using System.Collections.Generic;
using System.Data;//DataRowView
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{

    /// <summary>
    /// 
    /// </summary>
    public class FieldToParameters
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public FieldToParameters()
        {
            this.list_FieldKeies = new List<Fieldkey>();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// テーブルのフィールドを特定する情報。
        /// </summary>
        protected List<Fieldkey> list_FieldKeies;

        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namelist_Field"></param>
        /// <param name="value_TableH"></param>
        /// <param name="log_Reports"></param>
        public void AddField(
            string namelist_Field,
            Table_Humaninput value_TableH,
            Log_Reports log_Reports
            )
        {
            List<string> list_NameField = new CsvTo_ListImpl().Read(namelist_Field);
            RecordFielddef recordFielddef;

             bool bHit = value_TableH.TryGetFieldDefinitionByName(
                out recordFielddef,
                list_NameField,
                true,
                log_Reports
                );
            if (!log_Reports.Successful || !bHit)
            {
                // 既エラー。
                goto gt_EndMethod;
            }

            int nIx = 0;
            recordFielddef.ForEach(delegate(Fielddef fielddefinition, ref bool isBreak2, Log_Reports log_Reports2)
            {
                this.list_FieldKeies.Add(
                    new Fieldkey(list_NameField[nIx], fielddefinition.ToString_Type(), fielddefinition.Comment));

                nIx++;
            }, log_Reports);


            //
            //
            //
            //
        gt_EndMethod:
            return;
        }

        // ──────────────────────────────

        public void Perform(
            ref Builder_TexttemplateP1pImpl ref_FormatString,
            DataRowView dataRowView,
            Table_Humaninput xenonTable,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "Perform", log_Reports);

            // TODO IDは「前ゼロ付き文字列」または「int型」なので、念のため一度文字列に変換。
            int nP1pNumber = 1;
            foreach (Fieldkey fieldKey in list_FieldKeies)
            {
                //"[" + oTable.Name + "]テーブルの或る行の[" + fieldKey.Name + "]フィールド値。"//valueOTable.SourceFilePath.HumanInputText

                Cell valueH = Utility_Row.GetFieldvalue(
                    fieldKey.Name,
                    dataRowView.Row,
                    true,
                    log_Reports,
                    fieldKey.Description
                );
                if (!log_Reports.Successful)
                {
                    // 既エラー。
                    goto gt_EndMethod;
                }


                // 正常時
                EnumTypeFielddef typeFd = FielddefImpl.TypefieldFromString(fieldKey.Name_Type, true, log_Reports);
                switch (typeFd)
                {
                    case EnumTypeFielddef.String:
                        {
                            ref_FormatString.Dictionary_NumberAndValue_Parameter.Add(
                                nP1pNumber,
                                valueH.Text// String_HumaninputImpl.ParseString(valueH)
                                );
                        }
                        break;
                    case EnumTypeFielddef.Int:
                        {
                            ref_FormatString.Dictionary_NumberAndValue_Parameter.Add(
                                nP1pNumber,
                                valueH.Text// Int_HumaninputImpl.ParseString(valueH)
                                );
                        }
                        break;
                    case EnumTypeFielddef.Bool:
                        {
                            ref_FormatString.Dictionary_NumberAndValue_Parameter.Add(
                                nP1pNumber,
                                valueH.Text// Bool_HumaninputImpl.ParseString(valueH)
                                );
                        }
                        break;
                    default:
                        {
                            // 未定義の型は、string扱い。
                            ref_FormatString.Dictionary_NumberAndValue_Parameter.Add(
                                nP1pNumber,
                                valueH.Text// String_HumaninputImpl.ParseString(valueH)
                                );
                        }
                        break;
                }

                nP1pNumber++;
            }//foreach

            // 正常
            goto gt_EndMethod;
            //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return;
        }

        //────────────────────────────────────────
        #endregion



    }
}
