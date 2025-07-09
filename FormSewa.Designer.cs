namespace Villain
{
    partial class FormSewa
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
            this.txtIDKontrak = new System.Windows.Forms.TextBox();
            this.lblIDKontrak = new System.Windows.Forms.Label();
            this.dgvKontrakSewa = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtBiaya = new System.Windows.Forms.TextBox();
            this.cmbVilla = new System.Windows.Forms.ComboBox();
            this.cmbPengunjung = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblBiaya = new System.Windows.Forms.Label();
            this.lblVilla = new System.Windows.Forms.Label();
            this.lblPengunjung = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKontrakSewa)).BeginInit();
            this.SuspendLayout();
            // 
            // txtIDKontrak
            // 
            this.txtIDKontrak.Location = new System.Drawing.Point(250, -38);
            this.txtIDKontrak.Name = "txtIDKontrak";
            this.txtIDKontrak.ReadOnly = true;
            this.txtIDKontrak.Size = new System.Drawing.Size(228, 22);
            this.txtIDKontrak.TabIndex = 24;
            // 
            // lblIDKontrak
            // 
            this.lblIDKontrak.AutoSize = true;
            this.lblIDKontrak.Location = new System.Drawing.Point(138, -35);
            this.lblIDKontrak.Name = "lblIDKontrak";
            this.lblIDKontrak.Size = new System.Drawing.Size(71, 16);
            this.lblIDKontrak.TabIndex = 19;
            this.lblIDKontrak.Text = "ID Kontrak:";
            // 
            // dgvKontrakSewa
            // 
            this.dgvKontrakSewa.AllowUserToAddRows = false;
            this.dgvKontrakSewa.AllowUserToDeleteRows = false;
            this.dgvKontrakSewa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKontrakSewa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKontrakSewa.Location = new System.Drawing.Point(140, 257);
            this.dgvKontrakSewa.MultiSelect = false;
            this.dgvKontrakSewa.Name = "dgvKontrakSewa";
            this.dgvKontrakSewa.ReadOnly = true;
            this.dgvKontrakSewa.RowHeadersWidth = 51;
            this.dgvKontrakSewa.RowTemplate.Height = 25;
            this.dgvKontrakSewa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKontrakSewa.Size = new System.Drawing.Size(570, 213);
            this.dgvKontrakSewa.TabIndex = 39;
            this.dgvKontrakSewa.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKontrakSewa_CellClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(324, 214);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(86, 29);
            this.btnDelete.TabIndex = 38;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(232, 214);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(86, 29);
            this.btnEdit.TabIndex = 36;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(140, 214);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 29);
            this.btnAdd.TabIndex = 35;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(249, 161);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(228, 24);
            this.cmbStatus.TabIndex = 34;
            // 
            // txtBiaya
            // 
            this.txtBiaya.Location = new System.Drawing.Point(249, 124);
            this.txtBiaya.Name = "txtBiaya";
            this.txtBiaya.Size = new System.Drawing.Size(228, 22);
            this.txtBiaya.TabIndex = 33;
            // 
            // cmbVilla
            // 
            this.cmbVilla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVilla.FormattingEnabled = true;
            this.cmbVilla.Location = new System.Drawing.Point(250, 90);
            this.cmbVilla.Name = "cmbVilla";
            this.cmbVilla.Size = new System.Drawing.Size(228, 24);
            this.cmbVilla.TabIndex = 32;
            // 
            // cmbPengunjung
            // 
            this.cmbPengunjung.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPengunjung.FormattingEnabled = true;
            this.cmbPengunjung.Location = new System.Drawing.Point(250, 52);
            this.cmbPengunjung.Name = "cmbPengunjung";
            this.cmbPengunjung.Size = new System.Drawing.Size(228, 24);
            this.cmbPengunjung.TabIndex = 31;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(250, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(228, 22);
            this.textBox1.TabIndex = 30;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(137, 164);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 16);
            this.lblStatus.TabIndex = 29;
            this.lblStatus.Text = "Status:";
            // 
            // lblBiaya
            // 
            this.lblBiaya.AutoSize = true;
            this.lblBiaya.Location = new System.Drawing.Point(137, 127);
            this.lblBiaya.Name = "lblBiaya";
            this.lblBiaya.Size = new System.Drawing.Size(45, 16);
            this.lblBiaya.TabIndex = 28;
            this.lblBiaya.Text = "Biaya:";
            // 
            // lblVilla
            // 
            this.lblVilla.AutoSize = true;
            this.lblVilla.Location = new System.Drawing.Point(138, 93);
            this.lblVilla.Name = "lblVilla";
            this.lblVilla.Size = new System.Drawing.Size(36, 16);
            this.lblVilla.TabIndex = 27;
            this.lblVilla.Text = "Villa:";
            // 
            // lblPengunjung
            // 
            this.lblPengunjung.AutoSize = true;
            this.lblPengunjung.Location = new System.Drawing.Point(138, 56);
            this.lblPengunjung.Name = "lblPengunjung";
            this.lblPengunjung.Size = new System.Drawing.Size(81, 16);
            this.lblPengunjung.TabIndex = 26;
            this.lblPengunjung.Text = "Pengunjung:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "ID Kontrak:";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(416, 214);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(80, 30);
            this.btnImport.TabIndex = 40;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(505, 214);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(80, 30);
            this.btnReport.TabIndex = 40;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // FormSewa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 556);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.dgvKontrakSewa);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.txtBiaya);
            this.Controls.Add(this.cmbVilla);
            this.Controls.Add(this.cmbPengunjung);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblBiaya);
            this.Controls.Add(this.lblVilla);
            this.Controls.Add(this.lblPengunjung);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIDKontrak);
            this.Controls.Add(this.lblIDKontrak);
            this.Name = "FormSewa";
            this.Text = "FormSewa";
            ((System.ComponentModel.ISupportInitialize)(this.dgvKontrakSewa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtIDKontrak;
        private System.Windows.Forms.Label lblIDKontrak;
        private System.Windows.Forms.DataGridView dgvKontrakSewa;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtBiaya;
        private System.Windows.Forms.ComboBox cmbVilla;
        private System.Windows.Forms.ComboBox cmbPengunjung;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblBiaya;
        private System.Windows.Forms.Label lblVilla;
        private System.Windows.Forms.Label lblPengunjung;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnReport;
    }
}