namespace ExportFromExcelToDatabase.Forms
{
    partial class FormShowingDataFromFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSingleValue = new System.Windows.Forms.TabPage();
            this.dataGridViewSingleValue = new System.Windows.Forms.DataGridView();
            this.Field = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.tabPageSingleValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSingleValue)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageSingleValue);
            this.tabControl.Location = new System.Drawing.Point(13, 13);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(759, 536);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageSingleValue
            // 
            this.tabPageSingleValue.Controls.Add(this.dataGridViewSingleValue);
            this.tabPageSingleValue.Location = new System.Drawing.Point(4, 22);
            this.tabPageSingleValue.Name = "tabPageSingleValue";
            this.tabPageSingleValue.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSingleValue.Size = new System.Drawing.Size(751, 510);
            this.tabPageSingleValue.TabIndex = 0;
            this.tabPageSingleValue.Text = "Одиночные значения";
            this.tabPageSingleValue.UseVisualStyleBackColor = true;
            // 
            // dataGridViewSingleValue
            // 
            this.dataGridViewSingleValue.AllowUserToAddRows = false;
            this.dataGridViewSingleValue.AllowUserToDeleteRows = false;
            this.dataGridViewSingleValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSingleValue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSingleValue.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewSingleValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Field,
            this.Code,
            this.Value});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSingleValue.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSingleValue.Location = new System.Drawing.Point(7, 7);
            this.dataGridViewSingleValue.Name = "dataGridViewSingleValue";
            this.dataGridViewSingleValue.ReadOnly = true;
            this.dataGridViewSingleValue.Size = new System.Drawing.Size(738, 497);
            this.dataGridViewSingleValue.TabIndex = 0;
            // 
            // Field
            // 
            this.Field.HeaderText = "Поле";
            this.Field.Name = "Field";
            this.Field.ReadOnly = true;
            this.Field.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Code
            // 
            this.Code.HeaderText = "Код";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Value
            // 
            this.Value.HeaderText = "Значение";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FormShowingDataFromFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormShowingDataFromFile";
            this.Text = "Данные файла";
            this.tabControl.ResumeLayout(false);
            this.tabPageSingleValue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSingleValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageSingleValue;
        private System.Windows.Forms.DataGridView dataGridViewSingleValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Field;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}