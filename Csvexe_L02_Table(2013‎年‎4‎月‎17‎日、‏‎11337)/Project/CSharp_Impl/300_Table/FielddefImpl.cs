using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{



    /// <summary>
    /// フィールド定義。
    /// 旧名：FielddefImpl
    /// </summary>
    public class FielddefImpl : Fielddef
    {



        #region 用意
        //────────────────────────────────────────

        public const string S_STRING = "string";

        public const string S_INT = "int";

        public const string S_BOOL = "bool";

        //────────────────────────────────────────
        #endregion



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="name_humanInput"></param>
        /// <param name="typeField">string,int,boolに対応。</param>
        public FielddefImpl(string name_Humaninput, EnumTypeFielddef typeField)
        {
            this.Name_Humaninput = name_Humaninput;
            this.Type_Field = typeField;
            this.comment = "";
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public static EnumTypeFielddef TypefieldFromString(
            string name_Typefield,
            bool isRequired,
            Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, "FielddefImpl", "TypefieldFromString", log_Reports);

            EnumTypeFielddef result;

            switch(name_Typefield)
            {
                case FielddefImpl.S_STRING:
                    {
                        result = EnumTypeFielddef.String;
                    }
                    break;
                case FielddefImpl.S_INT:
                    {
                        result = EnumTypeFielddef.Int;
                    }
                    break;
                case FielddefImpl.S_BOOL:
                    {
                        result = EnumTypeFielddef.Bool;
                    }
                    break;
                default:
                    {
                        //文字列型にしておく。
                        result = EnumTypeFielddef.String;

                        if (isRequired)
                        {
                            //エラー
                            goto gt_Error_Unspported;
                        }
                    }
                    break;
            }

            goto gt_EndMethod;
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Unspported:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー471！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();

                s.Append("指定された文字列[");
                s.Append(name_Typefield);
                s.Append("]は、サポートされていないデータベースの列の型でした。");
                s.Newline();

                s.Append("サポートされている型は、[");
                s.Append(FielddefImpl.S_STRING);
                s.Append("],[");
                s.Append(FielddefImpl.S_INT);
                s.Append("],[");
                s.Append(FielddefImpl.S_BOOL);
                s.Append("]のいずれかです。");
                s.Newline();

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
        return result;
        }

        //────────────────────────────────────────

        public Cell NewField(string nodeConfigtree, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Table.Name_Library, this, "NewField", log_Reports);

            Cell result;

            switch (this.Type_Field)
            {
                case EnumTypeFielddef.String:
                    {
                        result = new StringCellImpl(nodeConfigtree);
                    }
                    break;
                case EnumTypeFielddef.Int:
                    {
                        result = new IntCellImpl(nodeConfigtree);
                    }
                    break;
                case EnumTypeFielddef.Bool:
                    {
                        result = new BoolCellImpl(nodeConfigtree);
                    }
                    break;
                default:
                    {
                        // 未該当
                        result = null;
                        goto gt_Error_Unspported;
                    }
            }

            goto gt_EndMethod;
            //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Unspported:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー464！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();

                s.Append("フィールド定義を元にして、フィールド値を用意しようとしましたが、未定義のフィールド型でした。");
                s.Newline();

                s.Append("this.Type.ToString()=[");
                s.Append(this.ToString_Type());
                s.Append("]");
                s.Newline();

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────

        /// <summary>
        /// string,int,boolを返します。未該当の時は空文字列を返します。
        /// </summary>
        /// <returns></returns>
        public string ToString_Type()
        {
            string result;

            switch(this.Type_Field)
            {
                case EnumTypeFielddef.String:
                    {
                        result = FielddefImpl.S_STRING;
                    }
                    break;
                case EnumTypeFielddef.Int:
                    {
                        result = FielddefImpl.S_INT;
                    }
                    break;
                case EnumTypeFielddef.Bool:
                    {
                        result = FielddefImpl.S_BOOL;
                    }
                    break;
                default:
                    {
                        // 未該当
                        result = "";
                    }
                    break;
            }

            return result;
        }

        /// <summary>
        /// string,int,boolを返します。未該当の時はヌルを返します。
        /// </summary>
        /// <returns></returns>
        public Type ToType_Field()
        {
            if (this.Type_Field == EnumTypeFielddef.String)
            {
                return typeof(StringCellImpl);
            }
            else if (this.Type_Field == EnumTypeFielddef.Int)
            {
                return typeof(IntCellImpl);
            }
            else if (this.Type_Field == EnumTypeFielddef.Bool)
            {
                return typeof(BoolCellImpl);
            }
            else
            {
                // todo:エラー
                //
                // 未該当
                //
                return null;
            }
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 入力したままのフィールド名。
        /// </summary>
        private string name_Humaninput;

        /// <summary>
        /// トリムして大文字化したフィールド名。
        /// </summary>
        private string name_Trimupper;

        /// <summary>
        /// フィールドの名前。入力したままの文字列。
        /// </summary>
        public string Name_Humaninput
        {
            set
            {
                name_Humaninput = value;
                this.name_Trimupper = name_Humaninput.Trim().ToUpper();
            }
            get
            {
                return name_Humaninput;
            }
        }

        /// <summary>
        /// フィールドの名前。トリムして英字大文字に加工した文字列。読み取り専用。
        /// </summary>
        public string Name_Trimupper
        {
            get
            {
                return this.name_Trimupper;
            }
        }

        //────────────────────────────────────────

        private EnumTypeFielddef type_Field;

        /// <summary>
        /// フィールドの型。
        /// </summary>
        public EnumTypeFielddef Type_Field
        {
            get
            {
                return type_Field;
            }
            set
            {
                type_Field = value;
            }
        }

        //────────────────────────────────────────

        private string comment;

        /// <summary>
        /// フィールドについてのコメント。
        /// </summary>
        public string Comment
        {
            set
            {
                comment = value;
            }
            get
            {
                return comment;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
