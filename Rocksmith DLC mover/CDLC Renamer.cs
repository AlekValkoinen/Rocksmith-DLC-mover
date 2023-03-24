using System;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
//using System.Threading;

namespace Rocksmith_DLC_mover
{
    public partial class CDLCReNamer : Form
    {
        //setting appdata path as null so in the settings function I can do a nullcheck to falesafe it.
        string AppDataPath = null;
        string configPath = null;
        string dlcFolder = null;
        string cdlcFolder = null;
        public static bool Auto = false;
        //DLC append
        string dlcAppend = "\\dlc\\cdlc";
        public static string[] settings = new string[] { "", "", "0", "0" };

        public static FileSystemWatcher fs = new FileSystemWatcher();
        public CDLCReNamer()
        {
            InitializeComponent();
            btnTransfer.Enabled = false;
            checkForSettings();
            lblAuto.BackColor = Color.Red;
        }
        private void setLabel()
        {
            if (Auto)
            {
                lblAuto.BackColor = Color.Green;
            }
            else
            {
                lblAuto.BackColor = Color.Red;
            }
        }
        private void btnSetDownloadFolder_Click(object sender, EventArgs e)
        {
            //first we need to create a folder browser element. That's easy, so we're going to set that as Variable Dialog.
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK) //This shows the dialog, then if the result was 'ok' then it will do the following.
            {
                settings[0] = dialog.SelectedPath;
                print("Download folder is now set to " + settings[0]);
                writeSettings();
                //downFolder = dialog.SelectedPath;
                //statusBox.AppendText("Download folder has been set to " + downFolder);
            }
        }

        private void BtnSetRSFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            { 
                settings[1] = dialog.SelectedPath;
                print("RS folder has been set to " + settings[1]);
                writeSettings();
            }
        }

        //We're going to check for a setting file in AppData, for out program, If they don't exist, we're going to create them.
        private void checkForSettings()
        {
            //before we get started we want the local AppData path, this uses the System Namespace.
            //This will allow us to set the destination in a nice folder there. So let's do that first.
            string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //Now we create a variable with the "company" name for this I'm using OMD during development, Using caps in a directory is inherently a windows thing, Linux cares more so the convention is don't us caps or space on Linux filesystems. This is a strictly windows APP so it's fine to do caps)
            string orgFolder = FolderPath + "\\OMD";

            //DEBUG
            //print("DEBUG: AppdataPath: " + FolderPath);
            //print("DEBUG: ORG Path: " + orgFolder);
            //good, now we check if the path exists.

            //now the try catch block to check if the file exists, If it does, return, if it doesn't, create it, if creation fails, throw exception error"
            try
            {
                //Does the folder exist, Directory functions belong to Namespace System.IO
                if (Directory.Exists(orgFolder))
                {
                    print("Settings Folder Exists");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(orgFolder);
                    //print to the RTB
                    //first get the Date/Time as string. This is a pain in the ass and requires Globalization Namespace to make it more "Friendly" but less friendly to coding, To make things easier for localization later though we SHOULD do it this way. I'm going to put the date time function in another function, then just return the result here.
                    string Time = ConvertDate(Directory.GetCreationTime(orgFolder));
                    string debugMessage = orgFolder + " was created at " + Time + "\n";
                    print(debugMessage);
                }
            }
            //now the catch block
            catch (Exception e)
            {
                print("DEBUG EXCEPTION: " + e.ToString());
            }
            //Finally block in this case is empty
            finally
            {

            }
            AppDataPath = orgFolder;

            //time not to get or create the settings file
            //we'll set the file to config.txt remember you use a double \\ to denote a backslack in a string rather than an escape sequence
            configPath = orgFolder + "\\config.txt";
            //print(configPath);
            if (File.Exists(configPath))
            {
                print("Config Files Exist. Attempting to restore previous settings");
                readInSettings(configPath);
            }
            else
            {
                //file creation time. This is a bit of a weird block, the file should exist, and we aren't adding anything to it which means empty block braces.
                using (File.Create(configPath))
                {

                }
                print("Please set Directories to use before continueing");
            }

        }
        public string ConvertDate(DateTime dateTime)
        {
            string dateString;
            string Format = "d"; //this allows the standard US datetime specifier
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US"); //This creates the cultural format to use. This can be expanded to an array later for localization.
            dateString = dateTime.ToString(Format, culture);
            return dateString;
        }
        private void readInSettings(string filePath)
        {
            //read in the file by each line, line 0 will be the Download path, line 1 will be the RS path, it will add them to the settings list.
            int c = 0;
            foreach (string line in File.ReadLines(filePath))
            {
                if (c < 4)
                {
                    settings[c] = line;
                }
                else
                {
                    print("Hmm It seems there are more than 4 lines in settings, that shouldn't happen. Please check into that.");
                }
                c++;

            }
            foreach (string s in settings)
            {
                //print(s);
            }
            if (settings[0] == "")
            {
                print("The Download Directory is not set");
            }
            if (settings[1] == "" || settings[1] == null)
            {
                print("The RS Directory is not set.");
            }
            //print(settings[2]);
            //print(settings[3]);
            cbSaveOrig.Checked = settings[2] == "1" ? true : false;
            cbAutoSort.Checked = settings[3] == "1" ? true : false;
            

            
            checkDlcDirectories();
            checkReady();
        }

        private void writeSettings()
        {
            if (configPath != null)
            {
                using (TextWriter tw = new StreamWriter(configPath))
                {
                    tw.WriteLine(settings[0]);
                    tw.WriteLine(settings[1]);
                    tw.WriteLine(settings[2]);
                    tw.WriteLine(settings[3]);
                    //print(settings[2]);
                    //print(settings[3]);
                    print("DEBUG: Configuration updated");
                }
                //while we are here since this is called any time the directories are set, this is a good time to check if the DLC folder exists, if it does, does a CDLC folder exist inside of it.

            }
            else
            {
                print("DEBUG: CONFIG PATH IS NULL");
            }
            if (settings[0] != "" && settings[1] != "")
            {
                btnTransfer.Enabled = true;
            }
            checkReady();
        }

        private void checkDlcDirectories()
        {
            if (settings[1] != "")
            {
                dlcFolder = settings[1] + "\\dlc";
                cdlcFolder = settings[1] + dlcAppend;
                if (Directory.Exists(dlcFolder))
                {
                    print("DLC folder has been located. Checking for cdlc folder.");
                    if (Directory.Exists(cdlcFolder))
                    {
                        print("cdlc folder has been located.");
                    }
                    else
                    {
                        DirectoryInfo di = Directory.CreateDirectory(settings[1] + dlcAppend);
                        string Time = ConvertDate(Directory.GetCreationTime(settings[1] + dlcAppend));
                        string debugMessage = "DEBUG: " + settings[1] + dlcAppend + " was created at " + Time + "\n";
                        print(debugMessage);
                    }
                }
                else
                {
                    print("HOLDUP: Check your RS Directory, the DLC folder could not be found.");
                    print("This folder is created by default in a RS install. Either the directory is incorrect, or the installation may be damaged");
                }
            }
            else
            {
                print("RS Directory not set");
            }
        }

        private void checkReady()
        {
            if (settings[0] != "" && settings[1] != "")
            {
                if (Directory.Exists(cdlcFolder))
                {
                    btnTransfer.Enabled = true;
                }
            }
        }

        private void prepareTransfer()
        {
            string ext = "*.psarc";
            try
            {
                //var files = Directory.EnumerateFiles(settings[0], ext, SearchOption.AllDirectories);
                string[] files = Directory.GetFiles(settings[0], ext);
                if (Path.GetPathRoot(settings[0]) == Path.GetPathRoot(settings[1]) && cbSaveOrig.Checked != true)
                {
                    foreach (string file in files)
                    {

                        string fileName = file.Substring(settings[0].Length + 1);
                        print("FILENAME: " + fileName);
                        string message = Path.Combine(settings[0], file).ToString();
                        print("Preparing to move: " + message);
                        Directory.Move(file, Path.Combine(cdlcFolder, fileName));
                        print("Moved: " + message);
                    }
                }
                else
                {
                    print("Source and Destination are on different drives, Making copies then deleting source files");
                    foreach (string file in files)
                    {

                        string fileName = file.Substring(settings[0].Length + 1);
                        print("FILENAME: " + fileName);
                        string message = Path.Combine(settings[0], file).ToString();
                        print("Preparing to move: " + message);
                        File.Copy(Path.Combine(settings[0], fileName), Path.Combine(cdlcFolder, fileName), true);
                        //print("copied: " + message);
                        //verify the file now exists in destination.
                        if (File.Exists(Path.Combine(cdlcFolder, fileName)))
                        {
                            print("File copied successfully, removing source");
                            
                        }
                        else
                        {
                            print("FILE NOT COPIED, ABORTING");
                            return;
                        }
                    }
                    if (cbSaveOrig.Checked != true)
                    {
                        foreach (string f in files)
                        {
                            print("Copy complete, Deleting source files.");
                            File.Delete(f);
                        } 
                    }
                }

                if (cbAutoSort.Checked)
                {
                    cleanup();
                }

            }
            
            catch (Exception e)
            {
                print(e.ToString());
            }
        }


        private void cleanup()
        {
            string path = settings[1] + dlcAppend;
            string[] files = Directory.GetFiles(path, "*psarc");
            foreach (string f in files)
            {
                string fileName = f.Substring(path.Length + 1);
                string[] split = fileName.Split('_');
                string bandName = split[0];
                print(bandName);
                string bandFolder = path + "\\" + bandName;
                if (Directory.Exists(bandFolder))
                {
                    Directory.Move(f, Path.Combine(bandFolder, fileName));
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(Path.Combine(path, bandName));
                    Directory.Move(f, Path.Combine(bandFolder, fileName));
                }
                

            }
        }
        private void print(string message)
        {
            statusBox.AppendText(message + "\n");
            statusBox.ScrollToCaret();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            prepareTransfer();
        }

        private void btnCleanup_Click(object sender, EventArgs e)
        {
            cleanup();
        }

        private void cbSaveOrig_CheckedChanged(object sender, EventArgs e)
        {
            settings[2] = cbSaveOrig.Checked ? "1" : "0";
            writeSettings();
        }

        private void cbAutoSort_CheckedChanged(object sender, EventArgs e)
        {
            settings[3] = cbAutoSort.Checked ? "1" : "0";
            writeSettings();
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            if (Auto)
            {
                Auto = false;
                setLabel();
            }
            else
            {
                Auto = true;
                setLabel();
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
        public void DoAuto()
        {
            if (settings[0] != "")
            {
                fs.Path = settings[0];
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
                BeginInvoke(new Action(() => { this.print("New Song Detected: " + e.Name); }));
                prepareTransfer();
            }

        }


    } 

}
