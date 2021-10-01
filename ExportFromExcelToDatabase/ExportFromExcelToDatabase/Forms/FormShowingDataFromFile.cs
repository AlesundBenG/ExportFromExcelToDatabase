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
            for (int i = 0; i < data.table.Count; i++) {
                //tabControl.TabPages.Add(createPage(descriptors, data.table[i]));
                TabPage pagePage = new TabPage($"Значения таблицы {data.table[i].TableName}") {
                    BackColor = Color.White
                };
                tabControl.TabPages.Add(pagePage);
                pagePage.Controls.Add(createAndFillDataGridView(pagePage, descriptors, data.table[i]));
            }
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
                    string columnName = column.getValueToken("CODE");
                    if (columnName.Equals(table.Columns[i].ColumnName, StringComparison.OrdinalIgnoreCase)) {
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

        private DataGridView createAndFillDataGridView(TabPage pagePage, List<DescriptorObject> descriptors, DataTable table) {
            DataGridView dataGridView = new DataGridView();
            dataGridView.Location = new Point(7, 7);
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.Height = pagePage.Height - SystemInformation.HorizontalScrollBarHeight;
            dataGridView.Width = pagePage.Width - SystemInformation.VerticalScrollBarWidth;
            showTable(dataGridView, descriptors, table);
            return dataGridView;
        }
    }
}
