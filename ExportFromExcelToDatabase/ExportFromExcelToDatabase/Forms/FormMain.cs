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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Атрибуты*/

        /// <summary>
        /// Путь к EXE-файлу.
        /// </summary>
        private string _pathExe;
        /// <summary>
        /// Путь к дескриптору Excel-файла.
        /// </summary>
        private string _pathDescriptor;
        /// <summary>
        /// Путь к SQL-запросу.
        /// </summary>
        private string _pathQuery;
        /// <summary>
        /// Форма настроек.
        /// </summary>
        private FormSetting _formSetting;
        /// <summary>
        /// SQL-запрос.
        /// </summary>
        private string _querySQL;
        /// <summary>
        /// Список дескрипторов объектов.
        /// </summary>
        private List<DescriptorObject> _listDescriptorObject;
        /// <summary>
        /// Файлы для обработки.
        /// </summary>
        private List<string> _excelFiles;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Свойства*/

        /// <summary>
        /// Путь к EXE-файлу.
        /// </summary>
        public string PathExe {
            get { 
                return _pathExe; 
            }
        }
        /// <summary>
        /// Путь к дескриптору Excel-файла.
        /// </summary>
        public string PathDescriptor {
            get {
                return _pathDescriptor;
            }
            set {
                if (File.Exists(value)) {
                    _pathDescriptor = value;
                    _listDescriptorObject = readDescriptor(value);
                }
                else {
                    MessageBox.Show($"Не был найден дескриптор Excel-файла по пути: {value}!", "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Путь к SQL-запросу.
        /// </summary>
        public string PathQuery {
            get {
                return _pathQuery;
            }
            set {
                if (File.Exists(value)) {
                    _pathQuery = value;
                }
                else {
                    MessageBox.Show($"Не был найден SQL-запрос по пути: {value}!", "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Public методы*/

        /// <summary>
        /// Инициализация программы.
        /// </summary>
        public FormMain() {
            InitializeComponent();
            _pathExe = Environment.CurrentDirectory;
            if (File.Exists(_pathExe + "\\Sourse\\Descriptor.txt")) {
                _pathDescriptor = _pathExe + "\\Sourse\\Descriptor.txt";
                _listDescriptorObject = readDescriptor(_pathDescriptor);
            }
            else {
                MessageBox.Show($"Не был найден дескриптор Excel-файла по пути: {_pathExe}\\Sourse\\Descriptor.txt! Укажите путь к дескриптору в настройках!", "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (File.Exists(_pathExe + "\\Sourse\\Query.sql")) {
                _pathQuery = _pathExe + "\\Sourse\\Query.sql";
                _querySQL = readQuery(_pathQuery, false);
            }
            else {
                MessageBox.Show($"Не был найден SQL-запрос по пути: {_pathExe}\\Sourse\\Query.sql! Укажите путь к SQL-запросу в настройках!", "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Чтение дескриптора Excel-файла.
        /// </summary>
        /// <param name="pathDescriptor">Путь к дескриптору.</param>
        /// <returns>Список дескрипторов, либо исключение, если файл не найден.</returns>
        public List<DescriptorObject> readDescriptor(string pathDescriptor) {
            ReaderTextFile readerTextFile = new ReaderTextFile();
            ReaderDescriptor readerDescriptor = new ReaderDescriptor();
            string[] linesTextFile = readerTextFile.getSplitTextOnLines(pathDescriptor);
            string textFile = String.Join(" ", linesTextFile);
            return readerDescriptor.getListDescriptors(textFile);
        }

        /// <summary>
        /// Чтение SQL-запроса из файла.
        /// </summary>
        /// <param name="pathQuery">Путь к SQL-запросу.</param>
        /// <param name="deleteEnter">Убрать символы Enter из запроса.</param>
        /// <returns>SQL-запрос.</returns>
        public string readQuery(string pathQuery, bool deleteEnter) {
            ReaderTextFile readerTextFile = new ReaderTextFile();
            if (deleteEnter) {
                string[] linesTextFile = readerTextFile.getSplitTextOnLines(_pathDescriptor);
                return String.Join(" ", linesTextFile);
            }
            else {
                return readerTextFile.getText(pathQuery);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Private методы*/

        private void Form1_Load(object sender, EventArgs e) {
            /*
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
            */

        }

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
            _formSetting = new FormSetting(this);
            if (_pathQuery != null) {
                _formSetting.setQuery(_pathQuery, _querySQL);
            }
            if (_pathDescriptor != null) {
                _formSetting.setDescriptor(_pathDescriptor, _listDescriptorObject);
            }
            _formSetting.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void buttonStart_Click(object sender, EventArgs e) {

        }

        /// <summary>
        /// Подготовка к обработке: Добавить в список файлы и отобразить в dataGridViewProcess.
        /// </summary>
        /// <param name="pathFiles">Пути к файлам</param>
        /// <returns>0 - Успешно; -1 - Ошибка.</returns>
        private void prepareForProcess(string[] pathFiles) {
            dataGridViewProcess.Rows.Clear();
            _excelFiles = new List<string>();
            for (int i = 0; i < pathFiles.Length; i++) {
                string fileExtension = pathFiles[i].Substring(pathFiles[i].IndexOf('.') + 1);
                if ((fileExtension == "xlsx") || (fileExtension == "xls")) {
                    _excelFiles.Add(pathFiles[i]);
                    dataGridViewProcess.Rows.Add();
                    dataGridViewProcess.Rows[i].Cells[0].Value = pathFiles[i].Substring(pathFiles[i].LastIndexOf('\\') + 1);
                    dataGridViewProcess.Rows[i].Cells[1].Value = "Готов к обработке";
                }
            }
            if (_excelFiles.Count < 1) {
                MessageBox.Show("В папке нет Excel-файлов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
