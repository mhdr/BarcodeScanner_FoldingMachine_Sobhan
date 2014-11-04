using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Telerik.Windows.Documents.Fixed;
using Telerik.Windows.Documents.Fixed.UI;

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for WindowScannerManual.xaml
    /// </summary>
    public partial class WindowScannerManual : Window
    {
        public WindowScannerManual()
        {
            InitializeComponent();
        }

        private void WindowScannerManual_OnLoaded(object sender, RoutedEventArgs e)
        {
            Stream stream = new MemoryStream(Properties.Resources.ScannerManual);
            PdfViewerScannerManual.FitToWidth();
            PdfViewerScannerManual.DocumentSource = new PdfDocumentSource(stream);
        }
    }
}
