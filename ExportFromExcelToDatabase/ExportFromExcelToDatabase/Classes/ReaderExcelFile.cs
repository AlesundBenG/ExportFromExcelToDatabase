using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel; //Библиотека для чтения файла.

namespace ExportFromExcelToDatabase.Classes
{
    public class ReaderExcelFile
    {
        /// <summary>
        /// Чтение файла.
        /// </summary>
        /// <param name="pathFile">Путь к файлу.</param>
        /// <returns>Список листов, представленных в виде матрицы ячеек</returns>
        public List<string[,]> readFile(string pathFile) {
            //Запуск экселя.
            Application application = new Application();
            //Класс работы с файлом.
            Workbook workBook = application.Workbooks.Open(pathFile, 0, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //Список листов.
            List<string[,]> sheetsFile = new List<string[,]>();
            //Чтение листов.
            for (int iSheet = 1; iSheet <= workBook.Sheets.Count; iSheet++) {
                //Класс работы с листами.
                Worksheet workSheet = (Worksheet)workBook.Sheets[iSheet];
                //Последняя заполненная строка в столбце А.
                int iLastRow = workSheet.Cells[workSheet.Rows.Count, "A"].End[XlDirection.xlUp].Row;
                //Чтение данных с листа.
                var arrData = (object[,])workSheet.Range["A1:J" + iLastRow].Value;
                //Преобразование данных в строку.
                string[,] sheet = new string[arrData.GetLength(0), arrData.GetLength(1)];
                for (int i = 1; i < sheet.GetLength(0); i++) {
                    for (int j = 1; j < sheet.GetLength(1); j++) {
                        sheet[i - 1, j - 1] = arrData[i, j].ToString() ?? "";
                    }
                }
                //Добавление листа в список листов.
                sheetsFile.Add(sheet);
            }
            //Закрытие файла не сохраняя.
            workBook.Close(false, Type.Missing, Type.Missing);
            //Выход из экселя.
            application.Quit();
            return sheetsFile;
        }
    }
}
