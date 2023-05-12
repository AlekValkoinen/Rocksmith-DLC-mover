using System;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using Rocksmith_DLC_mover.Settings;
using Rocksmith_DLC_mover.Data_Helpers;
using System.ComponentModel;
//using System.Threading;

/*
 * This program is to make life easier for people that use custom content in Rocksmith.
 * The idea has been ventured several times, one member of the community made a shell script that scans
 * the download folder every 30 seconds for new content. While this works, it's not efficient
 * The tool lacks flexibility and options, and lastly, it is blasting read cycles onto the drive.
 * 
 * This program is my solution. What it does - 
 * 
 * This program will subscribe to the OS events of new files in the selected folder
 * when it is signaled there is a new file it will check if that is a PSARC file
 * in the event it is, it will move that file to the DLC location in rocksmith
 * it will then either leave it alone if the save original option is set
 * or delete the original file.
 * 
 * 
 * Exteneded functionality, if your CDLC folder is disorganized I've added a function to parse the band and song names (mostly reliably)
 * then autosort that.
 * 
 * 
 * THIS PROJECT AND CODE BELONGS TO ALEKSANDEROMD UNLESS OTHERWISE NOTATED.
 * PLEASE DO NOT ALTER OR TAKE CREDIT FOR MY WORK
 * 
 * 
 * That said, please do share it, but credit where it's due, in my code when I use someone elses code, I mention who and where
 * please do the same.
 * 
 * DISCLAIMER I am not responsible for anything. If thise program turns your computer into the core of a dying star, not my problem, it's your problem.
 * 
 * 
 * 
 * 
*/



namespace Rocksmith_DLC_mover
{
    public partial class CDLCReNamer : Form
    {
        public static bool Auto = false;
        Button btnClear = new Button();
        Button btnSetBackup = new Button();
        public static FileSystemWatcher fs = new FileSystemWatcher();
        BackgroundWorker FileWorker = new BackgroundWorker();


        public CDLCReNamer()
        {
            InitializeComponent();

            btnTransfer.Enabled = false;
            Settings.Settings_Manager.checkForSettings(statusBox, cbSaveOrig, cbAutoSort, btnTransfer, cbBackupCDLC);
            lblAuto.BackColor = Color.Red;
            ControlHelper controlHelper = new ControlHelper();
            btnClear = controlHelper.MakeButton(btnAbort.Right + 10, btnTransfer.Left - 10, btnTransfer.Height, "btnClear", "Clear Log");
            btnClear.Location = controlHelper.MakePoint(btnAbort.Location, btnAbort.Width + 10, true);
            btnClear.Click += new EventHandler(this.btnClear_Click);
            Controls.Add(btnClear);

            btnSetBackup = controlHelper.MakeButton(BtnSetRSFolder.Left, BtnSetRSFolder.Right, BtnSetRSFolder.Height, "btnSetBackup", "Set CDLC Backup Location");
            btnSetBackup.Location = controlHelper.MakePoint(BtnSetRSFolder.Location, BtnSetRSFolder.Height + 5, false);
            btnSetBackup.Click += new EventHandler(this.btnSetBackup_Click);
            Controls.Add(btnSetBackup);


            //set up background worker

            FileWorker.WorkerSupportsCancellation = true;
            FileWorker.WorkerReportsProgress = true;
            FileWorker.DoWork += new DoWorkEventHandler(FileWorker_DoWork);
            FileWorker.ProgressChanged += new ProgressChangedEventHandler(FileWorker_Updates);
        }

        private void FileWorker_Updates(object sender, ProgressChangedEventArgs e)
        {
            string message = e.UserState.ToString();
            DataHelpersClass.print(message, Color.Red, statusBox);
        }


        private void SetLabel()
        {
            lblAuto.BackColor = Auto ? Color.Green : Color.Red;
            //if (Auto)
            //{
            //    lblAuto.BackColor = Color.Green;
            //}
            //else
            //{
            //    lblAuto.BackColor = Color.Red;
            //}
        }

        private void btnSetBackup_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This task is Beta, Proceed with Caution, If you intend to make a backup, Check the box below. This function will create a CDLC folder in the Folder you choose at the time of the next transfer.", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK) //This shows the dialog, then if the result was 'ok' then it will do the following.
                {
                    Settings_Manager.settings[4] = dialog.SelectedPath;
                    DataHelpersClass.populateDlcFilesList(Settings_Manager.getDlcFolder(), Settings_Manager.getCdlcFolder());
                    DataHelpersClass.print("Backup folder is now set to " + Settings_Manager.settings[4], statusBox);
                    Settings_Manager.requestWriteSettings(btnTransfer, statusBox);
                    //downFolder = dialog.SelectedPath;
                    //statusBox.AppendText("Download folder has been set to " + downFolder);
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            statusBox.Clear();
        }
        private void btnSetDownloadFolder_Click(object sender, EventArgs e)
        {
            //first we need to create a folder browser element. That's easy, so we're going to set that as Variable Dialog.
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK) //This shows the dialog, then if the result was 'ok' then it will do the following.
            {
                Settings_Manager.settings[0] = dialog.SelectedPath;
                DataHelpersClass.populateDlcFilesList(Settings_Manager.getDlcFolder(), Settings_Manager.getCdlcFolder());
                DataHelpersClass.print("Download folder is now set to " + Settings_Manager.settings[0], statusBox);
                Settings_Manager.requestWriteSettings(btnTransfer, statusBox);
                //downFolder = dialog.SelectedPath;
                //statusBox.AppendText("Download folder has been set to " + downFolder);
            }
        }

        private void BtnSetRSFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            { 
                Settings_Manager.settings[1] = dialog.SelectedPath;
                DataHelpersClass.print("RS folder has been set to " + Settings_Manager.settings[1], statusBox);
                Settings_Manager.requestWriteSettings(btnTransfer, statusBox);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            FileHandling.ReqTransfer(statusBox, cbSaveOrig, cbAutoSort, cbBackupCDLC);
            //if (FileWorker.IsBusy != true)
            //{
            //    FileWorker.RunWorkerAsync();
            //}
            //FileHandling.ReqTransfer(statusBox, cbSaveOrig, cbAutoSort, cbBackupCDLC);
        }
        private void FileWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            else
            {
                FileHandling.ReqTransfer(statusBox, cbSaveOrig, cbAutoSort, cbBackupCDLC);
            }

        }


        private void btnCleanup_Click(object sender, EventArgs e)
        {
            FileHandling.ReqCleanup(statusBox);
        }

        private void cbSaveOrig_CheckedChanged(object sender, EventArgs e)
        {
            Settings_Manager.settings[2] = cbSaveOrig.Checked ? "1" : "0";
            Settings_Manager.requestWriteSettings(btnTransfer, statusBox);
        }

        private void cbAutoSort_CheckedChanged(object sender, EventArgs e)
        {
            Settings_Manager.settings[3] = cbAutoSort.Checked ? "1" : "0";
            Settings_Manager.requestWriteSettings(btnTransfer, statusBox);
        }
        private void cbMakeBackup_CheckedChanged(object sender, EventArgs e)
        {
            //befire we can commit to this, we need to make sure that we have a destination set, if not, we uncheck the box, and do not turn it on.
            if (cbBackupCDLC.Checked)
            {
                if (Settings_Manager.settings[4] == "")
                {
                    MessageBox.Show("You MUST set a backup directory before enabling this feature.", "SET BACKUP DIRECTORY", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbBackupCDLC.Checked = false;
                    return;
                }
            }
            Settings_Manager.settings[5] = cbBackupCDLC.Checked ? "1" : "0";
            Settings_Manager.requestWriteSettings(btnTransfer, statusBox);
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            if (Auto)
            {
                Auto = false;
                SetLabel();
            }
            else
            {
                Auto = true;
                SetLabel();
            }
            if (Auto)
            {
                DoAuto();
            }
            else
            {
                fs.EnableRaisingEvents = false;
            }
        }

        //These can likely be moved elsewhere, but for now this is convenient.
        public void DoAuto()
        {
            if (Settings_Manager.settings[0] != "")
            {
                fs.Path = Settings_Manager.settings[0];
                fs.IncludeSubdirectories = true;
                fs.Filter = "*.psarc";
                fs.Created += new FileSystemEventHandler(autoFile);
                fs.EnableRaisingEvents = true;
            }
        }
        
        public void autoFile(object source, FileSystemEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => { DataHelpersClass.print("New Song Detected: " + e.Name, statusBox); }));
                //FileHandling.ReqTransfer(statusBox, cbSaveOrig, cbAutoSort, cbBackupCDLC);
                if (!FileWorker.IsBusy)
                {
                    FileWorker.RunWorkerAsync();
                }
            }

        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            if (FileWorker.WorkerSupportsCancellation)
            {
                FileWorker.CancelAsync();
            }
        }
    } 

}
