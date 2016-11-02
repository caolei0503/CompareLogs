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
        }

        List<string> StandardLogKeyLines;
        List<string> TargetLogKeyLines;
        private void OpenStandardLog_Click(object sender, RoutedEventArgs e)
        {
            string fileFullName = LoadFilePath();// @"D:\PL5\TasksForPractice\CommonLog\Logs\Debug.log";
            string keyword = "dbput";
            //LoadFileContent(fileFullName, keyword, this.StandardLog);

            FlowDocument Doc = new FlowDocument();
            StandardLogKeyLines = FileIO.SelectLinesContainKeyword(FileIO.ReadFileAllLines(fileFullName), keyword);
            LoadStrsToTexBoxDoc(Doc, keyword, StandardLogKeyLines);
            this.StandardLog.Document = Doc;
        }
       
       //add a comment
        string LoadFilePath()
        {
            string fileFullName = string.Empty;
            //Create a new instance of the OpenFileDialog because it's an object.
            OpenFileDialog dialog = new OpenFileDialog();
            //Now set the file type
            dialog.Filter = "log files (*.log)|*.log|All files (*.*)|*.*";
            //Set the starting directory and the title.
            //dialog.InitialDirectory = "C:";
            dialog.Title = "Select a log file";
            dialog.RestoreDirectory = true;
            //Present to the user.
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                fileFullName = dialog.FileName;
            return fileFullName;
        }

        private void OpenTargetLog_Click(object sender, RoutedEventArgs e)
        {
            string fileFullName = LoadFilePath();// @"D:\PL5\TasksForPractice\CommonLog\Logs\Debug_1.log";       
            string keyword = "dbput";

            FlowDocument Doc = new FlowDocument();
            TargetLogKeyLines = FileIO.SelectLinesContainKeyword(FileIO.ReadFileAllLines(fileFullName), keyword);

            CompareLogLines compareLogs = new CompareLogLines(StandardLogKeyLines,TargetLogKeyLines);
            compareLogs.ExecuteCompareLogs();

            LoadLogLineResultsToTexBoxDoc(Doc, keyword, compareLogs.TargetLogLinesResults);
            this.TargetLog.Document = Doc;

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

        private void LoadStrsToTexBoxDoc(FlowDocument Doc, string Keyword, List<string> LogLineResults)
        {
            
            foreach (var item in LogLineResults)
            {
                //Paragraph p1 = new Paragraph(new Run(Keyword)); // Paragraph 类似于 html 的 P 标签  
                //p1.Foreground = new SolidColorBrush(Colors.Green);//设置字体颜色

                Paragraph p2 = new Paragraph();
                p2.Inlines.Add(new Run(Keyword + item)); // Paragraph 类似于 html 的 P 标签          
                p2.Foreground = new SolidColorBrush(Colors.Black);//设置字体颜色    
                Doc.Blocks.Add(p2);
            }

            // Doc.Blocks.Add(p1);
            
        }

        private void LoadLogLineResultsToTexBoxDoc(FlowDocument Doc, string Keyword, List<LogLineResult> LogLineResults)
        {
            
            foreach (var item in LogLineResults)
            {
                Paragraph p = new Paragraph(); // Paragraph 类似于 html 的 P 标签  
                var r = new Run(Keyword + item.LineContent); // Run 是一个 Inline 的标签  
                p.Inlines.Add(r);
                p.Foreground = new SolidColorBrush(item.IsMatched? Colors.Green: Colors.Red);//设置字体颜色 
                Doc.Blocks.Add(p);
            }                       
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
