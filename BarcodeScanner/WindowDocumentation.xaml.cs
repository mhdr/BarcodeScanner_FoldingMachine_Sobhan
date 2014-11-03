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

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for WindowDocumentation.xaml
    /// </summary>
    public partial class WindowDocumentation : Window
    {
        public WindowDocumentation()
        {
            InitializeComponent();
            
        }

        private void WindowDocumentation_OnLoaded(object sender, RoutedEventArgs e)
        {
            Stream stream=new MemoryStream(Properties.Resources.UserManual);
            PdfViewerUserManual.DocumentSource=new PdfDocumentSource(stream);
        }
    }
}
