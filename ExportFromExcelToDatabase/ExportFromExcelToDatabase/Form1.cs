using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExportFromExcelToDatabase.Classes;

namespace ExportFromExcelToDatabase
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            ReaderTextFile readerFile = new ReaderTextFile();
            ReaderExcelFile readerExcelFile = new ReaderExcelFile();

            string[] linesFile = readerFile.getSplitTextOnLines("C:\\Users\\bnl\\Desktop\\test.txt");
            ExcelFile excelFile = readerExcelFile.readFile("C:\\Users\\bnl\\Desktop\\test.xls");
            string file = String.Join(" ", linesFile);

            ReaderDescriptor readerDescriptor = new ReaderDescriptor();
            List<DescriptorObject> descriptors = readerDescriptor.getListDescriptors(file);

            ParserExcelFile parserExcelFile = new ParserExcelFile();
            parserExcelFile.parser(descriptors, excelFile);
        }
    }
}
