using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TIA_Addin_IO_List_Import
{
    public partial class LoadingBar : Form
    {

        int loadingBarTime = 0;
        public LoadingBar(int loadingTime)
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            loadingBarTime = loadingTime;
            //Set length of loading bar            
            pb_BuildingHardware.Maximum = loadingBarTime;

        }

        private void LoadingBar_Shown(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();                       
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < loadingBarTime; i++)
            {
                Thread.Sleep(500);
                backgroundWorker1.ReportProgress(i);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pb_BuildingHardware.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
