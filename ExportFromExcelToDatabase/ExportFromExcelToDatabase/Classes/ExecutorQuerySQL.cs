using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //Для работы с БД.
using System.Data; //Для работы с классом DataTable.

namespace ExportFromExcelToDatabase.Classes
{
    public class ExecutorQuerySQL
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////]
        /*Атрибуты*/

        /// <summary>
        /// Имя сервера.
        /// </summary>
        private string _server;
        /// <summary>
        /// Логин подключения.
        /// </summary>
        private string _login;
        /// <summary>
        /// Пароль подключения.
        /// </summary>
        private string _password;
        /// <summary>
        /// База данных.
        /// </summary>
        private string _database;
        /// <summary>
        /// Флаг наличия успешного подключения.
        /// </summary>
        private bool _thereIsConnection;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Свойства*/

        /// <summary>
        /// Имя сервера.
        /// </summary>
        public string Server {
            get { return _server; }
        }

        /// <summary>
        /// Логин.
        /// </summary>
        public string Login {
            get { return _login; }
        }

        public string Password {
            get {
                return _password;
            }
        }

        /// <summary>
        /// База данных.
        /// </summary>
        public string DataBase {
            get { return _database; }
        }

        /// <summary>
        /// Флаг подключения к базе данных.
        /// </summary>
        public bool ThereIsConnection {
            get { return _thereIsConnection; }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Public методы*/

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ExecutorQuerySQL() {
            _thereIsConnection = false;
        }

        /// <summary>
        /// Конструктор с подключением.
        /// </summary>
        /// <param name="server">Имя сервера</param>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="database">База данных</para
        /// <returns>Флаг успешного подключения</returns>
        public ExecutorQuerySQL(string server, string login, string password, string database) {
            connectDataBase(server, login, password, database);
        }

        /// <summary>
        /// Подключение к базе данных.
        /// </summary>
        /// <param name="server">Имя сервера</param>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="database">База данных</para
        /// <returns>Флаг успешного подключения</returns>
        public bool connectDataBase(string server, string login, string password, string database) {
            _server = server;
            _login = login;
            _password = password;
            _database = database;
            try {
                SqlConnection sqlConnection = new SqlConnection($"server = {_server}; uid = {_login}; pwd = {_password}; database = {_database}");
                sqlConnection.Open();
                sqlConnection.Close();
                _thereIsConnection = true;
            }
            catch {
                _thereIsConnection = false;
            }
            return _thereIsConnection;
        }

        /// <summary>
        /// Выполнение SQL-запроса.
        /// </summary>
        /// <param name="querySQL">Запрос</param>
        /// <returns>ТСписок таблиц, являющимися результатами запроса.</returns>
        public List<DataTable> executeComamnd(string querySQL) {
            List<DataTable> result = new List<DataTable>();
            if (_thereIsConnection) {
                SqlConnection sqlConnection = new SqlConnection($"server = {_server}; uid = {_login}; pwd = {_password}; database = {_database}");
                SqlCommand comamnd = new SqlCommand(querySQL, sqlConnection);
                sqlConnection.Open();
                SqlDataReader reader = comamnd.ExecuteReader();
                while (reader.HasRows) {
                    DataTable table = new DataTable();
                    for (int i = 0; i < reader.FieldCount; i++) {
                        table.Columns.Add(reader.GetName(i).ToString(), typeof(string));
                    }
                    while (reader.Read()) {
                        string[] row = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++) {
                            row[i] = reader.GetValue(i).ToString();
                        }
                        table.Rows.Add(row);
                    }
                    result.Add(table);
                    reader.NextResult();
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                return result;
            }
            else {
                throw new Exception("Попытка выполнения SQL-запроса при отсутствующем подключении к серверу!");
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
