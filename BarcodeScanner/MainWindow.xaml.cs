using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using BarcodeScanner.Lib;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Application = System.Windows.Forms.Application;
using Path = System.IO.Path;

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Barcode1Counter { get; set; }
        public int Barcode2Counter { get; set; }
        private ObservableCollection<BarcodeReader> _barcodeReader1Collection;
        private ObservableCollection<BarcodeReader> _barcodeReader2Collection;
        public int CorrectBarcodeScanCounter1 { get; set; }
        public int CorrectBarcodeScanCounter2 { get; set; }

        private ListCollectionView _view;
        private ListCollectionView _view2;

        private DispatcherTimer Timer11 = new DispatcherTimer(DispatcherPriority.Send);
        private DispatcherTimer Timer22 = new DispatcherTimer(DispatcherPriority.Send);

        private int LastRealCounter1=0;
        private string LastBarcode1 = "";

        // Config
        public int DelayAfterIncreasingCounter1;
        public int CheckCounter1Timer;
        public int CheckCounter2Timer;
        public string SystemManagerPath;

        //

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

        public int Machine1Counter { get; set; }
        public int Machine2Counter { get; set; }

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

        public ObservableCollection<BarcodeReader> BarcodeReader1Collection
        {
            get { return _barcodeReader1Collection; }
            set { _barcodeReader1Collection = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private FormReadBarcode formReadBarcode = new FormReadBarcode();

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadConfig();

            StartNIDistributedSystemManager();

            // Reset Counters
            PLCBool counter1Reset = new PLCBool(Statics.Counter1Reset);
            counter1Reset.Value = true;
            PLCBool counter2Reset = new PLCBool(Statics.Counter2Reset);
            counter2Reset.Value = true;

            counter1Reset.Value = false;
            counter2Reset.Value = false;
            //

            formReadBarcode.Barcode1Read += formReadBarcode_Barcode1Read;
            formReadBarcode.Barcode2Read += formReadBarcode_Barcode2Read;
            //formReadBarcode.Visible = false;
            formReadBarcode.Show();
            formReadBarcode.Hide();

            BindGridViewBarcodeReader1();
            BindGridViewBarcodeReader2();

            Timer11.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            Timer22.Interval = new TimeSpan(0, 0, 0, 0, 2);
            Timer11.IsEnabled = true;
            Timer22.IsEnabled = true;
            Timer11.Tick += Timer11_Tick;
            Timer22.Tick += Timer22_Tick;

        }

        void Timer22_Tick(object sender, EventArgs e)
        {
            if (!BarcodeScanner2Running)
            {
                return;
            }

            Lib.BarcodeReader reader = new BarcodeReader();
            reader.Number = Barcode2Counter;
            reader.Barcode = "";
            ReadStatus readStatus = ReadStatus.Blank;

            PLCInt plcInt1 = new PLCInt(Statics.Counter2DB);
            int realCounter = plcInt1.Value;

            if (Math.Abs(realCounter - CorrectBarcodeScanCounter2) > 1)
            {
                readStatus = ReadStatus.Blank;
                reader.Status = readStatus;
                reader.Barcode = "";
                reader.Date = DateTime.Now;
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    StopMachine2Motor();
                    StopBarcodeReader2();

                    BarcodeReader2Collection.Insert(0, reader);
                }));
            }
        }

        void Timer11_Tick(object sender, EventArgs e)
        {
            if (!BarcodeScanner1Running)
            {
                return;
            }

            PLCInt plcInt1 = new PLCInt(Statics.Counter1DB);
            int realCounter = plcInt1.Value;

            if (realCounter > LastRealCounter1)
            {
                Thread thread=new Thread(()=>InspectBarcode1(realCounter));
                thread.Start();

                // prepare for next cycle
                LastRealCounter1 = realCounter;
            }
        }

        private void InspectBarcode1(int realCounter)
        {
            object objLock = realCounter;

            lock (objLock)
            {
                Thread.Sleep(this.DelayAfterIncreasingCounter1);

                string barcode1 = LastBarcode1;

                Lib.BarcodeReader reader = new BarcodeReader();
                ReadStatus readStatus = ReadStatus.Blank;

                if (barcode1 != "")
                {
                    Barcode1Counter += 1;
                    CorrectBarcodeScanCounter1++;

                    
                    reader.Number = Barcode1Counter;
                    reader.Barcode = barcode1;

                    if (barcode1 == BarcodeScanner1Template)
                    {
                        readStatus = ReadStatus.OK;
                    }

                    if (barcode1 != BarcodeScanner1Template)
                    {
                        readStatus = ReadStatus.Mismatch;
                        reader.Number = Barcode1Counter - 1;

                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            StopMachine1Motor();
                            StopBarcodeReader1();                            
                        }));
                    }
                    reader.Status = readStatus;
                    reader.Date = DateTime.Now;

                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        BarcodeReader1Collection.Insert(0, reader);
                    }));
                }
                else
                {
                    reader = new BarcodeReader();
                    reader.Number = Barcode1Counter;
                    reader.Barcode = "";
                    readStatus = ReadStatus.Blank;

                    if (Math.Abs(realCounter - CorrectBarcodeScanCounter1) > 0)
                    {
                        readStatus = ReadStatus.Blank;
                        reader.Status = readStatus;
                        reader.Barcode = "";
                        reader.Date = DateTime.Now;
                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            BarcodeReader1Collection.Insert(0, reader);
                            StopMachine1Motor();
                            StopBarcodeReader1();
                        }));
                    }    
                }

                // prepare for next cycle
                LastBarcode1 = "";
            }
        }

        void formReadBarcode_Barcode2Read(object sender, BarcodeReadEventArgs e)
        {
            if (BarcodeScanner2TemplateRunning)
            {
                BarcodeScanner2Template = e.Code;
                TextBlockTemplate2.Text = BarcodeScanner2Template;
                RibbonButtonSetTemplate2.IsEnabled = true;
                BarcodeScanner2TemplateRunning = false;
                RibbonButtonCancleTemplate2.IsEnabled = false;
            }

            if (BarcodeScanner2Running)
            {
                Barcode2Counter += 1;
                CorrectBarcodeScanCounter2++;

                string barcode2 = e.Code;

                Lib.BarcodeReader reader = new BarcodeReader();
                reader.Number = Barcode2Counter;
                reader.Barcode = barcode2;
                ReadStatus readStatus = ReadStatus.Blank;

                if (barcode2 == BarcodeScanner2Template)
                {
                    readStatus = ReadStatus.OK;
                }

                if (barcode2 != BarcodeScanner2Template)
                {
                    readStatus = ReadStatus.Mismatch;
                    reader.Number = Barcode2Counter - 1;


                    StopMachine2Motor();
                    StopBarcodeReader2();
                }
                reader.Status = readStatus;
                reader.Date = DateTime.Now;

                BarcodeReader2Collection.Insert(0, reader);

            }
        }


        void formReadBarcode_Barcode1Read(object sender, Lib.BarcodeReadEventArgs e)
        {
            if (BarcodeScanner1TemplateRunning)
            {
                BarcodeScanner1Template = e.Code;
                TextBlockTemplate1.Text = BarcodeScanner1Template;
                RibbonButtonSetTemplate.IsEnabled = true;
                BarcodeScanner1TemplateRunning = false;
                RibbonButtonCancleTemplate.IsEnabled = false;
            }

            if (BarcodeScanner1Running)
            {
                LastBarcode1 = e.Code;
            }
        }
        private void RibbonButtonStart_OnClick(object sender, RoutedEventArgs e)
        {
            StartBarcodeReader1();
            ManageTopMost();
        }

        public void StartMachine1Motor()
        {
            PLCBool plcVariable = new PLCBool(Lib.Statics.Machine1Motor);
            plcVariable.Value = false;
        }

        public void StartMachine2Motor()
        {
            PLCBool plcVariable = new PLCBool(Lib.Statics.Machine2Motor);
            plcVariable.Value = false;
        }

        public void StopMachine1Motor()
        {
            PLCBool plcVariable = new PLCBool(Lib.Statics.Machine1Motor);
            plcVariable.Value = true;
        }

        public void StopMachine2Motor()
        {
            PLCBool plcVariable = new PLCBool(Lib.Statics.Machine2Motor);
            plcVariable.Value = true;
        }

        private void StartBarcodeReader1()
        {
            if (!BarcodeScanner1TemplateRunning)
            {
                if (string.IsNullOrEmpty(BarcodeScanner1Template))
                {
                    //ShowMsgOnStatusBar("You have to set a Template first");
                    return;
                }

                BarcodeScanner1Running = true;
                RibbonButtonStart.IsEnabled = false;
                RibbonButtonStop.IsEnabled = true;

                PLCInt plcInt1 = new PLCInt(Statics.Counter1DB);
                CorrectBarcodeScanCounter1 = plcInt1.Value;
                LastRealCounter1 = CorrectBarcodeScanCounter1;

                //Timer1.Start();
                Timer11.IsEnabled = true;

                StartMachine1Motor();
            }
        }

        private void StartBarcodeReader2()
        {
            if (!BarcodeScanner2TemplateRunning)
            {
                if (string.IsNullOrEmpty(BarcodeScanner2Template))
                {
                    //ShowMsgOnStatusBar("You have to set a Template first");
                    return;
                }

                BarcodeScanner2Running = true;
                RibbonButtonStart2.IsEnabled = false;
                RibbonButtonStop2.IsEnabled = true;

                PLCInt plcInt1 = new PLCInt(Statics.Counter2DB);
                CorrectBarcodeScanCounter2 = plcInt1.Value;

                //Timer2.Start();
                Timer22.IsEnabled = true;

                StartMachine2Motor();
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
            StopBarcodeReader1();
            ManageTopMost();
        }

        private void StopBarcodeReader1()
        {
            BarcodeScanner1Running = false;
            RibbonButtonStart.IsEnabled = true;
            RibbonButtonStop.IsEnabled = false;
            Timer11.IsEnabled = false;
        }

        private void StopBarcodeReader2()
        {
            BarcodeScanner2Running = false;
            RibbonButtonStart2.IsEnabled = true;
            RibbonButtonStop2.IsEnabled = false;
            Timer22.IsEnabled = false;
        }

        private void RibbonButtonSetTemplate_OnClick(object sender, RoutedEventArgs e)
        {
            if (!BarcodeScanner1Running)
            {
                BarcodeScanner1TemplateRunning = true;
                RibbonButtonSetTemplate.IsEnabled = false;
                RibbonButtonCancleTemplate.IsEnabled = true;
            }

            ManageTopMost();
        }

        private void BindGridViewBarcodeReader1()
        {
            BarcodeReader1Collection = new ObservableCollection<BarcodeReader>();
            CollectionViewSource barcodeReader1Source = (CollectionViewSource)FindResource("BarcodeReader1Source");
            barcodeReader1Source.Source = BarcodeReader1Collection;
            View = (ListCollectionView)barcodeReader1Source.View;
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
            StartBarcodeReader2();
            ManageTopMost();
        }

        private void RibbonButtonStop2_OnClick(object sender, RoutedEventArgs e)
        {
            StopBarcodeReader2();
            ManageTopMost();
        }

        private void RibbonButtonSetTemplate2_OnClick(object sender, RoutedEventArgs e)
        {
            if (!BarcodeScanner2Running)
            {
                BarcodeScanner2TemplateRunning = true;
                RibbonButtonSetTemplate2.IsEnabled = false;
                RibbonButtonCancleTemplate2.IsEnabled = true;
            }

            ManageTopMost();
        }

        private void RibbonButtonHID_OnClick(object sender, RoutedEventArgs e)
        {
            WindowHID windowHid=new WindowHID();
            windowHid.Show();
        }

        private void RibbonButtonSettings_OnClick(object sender, RoutedEventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "Config.json");
            Process process= Process.Start("notepad", path);
            process.WaitForExit();
            LoadConfig();
        }

        private void LoadConfig()
        {
            Config config = Config.LoadConfig();
            formReadBarcode.HID1 = string.Format("{0}\0", config.BarcodeReader1HID);
            formReadBarcode.HID2 = string.Format("{0}\0", config.BarcodeReader2HID);
            this.DelayAfterIncreasingCounter1 = config.DelayAfterIncreasingCounter1;
            this.CheckCounter1Timer = config.CheckCounter1Timer;
            this.CheckCounter2Timer = config.CheckCounter2Timer;
            this.SystemManagerPath = config.SystemManagerPath;

            this.StartNIDistributedSystemManager();
        }

        public void StartNIDistributedSystemManager()
        {
            // Start NI Distributed System Manager
            if (File.Exists(this.SystemManagerPath))
            {
                var processes =
                    Process.GetProcessesByName(
                        Path.GetFileNameWithoutExtension(this.SystemManagerPath));

                if (!processes.Any(x => x.MainModule.FileName == this.SystemManagerPath))
                {
                    Process.Start(this.SystemManagerPath);
                }

            }

            //
        }

        private void RibbonButtonAbout_OnClick(object sender, RoutedEventArgs e)
        {
            WindowAbout windowAbout=new WindowAbout();
            windowAbout.ShowDialog();
        }

        private void RibbonButtonUserManual_OnClick(object sender, RoutedEventArgs e)
        {
            WindowDocumentation windowDocumentation = new WindowDocumentation();
            windowDocumentation.Show();
        }

        private void ManageTopMost()
        {
            if (BarcodeScanner1Running || BarcodeScanner2Running || BarcodeScanner1TemplateRunning ||
                BarcodeScanner2TemplateRunning)
            {
                this.Topmost = true;
                RibbonTabTools.IsEnabled = false;
                RibbonTabHelp.IsEnabled = false;
            }
            else
            {
                this.Topmost = false;
                RibbonTabTools.IsEnabled = true;
                RibbonTabHelp.IsEnabled = true;
            }
        }

        private void RibbonButtonCancleTemplate_OnClick(object sender, RoutedEventArgs e)
        {
            RibbonButtonSetTemplate.IsEnabled = true;
            BarcodeScanner1TemplateRunning = false;
            RibbonButtonCancleTemplate.IsEnabled = false;
            ManageTopMost();
        }

        private void RibbonButtonCancleTemplate2_OnClick(object sender, RoutedEventArgs e)
        {
            RibbonButtonSetTemplate2.IsEnabled = true;
            BarcodeScanner2TemplateRunning = false;
            RibbonButtonCancleTemplate2.IsEnabled = false;
            ManageTopMost();
        }

        private void RibbonButtonExit_OnClick(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
