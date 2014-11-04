using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using BarcodeScanner.Lib;

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Log log=new Log(DateTime.Now,e.Exception.ToString());
            Log.Write(log);

            try
            {
                PLCBool plcVariable1 = new PLCBool(Statics.Machine1Motor);
                plcVariable1.Stop();

                PLCBool plcVariable2 = new PLCBool(Statics.Machine2Motor);
                plcVariable2.Stop();
            }
            catch (Exception)
            {

            }

            e.Handled = true;
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            PLCBool plcVariable1 = new PLCBool(Statics.Machine1Motor);
            plcVariable1.Stop();

            PLCBool plcVariable2 = new PLCBool(Statics.Machine2Motor);
            plcVariable2.Stop();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log log = new Log(DateTime.Now, e.ExceptionObject.ToString());
            Log.Write(log);

            try
            {
                PLCBool plcVariable1 = new PLCBool(Statics.Machine1Motor);
                plcVariable1.Stop();

                PLCBool plcVariable2 = new PLCBool(Statics.Machine2Motor);
                plcVariable2.Stop();
            }
            catch (Exception)
            {

            }
        }
    }
}
