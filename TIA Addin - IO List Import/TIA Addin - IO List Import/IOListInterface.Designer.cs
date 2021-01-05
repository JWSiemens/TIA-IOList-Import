
namespace TIA_Addin_IO_List_Import
{
    partial class IOListInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_ReadCSV = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_CSVFileLocation = new System.Windows.Forms.TextBox();
            this.tb_Status = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_LogFileLocation = new System.Windows.Forms.TextBox();
            this.btn_CreateHW = new System.Windows.Forms.Button();
            this.pb_HWProgress = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btn_AsyncTest = new System.Windows.Forms.Button();
            this.btn_Test = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ReadCSV
            // 
            this.btn_ReadCSV.Location = new System.Drawing.Point(76, 78);
            this.btn_ReadCSV.Name = "btn_ReadCSV";
            this.btn_ReadCSV.Size = new System.Drawing.Size(277, 146);
            this.btn_ReadCSV.TabIndex = 0;
            this.btn_ReadCSV.Text = "Read CSV File";
            this.btn_ReadCSV.UseVisualStyleBackColor = true;
            this.btn_ReadCSV.Click += new System.EventHandler(this.btn_ReadCSV_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(480, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(849, 379);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "CSV File Location";
            // 
            // tb_CSVFileLocation
            // 
            this.tb_CSVFileLocation.Location = new System.Drawing.Point(26, 291);
            this.tb_CSVFileLocation.Name = "tb_CSVFileLocation";
            this.tb_CSVFileLocation.Size = new System.Drawing.Size(411, 26);
            this.tb_CSVFileLocation.TabIndex = 3;
            this.tb_CSVFileLocation.Text = "H:\\VersionControlSpace\\io-list-import-tia-add-in\\IO List\\IO List Template.csv";
            // 
            // tb_Status
            // 
            this.tb_Status.Enabled = false;
            this.tb_Status.Location = new System.Drawing.Point(11, 411);
            this.tb_Status.Name = "tb_Status";
            this.tb_Status.Size = new System.Drawing.Size(1318, 26);
            this.tb_Status.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 449);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Log File Location";
            // 
            // tb_LogFileLocation
            // 
            this.tb_LogFileLocation.Location = new System.Drawing.Point(26, 488);
            this.tb_LogFileLocation.Name = "tb_LogFileLocation";
            this.tb_LogFileLocation.Size = new System.Drawing.Size(411, 26);
            this.tb_LogFileLocation.TabIndex = 6;
            this.tb_LogFileLocation.Text = "H:\\VersionControlSpace\\io-list-import-tia-add-in\\Compile Logs";
            // 
            // btn_CreateHW
            // 
            this.btn_CreateHW.Enabled = false;
            this.btn_CreateHW.Location = new System.Drawing.Point(76, 532);
            this.btn_CreateHW.Name = "btn_CreateHW";
            this.btn_CreateHW.Size = new System.Drawing.Size(277, 124);
            this.btn_CreateHW.TabIndex = 7;
            this.btn_CreateHW.Text = "Create HW from IO List";
            this.btn_CreateHW.UseVisualStyleBackColor = true;
            this.btn_CreateHW.Click += new System.EventHandler(this.btn_CreateHW_Click);
            // 
            // pb_HWProgress
            // 
            this.pb_HWProgress.Location = new System.Drawing.Point(496, 614);
            this.pb_HWProgress.Name = "pb_HWProgress";
            this.pb_HWProgress.Size = new System.Drawing.Size(833, 42);
            this.pb_HWProgress.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(540, 573);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Progress of Harware Build:";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btn_AsyncTest
            // 
            this.btn_AsyncTest.Location = new System.Drawing.Point(926, 472);
            this.btn_AsyncTest.Name = "btn_AsyncTest";
            this.btn_AsyncTest.Size = new System.Drawing.Size(348, 112);
            this.btn_AsyncTest.TabIndex = 10;
            this.btn_AsyncTest.Text = "Create HW Asynchronosly";
            this.btn_AsyncTest.UseVisualStyleBackColor = true;
            this.btn_AsyncTest.Click += new System.EventHandler(this.btn_AsyncTest_Click);
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(714, 478);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(103, 54);
            this.btn_Test.TabIndex = 11;
            this.btn_Test.Text = "Test";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // IOListInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1426, 753);
            this.Controls.Add(this.btn_Test);
            this.Controls.Add(this.btn_AsyncTest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pb_HWProgress);
            this.Controls.Add(this.btn_CreateHW);
            this.Controls.Add(this.tb_LogFileLocation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_Status);
            this.Controls.Add(this.tb_CSVFileLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_ReadCSV);
            this.Name = "IOListInterface";
            this.Text = "IO List Import";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ReadCSV;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_CSVFileLocation;
        private System.Windows.Forms.TextBox tb_Status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_LogFileLocation;
        private System.Windows.Forms.Button btn_CreateHW;
        private System.Windows.Forms.ProgressBar pb_HWProgress;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btn_AsyncTest;
        private System.Windows.Forms.Button btn_Test;
    }
}