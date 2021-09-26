
namespace ExportFromExcelToDatabase
{
    partial class FormSetting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBoxFilesPath = new System.Windows.Forms.GroupBox();
            this.labelDescriptorPathTitle = new System.Windows.Forms.Label();
            this.labelQueryPathTitle = new System.Windows.Forms.Label();
            this.labelDescriptorPath = new System.Windows.Forms.Label();
            this.labelQueryPath = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDescriptor = new System.Windows.Forms.TabPage();
            this.tabPageQuery = new System.Windows.Forms.TabPage();
            this.dataGridViewDescriptorObjects = new System.Windows.Forms.DataGridView();
            this.textBoxQuerySQL = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дескрипторExcelфайлаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьSQLзапросToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxFilesPath.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageDescriptor.SuspendLayout();
            this.tabPageQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDescriptorObjects)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxFilesPath
            // 
            this.groupBoxFilesPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFilesPath.BackColor = System.Drawing.Color.LightGray;
            this.groupBoxFilesPath.Controls.Add(this.panel1);
            this.groupBoxFilesPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxFilesPath.Location = new System.Drawing.Point(13, 27);
            this.groupBoxFilesPath.Name = "groupBoxFilesPath";
            this.groupBoxFilesPath.Size = new System.Drawing.Size(759, 94);
            this.groupBoxFilesPath.TabIndex = 0;
            this.groupBoxFilesPath.TabStop = false;
            this.groupBoxFilesPath.Text = "Файлы";
            // 
            // labelDescriptorPathTitle
            // 
            this.labelDescriptorPathTitle.AutoSize = true;
            this.labelDescriptorPathTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDescriptorPathTitle.Location = new System.Drawing.Point(10, 0);
            this.labelDescriptorPathTitle.Name = "labelDescriptorPathTitle";
            this.labelDescriptorPathTitle.Size = new System.Drawing.Size(136, 13);
            this.labelDescriptorPathTitle.TabIndex = 0;
            this.labelDescriptorPathTitle.Text = "Дескриптор Excel-файла:";
            // 
            // labelQueryPathTitle
            // 
            this.labelQueryPathTitle.AutoSize = true;
            this.labelQueryPathTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelQueryPathTitle.Location = new System.Drawing.Point(10, 22);
            this.labelQueryPathTitle.Name = "labelQueryPathTitle";
            this.labelQueryPathTitle.Size = new System.Drawing.Size(70, 13);
            this.labelQueryPathTitle.TabIndex = 1;
            this.labelQueryPathTitle.Text = "SQL-запрос:";
            // 
            // labelDescriptorPath
            // 
            this.labelDescriptorPath.AutoSize = true;
            this.labelDescriptorPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDescriptorPath.Location = new System.Drawing.Point(152, 0);
            this.labelDescriptorPath.Name = "labelDescriptorPath";
            this.labelDescriptorPath.Size = new System.Drawing.Size(77, 13);
            this.labelDescriptorPath.TabIndex = 2;
            this.labelDescriptorPath.Text = "DescriptorPath";
            // 
            // labelQueryPath
            // 
            this.labelQueryPath.AutoSize = true;
            this.labelQueryPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelQueryPath.Location = new System.Drawing.Point(152, 22);
            this.labelQueryPath.Name = "labelQueryPath";
            this.labelQueryPath.Size = new System.Drawing.Size(57, 13);
            this.labelQueryPath.TabIndex = 2;
            this.labelQueryPath.Text = "QueryPath";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.LightGray;
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(13, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(759, 410);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Содержимое файлов";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageDescriptor);
            this.tabControl1.Controls.Add(this.tabPageQuery);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(7, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(746, 384);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageDescriptor
            // 
            this.tabPageDescriptor.Controls.Add(this.dataGridViewDescriptorObjects);
            this.tabPageDescriptor.Location = new System.Drawing.Point(4, 22);
            this.tabPageDescriptor.Name = "tabPageDescriptor";
            this.tabPageDescriptor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDescriptor.Size = new System.Drawing.Size(738, 358);
            this.tabPageDescriptor.TabIndex = 0;
            this.tabPageDescriptor.Text = "Дескриптор Excel-файла";
            this.tabPageDescriptor.UseVisualStyleBackColor = true;
            // 
            // tabPageQuery
            // 
            this.tabPageQuery.Controls.Add(this.textBoxQuerySQL);
            this.tabPageQuery.Location = new System.Drawing.Point(4, 22);
            this.tabPageQuery.Name = "tabPageQuery";
            this.tabPageQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageQuery.Size = new System.Drawing.Size(738, 358);
            this.tabPageQuery.TabIndex = 1;
            this.tabPageQuery.Text = "SQL-запрос";
            this.tabPageQuery.UseVisualStyleBackColor = true;
            // 
            // dataGridViewDescriptorObjects
            // 
            this.dataGridViewDescriptorObjects.AllowUserToAddRows = false;
            this.dataGridViewDescriptorObjects.AllowUserToDeleteRows = false;
            this.dataGridViewDescriptorObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDescriptorObjects.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewDescriptorObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDescriptorObjects.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewDescriptorObjects.Location = new System.Drawing.Point(7, 7);
            this.dataGridViewDescriptorObjects.Name = "dataGridViewDescriptorObjects";
            this.dataGridViewDescriptorObjects.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDescriptorObjects.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewDescriptorObjects.Size = new System.Drawing.Size(725, 345);
            this.dataGridViewDescriptorObjects.TabIndex = 0;
            // 
            // textBoxQuerySQL
            // 
            this.textBoxQuerySQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQuerySQL.Location = new System.Drawing.Point(9, 7);
            this.textBoxQuerySQL.Multiline = true;
            this.textBoxQuerySQL.Name = "textBoxQuerySQL";
            this.textBoxQuerySQL.ReadOnly = true;
            this.textBoxQuerySQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxQuerySQL.Size = new System.Drawing.Size(723, 345);
            this.textBoxQuerySQL.TabIndex = 0;
            this.textBoxQuerySQL.WordWrap = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "Выберите файл";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.PowderBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.дескрипторExcelфайлаToolStripMenuItem,
            this.выбратьSQLзапросToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // дескрипторExcelфайлаToolStripMenuItem
            // 
            this.дескрипторExcelфайлаToolStripMenuItem.Name = "дескрипторExcelфайлаToolStripMenuItem";
            this.дескрипторExcelфайлаToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.дескрипторExcelфайлаToolStripMenuItem.Text = "Выбрать дескриптор Excel-файла";
            this.дескрипторExcelфайлаToolStripMenuItem.Click += new System.EventHandler(this.дескрипторExcelфайлаToolStripMenuItem_Click);
            // 
            // выбратьSQLзапросToolStripMenuItem
            // 
            this.выбратьSQLзапросToolStripMenuItem.Name = "выбратьSQLзапросToolStripMenuItem";
            this.выбратьSQLзапросToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.выбратьSQLзапросToolStripMenuItem.Text = "Выбрать SQL-запрос";
            this.выбратьSQLзапросToolStripMenuItem.Click += new System.EventHandler(this.выбратьSQLзапросToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.labelDescriptorPath);
            this.panel1.Controls.Add(this.labelQueryPath);
            this.panel1.Controls.Add(this.labelDescriptorPathTitle);
            this.panel1.Controls.Add(this.labelQueryPathTitle);
            this.panel1.Location = new System.Drawing.Point(11, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(742, 56);
            this.panel1.TabIndex = 3;
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxFilesPath);
            this.Controls.Add(this.menuStrip1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormSetting";
            this.Text = "Настройки";
            this.groupBoxFilesPath.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageDescriptor.ResumeLayout(false);
            this.tabPageQuery.ResumeLayout(false);
            this.tabPageQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDescriptorObjects)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFilesPath;
        private System.Windows.Forms.Label labelQueryPath;
        private System.Windows.Forms.Label labelDescriptorPath;
        private System.Windows.Forms.Label labelQueryPathTitle;
        private System.Windows.Forms.Label labelDescriptorPathTitle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDescriptor;
        private System.Windows.Forms.DataGridView dataGridViewDescriptorObjects;
        private System.Windows.Forms.TabPage tabPageQuery;
        private System.Windows.Forms.TextBox textBoxQuerySQL;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дескрипторExcelфайлаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьSQLзапросToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
    }
}