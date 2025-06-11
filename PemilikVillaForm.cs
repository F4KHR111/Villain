using System;
using System.Windows.Forms;

namespace Villain
{
    public partial class PemilikVillaForm : Form
    {
        public PemilikVillaForm()
        {
            InitializeComponent();
        }

        private void PemilikVillaForm_Load(object sender, EventArgs e)
        {
            // Bisa tempatkan inisialisasi awal disini jika perlu
        }

        private void btnPengunjung_Click(object sender, EventArgs e)
        {
            PengunjungForm pengunjungForm = new PengunjungForm();
            pengunjungForm.ShowDialog();
        }

        private void btnReservasi_Click(object sender, EventArgs e)
        {
            ReservasiForm reservasiForm = new ReservasiForm();
            reservasiForm.ShowDialog();
        }

        private void btnKontrakSewa_Click(object sender, EventArgs e)
        {
            FormSewa formSewa = new FormSewa();
            formSewa.ShowDialog();
        }


    }
}
