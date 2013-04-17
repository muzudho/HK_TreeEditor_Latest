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
        /// 編集中のテキストのファイルパス。
        /// </summary>
        private string rtfFile;
        public string RtfFile
        {
            get
            {
                return rtfFile;
            }
            set
            {
                rtfFile = value;
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
                return this.IsChangedTree || this.IsChangedRtf;
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

        public void Clear()
        {
            this.ProjectFolder = "";
            this.IsChangedTree = false;

            this.RtfFile = "";
            this.SavedRtf = "";
            this.IsChangedRtf = false;
        }

        public void RtfNow(string file, string contents)
        {
            this.RtfFile = file;
            this.SavedRtf = contents;
        }

    }


}
