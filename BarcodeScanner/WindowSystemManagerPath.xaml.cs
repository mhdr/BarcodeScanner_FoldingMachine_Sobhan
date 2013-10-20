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
using Microsoft.Win32;

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for WindowSystemManagerPath.xaml
    /// </summary>
    public partial class WindowSystemManagerPath : Window
    {
        public WindowSystemManagerPath()
        {
            InitializeComponent();
        }

        private void ButtonBrowse_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog=new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog().Value)
            {
                string path = openFileDialog.FileName;
                Properties.Settings.Default.SystemManagerPath = path;
                Properties.Settings.Default.Save();

                TextBoxPath.Text = path;
            }
        }

        private void WindowSystemManagerPath_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBoxPath.Text = Properties.Settings.Default.SystemManagerPath;
        }
    }
}
