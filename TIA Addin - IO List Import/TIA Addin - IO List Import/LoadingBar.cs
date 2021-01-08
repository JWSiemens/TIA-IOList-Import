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
        public LoadingBar()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                pb_BuildingHardware.Value = i * 10;
            }

            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i =0; i<10; i++)
            {
                Thread.Sleep(500);
                backgroundWorker1.ReportProgress(i * 10);
            }
            
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pb_BuildingHardware.Value = e.ProgressPercentage;
        }
    }
}
