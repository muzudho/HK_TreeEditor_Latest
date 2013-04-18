using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeEditor
{
    public class Utility
    {

        /// <summary>
        /// RTF相対パスから、CSV相対パスを算出。
        /// </summary>
        /// <param name="rtfRel"></param>
        /// <returns></returns>
        public static string RtfToCsv(string rtfRel)
        {
            string csvRel;

            if (rtfRel.ToLower().EndsWith(".rtf"))
            {
                csvRel = rtfRel.Substring(0, rtfRel.Length - ".rtf".Length) + ".csv";
            }
            else
            {
                csvRel = "";
            }

            return csvRel;
        }


        /// <summary>
        /// だいたい、一意になるだろうと思われる名前。既存するかどうかは調べていない。
        /// </summary>
        /// <returns></returns>
        public static string CreateUniqueFileName(UiMain uiMain1)
        {
            StringBuilder s = new StringBuilder();

            //タイムスタンプ
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
            s.Append("_");
            s.Append(uiMain1.NextId());//時間＋連番で、ある程度一意性を確保

            return s.ToString();
        }

        public static string CreateRtfRel(string fileNameWithoutExtension)
        {
            return @"node\" + fileNameWithoutExtension + @".rtf";
        }


        /// <summary>
        /// ノードを適当に作成。
        /// </summary>
        /// <param name="uiMain1"></param>
        /// <returns></returns>
        public static TreeNode2 CreateNode(UiMain uiMain1, TreeNode2 cloneSource, out string nodeText, out string nodeRel)
        {
            TreeNode2 tn;

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
                s.Append("_");
                s.Append(uiMain1.NextId());

                nodeText = s.ToString();
                nodeRel = @"node\" + nodeText + @".rtf";
            }

            tn = uiMain1.CreateTreeNode(cloneSource, nodeText, nodeRel, 0, "#000000", "#FFFFFF");

            return tn;
        }

    }
}
