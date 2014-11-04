using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.NetworkVariable;
using Newtonsoft.Json;

namespace BarcodeScanner.Lib
{
    public class Log
    {
        public DateTime Date;
        public string Message;

        public Log(DateTime date, string message)
        {
            this.Date = date;
            this.Message = message;
        }

        public static void Write(Log log)
        {
            string path = Path.Combine(Application.StartupPath, "Log.json");
            StreamWriter streamWriter = new StreamWriter(path, true, Encoding.UTF8);
            string jsonStr = JsonConvert.SerializeObject(log);
            streamWriter.WriteLine(jsonStr);
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}
