using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Xenon.Syntax
{
    /// <summary>
    /// 「%1%:%2%」といった文字列（ピーワンピー_テキスト）をセットしておき、
    /// リスト[1]=101
    /// リスト[2]=赤
    /// といったリスト（parametersリストと呼ぶ）を渡した後、
    /// Performすると、
    /// 「101:赤」
    /// といった文字列を返すクラスです。
    /// 
    /// [使い方]
    /// (1)newします。
    /// (2)Dictionary_NumberAndValue_Parameterに値（例：%1%="aaaa"）を追加します。
    /// (3)Text（テンプレート）を設定します。
    /// (4)Compileすると、Expression_Node_Stringを作ることができます。
    /// 
    /// 旧名：TextP1pImpl
    /// </summary>
    public class Builder_TexttemplateP1pImpl : Builder_TexttemplateP1p
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Builder_TexttemplateP1pImpl()
        {
            this.text = "";

            this.dictionary_NumberAndValue_Parameter = new Dictionary<int, string>();
        }

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Builder_TexttemplateP1pImpl(string text)
        {
            this.text = text;

            this.dictionary_NumberAndValue_Parameter = new Dictionary<int, string>();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// [1]=101
        /// [2]=赤
        /// といったディクショナリー。
        /// 
        /// キーは %1%や、%2%といった名前の中の数字。[1]から始める。
        /// Xn_L05_E:E_FtextTemplate#E_ExecuteでAddされます。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="log_Reports"></param>
        public void SetParameter(int key, string value, Log_Reports log_Reports)
        {
            this.dictionary_NumberAndValue_Parameter.Add(key, value);
        }

        public void TryGetParameter(out string out_Value, int key, Log_Reports log_Reports)
        {
            out_Value = this.dictionary_NumberAndValue_Parameter[key];
        }

        /// <summary>
        /// 登録されている「%1%」、「%2%」といった記号の数字を一覧します。
        /// リストに「1」、「2」といった数字に置き換えて返します。
        /// </summary>
        /// <returns></returns>
        public List<int> ExistsP1pNumbers(
            Dictionary_Expression_Node_String ecDic_Attr,
            Log_Reports log_Reports
            )
        {

            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "GetP1pNumbers",log_Reports);

            //
            //
            //
            //

            List<int> list = new List<int>();


            Dictionary<string,Expression_Node_String>.KeyCollection ecDic_Key = ecDic_Attr.Keys(log_Reports);

            foreach (string sKey in ecDic_Key)
            {
                //
                //
                //
                // p1p,p2p,p3p...といった名前かどうかを判定。
                //
                //
                //
                int nParamNameMatchedCount = 0;
                int nP1pNumber = 0;
                {
                    //正規表現
                    System.Text.RegularExpressions.Regex regexp =
                        new System.Text.RegularExpressions.Regex(
                            @"p([0-9])+p",
                        //                            @"p[0-9]+p",
                            System.Text.RegularExpressions.RegexOptions.IgnoreCase
                            );

                    //文字列検索を1回する。
                    System.Text.RegularExpressions.Match match = regexp.Match(sKey);

                    while (match.Success)
                    {
                        nParamNameMatchedCount++;

                        bool parsedSuccessful = int.TryParse( match.Groups[1].Value, out nP1pNumber);

                        match = match.NextMatch();
                    }
                }

                if (1 == nParamNameMatchedCount)
                {
                    //
                    //
                    //
                    // p1p,p2p,p3p...といった名前。
                    //
                    //
                    //
                    list.Add(nP1pNumber);
                }
                else
                {
                }
            }


            //
            //
            log_Method.EndMethod(log_Reports);
            return list;
        }

        //────────────────────────────────────────

        public Expression_Node_String Compile(
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Compile",log_Reports);
            //

            String sTxtTmpl = this.Text;




            Configurationtree_Node parent_Cnf = new Configurationtree_NodeImpl("!ハードコーディング_TextTemplateImpl#Compile", null);
            Expression_Node_StringImpl result = new Expression_Node_StringImpl(null,parent_Cnf);

            int nCur = 0;

            while (nCur < sTxtTmpl.Length)
            {
                int nPreCur = nCur;

                // 開き%記号（open percent）
                int nOp = sTxtTmpl.IndexOf('%', nCur);

                if (nOp != -1)
                {
                    // 開き%記号があった。

                    nCur = nOp + 1;//「開き%」の次へ。

                    // 閉じ%記号（close percent）
                    int cp = sTxtTmpl.IndexOf('%', nCur);

                    if (cp != -1)
                    {
                        // 閉じ%記号があれば。

                        nCur = cp + 1;//「閉じ%」の次へ。

                        // 「%」と「%」の間に数字があるはず。
                        // 「開き%」の次から、「閉じ% - 開き% - 1」文字分。（-1しないと、終了%を含んでしまう）
                        string sMarker = sTxtTmpl.Substring(nOp + 1, cp - nOp - 1);


                        // 「%1%」といった記号の数字部分。
                        int nParameterIndex;


                        try
                        {
                            nParameterIndex = Int32.Parse(sMarker);


                            // 開き「%」までを、まず文字列化。
                            int nPreLen = nOp - nPreCur;
                            string sPre = sTxtTmpl.Substring(nPreCur, nPreLen);
                            result.AppendTextNode(
                                sPre,
                                parent_Cnf,
                                log_Reports
                                );



                            // 引数から値を取得。

                            // %数字%を、Expression化して追加。
                            Expression_TexttemplateP1pImpl expr_P1p = new Expression_TexttemplateP1pImpl(null,parent_Cnf);
                            expr_P1p.NumberP1p = nParameterIndex;
                            expr_P1p.Dictionary_P1p = this.Dictionary_NumberAndValue_Parameter;

                            result.List_Expression_Child.Add(
                                expr_P1p,
                                log_Reports
                                );

                            // 続行。
                        }
                        catch (Exception)
                        {
                            // 数字でないようなら。

                            // 今回の判定は失敗したものとして、残りの長さ全て
                            int nRestLen = sTxtTmpl.Length - nPreCur;

                            result.AppendTextNode(
                                sTxtTmpl.Substring(nPreCur, nRestLen),
                                parent_Cnf,
                                log_Reports
                                );


                            nCur = sTxtTmpl.Length;//終了（最後の文字の次へカーソルを出す）
                        }
                    }
                    else
                    {
                        // 閉じ%がなければ。

                        // 今回の判定は失敗したものとして、残りの長さ全て
                        int nRestLen = sTxtTmpl.Length - nPreCur;

                        result.AppendTextNode(
                            sTxtTmpl.Substring(nPreCur, nRestLen),
                            parent_Cnf,
                            log_Reports
                            );

                        nCur = sTxtTmpl.Length;//終了（最後の文字の次へカーソルを出す）
                    }
                }
                else
                {
                    // 開き%がなければ。

                    // 残りの長さ全て
                    int nRestLen = sTxtTmpl.Length - nCur;

                    result.AppendTextNode(
                        sTxtTmpl.Substring(nCur, nRestLen),
                        parent_Cnf,
                        log_Reports
                        );

                    nCur = sTxtTmpl.Length;//終了（最後の文字の次へカーソルを出す）
                }
            }


            //
            //
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────

        public String Perform(Log_Reports log_Reports)
        {
            String sTxtTmpl = this.Text;

            StringBuilder sb = new StringBuilder();

            int nCur = 0;

            while (nCur < sTxtTmpl.Length)
            {
                if (!log_Reports.Successful)
                {
                    //エラー時は抜ける。
                    goto gt_EndMethod;
                }

                int nPreCur = nCur;
                //.WriteLine(this.GetType().Name + "#Perform: ループ開始 cur＝[" + cur + "] preCur＝[" + preCur + "] txtTmpl.Length＝[" + txtTmpl.Length + "]");

                // 開き%記号（open percent）
                int nOp = sTxtTmpl.IndexOf('%', nCur);
                //.WriteLine(this.GetType().Name + "#Perform: op＝[" + op + "]");
                if (nOp != -1)
                {
                    // 開き%記号があった。
                    //.WriteLine(this.GetType().Name + "#Perform: 開き%記号があった。");

                    nCur = nOp+1;//「開き%」の次へ。
                    //.WriteLine(this.GetType().Name + "#Perform: cur＝[" + cur + "]");

                    // 閉じ%記号（close percent）
                    int nCp = sTxtTmpl.IndexOf('%', nCur);
                    //.WriteLine(this.GetType().Name + "#Perform: cp＝[" + cp + "]");

                    if (nCp != -1)
                    {
                        // 閉じ%記号があれば。
                        //.WriteLine(this.GetType().Name + "#Perform: 閉じ%記号があった。");

                        nCur = nCp+1;//「閉じ%」の次へ。
                        //.WriteLine(this.GetType().Name + "#Perform: cur＝[" + cur + "]");

                        // 「%」と「%」の間に数字があるはず。
                        // 「開き%」の次から、「閉じ% - 開き% - 1」文字分。（-1しないと、終了%を含んでしまう）
                        string sMarker = sTxtTmpl.Substring(nOp+1, nCp - nOp -1);

                        //.WriteLine(this.GetType().Name + "#Perform: marker＝[" + marker + "]");
                        int nParameterIndex;

                        try
                        {
                            nParameterIndex = Int32.Parse(sMarker);
                            //.WriteLine(this.GetType().Name + "#Perform: parameterIndex＝[" + parameterIndex + "]");


                            // %数字%を、引数の内容に置換。
                            //.WriteLine(this.GetType().Name + "#Perform: %数字%を、引数の内容に置換。");


                            // 開き「%」までを、まず文字列化。
                            int nPreLen = nOp - nPreCur;
                            string sPre = sTxtTmpl.Substring(nPreCur, nPreLen);
                            sb.Append(sPre);


                            //string paramValue = this.Parameters[parameterIndex];
                            string sParamValue;
                            //sParamValue = this.Dictionary_NumberAndValue_Parameter[nParameterIndex];
                            this.TryGetParameter(out sParamValue, nParameterIndex, log_Reports);

                            sb.Append(sParamValue);
                        }
                        catch (Exception)
                        {
                            // 数字でないようなら。

                            // 今回の判定は失敗したものとして、残りの長さ全て
                            int nRestLen = sTxtTmpl.Length - nPreCur;
                            sb.Append(sTxtTmpl.Substring(nPreCur, nRestLen));
                            nCur = sTxtTmpl.Length;//終了（最後の文字の次へカーソルを出す）
                        }
                    }
                    else
                    {
                        // 閉じ%がなければ。
                        //.WriteLine(this.GetType().Name + "#Perform: 閉じ%がなければ。");

                        // 今回の判定は失敗したものとして、残りの長さ全て
                        int nRestLen = sTxtTmpl.Length - nPreCur;
                        //.WriteLine(this.GetType().Name + "#Perform: restLen＝[" + restLen + "]");
                        sb.Append(sTxtTmpl.Substring(nPreCur, nRestLen));
                        //.WriteLine(this.GetType().Name + "#Perform: resultTxt＝[" + resultTxt.ToString() + "]");
                        nCur = sTxtTmpl.Length;//終了（最後の文字の次へカーソルを出す）
                        //.WriteLine(this.GetType().Name + "#Perform: cur＝[" + cur + "]");
                    }
                }
                else
                {
                    // 開き%がなければ。
                    //.WriteLine(this.GetType().Name + "#Perform: 開き%がなければ。");

                    // 残りの長さ全て
                    int nRestLen = sTxtTmpl.Length - nCur;
                    //.WriteLine(this.GetType().Name + "#Perform: restLen＝[" + restLen + "]");
                    sb.Append(sTxtTmpl.Substring(nCur, nRestLen));
                    //.WriteLine(this.GetType().Name + "#Perform: resultTxt＝[" + resultTxt.ToString() + "]");
                    nCur = sTxtTmpl.Length;//終了（最後の文字の次へカーソルを出す）
                    //.WriteLine(this.GetType().Name + "#Perform: cur＝[" + cur + "]");
                }
            }

            goto gt_EndMethod;
            //
        gt_EndMethod:
            return sb.ToString();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private string text;

        /// <summary>
        /// 「%1%:%2%」といった文字列（テキスト_テンプレートと呼ぶ）。
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        //────────────────────────────────────────

        private Dictionary<int, string> dictionary_NumberAndValue_Parameter;

        /// <summary>
        /// [1]=101
        /// [2]=赤
        /// といったディクショナリー。
        /// 
        /// キーは %1%や、%2%といった名前の中の数字。[1]から始める。
        /// Xn_L05_E:E_FtextTemplate#E_ExecuteでAddされます。
        /// </summary>
        public Dictionary<int, string> Dictionary_NumberAndValue_Parameter
        {
            get
            {
                return dictionary_NumberAndValue_Parameter;
            }
            //set
            //{
            //    dictionary_NumberAndValue_Parameter = value;
            //}
        }

        //────────────────────────────────────────
        #endregion



    }
}
