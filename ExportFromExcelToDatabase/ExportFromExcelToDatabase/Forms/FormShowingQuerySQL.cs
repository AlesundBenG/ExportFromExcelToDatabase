using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportFromExcelToDatabase.Forms
{
    public partial class FormShowingQuerySQL : Form
    {
        public FormShowingQuerySQL(string querySQL) {
            InitializeComponent();
            textBoxQuerySQL.Text = querySQL;
        }
    }
}
