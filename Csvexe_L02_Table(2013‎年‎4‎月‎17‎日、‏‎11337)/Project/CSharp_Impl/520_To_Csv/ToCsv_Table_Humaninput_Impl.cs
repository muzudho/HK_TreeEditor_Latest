using System;
using System.Collections.Generic;
using System.Data;//DataTable
using System.Linq;
using System.Text;

using Xenon.Syntax;//WarningReports



namespace Xenon.Table
{
    public class ToCsv_Table_Humaninput_Impl
    {



        #region 初期化
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public ToCsv_Table_Humaninput_Impl()
        {
            this.exceptedFields = new ExceptedFields();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// テーブルを、SRS形式のCSVファイルで書き出します。
        /// 
        /// 
        /// SRS仕様の実装状況
        /// ここでは、先頭行を0行目と数えるものとします。
        /// (1)CSVの1行目は列名です。
        /// (2)2行目は型名です。
        /// (3)3行目はコメントです。
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
        public string ToCsvText(
            Table_Humaninput tableH,
            Log_Reports log_Reports
            )
        {
            string sResult;

            if (tableH.Format_Table_Humaninput.IsRowcolumnreverse)
            {
                //
                // 行と列が逆になっているテーブル
                //

                ToCsv_Table_Humaninput_RowColReversedImpl toCsv_RowColReversed = new ToCsv_Table_Humaninput_RowColReversedImpl();
                toCsv_RowColReversed.O_ExceptedFields = this.ExceptedFields;

                sResult = toCsv_RowColReversed.ToCsvText(tableH, log_Reports);
                if (!log_Reports.Successful)
                {
                    // 既エラー
                    goto gt_EndMethod;
                }
            }
            else
            {
                ToCsv_Table_Humaninput_RowColRegularImpl toCsv_Normal = new ToCsv_Table_Humaninput_RowColRegularImpl();
                toCsv_Normal.ExceptedFields = this.ExceptedFields;

                sResult = toCsv_Normal.ToCsvText(tableH, log_Reports);
                if (!log_Reports.Successful)
                {
                    // 既エラー
                    goto gt_EndMethod;
                }
            }

            goto gt_EndMethod;

            //
        //
        //
        //
        gt_EndMethod:
            return sResult;
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
        #endregion


         
    }
}
