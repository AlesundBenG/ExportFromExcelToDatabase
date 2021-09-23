using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; //Библиотека для работы с файлами.

namespace ExportFromExcelToDatabase.Classes
{
    /// <summary>
    /// Чтение текстовых файлов.
    /// </summary>
    public class ReaderTextFile
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Public методы*/

        /// <summary>
        /// Чтение файла.
        /// </summary>
        /// <param name="pathFile">Путь к файлу.</param>
        /// <returns>Весь файл в одну строку с сохранением символов Enter и Tab.</returns>
        public string getText(string pathFile) {
            using (FileStream filestream = File.OpenRead(pathFile)) {
                /*
                 * Чтение файла по байтам.
                byte[] array = new byte[filestream.Length];
                filestream.Read(array, 0, array.Length);
                return Encoding.UTF8.GetString(array);
                */
                return File.ReadAllText(pathFile);
            }
        }

        /// <summary>
        /// Чтение файла.
        /// </summary>
        /// <param name="pathFile">Путь к файлу.</param>
        /// <returns>Весь файл разбит на строки с сохранением символов Tab.</returns>
        public string[] getSplitTextOnLines(string pathFile) {
            return File.ReadAllLines(pathFile);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
