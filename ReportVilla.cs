using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MathNet.Numerics;
using Microsoft.Reporting.WinForms;
using NPOI.POIFS.Crypt.Dsig;
using NPOI.SS.Formula.Functions;
using Villain;
using System.IO;
using static NPOI.SS.Formula.PTG.ArrayPtg;

namespace Villain

{
    public partial class ReportVilla : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";
        public ReportVilla()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
        }

        private void ReportVilla_Load(object sender, EventArgs e)
        {
            // Setup ReportViewer data
            SetupReportViewer();
            // Refresh report to display data
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {

            // SQL query to retrieve the required data from the database
            string query = @"
                SELECT VillaID, NamaVilla, AlamatVilla, Deskripsi, Harga, StatusVilla
                FROM Villa";


            // Create a DataTable to store the data
            DataTable dt = new DataTable();

            // Use SqlDataAdapter to fill the DataTable with data from the database
            using (SqlConnection conn = new SqlConnection(strKonek))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            // Create a ReportDataSource
            ReportDataSource rds = new ReportDataSource("DataSet1", dt); // Make sure "DataSet1" matches your RDLC dataset name

            // Clear any existing data sources and add the new data source
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Set the path to the report (.rdlc file)
            // Change this to the actual path of your RDLC file
            string reportPath = System.IO.Path.Combine(Application.StartupPath, "VillaReport.rdlc");
            reportViewer1.LocalReport.ReportPath = reportPath;


            // Refresh the ReportViewer to show the updated report
            reportViewer1.RefreshReport();
        }


    }
}
