using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{


    /// <summary>
    /// インデント・レベルを設定可能なテキスト。
    /// </summary>
    public interface Log_TextIndented
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// インデントを１段深くします。
        /// </summary>
        void Increment();

        /// <summary>
        /// インデントを１段解除します。
        /// </summary>
        void Decrement();

        /// <summary>
        /// 改行を追加します。
        /// </summary>
        void Newline();

        /// <summary>
        /// インデントレベル指定付き。
        /// </summary>
        /// <param name="nCount_Indent"></param>
        /// <param name="obj"></param>
        void AppendI(int nCount_Indent, object obj);

        /// <summary>
        /// インデントレベル指定付き。
        /// </summary>
        /// <param name="nCount_Indent"></param>
        /// <param name="sValue"></param>
        void AppendI(int nCount_Indent, string sValue);

        /// <summary>
        /// インデントレベル指定付き。
        /// </summary>
        /// <param name="nCount_Indent"></param>
        /// <param name="nValue"></param>
        void AppendI(int nCount_Indent, int nValue);

        /// <summary>
        /// インデントレベル指定付き。
        /// </summary>
        /// <param name="nCount_Indent"></param>
        /// <param name="bValue"></param>
        void AppendI(int nCount_Indent, bool bValue);

        /// <summary>
        /// インデントレベル指定付き。
        /// </summary>
        /// <param name="nCount_Indent"></param>
        /// <param name="value"></param>
        void AppendI(int nCount_Indent, char value);

        /// <summary>
        /// インデントレベル指定付き。
        /// </summary>
        /// <param name="obj"></param>
        void Append(object obj);

        /// <summary>
        /// インデントレベル指定付き。
        /// </summary>
        /// <param name="sValue"></param>
        void Append(string sValue);

        /// <summary>
        /// インデントレベル指定付き。
        /// </summary>
        /// <param name="nValue"></param>
        void Append(int nValue);

        /// <summary>
        /// インデントレベル指定付き。
        /// </summary>
        /// <param name="bValue"></param>
        void Append(bool bValue);

        /// <summary>
        /// インデントレベル指定付き。
        /// </summary>
        /// <param name="value"></param>
        void Append(char value);

        /// <summary>
        /// テキスト。
        /// </summary>
        /// <returns></returns>
        string ToString();

        //────────────────────────────────────────
        #endregion



    }
}
