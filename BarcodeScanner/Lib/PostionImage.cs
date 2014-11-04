using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeScanner.Lib
{
    public class PostionImage
    {
        public string MachineName;
        public string ProductionName;
        public Bitmap Image;

        public PostionImage(string machineName, string productionName, Bitmap image)
        {
            this.MachineName = machineName;
            this.ProductionName = productionName;
            this.Image = image;
        }

        public static List<PostionImage> GetImagesForMachine1()
        {
            List<PostionImage> resultList = new List<PostionImage>();


            return resultList;
        }

        public static List<PostionImage> GetImagesForMachine2()
        {
            List<PostionImage> resultList=new List<PostionImage>();

            PostionImage image1=new PostionImage("Machine 2","MabThera 100 mg/10 ml",Properties.Resources.Machine2MabThera100);
            PostionImage image2 = new PostionImage("Machine 2", "Xeloda 500 mg", Properties.Resources.Machine2Xeloda500);
            PostionImage image3 = new PostionImage("Machine 2", "MabThera 500 mg/50 ml", Properties.Resources.Machine2MabThera500);


            resultList.Add(image1);
            resultList.Add(image2);
            resultList.Add(image3);

            return resultList;
        }
    }
}
