using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeEditor
{


    public class Contents
    {

        /// <summary>
        /// リッチテキストボックスの x 移動量。
        /// </summary>
        private int scrollX;
        public int ScrollX
        {
            get
            {
                return scrollX;
            }
            set
            {
                scrollX = value;
            }
        }

        /// <summary>
        /// リッチテキストボックスの y 移動量。
        /// </summary>
        private int scrollY;
        public int ScrollY
        {
            get
            {
                return scrollY;
            }
            set
            {
                scrollY = value;
            }
        }



        /// <summary>
        /// 別ページでコピーしたものがクリップボードに残っているかどうかを判定。
        /// 
        /// 0:　なにもない。
        /// 1:　同じページでコピーしたものがクリップボードにある。
        /// 2:　別のページでコピーしたものがクリップボードに残っている。
        /// </summary>
        private int anothePageCopy;
        public int AnotherPageCopy
        {
            get
            {
                return anothePageCopy;
            }
            set
            {
                anothePageCopy = value;
            }
        }


        /// <summary>
        /// プロジェクト名。
        /// </summary>
        private string projectName;
        public string ProjectName
        {
            get
            {
                return projectName;
            }
            //set
            //{
            //    projectName = value;
            //}
        }


        /// <summary>
        /// プロジェクト・フォルダー。
        /// </summary>
        private string projectFolder;
        public string ProjectFolder
        {
            get
            {
                return projectFolder;
            }
            //set
            //{
            //    projectFolder = value;
            //}
        }

        /// <summary>
        /// プロジェクトの場所をセット。
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="projectFolder"></param>
        public void SetProject(string projectName, string projectFolder)
        {
            this.projectName = projectName;
            this.projectFolder = projectFolder;
        }

        /// <summary>
        /// 保存されているRTF。変更を判定するための比較用。
        /// </summary>
        private string savedRtf;
        public string SavedRtf
        {
            get
            {
                return savedRtf;
            }
            set
            {
                savedRtf = value;
            }
        }

        /// <summary>
        /// 保存されているCSV。変更を判定するための比較用。
        /// </summary>
        private string savedCsv;
        public string SavedCsv
        {
            get
            {
                return savedCsv;
            }
            set
            {
                savedCsv = value;
            }
        }

        /// <summary>
        /// 編集中のRTFのファイルパス。プロジェクト・フォルダーからの相対パス。
        /// </summary>
        private string rtfFileRel;
        public string RtfRel
        {
            get
            {
                return rtfFileRel;
            }
            set
            {
                rtfFileRel = value;
            }
        }

        /// <summary>
        /// プロジェクト（ツリー、カレントRTF、カレントCSV）の
        /// いずれかを変更していれば真。保存しているものと変わらなければ偽。
        /// </summary>
        public bool IsChangedProject
        {
            get
            {
                return this.IsChangedTree || this.IsChangedRtf || this.IsChangedCsv;
            }
        }


        /// <summary>
        /// ツリーを編集したら真。
        /// </summary>
        private bool isChangedTree;
        public bool IsChangedTree
        {
            get
            {
                return isChangedTree;
            }
            set
            {
                isChangedTree = value;
            }
        }

        /// <summary>
        /// カレントRTFを編集したら真。
        /// </summary>
        private bool isChangedRtf;
        public bool IsChangedRtf
        {
            get
            {
                return isChangedRtf;
            }
            set
            {
                isChangedRtf = value;
            }
        }

        /// <summary>
        /// CSVモードのカレント画像素材を編集したら真。
        /// </summary>
        private bool isChangedCsv;
        public bool IsChangedCsv
        {
            get
            {
                return isChangedCsv;
            }
            set
            {
                isChangedCsv = value;
            }
        }

        public void Clear()
        {
            //プロジェクト／ツリー
            this.SetProject( "", "");
            this.IsChangedTree = false;

            //RTF
            this.RtfRel = "";
            this.SavedRtf = "";
            this.IsChangedRtf = false;

            //CSV
            this.SavedCsv = "";
            this.IsChangedCsv = false;

            //スクロールバー
            this.ScrollX = 0;
            this.ScrollY = 0;
        }

        public Contents()
        {
            this.Clear();
        }

        /// <summary>
        /// @"node\食べ物.rtf" といった、プロジェクト・フォルダーからの相対パスで設定してください。
        /// </summary>
        /// <param name="file"></param>
        /// <param name="contents"></param>
        public void RtfNow(string file, string contents)
        {
            this.RtfRel = file;
            this.SavedRtf = contents;
        }

        /// <summary>
        /// @"node\食べ物.csv" といった、プロジェクト・フォルダーからの相対パスで設定してください。
        /// </summary>
        /// <param name="file"></param>
        /// <param name="contents"></param>
        public void CsvNow(string contents)
        {
            this.SavedCsv = contents;
        }

    }


}
