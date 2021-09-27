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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSingleValue = new System.Windows.Forms.TabPage();
            this.tabPageTable1 = new System.Windows.Forms.TabPage();
            this.dataGridViewSingleValue = new System.Windows.Forms.DataGridView();
            this.Field = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTable1 = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPageSingleValue.SuspendLayout();
            this.tabPageTable1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSingleValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageSingleValue);
            this.tabControl1.Controls.Add(this.tabPageTable1);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(759, 536);
            this.tabControl1.TabIndex = 0;
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
            // tabPageTable1
            // 
            this.tabPageTable1.Controls.Add(this.dataGridViewTable1);
            this.tabPageTable1.Location = new System.Drawing.Point(4, 22);
            this.tabPageTable1.Name = "tabPageTable1";
            this.tabPageTable1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTable1.Size = new System.Drawing.Size(751, 510);
            this.tabPageTable1.TabIndex = 1;
            this.tabPageTable1.Text = "Табличные значения";
            this.tabPageTable1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewSingleValue
            // 
            this.dataGridViewSingleValue.AllowUserToAddRows = false;
            this.dataGridViewSingleValue.AllowUserToDeleteRows = false;
            this.dataGridViewSingleValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSingleValue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewSingleValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSingleValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Field,
            this.Code,
            this.Value});
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
            this.Field.Width = 58;
            // 
            // Code
            // 
            this.Code.HeaderText = "Код";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 51;
            // 
            // Value
            // 
            this.Value.HeaderText = "Значение";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Width = 80;
            // 
            // dataGridViewTable1
            // 
            this.dataGridViewTable1.AllowUserToAddRows = false;
            this.dataGridViewTable1.AllowUserToDeleteRows = false;
            this.dataGridViewTable1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTable1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTable1.Location = new System.Drawing.Point(7, 7);
            this.dataGridViewTable1.Name = "dataGridViewTable1";
            this.dataGridViewTable1.ReadOnly = true;
            this.dataGridViewTable1.Size = new System.Drawing.Size(738, 497);
            this.dataGridViewTable1.TabIndex = 0;
            // 
            // FormShowingDataFromFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormShowingDataFromFile";
            this.Text = "Данные файла";
            this.tabControl1.ResumeLayout(false);
            this.tabPageSingleValue.ResumeLayout(false);
            this.tabPageTable1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSingleValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSingleValue;
        private System.Windows.Forms.DataGridView dataGridViewSingleValue;
        private System.Windows.Forms.TabPage tabPageTable1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Field;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridView dataGridViewTable1;
    }
}