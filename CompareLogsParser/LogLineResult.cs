using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareLogsParser
{
    public class LogLineResult
    {
        public int Index;
        public string LineKeyword;
        public string LineContent;
        public bool IsMatched;
        public override bool Equals(object obj)
        {
            bool isEqual = false;
            LogLineResult other = (LogLineResult)obj;
            if (other.LineKeyword == this.LineKeyword && other.LineContent == this.LineContent) isEqual = true;
            return isEqual;
        }
    }

    /// <summary>
    /// reserve for future
    /// different status, different color
    /// </summary>
    enum MatchStatus
    {
        Matched,
        Alike,
        TotallyDifferent
    }
}
