namespace Villain
{
    partial class VillaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvVilla;
        private System.Windows.Forms.TextBox txtVillaID;
        private System.Windows.Forms.TextBox txtNamaVilla;
        private System.Windows.Forms.TextBox txtAlamatVilla;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.NumericUpDown numericHarga;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblVillaID;
        private System.Windows.Forms.Label lblNamaVilla;
        private System.Windows.Forms.Label lblAlamatVilla;
        private System.Windows.Forms.Label lblDeskripsi;
        private System.Windows.Forms.Label lblHarga;
        private System.Windows.Forms.Label lblStatus;

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

        private void InitializeComponent()
        {
            this.dgvVilla = new System.Windows.Forms.DataGridView();
            this.txtVillaID = new System.Windows.Forms.TextBox();
            this.txtNamaVilla = new System.Windows.Forms.TextBox();
            this.txtAlamatVilla = new System.Windows.Forms.TextBox();
            this.txtDeskripsi = new System.Windows.Forms.TextBox();
            this.numericHarga = new System.Windows.Forms.NumericUpDown();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblVillaID = new System.Windows.Forms.Label();
            this.lblNamaVilla = new System.Windows.Forms.Label();
            this.lblAlamatVilla = new System.Windows.Forms.Label();
            this.lblDeskripsi = new System.Windows.Forms.Label();
            this.lblHarga = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVilla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericHarga)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvVilla
            // 
            this.dgvVilla.AllowUserToAddRows = false;
            this.dgvVilla.AllowUserToDeleteRows = false;
            this.dgvVilla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVilla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVilla.Location = new System.Drawing.Point(330, 12);
            this.dgvVilla.MultiSelect = false;
            this.dgvVilla.Name = "dgvVilla";
            this.dgvVilla.ReadOnly = true;
            this.dgvVilla.RowHeadersWidth = 51;
            this.dgvVilla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVilla.Size = new System.Drawing.Size(550, 400);
            this.dgvVilla.TabIndex = 0;
            this.dgvVilla.SelectionChanged += new System.EventHandler(this.dgvVilla_SelectionChanged);
            this.dgvVilla.Click += new System.EventHandler(this.dgvVilla_SelectionChanged);
            // 
            // txtVillaID
            // 
            this.txtVillaID.Location = new System.Drawing.Point(110, 15);
            this.txtVillaID.Name = "txtVillaID";
            this.txtVillaID.ReadOnly = true;
            this.txtVillaID.Size = new System.Drawing.Size(200, 22);
            this.txtVillaID.TabIndex = 1;
            // 
            // txtNamaVilla
            // 
            this.txtNamaVilla.Location = new System.Drawing.Point(110, 50);
            this.txtNamaVilla.Name = "txtNamaVilla";
            this.txtNamaVilla.Size = new System.Drawing.Size(200, 22);
            this.txtNamaVilla.TabIndex = 2;
            // 
            // txtAlamatVilla
            // 
            this.txtAlamatVilla.Location = new System.Drawing.Point(110, 85);
            this.txtAlamatVilla.Name = "txtAlamatVilla";
            this.txtAlamatVilla.Size = new System.Drawing.Size(200, 22);
            this.txtAlamatVilla.TabIndex = 3;
            // 
            // txtDeskripsi
            // 
            this.txtDeskripsi.Location = new System.Drawing.Point(110, 120);
            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.Name = "txtDeskripsi";
            this.txtDeskripsi.Size = new System.Drawing.Size(200, 60);
            this.txtDeskripsi.TabIndex = 4;
            // 
            // numericHarga
            // 
            this.numericHarga.Location = new System.Drawing.Point(110, 190);
            this.numericHarga.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericHarga.Name = "numericHarga";
            this.numericHarga.Size = new System.Drawing.Size(200, 22);
            this.numericHarga.TabIndex = 5;
            this.numericHarga.ThousandsSeparator = true;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(110, 225);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(200, 22);
            this.txtStatus.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(220, 322);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 35);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(110, 322);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 35);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblVillaID
            // 
            this.lblVillaID.AutoSize = true;
            this.lblVillaID.Location = new System.Drawing.Point(15, 18);
            this.lblVillaID.Name = "lblVillaID";
            this.lblVillaID.Size = new System.Drawing.Size(49, 16);
            this.lblVillaID.TabIndex = 9;
            this.lblVillaID.Text = "Villa ID";
            // 
            // lblNamaVilla
            // 
            this.lblNamaVilla.AutoSize = true;
            this.lblNamaVilla.Location = new System.Drawing.Point(15, 53);
            this.lblNamaVilla.Name = "lblNamaVilla";
            this.lblNamaVilla.Size = new System.Drawing.Size(73, 16);
            this.lblNamaVilla.TabIndex = 10;
            this.lblNamaVilla.Text = "Nama Villa";
            // 
            // lblAlamatVilla
            // 
            this.lblAlamatVilla.AutoSize = true;
            this.lblAlamatVilla.Location = new System.Drawing.Point(15, 88);
            this.lblAlamatVilla.Name = "lblAlamatVilla";
            this.lblAlamatVilla.Size = new System.Drawing.Size(78, 16);
            this.lblAlamatVilla.TabIndex = 11;
            this.lblAlamatVilla.Text = "Alamat Villa";
            // 
            // lblDeskripsi
            // 
            this.lblDeskripsi.AutoSize = true;
            this.lblDeskripsi.Location = new System.Drawing.Point(15, 123);
            this.lblDeskripsi.Name = "lblDeskripsi";
            this.lblDeskripsi.Size = new System.Drawing.Size(64, 16);
            this.lblDeskripsi.TabIndex = 12;
            this.lblDeskripsi.Text = "Deskripsi";
            // 
            // lblHarga
            // 
            this.lblHarga.AutoSize = true;
            this.lblHarga.Location = new System.Drawing.Point(15, 192);
            this.lblHarga.Name = "lblHarga";
            this.lblHarga.Size = new System.Drawing.Size(45, 16);
            this.lblHarga.TabIndex = 13;
            this.lblHarga.Text = "Harga";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 228);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "Status";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 270);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 35);
            this.button1.TabIndex = 7;
            this.button1.Text = "Edit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(110, 270);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 35);
            this.button2.TabIndex = 7;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // VillaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 430);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblHarga);
            this.Controls.Add(this.lblDeskripsi);
            this.Controls.Add(this.lblAlamatVilla);
            this.Controls.Add(this.lblNamaVilla);
            this.Controls.Add(this.lblVillaID);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.numericHarga);
            this.Controls.Add(this.txtDeskripsi);
            this.Controls.Add(this.txtAlamatVilla);
            this.Controls.Add(this.txtNamaVilla);
            this.Controls.Add(this.txtVillaID);
            this.Controls.Add(this.dgvVilla);
            this.Name = "VillaForm";
            this.Text = "Villa Form - CRUD";
            ((System.ComponentModel.ISupportInitialize)(this.dgvVilla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericHarga)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
