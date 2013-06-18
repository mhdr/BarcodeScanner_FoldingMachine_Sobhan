using System;
using BarcodeScanner.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class PLCVariableTests
    {
        [TestMethod]
        public void Start01()
        {
            BarcodeScanner.Lib.PLCVariable plcVariable=new PLCVariable(BarcodeScanner.Lib.Statics.Machine1Motor);
            plcVariable.Start();
        }

        [TestMethod]
        public void Stop01()
        {
            BarcodeScanner.Lib.PLCVariable plcVariable = new PLCVariable(BarcodeScanner.Lib.Statics.Machine1Motor);
            plcVariable.Stop();
        }

        [TestMethod]
        public void Start02()
        {
            BarcodeScanner.Lib.PLCVariable plcVariable = new PLCVariable(BarcodeScanner.Lib.Statics.Machine2Motor);
            plcVariable.Start();
        }

        [TestMethod]
        public void Stop02()
        {
            BarcodeScanner.Lib.PLCVariable plcVariable = new PLCVariable(BarcodeScanner.Lib.Statics.Machine2Motor);
            plcVariable.Stop();
        }
    }
}
