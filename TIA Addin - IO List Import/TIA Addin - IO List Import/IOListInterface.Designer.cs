
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IOListInterface));
            this.btn_ReadCSV = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_CSVFileLocation = new System.Windows.Forms.TextBox();
            this.tb_Status = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_LogFileLocation = new System.Windows.Forms.TextBox();
            this.btn_CreateHW = new System.Windows.Forms.Button();
            this.btn_CreateSample = new System.Windows.Forms.Button();
            this.btnIOListLocDialog = new System.Windows.Forms.Button();
            this.btnLogLocationDialog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ReadCSV
            // 
            this.btn_ReadCSV.Location = new System.Drawing.Point(76, 29);
            this.btn_ReadCSV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ReadCSV.Name = "btn_ReadCSV";
            this.btn_ReadCSV.Size = new System.Drawing.Size(277, 101);
            this.btn_ReadCSV.TabIndex = 0;
            this.btn_ReadCSV.Text = "Read CSV File";
            this.btn_ReadCSV.UseVisualStyleBackColor = true;
            this.btn_ReadCSV.Click += new System.EventHandler(this.btn_ReadCSV_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(480, 12);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(849, 379);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "CSV File Location";
            // 
            // tb_CSVFileLocation
            // 
            this.tb_CSVFileLocation.Location = new System.Drawing.Point(26, 204);
            this.tb_CSVFileLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_CSVFileLocation.Name = "tb_CSVFileLocation";
            this.tb_CSVFileLocation.Size = new System.Drawing.Size(375, 26);
            this.tb_CSVFileLocation.TabIndex = 3;
            this.tb_CSVFileLocation.Text = "H:\\VersionControlSpace\\io-list-import-tia-add-in\\IO List\\IO List Template.csv";
            // 
            // tb_Status
            // 
            this.tb_Status.Enabled = false;
            this.tb_Status.Location = new System.Drawing.Point(11, 411);
            this.tb_Status.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.tb_LogFileLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_LogFileLocation.Name = "tb_LogFileLocation";
            this.tb_LogFileLocation.Size = new System.Drawing.Size(411, 26);
            this.tb_LogFileLocation.TabIndex = 6;
            this.tb_LogFileLocation.Text = "H:\\VersionControlSpace\\io-list-import-tia-add-in\\Compile Logs";
            // 
            // btn_CreateHW
            // 
            this.btn_CreateHW.Location = new System.Drawing.Point(76, 542);
            this.btn_CreateHW.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_CreateHW.Name = "btn_CreateHW";
            this.btn_CreateHW.Size = new System.Drawing.Size(277, 114);
            this.btn_CreateHW.TabIndex = 11;
            this.btn_CreateHW.Text = "Create Hardware";
            this.btn_CreateHW.UseVisualStyleBackColor = true;
            this.btn_CreateHW.Click += new System.EventHandler(this.btn_CreateHW_Click);
            // 
            // btn_CreateSample
            // 
            this.btn_CreateSample.Location = new System.Drawing.Point(76, 286);
            this.btn_CreateSample.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_CreateSample.Name = "btn_CreateSample";
            this.btn_CreateSample.Size = new System.Drawing.Size(277, 88);
            this.btn_CreateSample.TabIndex = 12;
            this.btn_CreateSample.Text = "Create Sample CSV";
            this.btn_CreateSample.UseVisualStyleBackColor = true;
            this.btn_CreateSample.Click += new System.EventHandler(this.btn_CreateSample_Click);
            // 
            // btnIOListLocDialog
            // 
            this.btnIOListLocDialog.Location = new System.Drawing.Point(400, 201);
            this.btnIOListLocDialog.Name = "btnIOListLocDialog";
            this.btnIOListLocDialog.Size = new System.Drawing.Size(45, 32);
            this.btnIOListLocDialog.TabIndex = 13;
            this.btnIOListLocDialog.Text = "...";
            this.btnIOListLocDialog.UseVisualStyleBackColor = true;
            this.btnIOListLocDialog.Click += new System.EventHandler(this.btnIOListLocDialog_Click);
            // 
            // btnLogLocationDialog
            // 
            this.btnLogLocationDialog.Location = new System.Drawing.Point(436, 485);
            this.btnLogLocationDialog.Name = "btnLogLocationDialog";
            this.btnLogLocationDialog.Size = new System.Drawing.Size(45, 32);
            this.btnLogLocationDialog.TabIndex = 14;
            this.btnLogLocationDialog.Text = "...";
            this.btnLogLocationDialog.UseVisualStyleBackColor = true;
            this.btnLogLocationDialog.Click += new System.EventHandler(this.btnLogLocationDialog_Click);
            // 
            // IOListInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1426, 752);
            this.Controls.Add(this.btnLogLocationDialog);
            this.Controls.Add(this.btnIOListLocDialog);
            this.Controls.Add(this.btn_CreateSample);
            this.Controls.Add(this.btn_CreateHW);
            this.Controls.Add(this.tb_LogFileLocation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_Status);
            this.Controls.Add(this.tb_CSVFileLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_ReadCSV);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.Button btn_CreateSample;
        private System.Windows.Forms.Button btnIOListLocDialog;
        private System.Windows.Forms.Button btnLogLocationDialog;
    }
}