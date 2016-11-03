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
using System.Windows.Forms;
//add a comment, just for trying the GIT interact with the VS2015.
//add a comment, try the VS2015 interact with the GIT.

namespace CompareLogsUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            this.OpenStandardLog.Click += OpenStandardLog_Click;
            this.OpenTargetLog.Click += OpenTargetLog_Click;
            this.ShowLogDiff.Click += ShowLogDiff_Click;
        }

        bool isOnlyShowDifference = false;
        private void ShowLogDiff_Click(object sender, RoutedEventArgs e)
        {
            this.isOnlyShowDifference = this.ShowLogDiff.IsChecked.Value;
            UpdateResultsToTextBox();
        }

        List<LogLineResult> StandardLogKeyLines;
        List<LogLineResult> TargetLogKeyLines;
        private void OpenStandardLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileFullName = LoadFilePath();// @"D:\PL5\TasksForPractice\CommonLog\Logs\Debug.log";            
                string keyword = "dbput";
                StandardLogKeyLines = FileIO.SelectLinesContainKeyword(FileIO.ReadFileAllLines(fileFullName), keyword);

                keyword = "dbcc";
                StandardLogKeyLines.AddRange(FileIO.SelectLinesContainKeyword(FileIO.ReadFileAllLines(fileFullName), keyword));
                this.StandardLog.Document = LogLineResultsToTextBoxDoc(StandardLogKeyLines, Colors.Black, Colors.Black);
            }
            catch (Exception)
            {

             //   throw;
            }            
        }
       
        
        private void OpenTargetLog_Click(object sender, RoutedEventArgs e)
        {
            string fileFullName = LoadFilePath();// @"D:\PL5\TasksForPractice\CommonLog\Logs\Debug_1.log";                   

            string keyword = "dbput";
            TargetLogKeyLines = FileIO.SelectLinesContainKeyword(FileIO.ReadFileAllLines(fileFullName), keyword);
            keyword = "dbcc";
            TargetLogKeyLines.AddRange(FileIO.SelectLinesContainKeyword(FileIO.ReadFileAllLines(fileFullName), keyword));

            CompareTwoLogs();
            UpdateResultsToTextBox();
        }

        string LoadFilePath()
        {
            string fileFullName = string.Empty;
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();//Create a new instance of the OpenFileDialog because it's an object.
                dialog.Filter = "log files (*.log)|*.log|All files (*.*)|*.*";//Now set the file type
                dialog.Title = "Select a log file";
                dialog.RestoreDirectory = true;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) fileFullName = dialog.FileName;
            }
            catch (Exception)
            {
               // throw;
            }            
            return fileFullName;
        }
        void UpdateResultsToTextBox()
        {
            try
            {
                this.StandardLog.Document = LogLineResultsToTextBoxDoc(compareLogs.StandardLogLinesResults, Colors.Black, Colors.Black);
                this.TargetLog.Document = LogLineResultsToTextBoxDoc(compareLogs.TargetLogLinesResults, Colors.Green, Colors.Red);
            }
            catch (Exception e)
            {

                //throw e;
            }         
        }

        CompareLogLines compareLogs;
        void CompareTwoLogs()
        {
            try
            {
                compareLogs = new CompareLogLines(StandardLogKeyLines, TargetLogKeyLines);
                compareLogs.ExecuteCompareLogs();
            }
            catch (Exception e)
            {
               // throw e;
            }
            
        }
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
                    if(!item.IsMatched)
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

        static double commonVerticalOffset = 0;
        private void SynchronizeScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                ScrollViewer sv = e.OriginalSource as ScrollViewer;
                commonVerticalOffset = sv.VerticalOffset;
                this.TargetLog.ScrollToVerticalOffset(commonVerticalOffset);
                this.StandardLog.ScrollToVerticalOffset(commonVerticalOffset);
            }
            catch
            { }
        }
        private void StandardScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                ScrollViewer sv = e.OriginalSource as ScrollViewer;
                commonVerticalOffset = sv.VerticalOffset;
                this.TargetLog.ScrollToVerticalOffset(commonVerticalOffset);

            }
            catch
            { }
        }

        private void TargetScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                ScrollViewer sv = e.OriginalSource as ScrollViewer;
                commonVerticalOffset = sv.VerticalOffset;
                this.StandardLog.ScrollToVerticalOffset(commonVerticalOffset);
            }
            catch
            { }
        }

        /// <summary>
        /// demo
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="Doc"></param>      
        private void Test(FlowDocument Doc, Color textColor, string str)
        {
            Paragraph p = new Paragraph(); // Paragraph 类似于 html 的 P 标签  
            var r = new Run(str); // Run 是一个 Inline 的标签  
            p.Inlines.Add(r);
            p.Foreground = new SolidColorBrush(textColor);//设置字体颜色   
            Doc.Blocks.Add(p);
        }

    }
}
