﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Siemens.Engineering;
using Siemens.Engineering.Compiler;
using Siemens.Engineering.Hmi;
using Siemens.Engineering.HW;
using Siemens.Engineering.HW.Features;
using Siemens.Engineering.SW;
using Siemens.Engineering.SW.Tags;






namespace TIA_Addin_IO_List_Import
{
    public partial class IOListInterface : Form
    {
        public TiaPortal MyTiaPortal
        {
            get; set;
        }
        public Project MyProject
        {
            get; set;
        }

        public DataTable dataTable
        {
            get; set;
        }

        public IOListInterface(TiaPortal tiaPortal)
        {
            InitializeComponent();                 

            //Move project info to the form
            MyTiaPortal = tiaPortal;
            MyProject = MyTiaPortal.Projects.First();
        }


        private void btn_ReadCSV_Click(object sender, EventArgs e)
        {
            //this function reads in data from CSV file and populates a data grid view
            //disable read and import buttons
            btn_ReadCSV.Enabled = false;
            btn_CreateHW.Enabled = false;
            try
            {
                string csvFilePath = tb_CSVFileLocation.Text;
                string[] Lines = File.ReadAllLines(csvFilePath);
                string[] Fields;
                Fields = Lines[0].Split(new char[] { ',' });
                int numColumns = Fields.GetLength(0);
                DataTable dt = new DataTable();
                for (int i = 0; i < numColumns; i++)                        
                    dt.Columns.Add(Fields[i], typeof(string));
                DataRow Row;
                for (int i = 1; i < Lines.GetLength(0); i++)
                {
                    Fields = Lines[i].Split(new char[] { ',' });
                    Row = dt.NewRow();
                    for (int j = 0; j < numColumns; j++)
                        Row[j] = Fields[j];
                    dt.Rows.Add(Row);
                }
                dataGridView1.DataSource = dt;

                //Enable read and import button
                btn_CreateHW.Enabled = true;
                btn_ReadCSV.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.ToString());
                //Disbale import button and reenable read button
                btn_ReadCSV.Enabled = true;
                btn_CreateHW.Enabled = false;
                //throw; // this throws an error and stops the application
            }
        }            
      

        private void btn_CreateHW_Click(object sender, EventArgs e)
        {            
            //Move dataTable to original program
            dataTable = (DataTable)dataGridView1.DataSource;
            this.DialogResult = DialogResult.OK;
        }

        private void btn_CreateSample_Click(object sender, EventArgs e)
        {
            //All this function does is creates a csv with the correct headers, may add on to give a few example rows as well

            string csvFilePath = "H:\\VersionControlSpace\\io-list-import-tia-add-in\\IO List\\sampleIO.csv";
            DataTable dt = new DataTable();
            dt.Columns.Add("Tagname");   //during the transfer the text changes to all lower case            
            dt.Columns.Add("IO Type");
            dt.Columns.Add("Rack");
            dt.Columns.Add("Slot");
            dt.Columns.Add("Channel");
            dt.Columns.Add("PLC");
            dt.Columns.Add("Signal Type");
            dt.Columns.Add("Part Number");
            dt.Columns.Add("Firmware Version");
            dt.Columns.Add("IP Address");
            dt.Columns.Add("Subnet Mask");
            dt.Columns.Add("Default Gateway");
            StreamWriter sw = new StreamWriter(csvFilePath, false);

            for (int i = 0; i< dt.Columns.Count; i++)
            {
                sw.Write(dt.Columns[i].ColumnName);
                if (i < dt.Columns.Count - 1)
                    sw.Write(",");
            }


            //Clean up...
            sw.Close();
            dt.Dispose();
        }


        //Create folder dialog for log location
        FolderBrowserDialog folderDialog = new FolderBrowserDialog()
        {
            //Set folder dialog attributes
            ShowNewFolderButton = true
        };

        //Create file dialog for IO list location
        OpenFileDialog fileDialog = new OpenFileDialog()
        {
            //Set filedialog attributes
            DefaultExt = "csv",
            CheckPathExists = true,
            CheckFileExists = true,
            Title = "Browse for csv file",
            Filter = "csv files (*.csv)|*.csv",
            FilterIndex = 2
        };
             

        private void btnIOListLocDialog_Click(object sender, EventArgs e)
        {               
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tb_CSVFileLocation.Text = fileDialog.FileName;
            }
        }

        private void btnLogLocationDialog_Click(object sender, EventArgs e)
        {            
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tb_LogFileLocation.Text = folderDialog.SelectedPath;

            }
        }
    }
}
