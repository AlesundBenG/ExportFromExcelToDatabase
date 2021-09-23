using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportFromExcelToDatabase.Classes
{
    /// <summary>
    /// Простое представление значений (Имя - значение).
    /// </summary>
    public class Token
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Атрибуты*/
        
        /// <summary>
        /// Имя токена.
        /// </summary>
        private string _name;
        /// <summary>
        /// Значение токена.
        /// </summary>
        private string _value;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Свойства*/

        /// <summary>
        /// Имя токена. Примечание: NULL заменяется на пустую строку.
        /// </summary>
        public string Name {
            get {
                return _name;
            }
            set {
                _name = value ?? "";
            }
        }
        /// <summary>
        /// Значение токена. Примечание: NULL заменяется на пустую строку.
        /// </summary>
        public string Value {
            get {
                return _value;
            }
            set {
                _value = value ?? "";
            }
        }
    }
}
