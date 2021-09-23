﻿using System;
using System.Collections.Generic;
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
    ///    SECTION_BOTTOM: (1 - поиск от раздела до нижнего края, 0 - поиск с верхнего края до нижнего края, изначально 0)
    ///    SECTION_UP: (1 - поиск с верхнего края и до раздела, 0 - поиск с верхнего края до нижнего края, изначально 0)
    ///    SECTION_LEFT: (1 - поиск с левого края и до раздела, 0 - поиск с левого края до правого края, изначально 0)
    ///    SECTION_RIGHT: (1 - поиск от раздела и до правого края, 0 - поиск с левого края до правого края, изначально 0)
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
    ///    SECTION_BOTTOM: (1 - поиск от раздела до нижнего края, 0 - поиск с верхнего края до нижнего края, изначально 0)
    ///    SECTION_UP: (1 - поиск с верхнего края и до раздела, 0 - поиск с верхнего края до нижнего края, изначально 0)
    ///    SECTION_LEFT: (1 - поиск с левого края и до раздела, 0 - поиск с левого края до правого края, изначально 0)
    ///    SECTION_RIGHT: (1 - поиск от раздела и до правого края, 0 - поиск с левого края до правого края, изначально 0)
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

         public void parser(List<DescriptorObject> descriptors, ExcelFile file) {
            for (int i = 0; i < descriptors.Count; i++) {
                if (descriptors[i].NameObject == "singleValue") {

                }
                else if (descriptors[i].NameObject == "table") {

                }
            }
        }


        /// <summary>
        /// Получить из списка токенов значение токена по имени.
        /// </summary>
        /// <param name="tokens">Список токенов.</param>
        /// <param name="nameToken">Имя токена.</param>
        /// <returns>Значение токена. NULl, если токен с таким именем не найден.</returns>
        private string getValueToken(DescriptorObject descriptor, string nameToken) {
            for (int i = 0; i < descriptor.CountToken; i++) {
                if (descriptor.getToken(i).Name == nameToken) {
                    return descriptor.getToken(i).Value;
                }
            }
            return null;
        }

        private string getSingleValue(DescriptorObject descriptor, ExcelFile file) {
            List<string[,]> sheetForSearch = getSheetForSearch(descriptor, file);
            /*
            string SECTION_NAME = getValueToken(descriptor, "SECTION_NAME");
            bool SECTION_BOTTOM = ((getValueToken(descriptor, "SECTION_BOTTOM") ?? "0") == "0") ? false : true;
            bool SECTION_UP = ((getValueToken(descriptor, "SECTION_UP") ?? "0") == "0") ? false : true;
            bool SECTION_LEFT = ((getValueToken(descriptor, "SECTION_LEFT") ?? "0") == "0") ? false : true;
            bool SECTION_RIGHT = ((getValueToken(descriptor, "SECTION_RIGHT") ?? "0") == "0") ? false : true;
            string FIELD = getValueToken(descriptor, "FIELD");
            string CODE = getValueToken(descriptor, "CODE");
            int OFFEST_ROW = ((getValueToken(descriptor, "OFFEST_ROW") ?? "0") == "0") ? 0 : Convert.ToInt32(getValueToken(descriptor, "OFFEST_ROW"));
            int OFFEST_COLUMN = ((getValueToken(descriptor, "OFFEST_COLUMN") ?? "0") == "0") ? 0 : Convert.ToInt32(getValueToken(descriptor, "OFFEST_COLUMN"));
            */
        }

        /// <summary>
        /// Получить листы для поиска.
        /// </summary>
        /// <param name="descriptor">Дескриптор объекта.</param>
        /// <param name="file">Страницы файла.</param>
        /// <returns>Страницы для поиска.</returns>
        private List<string[,]> getSheetForSearch(DescriptorObject descriptor, ExcelFile file) {
            string SHEET_NUMBER = getValueToken(descriptor, "SHEET_NUMBER");
            string SHEET_NAME = getValueToken(descriptor, "SHEET_NAME");
            //Отбор листов для поиска.
            List<string[,]> sheet = new List<string[,]>();
            //Не важно, на каких листах.
            if ((SHEET_NUMBER == null) && (SHEET_NAME == null)) {
                for (int i = 0; i < file.CountSheet; i++) {
                    sheet.Add(file.getSheet(i));
                }
                return sheet;
            }
            //Важно на какой странице и известен номер.
            if (SHEET_NUMBER != null) {
                int sheetNumber = Convert.ToInt32(SHEET_NUMBER);
                if (sheetNumber > sheet.Count) {
                    throw new Exception($"ParserExcelFile: Номер странциы у {descriptor.NameObject} с кодом {getValueToken(descriptor, "CODE")} превосходит количество страниц в файле.");
                }
                if (sheetNumber == 0) {
                    throw new Exception($"ParserExcelFile: Номер странциы у {descriptor.NameObject} с кодом {getValueToken(descriptor, "CODE")} равен 0, но счет страниц осуществляется с 1");
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
                throw new Exception($"ParserExcelFile: Не найдена страница с именем {SHEET_NAME} для {descriptor.NameObject} с кодом {getValueToken(descriptor, "CODE")});
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
            //Проверка наличия секции для поиска.
            string SECTION_NAME = getValueToken(descriptor, "SECTION_NAME");
            if (SECTION_NAME == null) {
                return new CoordinatesPlace() {
                    lineFrom = 0, columnFrom = 0, lineTo = sheet.GetLength(0), columnTo = sheet.GetLength(1),
                };
            }
            //Поиск самой секции.
            CoordinatesPlace coordinateSection = new CoordinatesPlace();
            bool found = false;
            for (int i = 0; i < sheet.GetLength(0); i++) {
                for (int j = 0; j < sheet.GetLength(1); j++) {
                    if (sheet[i, j] == SECTION_NAME) {
                        found = true;
                        coordinateSection.lineFrom = i;
                        coordinateSection.columnFrom = j;
                        break;
                    }
                }
                if (found) {
                    break;
                }
            }
            if (!found) {
                return new CoordinatesPlace() {
                    lineFrom = 0, columnFrom = 0, lineTo = 0, columnTo = 0,
                };
            }
            //Высчитывание области относительно флагов.
            bool SECTION_BOTTOM = ((getValueToken(descriptor, "SECTION_BOTTOM") ?? "0") == "0") ? false : true;
            bool SECTION_UP = ((getValueToken(descriptor, "SECTION_UP") ?? "0") == "0") ? false : true;
            bool SECTION_LEFT = ((getValueToken(descriptor, "SECTION_LEFT") ?? "0") == "0") ? false : true;
            bool SECTION_RIGHT = ((getValueToken(descriptor, "SECTION_RIGHT") ?? "0") == "0") ? false : true;

        }
    }
}