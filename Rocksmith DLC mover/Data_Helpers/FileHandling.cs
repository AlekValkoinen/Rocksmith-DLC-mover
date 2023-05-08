using Rocksmith_DLC_mover.Settings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rocksmith_DLC_mover.Data_Helpers
{
    internal static class FileHandling
    {
        private static void prepareTransfer(RichTextBox text, CheckBox cbSaveOrig, CheckBox cbAutoSort)
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
            }

            catch (Exception e)
            {
                DataHelpersClass.print(e.ToString(), text);
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



        public static void ReqTransfer(RichTextBox text, CheckBox cbSaveOrig, CheckBox cbAutoSort)
        {
            prepareTransfer(text, cbSaveOrig, cbAutoSort);
        }
        public static void ReqCleanup(RichTextBox text)
        {
            cleanup(text);
        }




    }
}
