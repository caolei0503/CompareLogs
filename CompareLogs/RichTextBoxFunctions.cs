using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using CompareLogsParser;

namespace CompareLogsUI
{
    public partial class MainWindow : Window
    {
        private FlowDocument StrsToTexBoxDoc(string Keyword, List<string> LogLineResults)
        {
            FlowDocument Doc = new FlowDocument();
            foreach (var item in LogLineResults)
            {
                //Paragraph p1 = new Paragraph(new Run(Keyword)); // Paragraph 类似于 html 的 P 标签  
                //p1.Foreground = new SolidColorBrush(Colors.Green);//设置字体颜色

                Paragraph p2 = new Paragraph();
                p2.Inlines.Add(new Run(Keyword + item)); // Paragraph 类似于 html 的 P 标签          
                p2.Foreground = new SolidColorBrush(Colors.Black);//设置字体颜色    
                Doc.Blocks.Add(p2);
            }
            return Doc;
        }

        private FlowDocument LogLineResultsToTextBoxDoc(List<LogLineResult> LogLineResults, Color colorMatch, Color colorDisMatch)
        {

            FlowDocument Doc = new FlowDocument();
            if (isOnlyShowDifference)
            {
                foreach (var item in LogLineResults)
                {
                    if (!item.IsMatched)
                    {
                        Paragraph p = new Paragraph(); // Paragraph 类似于 html 的 P 标签  
                        var r = new Run(item.LineKeyword + item.LineContent); // Run 是一个 Inline 的标签  
                        p.Inlines.Add(r);
                        p.Foreground = new SolidColorBrush(colorDisMatch);//设置字体颜色 
                        Doc.Blocks.Add(p);
                    }
                }
            }
            else
            {
                foreach (var item in LogLineResults)
                {
                    Paragraph p = new Paragraph(); // Paragraph 类似于 html 的 P 标签  
                    var r = new Run(item.LineKeyword + item.LineContent); // Run 是一个 Inline 的标签  
                    p.Inlines.Add(r);
                    p.Foreground = new SolidColorBrush(item.IsMatched ? colorMatch : colorDisMatch);//设置字体颜色 
                    Doc.Blocks.Add(p);
                }
            }

            return Doc;
        }

    }
}
