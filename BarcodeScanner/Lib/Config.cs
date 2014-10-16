using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace BarcodeScanner.Lib
{
    public class Config
    {
        public string BarcodeReader1HID;
        public string BarcodeReader2HID;

        public static Config LoadConfig()
        {
            string path = Path.Combine(Application.StartupPath, "Config.json");
            StreamReader streamReader = new StreamReader(path);
            string jsonStr = streamReader.ReadToEnd();
            Config config = JsonConvert.DeserializeObject<Config>(jsonStr);

            return config;
        }
    }
}
