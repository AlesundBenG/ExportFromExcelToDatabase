using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; //Для работы с классом DataTable.


namespace ExportFromExcelToDatabase.Classes
{
    /// <summary>
    /// Генератор SQL-запросов на основе файла и прочитанных данных.
    /// </summary>
    public class GeneratorSQLCommand
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Атрибуты*/

        //Символы пробелов.
        private readonly char[] symbolsSpace = new char[] { ' ', '\t' };


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

            }
            return command;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Private методы*/



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
