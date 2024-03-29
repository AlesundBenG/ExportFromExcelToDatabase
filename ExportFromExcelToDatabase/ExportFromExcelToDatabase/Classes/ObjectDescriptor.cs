﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportFromExcelToDatabase.Classes
{
    /// <summary>
    /// Описание объекта для парсера.
    /// </summary>
    public class ObjectDescriptor
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Атрибуты*/

        /// <summary>
        /// Имя объекта (Тег).
        /// </summary>
        private string _name;
        /// <summary>
        /// Список атрибутов объекта.
        /// </summary>
        private List<Token> _listToken;
        /// <summary>
        /// Список вложенных объектов в данный объект (Например <column> в <table>).
        /// </summary>
        private List<ObjectDescriptor> _listNestedObject;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Свойства*/

        /// <summary>
        /// Имя объекта (Тег).
        /// </summary>
        public string NameObject {
            get {
                return _name;
            }
            set {
                _name = value;
            }

        }

        /// <summary>
        /// Количество атрибутов объекта.
        /// </summary>
        public int CountToken {
            get { return _listToken.Count; }
        }

        /// <summary>
        /// Количество вложенных объектов в данный объект.
        /// </summary>
        public int CountNestedObject {
            get { return _listNestedObject.Count; }
        }

        public Token Token {
            get => default;
            set {
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Public методы*/

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ObjectDescriptor() {
            _listToken = new List<Token>();
            _listNestedObject = new List<ObjectDescriptor>();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ObjectDescriptor(string nameObject) {
            this.NameObject = nameObject;
            _listToken = new List<Token>();
            _listNestedObject = new List<ObjectDescriptor>();
        }

        /// <summary>
        /// Добавление атрибута.
        /// </summary>
        /// <param name="token">Атрибут.</param>
        public void addToken(Token token) {
            _listToken.Add(token);
        }

        /// <summary>
        /// Удаление Атрибута.
        /// </summary>
        /// <param name="index">Индекс атрибута в списке.</param>
        public void deleteToken(int index) {
            if (_listToken.Count > index) {
                _listToken.RemoveAt(index);
            }
        }

        /// <summary>
        /// Удаление атрибута.
        /// </summary>
        /// <param name="token">Удаляемый атрибут.</param>
        public void deleteToken(Token token) {
            _listToken.Remove(token);
        }

        /// <summary>
        /// Получить атрибут.
        /// </summary>
        /// <param name="index">Индекс атрибута.</param>
        /// <returns>Атрибут, либо исключение при выходе за границы списка.</returns>
        public Token getToken(int index) {
            if (_listToken.Count > index) {
                return _listToken[index];
            }
            else {
                throw new ArgumentException($"Class DescriptorObject: попытка получить {index} атрибут объекта {_name}, когда количество атрибутов {_listToken.Count}");
            }
        }

        /// <summary>
        /// Добавление вложенного объекта.
        /// </summary>
        /// <param name="nestedObject">Объект, который вложен в текущий.</param>
        public void addNestedObject(ObjectDescriptor nestedObject) {
            _listNestedObject.Add(nestedObject);
        }

        /// <summary>
        /// Удаление вложенного объекта.
        /// </summary>
        /// <param name="index">Индекс вложенного объекта в списке.</param>
        public void deleteNestedObject(int index) {
            if (_listNestedObject.Count > index) {
                _listNestedObject.RemoveAt(index);
            }
        }

        /// <summary>
        /// Получение вложенного объекта в текущий объект.
        /// </summary>
        /// <param name="index">Индекс вложенного объекта.</param>
        /// <returns>Вложенный объект, либо исключение при выходе за границы списка.</returns>
        public ObjectDescriptor getNestedObject(int index) {
            if (_listNestedObject.Count > index) {
                return _listNestedObject[index];
            }
            else {
                throw new ArgumentException($"Class DescriptorObject: попытка получить {index} вложенный объект объекта {_name}, когда количество объектов {_listNestedObject.Count}");
            }
        }

        /// <summary>
        /// Получить значение атбриута объекта по коду атрибута (без учета регистра).
        /// </summary>
        /// <param name="nameToken">Имя атрибута.</param>
        /// <returns>Значение атрибута, либо NULl, если такого атрибута нет.</returns>
        public string getValueToken(string nameToken) {
            for (int i = 0; i < _listToken.Count; i++) {
                if (nameToken.Equals(_listToken[i].Name, StringComparison.OrdinalIgnoreCase)) {
                    return _listToken[i].Value;
                }
            }
            return null;
        }
    }
}
