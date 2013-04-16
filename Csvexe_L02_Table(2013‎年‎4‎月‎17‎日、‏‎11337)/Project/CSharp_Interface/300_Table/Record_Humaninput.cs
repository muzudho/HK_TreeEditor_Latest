using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Xenon.Syntax;

namespace Xenon.Table
{



    #region 用意
    //────────────────────────────────────────

    public delegate void DELEGATE_Fields(Cell field, ref bool isBreak, Log_Reports log_Reports);

    //────────────────────────────────────────
    #endregion



    public interface Record_Humaninput : Configuration_Node
    {



        #region アクション
        //────────────────────────────────────────
        //
        // テーブル改造
        //

        /// <summary>
        /// 指定のフィールドから左を、全て右に１列分ずらします。一番右の列は無くなります。
        /// </summary>
        /// <param name="columnIndex"></param>
        void Insert(int columnIndex, Cell valueH, Log_Reports log_Reports);

        //────────────────────────────────────────
        //
        //
        //

        void ForEach(DELEGATE_Fields delegate_Fields, Log_Reports log_Reports);

        //────────────────────────────────────────

        /// <summary>
        /// 配列の要素を取得します。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Cell ValueAt(int index);

        /// <summary>
        /// 配列の要素を取得します。
        /// </summary>
        /// <param name="name_Field"></param>
        /// <returns></returns>
        Cell ValueAt(string name_Field);
        
        /// <summary>
        /// 配列の要素をテキストとして取得します。
        /// </summary>
        /// <param name="name_Field"></param>
        /// <returns></returns>
        string TextAt(string name_Field);

        /// <summary>
        /// 配列の要素を取得します。
        /// </summary>
        /// <param name="index"></param>
        void SetValueAt(int index, Cell valueH, Log_Reports log_Reports );

        /// <summary>
        /// 配列の要素を取得します。
        /// </summary>
        /// <param name="name_Field"></param>
        void SetValueAt(string name_Field, Cell valueH, Log_Reports log_Reports );

        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <returns>該当がなければ -1。</returns>
        int ColumnIndexOf_Trimupper(string expected);

        //────────────────────────────────────────
        
        /// <summary>
        /// デバッグ用に内容をダンプします。
        /// </summary>
        /// <returns></returns>
        string ToString_DebugDump();

        //────────────────────────────────────────

        void AddTo(Table_Humaninput tableH);

        void RemoveFrom(Table_Humaninput tableH);

        //────────────────────────────────────────
        #endregion



        //#region プロパティー
        ////────────────────────────────────────────

        ///// <summary>
        ///// テーブルのコンフィグ記述場所情報。
        ///// </summary>
        //Configuration_Node Configuration_Node
        //{
        //    get;
        //    set;
        //}

        ////────────────────────────────────────────
        //#endregion



    }



}
