using ExportFromExcelToDatabase.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExportFromExcelToDatabase.Tests
{
    [TestClass]
    public class DescriptorReaderTests
    {
        /// <summary>
        /// Получение токена по корректной строке, где Имя токена в одно слово и значение токена в одно слово.
        /// </summary>
        [TestMethod]
        public void getTokenByCorrectPathToken_OneWordTag_OneWordValue() {
            DescriptorReader reader = new DescriptorReader();
            string pathToken = "     SHEET_NUMBER   :  \t   1       ;       "; //Еще проверим, чтобы нормально обрабатывало пробелы и табуляцию.
            Token expectedToken = new Token("SHEET_NUMBER", "1");
            Token receivedToken = reader.getToken(pathToken);
            Assert.AreEqual(expectedToken.Name, receivedToken.Name);
            Assert.AreEqual(expectedToken.Value, receivedToken.Value);
        }

        /// <summary>
        /// Получение токена по корректной строке, где Имя токена в одно слово и значение токена в несколько слов без вложенных кавычек.
        /// </summary>
        [TestMethod]
        public void getTokenByCorrectPathToken_OneWordTag_SeveralWordValueNotContainТestedQuotes() {
            DescriptorReader reader = new DescriptorReader();
            string pathToken = "    SECTION_NAME:\t\"Данные о получателе социальных услуг(ПСУ)\"     ;     "; //Еще проверим, чтобы нормально обрабатывало пробелы и табуляцию.
            Token expectedToken = new Token("SECTION_NAME", "Данные о получателе социальных услуг(ПСУ)");
            Token receivedToken = reader.getToken(pathToken);
            Assert.AreEqual(expectedToken.Name, receivedToken.Name);
            Assert.AreEqual(expectedToken.Value, receivedToken.Value);
        }

        /// <summary>
        /// Получение токена по корректной строке, где Имя токена в одно слово и значение токена в несколько слов с вложенными кавычками.
        /// </summary>
        [TestMethod]
        public void getTokenByCorrectPathToken_OneWordTag_SeveralWordValueContainТestedQuotes() {
            DescriptorReader reader = new DescriptorReader();
            string pathToken = "SECTION_NAME: \"Данные о получателе социальных услуг \\\"ПСУ\\\"\";";
            Token expectedToken = new Token("SECTION_NAME", "Данные о получателе социальных услуг \"ПСУ\"");
            Token receivedToken = reader.getToken(pathToken);
            Assert.AreEqual(expectedToken.Name, receivedToken.Name);
            Assert.AreEqual(expectedToken.Value, receivedToken.Value);
        }

        /// <summary>
        /// Получение токена по корректной строке, где Имя токена в одно слово и значение токена в несколько слов с вложенными кавычками и табуляцией.
        /// </summary>
        [TestMethod]
        public void getTokenByCorrectPathToken_OneWordTag_SeveralWordValueContainТestedQuotesWithTab() {
            DescriptorReader reader = new DescriptorReader();
            string pathToken = "SECTION_NAME    : \t  \"  Данные  о   получателе  социальных  услуг   \\\"    ПСУ \\\"    \"  ;   ";
            Token expectedToken = new Token("SECTION_NAME", "  Данные  о   получателе  социальных  услуг   \"    ПСУ \"    ");
            Token receivedToken = reader.getToken(pathToken);
            Assert.AreEqual(expectedToken.Name, receivedToken.Name);
            Assert.AreEqual(expectedToken.Value, receivedToken.Value);
        }

        /// <summary>
        /// Получение токена по не корректной строке, где нет точки с запятой.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getTokenByUnCorrectPathToken_ThereIsNotSemicolon() {
            DescriptorReader reader = new DescriptorReader();
            string pathToken = "FIELD: Имя CODE: name;";
            Token receivedToken = reader.getToken(pathToken);
        }

        /// <summary>
        /// Получение токена по не корректной строке, где нет первых кавычек.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getTokenByUnCorrectPathToken_OneWordTag_SeveralWordValueWithoutFirstQuotes() {
            DescriptorReader reader = new DescriptorReader();
            string pathToken = "SECTION_NAME: Данные о получателе социальных услуг(ПСУ)\";";
            Token receivedToken = reader.getToken(pathToken);
        }

        /// <summary>
        /// Получение токена по не корректной строке, где нет вторых кавычек.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getTokenByUnCorrectPathToken_OneWordTag_SeveralWordValueWithoutSecondQuotes() {
            DescriptorReader reader = new DescriptorReader();
            string pathToken = "SECTION_NAME: \"Данные о получателе социальных услуг(ПСУ);";
            Token receivedToken = reader.getToken(pathToken);
        }

        /// <summary>
        /// Получение токена по корректной строке, где Имя токена в одно слово и значение токена в одно слово.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getTokenByUnCorrectPathToken_ThereIsNotColon() {
            DescriptorReader reader = new DescriptorReader();
            string pathToken = "SHEET_NUMBER  1;";
            Token receivedToken = reader.getToken(pathToken);
        }
    }
}
