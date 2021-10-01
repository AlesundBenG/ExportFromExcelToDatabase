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

namespace ExportFromExcelToDatabase.Forms
{
    public partial class FormShowingDataFromFile : Form
    {
        public FormShowingDataFromFile(List<DescriptorObject> descriptors, ParserResult data) {
            InitializeComponent();
            showSingleValue(dataGridViewSingleValue, descriptors, data.singleValue);
            showTable(dataGridViewTable1, descriptors, data.table[0]);
        }

        public void showSingleValue(DataGridView dataGridView, List<DescriptorObject> descriptors, List<Token> singleValue) {
            for (int i = 0; i < singleValue.Count; i++) {
                string field = "";
                for (int j = 0; j < descriptors.Count; j++) {
                    if (descriptors[j].getValueToken("CODE") == singleValue[i].Name) {
                        field = descriptors[j].getValueToken("FIELD");
                        break;
                    }
                }
                dataGridView.Rows.Add();
                dataGridView["Field", i].Value = field;
                dataGridView["Code", i].Value = singleValue[i].Name;
                dataGridView["Value", i].Value = singleValue[i].Value;
            }
        }

        public void showTable(DataGridView dataGridView, List<DescriptorObject> descriptors, DataTable table) {
            DescriptorObject descriptorTable = new DescriptorObject();
            for (int i = 0; i < descriptors.Count; i++) {
                if (descriptors[i].getValueToken("CODE") == table.TableName) {
                    descriptorTable = descriptors[i];
                    break;
                }
            }
            for (int i = 0; i < table.Columns.Count; i++) {
                for (int j = 0; j < descriptorTable.CountNestedObject; j++) {
                    DescriptorObject column = descriptorTable.getNestedObject(i);
                    if (column.getValueToken("CODE") == table.Columns[i].ColumnName) {
                        dataGridView.Columns.Add(table.Columns[i].ColumnName, column.getValueToken("NAME"));
                        break;
                    }
                }
            }
            for (int i = 0; i < table.Rows.Count; i++) {
                dataGridView.Rows.Add();
                for (int j = 0; j < table.Columns.Count; j++) {
                    dataGridView[j, i].Value = table.Rows[i][j].ToString();
                }
            }
        }
    }
}
