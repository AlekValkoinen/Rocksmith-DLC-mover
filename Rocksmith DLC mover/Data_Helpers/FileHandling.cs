using Rocksmith_DLC_mover.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Rocksmith_DLC_mover.Data_Helpers
{
    internal static class FileHandling
    {
        private static void prepareTransfer(RichTextBox text, CheckBox cbSaveOrig, CheckBox cbAutoSort, CheckBox cbMakeBackup)
        {
            string ext = "*.psarc";
            try
            {
                //var files = Directory.EnumerateFiles(settings[0], ext, SearchOption.AllDirectories);
                string[] files = Directory.GetFiles(Settings_Manager.settings[0], ext);

                //we now have a list of files, the first thing we need to do is check the drive to see if it's a basic move, or a copy op.
                //I'm also going to check if the cbSaveOriginal is checked, I should actually put this all in a helper class.

                if (Path.GetPathRoot(Settings_Manager.settings[0]) == Path.GetPathRoot(Settings_Manager.settings[1]) && cbSaveOrig.Checked != true)
                {

                    foreach (string file in files)
                    {

                        string fileName = file.Substring(Settings_Manager.settings[0].Length + 1);
                        DataHelpersClass.print("FILENAME: " + fileName, text);
                        string message = Path.Combine(Settings_Manager.settings[0], file).ToString();
                        DataHelpersClass.print("Preparing to move: " + message, text);
                        //if they are saving originals or have previously this can become a dangerous operation, Check the destination to see if the file exists.
                        if (!File.Exists(Path.Combine(Settings_Manager.getCdlcFolder(), fileName)))
                        {
                            Directory.Move(file, Path.Combine(Settings_Manager.getCdlcFolder(), fileName));
                            DataHelpersClass.print("Moved: " + message, text);
                        }
                        else
                        {
                            DataHelpersClass.print("The file " + fileName + " Already exists in the destination, skipping move", Color.Red, text);
                        }
                    }
                }
                else
                {
                    //
                    DataHelpersClass.print("Source and Destination are on different drives, Making copies then deleting source files", text);
                    foreach (string file in files)
                    {

                        string fileName = file.Substring(Settings_Manager.settings[0].Length + 1);
                        DataHelpersClass.print("FILENAME: " + fileName, text);
                        string message = Path.Combine(Settings_Manager.settings[0], file).ToString();
                        DataHelpersClass.print("Preparing to move: " + message, text);

                        //again check to make sure the file doesn't already exist


                        if (!File.Exists(Path.Combine(Settings_Manager.getCdlcFolder(), fileName)))
                        {
                            File.Copy(Path.Combine(Settings_Manager.settings[0], fileName), Path.Combine(Settings_Manager.getCdlcFolder(), fileName), true);
                            //print("copied: " + message);
                            //verify the file now exists in destination.
                            if (File.Exists(Path.Combine(Settings_Manager.getCdlcFolder(), fileName)))
                            {
                                DataHelpersClass.print("File copied successfully, removing source", text);

                            }
                            else
                            {
                                DataHelpersClass.print("FILE NOT COPIED, ABORTING", text);
                                return;
                            }
                        }
                        else
                        {
                            DataHelpersClass.print("The file " + fileName + " Already exists in the destination, skipping move", Color.Red, text);
                        }
                    }
                    if (cbSaveOrig.Checked != true)
                    {
                        foreach (string f in files)
                        {
                            DataHelpersClass.print("Copy complete, Deleting source files.", text);
                            File.Delete(f);
                        }
                    }
                }

                if (cbAutoSort.Checked)
                {
                    cleanup(text);
                }
                if (cbMakeBackup.Checked)
                {
                    MakeBackup(text);
                }
            }

            catch (Exception e)
            {
                DataHelpersClass.print(e.ToString(), text);
            }
        }

        private static void MakeBackup(RichTextBox text)
        {
            //DataHelpersClass.print("Source Path is: " + Settings_Manager.getCdlcFolder(), Color.OrangeRed, text);
            //DataHelpersClass.print("Destination Path is: " + Settings_Manager.getBackupPath(), Color.OrangeRed, text);
            CopyDirectories(Settings_Manager.getCdlcFolder(), Settings_Manager.getBackupPath(), true, text);
        }

        private static void CopyDirectories(string SourceDirectory,  string DestinationDirectory, bool recursive, RichTextBox text)
        {

            // THis is going to need to be handled on a dedicated thread I think.
            DirectoryInfo cdlcInfo = new DirectoryInfo(SourceDirectory);
            DirectoryInfo Destination = new DirectoryInfo(DestinationDirectory);

            //Check Source
            if (!cdlcInfo.Exists)
            {
                MessageBox.Show("Tell the Dev The COPY Directories Function in FileHandling is not working correctly", "the dev fucked up", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //lets get the subdirectories in memory
            DirectoryInfo[] subDirectories = cdlcInfo.GetDirectories();

            //check destination, make it if it doesn't exist
            try
            {
                if (!Destination.Exists)
                {
                    DirectoryInfo di = Directory.CreateDirectory(Destination.FullName);
                    DataHelpersClass.print(Destination.FullName + " created successfully", Color.Green, text);
                }
            }
            catch
            {
                MessageBox.Show("The Destination folder does not exist and could not be created. Is this a write protected directory (program files?)", "Could not create Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //get any files that may exist in this folder and make the copies if they don't already exist.

            int testingInterator = 0;
            foreach (FileInfo file in cdlcInfo.GetFiles())
            {
                string targetFilePath = Path.Combine(Destination.FullName, file.Name);
                if (File.Exists(targetFilePath))
                {
                    DataHelpersClass.print("Files Exists, Skipping", Color.Green, text);
                    DataHelpersClass.print("Filename: " + targetFilePath, Color.Red, text);

                    testingInterator++;
                    if (testingInterator > 20)
                    {
                        break;
                    }
                }
                else
                {
                    file.CopyTo(targetFilePath);
                    DataHelpersClass.print(file.Name + "Successfully Copied" , Color.Green, text);
                }

            }
            //we need recursion here because we like to sort our crap.
            if (recursive)
            {
                foreach (DirectoryInfo subDir in subDirectories)
                {
                    string newDestinationDirectory = Path.Combine(DestinationDirectory, subDir.Name);
                    CopyDirectories(subDir.FullName, newDestinationDirectory, true, text);
                }
            }

        }

        private static void cleanup(RichTextBox text)
        {
            string path = Settings_Manager.settings[1] + Settings_Manager.dlcAppend;
            string[] files = Directory.GetFiles(path, "*psarc");
            List<string> toDelete = new List<string>();
            foreach (string f in files)
            {
                string fileName = f.Substring(path.Length + 1);
                string[] split = fileName.Split('_');

                string bandName = split[0];
                DataHelpersClass.print(bandName, text);
                string bandFolder = path + "\\" + bandName;
                if (Directory.Exists(bandFolder))
                {
                    //print("Debug Cleanup function point 1", Color.Green);
                    //DANGEROUS CODE, DID NOT CHECK IF FILE EXISTS IN FOLDER. FIXED

                    if (!File.Exists(Path.Combine(bandFolder, fileName)))
                    {
                        Directory.Move(f, Path.Combine(bandFolder, fileName));
                    }
                    else
                    {
                        DataHelpersClass.print("File Exists, Remove Skip move, and let it delete the extra", Color.Red, text);
                        //I need a list here of files to delete that don't move.
                        toDelete.Add(f);
                    }
                }
                else
                {
                    //print("Debug Cleanup function point 2", Color.Blue);
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                    DirectoryInfo di = Directory.CreateDirectory(Path.Combine(path, bandName));
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                    Directory.Move(f, Path.Combine(bandFolder, fileName));
                }


            }
            DataHelpersClass.print("Cleaning up duplicate files", Color.Green, text);
            foreach (string s in toDelete)
            {
                DataHelpersClass.print("Debug: path is: " + s, Color.Red, text);
                File.Delete(s);
            }

        }



        public static void ReqTransfer(RichTextBox text, CheckBox cbSaveOrig, CheckBox cbAutoSort, CheckBox cbMakeBackup)
        {
            prepareTransfer(text, cbSaveOrig, cbAutoSort, cbMakeBackup);
        }
        public static void ReqCleanup(RichTextBox text)
        {
            cleanup(text);
        }




    }
}
