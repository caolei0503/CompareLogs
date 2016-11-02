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

        public static List<string> SelectLinesContainKeyword(List<string> strs, string keyword, bool regularMode = false)
        {
            List<string> rtnList = new List<string>();
            foreach (var item in strs)
            {
                if(regularMode)
                {

                }
                else
                {
                    if(item.Contains(keyword))
                    {
                        rtnList.Add(item.Remove(0,item.IndexOf(keyword)+keyword.Length));
                    }
                }
            }

            return rtnList;
        }

    }
}
