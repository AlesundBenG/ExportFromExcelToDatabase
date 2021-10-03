using ExportFromExcelToDatabase.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExportFromExcelToDatabase.Tests
{
    [TestClass]
    public class TokenTests
    {
        /// <summary>
        /// Инициализация токена через пустой конструктор (Все поля должны быть пустыми).
        /// </summary>
        [TestMethod]
        public void constructorEmptyToken() {
            Token emptyToken = new Token();
            Assert.AreEqual(emptyToken.Name, null);
            Assert.AreEqual(emptyToken.Value, null);
        }

        /// <summary>
        /// Инициализация токена через конструктор с установкой входных параметров (Все поля должны быть равны входным параметрам).
        /// </summary>
        [TestMethod]
        public void constructorFillToken() {
            Token filltoken = new Token("CODE", "name");
            Assert.AreEqual(filltoken.Name, "CODE");
            Assert.AreEqual(filltoken.Value, "name");
        }

        /// <summary>
        /// Устанвока значений токена (Поля должны быть равны устанавливаемым параметрам).
        /// </summary>
        [TestMethod]
        public void changeValueToken() {
            Token emptyToken = new Token();
            emptyToken.Name = "CODE";
            emptyToken.Value = "name";
            Assert.AreEqual(emptyToken.Name, "CODE");
            Assert.AreEqual(emptyToken.Value, "name");
        }

        /// <summary>
        /// Установка NULL в качестве значения (В полях должен быть NULL, а не пустая строка).
        /// </summary>
        [TestMethod]
        public void setNullValueToken() {
            Token filltoken = new Token("CODE", "name");
            filltoken.Name = null;
            filltoken.Value = null;
            Assert.AreEqual(filltoken.Name, null);
            Assert.AreEqual(filltoken.Value, null);
        }

        /// <summary>
        /// Клонирование токена  (Объекты должны быть разными, но значения одинаковыми).
        /// </summary>
        [TestMethod]
        public void cloneToken() {
            Token filltoken = new Token("CODE", "name");
            Token clonedToken = (Token)filltoken.Clone();
            Assert.AreEqual(filltoken.Name, clonedToken.Name);
            Assert.AreEqual(filltoken.Value, clonedToken.Value);
            Assert.AreNotEqual(filltoken, clonedToken);
        }
    }
}
