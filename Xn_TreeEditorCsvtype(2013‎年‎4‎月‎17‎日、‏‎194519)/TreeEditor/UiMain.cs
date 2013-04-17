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


        public string GetTreeCsvFile(string projectFolder, bool isBackup)
        {
            StringBuilder sb = new StringBuilder();
            if (isBackup)
            {

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


                sb.Append(projectFolder);
                sb.Append(@"\backup\");
                sb.Append(time);
                sb.Append(@"\BK_Tree.csv");
            }
            else
            {
                sb.Append(projectFolder);
                sb.Append(@"\Tree.csv");
            }

            return sb.ToString();
        }

        public string GetTextFile(string projectFolder, string nodeName, bool isBackup)
        {
            StringBuilder sb = new StringBuilder();
            if (isBackup)
            {

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

                sb.Append(projectFolder);
                sb.Append(@"\backup\");
                sb.Append(time);
                sb.Append(@"\node\");
                sb.Append(nodeName);
                sb.Append(@".txt");
            }
            else
            {
                sb.Append(projectFolder);
                sb.Append(@"\node\");
                sb.Append(nodeName);
                sb.Append(@".txt");
            }

            return sb.ToString();
        }

        public string GetRtfFile(string projectFolder, string nodeName, bool isBackup)
        {
            StringBuilder sb = new StringBuilder();
            if (isBackup)
            {

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

                sb.Append(projectFolder);
                sb.Append(@"\backup\");
                sb.Append(time);
                sb.Append(@"\node\");
                sb.Append(nodeName);
                sb.Append(@".txt");
            }
            else
            {
                sb.Append(projectFolder);
                sb.Append(@"\node\");
                sb.Append(nodeName);
                sb.Append(@".rtf");
            }

            return sb.ToString();
        }

        public string GetResourceCsvFile(string projectFolder, string nodeName, bool isBackup)
        {
            StringBuilder sb = new StringBuilder();
            if (isBackup)
            {

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

                sb.Append(projectFolder);
                sb.Append(@"\backup\");
                sb.Append(time);
                sb.Append(@"\node\");
                sb.Append(nodeName);
                sb.Append(@".csv");
            }
            else
            {
                sb.Append(projectFolder);
                sb.Append(@"\node\");
                sb.Append(nodeName);
                sb.Append(@".csv");
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

        private void UiMain_Load(object sender, EventArgs e)
        {
            this.FitSize();

            this.OpenProject(Application.StartupPath+@"\save\default");

            //リストアイコン
            {
                this.imageList1.Images.Clear();
                string dir = this.GetListiconFolder(
                    this.Contents.ProjectFolder,
                    this.UiTextside1.NodeNameTxt1.Text
                    );
                if (Directory.Exists(dir))
                {
                    for (int n = 0; n < int.MaxValue; n++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(dir);
                        sb.Append(@"\");
                        sb.Append(n);
                        sb.Append(@".png");
                        string file = sb.ToString();

                        if (File.Exists(file))
                        {
                            Bitmap image = new Bitmap(file);
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

        private TreeNode2 NewTreeNode(string name, int iconN, string foreColorWeb, string backColorWeb)
        {
            TreeNode2 tn = new TreeNode2(this.NextId());
            tn.Text = name;
            tn.ImageIndex = iconN;
            tn.SelectedImageIndex = iconN;
            tn.Fore.Web = foreColorWeb;
            tn.Back.Web = backColorWeb;
            //System.Console.WriteLine("tn.FullPath=" + tn.FullPath);


            tn.ForeColor = Color.FromArgb(255, tn.Fore.Red, tn.Fore.Green, tn.Fore.Blue);
            tn.BackColor = Color.FromArgb(255, tn.Back.Red, tn.Back.Green, tn.Back.Blue);

            return tn;
        }

        /// <summary>
        /// 指定のプロジェクトに切替え。
        /// </summary>
        /// <param name="projectFolder"></param>
        public void OpenProject(string projectFolder)
        {
            Log_Method log_Method = new Log_MethodImpl();
            Log_ReportsImpl log_Reports = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod("--", this, "OpenProject", log_Reports);


            this.Clear();

            //━━━━━
            //設定
            //━━━━━
            this.Contents.ProjectFolder = projectFolder;

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
                                string rName = recordH.TextAt("NAME");
                                string rFile = recordH.TextAt("FILE");
                                string rForeColor = recordH.TextAt("FORE_COLOR");// #000000 形式の前景色
                                string rBackColor = recordH.TextAt("BACK_COLOR");// #000000 形式の後景色
                                //ystem.Console.WriteLine("NO=" + no + " EXPL=" + expl + " TREE=" + tree + " ICON=" + icon + " NAME=" + name + " FILE=" + rFile + " FORE_COLOR" + foreColor + " BACK_COLOR" + backColor);

                                int iconN;
                                int.TryParse(rIcon, out iconN);

                                TreeNode2 tn = this.NewTreeNode(rName, iconN, rForeColor, rBackColor);

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
                    System.Console.WriteLine("★失敗：ツリーCSV[" + file + "]がない。");
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
            }
            else
            {
                this.uiTextside1.NodeNameTxt1.Text = "";
            }

            //━━━━━
            //テキストファイル読込
            //━━━━━
            {
                if (this.ParentForm is Form1)//ビジュアルエディターでは、親フォームがForm1とは限らない。
                {
                    Actions.LoadPage(
                        (Form1)this.ParentForm,
                        this.GetTextFile(this.Contents.ProjectFolder, this.uiTextside1.NodeNameTxt1.Text, false),//ファイルパス
                        this.GetRtfFile(this.Contents.ProjectFolder, this.uiTextside1.NodeNameTxt1.Text, false),
                        this.GetResourceCsvFile(this.Contents.ProjectFolder, this.uiTextside1.NodeNameTxt1.Text, false)
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
        /// タイトルバー
        /// </summary>
        public void RefreshTitleBar()
        {
            StringBuilder sb = new StringBuilder();

            string head = this.Contents.ProjectFolder + @"\";

            //ファイルパス
            if ("" != this.Contents.TextFile)
            {
                string dir2;

                if (this.Contents.TextFile.StartsWith(head))
                {
                    dir2 = this.Contents.TextFile.Substring(head.Length, this.Contents.TextFile.Length - head.Length);
                }
                else
                {
                    dir2 = this.Contents.TextFile;
                }

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


        public void CreateSampleTextFile1(string file, out string contents)
        {
            contents = "";

            if (!File.Exists(file))
            {
                string[] lines = new string[]{
                        "--------------------------------------------------",
                        "◆ 流す",
                        "--------------------------------------------------",
                        "",
                        "************************************************************",
                        "◆流す",
                        "************************************************************",
                        "",
                        "※キーボード。",
                        "　かばん。",
                        "　壁。",
                        "",
                        "※炊飯器。",
                        "",
                        "松男「牛」",
                        "",
                        "竹男「わさび」",
                        "",
                        "梅男「種」",
                        "",
                        "",
                    };

                StringBuilder sb = new StringBuilder();
                foreach (string line in lines)
                {
                    sb.Append(line);
                    sb.Append(Environment.NewLine);
                }

                contents = sb.ToString();

                try
                {
                    System.Console.WriteLine("★サンプルファイル新規作成：　["+file+"]");
                    File.WriteAllText(file, contents, Encoding.GetEncoding("Shift_JIS"));
                }
                catch (Exception)
                {
                    System.Console.WriteLine("★失敗：書き込み失敗。");
                }
            }
        }

        public void CreateSampleRtfFile1(string file, out string contents)
        {
            contents = "";

            if (!File.Exists(file))
            {
                string[] lines = new string[]{
                        "{\rtf1}",
                    };

                StringBuilder sb = new StringBuilder();
                foreach (string line in lines)
                {
                    sb.Append(line);
                    sb.Append(Environment.NewLine);
                }

                contents = sb.ToString();

                try
                {
                    System.Console.WriteLine("★サンプルファイル新規作成：　[" + file + "]");
                    File.WriteAllText(file, contents, Encoding.GetEncoding("Shift_JIS"));
                }
                catch (Exception)
                {
                    System.Console.WriteLine("★失敗：書き込み失敗。");
                }
            }
        }

        public void CreateSampleResourceCsvFile1(string file, out string contents)
        {
            contents = "";

            if (!File.Exists(file))
            {
                string[] lines = new string[]{
                        "NO,FILE,X,Y,EOL",
                        "int,string,int,int,",
                        "自動連番,ファイルパス,,,",
                        "EOF,,,,",
                        "",
                    };

                StringBuilder sb = new StringBuilder();
                foreach (string line in lines)
                {
                    sb.Append(line);
                    sb.Append(Environment.NewLine);
                }

                contents = sb.ToString();

                try
                {
                    File.WriteAllText(file, contents, Encoding.GetEncoding("Shift_JIS"));
                }
                catch (Exception)
                {
                    System.Console.WriteLine("★失敗：書き込み失敗。");
                }
            }
        }

        /// <summary>
        /// 新しいプロジェクトを作成。
        /// </summary>
        /// <param name="projectName"></param>
        public void CreateDefaultProject(string projectFolder)
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
                    string[] lines = new string[]{
                        "NO,EXPL,TREE,ICON,NAME,FILE,EOL",
                        "int,string,int,int,string,string,",
                        "自動連番,コメント,ツリー階層,,,,",
                        ",,1,0,食べ物,食べ物.txt,",
                        ",,2,0,果物,果物.txt,",
                        ",,2,1,野菜,野菜.txt,",
                        ",,1,0,飲み物,飲み物.txt,",
                        "EOF,,,,,,",
                    };

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


            //━━━━━
            //ノード　飲み物
            //━━━━━
            {
                string contents;
                this.CreateSampleTextFile1(
                    this.GetTextFile(projectFolder, "飲み物", false),
                    out contents
                    );
            }
            {
                string contents;
                this.CreateSampleResourceCsvFile1(this.GetResourceCsvFile(projectFolder, "飲み物", false), out contents);
            }

            //━━━━━
            //ノード　果物
            //━━━━━
            {
                string file = this.GetTextFile(projectFolder, "果物", false);
                if (!File.Exists(file))
                {
                    string[] lines = new string[]{
                        "--------------------------------------------------",
                        "◆ 果物を右へ",
                        "--------------------------------------------------",
                        "",
                        "************************************************************",
                        "◆果物を右へ",
                        "************************************************************",
                        "",
                        "※目印。",
                        "　床。",
                        "　東。",
                        "",
                        "※カメ。",
                        "",
                        "クモ男「小型」",
                        "",
                        "ワニ男「Ｂ型」",
                        "",
                        "クラゲ男「Ｆ型」",
                        "",
                        "",
                        "",
                    };

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
            }
            {
                string contents;
                this.CreateSampleResourceCsvFile1(this.GetResourceCsvFile(projectFolder, "果物", false), out contents);
            }


            //━━━━━
            //ノード　食べ物
            //━━━━━
            {
                string filePath = this.GetTextFile(projectFolder, "食べ物", false);
                if (!File.Exists(filePath))
                {
                    string[] lines = new string[]{
                        "--------------------------------------------------",
                        "◆ 犬は歩き出す",
                        "--------------------------------------------------",
                        "",
                        "************************************************************",
                        "◆犬は歩き出す",
                        "************************************************************",
                        "",
                        "※火を噴く。",
                        "　横から槍が出る。",
                        "　刺さる。",
                        "",
                        "※マンホールのふたが飛ぶ。",
                        "",
                        "犬男「白い」",
                        "",
                        "熊男「Ｓｋｙ」",
                        "",
                        "兎男「ラーメン屋」",
                        "",
                        "",
                        "",
                    };

                    StringBuilder sb = new StringBuilder();
                    foreach (string line in lines)
                    {
                        sb.Append(line);
                        sb.Append(Environment.NewLine);
                    }

                    try
                    {
                        File.WriteAllText(filePath, sb.ToString(), Encoding.GetEncoding("Shift_JIS"));
                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("★失敗：書き込み失敗。");
                    }
                }
            }
            {
                string contents;
                this.CreateSampleResourceCsvFile1(this.GetResourceCsvFile(projectFolder, "食べ物", false), out contents);
            }


            //━━━━━
            //ノード　野菜
            //━━━━━
            {
                string filePath = this.GetTextFile(projectFolder, "野菜", false);
                if (!File.Exists(filePath))
                {
                    string[] lines = new string[]{
                        "--------------------------------------------------",
                        "◆ 野菜は語り出す",
                        "--------------------------------------------------",
                        "",
                        "************************************************************",
                        "◆野菜は語り出す",
                        "************************************************************",
                        "",
                        "※伸びる。",
                        "　茹でる。",
                        "　ひざまずく。",
                        "",
                        "※汁。",
                        "",
                        "火男「細い」",
                        "",
                        "水男「うねる」",
                        "",
                        "風男「切る」",
                        "",
                        "",
                        "",
                    };

                    StringBuilder sb = new StringBuilder();
                    foreach (string line in lines)
                    {
                        sb.Append(line);
                        sb.Append(Environment.NewLine);
                    }

                    try
                    {
                        File.WriteAllText(filePath, sb.ToString(), Encoding.GetEncoding("Shift_JIS"));
                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("★失敗：書き込み失敗。");
                    }
                }
            }
            {
                string contents;
                this.CreateSampleResourceCsvFile1(this.GetResourceCsvFile(projectFolder, "野菜", false), out contents);
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

                        sb.Append("テキスト[" + this.Contents.IsChangedText + "]\n");
                        sb.Append(Environment.NewLine);

                        sb.Append("画像配置[" + this.Contents.IsChangedResource + "]\n");
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
                        //テキストファイルの存在有無確認
                        //━━━━━
                        string textFile = this.GetTextFile(this.Contents.ProjectFolder, ht.Node.Text, false);
                        if (!File.Exists(textFile))
                        {
                            // なければ作る。
                            string contents;
                            this.CreateSampleTextFile1(textFile, out contents);
                            this.Contents.TextNow(textFile, contents);
                        }


                        //━━━━━
                        //RTFファイルの存在有無確認
                        //━━━━━
                        string rtfFile = this.GetRtfFile(this.Contents.ProjectFolder, ht.Node.Text, false);
                        if (!File.Exists(rtfFile))
                        {
                            // なければ作る。
                            string contents;
                            this.CreateSampleRtfFile1(rtfFile, out contents);
                            this.Contents.RtfNow(rtfFile, contents);
                        }


                        //━━━━━
                        //テキストファイルの存在有無確認
                        //━━━━━
                        string csvFile = this.GetResourceCsvFile(this.Contents.ProjectFolder, ht.Node.Text, false);
                        if (!File.Exists(csvFile))
                        {
                            // なければ作る。
                            string contents;
                            this.CreateSampleResourceCsvFile1(csvFile, out contents);
                            this.Contents.CsvNow(csvFile, contents);
                        }

                        //━━━━━
                        //テキストファイル読込
                        //━━━━━
                        Actions.LoadPage(
                            (Form1)this.ParentForm,
                            textFile,
                            rtfFile,
                            csvFile
                            );
                    }
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
        /// カレントテキストに変更があるかどうか確認します。
        /// </summary>
        public bool IsChangeText()
        {
            // 改行コードが違っても、文字が同じなら、変更なしと判定します。
            string text1 = this.Contents.SavedText.Replace("\r", "").Replace("\n", "");
            string text2 = this.UiTextside1.TextareaText.Replace("\r", "").Replace("\n", "");

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

            if (null != this.TreeView1.SelectedNode && this.TreeView1.SelectedNode is TreeNode2)
            {
                TreeNode2 tn = (TreeNode2)this.TreeView1.SelectedNode;
                NodePropertyDialog dlg = new NodePropertyDialog();

                dlg.SelectedImageIndex = this.TreeView1.SelectedNode.SelectedImageIndex;

                dlg.ForeRed = tn.Fore.Red;
                dlg.ForeGreen = tn.Fore.Green;
                dlg.ForeBlue = tn.Fore.Blue;
                dlg.BackRed = tn.Back.Red;
                dlg.BackGreen = tn.Back.Green;
                dlg.BackBlue = tn.Back.Blue;

                dlg.NodeNameText = this.TreeView1.SelectedNode.Text;

                DialogResult result = dlg.ShowDialog((Form1)this.ParentForm);

                bool isSuccessful = true;
                try
                {
                    System.IO.Path.GetFullPath(dlg.NodeNameText);
                }
                catch (Exception)
                {
                    // ファイル名に使えない文字や、文字数が長すぎた時に例外が投げられます。
                    Form1 form1 = (Form1)this.ParentForm;
                    MessageBox.Show(form1, "ファイル名に使えない名前？\n[" + dlg.NodeNameText + "]\n処理を中断します。", "何らかのエラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    isSuccessful = false;
                }

                if (isSuccessful)
                {
                    // ツリーが更新されたと判定。
                    this.Contents.IsChangedTree = true;

                    this.TreeView1.SelectedNode.ImageIndex = dlg.SelectedImageIndex;
                    this.TreeView1.SelectedNode.SelectedImageIndex = dlg.SelectedImageIndex;

                    tn.Fore.Red = dlg.ForeRed;
                    tn.Fore.Green = dlg.ForeGreen;
                    tn.Fore.Blue = dlg.ForeBlue;
                    tn.Back.Red = dlg.BackRed;
                    tn.Back.Green = dlg.BackGreen;
                    tn.Back.Blue = dlg.BackBlue;
                    this.TreeView1.SelectedNode.ForeColor = Color.FromArgb(255, dlg.ForeRed, dlg.ForeGreen, dlg.ForeBlue);
                    this.TreeView1.SelectedNode.BackColor = Color.FromArgb(255, dlg.BackRed, dlg.BackGreen, dlg.BackBlue);

                    if (this.TreeView1.SelectedNode.Text != dlg.NodeNameText)
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
                        string srcTextFile = this.GetTextFile(this.Contents.ProjectFolder, this.TreeView1.SelectedNode.Text, false);
                        string srcCsvFile = this.GetResourceCsvFile(this.Contents.ProjectFolder, this.TreeView1.SelectedNode.Text, false);
                        //移動先ファイル名
                        string dstTextFile = this.GetTextFile(this.Contents.ProjectFolder, dlg.NodeNameText, false);
                        string dstCsvFile = this.GetResourceCsvFile(this.Contents.ProjectFolder, dlg.NodeNameText, false);

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

                        bool textA = false;
                        bool textB = false;
                        bool textC = false;
                        bool textD = false;
                        bool csvA = false;
                        bool csvB = false;
                        bool csvC = false;
                        bool csvD = false;

                        if (!File.Exists(srcTextFile))
                        {
                            if (!File.Exists(dstTextFile))
                            {
                                //（１）
                                System.Console.WriteLine("TXT（１）と判断");
                                textB = true;
                            }
                            else
                            {
                                //（２）
                                System.Console.WriteLine("TXT（２）と判断");
                                textA = true;
                                textC = true;
                            }
                        }
                        else
                        {
                            if (!File.Exists(dstTextFile))
                            {
                                //（３）
                                System.Console.WriteLine("TXT（３）と判断");
                                textD = true;
                            }
                            else
                            {
                                //（４）
                                System.Console.WriteLine("TXT（４）と判断");
                                textA = true;
                                textC = true;
                            }
                        }

                        if (!File.Exists(srcCsvFile))
                        {
                            if (!File.Exists(dstCsvFile))
                            {
                                //（１）
                                System.Console.WriteLine("CSV（１）と判断");
                                csvB = true;
                            }
                            else
                            {
                                //（２）
                                System.Console.WriteLine("CSV（２）と判断");
                                csvA = true;
                                csvC = true;
                            }
                        }
                        else
                        {
                            if (!File.Exists(dstCsvFile))
                            {
                                //（３）
                                System.Console.WriteLine("CSV（３）と判断");
                                csvD = true;
                            }
                            else
                            {
                                //（４）
                                System.Console.WriteLine("CSV（４）と判断");
                                csvA = true;
                                csvC = true;
                            }
                        }

                        Form1 form1 = (Form1)this.ParentForm;

                        //─────
                        //（Ａ）保存を要求し、
                        //　　　　断れば中断する
                        //─────
                        if (textA || csvA)
                        {
                            DialogResult result2 = MessageBox.Show(form1, "ノード名が変更されました。\n処理を続けるには、現在開いているファイルを保存する必要があります。\n[" + srcTextFile + "]\n[" + srcCsvFile + "]\n保存しますか？", "警告", MessageBoxButtons.YesNoCancel);
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
                        if (textB)
                        {
                            string contents = "";
                            this.CreateSampleTextFile1(srcTextFile, out contents);
                            this.Contents.TextNow(srcTextFile, contents);
                        }

                        if (csvB)
                        {
                            string contents = "";
                            this.CreateSampleResourceCsvFile1(srcCsvFile, out contents);
                            this.Contents.CsvNow(srcCsvFile, contents);
                        }


                        //─────
                        //（Ｃ）移動先ファイルをロードし
                        //　　　　カレントとする
                        //─────
                        if (textC)
                        {
                            // 既にあるテキストファイル
                            MessageBox.Show("既にあるテキストファイルを読み込みます。テキスト[" + dstTextFile + "]");
                            Actions.LoadPageText(form1, dstTextFile);
                        }

                        if (csvC)
                        {
                            // 既にあるCSVファイル
                            MessageBox.Show("既にあるCSVファイルを読み込みます。リソース[" + dstCsvFile + "]");
                            Actions.LoadPageCsv(form1, dstCsvFile);
                        }


                        //─────
                        //（Ｄ）移動元ファイルをリネームし
                        //　　　　移動先をカレントとする
                        //─────
                        if (textD)
                        {
                            File.Move(srcTextFile, dstTextFile);
                            this.Contents.TextNow(dstTextFile, this.Contents.SavedText);
                        }

                        if (csvD)
                        {
                            File.Move(srcCsvFile, dstCsvFile);
                            this.Contents.CsvNow(dstCsvFile, this.Contents.SavedCsv);
                        }


                        //━━━━━
                        // ノード名の変更
                        //━━━━━
                        this.TreeView1.SelectedNode.Text = dlg.NodeNameText;//ツリービュー
                        this.UiTextside1.NodeNameTxt1.Text = dlg.NodeNameText;//ノード名表示
                    }

                gt_Abort://処理を中断した場合の飛び先。
                    ;
                }

                dlg.Dispose();
            }
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


            if (null == this.TreeView1.SelectedNode)
            {

            }
            else if (null != this.TreeView1.SelectedNode.Parent)
            {
                TreeNode2 tn = this.NewTreeNode(nodeName, 0, "#000000", "#FFFFFF");

                //テキスト
                {
                    string contents;
                    this.CreateSampleTextFile1(this.GetTextFile(this.Contents.ProjectFolder, nodeName, false), out contents);
                }

                //CSV
                {
                    string contents;
                    this.CreateSampleResourceCsvFile1(this.GetResourceCsvFile(this.Contents.ProjectFolder, nodeName, false), out contents);
                }

                this.TreeView1.SelectedNode.Parent.Nodes.Add(tn);
            }
            else
            {
                TreeNode2 tn = this.NewTreeNode(nodeName, 0, "#000000", "#FFFFFF");

                //テキスト
                {
                    string contents;
                    this.CreateSampleTextFile1(this.GetTextFile(this.Contents.ProjectFolder, nodeName, false), out contents);
                }

                //CSV
                {
                    string contents;
                    this.CreateSampleResourceCsvFile1(this.GetResourceCsvFile(this.Contents.ProjectFolder, nodeName, false), out contents);
                }

                // トップ階層の場合
                this.TreeView1.Nodes.Add(tn);
                //System.Console.WriteLine("親要素がないので、弟ノードを足せません。");
            }
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

            this.TreeView1.Nodes.Remove(this.TreeView1.SelectedNode);
            this.Contents.TextNow("", "");
            this.Contents.CsvNow("", "");

            this.UiTextside1.Clear();
        }

        /// <summary>
        /// データ修正ボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton45_Click(object sender, EventArgs e)
        {
            ModifyDialog dlg = new ModifyDialog();

            DialogResult result = dlg.ShowDialog((Form1)this.ParentForm);



            dlg.Dispose();
        }

        /// <summary>
        /// ノードを上に移動するボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            this.MoveTreeNodeInSiblings(true);
        }

        /// <summary>
        /// ノードを下に移動するボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            this.MoveTreeNodeInSiblings(false);
        }

        private void MoveTreeNodeInSiblings(Boolean upward)
        {
            //要素をCloneしてコピーすると比較的簡単に入れ替えられる//

            TreeNode2 nodeClone = (TreeNode2)this.TreeView1.SelectedNode.Clone();
            TreeNode2 node = (TreeNode2)this.TreeView1.SelectedNode;
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

            this.TreeView1.Visible = false;//描画対策
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
            this.TreeView1.Visible = true;
            this.TreeView1.SelectedNode = nodeClone;
            this.TreeView1.Focus();

            // ツリーに更新があったものと判定します。
            this.Contents.IsChangedTree = true;
            this.RefreshTitleBar();
        }

        /// <summary>
        /// 親ノードへ移動
        /// </summary>
        /// <param name="upward"></param>
        private void MoveTreeNodeToParent()
        {
            //要素をCloneしてコピーすると比較的簡単に入れ替えられる//

            TreeNode2 nodeClone = (TreeNode2)this.TreeView1.SelectedNode.Clone();
            TreeNode2 node = (TreeNode2)this.TreeView1.SelectedNode;
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
                    this.TreeView1.Visible = false;//描画対策

                    //元の位置を消して
                    parentNode.Nodes.Remove(node);

                    //ルートのクローンを追加します。
                    this.TreeView1.Nodes.Add(nodeClone);

                    this.TreeView1.Visible = true;
                }
            }
            else
            {
                //親の親
                TreeNode2 parentParentNode = (TreeNode2)parentNode.Parent;

                //描画対策付き
                {
                    this.TreeView1.Visible = false;//描画対策

                    //元の位置を消して
                    parentNode.Nodes.Remove(node);

                    //移動先にクローンを追加します。
                    parentParentNode.Nodes.Add(nodeClone);

                    this.TreeView1.Visible = true;
                }
            }


            this.TreeView1.SelectedNode = nodeClone;
            this.TreeView1.Focus();

            // ツリーに更新があったものと判定します。
            this.Contents.IsChangedTree = true;
            this.RefreshTitleBar();
        }

        /// <summary>
        /// フォルダーを開くボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Actions.Open((Form1)this.ParentForm);
        }

        /// <summary>
        /// ノードを親へ移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton46_Click(object sender, EventArgs e)
        {
            this.MoveTreeNodeToParent();
        }

        /// <summary>
        /// 子ノードの追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton23_Click(object sender, EventArgs e)
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


            if (null == this.TreeView1.SelectedNode)
            {

            }
            else
            {
                TreeNode2 tn = this.NewTreeNode(nodeName, 0, "#000000", "#FFFFFF");

                //テキスト
                {
                    string contents;
                    this.CreateSampleTextFile1(this.GetTextFile(this.Contents.ProjectFolder, nodeName, false), out contents);
                }

                //CSV
                {
                    string contents;
                    this.CreateSampleResourceCsvFile1(this.GetResourceCsvFile(this.Contents.ProjectFolder, nodeName, false), out contents);
                }

                this.TreeView1.SelectedNode.Nodes.Add(tn);
            }

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

            this.Contents.IsRtfMode = dlg.UiConfiguration1.IsRtf;

            dlg.Dispose();
        }


    }
}
