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

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for WindowHIDs.xaml
    /// </summary>
    public partial class WindowHIDs : Window
    {
        public WindowHIDs()
        {
            InitializeComponent();
        }

        private void WindowHIDs_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBoxHID1.Text = Properties.Settings.Default.HID1;
            TextBoxHID2.Text = Properties.Settings.Default.HID2;
        }

        private void WindowHIDs_OnClosed(object sender, EventArgs e)
        {
            Properties.Settings.Default.HID1 = TextBoxHID1.Text;
            Properties.Settings.Default.HID2 = TextBoxHID2.Text;
            Properties.Settings.Default.Save();
        }
    }
}
