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
    public class Token: ICloneable
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
        /// Имя токена.
        /// </summary>
        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }
        /// <summary>
        /// Значение токена.
        /// </summary>
        public string Value {
            get {
                return _value;
            }
            set {
                _value = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Public методы*/

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Token() {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя токена.</param>
        /// <param name="value">Значение токена.</param>
        public Token(string name, string value) {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Получить копию объекта.
        /// </summary>
        /// <returns>Копия объекта.</returns>
        public object Clone() {
            return new Token(_name, _value);
        }
    }
}
