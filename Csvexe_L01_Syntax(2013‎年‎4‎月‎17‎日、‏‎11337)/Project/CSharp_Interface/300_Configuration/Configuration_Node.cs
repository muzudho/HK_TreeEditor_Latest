using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{



    public interface Configuration_Node
    {





        #region アクション
        //────────────────────────────────────────


        /// <summary>
        /// 位置型のパンくずリスト。
        /// この設定の書かれているファイル名、要素等を示す文字列。
        /// 
        /// 無限ループを防ぐために、このメソッドの中で参照できるのは、
        /// 親元のオブジェクトのみです。
        /// </summary>
        void ToText_Locationbreadcrumbs(Log_TextIndented s);

        /// <summary>
        /// 問題が起こったときに、設定ファイル等で、どのような内容だったのかを示す説明などに利用。
        /// 
        /// 無限ループを防ぐために、このメソッドの中では、
        /// 親を参照してはいけません。
        /// 
        /// 同じインスタンスの、ToText_Content の中で使うことができます。
        /// </summary>
        void ToText_Content(Log_TextIndented s);

        /// <summary>
        /// ノード名を指定して、直近の親ノードを取得します。
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        Configuration_Node GetParentByNodename(string sName, EnumConfiguration enumConf, bool bRequired, Log_Reports log_Reports);


        //────────────────────────────────────────
        #endregion




        #region プロパティー
        //────────────────────────────────────────


        /// <summary>
        /// 関数なら「Sf:Cell;」といった関数名。
        /// </summary>
        string Name
        {
            get;
        }


        /// <summary>
        /// 親。なければヌル。
        /// </summary>
        Configuration_Node Parent
        {
            get;
        }


        //────────────────────────────────────────
        #endregion



    }



}
