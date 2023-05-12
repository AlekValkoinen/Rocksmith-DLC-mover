
namespace Rocksmith_DLC_mover
{
    partial class CDLCReNamer
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
            this.statusBox = new System.Windows.Forms.RichTextBox();
            this.btnSetDownloadFolder = new System.Windows.Forms.Button();
            this.BtnSetRSFolder = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.btnCleanup = new System.Windows.Forms.Button();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.cbBackupCDLC = new System.Windows.Forms.CheckBox();
            this.cbAutoSort = new System.Windows.Forms.CheckBox();
            this.cbSaveOrig = new System.Windows.Forms.CheckBox();
            this.btnAuto = new System.Windows.Forms.Button();
            this.lblAuto = new System.Windows.Forms.Label();
            this.gbOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(13, 13);
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.Size = new System.Drawing.Size(557, 617);
            this.statusBox.TabIndex = 0;
            this.statusBox.Text = "";
            // 
            // btnSetDownloadFolder
            // 
            this.btnSetDownloadFolder.Location = new System.Drawing.Point(595, 13);
            this.btnSetDownloadFolder.Name = "btnSetDownloadFolder";
            this.btnSetDownloadFolder.Size = new System.Drawing.Size(187, 49);
            this.btnSetDownloadFolder.TabIndex = 1;
            this.btnSetDownloadFolder.Text = "Set Download Folder";
            this.btnSetDownloadFolder.UseVisualStyleBackColor = true;
            this.btnSetDownloadFolder.Click += new System.EventHandler(this.btnSetDownloadFolder_Click);
            // 
            // BtnSetRSFolder
            // 
            this.BtnSetRSFolder.Location = new System.Drawing.Point(595, 68);
            this.BtnSetRSFolder.Name = "BtnSetRSFolder";
            this.BtnSetRSFolder.Size = new System.Drawing.Size(187, 48);
            this.BtnSetRSFolder.TabIndex = 2;
            this.BtnSetRSFolder.Text = "Set RS Folder";
            this.BtnSetRSFolder.UseVisualStyleBackColor = true;
            this.BtnSetRSFolder.Click += new System.EventHandler(this.BtnSetRSFolder_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(595, 686);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(187, 50);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(13, 677);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(187, 59);
            this.btnAbort.TabIndex = 4;
            this.btnAbort.Text = "Abort Transfer";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(383, 677);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(187, 59);
            this.btnTransfer.TabIndex = 5;
            this.btnTransfer.Text = "Start Transfer";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // btnCleanup
            // 
            this.btnCleanup.Location = new System.Drawing.Point(595, 258);
            this.btnCleanup.Name = "btnCleanup";
            this.btnCleanup.Size = new System.Drawing.Size(187, 48);
            this.btnCleanup.TabIndex = 6;
            this.btnCleanup.Text = "Cleanup CDLC folder";
            this.btnCleanup.UseVisualStyleBackColor = true;
            this.btnCleanup.Click += new System.EventHandler(this.btnCleanup_Click);
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.cbBackupCDLC);
            this.gbOptions.Controls.Add(this.cbAutoSort);
            this.gbOptions.Controls.Add(this.cbSaveOrig);
            this.gbOptions.Location = new System.Drawing.Point(595, 388);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(200, 170);
            this.gbOptions.TabIndex = 7;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // cbBackupCDLC
            // 
            this.cbBackupCDLC.AutoSize = true;
            this.cbBackupCDLC.Location = new System.Drawing.Point(7, 120);
            this.cbBackupCDLC.Name = "cbBackupCDLC";
            this.cbBackupCDLC.Size = new System.Drawing.Size(171, 24);
            this.cbBackupCDLC.TabIndex = 2;
            this.cbBackupCDLC.Text = "Auto Back up CDLC";
            this.cbBackupCDLC.UseVisualStyleBackColor = true;
            this.cbBackupCDLC.CheckedChanged += new System.EventHandler(this.cbMakeBackup_CheckedChanged);
            // 
            // cbAutoSort
            // 
            this.cbAutoSort.AutoSize = true;
            this.cbAutoSort.Location = new System.Drawing.Point(7, 80);
            this.cbAutoSort.Name = "cbAutoSort";
            this.cbAutoSort.Size = new System.Drawing.Size(152, 24);
            this.cbAutoSort.TabIndex = 1;
            this.cbAutoSort.Text = "Auto Sort on Xfer";
            this.cbAutoSort.UseVisualStyleBackColor = true;
            this.cbAutoSort.CheckedChanged += new System.EventHandler(this.cbAutoSort_CheckedChanged);
            // 
            // cbSaveOrig
            // 
            this.cbSaveOrig.AutoSize = true;
            this.cbSaveOrig.Location = new System.Drawing.Point(7, 40);
            this.cbSaveOrig.Name = "cbSaveOrig";
            this.cbSaveOrig.Size = new System.Drawing.Size(158, 24);
            this.cbSaveOrig.TabIndex = 0;
            this.cbSaveOrig.Text = "Save Original Files";
            this.cbSaveOrig.UseVisualStyleBackColor = true;
            this.cbSaveOrig.CheckedChanged += new System.EventHandler(this.cbSaveOrig_CheckedChanged);
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(595, 582);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(187, 48);
            this.btnAuto.TabIndex = 8;
            this.btnAuto.Text = "Auto Mode";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // lblAuto
            // 
            this.lblAuto.Location = new System.Drawing.Point(595, 637);
            this.lblAuto.Name = "lblAuto";
            this.lblAuto.Size = new System.Drawing.Size(187, 34);
            this.lblAuto.TabIndex = 9;
            // 
            // CDLCReNamer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 748);
            this.Controls.Add(this.lblAuto);
            this.Controls.Add(this.btnAuto);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.btnCleanup);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.BtnSetRSFolder);
            this.Controls.Add(this.btnSetDownloadFolder);
            this.Controls.Add(this.statusBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CDLCReNamer";
            this.Text = "CDLC Renamer";
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox statusBox;
        private System.Windows.Forms.Button btnSetDownloadFolder;
        private System.Windows.Forms.Button BtnSetRSFolder;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnCleanup;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox cbAutoSort;
        private System.Windows.Forms.CheckBox cbSaveOrig;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Label lblAuto;
        private System.Windows.Forms.CheckBox cbBackupCDLC;
    }
}

