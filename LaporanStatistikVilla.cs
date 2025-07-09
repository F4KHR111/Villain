using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace Villain
{
    public partial class LaporanStatistikVilla : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";
        public LaporanStatistikVilla()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
        }

        private void LaporanStatistikVilla_Load(object sender, EventArgs e)
        {
            comboBoxLaporan.Items.AddRange(new string[] { "-- Pilih Data --", "Laporan Data Reservasi", "Laporan Data Pemasukan" });
            comboBoxLaporan.SelectedIndex = 0;

            LoadTahun(); // Tambah tahun dari database
            comboBoxTahun.SelectedIndexChanged += comboBoxTahun_SelectedIndexChanged;
            comboBoxLaporan.SelectedIndexChanged += comboBoxLaporan_SelectedIndexChanged;

            LoadChartData("");
        }

        private void LoadTahun()
        {
            string query = "SELECT DISTINCT YEAR(TanggalCheckIn) AS Tahun FROM Reservasi ORDER BY Tahun DESC";

            using (SqlConnection conn = new SqlConnection(strKonek))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                comboBoxTahun.Items.Clear();
                comboBoxTahun.Items.Add("-- Pilih Tahun --");

                while (reader.Read())
                {
                    comboBoxTahun.Items.Add(reader["Tahun"].ToString());
                }

                comboBoxTahun.SelectedIndex = 0;
            }
        }

        private void comboBoxTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLaporan.SelectedIndex > 0)
            {
                LoadChartData(comboBoxLaporan.SelectedItem.ToString());
            }
        }

        private void comboBoxLaporan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTahun.SelectedIndex > 0)
            {
                LoadChartData(comboBoxLaporan.SelectedItem.ToString());
            }
        }

        private void LoadChartData(string filter)
        {
            chartVillain.Series.Clear();
            chartVillain.Titles.Clear();
            chartVillain.Legends.Clear();
            chartVillain.ChartAreas.Clear();

            Dictionary<string, string> bulanMap = new Dictionary<string, string>()
    {
        { "01", "Januari" }, { "02", "Februari" }, { "03", "Maret" },
        { "04", "April" }, { "05", "Mei" }, { "06", "Juni" },
        { "07", "Juli" }, { "08", "Agustus" }, { "09", "September" },
        { "10", "Oktober" }, { "11", "November" }, { "12", "Desember" }
    };

            ChartArea ca = new ChartArea("MainArea");
            ca.AxisX.Title = "Bulan";
            ca.AxisY.Title = filter == "Laporan Data Pemasukan" ? "Pemasukan (Rp)" : "Jumlah Reservasi";
            ca.AxisX.LabelStyle.Angle = -45;
            ca.AxisX.Interval = 1;
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.LabelStyle.Format = "N0";
            ca.AxisY.MajorGrid.LineColor = Color.LightGray;
            ca.BackColor = Color.LightGoldenrodYellow;

            if (filter == "Laporan Data Reservasi")
            {
                ca.AxisY.Maximum = 100;
                ca.AxisY.Interval = 10;
            }
            else if (filter == "Laporan Data Pemasukan")
            {
                ca.AxisY.Maximum = 100_000_000;
                ca.AxisY.Interval = 10_000_000;
            }

            chartVillain.ChartAreas.Add(ca);

            string selectedYear = comboBoxTahun.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedYear) || selectedYear == "-- Pilih Tahun --")
                return;

            // 🛠️ FIXED: Declare the query string variable here
            string query = "";

            if (filter == "Laporan Data Reservasi")
            {
                query = $@"
            SELECT 
                FORMAT(TanggalCheckIn, 'MM') AS Bulan,
                COUNT(*) AS Jumlah
            FROM Reservasi
            WHERE YEAR(TanggalCheckIn) = {selectedYear}
            GROUP BY FORMAT(TanggalCheckIn, 'MM')";
            }
            else if (filter == "Laporan Data Pemasukan")
            {
                query = $@"
            SELECT 
                FORMAT(R.TanggalCheckIn, 'MM') AS Bulan,
                SUM(K.Biaya) AS Pemasukan
            FROM KontrakSewa K
            JOIN Reservasi R ON K.ReservasiID = R.ReservasiID
            WHERE K.StatusPembayaran = 'Lunas'
            AND YEAR(R.TanggalCheckIn) = {selectedYear}
            GROUP BY FORMAT(R.TanggalCheckIn, 'MM')";
            }
            else
            {
                return;
            }

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(strKonek))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            Dictionary<string, decimal> dataPerBulan = bulanMap.ToDictionary(b => b.Value, b => 0m);

            foreach (DataRow row in dt.Rows)
            {
                string kodeBulan = row["Bulan"].ToString();
                string namaBulan = bulanMap.ContainsKey(kodeBulan) ? bulanMap[kodeBulan] : kodeBulan;

                if (filter == "Laporan Data Reservasi")
                    dataPerBulan[namaBulan] = Convert.ToInt32(row["Jumlah"]);
                else if (filter == "Laporan Data Pemasukan")
                    dataPerBulan[namaBulan] = Convert.ToDecimal(row["Pemasukan"]);
            }

            Series series = new Series(filter == "Laporan Data Pemasukan" ? "Pemasukan" : "Reservasi")
            {
                ChartType = SeriesChartType.Column,
                Color = filter == "Laporan Data Pemasukan" ? Color.ForestGreen : Color.Firebrick,
                Font = new Font("Arial", 9, FontStyle.Bold),
                LabelForeColor = Color.Black
            };

            foreach (var bulan in bulanMap.Values)
            {
                int pointIndex = series.Points.AddXY(bulan, dataPerBulan[bulan]);

                if (filter == "Laporan Data Pemasukan")
                {
                    series.Points[pointIndex].Label = dataPerBulan[bulan].ToString("N0");
                    series.Points[pointIndex].LabelAngle = -90;
                }
                else
                {
                    series.IsValueShownAsLabel = true;
                    series.LabelFormat = "N0";
                }
            }

            chartVillain.Series.Add(series);
            chartVillain.Titles.Add("Grafik Laporan Statistika Villain");
            chartVillain.Legends.Add(new Legend("Legenda"));
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
