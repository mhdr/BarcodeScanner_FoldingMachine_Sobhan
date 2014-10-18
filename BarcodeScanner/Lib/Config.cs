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
            try
            {
                string path = Path.Combine(Application.StartupPath, "Config.json");
                StreamReader streamReader = new StreamReader(path);
                string jsonStr = streamReader.ReadToEnd();
                Config config = JsonConvert.DeserializeObject<Config>(jsonStr);

                return config;
            }
            catch (Exception)
            {
                // TODO behtare ke to log ham zakhire beshe
                Config config=new Config();
                config.InitializeWithEmpty();
                return config;
            }
        }

        public void InitializeWithEmpty()
        {
            this.BarcodeReader1HID = "";
            this.BarcodeReader2HID = "";
        }
    }
}
