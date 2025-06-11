using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Villain
{
    public partial class FormSewa : Form
    {
        private string connectionString = "Server=MSI\\RM_FAKHRI_W;Database=VillainApps;Trusted_Connection=True;";
        
        public FormSewa()
        {
            InitializeComponent();
            LoadComboPengunjung();
            LoadComboVilla();
            LoadStatusCombo();
            LoadData();
        }


        private void LoadStatusCombo()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Lunas");
            cmbStatus.Items.Add("Belum Lunas");
            cmbStatus.SelectedIndex = 0; // Atur default ke Pending
        }

        private void clearForm()
        {
            cmbStatus.SelectedIndex = -1;
            cmbPengunjung.SelectedIndex = -1;
            cmbVilla.SelectedIndex = -1;
            txtBiaya.Clear();
            txtIDKontrak.Clear();
        }

        private void LoadComboPengunjung()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT PengunjungID, NamaPengunjung FROM Pengunjung", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cmbPengunjung.DisplayMember = "NamaPengunjung";
                    cmbPengunjung.ValueMember = "PengunjungID";
                    cmbPengunjung.DataSource = dt;
                    cmbPengunjung.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal load combo pengunjung: " + ex.Message);
                }
            }
        }

        private void LoadComboVilla()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT VillaID, NamaVilla FROM Villa", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cmbVilla.DisplayMember = "NamaVilla";
                    cmbVilla.ValueMember = "VillaID";
                    cmbVilla.DataSource = dt;
                    cmbVilla.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal load combo villa: " + ex.Message);
                }
            }
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "EXEC sp_SelectAllKontrakSewa"; // Stored Procedure untuk select semua kontrak
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKontrakSewa.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validasi input
            if (cmbPengunjung.SelectedIndex == -1 || cmbVilla.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtBiaya.Text) || cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan lengkapi semua data terlebih dahulu.");
                return;
            }

            // Validasi angka pada biaya
            if (!decimal.TryParse(txtBiaya.Text, out decimal biaya))
            {
                MessageBox.Show("Masukkan angka yang valid untuk biaya.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Cari ReservasiID berdasarkan kombinasi PengunjungID dan VillaID
                        SqlCommand cmdCariReservasi = new SqlCommand(@"
                    SELECT TOP 1 ReservasiID
                    FROM Reservasi
                    WHERE PengunjungID = @PengunjungID AND VillaID = @VillaID
                    ORDER BY TanggalCheckIn DESC", conn, transaction); // Bisa disesuaikan dengan kriteria lain

                        cmdCariReservasi.Parameters.AddWithValue("@PengunjungID", cmbPengunjung.SelectedValue);
                        cmdCariReservasi.Parameters.AddWithValue("@VillaID", cmbVilla.SelectedValue);

                        object result = cmdCariReservasi.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("Tidak ditemukan Reservasi yang sesuai untuk Pengunjung dan Villa yang dipilih.");
                            transaction.Rollback();
                            return;
                        }

                        int reservasiID = Convert.ToInt32(result);

                        // Insert ke KontrakSewa menggunakan stored procedure
                        SqlCommand cmdInsertKontrak = new SqlCommand("sp_InsertKontrakSewa", conn, transaction);
                        cmdInsertKontrak.CommandType = CommandType.StoredProcedure;

                        cmdInsertKontrak.Parameters.AddWithValue("@ReservasiID", reservasiID);
                        cmdInsertKontrak.Parameters.AddWithValue("@Biaya", biaya);
                        cmdInsertKontrak.Parameters.AddWithValue("@StatusPembayaran", cmbStatus.SelectedItem.ToString());

                        cmdInsertKontrak.ExecuteNonQuery();

                        transaction.Commit();
                        MessageBox.Show("Data kontrak berhasil ditambahkan.");

                        LoadData();
                        clearForm();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception rollbackEx)
                        {
                            MessageBox.Show("Gagal rollback transaksi: " + rollbackEx.Message);
                        }

                        MessageBox.Show("Terjadi kesalahan saat menyimpan data: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi ke database gagal: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDKontrak.Text))
            {
                MessageBox.Show("Pilih data kontrak yang akan diedit.");
                return;
            }

            if (!decimal.TryParse(txtBiaya.Text, out decimal biaya))
            {
                MessageBox.Show("Masukkan angka yang valid untuk biaya.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        SqlCommand cmd = new SqlCommand("sp_UpdateKontrakSewa", conn, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@KontrakID", int.Parse(txtIDKontrak.Text));
                        cmd.Parameters.AddWithValue("@Biaya", biaya);
                        cmd.Parameters.AddWithValue("@StatusPembayaran", cmbStatus.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                        transaction.Commit();

                        MessageBox.Show("Data berhasil diperbarui.");
                        LoadData();
                        clearForm();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Gagal update data: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi ke database gagal: " + ex.Message);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDKontrak.Text))
            {
                MessageBox.Show("Pilih data kontrak yang akan dihapus.");
                return;
            }

            if (MessageBox.Show("Yakin ingin menghapus data?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            SqlCommand cmd = new SqlCommand("sp_DeleteKontrakSewa", conn, transaction);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@KontrakID", int.Parse(txtIDKontrak.Text));

                            cmd.ExecuteNonQuery();
                            transaction.Commit();

                            MessageBox.Show("Data berhasil dihapus.");
                            LoadData();
                            clearForm();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Gagal hapus data: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Koneksi ke database gagal: " + ex.Message);
                }
            }
        }
        private void dgvKontrakSewa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKontrakSewa.Rows[e.RowIndex];
                txtIDKontrak.Text = row.Cells["KontrakID"].Value.ToString();
                cmbPengunjung.SelectedValue = row.Cells["PengunjungID"].Value;
                cmbVilla.SelectedValue = row.Cells["VillaID"].Value;
                txtBiaya.Text = row.Cells["Biaya"].Value.ToString();
                cmbStatus.SelectedItem = row.Cells["StatusPembayaran"].Value.ToString();
            }
        }
    }
}
