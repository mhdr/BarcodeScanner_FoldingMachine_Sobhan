using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarcodeScanner.Lib
{
    public class PostionImage
    {
        public string MachineName;
        public string ProductionName;
        public string IamgePath;

        public PostionImage(string machineName, string productionName, string image)
        {
            this.MachineName = machineName;
            this.ProductionName = productionName;
            this.IamgePath = image;
        }

        public static List<PostionImage> GetImagesForMachine1()
        {
            List<PostionImage> resultList = new List<PostionImage>();


            return resultList;
        }

        public static List<PostionImage> GetImagesForMachine2()
        {
            List<PostionImage> resultList=new List<PostionImage>();

            PostionImage image1=new PostionImage("Machine 2","MabThera 100 mg/10 ml",Path.Combine(Application.StartupPath,"Images/Machine2MabThera100.jpg"));
            PostionImage image2 = new PostionImage("Machine 2", "Xeloda 500 mg", Path.Combine(Application.StartupPath, "Images/Machine2Xeloda500.jpg"));
            PostionImage image3 = new PostionImage("Machine 2", "MabThera 500 mg/50 ml", Path.Combine(Application.StartupPath, "Images/Machine2MabThera500.jpg"));


            resultList.Add(image1);
            resultList.Add(image2);
            resultList.Add(image3);

            return resultList;
        }
    }
}
