using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; //lib for read file.

namespace ExportFromExcelToDatabase.Classes
{
    public class ReaderTextFile
    {
        public string getText(string pathFile) {
            using (FileStream filestream = File.OpenRead(pathFile)) {
                /*
                //Read file as array bits and encoding.
                byte[] array = new byte[filestream.Length];
                filestream.Read(array, 0, array.Length);
                return Encoding.UTF8.GetString(array);
                */
                return File.ReadAllText(pathFile);

            }
        }

        public string[] getSplitTextOnLines(string pathFile) {
            return File.ReadAllLines(pathFile);
        }
    }
}
