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
using System.Windows.Shapes;
using BarcodeScanner.Lib;

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for WindowService.xaml
    /// </summary>
    public partial class WindowService : Window
    {
        public WindowService()
        {
            InitializeComponent();
        }

        private void ButtonStartMachine1_OnClick(object sender, RoutedEventArgs e)
        {
            BarcodeScanner.Lib.PLCBool plcVariable = new PLCBool(BarcodeScanner.Lib.Statics.Machine1Motor);
            plcVariable.Stop();
        }

        private void ButtonStopMachine1_OnClick(object sender, RoutedEventArgs e)
        {
            BarcodeScanner.Lib.PLCBool plcVariable = new PLCBool(BarcodeScanner.Lib.Statics.Machine1Motor);
            plcVariable.Start();
        }

        private void ButtonStartMachine2_OnClick(object sender, RoutedEventArgs e)
        {
            BarcodeScanner.Lib.PLCBool plcVariable = new PLCBool(BarcodeScanner.Lib.Statics.Machine2Motor);
            plcVariable.Stop();
        }

        private void ButtonStopMachine2_OnClick(object sender, RoutedEventArgs e)
        {
            BarcodeScanner.Lib.PLCBool plcVariable = new PLCBool(BarcodeScanner.Lib.Statics.Machine2Motor);
            plcVariable.Start();
        }
    }
}
