using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareLogsParser
{
    public class CompareLogLines
    {
        //public List<string> StandardLogLines { get; set; }
        //public List<string> TargetLogLines { get; set; }

        public List<LogLineResult> StandardLogLinesResults { get; private set; }
        public List<LogLineResult> TargetLogLinesResults { get; private set; }

        //public CompareLogLines(List<string> standardLogLines, List<string> targetLogLines)
        //{
        //    this.StandardLogLines = standardLogLines;
        //    this.TargetLogLines = targetLogLines;
        //    //InitLogLinesResults();
        //}

        public CompareLogLines(List<LogLineResult> standardLogLinesResults, List<LogLineResult> targetLogLinesResults)
        {
            this.StandardLogLinesResults = standardLogLinesResults;
            this.TargetLogLinesResults = targetLogLinesResults;
        }

        public void ExecuteCompareLogs()
        {
            int count = Math.Min(this.StandardLogLinesResults.Count, this.TargetLogLinesResults.Count);
            for (int i = 0; i < count; i++)
            {
                if (StandardLogLinesResults[i].Equals(TargetLogLinesResults[i]))
                {
                    TargetLogLinesResults[i].IsMatched = true;
                    StandardLogLinesResults[i].IsMatched = true;
                }
            }
        }

        //void InitLogLinesResults()
        //{
        //    InitStandardLogLinesResults();
        //    InitTargetLogLinesResults();
        //}
        //public void ExecuteCompareLogs()
        //{            
        //    int count = Math.Min(this.StandardLogLines.Count,this.TargetLogLines.Count);
        //    for (int i = 0; i < count; i++)
        //    {
        //        if (StandardLogLines[i] == TargetLogLines[i])
        //        {
        //            TargetLogLinesResults[i].IsMatched = true;
        //            StandardLogLinesResults[i].IsMatched = true;
        //        }
        //    }
        //}

        //void InitStandardLogLinesResults()
        //{
        //    StandardLogLinesResults = new List<LogLineResult>();
        //    int i = 0;
        //    foreach (var item in StandardLogLines)
        //    {
        //        LogLineResult standardLogLinesResult = new LogLineResult() { Index = i, LineContent=item, IsMatched=false };
        //        StandardLogLinesResults.Add(standardLogLinesResult);
        //        i++;
        //    }
        //}

        //void InitTargetLogLinesResults()
        //{
        //    TargetLogLinesResults = new List<LogLineResult>();
        //    int i = 0;
        //    foreach (var item in TargetLogLines)
        //    {
        //        LogLineResult targetLogLinesResult = new LogLineResult() { Index = i, LineContent = item, IsMatched = false };
        //        TargetLogLinesResults.Add(targetLogLinesResult);
        //        i++;
        //    }
        //}
    }

}
