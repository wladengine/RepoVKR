namespace RepoVKR_WinApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBigFileName = new System.Windows.Forms.Label();
            this.lblCurrentFIO = new System.Windows.Forms.Label();
            this.lblCurrentCatalog = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAllCount = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartImport = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblBigFileName);
            this.groupBox1.Controls.Add(this.lblCurrentFIO);
            this.groupBox1.Controls.Add(this.lblCurrentCatalog);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblAllCount);
            this.groupBox1.Controls.Add(this.lblCount);
            this.groupBox1.Controls.Add(this.progressBar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 217);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Прогресс";
            // 
            // lblBigFileName
            // 
            this.lblBigFileName.AutoSize = true;
            this.lblBigFileName.Location = new System.Drawing.Point(121, 113);
            this.lblBigFileName.Name = "lblBigFileName";
            this.lblBigFileName.Size = new System.Drawing.Size(0, 13);
            this.lblBigFileName.TabIndex = 7;
            // 
            // lblCurrentFIO
            // 
            this.lblCurrentFIO.AutoSize = true;
            this.lblCurrentFIO.Location = new System.Drawing.Point(121, 95);
            this.lblCurrentFIO.Name = "lblCurrentFIO";
            this.lblCurrentFIO.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentFIO.TabIndex = 6;
            // 
            // lblCurrentCatalog
            // 
            this.lblCurrentCatalog.AutoSize = true;
            this.lblCurrentCatalog.Location = new System.Drawing.Point(127, 78);
            this.lblCurrentCatalog.Name = "lblCurrentCatalog";
            this.lblCurrentCatalog.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentCatalog.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Текущий каталог:";
            // 
            // lblAllCount
            // 
            this.lblAllCount.AutoSize = true;
            this.lblAllCount.Location = new System.Drawing.Point(303, 20);
            this.lblAllCount.Name = "lblAllCount";
            this.lblAllCount.Size = new System.Drawing.Size(13, 13);
            this.lblAllCount.TabIndex = 3;
            this.lblAllCount.Text = "0";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(127, 20);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(13, 13);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "0";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 42);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(472, 23);
            this.progressBar.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Всего в обработке:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Всего обработано:";
            // 
            // btnStartImport
            // 
            this.btnStartImport.Location = new System.Drawing.Point(404, 12);
            this.btnStartImport.Name = "btnStartImport";
            this.btnStartImport.Size = new System.Drawing.Size(92, 23);
            this.btnStartImport.TabIndex = 1;
            this.btnStartImport.Text = "Start import";
            this.btnStartImport.UseVisualStyleBackColor = true;
            this.btnStartImport.Click += new System.EventHandler(this.btnStartImport_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(12, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(92, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "Test FILE";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 270);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnStartImport);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Импорт в базу";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartImport;
        private System.Windows.Forms.Label lblAllCount;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCurrentFIO;
        private System.Windows.Forms.Label lblCurrentCatalog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblBigFileName;
        private System.Windows.Forms.Button btnTest;
    }
}

