using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace CompareLogsParser
{
   public class FileIO
    {
       // public string FileFullPath { get; set; }
     
        public static List<string> ReadFileAllLines(string fileFullPath)
        {
            List<string> strs = new List<string>();
            strs = File.ReadAllLines(fileFullPath).ToList();
            return strs;
        }

        public static List<LogLineResult> SelectLinesContainKeyword(List<string> strs, string keyword, bool regularMode = false)
        {
            List<LogLineResult> rtnList = new List<LogLineResult>();
            int i = 0;
            foreach (var item in strs)
            {
                if(regularMode)
                {

                }
                else
                {
                    if(item.Contains(keyword))
                    {
                        LogLineResult logLineResult = new LogLineResult()
                        {
                            Index =i,
                            LineKeyword =keyword,
                            LineContent = item.Remove(0, item.IndexOf(keyword) + keyword.Length),
                            IsMatched =false
                        };
                        rtnList.Add(logLineResult);
                        i++;
                    }
                }
            }

            return rtnList;
        }

    }
}
