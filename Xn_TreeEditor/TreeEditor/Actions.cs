using System;
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

        public static void Load(Form1 form1,string textFilePath, string resourceCsvFilePath)
        {
            Log_Method log_Method = new Log_MethodImpl();
            Log_ReportsImpl log_Reports = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod("--", "Actions", "Load", log_Reports);

            if (File.Exists(textFilePath))
            {
                try
                {
                    //━━━━━
                    //テキスト読取り
                    //━━━━━
                    {
                        string text = File.ReadAllText(textFilePath, Encoding.GetEncoding("Shift_JIS"));
                        form1.UiMain1.NodeFileText = text;
                        form1.UiMain1.NodeFilePath = textFilePath;
                        form1.UiMain1.CsvFilePath = resourceCsvFilePath;
                        form1.UiMain1.UiTextside1.TextareaText = text;
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show(form1, textFilePath, "ノードTXTファイル[" + textFilePath + "]が無い");
            }


            //━━━━━
            //画像情報読取り
            //━━━━━
            if (File.Exists(resourceCsvFilePath))
            {
                try
                {
                    string text = File.ReadAllText(resourceCsvFilePath, Encoding.GetEncoding("Shift_JIS"));

                    CsvTo_TableImpl csvTo = new CsvTo_TableImpl();
                    Request_ReadsTableImpl request = new Request_ReadsTableImpl();
                    //request.Name_PutToTable = resourceCsvFilePath;
                    Format_TableImpl format = new Format_TableImpl();
                    Table_Humaninput table = csvTo.Read(
                        text,
                        request,
                        format,
                        log_Reports
                        );
                    if (log_Reports.Successful)
                    {
                        System.Console.WriteLine("★CSV読込完了 [" + resourceCsvFilePath + "]");

                        table.ForEach_Datapart(delegate(Record_Humaninput recordH, ref bool isBreak2, Log_Reports log_Reports2)
                        {
                            string no = recordH.TextAt("NO");
                            string expl = recordH.TextAt("EXPL");
                            string file = recordH.TextAt("FILE");
                            //左上座標
                            string x = recordH.TextAt("X");
                            string y = recordH.TextAt("Y");
                            System.Console.WriteLine("NO=" + no + " EXPL=" + expl + " FILE=" + file + " X=" + x + " Y=" + y);

                            int xN;
                            int.TryParse(x, out xN);

                            int yN;
                            int.TryParse(y, out yN);

                            PictureBox pic1 = form1.UiMain1.UiTextside1.NewPictureBox(file, new Point(xN, yN));

                            //中心座標に変更
                            pic1.Location = new Point(
                                pic1.Location.X + pic1.Width / 2,
                                pic1.Location.Y + pic1.Height / 2
                                );


                        }, log_Reports);

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
                System.Console.WriteLine("★失敗：リソースCSV[" + resourceCsvFilePath + "]がない。");
            }



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

        public static void Save(Form1 form1)
        {
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

                System.Console.WriteLine("ツリー書出し：" + sb.ToString());

                File.WriteAllText(
                    form1.UiMain1.GetTreeCsvFileName(form1.UiMain1.ProjectName, false),
                    sb.ToString(),
                    Encoding.GetEncoding("Shift_JIS")
                    );
            }
            catch (Exception e)
            {
                MessageBox.Show(form1, e.Message, "ツリー書き出し失敗。何らかのエラー");
            }


            if (File.Exists(form1.UiMain1.NodeFilePath))
            {
                //━━━━━
                //テキスト書出し
                //━━━━━
                File.WriteAllText(form1.UiMain1.NodeFilePath, form1.UiMain1.UiTextside1.TextareaText, Encoding.GetEncoding("Shift_JIS"));
                form1.UiMain1.NodeFileText = form1.UiMain1.UiTextside1.TextareaText;
                form1.UiMain1.TestChangeText();
            }
            else
            {
                MessageBox.Show(form1, form1.UiMain1.NodeFilePath, "ノードTXTファイル[" + form1.UiMain1.NodeFilePath + "]が無い");
            }


            try
            {
                //━━━━━
                //画像情報書出し
                //━━━━━
                if ("" != form1.UiMain1.CsvFilePath)
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

                    System.Console.WriteLine("CSVセーブ内容：" + sb.ToString());
                    System.Console.WriteLine("form1.UiMain1.CsvFilePath：" + form1.UiMain1.CsvFilePath);
                    File.WriteAllText(
                        form1.UiMain1.CsvFilePath,
                        sb.ToString(),
                        Encoding.GetEncoding("Shift_JIS")
                        );
                }
                else
                {
                    System.Console.WriteLine("form1.UiMain1.CsvFilePathが未指定。");
                }

                System.Console.WriteLine("セーブ： [" + form1.UiMain1.NodeFilePath + "]　[" + form1.UiMain1.CsvFilePath + "]");
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

    }
}
