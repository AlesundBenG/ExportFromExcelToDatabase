using ExportFromExcelToDatabase.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            string pathToken = "SHEET_NUMBER: 1;";
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
            string pathToken = "SECTION_NAME: \"Данные о получателе социальных услуг(ПСУ)\";";
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

    }
}
