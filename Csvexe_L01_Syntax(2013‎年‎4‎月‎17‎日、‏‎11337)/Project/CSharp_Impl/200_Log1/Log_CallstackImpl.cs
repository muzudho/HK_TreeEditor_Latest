using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{

    /// <summary>
    /// コールスタック。
    /// 
    /// 呼び出した関数名をスタックに積んでいく。
    /// </summary>
    public class Log_CallstackImpl : Log_Callstack
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Log_CallstackImpl()
        {
            this.stack = new Stack<Log_RecordCallstack>();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public void Push(Log_Method log_Method)
        {
            Log_RecordCallstack log_RecordCallstack = new Log_CallstackRecordImpl();
            log_RecordCallstack.Log_Method = log_Method;
            this.stack.Push(log_RecordCallstack);
        }

        public void Push(Log_Method log_Method, string sStatementComment)
        {
            Log_RecordCallstack log_RecordCallstack = new Log_CallstackRecordImpl();
            log_RecordCallstack.Log_Method = log_Method;
            log_RecordCallstack.Comment_Statement = sStatementComment;
            this.stack.Push(log_RecordCallstack);
        }

        //────────────────────────────────────────

        public void Pop(Log_Method log_Method)
        {
            Log_RecordCallstack err_Log_RecordCallstack;
            if (0 < this.stack.Count)
            {
                Log_RecordCallstack log_RecordCallstack = this.stack.Pop();

                if (!log_RecordCallstack.Log_Method.Equals(log_Method))
                {
                    // エラー
                    err_Log_RecordCallstack = log_RecordCallstack;
                    goto gt_Error_MissmatchPop;
                }
            }
            else
            {
                // エラー
                goto gt_Error_EmptyPop;
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_MissmatchPop:
            {
                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("最後にPushしたものに一致しないものをPopしました。");
                s.Newline();
                s.Append("今回Popした問題場所　：　今回Popしたもの");
                s.Newline();
                s.Append("定義SLibraryName  [" + log_Method.Name_Library + "]　：　[" + err_Log_RecordCallstack.Log_Method.Name_Library + "]");
                s.Newline();
                s.Append("呼出SClassName    [" + log_Method.Name_Class + "]　：　[" + err_Log_RecordCallstack.Log_Method.Name_Class + "]");
                s.Newline();
                s.Append("BStatic       [" + log_Method.IsStatic + "]　：　[" + err_Log_RecordCallstack.Log_Method.IsStatic + "]");
                s.Newline();
                s.Append("SMethodName   [" + log_Method.Name_Method + "]　：　[" + err_Log_RecordCallstack.Log_Method.Name_Method + "]");
                s.Newline();
                s.Append("もしかして？");
                s.Newline();
                s.Append("　　・EndMethod()が飛ばされた？　関数の途中でreturnやExceptionで抜けた場合や、EndMethodの記述漏れ？");
                s.Newline();

                //throw new Exception(s.ToString());
            }
            goto gt_EndMethod;
        gt_Error_EmptyPop:
            {
                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("PushしたものがないのにPopしました。");
                s.Newline();
                s.Append("今回Popした問題場所");
                s.Newline();
                s.Append("定義SLibraryName  [" + log_Method.Name_Library + "]");
                s.Newline();
                s.Append("呼出SClassName    [" + log_Method.Name_Class + "]");
                s.Newline();
                s.Append("BStatic       [" + log_Method.IsStatic + "]");
                s.Newline();
                s.Append("SMethodName   [" + log_Method.Name_Method + "]");
                s.Newline();

                throw new Exception(s.ToString());
            }
            //goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            ;
        }

        public void Pop(Log_Method log__Method, string sStatementComment)
        {
            Log_RecordCallstack err_Log_RecordCallstack;
            if (0 < this.stack.Count)
            {
                Log_RecordCallstack log_RecordCallstack = this.stack.Pop();

                if (
                    !log_RecordCallstack.Log_Method.Equals(log__Method) ||
                    log_RecordCallstack.Comment_Statement != sStatementComment
                    )
                {
                    // エラー
                    err_Log_RecordCallstack = log_RecordCallstack;
                    goto gt_Error_MissmatchPop;
                }
            }
            else
            {
                // エラー
                goto gt_Error_EmptyPop;
            }
            goto gt_EndMethod;
            //
            //
            #region 異常系
            //────────────────────────────────────────
        gt_Error_MissmatchPop:
            {
                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("最後にPushしたものに一致しないものをPopしました。");
                s.Newline();
                s.Append("今回Popした問題場所　：　今回Popしたもの");
                s.Newline();
                s.Append("定義SLibraryName  [" + log__Method.Name_Library + "]　：　[" + err_Log_RecordCallstack.Log_Method.Name_Library + "]");
                s.Newline();
                s.Append("呼出SClassName    [" + log__Method.Name_Class + "]　：　[" + err_Log_RecordCallstack.Log_Method.Name_Class + "]");
                s.Newline();
                s.Append("BStatic       [" + log__Method.IsStatic + "]　：　[" + err_Log_RecordCallstack.Log_Method.IsStatic + "]");
                s.Newline();
                s.Append("SMethodName   [" + log__Method.Name_Method + "]　：　[" + err_Log_RecordCallstack.Log_Method.Name_Method + "]");
                s.Newline();
                s.Append("もしかして？");
                s.Newline();
                s.Append("　　・関数の途中でreturnやExceptionで抜け、EndMethod()が飛ばされた？");
                s.Newline();

                throw new Exception(s.ToString());
            }
            //────────────────────────────────────────
        gt_Error_EmptyPop:
            {
                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("PushしたものがないのにPopしました。");
                s.Newline();
                s.Append("今回Popした問題場所");
                s.Newline();
                s.Append("定義SLibraryName  [" + log__Method.Name_Library + "]");
                s.Newline();
                s.Append("呼出SClassName    [" + log__Method.Name_Class + "]");
                s.Newline();
                s.Append("BStatic       [" + log__Method.IsStatic + "]");
                s.Newline();
                s.Append("SMethodName   [" + log__Method.Name_Method + "]");
                s.Newline();

                throw new Exception(s.ToString());
            }
            //────────────────────────────────────────
            #endregion
            //
            //
        gt_EndMethod:
            ;
        }

        //────────────────────────────────────────

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (this.stack.Count < 1)
            {
                sb.Append("（コールスタックは空です　／　デバッグモードでないのかもしれません）");
            }

            foreach (Log_RecordCallstack d_Record in this.stack.Reverse())//this.stack
            {
                sb.Append(d_Record.ToText_Pathbreadcrumbs());
            }

            return sb.ToString();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// メソッドや、メソッド内の文の位置を特定できる情報を積んでいく。
        /// </summary>
        private Stack<Log_RecordCallstack> stack;

        //────────────────────────────────────────
        #endregion



    }
}
