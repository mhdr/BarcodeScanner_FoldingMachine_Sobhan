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
    /// Interaction logic for WindowHID.xaml
    /// </summary>
    public partial class WindowHID : Window
    {
        public WindowHID(int hidId)
        {
            HIDId = hidId;
            InitializeComponent();
        }

        private DatabaseEntities _entities=new DatabaseEntities();
        private int _hIDId;

        public DatabaseEntities Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public int HIDId
        {
            get { return _hIDId; }
            set { _hIDId = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxHID.Text = Entities.HIDs.FirstOrDefault(x => x.HIDId == HIDId).HID1;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            HID currentHid = Entities.HIDs.FirstOrDefault(x => x.HIDId == HIDId);
            currentHid.HID1 = @TextBoxHID.Text.Trim();
            Entities.SaveChanges();
        }

        private void WindowsHID_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TextBoxHID.Width = WindowsHID.Width - 60;
        }
    }
}
