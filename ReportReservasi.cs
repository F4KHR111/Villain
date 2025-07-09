using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Villain
{
    public partial class ReportReservasi : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";
        public ReportReservasi()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
        }

        private void ReportReservasi_Load(object sender, EventArgs e)
        {
            TampilkanLaporanReservasi();
        }

        private void TampilkanLaporanReservasi()
        {

            using (SqlConnection conn = new SqlConnection(strKonek))
            {
                SqlCommand cmd = new SqlCommand("sp_SelectAllReservasi", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Sesuaikan path ini dengan lokasi .rdlc kamu
                string reportPath = System.IO.Path.Combine(Application.StartupPath, "ReportReservasi.rdlc");
                reportViewer1.LocalReport.ReportPath = reportPath;


                reportViewer1.RefreshReport();
            }
        }
    }
}
