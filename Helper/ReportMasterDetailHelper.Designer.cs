namespace FresamLogistica.Helper
{
    partial class ReportMasterDetailHelper
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
            this.rvInfo = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvInfo
            // 
            this.rvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvInfo.Location = new System.Drawing.Point(0, 0);
            this.rvInfo.Name = "rvInfo";
            this.rvInfo.ServerReport.BearerToken = null;
            this.rvInfo.Size = new System.Drawing.Size(800, 450);
            this.rvInfo.TabIndex = 0;
            // 
            // ReportHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rvInfo);
            this.Name = "ReportMasterDetailHelper";
            this.Text = "Impresion";
            this.Load += new System.EventHandler(this.ReportMasterDetailHelper_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvInfo;
    }
}