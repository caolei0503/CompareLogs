using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareLogsParser
{
    public class CompareLogLines
    {
        public List<string> StandardLogLines { get; set; }
        public List<string> TargetLogLines { get; set; }

        public List<LogLineResult> StandardLogLinesResults;
        public List<LogLineResult> TargetLogLinesResults;

        public CompareLogLines(List<string> standardLogLines, List<string> targetLogLines)
        {
            this.StandardLogLines = standardLogLines;
            this.TargetLogLines = targetLogLines;
            InitLogLinesResults();
        }

        void InitLogLinesResults()
        {
            InitStandardLogLinesResults();
            InitTargetLogLinesResults();
        }
        public void ExecuteCompareLogs()
        {            
            int count = Math.Min(this.StandardLogLines.Count,this.TargetLogLines.Count);
            for (int i = 0; i < count; i++)
            {
                if (StandardLogLines[i] == TargetLogLines[i])
                {
                    TargetLogLinesResults[i].IsMatched = true;
                }
            }
        }

        void InitStandardLogLinesResults()
        {
            StandardLogLinesResults = new List<LogLineResult>();
            int i = 0;
            foreach (var item in StandardLogLines)
            {
                LogLineResult standardLogLinesResult = new LogLineResult() { Index = i, LineContent=item, IsMatched=false };
                StandardLogLinesResults.Add(standardLogLinesResult);
            }
        }

        void InitTargetLogLinesResults()
        {
            TargetLogLinesResults = new List<LogLineResult>();
            int i = 0;
            foreach (var item in TargetLogLines)
            {
                LogLineResult targetLogLinesResult = new LogLineResult() { Index = i, LineContent = item, IsMatched = false };
                TargetLogLinesResults.Add(targetLogLinesResult);
            }
        }
    }

    public class LogLineResult
    {
        public int Index;
        public string LineContent;
       // public string LineKeyword;
        public bool IsMatched;        
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
