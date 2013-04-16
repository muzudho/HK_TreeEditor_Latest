using System;
using System.Collections.Generic;
using System.IO;  //Directory, Path
using System.Linq;
using System.Text;
using System.Windows.Forms;//Application

namespace Xenon.Syntax
{

    /// <summary>
    /// ユーティリティー。
    /// 
    /// ファイルパスについて。
    /// </summary>
    public abstract class Util_Filepath
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// ファイルパスを、絶対ファイルパスに変換します。
        /// </summary>
        /// <param name="humaninput"></param>
        /// <param name="isTooLong"></param>
        /// <param name="isSafe_TooLong"></param>
        /// <param name="log_Reports"></param>
        /// <param name="cur_Conf"></param>
        /// <returns></returns>
        public static string ToAbsolute(
            string humaninput,
            ref bool isTooLong,
            bool isSafe_TooLong,
            Log_Reports log_Reports,
            Configurationtree_Node cur_Conf
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, "Util_Filepath", "ToAbsolute③", log_Reports);

            string sResult;

            if (log_Reports.Successful)
            {
                sResult = Util_Filepath.ToAbsolute(
                    "",
                    humaninput,
                    ref isTooLong,
                    isSafe_TooLong,
                    log_Reports,//out sErrorMsg,
                    cur_Conf
                    );
            }
            else
            {
                sResult = "";
            }

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return sResult;
        }

        //────────────────────────────────────────

        public static string ToAbsolute(
            Configurationtree_NodeFilepath filepath_Conf,
            ref bool isTooLong,
            bool isSafe_TooLong,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, "Util_Filepath", "ToAbsolute①", log_Reports);

            string sResult;

            if (log_Reports.Successful)
            {
                sResult = Util_Filepath.ToAbsolute(
                    "",
                    filepath_Conf.GetHumaninput(),
                    ref isTooLong,
                    isSafe_TooLong,
                    log_Reports,//out sErrorMsg,
                    filepath_Conf
                    );
            }
            else
            {
                sResult = "";
            }

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return sResult;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 「ディレクトリー」と「入力値」の２つを入力すると、「絶対パス」を返します。
        /// 
        /// ──────────
        /// 
        /// 未設定の場合は、空文字列を返します。
        /// ※bug:フォルダーパスの場合も空文字列になる？？
        /// 
        /// ・ファイルパスとして利用できない文字や、予約語が含まれていると例外を投げます。
        /// ・絶対パスの文字列の長さが、ファイルシステムで使える制限を越えると例外を投げます。
        /// 
        /// もし、設定されたパスが相対パスだった場合に、ベース・パスが設定されていなければ、
        /// 起動「.exe」のあったパスが頭に付く。
        /// </summary>
        /// <param name="baseDirectory"></param>
        /// <param name="humanInputText"></param>
        /// <param name="flagCheckPathTooLong">絶対パスの文字列の長さが、ファイルシステムで使える上限を超えていた場合に真、そうでない場合　偽にセットされます。</param>
        /// <param name="okPathTooLong">絶対パスの文字列の長さが、ファイルシステムで使える上限を超えていた場合に、「正常扱いにするなら」真、「エラー扱いにするなら」偽。</param>
        /// <param name="cur_Conf">デバッグ用情報。人間オペレーターが修正するべき箇所などの情報。</param>
        /// <returns></returns>
        public static string ToAbsolute(
            string directory_Base,
            string humaninput,
            ref bool isTooLong,
            bool isSafe_TooLong,
            Log_Reports log_Reports,
            Configuration_Node cur_Conf
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, "Util_Filepath", "ToAbsolute②", log_Reports);
            //
            //

            //
            // 修正履歴(2009-12-02)
            //
            // ・カレント・ディレクトリの移動を使ったコードを書いてはいけない。
            //   MS-DOSの名残り？
            //
            // ・起動「.exe」のディレクトリは Application.StartupPath で取得できる。
            //
            // ・備考：
            // System.IO.Directory.GetCurrentDirectory()は、
            // 「プロセスが開始されたディレクトリ」を返すので、
            // openFileDialogで開いたディレクトリを返すこともある。
            //
            // System.IO.Path.GetFullPath(path)も同じ。

            Exception err_Excp;

            string result_Filepath;//ファイルパス

            // フラグのクリアー。
            isTooLong = false;

            //
            // 人間がCSVファイルに記述しているファイル・パス。
            //
            // 「絶対パス」「相対パス」のどちらでも指定されます。
            //
            string filepath_Source = humaninput.Trim();

            if ("" == filepath_Source)
            {
                // 未設定の場合。
                result_Filepath = "";//ファイルパスとしては使えない文字列。
                goto gt_EndMethod;
            }

            // 「絶対パス」か、「相対パス」かを判断します。
            bool isRooted_Path = Util_Filepath.IsRooted(
                filepath_Source,
                log_Reports
                );

            if (!log_Reports.Successful)
            {
                // 既エラー。
                result_Filepath = "";//ファイルパスとしては使えない文字列。
                goto gt_EndMethod;
            }

            if (!isRooted_Path)
            {
                // 相対パスの場合

                // 「相対パス」に「ベース・ディレクトリー文字列」を連結して、「絶対パス」に変換します。

                if ("" != directory_Base)
                {
                    // 相対パスの相対元となるディレクトリーが設定されていれば。

                    if (!directory_Base.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    {
                        filepath_Source = directory_Base + Path.DirectorySeparatorChar + filepath_Source;
                    }
                    else
                    {
                        filepath_Source = directory_Base + filepath_Source;
                    }
                }
                else
                {
                    // 起動「.exe」のあったパスを、相対の元となるディレクトリーとします。

                    if (!directory_Base.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    {
                        filepath_Source = Application.StartupPath + Path.DirectorySeparatorChar + filepath_Source;
                    }
                    else
                    {
                        filepath_Source = Application.StartupPath + filepath_Source;
                    }
                }
            }

            // ここで、パスは　絶対パスに変換されています。

            try
            {
                // カレントディレクトリは使わない。

                // 絶対パスの場合、GetFullPathを通す必要はないが、
                // ファイルパスに使えない文字列を判定するために、
                // 例外を返すメソッドを使っています。
                result_Filepath = System.IO.Path.GetFullPath(filepath_Source);
            }
            catch (ArgumentException e)
            {
                // 指定のファイルパスに「*」など、ファイルパスとして使えない文字列が含まれていた場合など。

                result_Filepath = "";//ファイルパスとしては使えない文字列。

                err_Excp = e;
                goto gt_Error_ArgumentException;
            }
            catch (PathTooLongException e)
            {
                // ディレクトリーの文字数が、制限数を超えた場合などのエラー。

                result_Filepath = "";//ファイルパスとしては使えない文字列。

                if (isSafe_TooLong)
                {
                    // 正常処理扱いとします。

                }
                else
                {
                    // 異常扱いとします。

                    err_Excp = e;
                    goto gt_Error_PathTooLongException;
                }


                isTooLong = true;
            }
            catch (NotSupportedException e)
            {
                //パスのフォーマットが間違っているなどのエラー。
                result_Filepath = "";//ファイルパスとしては使えない文字列。
                err_Excp = e;
                goto gt_Error_NotSupportedException;
            }
            catch (Exception e)
            {
                // それ以外のエラー。
                result_Filepath = "";//ファイルパスとしては使えない文字列。
                err_Excp = e;
                goto gt_Error_Exception;
            }

            goto gt_EndMethod;
            //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_ArgumentException:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー107！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append(Environment.NewLine);
                s.Append("使えないファイルパスです。[");
                s.Append(filepath_Source);
                s.Append("]　：");

                s.Append(err_Excp.Message);
                cur_Conf.ToText_Locationbreadcrumbs(s);

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_PathTooLongException:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー108！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append(Environment.NewLine);
                s.Append("エラー 入力パス=[" + filepath_Source + "]：(" + err_Excp.GetType().Name + ") ");

                s.Append(err_Excp.Message);
                cur_Conf.ToText_Locationbreadcrumbs(s);

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_NotSupportedException:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー109！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append(Environment.NewLine);
                s.Append("ファイルパスが間違っているかもしれません。");
                s.Newline();
                s.AppendI(1,"入力パス=[" + filepath_Source + "]");
                s.Newline();

                // ヒント
                s.Append(r.Message_SException(err_Excp));
                cur_Conf.ToText_Locationbreadcrumbs(s);

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Exception:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー109！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append(Environment.NewLine);
                s.Append("エラー 入力パス=[" + filepath_Source + "]");
                s.Newline();

                // ヒント
                s.Append(r.Message_SException(err_Excp));
                cur_Conf.ToText_Locationbreadcrumbs(s);

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
            //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result_Filepath;
        }

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        /// <summary>
        /// パスはルートかどうか。
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsRooted(
            string filepath,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, "Util_Filepath", "IsRooted", log_Reports);
            //
            //
            bool bPathRooted;

            Exception err_Excp;
            try
            {
                // 「絶対パス」か、「相対パス」かを判断します。
                bPathRooted = System.IO.Path.IsPathRooted(filepath);
            }
            catch (ArgumentException e)
            {
                // エラー
                err_Excp = e;
                goto gt_Error_MissInput;
            }

            goto gt_EndMethod;
            //
            //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_MissInput:
            bPathRooted = false;
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー211！", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("エラー 入力パス=[" + filepath + "]：(" + err_Excp.GetType().Name + ") ");
                sb.Append(err_Excp.Message);

                r.Message = sb.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
            //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return bPathRooted;
        }

        //────────────────────────────────────────

        public static bool IsTooLong(
            string filepath_Humaninput_New,
            Log_Reports log_Reports,
            Configurationtree_Node cur_Conf
            )
        {
            return Util_Filepath.IsTooLong(
                "",
                filepath_Humaninput_New,
                log_Reports,// out sErrorMsg,
                cur_Conf
                );
        }

        //────────────────────────────────────────

        /// <summary>
        /// 絶対パスが、ファイルシステムで使えるファイルパスの文字列の長さの制限を越えていれば真。
        /// </summary>
        /// <param name="newDirectoryPath">指定するものがない場合は、System.Windows.Forms.StartupPath を入れてください。</param>
        /// <param name="newHumanInputFilePath"></param>
        /// <param name="cur_Conf"></param>
        public static bool IsTooLong(
            string folderpath_New,
            string filepath_Humaninput_New,
            Log_Reports log_Reports,
            Configurationtree_Node cur_Conf
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, "Util_Filepath", "IsTooLong", log_Reports);

            // フラグ。
            bool bFlagCheckPathTooLong = false;

            if (log_Reports.Successful)
            {
                // チェック。絶対パスにすることができればOK。
                Util_Filepath.ToAbsolute(
                    folderpath_New,
                    filepath_Humaninput_New,
                    ref bFlagCheckPathTooLong,
                    true,//ファイル名の長さが上限超過でも、正常処理扱いとします。
                    log_Reports,// out sErrorMsg,
                    cur_Conf
                    );
            }

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return bFlagCheckPathTooLong;
        }

        //────────────────────────────────────────
        #endregion



    }
}
