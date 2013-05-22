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
        private ObservableCollection<BarcodeReader> _barcodeReader2Collection;

        private ListCollectionView _view;
        private ListCollectionView _view2;

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

        private bool _barcodeScanner2Running;
        private bool _barcodeScanner2TemplateRunning;

        string BarcodeScanner1Template { get; set; }
        string BarcodeScanner2Template { get; set; }

        public ListCollectionView View
        {
            get { return _view; }
            set { _view = value; }
        }

        public ObservableCollection<BarcodeReader> BarcodeReader2Collection
        {
            get { return _barcodeReader2Collection; }
            set { _barcodeReader2Collection = value; }
        }

        public ListCollectionView View2
        {
            get { return _view2; }
            set { _view2 = value; }
        }

        public bool BarcodeScanner2Running
        {
            get { return _barcodeScanner2Running; }
            set { _barcodeScanner2Running = value; }
        }

        public bool BarcodeScanner2TemplateRunning
        {
            get { return _barcodeScanner2TemplateRunning; }
            set { _barcodeScanner2TemplateRunning = value; }
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
                if (string.IsNullOrEmpty(BarcodeScanner1Template))
                {
                    ShowMsgOnStatusBar("You have to set a Template first");
                    return;
                }

                BarcodeScanner1Running = true;
                RibbonButtonStart.IsEnabled = false;
                RibbonButtonStop.IsEnabled = true;
   
                BindGridViewBarcodeReader1();
            }
        }

        private void ShowMsgOnStatusBar(string msg)
        {
            ClearStatusBar();

            StatusBarBottom.Items.Add(msg);
        }

        private void ClearStatusBar()
        {
            StatusBarBottom.Items.Clear();
        }

        private void StatusBarBottom_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClearStatusBar();
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

        private void BindGridViewBarcodeReader2()
        {
            BarcodeReader2Collection = new ObservableCollection<BarcodeReader>();
            CollectionViewSource barcodeReader2Source = (CollectionViewSource)FindResource("BarcodeReader2Source");
            barcodeReader2Source.Source = BarcodeReader2Collection;
            View2 = (ListCollectionView)barcodeReader2Source.View;
        }

        private void RibbonButtonStart2_OnClick(object sender, RoutedEventArgs e)
        {
            if (!BarcodeScanner2TemplateRunning)
            {
                if (string.IsNullOrEmpty(BarcodeScanner2Template))
                {
                    ShowMsgOnStatusBar("You have to set a Template first");
                    return;
                }

                BarcodeScanner2Running = true;
                RibbonButtonStart2.IsEnabled = false;
                RibbonButtonStop2.IsEnabled = true;

                BindGridViewBarcodeReader2();
            }
        }

        private void RibbonButtonStop2_OnClick(object sender, RoutedEventArgs e)
        {
            BarcodeScanner2Running = false;
            RibbonButtonStart2.IsEnabled = true;
            RibbonButtonStop2.IsEnabled = false;
        }

        private void RibbonButtonSetTemplate2_OnClick(object sender, RoutedEventArgs e)
        {
            if (!BarcodeScanner2Running)
            {
                BarcodeScanner2TemplateRunning = true;
                RibbonButtonSetTemplate2.IsEnabled = false;
            }
        }
    }
}
