using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSampleGenerator.Classes
{
    public interface ISampleImportExport
    {
        bool export(List<SampleData> sampleList);
        bool openFile();
    }

    public abstract class ImportExport : FileTransformer
    {

        public string Write(string format, params object[] arg)
        {
            StringBuilder build = new StringBuilder();
            build.AppendFormat(format, arg);
            return build.ToString();
        }

        public abstract bool export(List<SampleData> sampleList);

        public bool openFile(string fileName)
        {
            bool bResult = false;
            try
            {
                string rootPath = System.AppDomain.CurrentDomain.BaseDirectory;
                rootPath = rootPath.Substring(rootPath.Length -1,1) == "\\" ? rootPath + @"Data\" : rootPath + @"\Data\" ;
                System.Diagnostics.Process.Start(rootPath + fileName);
                bResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bResult;
        }
    }


    public class FullNameSample : ImportExport, ISampleImportExport
    {
        private string fileName = "NameList.txt";
        public override bool export(List<SampleData> sampleList)
        {
            bool bResult = false;
            StringBuilder combindString = new StringBuilder();
            var selectList = sampleList.OrderBy(f => f.FirstName).ThenBy(t => t.LastName).Select(m => new { FirstName = m.FirstName, LastName = m.LastName }).ToArray();
            foreach (var row in selectList)
            {
                combindString.Append(row.FirstName + " " + row.LastName + "\r\n");
            }
            bResult = ExportFile(combindString.ToString(), "Data//" + fileName);
            return bResult;
        }

      
        public bool openFile()
        {
            return base.openFile(fileName);
        }

       
        public override string ToString()
        {
            return Write("class FullNameSample {0}{1}", "FileName:",fileName);
        }
    }

    public class AddressSample : ImportExport, ISampleImportExport
    {
        private string fileName = "AddressList.txt";
        public override bool export(List<SampleData> sampleList)
        {
            bool bResult = false;
            StringBuilder combindString = new StringBuilder();
            var selectAddressList = sampleList.OrderBy(f => f.Address).Select(m => new { Address = m.Address }).ToArray();
            foreach (var row in selectAddressList)
            {
                combindString.Append(row.Address + "\r\n");
            }
            bResult = ExportFile(combindString.ToString(), "Data//" + fileName);

            return bResult;
        }


        public bool openFile()
        {
            return base.openFile(fileName);
        }



        public override string ToString()
        {
            return Write("class AddressSample {0}{1}", "FileName:", fileName);
        }

    }
}
