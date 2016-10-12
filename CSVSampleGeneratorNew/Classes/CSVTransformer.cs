using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSampleGenerator.Classes
{
    public interface ITransformer
    {
        bool ReadCSV(string fileInput);
        bool ExportFile(string sampleData, string fileName);
    }

    public class FileTransformer : ITransformer
    {
        public Dictionary<int, List<string>> CSVOutput { get; set; }

        public bool ReadCSV(string fileInput)
        {
            CSVOutput = new Dictionary<int, List<string>>();
            bool bResult = false;
            try
            {
                string[] csvInputLines = File.ReadAllLines(fileInput);
                string[] rowInput;
                int rowCC = 0;
                List<string> rowList;
                foreach (string csvRow in csvInputLines)
                {
                    if (rowCC > 0)
                    {
                        rowInput = csvRow.Split(new char[] { ',' }, StringSplitOptions.None);
                        rowList = new List<string>();
                        for (int i = 0; i < rowInput.Length; i++)
                        {
                            rowList.Add( rowInput[i]);
                        }
                        CSVOutput.Add(rowCC, rowList);
                    }
                    rowCC++;
                }
                bResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bResult;
        }


        public bool ExportFile(string sampleData, string fileName)
        {
            bool bResult = false;
            try
            {
                File.WriteAllText(fileName, sampleData);
                bResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bResult;
        }
    }
}
