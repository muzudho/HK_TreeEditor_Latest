using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;//Calendar
using System.IO;
using Xenon.Syntax;
using Xenon.Table;

namespace TreeEditor
{
    public partial class UiMain : UserControl
    {

        /// <summary>
        /// アプリケーション起動中、重複しない値。
        /// </summary>
        private long latestId;
        public long NextId()
        {
            long n = this.latestId;
            this.latestId++;

            return n;
        }


        /// <summary>
        /// 編集内容。
        /// </summary>
        private Contents contents;
        public Contents Contents
        {
            get
            {
                return contents;
            }
            set
            {
                contents = value;
            }
        }





        public TreeView TreeView1
        {
            get
            {
                return this.treeView1;
            }
        }


        /// <summary>
        /// バックアップ・フォルダー。
        /// </summary>
        /// <param name="projectFolder"></param>
        /// <param name="isBackup"></param>
        /// <returns></returns>
        public string GetBackup(string projectFolder)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(projectFolder);
            sb.Append(@"\backup");

            return sb.ToString();
        }


        public string GetTreeCsvFile(string projectFolder, bool isBackup)
        {
            StringBuilder sb = new StringBuilder();
            if (isBackup)
            {
                sb.Append(this.GetBackup(projectFolder));
                sb.Append(@"\");

                //タイムスタンプ（分の10の位まで）
                string time;
                {
                    StringBuilder s = new StringBuilder();

                    DateTime now = System.DateTime.Now;
                    s.Append(String.Format("{0:D4}", now.Year));
                    s.Append(String.Format("{0:D2}", now.Month));
                    s.Append(String.Format("{0:D2}", now.Day));
                    s.Append("_");
                    s.Append(String.Format("{0:D2}", now.Hour));
                    s.Append(String.Format("{0:D2}", now.Minute - now.Minute%10));

                    time = s.ToString();
                }
                sb.Append(time);

                sb.Append(@"\Tree.csv");
            }
            else
            {
                sb.Append(projectFolder);
                sb.Append(@"\Tree.csv");
            }

            return sb.ToString();
        }

        /// <summary>
        /// ファイルの絶対パスを取得します。
        /// 
        /// RTF,CSVの両方に使えます。
        /// </summary>
        /// <param name="projectFolder"></param>
        /// <param name="rel">ファイル名。@"node\名前.rtf"といった書式。</param>
        /// <param name="isBackup"></param>
        /// <returns></returns>
        public string GetFileFull(string projectFolder, string rel, bool isBackup)
        {
            StringBuilder sb = new StringBuilder();

            if (isBackup)
            {
                sb.Append(this.GetBackup(projectFolder));
                sb.Append(@"\");


                //タイムスタンプ（分の10の位まで）
                string time;
                {
                    StringBuilder s = new StringBuilder();

                    DateTime now = System.DateTime.Now;
                    s.Append(String.Format("{0:D4}", now.Year));
                    s.Append(String.Format("{0:D2}", now.Month));
                    s.Append(String.Format("{0:D2}", now.Day));
                    s.Append("_");
                    s.Append(String.Format("{0:D2}", now.Hour));
                    s.Append(String.Format("{0:D2}", now.Minute - now.Minute % 10));

                    time = s.ToString();
                }
                sb.Append(time);


                sb.Append(@"\");
                sb.Append(rel);
            }
            else
            {
                sb.Append(projectFolder);
                sb.Append(@"\");
                sb.Append(rel);
            }

            return sb.ToString();
        }

        public string GetListiconFolder(string projectFolder, string nodeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(projectFolder);
            sb.Append(@"\img-listicon");

            return sb.ToString();
        }

        public string GetListiconFile(string projectName, string nodeName, int listiconIndex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetListiconFolder(projectName,nodeName));
            sb.Append(@"\");
            sb.Append(listiconIndex);
            sb.Append(@".png");

            return sb.ToString();
        }


        public ImageList ImageList1
        {
            get
            {
                return this.imageList1;
            }
        }


        public void Clear()
        {
            //━━━━━
            //ツリービュー
            //━━━━━
            this.treeView1.Nodes.Clear();


            //━━━━━
            //タイトル
            //━━━━━
            this.uiTextside1.NodeNameTxt1.Text = "";


            //━━━━━
            //テキストエリア
            //━━━━━
            this.UiTextside1.Clear();


            //━━━━━
            //設定
            //━━━━━
            this.Contents.Clear();
        }

        public UiMain()
        {
            InitializeComponent();
            this.Contents = new Contents();
        }

        private void FitSize()
        {
            this.toolStripContainer1.Location = new Point();
            this.toolStripContainer1.Width = this.ClientSize.Width;
            this.toolStripContainer1.Height = this.ClientSize.Height;

            this.treeView1.Location = new Point();
            this.treeView1.Width = this.splitContainer1.Panel1.Width;
            this.treeView1.Height = this.splitContainer1.Panel1.Height;

            this.uiTextside1.Location = new Point();
            this.uiTextside1.Width = this.splitContainer1.Panel2.Width;
            this.uiTextside1.Height = this.splitContainer1.Panel2.Height;
        }

        public string GetDefaultprojectFolder()
        {
            return Application.StartupPath + @"\start";
        }

        private void UiMain_Load(object sender, EventArgs e)
        {
            this.FitSize();

            //━━━━━
            //TEFファイルを解凍する
            //━━━━━
            Actions.UnzipTef(this.GetDefaultprojectFolder()+@".tef");


            //━━━━━
            //解凍後のプロジェクト・フォルダーを開く
            //━━━━━
            this.OpenProjectLaw(
                Path.GetFileNameWithoutExtension(this.GetDefaultprojectFolder()),
                this.GetDefaultprojectFolder()
                );


            //リストアイコン
            {
                this.imageList1.Images.Clear();
                string listiconFolderAbs = this.GetListiconFolder(
                    this.Contents.ProjectFolder,
                    this.UiTextside1.NodeNameTxt1.Text
                    );
                if (Directory.Exists(listiconFolderAbs))
                {
                    for (int n = 0; n < int.MaxValue; n++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(listiconFolderAbs);
                        sb.Append(@"\");
                        sb.Append(n);
                        sb.Append(@".png");
                        string abs = sb.ToString();

                        if (File.Exists(abs))
                        {
                            Bitmap image = new Bitmap(abs);
                            this.imageList1.Images.Add(image);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }


            //自動バックアップタイマー開始。
            //Actions.Buckup(this);//初回起動時にまず保存。
            this.timer1.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloneSource">クローンしないならヌル。</param>
        /// <param name="text"></param>
        /// <param name="rtfRel"></param>
        /// <param name="iconN"></param>
        /// <param name="foreColorWeb"></param>
        /// <param name="backColorWeb"></param>
        /// <returns></returns>
        public TreeNode2 CreateTreeNode(TreeNode2 cloneSource, string text, string rtfRel, int iconN, string foreColorWeb, string backColorWeb)
        {
            TreeNode2 tn;

            if (null == cloneSource)
            {
                tn = new TreeNode2(this.NextId());
                tn.Text = text;
                tn.RtfRel = rtfRel;
                tn.ImageIndex = iconN;
                tn.SelectedImageIndex = iconN;
                tn.Fore.Web = foreColorWeb;
                tn.Back.Web = backColorWeb;
                //System.Console.WriteLine("tn.FullPath=" + tn.FullPath);


                tn.ForeColor = Color.FromArgb(255, tn.Fore.Red, tn.Fore.Green, tn.Fore.Blue);
                tn.BackColor = Color.FromArgb(255, tn.Back.Red, tn.Back.Green, tn.Back.Blue);
            }
            else
            {
                tn = (TreeNode2)cloneSource.Clone();
                tn.Tag = this.NextId();
            }


            return tn;
        }

        /// <summary>
        /// 解凍後の、指定のプロジェクトに切替え。
        /// </summary>
        /// <param name="projectName">プロジェクト名。</param>
        /// <param name="projectFolder">プロジェクト・フォルダーの絶対パス。</param>
        public void OpenProjectLaw(string projectName, string projectFolder)
        {
            Log_Method log_Method = new Log_MethodImpl();
            Log_ReportsImpl log_Reports = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod("--", this, "OpenProject", log_Reports);


            this.Clear();

            //━━━━━
            //設定
            //━━━━━
            this.Contents.SetProject( projectName, projectFolder);

            //━━━━━
            //ツリービュー
            //━━━━━
            this.treeView1.ImageList = this.imageList1;

            //━━━━━
            //ツリーCSV
            //━━━━━
            {
                string file = this.GetTreeCsvFile(this.Contents.ProjectFolder,false);
                if (File.Exists(file))
                {
                    try
                    {
                        string text = File.ReadAllText(file, Encoding.GetEncoding("Shift_JIS"));

                        CsvTo_TableImpl csvTo = new CsvTo_TableImpl();
                        Request_ReadsTableImpl request = new Request_ReadsTableImpl();
                        //request.Name_PutToTable = filePath;
                        Format_TableImpl format = new Format_TableImpl();
                        Table_Humaninput table = csvTo.Read(
                            text,
                            request,
                            format,
                            log_Reports
                            );
                        if (log_Reports.Successful)
                        {
                            System.Console.WriteLine("★ツリーCSV読込完了 [" + file + "]");

                            List<TreeNode> treeNodeList = new List<TreeNode>();
                            treeNodeList.Add(null);//[0]はヌル。
                            table.ForEach_Datapart(delegate(Record_Humaninput recordH, ref bool isBreak2, Log_Reports log_Reports2)
                            {
                                //レコード
                                string rNo = recordH.TextAt("NO");
                                string rExpl = recordH.TextAt("EXPL");
                                string rTree = recordH.TextAt("TREE");
                                string rIcon = recordH.TextAt("ICON");
                                string rName = recordH.TextAt("TEXT");
                                string rFile = recordH.TextAt("FILE");
                                string rForeColor = recordH.TextAt("FORE_COLOR");// #000000 形式の前景色
                                string rBackColor = recordH.TextAt("BACK_COLOR");// #000000 形式の後景色
                                //ystem.Console.WriteLine("NO=" + no + " EXPL=" + expl + " TREE=" + tree + " ICON=" + icon + " NAME=" + name + " FILE=" + rFile + " FORE_COLOR" + foreColor + " BACK_COLOR" + backColor);

                                int iconN;
                                int.TryParse(rIcon, out iconN);

                                TreeNode2 tn = this.CreateTreeNode(null, rName, rFile, iconN, rForeColor, rBackColor);

                                int treeN;
                                int.TryParse(rTree, out treeN);
                                treeNodeList.Insert(treeN, tn);

                                if (treeN == 1)
                                {
                                    // 最上位階層に対してまとめて項目（ノード）を追加
                                    treeView1.Nodes.Add(tn);
                                }
                                else if (2 <= treeN)
                                {
                                    // 最上位階層に対してまとめて項目（ノード）を追加
                                    treeNodeList[treeN - 1].Nodes.Add(tn);
                                }

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
                    System.Console.WriteLine("★失敗：ツリーCSV[" + file + "]がない。projectFolder[" + projectFolder + "]");
                }
            }



            //━━━━━
            //ツリービュー
            //━━━━━
            //TreeNode treeNodeFruits = new TreeNode("果物");
            //TreeNode treeNodeVegetables = new TreeNode("野菜", 1, 1);
            //TreeNode[] treeNodeSubFolder = { treeNodeFruits, treeNodeVegetables };

            //// 下位階層に対してまとめて項目（ノード）を追加
            //TreeNode treeNodeFood = new TreeNode("食べ物", treeNodeSubFolder);

            //TreeNode treeNodeDrink = new TreeNode("飲み物");
            //TreeNode[] treeNodeRoot = { treeNodeFood, treeNodeDrink };

            //// 最上位階層に対してまとめて項目（ノード）を追加
            //treeView1.Nodes.AddRange(treeNodeRoot);


            //━━━━━
            //↓ビシュアルエディターでは、エラーになってしまう。必要だと思うが。
            //━━━━━
            //treeView1.TopNode.Expand();


            //━━━━━
            //先頭ノード名読込
            //━━━━━
            if (0 < this.TreeView1.Nodes.Count)
            {
                this.uiTextside1.NodeNameTxt1.Text = this.TreeView1.Nodes[0].Text;
                this.Contents.RtfFileRel = ((TreeNode2)this.TreeView1.Nodes[0]).RtfRel;
            }
            else
            {
                this.uiTextside1.NodeNameTxt1.Text = "";
                this.Contents.RtfFileRel = "";
            }

            //━━━━━
            //テキストファイル読込
            //━━━━━
            {
                if (this.ParentForm is Form1)//ビジュアルエディターでは、親フォームがForm1とは限らない。
                {
                    Actions.LoadPage(
                        (Form1)this.ParentForm,
                        this.Contents.RtfFileRel
                        );
                }
            }

            //━━━━━
            //タイトルバー
            //━━━━━
            this.RefreshTitleBar();


            System.Console.WriteLine("OpenProject情報　プロジェクト名："+this.Contents.ProjectFolder);
            System.Console.WriteLine("OpenProject情報　プロジェクトのファイルパス："+this.Contents.ProjectFolder);


            if (!log_Reports.Successful)
            {
                MessageBox.Show(this, log_Reports.ToText(), "エラー");
            }
        }

        /// <summary>
        /// ファイル名の重複部分をカット。
        /// </summary>
        /// <param name="abs">絶対ファイルパス</param>
        /// <returns></returns>
        public string CutHeadProjectAbs(string abs)
        {
            string head = this.Contents.ProjectFolder + @"\";
            string dir2;

            if (abs.StartsWith(head))
            {
                dir2 = abs.Substring(head.Length, abs.Length - head.Length);
            }
            else
            {
                dir2 = abs;
            }

            return dir2;
        }

        /// <summary>
        /// タイトルバー
        /// </summary>
        public void RefreshTitleBar()
        {
            StringBuilder sb = new StringBuilder();


            //ファイルパス
            if ("" != this.Contents.RtfFileRel)
            {
                string dir2 = this.Contents.RtfFileRel;

                sb.Append(dir2);
                sb.Append(" / ");
            }

            //プロジェクト・フォルダー
            sb.Append(Path.GetFileName(this.Contents.ProjectFolder));

            //アプリケーション名
            sb.Append(" / ツリーエディター");
            if(this.Contents.IsChangedProject)
            {
                sb.Append(" *");
            }

            this.ParentForm.Text = sb.ToString();
        }

        /// <summary>
        /// 新しいプロジェクトを作成。
        /// </summary>
        /// <param name="projectName"></param>
        public void CreateDefaultProjectLaw(string projectFolder, bool isCreateDammyData)
        {
            Log_Method log_Method = new Log_MethodImpl();
            Log_ReportsImpl log_Reports = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod("--", this, "CreateDefaultProject", log_Reports);


            //━━━━━
            //ディレクトリー　作成
            //━━━━━
            try
            {
                Directory.CreateDirectory(projectFolder);
            }
            catch (Exception)
            {
            }

            try
            {
                Directory.CreateDirectory(this.GetBackup(projectFolder));
            }
            catch (Exception)
            {
            }

            try
            {
                Directory.CreateDirectory(projectFolder + @"\img");
            }
            catch (Exception)
            {
            }

            try
            {
                Directory.CreateDirectory(projectFolder + @"\img-listicon");
            }
            catch (Exception)
            {
            }

            try
            {
                Directory.CreateDirectory(projectFolder + @"\node");
            }
            catch (Exception)
            {
            }


            //━━━━━
            //ツリーCSV 作成
            //━━━━━
            {
                string file = this.GetTreeCsvFile(projectFolder, false);
                if (!File.Exists(file))
                {
                    string[] lines;

                    if (isCreateDammyData)
                    {
                        lines = new string[]{
                        "NO,EXPL,TREE,ICON,TEXT,FILE,EOL",
                        "int,string,int,int,string,string,",
                        "自動連番,コメント,ツリー階層,,表示テキスト,ファイル,",
                        ",,1,0,食べ物,食べ物.txt,",
                        ",,2,0,果物,果物.txt,",
                        ",,2,1,野菜,野菜.txt,",
                        ",,1,0,飲み物,飲み物.txt,",
                        "EOF,,,,,,",
                    };
                    }
                    else
                    {
                        lines = new string[]{
                        "NO,EXPL,TREE,ICON,TEXT,FILE,EOL",
                        "int,string,int,int,string,string,",
                        "自動連番,コメント,ツリー階層,,表示テキスト,ファイル,",
                        "EOF,,,,,,",
                    };
                    }


                    StringBuilder sb = new StringBuilder();
                    foreach (string line in lines)
                    {
                        sb.Append(line);
                        sb.Append(Environment.NewLine);
                    }

                    try
                    {
                        File.WriteAllText(file, sb.ToString(), Encoding.GetEncoding("Shift_JIS"));
                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("★失敗：書き込み失敗。");
                    }
                }
                else
                {
                    System.Console.WriteLine("★失敗：ツリーCSV[" + file + "]がある。");
                }
            }


            if (isCreateDammyData)
            {
                //━━━━━
                //ノード　飲み物
                //━━━━━
                {
                    string contents;
                    Utility.CreateRtf(this, @"node\飲み物.rtf", out contents);
                }

                //━━━━━
                //ノード　果物
                //━━━━━
                {
                    string contents;
                    Utility.CreateRtf(this, @"node\果物.rtf", out contents);
                }


                //━━━━━
                //ノード　食べ物
                //━━━━━
                {
                    string contents;
                    Utility.CreateRtf(this, @"node\食べ物.rtf", out contents);
                }


                //━━━━━
                //ノード　野菜
                //━━━━━
                {
                    string contents;
                    Utility.CreateRtf(this, @"node\野菜.rtf", out contents);
                }
            }



            //━━━━━
            //リストアイコン　作成
            //━━━━━
            try
            {
                System.IO.File.Copy(@"system\img\listIcon0.png", projectFolder + @"\img-listicon\0.png");
                System.IO.File.Copy(@"system\img\listIcon1.png", projectFolder + @"\img-listicon\1.png");
                System.IO.File.Copy(@"system\img\listIcon2.png", projectFolder + @"\img-listicon\2.png");
                System.IO.File.Copy(@"system\img\listIcon3.png", projectFolder + @"\img-listicon\3.png");
            }
            catch (Exception)
            {
            }


            //━━━━━
            //保存
            //━━━━━
            Actions.Save((Form1)this.ParentForm);


            if (!log_Reports.Successful)
            {
                MessageBox.Show(this, log_Reports.ToText(), "エラー");
            }
        }

        private void UiMain_Resize(object sender, EventArgs e)
        {
            this.FitSize();
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo ht = treeView1.HitTest(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                if (ht.Location == TreeViewHitTestLocations.Label)
                {

                    //
                    //　　　　　　　　　　　　テキストを　　　　　　　　　　テキストの
                    //　　　　　　　　　　　　変更していれば　　　　　　　　変更がなければ
                    //　　　　　　　　　　┌──────────────┐
                    //　　　　　　　　　　│　　　　　　　　　　　　　　│
                    //変更内容を保存する　│　　　　　（１）　　　　　　│
                    //　　　　　　　　　　│　　　　　　　　　　　　　　│
                    //　　　　　　　　　　│　　　　　　　　　　　　　　│
                    //変更内容を保存しない│　　　　　（２）　　　　　　│
                    //　　　　　　　　　　│　　　　　　　　　　　　　　│
                    //　　　　　　　　　　│　　　　　　　　　　　　　　│
                    //キャンセル　　　　　│　　　　　（３）　　　　　　│
                    //　　　　　　　　　　│　　　　　　　　　　　　　　│
                    //　　　　　　　　　　└──────────────┼──────────────┐
                    //　　　　　　　　　　　　　　　　　　　　　　　　　│　　　　　　　　　　　　　│
                    //　　　　　　　　　　　　　　　　　　　　　　　　　│　　　　　（４）　　　　　│
                    //　　　　　　　　　　　　　　　　　　　　　　　　　│　　　　　　　　　　　　　│
                    //　　　　　　　　　　　　　　　　　　　　　　　　　└──────────────┘
                    //
                    //
                    //　　　　　　　　　　　　　　　　（１）　　　（２）　　　（３）　　　（４）
                    //　　　　　　　　　　　　　　┌─────┬─────┬─────┬─────┐
                    //　　　　　　　　　　　　　　│　　　　　│　　　　　│　　　　　│　　　　　│
                    //（Ａ）現在の内容を保存する　│　　○　　│　　　　　│　　　　　│　　　　　│
                    //　　　　　　　　　　　　　　│　　　　　│　　　　　│　　　　　│　　　　　│
                    //　　　　　　　　　　　　　　│　　　　　│　　　　　│　　　　　│　　　　　│
                    //（Ｂ）ページをロードする　　│　　○　　│　　○　　│　　　　　│　　○　　│
                    //　　　　　　　　　　　　　　│　　　　　│　　　　　│　　　　　│　　　　　│
                    //　　　　　　　　　　　　　　└─────┴─────┴─────┴─────┘
                    //
                    //
                    //

                    bool isSave = false;
                    bool isLoad = false;

                    if (this.Contents.IsChangedProject)
                    {
                        //（１）
                        StringBuilder sb = new StringBuilder();

                        sb.Append("変更内容を保存しますか？\n");
                        sb.Append(Environment.NewLine);

                        sb.Append("ツリー[" + this.Contents.IsChangedTree + "]\n");
                        sb.Append(Environment.NewLine);

                        sb.Append("RTF[" + this.Contents.IsChangedRtf + "]\n");
                        sb.Append(Environment.NewLine);

                        DialogResult result = MessageBox.Show(
                            sb.ToString(),
                            "ファイル変更",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Exclamation
                            );
                        switch (result)
                        {
                            case DialogResult.Yes:
                                isSave = true;
                                isLoad = true;
                                break;
                            case DialogResult.No:
                                isLoad = true;
                                break;
                            default:
                                // キャンセル
                                break;
                        }

                        // メッセージボックスを出すと選択されないので、再選択します。
                        this.treeView1.SelectedNode = ht.Node;
                    }
                    else
                    {
                        //（２）
                        isLoad = true;
                    }


                    if (isSave)
                    {
                        Actions.Save((Form1)this.ParentForm);
                    }


                    if (isLoad)
                    {
                        //━━━━━
                        //クリアー
                        //━━━━━
                        this.UiTextside1.Clear();


                        //━━━━━
                        //ノード名読込
                        //━━━━━
                        this.UiTextside1.NodeNameTxt1.Text = ht.Node.Text;


                        //━━━━━
                        //RTFファイルの存在有無確認
                        //━━━━━
                        string rtfRel = ((TreeNode2)ht.Node).RtfRel;
                        if (!File.Exists(this.GetFileFull(this.Contents.ProjectFolder, rtfRel, false)))
                        {
                            // なければ作る。
                            string contents;
                            Utility.CreateRtf(this, rtfRel, out contents);
                            this.Contents.RtfNow(rtfRel, contents);
                        }


                        //━━━━━
                        //CSVファイルの存在有無確認
                        //━━━━━
                        string csvRel = Utility.RtfToCsv(rtfRel);
                        if (!File.Exists(this.GetFileFull(this.Contents.ProjectFolder, csvRel, false)))
                        {
                            // なければ作る。
                            string contents;
                            Utility.CreateCsv(this, csvRel, out contents);
                            this.Contents.CsvNow( contents);
                        }


                        //━━━━━
                        //テキストファイル読込
                        //━━━━━
                        Actions.LoadPage((Form1)this.ParentForm, rtfRel);
                    }
                }
            }
            else if(e.Button==MouseButtons.Right)
            {
                // 右クリックした時

                if (ht.Location == TreeViewHitTestLocations.Label)
                {
                    // メッセージボックスを出すと選択されないので、再選択します。
                    this.treeView1.SelectedNode = ht.Node;

                    Point p1 = this.TreeView1.PointToScreen(e.Location);

                    this.contextMenuStrip1.Show(
                        new Point(
                            p1.X,//e.Location.X,
                            p1.Y - this.contextMenuStrip1.Height/2 //e.Location.Y
                            )                        
                        );
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Actions.New((Form1)this.ParentForm);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Actions.Undo((Form1)this.ParentForm);
        }

        private void toolStripButton44_Click(object sender, EventArgs e)
        {
            Actions.Redo((Form1)this.ParentForm);
        }

        public UiTextside UiTextside1
        {
            get
            {
                return this.uiTextside1;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Actions.Save((Form1)this.ParentForm);
        }


        /// <summary>
        /// カレントRTFに変更があるかどうか確認します。
        /// </summary>
        public bool IsChangeRtf()
        {
            // 改行コードが違っても、文字が同じなら、変更なしと判定します。
            string text1 = this.Contents.SavedRtf.Replace("\r", "").Replace("\n", "");
            string text2 = this.UiTextside1.TextareaRtf.Replace("\r", "").Replace("\n", "");

            return text1.CompareTo(text2) != 0;
        }

        /// <summary>
        /// ノードアイコン変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            //if (null != this.treeView1.SelectedNode)
            //{
            //    switch (this.treeView1.SelectedNode.ImageIndex)
            //    {
            //        case 0:
            //            this.treeView1.SelectedNode.ImageIndex = 1;
            //            this.treeView1.SelectedNode.SelectedImageIndex = 1;
            //            break;
            //        case 1:
            //            this.treeView1.SelectedNode.ImageIndex = 2;
            //            this.treeView1.SelectedNode.SelectedImageIndex = 2;
            //            break;
            //        case 2:
            //            this.treeView1.SelectedNode.ImageIndex = 3;
            //            this.treeView1.SelectedNode.SelectedImageIndex = 3;
            //            break;
            //        default:
            //            this.treeView1.SelectedNode.ImageIndex = 0;
            //            this.treeView1.SelectedNode.SelectedImageIndex = 0;
            //            break;
            //    }

            //    this.treeView1.Refresh();
            //}
        }

        /// <summary>
        /// カレントノードのプロパティー変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            Actions.SetNodeProperty(this);
        }

        /// <summary>
        /// 時限式で自動バックアップを取りたい。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            System.Console.WriteLine("timer1:自動バックアップを取りたい。");
            Actions.Buckup(this);
        }

        /// <summary>
        /// 弟ノードを作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            Actions.AddNodeToSibling(this,null);
        }


        /// <summary>
        /// ノードがドラッグされたとき。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeView tv = (TreeView)sender;
            tv.SelectedNode = (TreeNode2)e.Item;
            tv.Focus();
            //ノードのドラッグを開始する
            DragDropEffects dde = tv.DoDragDrop(e.Item, DragDropEffects.All);
            //移動した時は、ドラッグしたノードを削除する
            if ((dde & DragDropEffects.Move) == DragDropEffects.Move)
            {
                tv.Nodes.Remove((TreeNode2)e.Item);
            }
        }

        /// <summary>
        /// ドラッグしているとき。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_DragOver(object sender, DragEventArgs e)
        {
            //ドラッグされているデータがTreeNodeか調べる
            if (e.Data.GetDataPresent(typeof(TreeNode2)))
            {
                if ((e.KeyState & 8) == 8 &&
                    (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    //Ctrlキーが押されていればCopy
                    //"8"はCtrlキーを表す
                    e.Effect = DragDropEffects.Copy;
                }
                else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                {
                    //何も押されていなければMove
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                //TreeNodeでなければ受け入れない
                e.Effect = DragDropEffects.None;
            }

            //マウス下のNodeを選択する
            if (e.Effect != DragDropEffects.None)
            {
                TreeView tv = (TreeView)sender;
                //マウスのあるNodeを取得する
                TreeNode2 target = (TreeNode2)tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));
                //ドラッグされているNodeを取得する
                TreeNode2 source = (TreeNode2)e.Data.GetData(typeof(TreeNode2));
                //マウス下のNodeがドロップ先として適切か調べる
                if (target != null && target != source && !IsChildNode(source, target))
                {
                    //Nodeを選択する
                    if (target.IsSelected == false)
                    {
                        tv.SelectedNode = target;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        /// <summary>
        /// ドロップされたとき。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            //ドロップされたデータがTreeNodeか調べる
            if (e.Data.GetDataPresent(typeof(TreeNode2)))
            {
                TreeView tv = (TreeView)sender;
                //ドロップされたデータ(TreeNode)を取得
                TreeNode2 source = (TreeNode2)e.Data.GetData(typeof(TreeNode2));
                //ドロップ先のTreeNodeを取得する
                TreeNode2 target = (TreeNode2)tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));
                //マウス下のNodeがドロップ先として適切か調べる
                if (target != null && target != source && !IsChildNode(source, target))
                {
                    //ドロップされたNodeのコピーを作成
                    TreeNode2 cln = (TreeNode2)source.Clone();
                    //Nodeを追加
                    target.Nodes.Add(cln);
                    //ドロップ先のNodeを展開
                    target.Expand();
                    //追加されたNodeを選択
                    tv.SelectedNode = cln;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// あるTreeNodeが別のTreeNodeの子ノードか調べる
        /// </summary>
        /// <param name="parentNode">親ノードか調べるTreeNode</param>
        /// <param name="childNode">子ノードか調べるTreeNode</param>
        /// <returns>子ノードの時はTrue</returns>
        private static bool IsChildNode(TreeNode2 parentNode, TreeNode2 childNode)
        {
            if (childNode.Parent == parentNode)
            {
                return true;
            }
            else if (childNode.Parent != null)
            {
                return IsChildNode(parentNode, (TreeNode2)childNode.Parent);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ノードの削除。ファイルは削除しません。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            Actions.DeleteNode(this);
        }

        /// <summary>
        /// データ修正ボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton45_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ノードを上に移動するボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            Actions.MoveNodeInSiblings(this, true);
        }

        /// <summary>
        /// ノードを下に移動するボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            Actions.MoveNodeInSiblings(this, false);
        }



        /// <summary>
        /// フォルダーを開くボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Actions.OpenTef((Form1)this.ParentForm);
        }

        /// <summary>
        /// ノードを親へ移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton46_Click(object sender, EventArgs e)
        {
            Actions.MoveNodeToParent(this);
        }

        /// <summary>
        /// 子ノードの追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            Actions.AppendChildNode(this);
        }


        /// <summary>
        /// ツールボタンの[設定]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton43_Click(object sender, EventArgs e)
        {
            ConfigurationDialog dlg = new ConfigurationDialog();
            DialogResult result = dlg.ShowDialog((Form1)this.ParentForm);

            //this.Contents.IsRtfMode = dlg.UiConfiguration1.IsRtf;

            dlg.Dispose();
        }


        /// <summary>
        /// データ修正ボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton45_Click_1(object sender, EventArgs e)
        {
            ModifyDialog dlg = new ModifyDialog();

            DialogResult result = dlg.ShowDialog((Form1)this.ParentForm);



            dlg.Dispose();

        }

        /// <summary>
        /// ノードのコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            Actions.DuplicateNodeToSibling(this);
        }

        private void ノードの追加弟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.AddNodeToSibling(this, null);
        }

        private void ノードの追加子ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.AppendChildNode(this);
        }

        private void ノードの追加複製ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.DuplicateNodeToSibling(this);

        }

        private void ノードの削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.DeleteNode(this);
        }

        private void ノードを上に移動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.MoveNodeInSiblings(this,true);
        }

        private void ノードを下に移動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.MoveNodeInSiblings(this, false);
        }

        private void ノードを親に移動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.MoveNodeToParent(this);
        }

        private void プロパティー変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.SetNodeProperty(this);
        }

        private static void Sweep(TreeNode2 parent, List<string> rtfRel)
        {
            foreach (TreeNode2 child in parent.Nodes)
            {
                rtfRel.Add(child.RtfRel.ToLower());
                //ystem.Console.WriteLine("有=[" + child.RtfRel.ToLower() + "]");

                rtfRel.Add(Utility.RtfToCsv(child.RtfRel).ToLower());
                //ystem.Console.WriteLine("有=[" + Utility.RtfToCsv(child.RtfRel).ToLower() + "]");

                rtfRel.Add(Utility.RtfToTxt(child.RtfRel).ToLower());
                //ystem.Console.WriteLine("有=[" + Utility.RtfToTxt(child.RtfRel).ToLower() + "]");

                UiMain.Sweep(child, rtfRel);
            }
        }

        /// <summary>
        /// 掃除ボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton47_Click(object sender, EventArgs e)
        {

            if (this.Contents.IsChangedProject)
            {
                MessageBox.Show(this.ParentForm,"保存してから、このボタンを押してください。","情報",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                //━━━━━
                //ツリー構造を走査
                //━━━━━

                List<string> rtfRel = new List<string>();
                foreach (TreeNode2 tn in this.TreeView1.Nodes)
                {
                    rtfRel.Add(tn.RtfRel.ToLower());
                    //ystem.Console.WriteLine("有=[" + tn.RtfRel.ToLower() + "]");

                    rtfRel.Add(Utility.RtfToCsv(tn.RtfRel).ToLower());
                    //ystem.Console.WriteLine("有=[" + Utility.RtfToCsv(tn.RtfRel).ToLower() + "]");

                    rtfRel.Add(Utility.RtfToTxt(tn.RtfRel).ToLower());
                    //ystem.Console.WriteLine("有=[" + Utility.RtfToTxt(tn.RtfRel).ToLower() + "]");

                    UiMain.Sweep(tn, rtfRel);
                }


                //━━━━━
                //未使用判定
                //━━━━━

                List<string> remove = new List<string>();

                string[] files = Directory.GetFiles(this.Contents.ProjectFolder + @"\node");
                foreach (string abs in files)
                {
                    string rel2 = this.CutHeadProjectAbs(abs).ToLower();
                    if ( rtfRel.Contains( rel2 ))
                    {
                        //ystem.Console.WriteLine("file=[" + abs + "]　rel2=[" + rel2 + "]");
                    }
                    else
                    {
                        //ystem.Console.WriteLine("未使用　file=[" + abs + "]　rel2=[" + rel2 + "]");
                        remove.Add(abs);
                    }
                }


                //━━━━━
                //別フォルダーへ移動
                //━━━━━
                string dir = this.Contents.ProjectFolder + @"\node\ごみ箱_" + Utility.CreateUniqueFileName(this);
                Directory.CreateDirectory(dir);
                foreach (string abs in remove)
                {
                    File.Move(abs, dir + @"\" + Path.GetFileName(abs));
                }

            }

        }

        private void ノードを子に移動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.MoveNodeToChild(this);
        }

        /// <summary>
        /// 子へ移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton48_Click(object sender, EventArgs e)
        {
            Actions.MoveNodeToChild(this);
        }

    }
}
