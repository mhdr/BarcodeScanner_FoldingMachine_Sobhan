using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
using BarcodeScanner.Lib;

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for WindowPositionImage.xaml
    /// </summary>
    public partial class WindowPositionImage : Window
    {
        public WindowPositionImage()
        {
            InitializeComponent();
        }

        private List<Lib.PostionImage> Images1;
        private List<Lib.PostionImage> Images2;

        private void ComboBoxMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxProduction.Items.Clear();
            
            if (ComboBoxMachine.SelectionBoxItem.ToString() == "Machine 1")
            {
                foreach (PostionImage image in Images1)
                {
                    ComboBoxProduction.Items.Add(image.ProductionName);
                }
            }
            else if (ComboBoxMachine.SelectionBoxItem.ToString() == "Machine 2")
            {
                foreach (PostionImage image in Images2)
                {
                    ComboBoxProduction.Items.Add(image.ProductionName);
                }
            }
        }

        private void WindowPositionImage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Images1 = Lib.PostionImage.GetImagesForMachine1();
            Images2 = Lib.PostionImage.GetImagesForMachine2();
        }

        private void ButtonShow_OnClick(object sender, RoutedEventArgs e)
        {
            string machine="";
            string production="";

            try
            {
                machine = ComboBoxMachine.SelectionBoxItem.ToString();
                production = ComboBoxProduction.SelectionBoxItem.ToString();
            }
            catch (Exception)
            {
            }

            if (machine == "Machine 1")
            {
                Lib.PostionImage selectedImage = Images1.FirstOrDefault(x => x.MachineName == machine && x.ProductionName == production);

                if (selectedImage != null) ImageViewer.Source = new BitmapImage(new Uri(selectedImage.IamgePath));
            }
            else if (machine == "Machine 2")
            {
                Lib.PostionImage selectedImage = Images2.FirstOrDefault(x => x.MachineName == machine && x.ProductionName == production);

                if (selectedImage != null) ImageViewer.Source = new BitmapImage(new Uri(selectedImage.IamgePath));
            }
        }
    }
}
