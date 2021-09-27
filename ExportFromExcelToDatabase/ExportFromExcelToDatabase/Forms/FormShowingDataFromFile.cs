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
            showSingleValue(descriptors, data.singleValue);
            showTable(dataGridViewTable1, data.table[0]);

        }

        public void showSingleValue(List<DescriptorObject> descriptors, List<Token> singleValue) {
            for (int i = 0; i < singleValue.Count; i++) {
                string field = "";
                for (int j = 0; j < descriptors.Count; j++) {
                    if (descriptors[j].getValueToken("CODE") == singleValue[i].Name) {
                        field = descriptors[j].getValueToken("FIELD");
                        break;
                    }
                }
                dataGridViewSingleValue.Rows.Add();
                dataGridViewSingleValue["Field", i].Value = field;
                dataGridViewSingleValue["Code", i].Value = singleValue[i].Name;
                dataGridViewSingleValue["Value", i].Value = singleValue[i].Value;
            }
        }

        public void showTable(DataGridView dataGridView, DataTable table) {
            for (int i = 0; i < table.Columns.Count; i++) {
                dataGridView.Columns.Add(table.Columns[i].ColumnName, table.Columns[i].ColumnName);
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
