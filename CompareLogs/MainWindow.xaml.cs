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
            this.SearchKeyword.OnSearch += SearchKeyword_OnSearch;
        }

        private void SearchKeyword_OnSearch(object sender, RoutedEventArgs e)
        {
            CompareTwoLogs(this.SearchKeyword.Text);
            UpdateResultsToTextBox();
        }

        bool isOnlyShowDifference = false;
        private void ShowLogDiff_Click(object sender, RoutedEventArgs e)
        {
            this.isOnlyShowDifference = this.ShowLogDiff.IsChecked.Value;
            UpdateResultsToTextBox();
        }

        List<string> StandardLogAllLines;
        List<string> TargetLogAllLines;
        List<LogLineResult> StandardLogKeyLines;
        List<LogLineResult> TargetLogKeyLines;
        private void OpenStandardLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StandardLogAllLines = FileIO.ReadFileAllLines(LoadFilePath());

                string keyword = "dbput";
                StandardLogKeyLines = FileIO.SelectLinesContainKeyword(this.StandardLogAllLines, keyword);

                keyword = "dbcc";
                StandardLogKeyLines.AddRange(FileIO.SelectLinesContainKeyword(this.StandardLogAllLines, keyword));

                this.StandardLog.Document = LogLineResultsToTextBoxDoc(StandardLogKeyLines, Colors.Black, Colors.Black);
            }
            catch (Exception)
            {
             //   throw;
            }            
        }   
        
        private void OpenTargetLog_Click(object sender, RoutedEventArgs e)
        {
            TargetLogAllLines = FileIO.ReadFileAllLines(LoadFilePath());

            string keyword = "dbput";
            TargetLogKeyLines = FileIO.SelectLinesContainKeyword(this.TargetLogAllLines, keyword);
            keyword = "dbcc";
            TargetLogKeyLines.AddRange(FileIO.SelectLinesContainKeyword(this.TargetLogAllLines, keyword));

            CompareTwoLogs(this.StandardLogKeyLines,this.TargetLogKeyLines);
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
        void CompareTwoLogs(List<LogLineResult> standardLogKeyLines, List<LogLineResult> targetLogKeyLines)
        {
            try
            {
                compareLogs = new CompareLogLines(standardLogKeyLines, targetLogKeyLines);
                compareLogs.ExecuteCompareLogs();
            }
            catch (Exception e)
            {
               // throw e;
            }
            
        }    

        void CompareTwoLogs(string keyword)
        {
            StandardLogKeyLines = FileIO.SelectLinesContainKeyword(this.StandardLogAllLines, keyword);
            TargetLogKeyLines = FileIO.SelectLinesContainKeyword(this.TargetLogAllLines, keyword);
            CompareTwoLogs(this.StandardLogKeyLines, this.TargetLogKeyLines);
        }
    }
}
