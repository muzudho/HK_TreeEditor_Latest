using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    /// <summary>
    /// 関数。
    /// 
    /// 引数をセットし、実行することができます。
    /// </summary>
    public interface Expression_Node_Function : Expression_Node_String, Functionexecutable
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクターでは行わなかった初期値の設定を行います。
        /// </summary>
        /// <param name="parent_Expression"></param>
        /// <param name="cur_Conf"></param>
        /// <param name="memoryApplication"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        Expression_Node_Function NewInstance(
            Expression_Node_String parent_Expression,
            Configuration_Node cur_Conf,
            object/*MemoryApplication*/ memoryApplication,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────
        #endregion




        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// エラーレポート用です。
        /// 引数の名前をリストします。
        /// </summary>
        /// <param name="name_Argument"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        string ToString_ListNameargumentDefinition_ForReport();

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        /// <summary>
        /// エラーチェック用です。
        /// 引数の名前が、その関数の引数リスト定義に含まれているかどうかを判定します。
        /// </summary>
        /// <param name="name_Argument"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        bool ContainsName_ArgumentDefinition( string name_Argument, Log_Reports log_Reports );

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// Expression_Stringを関数として使うときの『ユーザー定義引数』のディクショナリー。
        /// </summary>
        Dictionary_Expression_Node_String Dictionary_Expression_Parameter
        {
            get;
            set;// 関数の引数を丸ごと渡す時に使う。
        }

        /// <summary>
        /// 呼び出されたイベントハンドラーの種類。
        /// </summary>
        EnumEventhandler EnumEventhandler
        {
            get;
            set;
        }

        Functionparameterset Functionparameterset
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
