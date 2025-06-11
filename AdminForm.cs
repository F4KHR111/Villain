using System;
using System.Windows.Forms;

namespace Villain
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void btnVilla_Click(object sender, EventArgs e)
        {
            VillaForm villaForm = new VillaForm();
            villaForm.ShowDialog();
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
