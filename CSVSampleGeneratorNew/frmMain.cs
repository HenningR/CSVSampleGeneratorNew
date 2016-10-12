using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSVSampleGenerator.Classes;

namespace CSVSampleGenerator
{
    public partial class frmCSVGenerator : Form
    {
        SampleCollector sampleCol;

        public frmCSVGenerator()
        {
            InitializeComponent();
        }

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            try
            {
                string rootPath = System.AppDomain.CurrentDomain.BaseDirectory; 

                dlgOpenFile.InitialDirectory = rootPath  + @"Data\";
                dlgOpenFile.Filter = "(CSV Files)|*.csv";
                dlgOpenFile.ShowDialog(this);
                txtFilePath.Text = dlgOpenFile.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(txtFilePath.Text))
            {
                sampleCol = new SampleCollector(txtFilePath.Text);

                if (sampleCol.Import() == false)
                {
                    MessageBox.Show("Import failed");
                }
                else
                {
                    MessageBox.Show("Import succeed");
                }
            }
            else
            {
                MessageBox.Show("Select a valid file");
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (sampleCol != null && sampleCol.sampleList != null)
            {
                ISampleImportExport fullNameExport = new FullNameSample();
                sampleCol.Print(fullNameExport);
                ISampleImportExport addressExport = new AddressSample();
                sampleCol.Print(addressExport);

                if (sampleCol.Export(fullNameExport))
                {
                    if (sampleCol.Export(addressExport) == false)
                    {
                        MessageBox.Show("Export failed");
                        return;
                    }

                    MessageBox.Show("Export succeed");
                

                    sampleCol.OpenFile(fullNameExport);
                    sampleCol.OpenFile(addressExport);
                }
                else
                {
                    MessageBox.Show("Export failed");
                }
            }
            else
            {
                MessageBox.Show("No valid data");
            }

        }
    }
}
