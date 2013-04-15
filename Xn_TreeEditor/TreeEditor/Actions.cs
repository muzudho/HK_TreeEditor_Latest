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

        public static void Load(Form1 form1,string textFilePath, string csvFilePath)
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
                        form1.UiMain1.TextFilePath = textFilePath;
                        form1.UiMain1.CsvFilePath = csvFilePath;
                        form1.UiMain1.FileText = text;
                        form1.UiMain1.UiTextside1.TextareaText = text;
                    }


                    //━━━━━
                    //画像情報読取り
                    //━━━━━
                    {
                        if (File.Exists(csvFilePath))
                        {
                            try
                            {
                                string text = File.ReadAllText(csvFilePath, Encoding.GetEncoding("Shift_JIS"));

                                CsvTo_TableImpl csvTo = new CsvTo_TableImpl();
                                Request_ReadsTableImpl request = new Request_ReadsTableImpl();
                                //request.Name_PutToTable = @"save\"+this.ProjectName+"\TREE.csv";
                                Format_TableImpl format = new Format_TableImpl();
                                Table_Humaninput table = csvTo.Read(
                                    text,
                                    request,
                                    format,
                                    log_Reports
                                    );
                                if (log_Reports.Successful)
                                {
                                    System.Console.WriteLine("★CSV読込完了");

                                    List<TreeNode> treeNodeList = new List<TreeNode>();
                                    treeNodeList.Add(null);//[0]はヌル。
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
                                            pic1.Location.X + pic1.Width/2,
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
                            System.Console.WriteLine("★失敗：Tree.csvがない。");
                        }
                    }

                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show(form1, textFilePath, "ファイルが無い");
            }

            if (!log_Reports.Successful)
            {
                MessageBox.Show(form1, log_Reports.ToText(), "エラー");
            }
        }

        public static void Save(Form1 form1)
        {
            if (File.Exists(form1.UiMain1.TextFilePath))
            {
                try
                {
                    //━━━━━
                    //テキスト書出し
                    //━━━━━
                    File.WriteAllText(form1.UiMain1.TextFilePath, form1.UiMain1.UiTextside1.TextareaText, Encoding.GetEncoding("Shift_JIS"));
                    form1.UiMain1.FileText = form1.UiMain1.UiTextside1.TextareaText;
                    form1.UiMain1.TestChangeText();


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
                                if (fp.StartsWith(Application.StartupPath+@"\"))
                                {
                                    fp = fp.Substring(Application.StartupPath.Length+@"\".Length);
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

                    System.Console.WriteLine("セーブ： [" + form1.UiMain1.TextFilePath + "]　[" + form1.UiMain1.CsvFilePath + "]");
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show(form1, form1.UiMain1.TextFilePath, "ファイルが無い");
            }
        }

    }
}
