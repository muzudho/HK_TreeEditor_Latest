using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// コントローラーに登録される形で使われます。
    /// </summary>
    public interface Functionlist : Functionexecutable
    {



        #region 生成と破棄
        //────────────────────────────────────────

        void InitializeBeforeUse(object/*MemoryApplication*/ memoryApplication);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// どのように使われている？
        /// 
        /// ①Functionwrapper_OnFormImplの「Execute_～」の中でForEach。
        /// 
        /// ②Xn_L09_OpyopyoImpl:SToE_EventImpl#SToE の中でAdd。
        /// s_Actionを解析して、持っておく。
        /// 
        /// 旧名：List_Function
        /// </summary>
        List<Expression_Node_Function> List_Item
        {
            get;
        }


        //────────────────────────────────────────
        #endregion



    }
}
