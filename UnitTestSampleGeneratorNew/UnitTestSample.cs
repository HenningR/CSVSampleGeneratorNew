using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSVSampleGenerator.Classes;
namespace UnitTestSampleGenerator
{
    [TestClass]
    public class UnitTestSample
    {
        [TestMethod]
        public void NegativeTestImport()
        {
            //Path Incorrect 
            string rootPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = rootPath + @"Data\Data.csv";
            SampleCollector sampleData = new SampleCollector(filePath);

            bool bResult = true;
            Assert.AreEqual(bResult, sampleData.Import());
        }

        [TestMethod]
        public void PositiveTestImport()
        {
            string rootPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = rootPath + @"\Data\Data.csv";
            SampleCollector sampleData = new SampleCollector(filePath);

            bool bResult = true;
            Assert.AreEqual(bResult, sampleData.Import());
        }

        [TestMethod]
        public void PositiveTestAddressExport()
        {
            string rootPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = rootPath + @"\Data\Data.csv";
            SampleCollector sampleData = new SampleCollector(filePath);
            bool bResult = true;

            ISampleImportExport sample = new AddressSample();

            Assert.AreEqual(bResult, sampleData.Import());
            Assert.AreEqual(bResult, sampleData.Export(sample));
        }


        [TestMethod]
        public void NegativeTestAddressExport()
        {
            string rootPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = rootPath + @"\Data\Data.csv";
            SampleCollector sampleData = new SampleCollector(filePath);
            bool bResult = true;

            //Forget to Import
            //Assert.AreEqual(bResult, sampleData.Import());

            ISampleImportExport sample = new AddressSample();

            Assert.AreEqual(bResult, sampleData.Export(sample));
        }

        [TestMethod]
        public void PositiveTestFullNameExport()
        {
            string rootPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = rootPath + @"\Data\Data.csv";
            SampleCollector sampleData = new SampleCollector(filePath);
            bool bResult = true;

            Assert.AreEqual(bResult, sampleData.Import());

            ISampleImportExport sample = new FullNameSample();
            Assert.AreEqual(bResult, sampleData.Export(sample));
        }



        [TestMethod]
        public void PositiveTestFullNameOpen()
        {
            string rootPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = rootPath + @"\Data\Data.csv";
            SampleCollector sampleData = new SampleCollector(filePath);
            bool bResult = true;

            Assert.AreEqual(bResult, sampleData.Import());
            ISampleImportExport sample = new FullNameSample();
            Assert.AreEqual(bResult, sampleData.Export(sample));


            try
            {
                sampleData.OpenFile(sample);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }


        }


        [TestMethod]
        public void NegativeTestAddressOpen()
        {
            
            string rootPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = rootPath + @"\Data\data.csv";
            SampleCollector sampleData = new SampleCollector(filePath);
            bool bResult = true;

            
            sampleData.Import();
            ISampleImportExport sample = new AddressSample();
            sampleData.Export(sample);

            try
            {
                filePath = rootPath + @"\Data\FullName.txt";
                System.IO.File.Delete(filePath);
                //File not found
                sampleData.OpenFile(sample);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }


        }

    }
}
