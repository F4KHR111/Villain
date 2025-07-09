using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;


namespace Villain
{
    public partial class FormPreviewData : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";
        public FormPreviewData(DataTable data)
        {
            InitializeComponent();
            strKonek = kn.connectionString();
            dgvPreview.DataSource = data;
        }

        private void FormPreviewData_Load(object sender, EventArgs e)
        {
            dgvPreview.AutoResizeColumns();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda ingin mengimpor data ke database?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Mengimpor data dari DataGridView ke database
                ImportDataToDatabase();
            }
        }

        private void ImportDataToDatabase()
        {
            int successCount = 0;
            int failCount = 0;
            StringBuilder logError = new StringBuilder();

            try
            {
                DataTable dt = (DataTable)dgvPreview.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    if (row == null || row.ItemArray.All(field => string.IsNullOrWhiteSpace(field?.ToString())))
                        continue;

                    try
                    {
                        using (SqlConnection conn = new SqlConnection(strKonek))
                        {
                            conn.Open();

                            if (dt.Columns.Contains("NamaVilla") && dt.Columns.Contains("AlamatVilla"))
                            {
                                // Insert ke tabel Villa
                                string query = "INSERT INTO Villa (NamaVilla, AlamatVilla, Deskripsi, Harga, StatusVilla) " +
                                               "VALUES (@NamaVilla, @AlamatVilla, @Deskripsi, @Harga, @StatusVilla)";
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@NamaVilla", row["NamaVilla"]);
                                    cmd.Parameters.AddWithValue("@AlamatVilla", row["AlamatVilla"]);
                                    cmd.Parameters.AddWithValue("@Deskripsi", row["Deskripsi"]);
                                    cmd.Parameters.AddWithValue("@Harga", Convert.ToDecimal(row["Harga"]));
                                    cmd.Parameters.AddWithValue("@StatusVilla", row["StatusVilla"]);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else if (dt.Columns.Contains("NamaPengunjung") && dt.Columns.Contains("Kontak"))
                            {
                                // Insert ke tabel Pengunjung
                                string query = "INSERT INTO Pengunjung (NamaPengunjung, Kontak) VALUES (@NamaPengunjung, @Kontak)";
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@NamaPengunjung", row["NamaPengunjung"]);
                                    cmd.Parameters.AddWithValue("@Kontak", row["Kontak"]);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else if (dt.Columns.Contains("TanggalCheckIn") && dt.Columns.Contains("TanggalCheckOut"))
                            {
                                string namaPengunjung = row["NamaPengunjung"].ToString();
                                string namaVilla = row["NamaVilla"].ToString();

                                int pengunjungID = -1;
                                int villaID = -1;

                                // Cari ID Pengunjung
                                using (SqlCommand findPengunjungCmd = new SqlCommand("SELECT PengunjungID FROM Pengunjung WHERE NamaPengunjung = @Nama", conn))
                                {
                                    findPengunjungCmd.Parameters.AddWithValue("@Nama", namaPengunjung);
                                    object result = findPengunjungCmd.ExecuteScalar();
                                    if (result != null)
                                    {
                                        pengunjungID = Convert.ToInt32(result);
                                    }
                                    else
                                    {
                                        throw new Exception($"Nama pengunjung '{namaPengunjung}' tidak ditemukan.");
                                    }
                                }

                                // Cari ID Villa
                                using (SqlCommand findVillaCmd = new SqlCommand("SELECT VillaID FROM Villa WHERE NamaVilla = @Nama", conn))
                                {
                                    findVillaCmd.Parameters.AddWithValue("@Nama", namaVilla);
                                    object result = findVillaCmd.ExecuteScalar();
                                    if (result != null)
                                    {
                                        villaID = Convert.ToInt32(result);
                                    }
                                    else
                                    {
                                        throw new Exception($"Nama villa '{namaVilla}' tidak ditemukan.");
                                    }
                                }

                                // Insert ke tabel Reservasi
                                string query = "INSERT INTO Reservasi (PengunjungID, VillaID, TanggalCheckIn, TanggalCheckOut, StatusReservasi) " +
                                               "VALUES (@PengunjungID, @VillaID, @TanggalCheckIn, @TanggalCheckOut, @StatusReservasi)";
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@PengunjungID", pengunjungID);
                                    cmd.Parameters.AddWithValue("@VillaID", villaID);
                                    cmd.Parameters.AddWithValue("@TanggalCheckIn", Convert.ToDateTime(row["TanggalCheckIn"]));
                                    cmd.Parameters.AddWithValue("@TanggalCheckOut", Convert.ToDateTime(row["TanggalCheckOut"]));
                                    cmd.Parameters.AddWithValue("@StatusReservasi", row["StatusReservasi"]);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            else if (dt.Columns.Contains("NamaPengunjung") && dt.Columns.Contains("NamaVilla") && dt.Columns.Contains("Biaya") && dt.Columns.Contains("StatusPembayaran"))
                            {
                                string namaPengunjung = row["NamaPengunjung"].ToString();
                                string namaVilla = row["NamaVilla"].ToString();

                                int pengunjungID = -1;
                                int villaID = -1;
                                int reservasiID = -1;

                                // Cari PengunjungID
                                using (SqlCommand cmdPengunjung = new SqlCommand("SELECT PengunjungID FROM Pengunjung WHERE NamaPengunjung = @Nama", conn))
                                {
                                    cmdPengunjung.Parameters.AddWithValue("@Nama", namaPengunjung);
                                    object result = cmdPengunjung.ExecuteScalar();
                                    if (result != null)
                                        pengunjungID = Convert.ToInt32(result);
                                    else
                                        throw new Exception($"Pengunjung '{namaPengunjung}' tidak ditemukan.");
                                }

                                // Cari VillaID
                                using (SqlCommand cmdVilla = new SqlCommand("SELECT VillaID FROM Villa WHERE NamaVilla = @Nama", conn))
                                {
                                    cmdVilla.Parameters.AddWithValue("@Nama", namaVilla);
                                    object result = cmdVilla.ExecuteScalar();
                                    if (result != null)
                                        villaID = Convert.ToInt32(result);
                                    else
                                        throw new Exception($"Villa '{namaVilla}' tidak ditemukan.");
                                }

                                // Cari ReservasiID berdasarkan kombinasi
                                using (SqlCommand cmdReservasi = new SqlCommand(@"
                                    SELECT TOP 1 ReservasiID FROM Reservasi
                                    WHERE PengunjungID = @PengunjungID AND VillaID = @VillaID
                                    ORDER BY TanggalCheckIn DESC", conn))
                                {
                                    cmdReservasi.Parameters.AddWithValue("@PengunjungID", pengunjungID);
                                    cmdReservasi.Parameters.AddWithValue("@VillaID", villaID);
                                    object result = cmdReservasi.ExecuteScalar();
                                    if (result != null)
                                        reservasiID = Convert.ToInt32(result);
                                    else
                                        throw new Exception($"Reservasi antara '{namaPengunjung}' dan '{namaVilla}' tidak ditemukan.");
                                }

                                // Insert ke KontrakSewa
                                string query = "INSERT INTO KontrakSewa (ReservasiID, Biaya, StatusPembayaran) VALUES (@ReservasiID, @Biaya, @StatusPembayaran)";
                                using (SqlCommand cmdInsert = new SqlCommand(query, conn))
                                {
                                    cmdInsert.Parameters.AddWithValue("@ReservasiID", reservasiID);
                                    cmdInsert.Parameters.AddWithValue("@Biaya", Convert.ToDecimal(row["Biaya"]));
                                    cmdInsert.Parameters.AddWithValue("@StatusPembayaran", row["StatusPembayaran"]);
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }

                            else
                            {
                                // Struktur kolom tidak cocok
                                throw new Exception("Struktur kolom tidak dikenali.");
                            }

                            successCount++;
                        }
                    }
                    catch (Exception innerEx)
                    {
                        failCount++;
                        logError.AppendLine($"Baris gagal: {innerEx.Message}");
                    }
                }

                string summary = $"Impor selesai.\nBerhasil: {successCount} baris\nGagal: {failCount} baris";

                if (failCount > 0)
                {
                    MessageBox.Show(summary + "\n\nDetail error:\n" + logError.ToString(), "Sebagian Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(summary, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan umum saat mengimpor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }

}
