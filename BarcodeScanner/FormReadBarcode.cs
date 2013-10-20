using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarcodeScanner.Lib;
using NationalInstruments.NetworkVariable;
using SharpDX.Multimedia;
using SharpDX.RawInput;
using SharpDX;

namespace BarcodeScanner
{
    public partial class FormReadBarcode : Form
    {
        public event EventHandler<Lib.BarcodeReadEventArgs> Barcode1Read;
        public event EventHandler<BarcodeReadEventArgs> Barcode2Read;

        protected virtual void OnBarcode2Read(BarcodeReadEventArgs e)
        {
            EventHandler<BarcodeReadEventArgs> handler = Barcode2Read;
            if (handler != null) handler(this, e);
        }

        private string _barcode1;
        private string _barcode2;

        public string Barcode1
        {
            get { return _barcode1; }
            set { _barcode1 = value; }
        }

        public string Barcode2
        {
            get { return _barcode2; }
            set { _barcode2 = value; }
        }

        protected virtual void OnBarcode1Read(BarcodeReadEventArgs e)
        {
            EventHandler<BarcodeReadEventArgs> handler = Barcode1Read;
            if (handler != null) handler(this, e);
        }

        public FormReadBarcode()
        {
            InitializeComponent();
        }

        public bool Device1HIDBeingSet = false;
        public bool Device2HIDBegingSet = false;
        private string deviceName1;
        private string deviceName2;
        private List<DeviceInfo> devices;
        private void FormReadBarcode_Load(object sender, EventArgs e)
        {
            devices = Device.GetDevices();
            string path = @"C:\HIDs.txt";
            StreamWriter writer = new StreamWriter(path);

            foreach (var deviceInfo in devices)
            {
                writer.WriteLine(deviceInfo.DeviceName);
            }

            writer.Flush();

            DatabaseEntities entities = new DatabaseEntities();
            deviceName1 = entities.HIDs.FirstOrDefault(x => x.HIDId == 1).HID1;
            deviceName2 = entities.HIDs.FirstOrDefault(x => x.HIDId == 2).HID1;
            Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, DeviceFlags.None);
            Device.KeyboardInput += Device_KeyboardInput;
        }

        void Device_KeyboardInput(object sender, KeyboardInputEventArgs e)
        {
            var device = devices.Find(x => x.Handle == e.Device);
   
            int length = device.DeviceName.Length;

            string deviceNameAfterReplace = device.DeviceName.Substring(0, length - 1);

            // "\\\\?\\HID#VID_0000&PID_0001#6&64975e5&0&0000#{884b96c3-56ef-11d1-bc8c-00a0c91405dd}\0"

            //if (device.DeviceName == "\\\\?\\HID#VID_0000&PID_0001#6&164bc7c4&0&0000#{884b96c3-56ef-11d1-bc8c-00a0c91405dd}\0")
            if (deviceNameAfterReplace == deviceName1)
            {
                if (e.State == KeyState.KeyUp)
                {
                    if (e.Key == Keys.Return)
                    {
                        OnBarcode1Read(new BarcodeReadEventArgs(string.Format("{0}", Barcode1)));
                        Barcode1 = "";
                    }
                    else
                    {
                        char barcode = (char)e.Key;
                        Barcode1 += barcode;
                    }
                }
            }
            //else if (device.DeviceName == "\\\\?\\HID#VID_0000&PID_0001#6&64975e5&0&0000#{884b96c3-56ef-11d1-bc8c-00a0c91405dd}\0")
            else if (deviceNameAfterReplace == deviceName2)
            {
                if (e.State == KeyState.KeyUp)
                {
                    if (e.Key == Keys.Return)
                    {
                        OnBarcode2Read(new BarcodeReadEventArgs(string.Format("{0}", Barcode2)));
                        Barcode2 = "";
                    }
                    else
                    {
                        char barcode = (char)e.Key;
                        Barcode2 += barcode;
                    }
                }
            }
        }

    }
}
