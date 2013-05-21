using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BarcodeScanner.Lib;

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Barcode1Counter { get; set; }
        public int Barcode2Counter { get; set; }
        public ObservableCollection<BarcodeReader> BarcodeReader1Collection;
        private ListCollectionView _view;

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

        public ListCollectionView View
        {
            get { return _view; }
            set { _view = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            FormReadBarcode formReadBarcode=new FormReadBarcode();
            formReadBarcode.Barcode1Read += formReadBarcode_Barcode1Read;
            //formReadBarcode.Visible = false;
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
                //TextBlockBarcode1.Text = "";
                //TextBlockBarcode1.Text = e.Code;
                Barcode1Counter += 1;
                string barcode1 = e.Code;

                Lib.BarcodeReader reader=new BarcodeReader();
                reader.Number = Barcode1Counter;
                reader.Barcode = barcode1;
                ReadStatus readStatus = ReadStatus.Blank;

                if (barcode1 == BarcodeScanner1Template)
                {
                    readStatus=ReadStatus.OK;
                }
                
                if (barcode1 != BarcodeScanner1Template)
                {
                    readStatus=ReadStatus.Mismatch;
                }

                reader.Status = readStatus;

                BarcodeReader1Collection.Insert(0,reader);
                //BarcodeReader1Collection.Add(reader);

                //TextBlockCounter1.Text = Barcode1Counter.ToString();      
            }
        }
        private void RibbonButtonStart_OnClick(object sender, RoutedEventArgs e)
        {
            if (!BarcodeScanner1TemplateRunning)
            {
                BarcodeScanner1Running = true;
                RibbonButtonStart.IsEnabled = false;
                RibbonButtonStop.IsEnabled = true;
   
                BindGridViewBarcodeReader1();
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

        private void BindGridViewBarcodeReader1()
        {
            BarcodeReader1Collection=new ObservableCollection<BarcodeReader>();
            CollectionViewSource barcodeReader1Source = (CollectionViewSource) FindResource("BarcodeReader1Source");
            barcodeReader1Source.Source = BarcodeReader1Collection;
            View = (ListCollectionView) barcodeReader1Source.View;
        }
    }
}
