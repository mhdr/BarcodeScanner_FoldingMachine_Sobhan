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
            BarcodeScanner.Lib.PLCBool plcVariable=new PLCBool(BarcodeScanner.Lib.Statics.Machine1Motor);
            plcVariable.Value = false;
        }

        [TestMethod]
        public void Stop01()
        {
            BarcodeScanner.Lib.PLCBool plcVariable = new PLCBool(BarcodeScanner.Lib.Statics.Machine1Motor);
            plcVariable.Value=true;
        }

        [TestMethod]
        public void Start02()
        {
            BarcodeScanner.Lib.PLCBool plcVariable = new PLCBool(BarcodeScanner.Lib.Statics.Machine2Motor);
            plcVariable.Value = false;
        }

        [TestMethod]
        public void Stop02()
        {
            BarcodeScanner.Lib.PLCBool plcVariable = new PLCBool(BarcodeScanner.Lib.Statics.Machine2Motor);
            plcVariable.Value = true;

        }
    }
}
