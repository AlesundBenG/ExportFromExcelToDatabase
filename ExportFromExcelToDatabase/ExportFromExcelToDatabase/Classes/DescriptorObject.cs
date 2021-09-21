using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportFromExcelToDatabase.Classes
{
    public class DescriptorObject
    {
        private string _name;
        private List<Token> _listToken;
        private List<DescriptorObject> _listNestedObject;

        /// <summary>
        /// Remark: Null replaced on empty string
        /// </summary>
        public string NameObject {
            get {
                return _name;
            }
            set {
                _name = value ?? "";
            }

        }

        public int CountToken {
            get { return _listToken.Count; }
        }

        public int CountNestedObject {
            get { return _listNestedObject.Count; }
        }

        public DescriptorObject() {
            _listToken = new List<Token>();
            _listNestedObject = new List<DescriptorObject>();
        }

        public void addToken(Token token) {
            _listToken.Add(token);
        }

        public void deleteToken(int index) {
            if (_listToken.Count > index) {
                _listToken.RemoveAt(index);
            }
        }

        public void deleteToken(Token token) {
            _listToken.Remove(token);
        }

        public Token getToken(int index) {
            if (_listToken.Count > index) {
                return _listToken[index];
            }
            else {
                throw new ArgumentException($"Class DescriptorObject: attempt to get {index}th token, when number tokens {_listToken.Count}");
            }
        }

        public void addNestedObject(DescriptorObject nestedObject) {
            _listNestedObject.Add(nestedObject);
        }

        public void deleteNestedObject(int index) {
            if (_listNestedObject.Count > index) {
                _listNestedObject.RemoveAt(index);
            }
        }

        public void deleteToken(DescriptorObject nestedObject) {
            _listNestedObject.Remove(nestedObject);
        }

        public DescriptorObject getNestedObject(int index) {
            if (_listNestedObject.Count > index) {
                return _listNestedObject[index];
            }
            else {
                throw new ArgumentException($"Class DescriptorObject: attempt to get {index}th nested object, when number objects {_listNestedObject.Count}");
            }
        }
    }
}
