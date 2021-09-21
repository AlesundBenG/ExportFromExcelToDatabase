using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportFromExcelToDatabase.Classes
{
    public class Token
    {
        private string _name;
        private string _value;

        /// <summary>
        /// Remark: Null replaced on empty string
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
        /// Remark: Null replaced on empty string
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
