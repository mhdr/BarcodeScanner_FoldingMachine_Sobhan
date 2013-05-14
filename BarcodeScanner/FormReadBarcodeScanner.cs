using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.Multimedia;
using SharpDX.RawInput;

namespace BarcodeScanner
{
    public partial class FormReadBarcodeScanner : Form
    {
        public FormReadBarcodeScanner()
        {
            InitializeComponent();
        }

        private void FormReadBarcodeScanner_Load(object sender, EventArgs e)
        {
            Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, DeviceFlags.None);
            Device.KeyboardInput += Device_KeyboardInput;
        }

        void Device_KeyboardInput(object sender, KeyboardInputEventArgs e)
        {
            
        }
    }
}
