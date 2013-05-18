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
        private string _barcode1;

        public string Barcode1
        {
            get { return _barcode1; }
            set { _barcode1 = value; }
        }

        protected virtual void OnBarcode1Read(BarcodeReadEventArgs e)
        {
            EventHandler<BarcodeReadEventArgs> handler = Barcode1Read;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<Lib.BarcodeReadEventArgs> Barcode2Read;

        protected virtual void OnBarcode2Read(BarcodeReadEventArgs e)
        {
            EventHandler<BarcodeReadEventArgs> handler = Barcode2Read;
            if (handler != null) handler(this, e);
        }

        public FormReadBarcode()
        {
            InitializeComponent();
        }

        private void FormReadBarcode_Load(object sender, EventArgs e)
        {
            //var devices = Device.GetDevices();
            Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, DeviceFlags.None);
            Device.KeyboardInput += Device_KeyboardInput;
        }

        void Device_KeyboardInput(object sender, KeyboardInputEventArgs e)
        {
            if (e.State == KeyState.KeyUp)
            {
                if (e.Key == Keys.Return)
                {
                    OnBarcode1Read(new BarcodeReadEventArgs(string.Format("{0}",Barcode1)));
                    Barcode1 = "";
                }
                else
                {
                    char barcode = (char)e.Key;
                    Barcode1 += barcode;
                }    
            }
            
        }
    }
}
