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

namespace ExportToDatabaseFromFile
{
    public partial class FormLogging : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Атрибуты*/

        /// <summary>
        /// Установленное подключение к серверу.
        /// </summary>
        private ExecutorQuerySQL _executorQuerySQL;


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Свойства*/

        /// <summary>
        /// Исполнитель SQL-запросов (перед использованием проверить подключение к серверу).
        /// </summary>
        public ExecutorQuerySQL ExecutorQuerySQL {
            get {
                return _executorQuerySQL;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Public методы*/

        /// <summary>
        /// Инициализация формы и ее компонентов.
        /// </summary>
        public FormLogging() {
            InitializeComponent();
        }

        /// <summary>
        /// Инициализация формы и ее компонентов с установлением полей подключения.
        /// </summary>
        /// <param name="executorQuerySQL">Параметры подключения, которые будут установлены в качестве полей по умолчанию.</param>
        public FormLogging(ExecutorQuerySQL executorQuerySQL) {
            InitializeComponent();
            textBoxServer.Text = executorQuerySQL.Server;
            textBoxLogin.Text = executorQuerySQL.Login;
            textBoxPassword.Text = executorQuerySQL.Password;
            textBoxDatabase.Text = executorQuerySQL.DataBase;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Private методы*/

        /// <summary>
        /// Нажание кнопки подключения к базе данных.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConnect_Click(object sender, EventArgs e) {
            _executorQuerySQL = new ExecutorQuerySQL();
            _executorQuerySQL.connectDataBase(textBoxServer.Text, textBoxLogin.Text, textBoxPassword.Text, textBoxDatabase.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
