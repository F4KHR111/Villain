namespace Villain
{
    partial class KontrakSewaForm
    {
        private System.ComponentModel.IContainer components = null;

        // Dispose pattern
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblIDKontrak = new System.Windows.Forms.Label();
            this.lblPengunjung = new System.Windows.Forms.Label();
            this.lblVilla = new System.Windows.Forms.Label();
            this.lblBiaya = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtIDKontrak = new System.Windows.Forms.TextBox();
            this.cmbPengunjung = new System.Windows.Forms.ComboBox();
            this.cmbVilla = new System.Windows.Forms.ComboBox();
            this.txtBiaya = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvKontrakSewa = new System.Windows.Forms.DataGridView();
            this.btnImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKontrakSewa)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIDKontrak
            // 
            this.lblIDKontrak.AutoSize = true;
            this.lblIDKontrak.Location = new System.Drawing.Point(14, 21);
            this.lblIDKontrak.Name = "lblIDKontrak";
            this.lblIDKontrak.Size = new System.Drawing.Size(71, 16);
            this.lblIDKontrak.TabIndex = 0;
            this.lblIDKontrak.Text = "ID Kontrak:";
            // 
            // lblPengunjung
            // 
            this.lblPengunjung.AutoSize = true;
            this.lblPengunjung.Location = new System.Drawing.Point(14, 59);
            this.lblPengunjung.Name = "lblPengunjung";
            this.lblPengunjung.Size = new System.Drawing.Size(81, 16);
            this.lblPengunjung.TabIndex = 1;
            this.lblPengunjung.Text = "Pengunjung:";
            // 
            // lblVilla
            // 
            this.lblVilla.AutoSize = true;
            this.lblVilla.Location = new System.Drawing.Point(14, 96);
            this.lblVilla.Name = "lblVilla";
            this.lblVilla.Size = new System.Drawing.Size(36, 16);
            this.lblVilla.TabIndex = 2;
            this.lblVilla.Text = "Villa:";
            // 
            // lblBiaya
            // 
            this.lblBiaya.AutoSize = true;
            this.lblBiaya.Location = new System.Drawing.Point(13, 130);
            this.lblBiaya.Name = "lblBiaya";
            this.lblBiaya.Size = new System.Drawing.Size(45, 16);
            this.lblBiaya.TabIndex = 5;
            this.lblBiaya.Text = "Biaya:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(13, 167);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 16);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status:";
            // 
            // txtIDKontrak
            // 
            this.txtIDKontrak.Location = new System.Drawing.Point(126, 18);
            this.txtIDKontrak.Name = "txtIDKontrak";
            this.txtIDKontrak.ReadOnly = true;
            this.txtIDKontrak.Size = new System.Drawing.Size(228, 22);
            this.txtIDKontrak.TabIndex = 7;
            // 
            // cmbPengunjung
            // 
            this.cmbPengunjung.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPengunjung.FormattingEnabled = true;
            this.cmbPengunjung.Location = new System.Drawing.Point(126, 55);
            this.cmbPengunjung.Name = "cmbPengunjung";
            this.cmbPengunjung.Size = new System.Drawing.Size(228, 24);
            this.cmbPengunjung.TabIndex = 8;
            // 
            // cmbVilla
            // 
            this.cmbVilla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVilla.FormattingEnabled = true;
            this.cmbVilla.Location = new System.Drawing.Point(126, 93);
            this.cmbVilla.Name = "cmbVilla";
            this.cmbVilla.Size = new System.Drawing.Size(228, 24);
            this.cmbVilla.TabIndex = 9;
            // 
            // txtBiaya
            // 
            this.txtBiaya.Location = new System.Drawing.Point(125, 127);
            this.txtBiaya.Name = "txtBiaya";
            this.txtBiaya.Size = new System.Drawing.Size(228, 22);
            this.txtBiaya.TabIndex = 12;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(125, 164);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(228, 24);
            this.cmbStatus.TabIndex = 13;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(14, 288);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 29);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(106, 288);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(86, 29);
            this.btnEdit.TabIndex = 15;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(199, 288);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 29);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(291, 288);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(86, 29);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvKontrakSewa
            // 
            this.dgvKontrakSewa.AllowUserToAddRows = false;
            this.dgvKontrakSewa.AllowUserToDeleteRows = false;
            this.dgvKontrakSewa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKontrakSewa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKontrakSewa.Location = new System.Drawing.Point(14, 331);
            this.dgvKontrakSewa.MultiSelect = false;
            this.dgvKontrakSewa.Name = "dgvKontrakSewa";
            this.dgvKontrakSewa.ReadOnly = true;
            this.dgvKontrakSewa.RowHeadersWidth = 51;
            this.dgvKontrakSewa.RowTemplate.Height = 25;
            this.dgvKontrakSewa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKontrakSewa.Size = new System.Drawing.Size(526, 213);
            this.dgvKontrakSewa.TabIndex = 18;
            this.dgvKontrakSewa.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKontrakSewa_CellClick);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(387, 288);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(80, 30);
            this.btnImport.TabIndex = 19;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // KontrakSewaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 565);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.dgvKontrakSewa);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.txtBiaya);
            this.Controls.Add(this.cmbVilla);
            this.Controls.Add(this.cmbPengunjung);
            this.Controls.Add(this.txtIDKontrak);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblBiaya);
            this.Controls.Add(this.lblVilla);
            this.Controls.Add(this.lblPengunjung);
            this.Controls.Add(this.lblIDKontrak);
            this.Name = "KontrakSewaForm";
            this.Text = "Kontrak Sewa - CRUD";
            this.Load += new System.EventHandler(this.KontrakSewaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKontrakSewa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIDKontrak;
        private System.Windows.Forms.Label lblPengunjung;
        private System.Windows.Forms.Label lblVilla;
        private System.Windows.Forms.Label lblBiaya;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.TextBox txtIDKontrak;
        private System.Windows.Forms.ComboBox cmbPengunjung;
        private System.Windows.Forms.ComboBox cmbVilla;
        private System.Windows.Forms.TextBox txtBiaya;
        private System.Windows.Forms.ComboBox cmbStatus;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;

        private System.Windows.Forms.DataGridView dgvKontrakSewa;
        private System.Windows.Forms.Button btnImport;
    }
}
