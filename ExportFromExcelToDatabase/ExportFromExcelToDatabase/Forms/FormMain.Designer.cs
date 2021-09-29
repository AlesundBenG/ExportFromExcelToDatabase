
namespace ExportFromExcelToDatabase
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьПапкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настрокиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxProcess = new System.Windows.Forms.GroupBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.dataGridViewProcess = new System.Windows.Forms.DataGridView();
            this.Информация = new System.Windows.Forms.GroupBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShowData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShowQuerySQL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip.SuspendLayout();
            this.groupBoxProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcess)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.PowderBlue;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.настрокиToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьФайлToolStripMenuItem,
            this.выбратьПапкуToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // выбратьФайлToolStripMenuItem
            // 
            this.выбратьФайлToolStripMenuItem.Name = "выбратьФайлToolStripMenuItem";
            this.выбратьФайлToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.выбратьФайлToolStripMenuItem.Text = "Выбрать файл";
            this.выбратьФайлToolStripMenuItem.Click += new System.EventHandler(this.выбратьФайлToolStripMenuItem_Click);
            // 
            // выбратьПапкуToolStripMenuItem
            // 
            this.выбратьПапкуToolStripMenuItem.Name = "выбратьПапкуToolStripMenuItem";
            this.выбратьПапкуToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.выбратьПапкуToolStripMenuItem.Text = "Выбрать папку";
            this.выбратьПапкуToolStripMenuItem.Click += new System.EventHandler(this.выбратьПапкуToolStripMenuItem_Click);
            // 
            // настрокиToolStripMenuItem
            // 
            this.настрокиToolStripMenuItem.Name = "настрокиToolStripMenuItem";
            this.настрокиToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.настрокиToolStripMenuItem.Text = "Настроки";
            this.настрокиToolStripMenuItem.Click += new System.EventHandler(this.настрокиToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // groupBoxProcess
            // 
            this.groupBoxProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxProcess.BackColor = System.Drawing.Color.LightGray;
            this.groupBoxProcess.Controls.Add(this.buttonStart);
            this.groupBoxProcess.Controls.Add(this.dataGridViewProcess);
            this.groupBoxProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxProcess.Location = new System.Drawing.Point(13, 28);
            this.groupBoxProcess.Name = "groupBoxProcess";
            this.groupBoxProcess.Size = new System.Drawing.Size(759, 435);
            this.groupBoxProcess.TabIndex = 1;
            this.groupBoxProcess.TabStop = false;
            this.groupBoxProcess.Text = "Процесс";
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.BackColor = System.Drawing.Color.Orange;
            this.buttonStart.Enabled = false;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStart.Location = new System.Drawing.Point(638, 400);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(115, 29);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Запустить";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // dataGridViewProcess
            // 
            this.dataGridViewProcess.AllowUserToAddRows = false;
            this.dataGridViewProcess.AllowUserToDeleteRows = false;
            this.dataGridViewProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProcess.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProcess.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewProcess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProcess.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.Status,
            this.Message,
            this.ShowData,
            this.ShowQuerySQL});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewProcess.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewProcess.Location = new System.Drawing.Point(7, 20);
            this.dataGridViewProcess.Name = "dataGridViewProcess";
            this.dataGridViewProcess.ReadOnly = true;
            this.dataGridViewProcess.Size = new System.Drawing.Size(746, 373);
            this.dataGridViewProcess.TabIndex = 0;
            this.dataGridViewProcess.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProcess_CellClick);
            this.dataGridViewProcess.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewProcess_MouseClick);
            // 
            // Информация
            // 
            this.Информация.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Информация.BackColor = System.Drawing.Color.LightGray;
            this.Информация.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Информация.Location = new System.Drawing.Point(13, 469);
            this.Информация.Name = "Информация";
            this.Информация.Size = new System.Drawing.Size(759, 80);
            this.Информация.TabIndex = 2;
            this.Информация.TabStop = false;
            this.Информация.Text = "Информация";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "Наименование файла";
            this.openFileDialog.Filter = "Excel-файлы |*.xls; *.xlsx";
            // 
            // FileName
            // 
            this.FileName.HeaderText = "Файл";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 61;
            // 
            // Status
            // 
            this.Status.HeaderText = "Статус";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 66;
            // 
            // Message
            // 
            this.Message.HeaderText = "Доп. информация";
            this.Message.Name = "Message";
            this.Message.ReadOnly = true;
            this.Message.Width = 113;
            // 
            // ShowData
            // 
            this.ShowData.HeaderText = "Просмотр файла";
            this.ShowData.Name = "ShowData";
            this.ShowData.ReadOnly = true;
            this.ShowData.Width = 108;
            // 
            // ShowQuerySQL
            // 
            this.ShowQuerySQL.HeaderText = "Просмотр сгенерированного SQL-запроса";
            this.ShowQuerySQL.Name = "ShowQuerySQL";
            this.ShowQuerySQL.ReadOnly = true;
            this.ShowQuerySQL.Width = 167;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.Информация);
            this.Controls.Add(this.groupBoxProcess);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormMain";
            this.Text = "Загрузка данных в базу";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBoxProcess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настрокиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxProcess;
        private System.Windows.Forms.GroupBox Информация;
        private System.Windows.Forms.ToolStripMenuItem выбратьФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьПапкуToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewProcess;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShowData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShowQuerySQL;
    }
}

