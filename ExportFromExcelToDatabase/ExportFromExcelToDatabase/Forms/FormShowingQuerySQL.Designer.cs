namespace ExportFromExcelToDatabase.Forms
{
    partial class FormShowingQuerySQL
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
            this.textBoxQuerySQL = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxQuerySQL
            // 
            this.textBoxQuerySQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQuerySQL.Location = new System.Drawing.Point(13, 12);
            this.textBoxQuerySQL.Multiline = true;
            this.textBoxQuerySQL.Name = "textBoxQuerySQL";
            this.textBoxQuerySQL.ReadOnly = true;
            this.textBoxQuerySQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxQuerySQL.Size = new System.Drawing.Size(759, 537);
            this.textBoxQuerySQL.TabIndex = 0;
            this.textBoxQuerySQL.WordWrap = false;
            // 
            // FormShowingQuerySQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.textBoxQuerySQL);
            this.Name = "FormShowingQuerySQL";
            this.Text = "Сгенерированный SQL-запрос по файлу";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxQuerySQL;
    }
}