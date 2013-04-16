//
// Cushion
//
// アプリケーションを作るうえで、よく使うことになるもの。
//
using System;
using System.Collections.Generic;
using System.IO;  //Directory
using System.Linq;
using System.Text;
using System.Windows.Forms;//Application

namespace Xenon.Syntax
{
    /// <summary>
    /// ファイル・パス。
    /// 
    /// 人間オペレーターが設定ファイルなどに入力したファイルパスの記述そのままに保持。
    /// 
    /// 相対パス、絶対パスのどちらでも構いません。
    /// 
    /// 
    /// 備考1：
    /// ファイルパスに無効な値が含まれていることを事前にチェックするのは難しい。
    /// Windowsで作れないファイル名
    /// 「CON」「PRN」「AUX」「NUL」「COM0」～「COM9」「LPT0」～「LPT9」など。DOSのコマンド。
    /// これらを事前チェックで弾くのもローカル依存なので、例外でキャッチすること。
    /// 
    /// ファイル名として使えない文字は、
    /// 「filePath.IndexOfAny(Path.GetInvalidFileNameChars()) &lt; 0;」
    /// で確認可能。
    ///
    /// 備考2：
    ///
    /// パスは、文字列連結や変換の過程で「/」と「\」が混在することがあり、
    /// == を使った文字列比較では　一致を判定できない。
    ///
    /// </summary>
    public class Expression_Node_FilepathImpl : Expression_Leaf_StringImpl, Expression_Node_Filepath
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="s_Fpath"></param>
        public Expression_Node_FilepathImpl(Configurationtree_NodeFilepath fpath_Conf)
            : base(null, fpath_Conf)
        {
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// フォルダー絶対パスを指定すると、そのフォルダーパスを切り落とした文字列を返します。
        /// 
        /// 違うフォルダーだった場合、失敗します。
        /// 
        /// 先頭がディレクトリー区切り文字にならないようにして結果を返します。
        /// </summary>
        /// <param name="folerpath"></param>
        public void TryCutFolderpath(
            out string out_Filepath_New,
            Expression_Node_Filepath folderpath,
            bool isRequired,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "TryCutFolderpath", log_Reports);

            //まず、自分の絶対パス
            string my = this.Execute4_OnExpressionString(EnumHitcount.Unconstraint, log_Reports);
            //if(log_Method.CanDebug(1))
            //{
            //    log_Method.WriteDebug_ToConsole("my=[" + my + "]");
            //}

            //指定されたフォルダーの絶対パス
            string you = folderpath.Execute4_OnExpressionString(EnumHitcount.Unconstraint, log_Reports);
            //if (log_Method.CanDebug(1))
            //{
            //    log_Method.WriteDebug_ToConsole("you=[" + you + "]");
            //}

            //if (log_Method.CanDebug(1))
            //{
            //    log_Method.WriteDebug_ToConsole("my.StartsWith(you)=[" + my.StartsWith(you) + "]");
            //}

            if (my.StartsWith(you))
            {
                out_Filepath_New = my.Substring(you.Length);
                //if (log_Method.CanDebug(1))
                //{
                //    log_Method.WriteDebug_ToConsole("you.Length=[" + you.Length + "]");
                //    log_Method.WriteDebug_ToConsole("filepath_New1=[" + filepath_New1 + "]");
                //    log_Method.WriteDebug_ToConsole("filepath_New1.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString())=[" + filepath_New1.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()) + "]");
                //    if (filepath_New1.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
                //    {
                //        log_Method.WriteDebug_ToConsole("filepath_New1=[" + filepath_New1.Substring(1) + "]");
                //    }
                //}

                // 先頭がディレクトリー区切り文字だった場合、それを切り捨てます。
                if (out_Filepath_New.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
                {
                    out_Filepath_New = out_Filepath_New.Substring(1);
                }

                //Configurationtree_NodeFilepath filepath_Conf_New = new Configurationtree_NodeFilepathImpl(log_Method.Fullname, null);
                //filepath_Conf_New.InitPath(filepath_New1, log_Reports);
                //out_Filepath_ExprNew = new Expression_Node_FilepathImpl(filepath_Conf_New);
            }
            else
            {
                //失敗
                out_Filepath_New = "";

                if (isRequired)
                {
                    // エラー。
                    goto gt_Error_Failure;
                }
            }

            goto gt_EndMethod;
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Failure:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー922！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("ファイルパスの加工に失敗しました。\n");
                s.Append("[" + my + "]の頭から、フォルダー[" + you + "]を切りぬこうとしましたが、フォルダーが違いました。");


                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 設定ファイルに記述されているままのファイル・パス表記。
        /// 
        /// 相対パス、絶対パスのどちらでも構わない。
        /// 
        /// 例："Data\\Monster.csv"
        /// </summary>
        /// <param name="newHumanInputFilePath"></param>
        public void SetHumaninput(
            string filepath_Humaninput_New,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "SetHumaninput", log_Reports);

            if (this.Cur_Configuration is Configurationtree_NodeFilepath)
            {
                ((Configurationtree_NodeFilepath)this.Cur_Configuration).SetHumaninput(
                filepath_Humaninput_New,
                log_Reports
                );
            }
            else
            {
                // エラー。
                if (log_Reports.CanCreateReport)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー902！", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append(Environment.NewLine);
                    s.Append("#SetSHumanInput:型が違います。[" + this.Cur_Configuration.GetType().Name + "]");

                    r.Message = s.ToString();
                    log_Reports.EndCreateReport();
                }
            }

            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 絶対パスを取得します。
        /// 
        /// 未設定の場合は、空文字列を返します。
        /// 
        /// ・ファイルパスとして利用できない文字や、予約語が含まれていると例外を投げます。
        /// ・絶対パスの文字列の長さが、ファイルシステムで使える制限を越えると例外を投げます。
        /// 
        /// 設定されたパスが相対パスだった場合に、ベース・パスが設定されていなければ、
        /// 起動「.exe」のあったパスが頭に付く。
        /// </summary>
        /// <returns></returns>
        public override string Execute4_OnExpressionString(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Execute4_OnExpressionString", log_Reports);
            //
            //

            // 絶対パスにして返します。

            string sFpath;
            if (this.Cur_Configuration is Configurationtree_NodeFilepath)
            {
                Configurationtree_NodeFilepath cf_Fpath = (Configurationtree_NodeFilepath)this.Cur_Configuration;

                bool bCheckPathTooLong = false;

                if (log_Reports.Successful)
                {
                    sFpath = Util_Filepath.ToAbsolute(
                        this.Directory_Base,
                        cf_Fpath.GetHumaninput(),//this.SHumanInput相当
                        ref bCheckPathTooLong, //ファイル名の長さチェックは、もう済んでいるものとして、行いません。
                        false,//ファイル名の長さが上限超過ならエラー
                        log_Reports,//out sErrorMsg,
                        this.Cur_Configuration
                        );
                }
                else
                {
                    sFpath = "";
                }
            }
            else
            {
                // エラー。
                sFpath = "";
                if (log_Reports.CanCreateReport)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー901！", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append(Environment.NewLine);
                    s.Append("#GetSAbsoluteFilePath:型が違います。[" + this.Cur_Configuration.GetType().Name + "]");

                    r.Message = s.ToString();
                    log_Reports.EndCreateReport();
                }
            }

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return sFpath;
        }

        //────────────────────────────────────────

        /// <summary>
        /// このデータは、ファイルパス型だ、と想定して、ファイルパスを取得します。
        /// </summary>
        /// <returns></returns>
        public override Expression_Node_Filepath Execute4_OnExpressionString_AsFilepath(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            return this;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 相対パスが設定されていた場合、その相対元となるディレクトリーへのパスです。
        /// そうでない場合は、System.Windows.Forms.StartupPath を入れてください。
        /// </summary>
        /// <param name="newDirectoryPath"></param>
        public void SetDirectory_Base(
            string sFolderpath_New,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "SetSDirectory_Base", log_Reports);

            // ダミー・フラグ。使いません。
            bool bDammyFlagCheckPathTooLong = false;

            if (this.Cur_Configuration is Configurationtree_NodeFilepath)
            {
                Configurationtree_NodeFilepath cf_Fpath = ((Configurationtree_NodeFilepath)this.Cur_Configuration);

                // チェック。絶対パスにすることができればOK。
                Util_Filepath.ToAbsolute(
                    sFolderpath_New,
                    cf_Fpath.GetHumaninput(),
                    ref bDammyFlagCheckPathTooLong,
                    false,//ファイル名の長さが上限超過ならエラー
                    log_Reports,//out sErrorMsg,
                    this.Cur_Configuration
                    );
                if (!log_Reports.Successful)
                {
                    // 既エラー。
                    goto gt_EndMethod;
                }

                cf_Fpath.SetDirectory_Base(sFolderpath_New);
            }
            else
            {
                // エラー
                if (log_Reports.CanCreateReport)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー903！", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append(Environment.NewLine);
                    s.Append("#GetSAbsoluteFilePath:型が違います。[" + this.Cur_Configuration.GetType().Name + "]");

                    r.Message = s.ToString();
                    log_Reports.EndCreateReport();
                }
            }

            //
        //
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 拡張子を取得します。「.」は含みません。
        /// 
        /// ※１ステップでできない。→「System.IO.Path.GetExtensionを使った方がいいのでは。（「.」を含む）」
        /// 
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="log_Reports"></param>
        public void TryGetExtension(out string extension, Log_Reports log_Reports)
        {
            string filepathabsolute = this.Execute4_OnExpressionString(Syntax.EnumHitcount.Unconstraint, log_Reports);

            int index_Dot = filepathabsolute.LastIndexOf('.');
            if (-1 != index_Dot)
            {
                extension = filepathabsolute.Substring(index_Dot + 1);
            }
            else
            {
                extension = "";
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// ファイル名の頭と末尾に文字列を付けることができます。
        /// </summary>
        /// <param name="prefix">ファイル名の頭に付ける文字列。</param>
        /// <param name="suffix">ファイル名の末尾に付ける文字列。</param>
        public Expression_Node_Filepath Rename_Append(string prefix, string suffix, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Rename_Append", log_Reports);

            Expression_Node_Filepath result;

            string absolute = this.Execute4_OnExpressionString(Syntax.EnumHitcount.Unconstraint, log_Reports);
            if ("" == absolute)
            {
                result = null;
                goto gt_Error_Empty;
            }

            string directory = Path.GetDirectoryName(absolute);
            string filename = Path.GetFileNameWithoutExtension(absolute);
            string extension = Path.GetExtension(absolute);//拡張子の「.」を含む。
            absolute = Path.Combine(directory, prefix + filename + suffix + extension);
            Configurationtree_NodeFilepath config = new Configurationtree_NodeFilepathImpl("<rename>", null);
            config.InitPath(absolute, log_Reports);
            result = new Expression_Node_FilepathImpl(config);

            goto gt_EndMethod;
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Empty:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー931！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("ファイルパスが空文字列でした。");

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 相対パスが設定されていた場合、その相対元となるディレクトリーへのパスです。
        /// そうでない場合は、System.Windows.Forms.StartupPath を入れてください。
        /// </summary>
        public string Directory_Base
        {
            get
            {
                string sResult;

                if (this.Cur_Configuration is Configurationtree_NodeFilepath)
                {
                    sResult = ((Configurationtree_NodeFilepath)this.Cur_Configuration).Directory_Base;
                }
                else
                {
                    // エラー。
                    sResult = "＜" + Info_Syntax.Name_Library + ":" + this.GetType().Name + "#SBaseDirectory:型が違います。[" + this.Cur_Configuration.GetType().Name + "]＞";
                }

                return sResult;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 設定ファイルに記述されているままのファイル・パス表記。
        /// 
        /// 相対パス、絶対パスのどちらでも構わない。
        /// 
        /// 例："Data\\Monster.csv"
        /// </summary>
        /// <returns></returns>
        public string Humaninput
        {
            get
            {
                string sResult;

                if (this.Cur_Configuration is Configurationtree_NodeFilepath)
                {
                    sResult = ((Configurationtree_NodeFilepath)this.Cur_Configuration).GetHumaninput();
                }
                else
                {
                    // エラー。
                    sResult = "＜" + Info_Syntax.Name_Library + ":" + this.GetType().Name + "#GetString:型が違います。[" + this.Cur_Configuration.GetType().Name + "]＞";
                }

                return sResult;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
