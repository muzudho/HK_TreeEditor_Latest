using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeEditor
{
    public class TextHistory
    {

        /// <summary>
        /// テキストの丸ごと履歴。
        /// </summary>
        private List<string> historyText;
        public void Add(string text)
        {
            this.historyText.Add(text);
            this.historyCursor++;

            //上限
            if (100 < this.historyText.Count)
            {
                this.historyText.RemoveAt(0);
                this.historyCursor--;
            }
        }
        private int historyCursor;


        public TextHistory()
        {
            this.historyText = new List<string>();
            this.historyCursor = -1;
        }

        public bool TryUndo(out string text)
        {
            bool result;

            if (0 < this.historyText.Count && 0 < this.historyCursor)
            {
                this.historyCursor--;
                text = this.historyText[this.historyCursor];
                result = true;
            }
            else
            {
                text = "";
                result = false;
            }

            return result;
        }
        public bool TryRedo(out string text)
        {
            bool result;

            if (this.historyCursor < this.historyText.Count - 1)
            {
                this.historyCursor++;
                text = this.historyText[this.historyCursor];
                result = true;
            }
            else
            {
                text = "";
                result = false;
            }

            return result;
        }

    }
}
