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
using SharpDX.RawInput;

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for WindowHID.xaml
    /// </summary>
    public partial class WindowHID : Window
    {
        public WindowHID()
        {
            InitializeComponent();
        }

        private List<DeviceInfo> devicesBefore = new List<DeviceInfo>();
        private List<DeviceInfo> devicesAfter = new List<DeviceInfo>();
        private int _hIDId;

        private void ButtonStep1_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonStep1.IsEnabled = false;
            TextBoxHID.Text = "";
            devicesBefore = Device.GetDevices();
        }

        private void ButtonStep2_OnClick(object sender, RoutedEventArgs e)
        {
            devicesAfter = Device.GetDevices();
            string hid = GetLastDevice().Trim();
            hid= hid.Replace(@"\H", @"\\H");
            hid = string.Format(@"\\{0}", hid);
            TextBoxHID.Text = hid;
            ButtonStep1.IsEnabled = true;
        }

        private string GetLastDevice()
        {
            foreach (DeviceInfo deviceInfo in devicesAfter)
            {
                int found = 0;
                foreach (DeviceInfo info in devicesBefore)
                {
                    if (deviceInfo.DeviceName == info.DeviceName)
                    {
                        found++;
                    }
                }

                if (found == 0)
                {
                    return deviceInfo.DeviceName;
                }
            }

            return "";
        }
    }
}
