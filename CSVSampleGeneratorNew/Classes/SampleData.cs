using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSampleGenerator.Classes
{
    public class SampleData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class SampleCollector
    {
        public List<SampleData> sampleList { get; set; }
        private string _importFile;

        public SampleCollector(string csvFile)
        {
            _importFile = csvFile;
        }

        /// <summary>
        /// Import CSV File into memory
        /// </summary>
        /// <returns></returns>
        public bool Import()
        {
            FileTransformer transFormer = new FileTransformer();
            bool bResult = false;
            try
            {
                if (transFormer.ReadCSV(_importFile))
                {
                    sampleList = new List<SampleData>();
                    SampleData data = new SampleData();
                    foreach (List<string> dicList in transFormer.CSVOutput.Values)
                    {
                        data = new SampleData();
                        if (dicList.Count >= 3)
                        {
                            data.FirstName = dicList[0];
                            data.LastName = dicList[1];
                            data.Address = dicList[2];
                            data.PhoneNumber = dicList[3];
                        }
                        sampleList.Add(data);
                    }
                    bResult = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return bResult;
        }

        /// <summary>
        /// Export memory dump to specfic file export
        /// </summary>
        /// <param name="exportType"></param>
        /// <returns></returns>
        public bool Export(ISampleImportExport sampleExport)
        {
            bool bResult = false;
            try
            {
                bResult = sampleExport.export(sampleList);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bResult;
        }

        /// <summary>
        /// Open the exported file
        /// </summary>
        /// <param name="exportType"></param>
        public void OpenFile(ISampleImportExport sampleExport)
        {
            try
            {
                sampleExport.openFile();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Print(ISampleImportExport sampleExport)
        {
            System.Diagnostics.Debug.WriteLine(sampleExport);
        }

    }
}
