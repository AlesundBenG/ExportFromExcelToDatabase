using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; //Для работы с классом DataTable.


namespace ExportFromExcelToDatabase.Classes
{
    /// <summary>
    /// Генерация происходит на основе имеющегося SQL-запроса и прочитанных данных.
    /// Чтобы данные корректно вставились, нужно соблюдать правила:
    /// singleValue:
    ///    Оформление в запросе: #nameToken#, 
    ///    Замена: #nameToken# заменяется на valueToken.
    ///    Пример: 
    ///       До замены: DECLARE @date DATE = CONVERT(DATE, '#dateReport#') 
    ///       После замены: DECLARE @date DATE = CONVERT(DATE, '01-01-2021')   
    /// table: 
    ///    Оформление в запросе: #nameTable (#nameColume1#, #nameColume2#, ..., nameColumeN#)#
    ///    Замена: на данное место вставляются строки из таблицы в виде (valueColumn1, valueColumn2, ..., valueColumnN),
    ///    Пример:
    ///       До замены: INSERT INTO #DATA_FOR_INSERV (DATE_INFO, INFO)
    ///                     VALUES #dataForInsert (CONVERT(DATE, '#DATE_INFO#'), '#DATE_INFO#')#
    ///       После замены: INSERT INTO #DATA_FOR_INSERV (DATE_INFO, INFO)
    ///                     VALUES (CONVERT(DATE, '01-01-2021'), 'Новый год'),
    ///                            (CONVERT(DATE, '23-02-2021'), 'День защитника отечества')
    /// </summary>
    public class GeneratorSQLCommand
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Атрибуты*/

        //Символы пробелов.
        private readonly char[] symbolsSpace = new char[] { ' ', '\t' };

        public ParserResult ParserResult {
            get => default;
            set {
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Public методы*/

        /// <summary>
        /// Вставка параметров в SQL-запрос.
        /// </summary>
        /// <param name="command">SQL-запрос.</param>
        /// <param name="singleValue">Одиночные значения.</param>
        /// <param name="table">Табличные значения.</param>
        /// <returns>Запрос со вставленными данными.</returns>
        public string insertDataToCommand(string command, List<Token> singleValue, List<DataTable> table) {
            for (int i = 0; i < singleValue.Count; i++) {
                Token token = singleValue[i];
                command = command.Replace($"#{token.Name}#", token.Value);
            }
            for (int i = 0; i < table.Count; i++) {
                int startPatternTable = command.IndexOf($"<{table[i].TableName}");
                int endPatternTable = command.IndexOf('>', startPatternTable + 1);
                string patternTable = command.Substring(startPatternTable, endPatternTable - startPatternTable + 1);
                int startPatternValues = -1;
                int endPatternValues = -1;
                for (int position = startPatternTable; position < endPatternTable; position++) {
                    if ((command[position] == '(') && (startPatternValues == -1)) {
                        startPatternValues = position;
                        break;
                    }
                }
                for (int position = endPatternTable; position > startPatternValues; position--) {
                    if ((command[position] == ')') && (endPatternValues == -1)) {
                        endPatternValues = position;
                        break;
                    }
                }
                string patternValues = command.Substring(startPatternValues, endPatternValues - startPatternValues + 1);
                string values = getValuesTable(patternValues, table[i]);
                command = command.Replace(patternTable, values);
            }
            return command;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Private методы*/

        /// <summary>
        /// Получить значения таблицы в виде строки типа: (Значения столбцов 1 строки), (Значения столбцов 2 строки), ... (Значения столбцов N строки)
        /// </summary>
        /// <param name="pattern">Шаблон строк для вставки: (#nameColume1#, #nameColume2#, ..., nameColumeN#)</param>
        /// <param name="table">Таблица, из которой берутся значения.</param>
        /// <returns>Строка типа: (Значения столбцов 1 строки), (Значения столбцов 2 строки), ... (Значения столбцов N строки)</returns>
        private string getValuesTable(string pattern, DataTable table) {
            string allValues = "";
            for (int i = 0; i < table.Rows.Count; i++) {
                string rowValue = pattern;
                for (int j = 0; j < table.Columns.Count; j++) {
                    string columnName = table.Columns[j].ColumnName;
                    rowValue = rowValue.Replace($"#{columnName}#", table.Rows[i][j].ToString());
                }
                if (i != 0) {
                    allValues = allValues + ", ";
                }
                allValues = allValues + rowValue + '\n';
            }
            return allValues;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
