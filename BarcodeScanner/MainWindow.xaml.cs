using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Barcode1Counter { get; set; }
        public int Barcode2Counter { get; set; }

        public bool BarcodeScanner1Running
        {
            get { return _barcodeScanner1Running; }
            set { _barcodeScanner1Running = value; }
        }

        public bool BarcodeScanner1TemplateRunning
        {
            get { return _barcodeScanner1TemplateRunning; }
            set { _barcodeScanner1TemplateRunning = value; }
        }

        private bool _barcodeScanner1Running;
        private bool _barcodeScanner1TemplateRunning;

        string BarcodeScanner1Template { get; set; }
        string BarcodeScanner2Template { get; set; }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            FormReadBarcode formReadBarcode=new FormReadBarcode();
            formReadBarcode.Barcode1Read += formReadBarcode_Barcode1Read;
            formReadBarcode.Show();
            formReadBarcode.Hide();
        }

        void formReadBarcode_Barcode1Read(object sender, Lib.BarcodeReadEventArgs e)
        {
            if (BarcodeScanner1TemplateRunning)
            {
                BarcodeScanner1Template = e.Code;
                TextBlockTemplate1.Text = BarcodeScanner1Template;
                RibbonButtonSetTemplate.IsEnabled = true;
                BarcodeScanner1TemplateRunning = false;
            }
            
            if (BarcodeScanner1Running)
            {
                TextBlockBarcode1.Text = "";
                TextBlockBarcode1.Text = e.Code;
                Barcode1Counter += 1;
                TextBlockCounter1.Text = Barcode1Counter.ToString();      
            }
        }
        private void RibbonButtonStart_OnClick(object sender, RoutedEventArgs e)
        {
            if (!BarcodeScanner1TemplateRunning)
            {
                BarcodeScanner1Running = true;
                RibbonButtonStart.IsEnabled = false;
                RibbonButtonStop.IsEnabled = true;   
            }
        }

        private void RibbonButtonStop_OnClick(object sender, RoutedEventArgs e)
        {
            BarcodeScanner1Running = false;
            RibbonButtonStart.IsEnabled = true;
            RibbonButtonStop.IsEnabled = false;
        }

        private void RibbonButtonSetTemplate_OnClick(object sender, RoutedEventArgs e)
        {
            if (!BarcodeScanner1Running)
            {
                BarcodeScanner1TemplateRunning = true;
                RibbonButtonSetTemplate.IsEnabled = false;
            }
        }
    }
}
