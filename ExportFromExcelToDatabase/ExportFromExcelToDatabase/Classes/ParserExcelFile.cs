using System;
using System.Collections.Generic;
using System.Data; //Для использования класса DataTable.
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportFromExcelToDatabase.Classes
{
    /// <summary>
    /// Класс для извлечения данных из Excel файла, представленного списком матриц ячеек.
    /// Парсинг осуществляется на основе дескриптора, в котором хранится информация о извлекаемых данных.
    /// Тег <singleValue>:
    /// Описание: 
    ///    информация об одиночных данных (в одном поле).
    /// Атрибуты тега:
    ///    SHEET_NUMBER: номер страницы, отсчет с единицы (Если не указано, то номер не учиытвается)
    ///    SHEET_NAME: название странциы (Если не указано, то имя не учиытвается)
    ///    SECTION_NAME: раздел на странице (для уточнения, если несколько полей с одним и тем же текстом)
    ///    SECTION_BOTTOM_LEFT: (1 - поиск в левой нижней части от раздела)
    ///    SECTION_BOTTOM_RIGHT: (1 - поиск в правой нижней части от раздела)
    ///    SECTION_UP_LEFT: (1 - поиск в левой верхней части от раздела)
    ///    SECTION_UP_RIGHT: (1 - поиск в правой верхней части от раздела)
    ///    FIELD: текст ячейки, относительно которой ищется значение.
    ///    CODE: условный код этого значения (для идентификации этого значения).
    ///    OFFEST_ROW: смещение по строке относительно ячейки с текстом (начальное значение 0)
    ///    OFFEST_COLUMN: смещение по столбцу относительно ячейки с текстом (начальное значение 0)
    /// Тег <table>:
    /// Описание: 
    ///    информация о таблице.
    /// Атрибуты тега: 
    ///    SHEET_NUMBER: номер страницы (Если не указано, то номер не учиытвается)
    ///    SHEET_NAME: название странциы (Если не указано, то имя не учиытвается)
    ///    SECTION_NAME: раздел на странице (для уточнения, если несколько таблиц с одними и теми же столбцами)
    ///    SECTION_BOTTOM_LEFT: (1 - поиск в левой нижней части от раздела)
    ///    SECTION_BOTTOM_RIGHT: (1 - поиск в правой нижней части от раздела)
    ///    SECTION_UP_LEFT: (1 - поиск в левой верхней части от раздела)
    ///    SECTION_UP_RIGHT: (1 - поиск в правой верхней части от раздела)
    ///    CODE: условный код этой таблицы (для идентификации этой таблицы).
    /// Тег <column>:
    /// Описание: 
    ///    является вложенным в тег <table> и является информацией о столбце таблицы.
    /// Атрибуты тега: 
    ///    NAME: Текст столбца.
    ///    CODE: условный код этого столбца (для идентификации этого столбца).
    ///    FINAL_CELL: последняя строка таблицы (Если не указана, то любой текст является сигналом конца таблицы)  
    ///    P.S. Последняя строка - когда одновременн все столбцы соответствуют своим FINAL_CELL. 
    /// </summary>
    public class ParserExcelFile
    {
        /// <summary>
        /// Координаты места.
        /// </summary>
        private struct CoordinatesPlace
        {
            public int lineFrom, columnFrom, lineTo, columnTo;
        }

        /// <summary>
        /// Получить данные из файла по дескриптору.
        /// </summary>
        /// <param name="descriptors">Дескриптор.</param>
        /// <param name="file">Файл, представленный списком страниц в виде матрицы ячеек.</param>
        /// <returns>Список одиночных значений и список таблиц.</returns>
        public ParserResult parser(List<DescriptorObject> descriptors, ExcelFile file) {
            ParserResult result = new ParserResult {
                singleValue = new List<Token>(),
                table = new List<DataTable>()
            };
            for (int i = 0; i < descriptors.Count; i++) {
                switch(descriptors[i].NameObject) {
                    case "singleValue": 
                        result.singleValue.Add(getSingleValue(descriptors[i], file)); 
                        break;
                    case "table": 
                        result.table.Add(getTable(descriptors[i], file)); 
                        break;
                    default:
                        throw new Exception($"ParserExcelFile: Встречен неизвестный тег: {descriptors[i].NameObject}.");
                }
            }
            return result;
        }


        /// <summary>
        /// Получить значение из файла по дескриптору.
        /// </summary>
        /// <param name="descriptor">Дескриптор.</param>
        /// <param name="file">Файл.</param>
        /// <returns>Токен - код и значение. NULL если ничего не найдено.</returns>
        public Token getSingleValue(DescriptorObject descriptor, ExcelFile file) {
            List<string[,]> sheetForSearch = getSheetForSearch(descriptor, file);
            string FIELD = descriptor.getValueToken("FIELD");                     
            for (int iSheet = 0; iSheet < sheetForSearch.Count; iSheet++) {
                CoordinatesPlace coordinatesPlace = getCoordinatesPlaceInSneet(descriptor, sheetForSearch[iSheet]); //Место для поиска относительно параметров дескриптора.
                for (int iLine = coordinatesPlace.lineFrom; iLine < coordinatesPlace.lineTo; iLine++) {
                    for (int iColumn = coordinatesPlace.columnFrom; iColumn < coordinatesPlace.columnTo; iColumn++) {
                        if (sheetForSearch[iSheet][iLine, iColumn] == FIELD) {
                            int OFFEST_ROW = ((descriptor.getValueToken("OFFEST_ROW") ?? "0") == "0") ? 0 : Convert.ToInt32(descriptor.getValueToken("OFFEST_ROW"));          //Смещение значения относительно поля.
                            int OFFEST_COLUMN = ((descriptor.getValueToken("OFFEST_COLUMN") ?? "0") == "0") ? 0 : Convert.ToInt32(descriptor.getValueToken("OFFEST_COLUMN")); //Смещение значения относительно поля.
                            return new Token() {
                                Name = descriptor.getValueToken("CODE"),
                                Value = sheetForSearch[iSheet][iLine + OFFEST_ROW, iColumn + OFFEST_COLUMN]
                            };
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Получить таблицу из файла по дескриптору.
        /// </summary>
        /// <param name="descriptor">Дескриптор.</param>
        /// <param name="file">Файл.</param>
        /// <returns>Таблица.</returns>
        public DataTable getTable(DescriptorObject descriptor, ExcelFile file) {
            List<string[,]> sheetForSearch = getSheetForSearch(descriptor, file);
            for (int i = 0; i < sheetForSearch.Count; i++) {
                CoordinatesPlace coordinates = getCoordinatesPlaceInSneet(descriptor, sheetForSearch[i]);
                List<string> nameColumns = new List<string>();
                List<string> finalLine = new List<string>();
                for (int iColumn = 0; iColumn < descriptor.CountNestedObject; iColumn++) {
                    DescriptorObject column = descriptor.getNestedObject(iColumn);
                    nameColumns.Add(column.getValueToken("NAME"));
                    finalLine.Add(column.getValueToken("FINAL_CELL"));
                }
                for (int iLine = coordinates.lineFrom; iLine < coordinates.lineTo; iLine++) {
                    for (int iColumn = coordinates.columnFrom; iColumn < coordinates.columnTo; iColumn++) {
                        if (nameColumns[0] == sheetForSearch[i][iLine, iColumn]) {
                            List<int> indexColumn = getIndexColumn(nameColumns, sheetForSearch[i], iLine);
                            if (indexColumn != null) {
                                int finalLineIndex = getIndexFinalLine(iLine, indexColumn, finalLine, sheetForSearch[i]);
                                return fillTable(iLine, finalLineIndex, sheetForSearch[i], nameColumns, indexColumn);
                            }
                        }
                    }
                }
            }
            return null;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Privat методы*/

        private DataTable fillTable(int headerLine, int finalLine, string[,] sheet, List<string> nameColumn, List<int> indexColumn) {
            DataTable table = new DataTable();
            for (int i = 0; i < nameColumn.Count; i++) {
                table.Columns.Add(nameColumn[i], typeof(string));
            }
            for (int iLine = headerLine + 1; iLine < finalLine; iLine++) {
                DataRow line = table.NewRow();
                for (int column = 0; column < indexColumn.Count; column++) {
                    line[column] = sheet[iLine, indexColumn[column]];
                }
                table.Rows.Add(line);
            }
            return table;
        }

        /// <summary>
        /// Получить листы для поиска.
        /// </summary>
        /// <param name="descriptor">Дескриптор объекта.</param>
        /// <param name="file">Страницы файла.</param>
        /// <returns>Страницы для поиска.</returns>
        private List<string[,]> getSheetForSearch(DescriptorObject descriptor, ExcelFile file) {
            string SHEET_NUMBER = descriptor.getValueToken("SHEET_NUMBER");
            string SHEET_NAME = descriptor.getValueToken("SHEET_NAME");
            List<string[,]> sheet = new List<string[,]>(); //Отбор листов для поиска.
            //Не важно, на каких листах.
            if ((SHEET_NUMBER == null) && (SHEET_NAME == null)) { //Не важно, на каких листах.
                for (int i = 0; i < file.CountSheet; i++) {
                    sheet.Add(file.getSheet(i));
                }
                return sheet;
            }
            //Важно на какой странице и известен номер.
            if (SHEET_NUMBER != null) {
                int sheetNumber = Convert.ToInt32(SHEET_NUMBER);
                if (sheetNumber > sheet.Count) {
                    throw new Exception($"ParserExcelFile: Номер странциы у {descriptor.NameObject} с кодом {descriptor.getValueToken("CODE")} превосходит количество страниц в файле.");
                }
                if (sheetNumber == 0) {
                    throw new Exception($"ParserExcelFile: Номер странциы у {descriptor.NameObject} с кодом {descriptor.getValueToken("CODE")} равен 0, но счет страниц осуществляется с 1");
                }
                sheet.Add(file.getSheet(sheetNumber));
                return sheet;
            }
            //Важно на какой странице и известно имя.
            if (SHEET_NAME != null) {
                for (int i = 0; i < sheet.Count; i++) {
                    if (SHEET_NAME == file.getTitleSheet(i)) {
                        sheet.Add(file.getSheet(i));
                        return sheet;
                    }
                }
                throw new Exception($"ParserExcelFile: Не найдена страница с именем {SHEET_NAME} для {descriptor.NameObject} с кодом {descriptor.getValueToken("CODE")}");
            }
            throw new Exception("ParserExcelFile: не предусмотрен вариант при выборе страниц для поиска");
        }

        /// <summary>
        /// Получение места для поиска.
        /// </summary>
        /// <param name="descriptor">Дескриптор объекта.</param>
        /// <param name="sheet">Страница.</param>
        /// <returns>Координаты места. Если все координаты 0, то не найдена секция, если она нужна.</returns>
        private CoordinatesPlace getCoordinatesPlaceInSneet(DescriptorObject descriptor, string[,] sheet) {
            CoordinatesPlace coordinates = new CoordinatesPlace() {lineFrom = 0, columnFrom = 0, lineTo = 0, columnTo = 0};
            //Проверка наличия секции для поиска.
            string SECTION_NAME = descriptor.getValueToken("SECTION_NAME");
            if (SECTION_NAME == null) {
                coordinates.lineTo = sheet.GetLength(0);
                coordinates.columnTo = sheet.GetLength(1);
                return coordinates;
            }
            //Поиск самой секции.
            bool found = false;
            int tileSectionLine = 0, titleSectionColumn = 0;
            for (int i = 0; i < sheet.GetLength(0); i++) {
                for (int j = 0; j < sheet.GetLength(1); j++) {
                    if (sheet[i, j] == SECTION_NAME) {
                        found = true;
                        coordinates.lineFrom = i;
                        coordinates.lineTo = i;
                        coordinates.columnFrom = j;
                        coordinates.columnTo = j;
                        break;
                    }
                }
                if (found) {
                    break;
                }
            }
            if (!found) {
                return coordinates; //Все значения 0.
            }
            //Высчитывание области относительно флагов.
            bool SECTION_BOTTOM_LEFT    = ((descriptor.getValueToken("SECTION_BOTTOM_LEFT") ?? "0") == "0") ? false : true;
            bool SECTION_BOTTOM_RIGHT   = ((descriptor.getValueToken("SECTION_BOTTOM_RIGHT") ?? "0") == "0") ? false : true;
            bool SECTION_UP_LEFT        = ((descriptor.getValueToken("SECTION_UP_LEFT") ?? "0") == "0") ? false : true;
            bool SECTION_UP_RIGHT       = ((descriptor.getValueToken("SECTION_UP_RIGHT") ?? "0") == "0") ? false : true;
            if (SECTION_BOTTOM_LEFT) {
                coordinates.lineTo = sheet.GetLength(0);
                coordinates.columnFrom = 0;
            }
            if (SECTION_BOTTOM_RIGHT) {
                coordinates.lineTo = sheet.GetLength(0);
                coordinates.columnTo = sheet.GetLength(1);
            }
            if (SECTION_UP_LEFT) {
                coordinates.lineFrom = 0;
                coordinates.columnFrom = 0;
            }
            if (SECTION_UP_RIGHT) {
                coordinates.lineFrom = 0;
                coordinates.columnTo = sheet.GetLength(1);
            }
            return coordinates;
        }

        /// <summary>
        /// Получение координат таблицы.
        /// </summary>
        /// <param name="descriptor">Дескриптор объекта.</param>
        /// <param name="sheet">Страница.</param>
        /// <returns>Координаты таблицы. Если все координаты 0, то не найдена таблица.</returns>
        private CoordinatesPlace getCoordinatesTable(DescriptorObject descriptor, string[,] sheet) {
            //Получение названия столбцов.
            List<string> nameColumns = new List<string>();
            List<string> finalLine= new List<string>();
            for (int i = 0; i < descriptor.CountNestedObject; i++) {
                DescriptorObject column = descriptor.getNestedObject(i);
                nameColumns.Add(column.getValueToken("NAME"));
                finalLine.Add(column.getValueToken("FINAL_CELL"));
            }
            //Если нет столбцов.
            if (nameColumns.Count == 0) {
                return new CoordinatesPlace() { lineFrom = 0, columnFrom = 0, lineTo = 0, columnTo = 0 };
            }
            CoordinatesPlace coordinates = getCoordinatesPlaceInSneet(descriptor, sheet);

            for (int iLine = coordinates.lineFrom; iLine < coordinates.lineTo; iLine++) {
                for (int iColumn = coordinates.columnFrom; iColumn < coordinates.columnTo; iColumn++) {
                    if (nameColumns[0] == sheet[iLine, iColumn]) {
                        List<int> indexColumn = getIndexColumn(nameColumns, sheet, iLine);
                        if (indexColumn != null) {
                            int finalLineIndex = getIndexFinalLine(iLine, indexColumn, finalLine, sheet);
                        }
                    }
                }
            }
            return coordinates;
        }

        /// <summary>
        /// Каждому столбцу из списка подставляется номер столбца на листе.
        /// </summary>
        /// <param name="nameColumns">Названия столбцов для поиска.</param>
        /// <param name="sheet">Страница.</param>
        /// <param name="iLine">Строка, в которой ищутся названия столбцов.</param>
        /// <returns>Список столбцов на листе, соответствующий названиям столбцов. NULL, если не найдены все столбцы.</returns>
        private List<int> getIndexColumn(List<string> nameColumns, string[,] sheet, int iLine) {
            List<int> indexColumn = new List<int>();
            for (int i = 0; i < nameColumns.Count; i++) {
                bool found = false;
                for (int iColumn = 0; iColumn < sheet.GetLength(1); iColumn++) {
                    if (nameColumns[i] == sheet[iLine, iColumn]) {
                        found = true;
                        indexColumn.Add(iColumn);
                        break;
                    }
                }
                if (!found) {
                    return null;
                }
            }
            return indexColumn;
        }

        private int getIndexFinalLine(int headerLine, List<int> indexColumn, List<string> finalLine, string[,] sheet) {
            for (int i = headerLine; i < sheet.GetLength(0); i++) {
                bool fullEqually = true;
                for (int j = 0; j < indexColumn.Count; j++) {
                    if (finalLine[j] != null) {
                        if (sheet[i, indexColumn[j]] != finalLine[j]) {
                            fullEqually = false;
                            break;
                        }
                    }
                }
                if (fullEqually) {
                    return i;
                }
            }
            return sheet.GetLength(0);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
