using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
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
        /// プロジェクト・フォルダーを開く。
        /// </summary>
        /// <param name="form1"></param>
        public static void Open(Form1 form1)
        {
            FolderBrowserDialog dlg1 = new FolderBrowserDialog();
            dlg1.Description = "ツリーエディターのプロジェクト・フォルダーを開いてください。";
            DialogResult reslut1 = dlg1.ShowDialog();

            if (reslut1 == DialogResult.OK)
            {
                string dir = dlg1.SelectedPath;
                if ("" != dir)
                {
                    form1.UiMain1.OpenProject(dir);
                }
            }
        }

        /// <summary>
        /// プロジェクトの新規作成。
        /// </summary>
        public static void New(Form1 form1)
        {
            FolderBrowserDialog dlg1 = new FolderBrowserDialog();
            dlg1.Description = "空っぽのフォルダーを開いてください。\nそこをプロジェクト・フォルダーとして使います。";
            DialogResult reslut1 = dlg1.ShowDialog();

            if (reslut1 == DialogResult.OK)
            {
                //━━━━━
                // 空っぽのフォルダーの中に、ダミー・プロジェクトを作成。
                //━━━━━
                {
                    string folder = dlg1.SelectedPath;
                    if ("" != folder)
                    {
                        string[] files = Directory.GetFileSystemEntries(folder);
                        if (null != files && 0 < files.Length)
                        {
                            MessageBox.Show(form1, "何かが" + files.Length + "個、\n["+folder+"]フォルダーの中に入っています。\n空のフォルダーを選んでください。\n" + folder, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            form1.UiMain1.CreateDefaultProject(folder, true);
                            form1.UiMain1.OpenProject(folder);
                        }
                    }
                    else
                    {
                        goto gt_Abort;
                    }
                }
            }


            gt_Abort:
            dlg1.Dispose();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form1"></param>
        /// <param name="rel">RTFのプロジェクト・フォルダーからの相対ファイルパス</param>
        private static void LoadPageRtf(Form1 form1, string rel)
        {
            string abs = form1.UiMain1.GetFileFull(form1.UiMain1.Contents.ProjectFolder, rel, false);
            if (File.Exists(abs))
            {
                try
                {
                    //━━━━━
                    //テキスト読取り
                    //━━━━━
                    {
                        string contents = File.ReadAllText(abs, Encoding.GetEncoding("Shift_JIS"));
                        //ystem.Console.WriteLine("★RTFファイルを読み込みます。[" + abs + "]　[" + contents + "]");
                        form1.UiMain1.Contents.RtfNow( rel, contents);

                        form1.UiMain1.UiTextside1.TextareaRtf = contents;
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show(form1, "ノードRTFファイルが無い。\n[" + abs + "]", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }
        }

        private static void LoadPageCsv(Form1 form1, string rel)
        {
            Log_Method log_Method = new Log_MethodImpl();
            Log_ReportsImpl log_Reports = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod("--", "Actions", "LoadPageCsv", log_Reports);

            //━━━━━
            //画像情報読取り
            //━━━━━
            string abs = form1.UiMain1.GetFileFull(form1.UiMain1.Contents.ProjectFolder, rel, false);
            if (File.Exists(abs))
            {
                bool oldCsvMode = form1.UiMain1.UiTextside1.IsImageCsvMode;
                form1.UiMain1.UiTextside1.IsImageCsvMode = true;

                try
                {
                    string contents = File.ReadAllText(abs, Encoding.GetEncoding("Shift_JIS"));

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
                            //ystem.Console.WriteLine("NO=" + rNo + " EXPL=" + rExpl + " FILE=" + rFile + " X=" + rX + " Y=" + rY);

                            int xN;
                            int.TryParse(rX, out xN);

                            int yN;
                            int.TryParse(rY, out yN);

                            form1.UiMain1.UiTextside1.AddPicture(rFile, new Point(xN,yN), true);

                        }, log_Reports);

                        System.Console.WriteLine("★CSV読込完了 [" + abs + "]");
                        form1.UiMain1.Contents.CsvNow( contents);
                        form1.UiMain1.Contents.IsChangedCsv = false;
                        form1.UiMain1.RefreshTitleBar();
                    }
                    else
                    {
                        System.Console.WriteLine("★CSV読込失敗：" + log_Reports.ToText());
                    }
                }
                catch (Exception)
                {
                }

                form1.UiMain1.UiTextside1.IsImageCsvMode = oldCsvMode;
            }
            else
            {
                System.Console.WriteLine("★失敗：リソースCSV[" + abs + "]がない。");
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
        public static void LoadPage(Form1 form1, string rtfRel)
        {
            string csvRel = Utility.RtfToCsv(rtfRel);

            Log_Method log_Method = new Log_MethodImpl();
            Log_ReportsImpl log_Reports = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod("--", "Actions", "Load", log_Reports);

            System.Console.WriteLine("ロードページ　予想プロジェクト・フォルダー：" + form1.UiMain1.Contents.ProjectFolder);
            System.Console.WriteLine("ロードページ　予想ツリーファイル：" + form1.UiMain1.GetTreeCsvFile(form1.UiMain1.Contents.ProjectFolder, false));
            System.Console.WriteLine("ロードページ　予想RTFファイル：" + form1.UiMain1.GetFileFull(form1.UiMain1.Contents.ProjectFolder, rtfRel, false));
            System.Console.WriteLine("ロードページ　予想CSVファイル：" + form1.UiMain1.GetFileFull(form1.UiMain1.Contents.ProjectFolder, csvRel, false));

            Actions.LoadPageRtf(form1, rtfRel);

            Actions.LoadPageCsv(form1, csvRel);

            form1.UiMain1.UiTextside1.TextHistory.Clear();

            if (!log_Reports.Successful)
            {
                MessageBox.Show(form1, log_Reports.ToText(), "エラー");
            }
        }

        /// <summary>
        /// 再帰関数
        /// </summary>
        private static void RecursiveTree(Form1 form1, int autoNum, int tree, TreeNode tn, StringBuilder sb)
        {
            tree++;
            foreach (TreeNode2 tn2 in tn.Nodes)
            {
                string foreColorWeb = "#000000";
                string backColorWeb = "#FFFFFF";

                foreColorWeb = tn2.Fore.Web;
                backColorWeb = tn2.Back.Web;

                // TODO:ダブルクォーテーションで囲みたい。
                sb.Append(autoNum + ",," + tree + "," + tn2.SelectedImageIndex + "," + tn2.Text + "," + form1.UiMain1.CutHeadProject(tn2.RtfRel) + "," + foreColorWeb + "," + backColorWeb + ",");
                sb.Append(Environment.NewLine);
                autoNum++;

                Actions.RecursiveTree(form1, autoNum, tree, tn2, sb);
            }
            tree--;
        }

        /// <summary>
        /// RTF,CSVを書き出します。
        /// </summary>
        /// <param name="form1"></param>
        /// <param name="rtfRel"></param>
        /// <param name="contents"></param>
        private static void SaveRtfCsv(Form1 form1, string rtfRel, string rtfContents)
        {
            string csvRel = Utility.RtfToCsv(rtfRel);

            //━━━━━
            //RTF書出し
            //━━━━━
            {
                string rtfAbs = form1.UiMain1.GetFileFull(form1.UiMain1.Contents.ProjectFolder, rtfRel, false);

                if (File.Exists(rtfAbs))
                {

                    //ystem.Console.WriteLine("セーブ　予想テキスト：" + form1.UiMain1.UiTextside1.TextareaText);
                    File.WriteAllText(rtfAbs, rtfContents, Encoding.GetEncoding("Shift_JIS"));
                    form1.UiMain1.Contents.IsChangedRtf = false;
                    form1.UiMain1.Contents.RtfNow(rtfRel, rtfContents);
                    form1.UiMain1.RefreshTitleBar();
                }
                else
                {
                    MessageBox.Show(form1, "ノードRTFファイルが無い。[" + rtfAbs + "]", "警告");
                }
            }


            //━━━━━
            //画像情報書出し
            //━━━━━
            {
                string csvAbs = form1.UiMain1.GetFileFull(form1.UiMain1.Contents.ProjectFolder, csvRel, false);

                try
                {
                    if ("" != csvRel)
                    {
                        StringBuilder sb = new StringBuilder();
                        int autoNum = 0;

                        sb.Append("NO,EXPL,FILE,X,Y,EOL");
                        sb.Append(Environment.NewLine);

                        sb.Append("int,string,string,int,int,");
                        sb.Append(Environment.NewLine);

                        sb.Append("自動連番,解説,ファイルパス,左上座標,左上座標,");
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

                        string csvContents = sb.ToString();
                        //ystem.Console.WriteLine("セーブ　予想CSV：" + contents);

                        File.WriteAllText(
                            csvAbs,
                            csvContents,
                            Encoding.GetEncoding("Shift_JIS")
                            );
                        form1.UiMain1.Contents.IsChangedCsv = false;
                        form1.UiMain1.RefreshTitleBar();
                        form1.UiMain1.Contents.CsvNow( csvContents);
                    }
                    else
                    {
                        MessageBox.Show(form1, "form1.UiMain1.CsvFilePathが未指定。", "エラー");
                    }

                    System.Console.WriteLine("セーブ： [" + csvRel + "]");
                }
                catch (Exception)
                {
                }
            }

        }

        public static void Save(Form1 form1)
        {
            System.Console.WriteLine("セーブ　予想プロジェクト・フォルダー：" + form1.UiMain1.Contents.ProjectFolder);
            System.Console.WriteLine("セーブ　予想ツリーファイル：" + form1.UiMain1.GetTreeCsvFile(form1.UiMain1.Contents.ProjectFolder, false));
            System.Console.WriteLine("セーブ　予想RTFファイル：" + form1.UiMain1.GetFileFull(form1.UiMain1.Contents.ProjectFolder, form1.UiMain1.Contents.RtfFileRel, false));
            System.Console.WriteLine("セーブ　予想CSVファイル：" + form1.UiMain1.GetFileFull(form1.UiMain1.Contents.ProjectFolder, Utility.RtfToCsv(form1.UiMain1.Contents.RtfFileRel), false));

            try
            {
                //━━━━━
                //ツリー書出し
                //━━━━━
                StringBuilder sb = new StringBuilder();
                int autoNum = 0;

                sb.Append("NO,EXPL,TREE,ICON,TEXT,FILE,FORE_COLOR,BACK_COLOR,EOL");
                sb.Append(Environment.NewLine);

                sb.Append("int,string,int,int,string,string,string,string,");
                sb.Append(Environment.NewLine);

                sb.Append("自動連番,コメント,ツリー階層,,表示テキスト,ファイル,前景色,背景色,");
                sb.Append(Environment.NewLine);

                int tree = 1;
                foreach (TreeNode2 tn in form1.UiMain1.TreeView1.Nodes)
                {
                    string foreColorWeb = "#000000";
                    string backColorWeb = "#FFFFFF";

                    foreColorWeb = tn.Fore.Web;
                    backColorWeb = tn.Back.Web;

                    // TODO:文字列はダブルクォーテーションで囲まないと。
                    sb.Append(autoNum + ",," + tree + "," + tn.SelectedImageIndex + "," + tn.Text + "," + form1.UiMain1.CutHeadProject(tn.RtfRel) + "," + foreColorWeb + "," + backColorWeb + ",");
                    sb.Append(Environment.NewLine);
                    autoNum++;

                    Actions.RecursiveTree(form1, autoNum, tree, tn, sb);
                }

                sb.Append("EOF,,,,,,,,");
                sb.Append(Environment.NewLine);

                string contents = sb.ToString();
                //ystem.Console.WriteLine("セーブ　予想ツリー：" + contents);

                File.WriteAllText(
                    form1.UiMain1.GetTreeCsvFile(form1.UiMain1.Contents.ProjectFolder, false),
                    contents,
                    Encoding.GetEncoding("Shift_JIS")
                    );
                form1.UiMain1.Contents.IsChangedTree = false;
                form1.UiMain1.RefreshTitleBar();
            }
            catch (Exception e)
            {
                MessageBox.Show(form1, e.Message, "ツリー書き出し失敗。何らかのエラー");
            }


            Actions.SaveRtfCsv(
                form1,
                form1.UiMain1.Contents.RtfFileRel,
                form1.UiMain1.UiTextside1.TextareaRtf
                );
        }


        /// <summary>
        /// タイマーによるバックアップを取ります。
        /// 
        /// （１）ツリー
        /// （２）カレント・テキスト
        /// （３）カレント・ＣＳＶ
        /// 
        /// だけが対象です。
        /// </summary>
        public static void Buckup(UiMain uiMain)
        {

            //━━━━━
            // カレント・ツリーCSV
            //━━━━━
            try
            {
                //コピー先ディレクトリー
                string bkAbs = uiMain.GetTreeCsvFile(uiMain.Contents.ProjectFolder, true);
                string dir = Path.GetDirectoryName(bkAbs);
                System.Console.WriteLine("ツリー：bkAbs[" + bkAbs + "]　[" + dir + "]");

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.Copy(
                    uiMain.GetTreeCsvFile(uiMain.Contents.ProjectFolder, false),
                    bkAbs,
                    true
                    );
            }
            catch (Exception exc)
            {
                System.Console.WriteLine("ツリーCSVのバックアップに失敗　[" + uiMain.GetTreeCsvFile(uiMain.Contents.ProjectFolder, false) + "]→[" + uiMain.GetTreeCsvFile(uiMain.Contents.ProjectFolder, true) + "]　：" + exc.Message);
            }

            //━━━━━
            // カレントRTF
            //━━━━━
            try
            {
                //コピー先ディレクトリー
                string bkAbs = uiMain.GetFileFull(uiMain.Contents.ProjectFolder, uiMain.Contents.RtfFileRel,true);
                string dir = Path.GetDirectoryName(bkAbs);
                System.Console.WriteLine("RTF：bkAbs[" + bkAbs + "]　[" + dir + "]");

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.Copy(
                    uiMain.GetFileFull(uiMain.Contents.ProjectFolder, uiMain.Contents.RtfFileRel, false),
                    bkAbs,
                    true
                    );
            }
            catch (Exception exc)
            {
                System.Console.WriteLine("カレント・RTFのバックアップに失敗　[" + uiMain.GetFileFull(uiMain.Contents.ProjectFolder, uiMain.Contents.RtfFileRel, false) + "]→[" + uiMain.GetFileFull(uiMain.Contents.ProjectFolder, uiMain.Contents.RtfFileRel, true) + "]　：" + exc.Message);
            }
        }


        public static TreeNode2 AppendChildNode(UiMain uiMain1)
        {
            // ノード名を適当に作成。
            string nodeText;
            string nodeRel;
            {
                nodeText = Utility.CreateUniqueFileName(uiMain1);
                nodeRel = Utility.CreateRtfRel(nodeText);
            }

            TreeNode2 tn = uiMain1.CreateTreeNode(null, nodeText, nodeRel, 0, "#000000", "#FFFFFF");

            //RTF
            {
                string contents;
                uiMain1.CreateSampleRtf( nodeRel, out contents);
            }

            if (null == uiMain1.TreeView1.SelectedNode)
            {
            }
            else
            {
                uiMain1.TreeView1.SelectedNode.Nodes.Add(tn);
            }

            return tn;
        }


        /// <summary>
        /// 適当にノードを作成、末弟に追加。
        /// </summary>
        /// <param name="uiMain1"></param>
        /// <returns></returns>
        public static TreeNode2 AddNodeToSibling(UiMain uiMain1, TreeNode2 cloneSource)
        {
            // ノード名を適当に作成。


            bool isSibling = false;

            if (null == uiMain1.TreeView1.SelectedNode)
            {

            }
            else if (null != uiMain1.TreeView1.SelectedNode.Parent)
            {
                isSibling = true;
            }
            else
            {
            }

            string nodeText;
            string nodeRel;
            TreeNode2 tn = Utility.CreateNode(uiMain1, cloneSource, out nodeText, out nodeRel );

            if (isSibling)
            {
                //━━━━━
                //弟要素を追加。
                //━━━━━

                //RTF
                {
                    string contents;
                    uiMain1.CreateSampleRtf( nodeRel, out contents);
                }

                uiMain1.TreeView1.SelectedNode.Parent.Nodes.Add(tn);
                uiMain1.Contents.IsChangedTree = true;
                uiMain1.RefreshTitleBar();
            }
            else
            {
                //━━━━━
                //ルート要素を追加。
                //━━━━━

                //RTF
                {
                    string contents;
                    uiMain1.CreateSampleRtf(nodeRel, out contents);
                }

                // トップ階層の場合
                uiMain1.TreeView1.Nodes.Add(tn);
                uiMain1.Contents.IsChangedTree = true;
                uiMain1.RefreshTitleBar();
            }

            return tn;
        }

        /// <summary>
        /// 選択ノードを複製。
        /// </summary>
        /// <param name="uiMain1"></param>
        /// <returns></returns>
        public static TreeNode2 DuplicateNodeToSibling(UiMain uiMain1)
        {

            // カレントノード
            TreeNode2 tn0 = (TreeNode2)uiMain1.TreeView1.SelectedNode;

            // 複製ノード
            TreeNode2 tn = null;
            if (null != tn0)
            {
                // 末弟に、クローン・ノード（タグは新しい値を割り振り）を追加。
                tn = Actions.AddNodeToSibling(uiMain1, tn0);


                //━━━━━
                //ファイルをダミー名に変更します。
                //━━━━━
                string fileName = Utility.CreateUniqueFileName(uiMain1);
                string rtfRel = Utility.CreateRtfRel(fileName);


                //━━━━━
                //RTF、CSVファイルを複製します。
                //━━━━━
                {
                    string srcRtfAbs = uiMain1.GetFileFull(uiMain1.Contents.ProjectFolder, tn.RtfRel, false);
                    string dstRtfAbs = uiMain1.GetFileFull(uiMain1.Contents.ProjectFolder, rtfRel, false);
                    File.Copy(srcRtfAbs, dstRtfAbs);

                    string srcCsvAbs = uiMain1.GetFileFull(uiMain1.Contents.ProjectFolder, Utility.RtfToCsv(tn.RtfRel), false);
                    string dstCsvAbs = uiMain1.GetFileFull(uiMain1.Contents.ProjectFolder, Utility.RtfToCsv(rtfRel), false);
                    File.Copy(srcCsvAbs, dstCsvAbs);
                }


                tn.RtfRel = rtfRel;
                // ファイル名を複製したが、複製なので構わない。

            }

            return tn;
        }

        public static void MoveTreeNodeInSiblings(UiMain uiMain1, Boolean upward)
        {
            //要素をCloneしてコピーすると比較的簡単に入れ替えられる//

            TreeNode2 nodeClone = (TreeNode2)uiMain1.TreeView1.SelectedNode.Clone();
            TreeNode2 node = (TreeNode2)uiMain1.TreeView1.SelectedNode;
            TreeNode2 fowardNode = upward ? (TreeNode2)node.PrevNode : (TreeNode2)node.NextNode;

            if (node.Parent == null)
            {
                MessageBox.Show("最上位の要素の入れ替えはできません");
                return;
            }
            else if (null == fowardNode)
            {
                MessageBox.Show("移動先がありません。");
                return;
            }

            //参照のコピー
            TreeNode2 ParentClone = (TreeNode2)node.Parent.Clone();
            System.Collections.IEnumerator en = ParentClone.Nodes.GetEnumerator();
            TreeNode2 ParentRef = (TreeNode2)node.Parent;

            int nodeHash = node.Tag.GetHashCode();//タグにデータが入ってること前提
            int fowardHash = fowardNode.Tag.GetHashCode();

            node.Parent.Nodes.Clear();//入れ替えのために消す

            uiMain1.TreeView1.Visible = false;//描画対策
            //基本的にクローンした要素に差し替える工程
            while (en.MoveNext())
            {
                TreeNode2 tn = (TreeNode2)en.Current;
                if (fowardHash == tn.Tag.GetHashCode())
                {
                    if (upward)
                    {
                        ParentRef.Nodes.Add(nodeClone);
                        ParentRef.Nodes.Add(tn);
                    }
                    else
                    {
                        ParentRef.Nodes.Add(tn);
                        ParentRef.Nodes.Add(nodeClone);
                    }

                }
                else if (nodeHash == tn.Tag.GetHashCode())
                {
                    /*Skip*/
                }
                else
                {
                    ParentRef.Nodes.Add(tn);
                }
            }
            uiMain1.TreeView1.Visible = true;
            uiMain1.TreeView1.SelectedNode = nodeClone;
            uiMain1.TreeView1.Focus();

            // ツリーに更新があったものと判定します。
            uiMain1.Contents.IsChangedTree = true;
            uiMain1.RefreshTitleBar();
        }

        /// <summary>
        /// 親ノードへ移動
        /// </summary>
        /// <param name="upward"></param>
        public static void MoveTreeNodeToParent(UiMain uiMain1)
        {
            //要素をCloneしてコピーすると比較的簡単に入れ替えられる//

            TreeNode2 nodeClone = (TreeNode2)uiMain1.TreeView1.SelectedNode.Clone();
            TreeNode2 node = (TreeNode2)uiMain1.TreeView1.SelectedNode;
            TreeNode2 parentNode = (TreeNode2)node.Parent;

            if (null == parentNode)
            {
                MessageBox.Show("移動先がありません。");
                return;
            }

            if (null == parentNode.Parent)
            {
                // ルートノードを増やします。

                //描画対策付き
                {
                    uiMain1.TreeView1.Visible = false;//描画対策

                    //元の位置を消して
                    parentNode.Nodes.Remove(node);

                    //ルートのクローンを追加します。
                    uiMain1.TreeView1.Nodes.Add(nodeClone);

                    uiMain1.TreeView1.Visible = true;
                }
            }
            else
            {
                //親の親
                TreeNode2 parentParentNode = (TreeNode2)parentNode.Parent;

                //描画対策付き
                {
                    uiMain1.TreeView1.Visible = false;//描画対策

                    //元の位置を消して
                    parentNode.Nodes.Remove(node);

                    //移動先にクローンを追加します。
                    parentParentNode.Nodes.Add(nodeClone);

                    uiMain1.TreeView1.Visible = true;
                }
            }


            uiMain1.TreeView1.SelectedNode = nodeClone;
            uiMain1.TreeView1.Focus();

            // ツリーに更新があったものと判定します。
            uiMain1.Contents.IsChangedTree = true;
            uiMain1.RefreshTitleBar();
        }

        public static void SetNodeProperty(UiMain uiMain1)
        {
            if (null != uiMain1.TreeView1.SelectedNode && uiMain1.TreeView1.SelectedNode is TreeNode2)
            {
                TreeNode2 tn = (TreeNode2)uiMain1.TreeView1.SelectedNode;
                NodePropertyDialog dlg = new NodePropertyDialog();

                dlg.SelectedImageIndex = uiMain1.TreeView1.SelectedNode.SelectedImageIndex;

                dlg.ForeRed = tn.Fore.Red;
                dlg.ForeGreen = tn.Fore.Green;
                dlg.ForeBlue = tn.Fore.Blue;
                dlg.BackRed = tn.Back.Red;
                dlg.BackGreen = tn.Back.Green;
                dlg.BackBlue = tn.Back.Blue;

                dlg.NodeText = uiMain1.TreeView1.SelectedNode.Text;
                dlg.NodeFile = ((TreeNode2)uiMain1.TreeView1.SelectedNode).RtfRel;

                DialogResult result = dlg.ShowDialog((Form1)uiMain1.ParentForm);

                bool isSuccessful = true;
                try
                {
                    System.IO.Path.GetFullPath(dlg.NodeFile);
                }
                catch (Exception)
                {
                    // ファイル名に使えない文字や、文字数が長すぎた時に例外が投げられます。
                    Form1 form1 = (Form1)uiMain1.ParentForm;
                    MessageBox.Show(form1, "ファイル名に使えないファイル名？\n[" + dlg.NodeFile + "]\n処理を中断します。", "何らかのエラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    isSuccessful = false;
                }

                if (isSuccessful)
                {
                    // ツリーが更新されたと判定。
                    uiMain1.Contents.IsChangedTree = true;

                    uiMain1.TreeView1.SelectedNode.ImageIndex = dlg.SelectedImageIndex;
                    uiMain1.TreeView1.SelectedNode.SelectedImageIndex = dlg.SelectedImageIndex;

                    tn.Fore.Red = dlg.ForeRed;
                    tn.Fore.Green = dlg.ForeGreen;
                    tn.Fore.Blue = dlg.ForeBlue;
                    tn.Back.Red = dlg.BackRed;
                    tn.Back.Green = dlg.BackGreen;
                    tn.Back.Blue = dlg.BackBlue;
                    uiMain1.TreeView1.SelectedNode.ForeColor = Color.FromArgb(255, dlg.ForeRed, dlg.ForeGreen, dlg.ForeBlue);
                    uiMain1.TreeView1.SelectedNode.BackColor = Color.FromArgb(255, dlg.BackRed, dlg.BackGreen, dlg.BackBlue);

                    ((TreeNode2)uiMain1.TreeView1.SelectedNode).Text = dlg.NodeText;
                    if (((TreeNode2)uiMain1.TreeView1.SelectedNode).RtfRel != dlg.NodeFile)
                    {
                        //━━━━━
                        // ファイル名を変更した場合
                        //━━━━━
                        Actions.ChangeRtfRel(uiMain1, (TreeNode2)uiMain1.TreeView1.SelectedNode, dlg.NodeFile);
                    }
                }

                dlg.Dispose();
            }
        }

        /// <summary>
        /// RTF,CSVファイルを変えた後の処理。
        /// 
        /// リネーム等を行う。ただし、複製用途の場合は　これを使わない。
        /// </summary>
        /// <param name="uiMain1"></param>
        /// <param name="selectedNode"></param>
        /// <param name="newNodeFile"></param>
        private static void ChangeRtfRel(UiMain uiMain1, TreeNode2 selectedNode, string newRtfRel)
        {
            //　　　　　　　　　　　　移動先ファイルがない　　　移動先ファイルがある
            //　　　　　　　　　　┌────────────┬────────────┐
            //　　　　　　　　　　│　　　　　　　　　　　　│　　　　　　　　　　　│
            //移動元ファイルがない│　　　　　（１）　　　　│　　　　　（２）　　　│
            //　　　　　　　　　　│　　　　　　　　　　　　│　　　　　　　　　　　│
            //　　　　　　　　　　├────────────┼────────────┤
            //　　　　　　　　　　│　　　　　　　　　　　　│　　　　　　　　　　　│
            //移動元ファイルがある│　　　　　（３）　　　　│　　　　　（４）　　　│
            //　　　　　　　　　　│　　　　　　　　　　　　│　　　　　　　　　　　│
            //　　　　　　　　　　└────────────┴────────────┘


            //移動元ファイル名
            string srcRtfRel = selectedNode.RtfRel;
            string srcRtfAbs = uiMain1.GetFileFull(uiMain1.Contents.ProjectFolder, srcRtfRel, false);
            //移動先ファイル名
            string dstRtfRel = newRtfRel;
            string dstRtfAbs = uiMain1.GetFileFull(uiMain1.Contents.ProjectFolder, dstRtfRel, false);

            //　　　　　　　　　　　　　　　　　　（１）　　　（２）　　　（３）　　　（４）
            //　　　　　　　　　　　　　　　　┌─────┬─────┬─────┬─────┐
            //　　　　　　　　　　　　　　　　│　　　　　│　　　　　│　　　　　│　　　　　│
            //（Ａ）保存を要求し、　　　　　　│　　　　　│　　○　　│　　　　　│　　○　　│
            //　　　　断れば中断する　　　　　│　　　　　│　　　　　│　　　　　│　　　　　│
            //　　　　　　　　　　　　　　　　│　　　　　│　　　　　│　　　　　│　　　　　│
            //（Ｂ）新規作成し、　　　　　　　│　　○　　│　　　　　│　　　　　│　　　　　│
            //　　　　カレントとする　　　　　│　　　　　│　　　　　│　　　　　│　　　　　│
            //　　　　　　　　　　　　　　　　│　　　　　│　　　　　│　　　　　│　　　　　│
            //（Ｃ）移動先ファイルをロードし　│　　　　　│　　○　　│　　　　　│　　○　　│
            //　　　　カレントとする　　　　　│　　　　　│　　　　　│　　　　　│　　　　　│
            //　　　　　　　　　　　　　　　　│　　　　　│　　　　　│　　　　　│　　　　　│
            //（Ｄ）移動元ファイルをリネームし│　　　　　│　　　　　│　　○　　│　　　　　│
            //　　　　移動先をカレントとする　│　　　　　│　　　　　│　　　　　│　　　　　│
            //　　　　　　　　　　　　　　　　│　　　　　│　　　　　│　　　　　│　　　　　│
            //　　　　　　　　　　　　　　　　└─────┴─────┴─────┴─────┘

            bool rtfA = false;
            bool rtfB = false;
            bool rtfC = false;
            bool rtfD = false;

            if (!File.Exists(srcRtfAbs))
            {
                if (!File.Exists(dstRtfAbs))
                {
                    //（１）
                    //ystem.Console.WriteLine("RTF（１）と判断");
                    rtfB = true;
                }
                else
                {
                    //（２）
                    //ystem.Console.WriteLine("RTF（２）と判断");
                    rtfA = true;
                    rtfC = true;
                }
            }
            else
            {
                if (!File.Exists(dstRtfAbs))
                {
                    //（３）
                    //ystem.Console.WriteLine("RTF（３）と判断");
                    rtfD = true;
                }
                else
                {
                    //（４）
                    //ystem.Console.WriteLine("RTF（４）と判断");
                    rtfA = true;
                    rtfC = true;
                }
            }

            Form1 form1 = (Form1)uiMain1.ParentForm;

            //─────
            //（Ａ）保存を要求し、
            //　　　　断れば中断する
            //─────
            if (rtfA)
            {
                DialogResult result2 = MessageBox.Show(form1, "ノード名が変更されました。\n処理を続けるには、現在開いているファイルを保存する必要があります。\n[" + srcRtfAbs + "]\n保存しますか？", "警告", MessageBoxButtons.YesNoCancel);
                switch (result2)
                {
                    case DialogResult.Yes:
                        //保存して続行
                        Actions.Save(form1);
                        break;
                    case DialogResult.No:
                        //保存せずに続行
                        break;
                    default:
                        //中断
                        goto gt_Abort;
                }
            }


            //─────
            //（Ｂ）新規作成し、
            //　　　　カレントとする
            //─────
            if (rtfB)
            {
                string contents = "";
                uiMain1.CreateSampleRtf(srcRtfRel, out contents);
                uiMain1.Contents.RtfNow(srcRtfRel, contents);
            }


            //─────
            //（Ｃ）移動先ファイルをロードし
            //　　　　カレントとする
            //─────
            if (rtfC)
            {
                // 既にあるテキストファイル
                MessageBox.Show("既にあるテキストファイルを読み込みます。テキスト[" + dstRtfAbs + "]");
                Actions.LoadPageRtf(form1, dstRtfRel);
            }


            //─────
            //（Ｄ）移動元ファイルをリネームし
            //　　　　移動先をカレントとする
            //─────
            if (rtfD)
            {
                try
                {
                    File.Move(srcRtfAbs, dstRtfAbs);
                }
                catch (Exception e)
                {
                    MessageBox.Show(form1, "[" + dstRtfAbs + "]\n　エラー："+e.ToString(), "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                uiMain1.Contents.RtfNow( dstRtfRel, uiMain1.Contents.SavedRtf);
            }


            //━━━━━
            // ノード名の変更
            //━━━━━
            selectedNode.RtfRel = dstRtfRel;
            //selectedNode.Text = newNode;//ツリービュー
            //uiMain1.UiTextside1.NodeNameTxt1.Text = newNode;//ノード名表示

        gt_Abort://処理を中断した場合の飛び先。
            ;
        }

        /// <summary>
        /// ノードの削除
        /// </summary>
        public static void DeleteNode(UiMain uiMain1)
        {
            uiMain1.TreeView1.Nodes.Remove(uiMain1.TreeView1.SelectedNode);
            uiMain1.Contents.RtfNow("", "{\rtf1}");

            uiMain1.UiTextside1.Clear();
        }


        public static void ImportStoryEditor(UiMain uiMain1)
        {

            Form1 form1 = (Form1)uiMain1.ParentForm;

            bool isSuccessful = true;

            //階層付きテキスト
            string importFile="";

            // 階層付きテキストを読取り
            if (isSuccessful)
            {
                OpenFileDialog dlg1 = new OpenFileDialog();

                //はじめに「ファイル名」で表示される文字列を指定する
                dlg1.FileName = "default.TXT";

                //はじめに表示されるフォルダを指定する
                //指定しない（空の文字列）の時は、現在のディレクトリが表示される
                //dlg.InitialDirectory = @"C:\";

                //[ファイルの種類]に表示される選択肢を指定する
                //指定しないとすべてのファイルが表示される
                dlg1.Filter = "TXTファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";

                //[ファイルの種類]ではじめに選択されているフィルター
                dlg1.FilterIndex = 1;

                //タイトルを設定する
                dlg1.Title = "階層付きテキストを選択してください";

                //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                dlg1.RestoreDirectory = true;

                //存在しないファイルの名前が指定されたとき警告を表示する
                //デフォルトでTrueなので指定する必要はない
                dlg1.CheckFileExists = true;

                //存在しないパスが指定されたとき警告を表示する
                //デフォルトでTrueなので指定する必要はない
                dlg1.CheckPathExists = true;

                //ダイアログを表示する
                if (dlg1.ShowDialog() == DialogResult.OK)
                {
                    importFile = dlg1.FileName;
                }
                else
                {
                    isSuccessful = false;
                }

                dlg1.Dispose();
            }

            string folder="";

            // プロジェクトを選択
            if (isSuccessful)
            {
                FolderBrowserDialog dlg2 = new FolderBrowserDialog();
                dlg2.Description = "空っぽのフォルダーを開いてください。\nそこに階層付きテキストを変換した内容を展開します。";
                DialogResult reslut1 = dlg2.ShowDialog();

                if (reslut1 == DialogResult.OK)
                {
                    //━━━━━
                    // 空っぽのフォルダーの中に、ダミー・プロジェクトを作成。
                    //━━━━━
                    folder = dlg2.SelectedPath;
                }

                dlg2.Dispose();
            }

            if (isSuccessful)
            {
                if ("" != folder)
                {
                    string nodeText = "";
                    string rtfRel2 = "";
                    string csvRel2 = "";

                    string[] files = Directory.GetFileSystemEntries(folder);
                    if (null != files && 0 < files.Length)
                    {
                        MessageBox.Show(form1, "何かフォルダーの中に入っています。\n空のフォルダーを選んでください。\n" + folder, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        uiMain1.Clear();

                        uiMain1.CreateDefaultProject(folder, false);
                        uiMain1.OpenProject(folder);


                        string[] lines = System.IO.File.ReadAllLines(importFile, Encoding.GetEncoding("Shift_JIS"));
                        int n = 0;//自動連番
                        int tree = 1;
                        List<TreeNode2> breadcrumb = new List<TreeNode2>();
                        breadcrumb.Add(null);//[0]
                        breadcrumb.Add(null);//[1]
                        StringBuilder buffer = new StringBuilder();
                        //連続するドットの数
                        Regex reg = new Regex(@"^(\.+).*", RegexOptions.Compiled | RegexOptions.Singleline);
                        foreach (string line in lines)
                        {
                            string line2 = line.Trim();
                            if (line2.StartsWith("."))
                            {
                                if (0 < buffer.Length)
                                {
                                    // 溜まっているテキストを出力
                                    RichTextBox rtb = new RichTextBox();
                                    rtb.Text = buffer.ToString();
                                    Actions.SaveRtfCsv(form1, rtfRel2, rtb.Rtf);
                                    buffer.Clear();
                                }

                                MatchCollection mc = reg.Matches(line2);
                                int dots = 0;
                                foreach (Match m in mc)
                                {
                                    dots = m.Groups[1].Length;
                                }

                                TreeNode2 tn;
                                if (tree == dots)
                                {
                                    //━━━━━
                                    //同階層
                                    //━━━━━

                                    //末弟
                                    tn = Actions.AddNodeToSibling(uiMain1,null);
                                    uiMain1.TreeView1.SelectedNode = tn;

                                    breadcrumb.Insert(tree, tn);

                                    //以前の末弟を削除
                                    breadcrumb.RemoveAt(breadcrumb.Count - 1);
                                    //breadcrumb.RemoveRange( tree+1, breadcrumb.Count-tree-1);
                                    //ystem.Console.WriteLine("（１）末弟　dots[" + dots + "] tree[" + tree + "] tree+1[" + (tree + 1) + "] breadcrumb.Count-tree-1[" + (breadcrumb.Count - tree - 1) + "] breadcrumb.Count[" + breadcrumb.Count+"]");
                                }
                                else if (tree + 1 == dots)
                                {
                                    //━━━━━
                                    //子階層
                                    //━━━━━

                                    //子
                                    tn = Actions.AppendChildNode(uiMain1);
                                    uiMain1.TreeView1.SelectedNode = tn;
                                    tree++;
                                    breadcrumb.Insert(tree, tn);

                                    //breadcrumb.RemoveRange(tree + 1, breadcrumb.Count - tree - 1);
                                    //ystem.Console.WriteLine("（２）子　dots[" + dots + "] tree[" + tree + "] tree+1[" + (tree + 1) + "] breadcrumb.Count-tree-1[" + (breadcrumb.Count - tree - 1) + "] breadcrumb.Count[" + breadcrumb.Count + "]");
                                }
                                else
                                {
                                    //━━━━━
                                    //遡り階層
                                    //━━━━━

                                    //兄を選択
                                    TreeNode2 elder = breadcrumb[dots];
                                    uiMain1.TreeView1.SelectedNode = elder;

                                    //遡る数
                                    int remove = tree - dots;
                                    //兄より下をカット
                                    breadcrumb.RemoveRange(dots + 1, remove);



                                    //遡って末弟
                                    tn = Actions.AddNodeToSibling(uiMain1,null);
                                    uiMain1.TreeView1.SelectedNode = tn;
                                    breadcrumb.Insert(dots, tn);

                                    //（３）遡り　dots[2] tree[2] 親[1] tree+1[3] breadcrumb.Count-tree-1[0] breadcrumb.Count[3] remove[2]
                                    //ystem.Console.WriteLine("（３）遡り　dots[" + dots + "] 兄「" + elder.Text + "」 tree[" + tree + "] breadcrumb.Count[" + breadcrumb.Count + "] remove[" + remove + "]");
                                    tree = dots;
                                }

                                //ノードテキスト：頭のドットを除去します。
                                nodeText = line2.Substring(dots, line2.Length - dots);
                                tn.Text = nodeText;

                                //RTFファイル
                                {
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append(@"node\(");
                                    sb.Append(n);
                                    n++;
                                    sb.Append(")");

                                    sb.Append(nodeText);
                                    sb.Append(".rtf");
                                    rtfRel2 = sb.ToString();
                                }

                                //CSVファイル
                                {
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append(@"node\(");
                                    sb.Append(n);
                                    n++;
                                    sb.Append(")");

                                    sb.Append(nodeText);
                                    sb.Append(".csv");
                                    csvRel2 = sb.ToString();
                                }

                                Actions.ChangeRtfRel(uiMain1, tn, rtfRel2);
                            }
                            else
                            {
                                buffer.AppendLine(line);
                            }
                        }

                        if (0 < buffer.Length)
                        {
                            // 溜まっているテキストを出力
                            RichTextBox rtb = new RichTextBox();
                            rtb.Text = buffer.ToString();
                            Actions.SaveRtfCsv(form1, rtfRel2, rtb.Rtf);
                            buffer.Clear();
                        }
                    }

                    //最後のページを、開いた状態にします。
                    Actions.LoadPage(form1, rtfRel2);
                    //保存します。主にTree.csvのために。
                    Actions.Save(form1);
                }
                else
                {
                }

            }
        }

    }
}
