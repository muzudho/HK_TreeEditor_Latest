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
        /// プロジェクト・フォルダーを開く。
        /// </summary>
        /// <param name="form1"></param>
        public static void Open(Form1 form1)
        {
            NewProjectDialog dlg = new NewProjectDialog();
            dlg.UiNewProject.IsNew = false;
            dlg.ShowDialog(form1);


            string dir = dlg.UiNewProject.NewProjectFolderTxt1.Text;
            if ("" != dir)
            {
                form1.UiMain1.OpenProject(dir);
            }
        }

        /// <summary>
        /// プロジェクトの新規作成。
        /// </summary>
        public static void New(Form1 form1)
        {
            NewProjectDialog dlg = new NewProjectDialog();
            dlg.UiNewProject.IsNew = true;//新規作成用
            dlg.ShowDialog(form1);

            //━━━━━
            // 空っぽのフォルダーの中に、ダミー・プロジェクトを作成。
            //━━━━━
            {
                string dir = dlg.UiNewProject.NewProjectFolderTxt1.Text;
                if ("" != dir)
                {
                    string[] files = Directory.GetFileSystemEntries(dir);
                    if (null != files && 0 < files.Length)
                    {
                        MessageBox.Show("何かフォルダーの中に入っています。\n空のフォルダーを選んでください。\n" + dir);
                    }
                    else
                    {
                        form1.UiMain1.CreateDefaultProject(dir);
                        form1.UiMain1.OpenProject(dir);
                    }
                }
                else
                {
                    goto gt_Abort;
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

            gt_Abort:
            dlg.Dispose();

        }

        public static void LoadPageRtf(Form1 form1, string file)
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
                        //ystem.Console.WriteLine("★RTFファイルを読み込みます。[" + file + "]　[" + contents + "]");
                        form1.UiMain1.Contents.RtfNow(file, contents);

                        form1.UiMain1.UiTextside1.TextareaRtf = contents;
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show(form1, file, "ノードRTFファイル[" + file + "]が無い");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="form1"></param>
        /// <param name="textFile">ファイルパス。</param>
        /// <param name="csvFile">画像情報用ファイルパス。</param>
        public static void LoadPage(Form1 form1, string rtfFile)
        {
            Log_Method log_Method = new Log_MethodImpl();
            Log_ReportsImpl log_Reports = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod("--", "Actions", "Load", log_Reports);

            System.Console.WriteLine("ロードページ　予想プロジェクト・フォルダー：" + form1.UiMain1.Contents.ProjectFolder);
            System.Console.WriteLine("ロードページ　予想ツリーファイル：" + form1.UiMain1.GetTreeCsvFile(form1.UiMain1.Contents.ProjectFolder, false));
            System.Console.WriteLine("ロードページ　予想RTFファイル：" + rtfFile);

            Actions.LoadPageRtf(form1,rtfFile);

            form1.UiMain1.UiTextside1.TextHistory.Clear();

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
            System.Console.WriteLine("セーブ　予想プロジェクト・フォルダー：" + form1.UiMain1.Contents.ProjectFolder);
            System.Console.WriteLine("セーブ　予想ツリーファイル：" + form1.UiMain1.GetTreeCsvFile(form1.UiMain1.Contents.ProjectFolder, false));
            System.Console.WriteLine("セーブ　予想RTFファイル：" + form1.UiMain1.Contents.RtfFile);

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


            if (File.Exists(form1.UiMain1.Contents.RtfFile))
            {
                //━━━━━
                //RTF書出し
                //━━━━━

                //ystem.Console.WriteLine("セーブ　予想テキスト：" + form1.UiMain1.UiTextside1.TextareaText);
                File.WriteAllText(form1.UiMain1.Contents.RtfFile, form1.UiMain1.UiTextside1.TextareaRtf, Encoding.GetEncoding("Shift_JIS"));
                form1.UiMain1.Contents.IsChangedRtf = false;
                form1.UiMain1.Contents.RtfNow(form1.UiMain1.Contents.RtfFile, form1.UiMain1.UiTextside1.TextareaRtf);
                form1.UiMain1.RefreshTitleBar();
            }
            else
            {
                MessageBox.Show(form1, "ノードRTFファイル[" + form1.UiMain1.Contents.RtfFile + "]が無い", "警告");
            }
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
                string file = uiMain.GetTreeCsvFile(uiMain.Contents.ProjectFolder, true);
                string dir = Path.GetDirectoryName(file);
                System.Console.WriteLine("ツリー：file[" + file + "]　[" + dir + "]");

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.Copy(
                    uiMain.GetTreeCsvFile(uiMain.Contents.ProjectFolder, false),
                    file,
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
                string file = uiMain.GetRtfFile(uiMain.Contents.ProjectFolder, uiMain.UiTextside1.NodeNameTxt1.Text, true);
                string dir = Path.GetDirectoryName(file);
                System.Console.WriteLine("RTF：file[" + file + "]　[" + dir + "]");

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.Copy(
                    uiMain.GetRtfFile(uiMain.Contents.ProjectFolder, uiMain.UiTextside1.NodeNameTxt1.Text, false),
                    file,
                    true
                    );
            }
            catch (Exception exc)
            {
                System.Console.WriteLine("カレント・RTFのバックアップに失敗　[" + uiMain.GetRtfFile(uiMain.Contents.ProjectFolder, uiMain.UiTextside1.NodeNameTxt1.Text, false) + "]→[" + uiMain.GetRtfFile(uiMain.Contents.ProjectFolder, uiMain.UiTextside1.NodeNameTxt1.Text, true) + "]　：" + exc.Message);
            }
        }


        public static void AppendChildNode(UiMain uiMain1)
        {
            // ノード名を適当に作成。
            string nodeName;
            {
                //タイムスタンプ
                StringBuilder s = new StringBuilder();

                DateTime now = System.DateTime.Now;
                s.Append(String.Format("{0:D4}", now.Year));
                s.Append(String.Format("{0:D2}", now.Month));
                s.Append(String.Format("{0:D2}", now.Day));
                s.Append("_");
                s.Append(String.Format("{0:D2}", now.Hour));
                s.Append(String.Format("{0:D2}", now.Minute));
                s.Append(String.Format("{0:D2}", now.Second));
                s.Append("_");
                s.Append(String.Format("{0:D3}", now.Millisecond));

                nodeName = s.ToString();
            }


            if (null == uiMain1.TreeView1.SelectedNode)
            {

            }
            else
            {
                TreeNode2 tn = uiMain1.NewTreeNode(nodeName, 0, "#000000", "#FFFFFF");

                //RTF
                {
                    string contents;
                    uiMain1.CreateSampleTextFile1(uiMain1.GetRtfFile(uiMain1.Contents.ProjectFolder, nodeName, false), out contents);
                }

                uiMain1.TreeView1.SelectedNode.Nodes.Add(tn);
            }
        }


        public static void AddNodeToSibling(UiMain uiMain1)
        {
            // ノード名を適当に作成。
            string nodeName;
            {
                //タイムスタンプ
                StringBuilder s = new StringBuilder();

                DateTime now = System.DateTime.Now;
                s.Append(String.Format("{0:D4}", now.Year));
                s.Append(String.Format("{0:D2}", now.Month));
                s.Append(String.Format("{0:D2}", now.Day));
                s.Append("_");
                s.Append(String.Format("{0:D2}", now.Hour));
                s.Append(String.Format("{0:D2}", now.Minute));
                s.Append(String.Format("{0:D2}", now.Second));
                s.Append("_");
                s.Append(String.Format("{0:D3}", now.Millisecond));

                nodeName = s.ToString();
            }


            if (null == uiMain1.TreeView1.SelectedNode)
            {

            }
            else if (null != uiMain1.TreeView1.SelectedNode.Parent)
            {
                TreeNode2 tn = uiMain1.NewTreeNode(nodeName, 0, "#000000", "#FFFFFF");

                //RTF
                {
                    string contents;
                    uiMain1.CreateSampleTextFile1(uiMain1.GetRtfFile(uiMain1.Contents.ProjectFolder, nodeName, false), out contents);
                }

                uiMain1.TreeView1.SelectedNode.Parent.Nodes.Add(tn);
            }
            else
            {
                TreeNode2 tn = uiMain1.NewTreeNode(nodeName, 0, "#000000", "#FFFFFF");

                //RTF
                {
                    string contents;
                    uiMain1.CreateSampleTextFile1(uiMain1.GetRtfFile(uiMain1.Contents.ProjectFolder, nodeName, false), out contents);
                }

                // トップ階層の場合
                uiMain1.TreeView1.Nodes.Add(tn);
                //System.Console.WriteLine("親要素がないので、弟ノードを足せません。");
            }
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

                dlg.NodeNameText = uiMain1.TreeView1.SelectedNode.Text;

                DialogResult result = dlg.ShowDialog((Form1)uiMain1.ParentForm);

                bool isSuccessful = true;
                try
                {
                    System.IO.Path.GetFullPath(dlg.NodeNameText);
                }
                catch (Exception)
                {
                    // ファイル名に使えない文字や、文字数が長すぎた時に例外が投げられます。
                    Form1 form1 = (Form1)uiMain1.ParentForm;
                    MessageBox.Show(form1, "ファイル名に使えない名前？\n[" + dlg.NodeNameText + "]\n処理を中断します。", "何らかのエラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

                    if (uiMain1.TreeView1.SelectedNode.Text != dlg.NodeNameText)
                    {
                        //━━━━━
                        // ノード名を変更した場合
                        //━━━━━

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
                        string srcRtfFile = uiMain1.GetRtfFile(uiMain1.Contents.ProjectFolder, uiMain1.TreeView1.SelectedNode.Text, false);
                        //移動先ファイル名
                        string dstRtfFile = uiMain1.GetRtfFile(uiMain1.Contents.ProjectFolder, dlg.NodeNameText, false);

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

                        if (!File.Exists(srcRtfFile))
                        {
                            if (!File.Exists(dstRtfFile))
                            {
                                //（１）
                                System.Console.WriteLine("RTF（１）と判断");
                                rtfB = true;
                            }
                            else
                            {
                                //（２）
                                System.Console.WriteLine("RTF（２）と判断");
                                rtfA = true;
                                rtfC = true;
                            }
                        }
                        else
                        {
                            if (!File.Exists(dstRtfFile))
                            {
                                //（３）
                                System.Console.WriteLine("RTF（３）と判断");
                                rtfD = true;
                            }
                            else
                            {
                                //（４）
                                System.Console.WriteLine("RTF（４）と判断");
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
                            DialogResult result2 = MessageBox.Show(form1, "ノード名が変更されました。\n処理を続けるには、現在開いているファイルを保存する必要があります。\n[" + srcRtfFile + "]\n[" + srcRtfFile + "]\n保存しますか？", "警告", MessageBoxButtons.YesNoCancel);
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
                            uiMain1.CreateSampleTextFile1(srcRtfFile, out contents);
                            uiMain1.Contents.RtfNow(srcRtfFile, contents);
                        }


                        //─────
                        //（Ｃ）移動先ファイルをロードし
                        //　　　　カレントとする
                        //─────
                        if (rtfC)
                        {
                            // 既にあるテキストファイル
                            MessageBox.Show("既にあるテキストファイルを読み込みます。テキスト[" + dstRtfFile + "]");
                            Actions.LoadPageRtf(form1, dstRtfFile);
                        }


                        //─────
                        //（Ｄ）移動元ファイルをリネームし
                        //　　　　移動先をカレントとする
                        //─────
                        if (rtfD)
                        {
                            File.Move(srcRtfFile, dstRtfFile);
                            uiMain1.Contents.RtfNow(dstRtfFile, uiMain1.Contents.SavedRtf);
                        }


                        //━━━━━
                        // ノード名の変更
                        //━━━━━
                        uiMain1.TreeView1.SelectedNode.Text = dlg.NodeNameText;//ツリービュー
                        uiMain1.UiTextside1.NodeNameTxt1.Text = dlg.NodeNameText;//ノード名表示
                    }

                gt_Abort://処理を中断した場合の飛び先。
                    ;
                }

                dlg.Dispose();
            }
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

    }
}
