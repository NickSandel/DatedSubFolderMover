using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace DatedSubFolderMover
{
    class Program
    {
        static void Main(string[] args)
        {
            string yearFolder;
            string monthFolder;
            //By default will run in mode of doing year folders and then month
            //Only expected input will be parent directory name
            Console.WriteLine("Please enter sub directory name:\n");
            //Console.Write(Directory.GetCurrentDirectory() + @"\");
            //string directoryName = Directory.GetCurrentDirectory() + @"\" + Console.ReadLine();
            string directoryName = Console.ReadLine();
            DirectoryInfo parentDirectory = new DirectoryInfo(directoryName);
            Console.WriteLine("Directory selected is: " + parentDirectory.Name);

            int limiter = 1000;
            int counter = 0;

            IEnumerable<FileInfo> files = parentDirectory.GetFiles();
            foreach (FileInfo file in files)
            {
                counter++;
                //Console.WriteLine(string.Concat("File name is: ", file.Name, " File Date Created is: ", file.LastWriteTime));
                //yearFolder = file.LastWriteTime.Year;
                yearFolder = file.Name.Substring(0, 4);
                monthFolder = file.Name.Substring(4, 2);

                string[] months = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
                int test;
                if (!int.TryParse(yearFolder, out test))
                    break;

                if (!months.Contains(monthFolder))
                    break;
                //monthFolder = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(file.LastWriteTime.Month);
                //monthFolder = file.LastWriteTime.Month.ToString().PadLeft(2, '0');
                //Console.WriteLine(string.Concat("Year folder will be: ", yearFolder, " month folder will be: ", monthFolder));

                string yearDirectory = parentDirectory + @"\" + yearFolder;
                if (!Directory.Exists(yearDirectory))
                {
                    Directory.CreateDirectory(yearDirectory);
                }

                string monthDirectory = parentDirectory + @"\" + yearFolder + @"\" + monthFolder;
                if (!Directory.Exists(monthDirectory))
                {
                    Directory.CreateDirectory(monthDirectory);
                }

                File.Move(file.FullName, monthDirectory + @"\" + file.Name);

                if (counter == limiter)
                    break;
            }

            Console.WriteLine("File Moves Completed");
            Console.ReadLine();
        }
    }
}
