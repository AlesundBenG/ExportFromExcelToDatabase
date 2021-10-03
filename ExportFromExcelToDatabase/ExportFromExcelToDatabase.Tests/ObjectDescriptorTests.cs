using ExportFromExcelToDatabase.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExportFromExcelToDatabase.Tests
{
    [TestClass]
    public class ObjectDescriptorTests
    {
        /// <summary>
        /// Инициализация токена через пустой конструктор. Не должно быть исключений при обращении к свойствам объекта.
        /// </summary>
        [TestMethod]
        public void constructorEmptyDescriptor() {
            ObjectDescriptor descriptor = new ObjectDescriptor();
            Assert.AreEqual(descriptor.NameObject, null);
            Assert.AreEqual(descriptor.CountToken, 0);
            Assert.AreEqual(descriptor.CountNestedObject, 0);
        }

        /// <summary>
        /// Инициализация токена через конструктор с установкой входных параметров (Все поля должны быть равны входным параметрам).
        /// </summary>
        [TestMethod]
        public void constructorFillDescriptor() {
            ObjectDescriptor descriptor = new ObjectDescriptor("singleValue");
            Assert.AreEqual(descriptor.NameObject, "singleValue");
            Assert.AreEqual(descriptor.CountToken, 0);
            Assert.AreEqual(descriptor.CountNestedObject, 0);
        }

        /// <summary>
        /// Добавление и обращение к токенам объекта.
        /// </summary>
        [TestMethod]
        public void addAndGetToken() {
            ObjectDescriptor descriptor = new ObjectDescriptor("singleValue");
            Assert.AreEqual(descriptor.NameObject, "singleValue");
            Token[] insertedToken = new Token[3] {
                new Token("FIELD", "имя"),
                new Token("CODE", "name"),
                new Token("OFFEST_COLUMN", "1")
            };
            for (int i = 0; i < insertedToken.Length; i++) {
                descriptor.addToken(insertedToken[i]);
            }
            Assert.AreEqual(descriptor.CountToken, 3);
            Assert.AreEqual(descriptor.CountNestedObject, 0);
            for (int i = 0; i < insertedToken.Length; i++) {
                Token receivedToken = descriptor.getToken(i);
                Assert.AreEqual(receivedToken.Name, insertedToken[i].Name);
                Assert.AreEqual(receivedToken.Value, insertedToken[i].Value);
            }
            for (int i = 0; i < insertedToken.Length; i++) {
                string lowerCaseName = insertedToken[i].Name.ToLower();
                string receivedValueToken = descriptor.getValueToken(lowerCaseName);
                Assert.AreEqual(receivedValueToken, insertedToken[i].Value);
            }
        }

        /// <summary>
        /// Добавление и удаление токенов из дескриптора.
        /// </summary>
        [TestMethod]
        public void addAndDeleteToken() {
            ObjectDescriptor descriptor = new ObjectDescriptor("singleValue");
            Assert.AreEqual(descriptor.NameObject, "singleValue");
            Token[] insertedToken = new Token[3] {
                new Token("FIELD", "имя"),
                new Token("CODE", "name"),
                new Token("OFFEST_COLUMN", "1")
            };
            for (int i = 0; i < insertedToken.Length; i++) {
                descriptor.addToken(insertedToken[i]);
            }
            Assert.AreEqual(descriptor.CountToken, 3);
            Assert.AreEqual(descriptor.CountNestedObject, 0);
            descriptor.deleteToken(1);
            descriptor.deleteToken(insertedToken[0]);
            Assert.AreEqual(descriptor.CountToken, 1);
            Assert.AreEqual(descriptor.CountNestedObject, 0);
            Assert.AreEqual(descriptor.getToken(0).Name, insertedToken[2].Name);
        }

        /// <summary>
        /// Добавление, получение и удаление вложенных объектов.
        /// </summary>
        [TestMethod]
        public void addAndGetAndDeleteNestedObject() {
            ObjectDescriptor descriptor = new ObjectDescriptor("table");
            Assert.AreEqual(descriptor.NameObject, "table");
            Token[] insertedToken = new Token[3] {
                new Token("SHEET_NUMBER", "2"),
                new Token("CODE", "DATA_FOR_INSERV"),
                new Token("INCLUDE_FINAL_ROW", "1")
            };
            for (int i = 0; i < insertedToken.Length; i++) {
                descriptor.addToken(insertedToken[i]);
            }
            Assert.AreEqual(descriptor.CountToken, 3);
            Assert.AreEqual(descriptor.CountNestedObject, 0);
            ObjectDescriptor[] nestedObject = new ObjectDescriptor[4];
            for (int i = 0; i < nestedObject.Length; i++) {
                nestedObject[i] = new ObjectDescriptor("column");
            }
            nestedObject[0].addToken(new Token("NAME", "Код"));
            nestedObject[0].addToken(new Token("CODE", "TYPE_SERV_CODE"));
            nestedObject[1].addToken(new Token("NAME", "Описание"));
            nestedObject[1].addToken(new Token("CODE", "TYPE_SERV_NAME"));
            nestedObject[1].addToken(new Token("FINAL_CELL", "ИТОГО:"));
            nestedObject[2].addToken(new Token("NAME", "Кол-во оказанных услуг (всего)"));
            nestedObject[2].addToken(new Token("CODE", "COUNT_SERV_NORMAL"));
            nestedObject[3].addToken(new Token("NAME", "Кол-во оказанных услуг сверх объемов, определяемых стандартом"));
            nestedObject[3].addToken(new Token("CODE", "COUNT_SERV_OVER"));
            for (int i = 0; i < nestedObject.Length; i++) {
                descriptor.addNestedObject(nestedObject[i]);
            }
            Assert.AreEqual(descriptor.CountToken, 3);
            Assert.AreEqual(descriptor.CountNestedObject, 4);
            for (int i = 0; i < nestedObject.Length; i++) {
                Assert.AreEqual(descriptor.getNestedObject(i).getValueToken("NAME"), nestedObject[i].getValueToken("NAME"));
                Assert.AreEqual(descriptor.getNestedObject(i).getValueToken("CODE"), nestedObject[i].getValueToken("CODE"));
            }
            int indexDeletedObject = 1;
            descriptor.deleteNestedObject(indexDeletedObject);
            for (int i = 0; i < nestedObject.Length - 1; i++) {
                if (i < indexDeletedObject) {
                    Assert.AreEqual(descriptor.getNestedObject(i).getValueToken("NAME"), nestedObject[i].getValueToken("NAME"));
                    Assert.AreEqual(descriptor.getNestedObject(i).getValueToken("CODE"), nestedObject[i].getValueToken("CODE"));
                }
                else {
                    Assert.AreEqual(descriptor.getNestedObject(i).getValueToken("NAME"), nestedObject[i + 1].getValueToken("NAME"));
                    Assert.AreEqual(descriptor.getNestedObject(i).getValueToken("CODE"), nestedObject[i + 1].getValueToken("CODE"));
                }
            }
        }
    }
}
