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

namespace CompareLogsUI
{
    public partial class MainWindow : Window
    {
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
    }
}
