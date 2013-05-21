using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BarcodeScanner.Lib
{
    public class BarcodeGridViewStyle : StyleSelector
    {
        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is BarcodeReader)
            {
                BarcodeReader reader = item as BarcodeReader;

                if (reader.Status == ReadStatus.Blank)
                {
                    return BlankStyle;
                }
                else if (reader.Status == ReadStatus.Mismatch)
                {
                    return MismatchStyle;
                }
                else
                {
                    return OkStyle;
                }   
            }
            else
            {
                return null;
            }

            return base.SelectStyle(item, container);
        }

        public Style OkStyle { get; set; }
        public Style BlankStyle { get; set; }
        public Style MismatchStyle { get; set; }
    }
}
