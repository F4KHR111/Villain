namespace Villain
{
    partial class PemilikVillaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnPengunjung;
        private System.Windows.Forms.Button btnReservasi;
        private System.Windows.Forms.Button btnKontrakSewa;

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
            this.btnPengunjung = new System.Windows.Forms.Button();
            this.btnReservasi = new System.Windows.Forms.Button();
            this.btnKontrakSewa = new System.Windows.Forms.Button();
            this.btnGrafik = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPengunjung
            // 
            this.btnPengunjung.Location = new System.Drawing.Point(27, 24);
            this.btnPengunjung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPengunjung.Name = "btnPengunjung";
            this.btnPengunjung.Size = new System.Drawing.Size(133, 64);
            this.btnPengunjung.TabIndex = 0;
            this.btnPengunjung.Text = "Pengunjung";
            this.btnPengunjung.UseVisualStyleBackColor = true;
            this.btnPengunjung.Click += new System.EventHandler(this.btnPengunjung_Click);
            // 
            // btnReservasi
            // 
            this.btnReservasi.Location = new System.Drawing.Point(178, 24);
            this.btnReservasi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReservasi.Name = "btnReservasi";
            this.btnReservasi.Size = new System.Drawing.Size(133, 64);
            this.btnReservasi.TabIndex = 1;
            this.btnReservasi.Text = "Reservasi";
            this.btnReservasi.UseVisualStyleBackColor = true;
            this.btnReservasi.Click += new System.EventHandler(this.btnReservasi_Click);
            // 
            // btnKontrakSewa
            // 
            this.btnKontrakSewa.Location = new System.Drawing.Point(329, 24);
            this.btnKontrakSewa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnKontrakSewa.Name = "btnKontrakSewa";
            this.btnKontrakSewa.Size = new System.Drawing.Size(133, 64);
            this.btnKontrakSewa.TabIndex = 2;
            this.btnKontrakSewa.Text = "Kontrak Sewa";
            this.btnKontrakSewa.UseVisualStyleBackColor = true;
            this.btnKontrakSewa.Click += new System.EventHandler(this.btnKontrakSewa_Click);
            // 
            // btnGrafik
            // 
            this.btnGrafik.Location = new System.Drawing.Point(27, 100);
            this.btnGrafik.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGrafik.Name = "btnGrafik";
            this.btnGrafik.Size = new System.Drawing.Size(435, 47);
            this.btnGrafik.TabIndex = 2;
            this.btnGrafik.Text = "Lihat Grafik Laporan";
            this.btnGrafik.UseVisualStyleBackColor = true;
            this.btnGrafik.Click += new System.EventHandler(this.btnGrafik_Click);
            // 
            // PemilikVillaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 160);
            this.Controls.Add(this.btnGrafik);
            this.Controls.Add(this.btnKontrakSewa);
            this.Controls.Add(this.btnReservasi);
            this.Controls.Add(this.btnPengunjung);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "PemilikVillaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pemilik Villa Form";
            this.Load += new System.EventHandler(this.PemilikVillaForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGrafik;
    }
}
