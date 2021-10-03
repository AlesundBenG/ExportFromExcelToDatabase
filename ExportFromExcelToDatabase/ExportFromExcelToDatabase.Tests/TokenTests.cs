using ExportFromExcelToDatabase.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExportFromExcelToDatabase.Tests
{
    [TestClass]
    public class TokenTests
    {
        /// <summary>
        /// ������������� ������ ����� ������ ����������� (��� ���� ������ ���� �������).
        /// </summary>
        [TestMethod]
        public void constructorEmptyToken() {
            Token emptyToken = new Token();
            Assert.AreEqual(emptyToken.Name, null);
            Assert.AreEqual(emptyToken.Value, null);
        }

        /// <summary>
        /// ������������� ������ ����� ����������� � ���������� ������� ���������� (��� ���� ������ ���� ����� ������� ����������).
        /// </summary>
        [TestMethod]
        public void constructorFillToken() {
            Token filltoken = new Token("CODE", "name");
            Assert.AreEqual(filltoken.Name, "CODE");
            Assert.AreEqual(filltoken.Value, "name");
        }

        /// <summary>
        /// ��������� �������� ������ (���� ������ ���� ����� ��������������� ����������).
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
        /// ��������� NULL � �������� �������� (� ����� ������ ���� NULL, � �� ������ ������).
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
        /// ������������ ������  (������� ������ ���� �������, �� �������� �����������).
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
