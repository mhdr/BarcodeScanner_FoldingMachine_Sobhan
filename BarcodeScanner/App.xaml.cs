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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
