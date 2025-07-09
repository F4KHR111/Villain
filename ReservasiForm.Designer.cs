namespace Villain
{
    partial class ReservasiForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvReservasi;
        private System.Windows.Forms.ComboBox cmbPengunjung;
        private System.Windows.Forms.ComboBox cmbVilla;
        private System.Windows.Forms.TextBox txtIDReservasi;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblIDReservasi;
        private System.Windows.Forms.Label lblPengunjung;
        private System.Windows.Forms.Label lblVilla;
        private System.Windows.Forms.Label lblCheckIn;
        private System.Windows.Forms.Label lblCheckOut;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvReservasi = new System.Windows.Forms.DataGridView();
            this.cmbPengunjung = new System.Windows.Forms.ComboBox();
            this.cmbVilla = new System.Windows.Forms.ComboBox();
            this.txtIDReservasi = new System.Windows.Forms.TextBox();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblIDReservasi = new System.Windows.Forms.Label();
            this.lblPengunjung = new System.Windows.Forms.Label();
            this.lblVilla = new System.Windows.Forms.Label();
            this.lblCheckIn = new System.Windows.Forms.Label();
            this.lblCheckOut = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservasi)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReservasi
            // 
            this.dgvReservasi.AllowUserToAddRows = false;
            this.dgvReservasi.AllowUserToDeleteRows = false;
            this.dgvReservasi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReservasi.Location = new System.Drawing.Point(20, 20);
            this.dgvReservasi.MultiSelect = false;
            this.dgvReservasi.Name = "dgvReservasi";
            this.dgvReservasi.ReadOnly = true;
            this.dgvReservasi.RowHeadersWidth = 51;
            this.dgvReservasi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReservasi.Size = new System.Drawing.Size(600, 200);
            this.dgvReservasi.TabIndex = 0;
            this.dgvReservasi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReservasi_CellClick);
            // 
            // cmbPengunjung
            // 
            this.cmbPengunjung.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPengunjung.FormattingEnabled = true;
            this.cmbPengunjung.Location = new System.Drawing.Point(110, 272);
            this.cmbPengunjung.Name = "cmbPengunjung";
            this.cmbPengunjung.Size = new System.Drawing.Size(200, 24);
            this.cmbPengunjung.TabIndex = 4;
            // 
            // cmbVilla
            // 
            this.cmbVilla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVilla.FormattingEnabled = true;
            this.cmbVilla.Location = new System.Drawing.Point(110, 307);
            this.cmbVilla.Name = "cmbVilla";
            this.cmbVilla.Size = new System.Drawing.Size(200, 24);
            this.cmbVilla.TabIndex = 6;
            // 
            // txtIDReservasi
            // 
            this.txtIDReservasi.Location = new System.Drawing.Point(110, 237);
            this.txtIDReservasi.Name = "txtIDReservasi";
            this.txtIDReservasi.ReadOnly = true;
            this.txtIDReservasi.Size = new System.Drawing.Size(100, 22);
            this.txtIDReservasi.TabIndex = 2;
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckIn.Location = new System.Drawing.Point(110, 340);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(120, 22);
            this.dtpCheckIn.TabIndex = 8;
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckOut.Location = new System.Drawing.Point(110, 375);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(120, 22);
            this.dtpCheckOut.TabIndex = 10;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(350, 235);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 27);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(350, 275);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 27);
            this.btnEdit.TabIndex = 14;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(350, 315);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 27);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblIDReservasi
            // 
            this.lblIDReservasi.AutoSize = true;
            this.lblIDReservasi.Location = new System.Drawing.Point(20, 240);
            this.lblIDReservasi.Name = "lblIDReservasi";
            this.lblIDReservasi.Size = new System.Drawing.Size(88, 16);
            this.lblIDReservasi.TabIndex = 1;
            this.lblIDReservasi.Text = "ID Reservasi:";
            // 
            // lblPengunjung
            // 
            this.lblPengunjung.AutoSize = true;
            this.lblPengunjung.Location = new System.Drawing.Point(20, 275);
            this.lblPengunjung.Name = "lblPengunjung";
            this.lblPengunjung.Size = new System.Drawing.Size(81, 16);
            this.lblPengunjung.TabIndex = 3;
            this.lblPengunjung.Text = "Pengunjung:";
            // 
            // lblVilla
            // 
            this.lblVilla.AutoSize = true;
            this.lblVilla.Location = new System.Drawing.Point(20, 310);
            this.lblVilla.Name = "lblVilla";
            this.lblVilla.Size = new System.Drawing.Size(36, 16);
            this.lblVilla.TabIndex = 5;
            this.lblVilla.Text = "Villa:";
            // 
            // lblCheckIn
            // 
            this.lblCheckIn.AutoSize = true;
            this.lblCheckIn.Location = new System.Drawing.Point(20, 345);
            this.lblCheckIn.Name = "lblCheckIn";
            this.lblCheckIn.Size = new System.Drawing.Size(62, 16);
            this.lblCheckIn.TabIndex = 7;
            this.lblCheckIn.Text = "Check-In:";
            // 
            // lblCheckOut
            // 
            this.lblCheckOut.AutoSize = true;
            this.lblCheckOut.Location = new System.Drawing.Point(20, 380);
            this.lblCheckOut.Name = "lblCheckOut";
            this.lblCheckOut.Size = new System.Drawing.Size(72, 16);
            this.lblCheckOut.TabIndex = 9;
            this.lblCheckOut.Text = "Check-Out:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 415);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 16);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(110, 407);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(200, 24);
            this.cmbStatus.TabIndex = 16;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(350, 352);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 26);
            this.btnImport.TabIndex = 17;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(350, 388);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 26);
            this.btnReport.TabIndex = 17;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // ReservasiForm
            // 
            this.ClientSize = new System.Drawing.Size(640, 450);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.dgvReservasi);
            this.Controls.Add(this.lblIDReservasi);
            this.Controls.Add(this.txtIDReservasi);
            this.Controls.Add(this.lblPengunjung);
            this.Controls.Add(this.cmbPengunjung);
            this.Controls.Add(this.lblVilla);
            this.Controls.Add(this.cmbVilla);
            this.Controls.Add(this.lblCheckIn);
            this.Controls.Add(this.dtpCheckIn);
            this.Controls.Add(this.lblCheckOut);
            this.Controls.Add(this.dtpCheckOut);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Name = "ReservasiForm";
            this.Text = "Reservasi Form - CRUD";
            this.Load += new System.EventHandler(this.ReservasiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservasi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnReport;
    }
}
