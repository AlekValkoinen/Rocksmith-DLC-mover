using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace Rocksmith_DLC_mover
{
    public static class DataHelpersClass
    {
        //This class functions as a very basic way for me to handle the data more modularly, for the start, populating the list of DLC we already have, so we need the DLC and CLDCL folder.
        //Instead of storing that globally in the class, it will be passed in at the time we invoke the functions.

        public static List<string> populateDlcFilesList(string dlcfolder, string cdlcFolder)
        {
            List<string> dlcFiles = new List<string>();
            string[] files = Directory.GetFiles(dlcfolder);
            foreach (string f in files)
            {
                dlcFiles.Add(f);
            }
            string[] files2 = Directory.GetFiles(cdlcFolder);
            foreach (string f in files2)
            {
                dlcFiles.Add(f);
            }
            return dlcFiles;
        }

        public static void print(string message, RichTextBox tb)
        {
            tb.AppendText(message + "\n");
            tb.ScrollToCaret();
        }
        public static void print(string message, Color color, RichTextBox tb)
        {
            tb.AppendText(message + "\n", color);
            tb.ScrollToCaret();
        }

        public static string ConvertDate(DateTime dateTime)
        {
            string dateString;
            string Format = "d"; //this allows the standard US datetime specifier
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US"); //This creates the cultural format to use. This can be expanded to an array later for localization.
            dateString = dateTime.ToString(Format, culture);
            return dateString;
        }

    }


}
