using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BarcodeScanner.Annotations;

namespace BarcodeScanner.Lib
{
    public class BarcodeReader : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _number;
        private string _barcode;
        private ReadStatus _status;

        public int Number
        {
            get { return _number; }
            set { _number = value;
            OnPropertyChanged();
            }
        }

        public string Barcode
        {
            get { return _barcode; }
            set { _barcode = value;
            OnPropertyChanged();
            }
        }

        public ReadStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
