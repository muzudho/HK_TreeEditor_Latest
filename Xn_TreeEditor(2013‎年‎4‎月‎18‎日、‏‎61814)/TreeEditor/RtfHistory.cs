using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeEditor
{
    public class RtfHistory
    {

        /// <summary>
        /// RTFの丸ごと履歴。
        /// </summary>
        private List<string> historyRtf;
        public void Add(string rtf)
        {
            this.historyRtf.Add(rtf);
            this.historyCursor++;

            //上限
            if (100 < this.historyRtf.Count)
            {
                this.historyRtf.RemoveAt(0);
                this.historyCursor--;
            }
        }
        private int historyCursor;


        public RtfHistory()
        {
            this.Clear();
        }

        public void Clear()
        {
            this.historyRtf = new List<string>();
            this.historyCursor = -1;
        }

        public bool TryUndo(out string rtf)
        {
            bool result;

            if (0 < this.historyRtf.Count && 0 < this.historyCursor)
            {
                this.historyCursor--;
                rtf = this.historyRtf[this.historyCursor];
                result = true;
            }
            else
            {
                rtf = "";
                result = false;
            }

            return result;
        }
        public bool TryRedo(out string rtf)
        {
            bool result;

            if (this.historyCursor < this.historyRtf.Count - 1)
            {
                this.historyCursor++;
                rtf = this.historyRtf[this.historyCursor];
                result = true;
            }
            else
            {
                rtf = "";
                result = false;
            }

            return result;
        }

    }
}
