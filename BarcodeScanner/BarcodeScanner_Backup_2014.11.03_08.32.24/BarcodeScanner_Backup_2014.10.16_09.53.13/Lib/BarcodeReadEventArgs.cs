using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeScanner.Lib
{
    public class BarcodeReadEventArgs:EventArgs
    {
        public string Code { get; set; }

        public BarcodeReadEventArgs()
        {
            
        }

        public BarcodeReadEventArgs(string code)
        {
            this.Code = code;
        }
    }
}
