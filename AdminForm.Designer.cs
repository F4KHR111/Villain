namespace Villain
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnVilla;
        private System.Windows.Forms.Button btnPengunjung;
        private System.Windows.Forms.Button btnReservasi;
        private System.Windows.Forms.Button btnKontrakSewa;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnVilla = new System.Windows.Forms.Button();
            this.btnPengunjung = new System.Windows.Forms.Button();
            this.btnReservasi = new System.Windows.Forms.Button();
            this.btnKontrakSewa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnVilla
            // 
            this.btnVilla.Location = new System.Drawing.Point(50, 30);
            this.btnVilla.Name = "btnVilla";
            this.btnVilla.Size = new System.Drawing.Size(150, 100);
            this.btnVilla.TabIndex = 0;
            this.btnVilla.Text = "Villa";
            this.btnVilla.UseVisualStyleBackColor = true;
            this.btnVilla.Click += new System.EventHandler(this.btnVilla_Click);
            // 
            // btnPengunjung
            // 
            this.btnPengunjung.Location = new System.Drawing.Point(230, 30);
            this.btnPengunjung.Name = "btnPengunjung";
            this.btnPengunjung.Size = new System.Drawing.Size(150, 100);
            this.btnPengunjung.TabIndex = 1;
            this.btnPengunjung.Text = "Pengunjung";
            this.btnPengunjung.UseVisualStyleBackColor = true;
            this.btnPengunjung.Click += new System.EventHandler(this.btnPengunjung_Click);
            // 
            // btnReservasi
            // 
            this.btnReservasi.Location = new System.Drawing.Point(50, 160);
            this.btnReservasi.Name = "btnReservasi";
            this.btnReservasi.Size = new System.Drawing.Size(150, 100);
            this.btnReservasi.TabIndex = 2;
            this.btnReservasi.Text = "Reservasi";
            this.btnReservasi.UseVisualStyleBackColor = true;
            this.btnReservasi.Click += new System.EventHandler(this.btnReservasi_Click);
            // 
            // btnKontrakSewa
            // 
            this.btnKontrakSewa.Location = new System.Drawing.Point(230, 160);
            this.btnKontrakSewa.Name = "btnKontrakSewa";
            this.btnKontrakSewa.Size = new System.Drawing.Size(150, 100);
            this.btnKontrakSewa.TabIndex = 3;
            this.btnKontrakSewa.Text = "Kontrak Sewa";
            this.btnKontrakSewa.UseVisualStyleBackColor = true;
            this.btnKontrakSewa.Click += new System.EventHandler(this.btnKontrakSewa_Click);
            // 
            // AdminForm
            // 
            this.ClientSize = new System.Drawing.Size(434, 311);
            this.Controls.Add(this.btnKontrakSewa);
            this.Controls.Add(this.btnReservasi);
            this.Controls.Add(this.btnPengunjung);
            this.Controls.Add(this.btnVilla);
            this.Name = "AdminForm";
            this.Text = "Admin Form";
            this.ResumeLayout(false);
        }
    }
}
