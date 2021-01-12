
namespace TIA_Addin_IO_List_Import
{
    partial class LoadingBar
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
            this.pb_BuildingHardware = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // pb_BuildingHardware
            // 
            this.pb_BuildingHardware.Location = new System.Drawing.Point(41, 35);
            this.pb_BuildingHardware.Name = "pb_BuildingHardware";
            this.pb_BuildingHardware.Size = new System.Drawing.Size(680, 72);
            this.pb_BuildingHardware.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pb_BuildingHardware.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // LoadingBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 140);
            this.Controls.Add(this.pb_BuildingHardware);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Name = "LoadingBar";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pb_BuildingHardware;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}