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
using ExportFromExcelToDatabase.Forms;

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
        /// <summary>
        /// Данные Excel-файлов.
        /// </summary>
        private List<ParserResult> _dataExcelFiles;

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
        }
        /// <summary>
        /// Путь к SQL-запросу.
        /// </summary>
        public string PathQuery {
            get {
                return _pathQuery;
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
            setDescriptor(_pathExe + "\\Sourse\\Descriptor.txt");
            setQuerySQL(_pathExe + "\\Sourse\\Query.sql");
        }

        /// <summary>
        /// Чтение и установка дескриптора Excel-файла.
        /// </summary>
        /// <param name="pathDescriptor">Путь к дескриптору Excel-файла.</param>
        public void setDescriptor(string pathDescriptor) {
            if (File.Exists(pathDescriptor) && _pathDescriptor != pathDescriptor) {
                try {
                    _pathDescriptor = pathDescriptor;
                    _listDescriptorObject = readDescriptor(pathDescriptor);
                }
                catch(Exception exception) {
                    MessageBox.Show(exception.Message, "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!File.Exists(pathDescriptor)) {
                MessageBox.Show($"Не был найден дескриптор Excel-файла по пути: {pathDescriptor}!", "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Чтение и установка SQL-запроса.
        /// </summary>
        /// <param name="pathQuery"></param>
        public void setQuerySQL(string pathQuery) {
            if (File.Exists(pathQuery) && _pathQuery != pathQuery) {
                _pathQuery = pathQuery;
                _querySQL = readQuery(pathQuery, false);
            }
            else {
                MessageBox.Show($"Не был найден SQL-запрос по пути: {pathQuery}!", "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if ((_pathDescriptor != null) && (_pathQuery != null)) {
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    prepareForProcess(new string[1] { openFileDialog.FileName });
                }
            } 
            else if (_pathDescriptor == null) {
                MessageBox.Show($"Не выбран дескриптор Excel-файла!", "Ошибка настроек программы", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (_pathQuery != null) {
                MessageBox.Show($"Не выбран SQL-запрос!", "Ошибка настроек программы", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void выбратьПапкуToolStripMenuItem_Click(object sender, EventArgs e) {
            if ((_pathDescriptor != null) && (_pathQuery != null)) {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                    prepareForProcess(Directory.GetFiles(folderBrowserDialog.SelectedPath));
                }
            }
            else if (_pathDescriptor == null) {
                MessageBox.Show($"Не выбран дескриптор Excel-файла!", "Ошибка настроек программы", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (_pathQuery != null) {
                MessageBox.Show($"Не выбран SQL-запрос!", "Ошибка настроек программы", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            _dataExcelFiles = new List<ParserResult>();
            ReaderExcelFile readerFile = new ReaderExcelFile();
            ParserExcelFile parser = new ParserExcelFile();
            for (int i = 0; i < pathFiles.Length; i++) {
                string fileExtension = pathFiles[i].Substring(pathFiles[i].LastIndexOf('.') + 1);
                if ((fileExtension == "xlsx") || (fileExtension == "xls")) {
                    _excelFiles.Add(pathFiles[i]);
                    _dataExcelFiles.Add(parser.parser(_listDescriptorObject, readerFile.readFile(pathFiles[i])));
                    dataGridViewProcess.Rows.Add();
                    dataGridViewProcess["FileName", i].Value = pathFiles[i].Substring(pathFiles[i].LastIndexOf('\\') + 1);
                    dataGridViewProcess["Status", i].Value = "Готов к обработке";
                }
            }
            if (_excelFiles.Count < 1) {
                MessageBox.Show("В папке нет Excel-файлов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewProcess_MouseClick(object sender, MouseEventArgs e) {
            
        }

        private void dataGridViewProcess_CellClick(object sender, DataGridViewCellEventArgs e) {
            FormShowingDataFromFile formShowing = new FormShowingDataFromFile(_listDescriptorObject, _dataExcelFiles[e.RowIndex]);
            formShowing.Show();
        }
    }
}
