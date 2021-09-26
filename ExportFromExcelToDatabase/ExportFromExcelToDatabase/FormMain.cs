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
using System.IO;    //Для работы с папками.

namespace ExportFromExcelToDatabase
{
    public partial class FormMain : Form
    {
        public FormMain() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            ReaderTextFile readerFile = new ReaderTextFile();
            ReaderExcelFile readerExcelFile = new ReaderExcelFile();

            string[] linesFile = readerFile.getSplitTextOnLines("C:\\Users\\batas\\Desktop\\test.txt");
            string[] linesFileSQL = readerFile.getSplitTextOnLines("C:\\Users\\batas\\Desktop\\testSQL.txt");
            ExcelFile excelFile = readerExcelFile.readFile("C:\\Users\\batas\\Desktop\\test.xls");
            string file = String.Join(" ", linesFile);
            string fileSQL = String.Join(" ", linesFileSQL);
            ReaderDescriptor readerDescriptor = new ReaderDescriptor();
            List<DescriptorObject> descriptors = readerDescriptor.getListDescriptors(file);

            ParserExcelFile parserExcelFile = new ParserExcelFile();
            ParserResult result = parserExcelFile.parser(descriptors, excelFile);

            GeneratorSQLCommand generator = new GeneratorSQLCommand();
            string command = generator.insertDataToCommand(fileSQL, result.singleValue, result.table);


        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void выбратьФайлToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                prepareForProcess(new string[1] { openFileDialog.FileName });
            }
        }

        private void выбратьПапкуToolStripMenuItem_Click(object sender, EventArgs e) {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                prepareForProcess(Directory.GetFiles(folderBrowserDialog.SelectedPath));
            }
        }

        private void настрокиToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void buttonStart_Click(object sender, EventArgs e) {

        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Подготовка к обработке.
        /// </summary>
        /// <param name="pathFiles">Пути к файлам</param>
        /// <returns>0 - Успешно; -1 - Ошибка.</returns>
        private int prepareForProcess(string[] pathFiles) {
            if (pathFiles.Length < 1) {
                MessageBox.Show("Ошибка", "В папке нет файлов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridViewProcess.Rows.Clear();
                return -1;
            }
            dataGridViewProcess.Rows.Clear();
            dataGridViewProcess.Rows.Add(pathFiles.Length);
            for (int i = 0; i < pathFiles.Length; i++) {
                dataGridViewProcess.Rows[i].Cells[0].Value = pathFiles[i].Substring(pathFiles[i].LastIndexOf('\\') + 1);
                dataGridViewProcess.Rows[i].Cells[1].Value = "Готов к обработке";
            }
            return 0;
        }


    }
}
