using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Table
{

    /// <summary>
    /// 旧名：AbstractValue_HumaninputImpl
    /// </summary>
    abstract public class AbstractCellImpl : Configurationtree_NodeImpl, Cell
    {



        #region 生成と破棄
        //────────────────────────────────────────        

        public AbstractCellImpl(string nodeConfigtree)
            : base(nodeConfigtree, null)
        {
            this.isSpaced = true;
            this.text = "";
        }

        //────────────────────────────────────────        
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// デバッグ用情報。
        /// </summary>
        abstract public override void ToText_Content(Log_TextIndented txt);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 入力データそのままの形。
        /// ・派生クラスでセット使用。
        /// </summary>
        protected string text;

        /// <summary>
        /// 入力データそのままの形。
        /// </summary>
        public virtual string Text
        {
            get
            {
                return text;
            }
            set
            {
                if ("" == value.Trim())
                {
                    isSpaced = true;
                }
                else
                {
                    isSpaced = false;
                }

                isValidated = true;
                this.text = value;
            }
        }

        //────────────────────────────────────────        

        /// <summary>
        /// 文字列データを int型や bool型などに変換済みなら真、
        /// できていないなら偽。
        /// 空白は真。
        /// ・派生クラスでセット使用。
        /// </summary>
        protected bool isValidated;
        public bool IsValidated
        {
            get
            {
                return isValidated;
            }
        }

        //────────────────────────────────────────        

        /// <summary>
        /// 空欄なら真。
        /// 
        /// 空白のみの場合、真。
        /// 空白以外の文字が含まれていれば偽。
        /// ・派生クラスでセット使用。
        /// </summary>
        protected bool isSpaced;
        public bool IsSpaces()
        {
            return isSpaced;
        }

        //────────────────────────────────────────        
        #endregion



    }
}
