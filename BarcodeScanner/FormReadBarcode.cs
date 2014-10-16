using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarcodeScanner.Lib;
using SharpDX.Multimedia;
using SharpDX.RawInput;
using SharpDX;

namespace BarcodeScanner
{
    public partial class FormReadBarcode : Form
    {
        public event EventHandler<Lib.BarcodeReadEventArgs> Barcode1Read;
        public event EventHandler<BarcodeReadEventArgs> Barcode2Read;

        private string HID1;
        private string HID2;

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

        private void FormReadBarcode_Load(object sender, EventArgs e)
        {
            Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, DeviceFlags.None);
            Device.KeyboardInput += Device_KeyboardInput;

            Config config = Config.LoadConfig();
            HID1 = string.Format("{0}\0", config.BarcodeReader1HID);
            HID2 = string.Format("{0}\0", config.BarcodeReader2HID);
        }

        void Device_KeyboardInput(object sender, KeyboardInputEventArgs e)
        {
            var devices = Device.GetDevices();

            var device = devices.Find(x => x.Handle == e.Device);

            // Correct HID Format
            // "\\\\?\\HID#VID_0000&PID_0001#6&1a1f2f6&0&0000#{884b96c3-56ef-11d1-bc8c-00a0c91405dd}\0

            if (device.DeviceName ==HID1)
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
            else if (device.DeviceName ==HID2)
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
