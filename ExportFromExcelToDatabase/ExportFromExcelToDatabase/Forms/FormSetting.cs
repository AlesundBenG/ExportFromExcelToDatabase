using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportFromExcelToDatabase
{
    public partial class FormSetting : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Атрибуты*/

        private FormMain _formMain;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Public методы*/

        /// <summary>
        /// Конструктор.
        /// </summary>
        public FormSetting(FormMain formMain) {
            InitializeComponent();
            _formMain = formMain;

        }

        /// <summary>
        /// Отобразить SQL-запрос на форме.
        /// </summary>
        /// <param name="queryPath">Путь к SQL-запросу.</param>
        /// <param name="query">SQL-запрос.</param>
        public void setQuery(string queryPath, string query) {
            labelQueryPath.Text = queryPath;
            textBoxQuerySQL.Text = query;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Private методы*/

        private void дескрипторExcelфайлаToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                labelDescriptorPath.Text = openFileDialog.FileName;
                _formMain.PathDescriptor = openFileDialog.FileName;
            }
        }

        private void выбратьSQLзапросToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string queryPath = openFileDialog.FileName;
                string query = _formMain.readQuery(queryPath, false);
                _formMain.PathQuery = queryPath;
                setQuery(queryPath, query);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
