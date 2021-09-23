using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; //Для работы с классом DataTable.

namespace ExportFromExcelToDatabase.Classes
{
    /// <summary>
    /// Результат работы парсера - Список одиночных значений и список таблиц.
    /// </summary>
    public struct ParserResult
    {
        /// <summary>
        /// Список одиночных значений.
        /// </summary>
        public List<Token> singleValue;
        /// <summary>
        /// Список таблиц.
        /// </summary>
        public List<DataTable> table;
    }
}
