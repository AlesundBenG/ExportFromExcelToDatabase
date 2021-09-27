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

        public void setDescriptor(string descriptorPath, List<DescriptorObject> descriptors) {
            labelDescriptorPath.Text = descriptorPath;
            dataGridViewDescriptorObjects.Rows.Clear();
            int currentPrintedObject = 0;
            for (int i = 0; i < descriptors.Count; i++) {
                currentPrintedObject = currentPrintedObject + printDescriptor(0, currentPrintedObject + 1, descriptors[i]);
            }
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
                string descriptorPath = openFileDialog.FileName;
                List<DescriptorObject> descriptors = _formMain.readDescriptor(descriptorPath);
                _formMain.PathDescriptor = descriptorPath;
                setDescriptor(descriptorPath, descriptors);
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

        /// <summary>
        /// Отобразить дескриптор в таблице.
        /// </summary>
        /// <param name="numberParent">Номер родителя объекта.</param>
        /// <param name="currentNumber">Номер объекта</param>
        /// <param name="descriptor">Дескриптор объекта</param>
        /// <returns>Количество отображенных объектов, так как одоин объект может содержать в себе вложенные дескрипторы.</returns>
        private int printDescriptor(int numberParent, int currentNumber, DescriptorObject descriptor) {
            int countPrintedObject = 1; //Количество отображенных дескрипторов. 1 так как текущий отображен.
            for (int i = 0; i < descriptor.CountToken; i++) {
                dataGridViewDescriptorObjects.Rows.Add();
                int indexRow = dataGridViewDescriptorObjects.Rows.Count - 1;
                dataGridViewDescriptorObjects["Number", indexRow].Value = currentNumber;
                dataGridViewDescriptorObjects["numberParent", indexRow].Value = numberParent;
                dataGridViewDescriptorObjects["tag", indexRow].Value = descriptor.NameObject;
                dataGridViewDescriptorObjects["Attribute", indexRow].Value = descriptor.getToken(i).Name;
                dataGridViewDescriptorObjects["value", indexRow].Value = descriptor.getToken(i).Value;
            }
            for (int i = 0; i < descriptor.CountNestedObject; i++) {
                countPrintedObject = countPrintedObject + printDescriptor(currentNumber, currentNumber + countPrintedObject, descriptor.getNestedObject(i));
            }
            return countPrintedObject;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
