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
using System.Threading; //Для работы с потоками.
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

        private string[] _pathFiles;

        /// <summary>
        /// Сгенерированные SQL-запросы для Excel-файлов.
        /// </summary>
        private List<string> _queryForExcelFiles;

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
            _pathDescriptor = _pathExe + "\\Sourse\\Descriptor.txt";
            _pathQuery = _pathExe + "\\Sourse\\Query.sql";
        }

        /// <summary>
        /// Чтение и установка дескриптора Excel-файла.
        /// </summary>
        /// <param name="pathDescriptor">Путь к дескриптору Excel-файла.</param>
        /// <returns>0 - Успешно, 1 - Ошибка при чтении, 2 - Файл не найден.</returns>
        public int setDescriptor(string pathDescriptor) {
            if (File.Exists(pathDescriptor)) {
                try {
                    _pathDescriptor = pathDescriptor;
                    _listDescriptorObject = readDescriptor(pathDescriptor);
                    return 0;
                }
                catch(Exception exception) {
                    MessageBox.Show(exception.Message, "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1;
                }
            }
            else {
                MessageBox.Show($"Не был найден дескриптор Excel-файла по пути: {pathDescriptor}!", "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 2;
            }
        }

        /// <summary>
        /// Чтение и установка SQL-запроса.
        /// </summary>
        /// <param name="pathQuery"></param>
        /// <returns>0 - Успешно, 1 - Файл не найден.</returns>
        public int setQuerySQL(string pathQuery) {
            if (File.Exists(pathQuery)) {
                _pathQuery = pathQuery;
                _querySQL = readQuery(pathQuery, false);
                return 0;
            }
            else {
                MessageBox.Show($"Не был найден SQL-запрос по пути: {pathQuery}!", "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
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

        private void выбратьФайлToolStripMenuItem_Click(object sender, EventArgs e) {
            bool successSetDescriptor = (setDescriptor(_pathDescriptor) == 0);
            bool successSetQuerySQL = (setQuerySQL(_pathQuery) == 0);
            //В чем ошибка установки вылетает при выполнении функции setDescriptor и setQuerySQL.
            if (successSetDescriptor && successSetQuerySQL) {
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    _pathFiles = new string[1] { openFileDialog.FileName };
                    Thread thread = new Thread(prepareForProcess);
                    thread.Start();

                    //prepareForProcess(new string[1] { openFileDialog.FileName });
                }
            } 
        }

        private void выбратьПапкуToolStripMenuItem_Click(object sender, EventArgs e) {
            bool successSetDescriptor = (setDescriptor(_pathDescriptor) == 0);
            bool successSetQuerySQL = (setQuerySQL(_pathQuery) == 0);
            //В чем ошибка установки вылетает при выполнении функции setDescriptor и setQuerySQL.
            if (successSetDescriptor && successSetQuerySQL) {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                    _pathFiles = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                    Thread thread = new Thread(prepareForProcess);
                    thread.Start();
                    //prepareForProcess(Directory.GetFiles(folderBrowserDialog.SelectedPath));
                }
            }
        }

        private void настрокиToolStripMenuItem_Click(object sender, EventArgs e) {
            bool successSetDescriptor = (setDescriptor(_pathDescriptor) == 0);
            bool successSetQuerySQL = (setQuerySQL(_pathQuery) == 0);
            FormSetting formSetting = new FormSetting(this);
            if (successSetDescriptor) {
                formSetting.setDescriptor(_pathDescriptor, _listDescriptorObject);
            }
            if (successSetQuerySQL) {
                formSetting.setQuery(_pathQuery, _querySQL);
            }
            formSetting.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void buttonStart_Click(object sender, EventArgs e) {

        }

        private void dataGridViewProcess_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) {
                if (e.ColumnIndex == dataGridViewProcess.Columns["ShowData"].Index) {
                    FormShowingDataFromFile formShowing = new FormShowingDataFromFile(_listDescriptorObject, _dataExcelFiles[e.RowIndex]);
                    formShowing.Show();
                }
                else if (e.ColumnIndex == dataGridViewProcess.Columns["ShowQuerySQL"].Index) {
                    FormShowingQuerySQL formShowing = new FormShowingQuerySQL(_queryForExcelFiles[e.RowIndex]);
                    formShowing.Show();
                }
            }
        }

        /// <summary>
        /// Подготовка к обработке: Добавить в список файлы и отобразить в dataGridViewProcess.
        /// </summary>
        /// <param name="pathFiles">Пути к файлам</param>
        /// <returns>0 - Успешно; -1 - Ошибка.</returns>
        private void prepareForProcess() {
            dataGridViewProcess.Rows.Clear();
            _excelFiles = new List<string>();
            _dataExcelFiles = new List<ParserResult>();
            _queryForExcelFiles = new List<string>();
            progressBar.Invoke(new Action(() => progressBar.Value = 0));
            progressBar.Invoke(new Action(() => progressBar.Maximum = _pathFiles.Length));
            for (int i = 0; i < _pathFiles.Length; i++) {
                prepareFileForProcess(_pathFiles[i]);
                progressBar.Invoke(new Action(() => progressBar.Value += 1));
            }
            if (_excelFiles.Count < 1) {
                MessageBox.Show("В папке нет Excel-файлов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Подготовка файла к обработке: Чтение файла, парсинг файла, генерация SQL-запроса.
        /// Добавление файла в список файлов для обработки, добавление данных файла в список данных файлов, добавление сгенерированного SQl-запроса.
        /// Плюс файл отображается в dataGridViewProcess.
        /// </summary>
        /// <param name="pathFile">Путь к файлу.</param>
        /// <returns>0 - Успешно, 1 - файл не является Excel-файлом</returns>
        private int prepareFileForProcess(string pathFile) {
            string fileExtension = pathFile.Substring(pathFile.LastIndexOf('.') + 1);
            if ((fileExtension == "xlsx") || (fileExtension == "xls")) {
                ReaderExcelFile readerFile = new ReaderExcelFile();
                ParserExcelFile parser = new ParserExcelFile();
                GeneratorSQLCommand generator = new GeneratorSQLCommand();
                ExcelFile excelFile = readerFile.readFile(pathFile);
                ParserResult parserResult = parser.parser(_listDescriptorObject, excelFile);
                string querySQL = generator.insertDataToCommand(_querySQL, parserResult.singleValue, parserResult.table);
                _excelFiles.Add(pathFile);
                _dataExcelFiles.Add(parserResult);
                _queryForExcelFiles.Add(querySQL);
                int lastRow = dataGridViewProcess.Rows.Count;
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess.Rows.Add()));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["FileName", lastRow].Value = pathFile.Substring(pathFile.LastIndexOf('\\') + 1)));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Status", lastRow].Value = "Готов к обработке"));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["ShowData", lastRow].Value = "Показать"));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["ShowQuerySQL", lastRow].Value = "Показать"));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["ShowData", lastRow].Style.BackColor = Color.LightGreen));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["ShowQuerySQL", lastRow].Style.BackColor = Color.LightGreen));
                return 0;
            }
            else {
                return 1;
            }
        }
    }
}
