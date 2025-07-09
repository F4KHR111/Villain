using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Villain
{
    public partial class ReportSewa : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";
        public ReportSewa()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
        }

        private void ReportSewa_Load(object sender, EventArgs e)
        {
            TampilkanLaporanSewa();
        }

        private void TampilkanLaporanSewa()
        {

            using (SqlConnection conn = new SqlConnection(strKonek))
            {
                SqlCommand cmd = new SqlCommand("sp_SelectAllKontrakSewa", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ReportDataSource rds = new ReportDataSource("DataSetSewa", dt);

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Sesuaikan path ini dengan lokasi .rdlc kamu
                string reportPath = System.IO.Path.Combine(Application.StartupPath, "Sewa.rdlc");
                reportViewer1.LocalReport.ReportPath = reportPath;


                reportViewer1.RefreshReport();
            }
        }

        private void ReportSewa_Load_1(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
