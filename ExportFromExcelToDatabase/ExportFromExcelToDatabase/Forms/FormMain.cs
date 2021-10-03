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
    /// <summary>
    /// Метаданные файла.
    /// </summary>
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
        /// <summary>
        /// Мета данные файлов в папке.
        /// </summary>
        private MetadataFile[] _metadataFiles;
        /// <summary>
        /// Шаблон SQL-запроса.
        /// </summary>
        private string _querySQL;
        /// <summary>
        /// Список дескрипторов объектов.
        /// </summary>
        private List<ObjectDescriptor> _listDescriptorObject;
        /// <summary>
        /// Файлы в выбранной папке.
        /// </summary>
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
        public List<ObjectDescriptor> readDescriptor(string pathDescriptor) {
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
            Thread thread = new Thread(executeQuerySQL);
            thread.Start();
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
            setEnableButton(buttonStart, false);
            setColorButton(buttonStart, Color.Orange);
            setTexLabel(labelCondition, "Чтение файлов");
            initMetadataFiles(_pathFiles);
            showFiles(_metadataFiles);
            resetProgressBar(progressBar);
            setMaxValueProgressBar(progressBar, _pathFiles.Length);
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
                incrementProgressBar(progressBar);
            }
            setEnableButton(buttonStart, true);
            setColorButton(buttonStart, Color.LightGreen);
            setTexLabel(labelCondition, "");
            resetProgressBar(progressBar);
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
            clearDataGridView(dataGridViewProcess);
            for (int i = 0; i < files.Length; i++) {
                string nameFile = files[i].pathFile.Substring(files[i].pathFile.LastIndexOf('\\') + 1);
                addRowDataGridView(dataGridViewProcess);
                setCellValueDataGridView(dataGridViewProcess, i, "FileName", nameFile);
            }
        }

        /// <summary>
        /// Проверка расширения файла.
        /// </summary>
        /// <param name="indexFile">Идентификатор.</param>
        /// <returns>0 - Допустимое расширение, 1 - не допустимое расширение.</returns>
        private int checkFilesExtension(int indexFile) {
            string fileExtension = _metadataFiles[indexFile].pathFile.Substring(_metadataFiles[indexFile].pathFile.LastIndexOf('.') + 1);
            if ((fileExtension == "xlsx") || (fileExtension == "xls")) {
                _metadataFiles[indexFile].conditionProcess = 1;
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Status", "Открытие файла");
                return 0;
            }
            else {
                _metadataFiles[indexFile].conditionProcess = -1;
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Status", "Ошибка открытия файла");
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Message", "Не верное расширение файла");
                setRowColorDataGridView(dataGridViewProcess, indexFile, Color.LightGray);
                return 1;
            }
        }

        /// <summary>
        /// Чтение файла.
        /// </summary>
        /// <param name="indexFile">Идентификатор файла.</param>
        /// <returns>0 - Успешно, 1 - ошибка.</returns>
        private int getDataFile(int indexFile) {
            ReaderExcelFile readerFile = new ReaderExcelFile();
            try {
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Status", "Чтение файла");
                _metadataFiles[indexFile].dataFile = readerFile.readFile(_metadataFiles[indexFile].pathFile);
                _metadataFiles[indexFile].conditionProcess = 2;
                return 0;
            }
            catch (Exception exception) {
                _metadataFiles[indexFile].conditionProcess = -2;
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Status", "Ошибка чтения файла");
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Message", exception.Message);
                setRowColorDataGridView(dataGridViewProcess, indexFile, Color.LightGray);
                return 1;
            }
        }

        /// <summary>
        /// Извлечение данных из файла.
        /// </summary>
        /// <param name="indexFile">Идентификатор файла.</param>
        /// <returns>0 - Успешно, 1 - ошибка.</returns>
        private int parseFile(int indexFile) {
            ParserExcelFile parser = new ParserExcelFile();
            try {
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Status", "Извлечение данных из файла");
                _metadataFiles[indexFile].parsedData = parser.parser(_listDescriptorObject, _metadataFiles[indexFile].dataFile);
                _metadataFiles[indexFile].conditionProcess = 3;
                setCellValueDataGridView(dataGridViewProcess, indexFile, "ShowData", "Показать");
                setCellColorDataGridView(dataGridViewProcess, indexFile, "ShowData", Color.LightBlue);
                return 0;
            }
            catch (Exception exception) {
                _metadataFiles[indexFile].conditionProcess = -3;
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Status", "Ошибка извлечения данных");
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Message", exception.Message);
                setRowColorDataGridView(dataGridViewProcess, indexFile, Color.LightGray);
                return 1;
            }
        }

        /// <summary>
        /// Генерация SQL-запроса для файла.
        /// </summary>
        /// <param name="indexFile">Идентификатор файла.</param>
        /// <returns>0 - Успешно, 1 - ошибка.</returns>
        private int generateQuery(int indexFile) {
            GeneratorSQLCommand generator = new GeneratorSQLCommand();
            try {
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Status", "Генерация запроса");
                _metadataFiles[indexFile].queryForFile = generator.insertDataToCommand(_querySQL, _metadataFiles[indexFile].parsedData.singleValue, _metadataFiles[indexFile].parsedData.table);
                _metadataFiles[indexFile].conditionProcess = 4;
                setCellValueDataGridView(dataGridViewProcess, indexFile, "ShowQuerySQL", "Показать");
                setCellColorDataGridView(dataGridViewProcess, indexFile, "ShowQuerySQL", Color.LightBlue);
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Status", "Готов к обработке");
                return 0;
            }
            catch (Exception exception) {
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Status", "Ошибка генерации запроса");
                setCellValueDataGridView(dataGridViewProcess, indexFile, "Message", exception.Message);
                setRowColorDataGridView(dataGridViewProcess, indexFile, Color.LightGray);
                return 1;
            }
        }

        /// <summary>
        /// Выполнение сгенерированных SQL-запросов.
        /// </summary>
        private void executeQuerySQL() {
            ExecutorQuerySQL executor = new ExecutorQuerySQL();
            setTexLabel(labelCondition, "Выполнение запросов");
            resetProgressBar(progressBar);
            setMaxValueProgressBar(progressBar, _pathFiles.Length);
            for (int i = 0; i < _metadataFiles.Length; i++) {
                if (_metadataFiles[i].conditionProcess == 4) {
                    try {
                        List<DataTable> result = executor.executeComamnd(_metadataFiles[i].queryForFile);
                        if (result.Count > 0) {
                            string thereIsError = result[0].Rows[0]["thereIsError"].ToString();
                            if (thereIsError == "0") {
                                setCellValueDataGridView(dataGridViewProcess, i, "Status", "Запрос успешно выполнен");
                                setCellColorDataGridView(dataGridViewProcess, i, "Status", Color.LightGreen);
                            }
                            else {
                                setCellValueDataGridView(dataGridViewProcess, i, "Status", "Ошибка выполнения запроса");
                                setCellColorDataGridView(dataGridViewProcess, i, "Status", Color.Red);
                                setCellValueDataGridView(dataGridViewProcess, i, "Message", result[0].Rows[0]["message"].ToString());
                            }
                        }
                    }
                    catch (Exception exception) {
                        setCellValueDataGridView(dataGridViewProcess, i, "Status", "Ошибка выполнения запроса");
                        setCellColorDataGridView(dataGridViewProcess, i, "Status", Color.Red);
                        setCellValueDataGridView(dataGridViewProcess, i, "Message", exception.Message);
                    }
                }
                incrementProgressBar(progressBar);
            }
            resetProgressBar(progressBar);
            setTexLabel(labelCondition, "");
        }

        /// <summary>
        /// Установка доступноси кнопки на нажатие.
        /// </summary>
        /// <param name="button">Кнопка.</param>
        /// <param name="enable">Доступность к нажатию.</param>
        private void setEnableButton(Button button, bool enable) {
            button.Invoke(new Action(() => button.Enabled = enable));
        }

        /// <summary>
        /// Установка цвета кнопки.
        /// </summary>
        /// <param name="button">Кнопка.</param>
        /// <param name="color">Цвет.</param>
        private void setColorButton(Button button, Color color) {
            button.Invoke(new Action(() => button.BackColor = color));
        }

        /// <summary>
        /// Установка текста таблички.
        /// </summary>
        /// <param name="label">Табличка.</param>
        /// <param name="text">Текст.</param>
        private void setTexLabel(Label label, string text) {
            label.Invoke(new Action(() => label.Text = text));
        }

        /// <summary>
        /// Сброс прогресс бара.
        /// </summary>
        /// <param name="progressBar">Прогресс бар</param>
        private void resetProgressBar(ProgressBar progressBar) {
            progressBar.Invoke(new Action(() => progressBar.Value = 0));
        }

        /// <summary>
        /// Установка максимального допустимого значения в прогресс баре.
        /// </summary>
        /// <param name="progressBar">Прогресс бар.</param>
        /// <param name="maxValue">Максимально допустимое значение.</param>
        private void setMaxValueProgressBar(ProgressBar progressBar, int maxValue) {
            progressBar.Invoke(new Action(() => progressBar.Maximum = maxValue));
        }

        /// <summary>
        /// Увеличить на единицу значение прогресс бара.
        /// </summary>
        /// <param name="progressBar">Прогресс бар.</param>
        private void incrementProgressBar(ProgressBar progressBar) {
            progressBar.Invoke(new Action(() => progressBar.Value += 1));
        }

        /// <summary>
        /// Очистка таблицы.
        /// </summary>
        /// <param name="dataGridView">Таблица.</param>
        private void clearDataGridView(DataGridView dataGridView) {
            dataGridView.Invoke(new Action(() => dataGridView.Rows.Clear()));
        }

        /// <summary>
        /// Добавить строку в таблицу.
        /// </summary>
        /// <param name="dataGridView">Таблица.</param>
        private void addRowDataGridView(DataGridView dataGridView) {
            dataGridView.Invoke(new Action(() => dataGridView.Rows.Add()));
        }

        /// <summary>
        /// Установка значения в ячейку таблицы.
        /// </summary>
        /// <param name="dataGridView">Таблица.</param>
        /// <param name="row">Строка.</param>
        /// <param name="columnName">Название столбца.</param>
        /// <param name="value">Значение.</param>
        private void setCellValueDataGridView(DataGridView dataGridView, int row, string columnName, string value) {
            dataGridView.Invoke(new Action(() => dataGridView[columnName, row].Value = value));
        }

        /// <summary>
        /// Установка цвета строке таблицы.
        /// </summary>
        /// <param name="dataGridView">Таблица.</param>
        /// <param name="row">Строка.</param>
        /// <param name="color">Цвет.</param>
        private void setRowColorDataGridView(DataGridView dataGridView, int row, Color color) {
            dataGridView.Invoke(new Action(() => dataGridView.Rows[row].DefaultCellStyle.BackColor = color));
        }

        /// <summary>
        /// Установка цвета ячейке таблицы.
        /// </summary>
        /// <param name="dataGridView">Таблица.</param>
        /// <param name="row">Строка.</param>
        /// <param name="columnName">Название столбца.</param>
        /// <param name="color">Цвет.</param>
        private void setCellColorDataGridView(DataGridView dataGridView, int row, string columnName, Color color) {
            dataGridView.Invoke(new Action(() => dataGridView[columnName, row].Style.BackColor = color));
        }
    }
}
