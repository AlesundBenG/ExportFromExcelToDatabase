using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportFromExcelToDatabase.Classes
{
    /// <summary>
    /// Excel-файл.
    /// </summary>
    public class ExcelFile
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Атрибуты*/

        /// <summary>
        /// Путь к файлу.
        /// </summary>
        private string _pathFile;
        /// <summary>
        /// Наименования страниц.
        /// </summary>
        private List<string> _titleSheet;
        /// <summary>
        /// Страницы файла.
        /// </summary>
        private List<string[,]> _sheetsFile;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Свойства*/

        /// <summary>
        /// Расположение файла.
        /// </summary>
        public string PathFile {
            get {
                return _pathFile;
            }
        }

        /// <summary>
        /// Количество страниц в файле.
        /// </summary>
        public int CountSheet {
            get {
                return _titleSheet.Count;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Public методы*/

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="pathFile">Путь к файлу.</param>
        /// <param name="titleSheet">Наименование страниц.</param>
        /// <param name="sheetFile">Страницы файла.</param>
        public ExcelFile(string pathFile, List<string> titleSheet, List<string[,]> sheetFile) {
            _pathFile = pathFile ?? "";
            _titleSheet = titleSheet ?? new List<string>();
            _sheetsFile = sheetFile ?? new List<string[,]>();
        }

        /// <summary>
        /// Получить наименование страницы.
        /// </summary>
        /// <param name="numberSheet">Номер страницы с 0.</param>
        /// <returns>Наименование страницы, либо исключение.</returns>
        public string getTitleSheet(int numberSheet) {
            if (numberSheet >= _titleSheet.Count) {
                throw new Exception($"Попытка обратиться к странице {numberSheet + 1} при количестве страниц {_titleSheet.Count}, путь к файлу: {_pathFile}");
            }
            return _titleSheet[numberSheet];
        }

        /// <summary>
        /// Получить содержимое странциы файла.
        /// </summary>
        /// <param name="numberSheet">Номер страницы.</param>
        /// <returns>Содержимое странциы в виде матрицы ячеек, либо исключение.</returns>
        public string[,] getSheet(int numberSheet) {
            if (numberSheet >= _titleSheet.Count) {
                throw new Exception($"Попытка обратиться к странице {numberSheet + 1} при количестве страниц {_sheetsFile.Count}, путь к файлу: {_pathFile}");
            }
            return _sheetsFile[numberSheet];
        }

    }
}
