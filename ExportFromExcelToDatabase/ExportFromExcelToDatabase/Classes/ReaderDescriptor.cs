using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportFromExcelToDatabase.Classes
{
    /// <summary>
    /// Класс для чтения дескриптора Excel-файла.
    /// </summary>
    public class ReaderDescriptor
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Атрибуты*/

        //Символы пробелов.
        private readonly char[] symbolsSpace = new char[] { ' ', '\t' };


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Public методы*/


        /// <summary>
        /// Получить список дескрипторов объектов из строки.
        /// </summary>
        /// <param name="descriptor">Строка, в которой содержатся дескрипторы объектов</param>
        /// <returns>Список дескипторов объектов</returns>
        public List<DescriptorObject> getListDescriptors(string descriptorText) {
            List<DescriptorObject> listDescriptors = new List<DescriptorObject>();
            int currentPosition = 0;
            while (currentPosition < descriptorText.Length) {
                currentPosition = goWhileMeetThoseSymbols(descriptorText, currentPosition, symbolsSpace);
                if (currentPosition == -1) {
                    break;
                }
                string descriptorObjectText = getObjectPath(descriptorText, currentPosition);
                DescriptorObject descriptorObject = getDescriptorObject(descriptorObjectText);
                listDescriptors.Add(descriptorObject);
                currentPosition = currentPosition + descriptorObjectText.Length;
            }
            return listDescriptors;
        }

        /// <summary>
        /// Сформировать дескриптор объекта из строки, которая является дескриптором в символьном представлении.
        /// </summary>
        /// <param name="objectsPart">Символьное представление дескриптора объекта, которое имеет следующий шаблон: <nameTag>...Tokens...</nameTag>.</param>
        /// <returns>Дескриптор объекта, сформированный из символьного представления</returns>
        private DescriptorObject getDescriptorObject(string objectsPart) {
            DescriptorObject descriptor = new DescriptorObject {
                NameObject = objectsPart.Substring(1, objectsPart.IndexOf('>') - 1)
            };
            string clouseTag = $"</{descriptor.NameObject}>";
            //string objectsPartWithoutTag = objectsPart.Replace($"<{descriptor.NameObject}>", "").Replace($"</{descriptor.NameObject}>", "");
            //Позиция значащих символов.
            int currentPosition = goWhileMeetThoseSymbols(objectsPart, descriptor.NameObject.Length + 2, symbolsSpace);
            while (currentPosition < objectsPart.Length) {
                currentPosition = goWhileMeetThoseSymbols(objectsPart, currentPosition, symbolsSpace);
                if (currentPosition == -1) {
                    break;
                }
                //Тег.
                if (objectsPart[currentPosition] == '<') {
                    //Конец.
                    if (objectsPart.Substring(currentPosition, descriptor.NameObject.Length + 3) == clouseTag) {
                        return descriptor;
                    }
                    else {
                        string nestedObjectText = getObjectPath(objectsPart, currentPosition);
                        DescriptorObject nestedObject = getDescriptorObject(nestedObjectText);
                        descriptor.addNestedObject(nestedObject);
                        currentPosition = currentPosition + nestedObjectText.Length;
                    }
                }
                //Токен.
                else {
                    string tokenPath = getTokenPath(objectsPart, currentPosition);
                    Token tokenObject = getToken(tokenPath);
                    descriptor.addToken(tokenObject);
                    currentPosition = currentPosition + tokenPath.Length;
                }
            }
            return descriptor;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Получение токена из строки определенного паттерна.
        /// </summary>
        /// <param name="pathToken">Строка с паттерном: Имя токена : значение; Имя и значение без пробелов, иначе нужно заключать в кавычки.</param>
        /// <returns>Токен: Имя и значение.</returns>
        private Token getToken(string pathToken) {
            string name, value; //Получаемые значения.
            int startPosition, endPosition; //Вспомогательные указатели.
            char[] symbolsBetweenNameAndValue = new char[] { ' ', ':', '\t' }; //Символы, которые могут встретиться между именем токена и значением.
            //Проход до значащих символов.
            startPosition = goWhileMeetThoseSymbols(pathToken, 0, symbolsSpace);
            //Имя токена одним словом.
            if (pathToken[startPosition] != '\"') {
                endPosition = goWhileNotMeetThoseSymbols(pathToken, startPosition, symbolsBetweenNameAndValue);
                name = pathToken.Substring(startPosition, endPosition - startPosition);
            }
            //Имя токена в несколько слов.
            else {
                name = getStringBetweenQuotation(pathToken, startPosition);
                endPosition = startPosition + name.Length + 2; //+2 Это квычки.
            }
            //Проход до значащих символов после имени токена.
            startPosition = endPosition;
            startPosition = goWhileMeetThoseSymbols(pathToken, startPosition, symbolsBetweenNameAndValue);
            //Значение токена одним словом.
            if (pathToken[startPosition] != '\"') {
                endPosition = goWhileNotMeetThoseSymbols(pathToken, startPosition, symbolsSpace);
                value = pathToken.Substring(startPosition, endPosition - startPosition + 1);
            }
            //Имя токена в несколько слов.
            else {
                value = getStringBetweenQuotation(pathToken, startPosition);
            }
            return new Token() {
                Name = name,
                Value = value
            };
        }

        /// <summary>
        /// Идти по строке, пока встречаются указанные символы.
        /// </summary>
        /// <param name="text">Исходная строка</param>
        /// <param name="startPosition">Начальна позиция</param>
        /// <param name="symbol">Массив символов</param>
        /// <returns>Позиция, на которой встречен иной символ. Если дошли до конца строки, то  -1</returns>
        private int goWhileMeetThoseSymbols(string text, int startPosition, char[] symbol) {
            int currentPosition = startPosition;
            bool foundSymbol;
            while (currentPosition < text.Length) {
                foundSymbol = false;
                for (int i = 0; i < symbol.Length; i++) {
                    if (text[currentPosition] == symbol[i]) {
                        currentPosition++;
                        foundSymbol = true;
                        break;
                    }
                }
                if (!foundSymbol) {
                    return currentPosition;
                }
            }
            return -1;
        }

        /// <summary>
        /// Идти по строке, пока не встретются указанные символы.
        /// </summary>
        /// <param name="text">Исходная строка</param>
        /// <param name="startPosition">Начальна позиция</param>
        /// <param name="symbol">Массив символов</param>
        /// <returns>Позиция, на которой встречен один из указанных символов. Если дошли до конца строки, то  -1</returns>
        private int goWhileNotMeetThoseSymbols(string text, int startPosition, char[] symbol) {
            int currentPosition = startPosition;
            while (currentPosition < text.Length) {
                for (int i = 0; i < symbol.Length; i++) {
                    if (text[currentPosition] == symbol[i]) {
                        return currentPosition;
                    }
                }
                currentPosition++;
            }
            return -1;
        }

        /// <summary>
        /// Получить строку между кавычками, стоящими после начальной позиции.
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="startPosition">Начальная позиция</param>
        /// <returns>Строка между кавычками, либо исключение, если одна из кавычек не была найдена.</returns>
        private string getStringBetweenQuotation(string text, int startPosition) {
            int currentPosition = startPosition;
            int firstQuotationPosition = currentPosition;
            bool foundFirst = false;
            while (currentPosition < text.Length) {
                if (text[currentPosition] == '\"') {
                    if ((text[currentPosition - 1] != '\\') && (!foundFirst)) {
                        foundFirst = true;
                        firstQuotationPosition = currentPosition;
                    }
                    else if ((text[currentPosition - 1] != '\\') && (foundFirst)) {
                        return text.Substring(firstQuotationPosition + 1, currentPosition - firstQuotationPosition - 1);
                    }
                }
                currentPosition++;
            }
            if (foundFirst) {
                throw new Exception($"ReaderDescriptor: Не найдена вторая кавычка: \"{text.Substring(firstQuotationPosition, text.Length - firstQuotationPosition)}");
            }
            else {
                throw new Exception($"ReaderDescriptor: Не найдена первая кавычка: \"{text.Substring(startPosition, text.Length - startPosition)}");
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Возвращение токена в символьном представлении.
        /// </summary>
        /// <param name="objectsPath">Дескриптор объекта в символьном представлении</param>
        /// <param name="startPosition">Начальная позиция токена</param>
        /// <returns>Токен в символьном представлении</returns>
        private string getTokenPath(string objectsPath, int startPosition) {
            int currentPosition = startPosition;
            bool betweenQuotation = false;
            while (currentPosition < objectsPath.Length) {
                if (objectsPath[currentPosition] == '\"') {
                    betweenQuotation = !betweenQuotation;
                }
                else if ((objectsPath[currentPosition] == ';') && (!betweenQuotation)) {
                    return objectsPath.Substring(startPosition, currentPosition - startPosition + 1);
                }
                else if ((objectsPath[currentPosition] == '<') && (!betweenQuotation)) {
                    throw new Exception("ReaderDescriptor: Не найден символ \";\" после объявления свойства.");
                }
                currentPosition++;
            }
            throw new Exception("ReaderDescriptor: Не найден символ \";\" после объявления свойства.");
        }

        /// <summary>
        /// Возвращение объекта в символьном представлении.
        /// </summary>
        /// <param name="descriptor">Дескриптор всех объектов.</param>
        /// <param name="startPosition">Начальная позиция.</param>
        /// <returns></returns>
        private string getObjectPath(string descriptor, int startPosition) {
            int currentPosition = goWhileMeetThoseSymbols(descriptor, startPosition, symbolsSpace);
            string tag;
            //Чтение тега.
            if (descriptor[startPosition] != '<') {
                throw new Exception($"ReaderDescriptor: the \"<\" was expected but was found {descriptor[currentPosition]} (position {currentPosition})");
            }
            int endTagPosition = descriptor.IndexOf('>', startPosition);
            if (endTagPosition == -1) {
                throw new Exception($"ReaderDescriptor: не найден парный символ \">\"");
            }
            else {
                tag = descriptor.Substring(startPosition + 1, endTagPosition - startPosition - 1);
            }
            //Поиск закрывающего тега.
            endTagPosition = descriptor.IndexOf($"</{tag}>", endTagPosition + 1) + tag.Length + 3;
            if (endTagPosition == -1) {
                throw new Exception($"ReaderDescriptor: не найден закрывающий тег <\\{tag}>");
            }
            else {
                return descriptor.Substring(startPosition, endTagPosition - startPosition);
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
