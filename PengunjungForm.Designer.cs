namespace Villain
{
    partial class PengunjungForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.dgvPengunjung = new System.Windows.Forms.DataGridView();
            this.txtIDPengunjung = new System.Windows.Forms.TextBox();
            this.txtNamaPengunjung = new System.Windows.Forms.TextBox();
            this.txtNomorHP = new System.Windows.Forms.TextBox();
            this.lblIDPengunjung = new System.Windows.Forms.Label();
            this.lblNamaPengunjung = new System.Windows.Forms.Label();
            this.lblNomorHP = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPengunjung)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPengunjung
            // 
            this.dgvPengunjung.AllowUserToAddRows = false;
            this.dgvPengunjung.AllowUserToDeleteRows = false;
            this.dgvPengunjung.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPengunjung.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPengunjung.Location = new System.Drawing.Point(12, 12);
            this.dgvPengunjung.MultiSelect = false;
            this.dgvPengunjung.Name = "dgvPengunjung";
            this.dgvPengunjung.ReadOnly = true;
            this.dgvPengunjung.RowHeadersWidth = 51;
            this.dgvPengunjung.RowTemplate.Height = 24;
            this.dgvPengunjung.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPengunjung.Size = new System.Drawing.Size(560, 200);
            this.dgvPengunjung.TabIndex = 0;
            this.dgvPengunjung.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPengunjung_CellClick);
            // 
            // txtIDPengunjung
            // 
            this.txtIDPengunjung.Location = new System.Drawing.Point(154, 230);
            this.txtIDPengunjung.Name = "txtIDPengunjung";
            this.txtIDPengunjung.ReadOnly = true;
            this.txtIDPengunjung.Size = new System.Drawing.Size(150, 22);
            this.txtIDPengunjung.TabIndex = 1;
            // 
            // txtNamaPengunjung
            // 
            this.txtNamaPengunjung.Location = new System.Drawing.Point(154, 270);
            this.txtNamaPengunjung.Name = "txtNamaPengunjung";
            this.txtNamaPengunjung.Size = new System.Drawing.Size(250, 22);
            this.txtNamaPengunjung.TabIndex = 2;
            // 
            // txtNomorHP
            // 
            this.txtNomorHP.Location = new System.Drawing.Point(154, 310);
            this.txtNomorHP.Name = "txtNomorHP";
            this.txtNomorHP.Size = new System.Drawing.Size(200, 22);
            this.txtNomorHP.TabIndex = 3;
            // 
            // lblIDPengunjung
            // 
            this.lblIDPengunjung.AutoSize = true;
            this.lblIDPengunjung.Location = new System.Drawing.Point(30, 233);
            this.lblIDPengunjung.Name = "lblIDPengunjung";
            this.lblIDPengunjung.Size = new System.Drawing.Size(94, 16);
            this.lblIDPengunjung.TabIndex = 4;
            this.lblIDPengunjung.Text = "ID Pengunjung";
            // 
            // lblNamaPengunjung
            // 
            this.lblNamaPengunjung.AutoSize = true;
            this.lblNamaPengunjung.Location = new System.Drawing.Point(30, 273);
            this.lblNamaPengunjung.Name = "lblNamaPengunjung";
            this.lblNamaPengunjung.Size = new System.Drawing.Size(118, 16);
            this.lblNamaPengunjung.TabIndex = 5;
            this.lblNamaPengunjung.Text = "Nama Pengunjung";
            // 
            // lblNomorHP
            // 
            this.lblNomorHP.AutoSize = true;
            this.lblNomorHP.Location = new System.Drawing.Point(30, 313);
            this.lblNomorHP.Name = "lblNomorHP";
            this.lblNomorHP.Size = new System.Drawing.Size(101, 16);
            this.lblNomorHP.TabIndex = 6;
            this.lblNomorHP.Text = "No. HP / Kontak";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(154, 350);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(244, 350);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(334, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 30);
            this.button1.TabIndex = 8;
            this.button1.Text = "Edit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(425, 350);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 30);
            this.button2.TabIndex = 8;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // PengunjungForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 401);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblNomorHP);
            this.Controls.Add(this.lblNamaPengunjung);
            this.Controls.Add(this.lblIDPengunjung);
            this.Controls.Add(this.txtNomorHP);
            this.Controls.Add(this.txtNamaPengunjung);
            this.Controls.Add(this.txtIDPengunjung);
            this.Controls.Add(this.dgvPengunjung);
            this.Name = "PengunjungForm";
            this.Text = "Pengunjung Form - CRUD";
            this.Load += new System.EventHandler(this.PengunjungForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPengunjung)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPengunjung;
        private System.Windows.Forms.TextBox txtIDPengunjung;
        private System.Windows.Forms.TextBox txtNamaPengunjung;
        private System.Windows.Forms.TextBox txtNomorHP;
        private System.Windows.Forms.Label lblIDPengunjung;
        private System.Windows.Forms.Label lblNamaPengunjung;
        private System.Windows.Forms.Label lblNomorHP;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
