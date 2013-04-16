﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Xenon.Syntax;
using Xenon.Table;

namespace TreeEditor
{


    public class Actions
    {

        public static void Undo(Form1 form1)
        {
            form1.UiMain1.UiTextside1.Undo();
        }

        public static void Redo(Form1 form1)
        {
            form1.UiMain1.UiTextside1.Redo();
        }

        /// <summary>
        /// プロジェクトの新規作成。
        /// </summary>
        public static void New(Form1 form1)
        {
            System.Console.WriteLine("プロジェクトを新規作成したい。");

            NewProjectDialog dlg = new NewProjectDialog();
            dlg.ShowDialog(form1);

            //━━━━━
            //ディレクトリー作成
            //━━━━━
            {
                string dir = dlg.UiNewProject.TextBox1.Text;
                System.Console.WriteLine("作成ディレクトリー：" + dir);

                if (Directory.Exists(@"save\" + dir))
                {
                    MessageBox.Show("もうあります。\n" + dir);
                }
                else
                {
                    form1.UiMain1.CreateDefaultProject(dir);
                    form1.UiMain1.OpenProject(dir);
                }
            }


            ////━━━━━
            ////ディレクトリー一覧
            ////━━━━━
            //{
            //    this.listBox1.Items.Clear();

            //    string[] dirs = Directory.GetDirectories("save");

            //    foreach (string dir in dirs)
            //    {
            //        System.Console.WriteLine("ディレクトリー：" + dir);

            //        string dir2 = dir;
            //        if (dir2.StartsWith(@"save\"))
            //        {
            //            dir2 = dir2.Substring(@"save\".Length, dir2.Length - @"save\".Length);
            //        }

            //        this.listBox1.Items.Add(dir2);
            //    }
            //}

            dlg.Dispose();

        }

        public static void LoadPageText(Form1 form1, string file)
        {
            if (File.Exists(file))
            {
                try
                {
                    //━━━━━
                    //テキスト読取り
                    //━━━━━
                    {
                        string contents = File.ReadAllText(file, Encoding.GetEncoding("Shift_JIS"));
                        System.Console.WriteLine("★テキストファイルを読み込みます。[" + file + "]　[" + contents + "]");
                        form1.UiMain1.Contents.TextNow(file, contents);

                        form1.UiMain1.UiTextside1.TextareaText = contents;
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show(form1, file, "ノードTXTファイル[" + file + "]が無い");
            }
        }

        public static void LoadPageCsv(Form1 form1, string file)
        {
            Log_Method log_Method = new Log_MethodImpl();
            Log_ReportsImpl log_Reports = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod("--", "Actions", "LoadPageCsv", log_Reports);

            //━━━━━
            //画像情報読取り
            //━━━━━
            if (File.Exists(file))
            {
                try
                {
                    string contents = File.ReadAllText(file, Encoding.GetEncoding("Shift_JIS"));

                    CsvTo_TableImpl csvTo = new CsvTo_TableImpl();
                    Request_ReadsTableImpl request = new Request_ReadsTableImpl();
                    //request.Name_PutToTable = resourceCsvFilePath;
                    Format_TableImpl format = new Format_TableImpl();
                    Table_Humaninput table = csvTo.Read(
                        contents,
                        request,
                        format,
                        log_Reports
                        );
                    if (log_Reports.Successful)
                    {
                        table.ForEach_Datapart(delegate(Record_Humaninput recordH, ref bool isBreak2, Log_Reports log_Reports2)
                        {
                            //レコード
                            string rNo = recordH.TextAt("NO");
                            string rExpl = recordH.TextAt("EXPL");
                            string rFile = recordH.TextAt("FILE");
                            //左上座標
                            string rX = recordH.TextAt("X");
                            string rY = recordH.TextAt("Y");
                            System.Console.WriteLine("NO=" + rNo + " EXPL=" + rExpl + " FILE=" + rFile + " X=" + rX + " Y=" + rY);

                            int xN;
                            int.TryParse(rX, out xN);

                            int yN;
                            int.TryParse(rY, out yN);

                            PictureBox pic1 = form1.UiMain1.UiTextside1.NewPictureBox(rFile, new Point(xN, yN));

                            //中心座標に変更
                            pic1.Location = new Point(
                                pic1.Location.X + pic1.Width / 2,
                                pic1.Location.Y + pic1.Height / 2
                                );

                        }, log_Reports);

                        System.Console.WriteLine("★CSV読込完了 [" + file + "]");
                        form1.UiMain1.Contents.CsvNow(file, contents);
                        form1.UiMain1.Contents.IsChangedResource = false;
                        form1.UiMain1.TestChangeText();
                    }
                    else
                    {
                        System.Console.WriteLine("★CSV読込失敗：" + log_Reports.ToText());
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                System.Console.WriteLine("★失敗：リソースCSV[" + file + "]がない。");
            }

            if (!log_Reports.Successful)
            {
                MessageBox.Show(form1, log_Reports.ToText(), "エラー");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form1"></param>
        /// <param name="textFile">ファイルパス。</param>
        /// <param name="csvFile">画像情報用ファイルパス。</param>
        public static void LoadPage(Form1 form1,string textFile, string csvFile)
        {
            Log_Method log_Method = new Log_MethodImpl();
            Log_ReportsImpl log_Reports = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod("--", "Actions", "Load", log_Reports);

            System.Console.WriteLine("ロードページ　予想プロジェクト名：" + form1.UiMain1.Contents.ProjectName);
            System.Console.WriteLine("ロードページ　予想ツリーファイル：" + form1.UiMain1.GetTreeCsvFileName(form1.UiMain1.Contents.ProjectName, false));
            System.Console.WriteLine("ロードページ　予想テキストファイル：" + textFile);
            System.Console.WriteLine("ロードページ　予想CSVファイル：" + csvFile);

            Actions.LoadPageText(form1, textFile);

            Actions.LoadPageCsv(form1, csvFile);

            if (!log_Reports.Successful)
            {
                MessageBox.Show(form1, log_Reports.ToText(), "エラー");
            }
        }

        /// <summary>
        /// 再帰関数
        /// </summary>
        public static void RecursiveTree(Form1 form1, int autoNum, int tree, TreeNode tn, StringBuilder sb)
        {
            tree++;
            foreach (TreeNode tn2 in tn.Nodes)
            {
                string foreColorWeb = "#000000";
                string backColorWeb = "#FFFFFF";

                if (tn2 is TreeNode2)
                {
                    TreeNode2 tn3 = (TreeNode2)tn2;
                    foreColorWeb = tn3.Fore.Web;
                    backColorWeb = tn3.Back.Web;
                }


                sb.Append(autoNum + ",," + tree + "," + tn2.SelectedImageIndex + "," + tn2.Text + "," + tn2.Text + ".txt," + foreColorWeb + "," + backColorWeb + ",");
                sb.Append(Environment.NewLine);
                autoNum++;

                Actions.RecursiveTree(form1, autoNum, tree, tn2, sb);
            }
            tree--;
        }

        public static void SavePage(Form1 form1)
        {
            System.Console.WriteLine("セーブページ　予想プロジェクト名：" + form1.UiMain1.Contents.ProjectName);
            System.Console.WriteLine("セーブページ　予想ツリーファイル：" + form1.UiMain1.GetTreeCsvFileName(form1.UiMain1.Contents.ProjectName, false));
            System.Console.WriteLine("セーブページ　予想テキストファイル：" + form1.UiMain1.Contents.TextFile);
            System.Console.WriteLine("セーブページ　予想CSVファイル：" + form1.UiMain1.Contents.CsvFile);

            try
            {
                //━━━━━
                //ツリー書出し
                //━━━━━
                StringBuilder sb = new StringBuilder();
                int autoNum = 0;

                sb.Append("NO,EXPL,TREE,ICON,NAME,FILE,FORE_COLOR,BACK_COLOR,EOL");
                sb.Append(Environment.NewLine);

                sb.Append("int,string,int,int,string,string,string,string,");
                sb.Append(Environment.NewLine);

                sb.Append("自動連番,コメント,ツリー階層,,,廃止方針,前景色,背景色,");
                sb.Append(Environment.NewLine);

                int tree = 1;
                foreach (TreeNode tn in form1.UiMain1.TreeView1.Nodes)
                {
                    string foreColorWeb = "#000000";
                    string backColorWeb = "#FFFFFF";

                    if (tn is TreeNode2)
                    {
                        TreeNode2 tn2 = (TreeNode2)tn;

                        foreColorWeb = tn2.Fore.Web;
                        backColorWeb = tn2.Back.Web;
                    }

                    sb.Append(autoNum + ",," + tree + "," + tn.SelectedImageIndex + "," + tn.Text + "," + tn.Text + ".txt," + foreColorWeb + "," + backColorWeb + ",");
                    sb.Append(Environment.NewLine);
                    autoNum++;

                    Actions.RecursiveTree(form1, autoNum, tree, tn, sb);
                }

                sb.Append("EOF,,,,,,,,");
                sb.Append(Environment.NewLine);

                string contents = sb.ToString();
                System.Console.WriteLine("セーブ　予想ツリー：" + contents);

                File.WriteAllText(
                    form1.UiMain1.GetTreeCsvFileName(form1.UiMain1.Contents.ProjectName, false),
                    contents,
                    Encoding.GetEncoding("Shift_JIS")
                    );
            }
            catch (Exception e)
            {
                MessageBox.Show(form1, e.Message, "ツリー書き出し失敗。何らかのエラー");
            }


            if (File.Exists(form1.UiMain1.Contents.TextFile))
            {
                //━━━━━
                //テキスト書出し
                //━━━━━

                System.Console.WriteLine("セーブ　予想テキスト：" + form1.UiMain1.UiTextside1.TextareaText);
                File.WriteAllText(form1.UiMain1.Contents.TextFile, form1.UiMain1.UiTextside1.TextareaText, Encoding.GetEncoding("Shift_JIS"));
                form1.UiMain1.Contents.TextNow(form1.UiMain1.Contents.TextFile, form1.UiMain1.UiTextside1.TextareaText);
                form1.UiMain1.TestChangeText();
            }
            else
            {
                MessageBox.Show(form1, form1.UiMain1.Contents.TextFile, "ノードTXTファイル[" + form1.UiMain1.Contents.TextFile + "]が無い");
            }


            try
            {
                //━━━━━
                //画像情報書出し
                //━━━━━
                if ("" != form1.UiMain1.Contents.CsvFile)
                {
                    StringBuilder sb = new StringBuilder();
                    int autoNum = 0;

                    sb.Append("NO,EXPL,FILE,X,Y,EOL");
                    sb.Append(Environment.NewLine);

                    sb.Append("int,string,string,int,int,");
                    sb.Append(Environment.NewLine);

                    sb.Append("自動連番,解説,ファイルパス,座標,座標,");
                    sb.Append(Environment.NewLine);

                    foreach (Control c in form1.UiMain1.UiTextside1.RichTextBox1.Controls)
                    {
                        if (c is PictureBox)
                        {
                            PictureBox pic = (PictureBox)c;

                            //ファイルパス
                            string fp = pic.ImageLocation;
                            if (fp.StartsWith(Application.StartupPath + @"\"))
                            {
                                fp = fp.Substring(Application.StartupPath.Length + @"\".Length);
                            }


                            sb.Append(autoNum + ",," + fp + "," + pic.Location.X + "," + pic.Location.Y + ",");
                            sb.Append(Environment.NewLine);
                            autoNum++;
                        }
                    }

                    sb.Append("EOF,,,,,");
                    sb.Append(Environment.NewLine);

                    string contents = sb.ToString();
                    System.Console.WriteLine("セーブ　予想CSV：" + contents);

                    File.WriteAllText(
                        form1.UiMain1.Contents.CsvFile,
                        contents,
                        Encoding.GetEncoding("Shift_JIS")
                        );
                    form1.UiMain1.Contents.IsChangedResource = false;
                    form1.UiMain1.TestChangeText();
                    form1.UiMain1.Contents.CsvNow(form1.UiMain1.Contents.CsvFile, contents);
                }
                else
                {
                    MessageBox.Show(form1,"form1.UiMain1.CsvFilePathが未指定。","エラー");
                }

                System.Console.WriteLine("セーブ： [" + form1.UiMain1.Contents.TextFile + "]　[" + form1.UiMain1.Contents.CsvFile + "]");
            }
            catch (Exception)
            {
            }

            //if (File.Exists(form1.UiMain1.NodeFilePath))
            //{
            //}
            //else
            //{
            //    MessageBox.Show(form1, form1.UiMain1.NodeFilePath, "ファイルが無い");
            //}
        }


        /// <summary>
        /// タイマーによるバックアップを取ります。起動時にも使われます。
        /// </summary>
        public static void Buckup(UiMain uiMain)
        {
            //━━━━━
            // ツリーCSV
            //━━━━━
            try
            {
                //コピー先ディレクトリー
                string file = uiMain.GetTreeCsvFileName(uiMain.Contents.ProjectName, true);
                string dir = Path.GetDirectoryName(file);
                System.Console.WriteLine("ツリー：file[" + file + "]　[" + dir + "]");

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.Copy(
                    uiMain.GetTreeCsvFileName(uiMain.Contents.ProjectName, false),
                    file,
                    true
                    );
            }
            catch (Exception exc)
            {
                System.Console.WriteLine("ツリーCSVのバックアップに失敗　[" + uiMain.GetTreeCsvFileName(uiMain.Contents.ProjectName, false) + "]→[" + uiMain.GetTreeCsvFileName(uiMain.Contents.ProjectName, true) + "]　：" + exc.Message);
            }

            //━━━━━
            // テキストTXT
            //━━━━━
            try
            {
                //コピー先ディレクトリー
                string file = uiMain.GetTextFileName(uiMain.Contents.ProjectName, uiMain.UiTextside1.NodeNameTxt1.Text, true);
                string dir = Path.GetDirectoryName(file);
                System.Console.WriteLine("テキスト：file[" + file + "]　[" + dir + "]");

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.Copy(
                    uiMain.GetTextFileName(uiMain.Contents.ProjectName, uiMain.UiTextside1.NodeNameTxt1.Text, false),
                    file,
                    true
                    );
            }
            catch (Exception exc)
            {
                System.Console.WriteLine("テキストTXTのバックアップに失敗　[" + uiMain.GetTextFileName(uiMain.Contents.ProjectName, uiMain.UiTextside1.NodeNameTxt1.Text, false) + "]→[" + uiMain.GetTextFileName(uiMain.Contents.ProjectName, uiMain.UiTextside1.NodeNameTxt1.Text, true) + "]　：" + exc.Message);
            }

            //━━━━━
            // リソースCSV
            //━━━━━
            try
            {
                //コピー先ディレクトリー
                string file = uiMain.GetResourceCsvFileName(uiMain.Contents.ProjectName, uiMain.UiTextside1.NodeNameTxt1.Text, true);
                string dir = Path.GetDirectoryName(file);
                System.Console.WriteLine("リソース：file[" + file + "]　[" + dir + "]");

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.Copy(
                    uiMain.GetResourceCsvFileName(uiMain.Contents.ProjectName, uiMain.UiTextside1.NodeNameTxt1.Text, false),
                    file,
                    true
                    );
            }
            catch (Exception exc)
            {
                System.Console.WriteLine("リソースCSVのバックアップに失敗　[" + uiMain.GetResourceCsvFileName(uiMain.Contents.ProjectName, uiMain.UiTextside1.NodeNameTxt1.Text, false) + "]→[" + uiMain.GetResourceCsvFileName(uiMain.Contents.ProjectName, uiMain.UiTextside1.NodeNameTxt1.Text, true) + "]　：" + exc.Message);
            }
        }
    }
}
