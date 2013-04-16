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
        /// プロジェクト名。
        /// </summary>
        private string projectName;
        public string ProjectName
        {
            get
            {
                return projectName;
            }
            set
            {
                projectName = value;
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
        /// テキストまたはCSVのどちらかを変更していれば真。保存しているものと変わらなければ偽。
        /// </summary>
        private bool isChangedPage;
        public bool IsChangedPage
        {
            get
            {
                return isChangedPage;
            }
            set
            {
                isChangedPage = value;
            }
        }

        /// <summary>
        /// 画像素材を編集したら真。
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
            this.ProjectName = "";
            this.IsChangedPage = false;

            this.TextFile = "";
            this.SavedText = "";

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
