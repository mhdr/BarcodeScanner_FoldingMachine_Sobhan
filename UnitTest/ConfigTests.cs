using System;
using BarcodeScanner.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class ConfigTests
    {
        [TestMethod]
        public void InitializeConfig()
        {
            Config config=new Config();
        }
    }
}
