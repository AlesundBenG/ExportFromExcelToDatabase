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
            string[] linesFile = readerFile.getSplitTextOnLines("C:\\Users\\batas\\Desktop\\test.txt");
            string file = String.Join(" ", linesFile);
            ReaderDescriptor readerDescriptor = new ReaderDescriptor();
            readerDescriptor.getListDescriptors(file);
        }
    }
}
