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
    public struct MetadataFile
    {
        /// <summary>
        /// Порядковый номер файла.
        /// </summary>
        public int indexFile;
        /// <summary>
        /// Путь к файлу.
        /// </summary>
        public string pathFile;
        /// <summary>
        /// Состояние обработки файла.
        /// </summary>
        public int conditionProcess;
        /// <summary>
        /// Данные из файла.
        /// </summary>
        public ExcelFile dataFile;
        /// <summary>
        /// Извлеченная информация.
        /// </summary>
        public ParserResult parsedData;
        /// <summary>
        /// Сгенерированный SQL-запрос.
        /// </summary>
        public string queryForFile;
    }

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

        private MetadataFile[] _metadataFiles;



        /// <summary>
        /// SQL-запрос.
        /// </summary>
        private string _querySQL;
        /// <summary>
        /// Список дескрипторов объектов.
        /// </summary>
        private List<DescriptorObject> _listDescriptorObject;

        private string[] _pathFiles;



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
                    //showFiles(_pathFiles);
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
                    //_pathFilesFolder = folderBrowserDialog.SelectedPath;
                    _pathFiles = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                    //showFiles(_pathFiles);
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
                    bool canShow = (dataGridViewProcess.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Показать");
                    if (canShow) {
                        FormShowingDataFromFile formShowing = new FormShowingDataFromFile(_listDescriptorObject, _metadataFiles[e.RowIndex].parsedData);
                        formShowing.Show();
                    }
                }
                else if (e.ColumnIndex == dataGridViewProcess.Columns["ShowQuerySQL"].Index) {
                    bool canShow = (dataGridViewProcess.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Показать");
                    if (canShow) {
                        FormShowingQuerySQL formShowing = new FormShowingQuerySQL(_metadataFiles[e.RowIndex].queryForFile);
                        formShowing.Show();
                    }
                }
            }
        }

        /// <summary>
        /// Подготовка к обработке: Добавить в список файлы и отобразить в dataGridViewProcess.
        /// </summary>
        /// <param name="pathFiles">Пути к файлам</param>
        /// <returns>0 - Успешно; -1 - Ошибка.</returns>
        private void prepareForProcess() {
            initMetadataFiles(_pathFiles);
            showFiles(_metadataFiles);
            progressBar.Invoke(new Action(() => progressBar.Value = 0));
            progressBar.Invoke(new Action(() => progressBar.Maximum = _pathFiles.Length));
            for (int i = 0; i < _metadataFiles.Length; i++) {
                bool successCheckFileExtension = (checkFilesExtension(i) == 0);
                if (successCheckFileExtension) {
                    bool successGetDataFile = (getDataFile(i) == 0);
                    if (successGetDataFile) {
                        bool successParseFile = (parseFile(i) == 0);
                        if (successParseFile) {
                            generateQuery(i);
                        }
                    }
                }
                progressBar.Invoke(new Action(() => progressBar.Value += 1));
            }
        }

        /// <summary>
        /// Инициализация мета-данных файлов.
        /// </summary>
        /// <param name="pathFiles">Пути к файлам.</param>
        private void initMetadataFiles(string[] pathFiles) {
            _metadataFiles = new MetadataFile[pathFiles.Length];
            for (int i = 0; i < pathFiles.Length; i++) {
                _metadataFiles[i].indexFile = i;
                _metadataFiles[i].pathFile = pathFiles[i];
                _metadataFiles[i].conditionProcess = 0;
            }
        }

        /// <summary>
        /// Загрузка в dataGridViewProcess файлов.
        /// </summary>
        /// <param name="pathFiles">Пути к файлам.</param>
        private void showFiles(MetadataFile[] files) {
            dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess.Rows.Clear()));   
            for (int i = 0; i < files.Length; i++) {
                string nameFile = files[i].pathFile.Substring(files[i].pathFile.LastIndexOf('\\') + 1);
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess.Rows.Add()));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["FileName", i].Value = nameFile));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private int checkFilesExtension(int indexFile) {
            string fileExtension = _metadataFiles[indexFile].pathFile.Substring(_metadataFiles[indexFile].pathFile.LastIndexOf('.') + 1);
            if ((fileExtension == "xlsx") || (fileExtension == "xls")) {
                _metadataFiles[indexFile].conditionProcess = 1;
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Status", indexFile].Value = "Открытие файла"));
                return 0;
            }
            else {
                _metadataFiles[indexFile].conditionProcess = -1;
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Status", indexFile].Value = "Ошибка открытия файла"));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Message", indexFile].Value = "Не верное расширение файла"));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess.Rows[indexFile].DefaultCellStyle.BackColor = Color.LightGray));
                return 1;
            }
        }

        private int getDataFile(int indexFile) {
            ReaderExcelFile readerFile = new ReaderExcelFile();
            try {
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Status", indexFile].Value = "Чтение файла"));
                _metadataFiles[indexFile].dataFile = readerFile.readFile(_metadataFiles[indexFile].pathFile);
                _metadataFiles[indexFile].conditionProcess = 2;
                return 0;
            }
            catch (Exception exception) {
                _metadataFiles[indexFile].conditionProcess = -2;
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Status", indexFile].Value = "Ошибка чтения файла"));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Message", indexFile].Value = exception.Message));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess.Rows[indexFile].DefaultCellStyle.BackColor = Color.LightGray));
                return 1;
            }
        }

        private int parseFile(int indexFile) {
            ParserExcelFile parser = new ParserExcelFile();
            try {
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Status", indexFile].Value = "Извлечение данных из файла"));
                _metadataFiles[indexFile].parsedData = parser.parser(_listDescriptorObject, _metadataFiles[indexFile].dataFile);
                _metadataFiles[indexFile].conditionProcess = 3;
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["ShowData", indexFile].Value = "Показать"));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["ShowData", indexFile].Style.BackColor = Color.LightGreen));
                return 0;
            }
            catch (Exception exception) {
                _metadataFiles[indexFile].conditionProcess = -3;
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Status", indexFile].Value = "Ошибка извлечения данных"));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Message", indexFile].Value = exception.Message));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess.Rows[indexFile].DefaultCellStyle.BackColor = Color.LightGray));
                return 1;
            }
        }

        private int generateQuery(int indexFile) {
            GeneratorSQLCommand generator = new GeneratorSQLCommand();
            try {
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Status", indexFile].Value = "Генерация запроса"));
                _metadataFiles[indexFile].queryForFile = generator.insertDataToCommand(_querySQL, _metadataFiles[indexFile].parsedData.singleValue, _metadataFiles[indexFile].parsedData.table);
                _metadataFiles[indexFile].conditionProcess = 4;
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["ShowQuerySQL", indexFile].Value = "Показать"));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["ShowQuerySQL", indexFile].Style.BackColor = Color.LightGreen));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Status", indexFile].Value = "Готов к обработке"));
                return 0;
            }
            catch (Exception exception) {
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Status", indexFile].Value = "Ошибка генерации запроса"));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess["Message", indexFile].Value = exception.Message));
                dataGridViewProcess.Invoke(new Action(() => dataGridViewProcess.Rows[indexFile].DefaultCellStyle.BackColor = Color.LightGray));
                return 1;
            }
        }

    }
}
