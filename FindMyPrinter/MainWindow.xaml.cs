using FindMyPrinter.Models;
using System.Data.Common;
using System.Diagnostics;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FindMyPrinter
{
    public partial class MainWindow : Window
    {
        public List<Printer> Printers { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            this.Printers = new List<Printer>();
            Printers.Add(new Printer("192.168.1.48", "Kék"));
            Printers.Add(new Printer("192.168.1.245", "Szürke"));
        }

        private void CopyIP_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.Tag is string ipAddress)
            {
                Clipboard.SetText(ipAddress);
            }
        }

        private void OpenBrowser_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.Tag is string ipAddress)
            {
                Process.Start(new ProcessStartInfo("http://" + ipAddress) { UseShellExecute = true });
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Total width of the window, excluding some margin or padding
            double totalWidth = PrinterListView.ActualWidth;

            // Subtract space for padding/margins (10px for example)
            double usableWidth = totalWidth - 20;

            // Calculate each column width (assuming you want equal width for all columns)
            double columnWidth = usableWidth / 4; // 4 columns in total

            // Set the width of each column dynamically
            ipColumn.Width = columnWidth;
            nameColumn.Width = columnWidth;
            copyColumn.Width = columnWidth;
            browserColumn.Width = columnWidth;
        }

        private void Scan_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}