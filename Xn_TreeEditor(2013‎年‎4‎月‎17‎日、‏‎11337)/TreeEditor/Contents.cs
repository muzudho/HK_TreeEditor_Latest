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
        /// プロジェクト・フォルダー。
        /// </summary>
        private string projectFolder;
        public string ProjectFolder
        {
            get
            {
                return projectFolder;
            }
            set
            {
                projectFolder = value;
            }
        }

        /// <summary>
        /// 保存されているテキスト。変更を判定するための比較用。
        /// </summary>
        private string savedText;
        public string SavedText
        {
            get
            {
                return savedText;
            }
            set
            {
                savedText = value;
            }
        }

        /// <summary>
        /// 編集中のテキストのファイルパス。
        /// </summary>
        private string textFile;
        public string TextFile
        {
            get
            {
                return textFile;
            }
            set
            {
                textFile = value;
            }
        }

        /// <summary>
        /// 編集中のテキストのファイルパス。できれば、プロジェクト・フォルダーの相対。
        /// </summary>
        public string TextFileRel
        {
            get
            {

                return textFile;
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
        /// 編集中のCSVのファイルパス。
        /// </summary>
        private string csvFile;
        public string CsvFile
        {
            get
            {
                return csvFile;
            }
            set
            {
                csvFile = value;
            }
        }


        /// <summary>
        /// プロジェクト（ツリー、カレントテキスト、カレントCSV）の
        /// いずれかを変更していれば真。保存しているものと変わらなければ偽。
        /// </summary>
        public bool IsChangedProject
        {
            get
            {
                return this.IsChangedTree || this.IsChangedText || this.IsChangedResource;
            }
        }

        /// <summary>
        /// ページ（カレントテキスト、カレントCSV）の
        /// いずれかを変更していれば真。保存しているものと変わらなければ偽。
        /// </summary>
        public bool IsChangedPage
        {
            get
            {
                return this.IsChangedText || this.IsChangedResource;
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
        /// カレントテキストを編集したら真。
        /// </summary>
        private bool isChangedText;
        public bool IsChangedText
        {
            get
            {
                return isChangedText;
            }
            set
            {
                isChangedText = value;
            }
        }

        /// <summary>
        /// カレント画像素材を編集したら真。
        /// </summary>
        private bool isChangedResource;
        public bool IsChangedResource
        {
            get
            {
                return isChangedResource;
            }
            set
            {
                isChangedResource = value;
            }
        }


        public void Clear()
        {
            this.ProjectFolder = "";
            this.IsChangedTree = false;

            this.TextFile = "";
            this.SavedText = "";
            this.IsChangedText = false;

            this.CsvFile = "";
            this.SavedCsv = "";
            this.IsChangedResource = false;
        }

        public void TextNow(string file, string contents)
        {
            this.TextFile = file;
            this.SavedText = contents;
        }

        public void CsvNow(string file, string contents)
        {
            this.CsvFile = file;
            this.SavedCsv = contents;
        }

    }


}
