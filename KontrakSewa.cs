using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Villain
{
    public partial class KontrakSewaForm : Form
    {
        private string connectionString = "Server=MSI\\RM_FAKHRI_W;Database=VillainApps;Trusted_Connection=True;";
        private bool isEditMode = false;
        private bool isAddMode = false;

        public KontrakSewaForm()
        {
            InitializeComponent();
            LoadData();
            LoadComboPengunjung();
            LoadComboVilla();
            LoadStatusCombo();
        }



        private void LoadStatusCombo()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Lunas");
            cmbStatus.Items.Add("Belum Lunas");
            cmbStatus.SelectedIndex = 0; // Atur default ke Pending
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
        private void KontrakSewaForm_Load(object sender, EventArgs e)
        {
            // Misalnya kamu mau load data dari database
            // LoadKontrakSewaData();  <-- jika kamu punya fungsi ini
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
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data: " + ex.Message);
            }
        }

        private void ClearInputs()
        {
            txtIDKontrak.Text = "";
            cmbPengunjung.SelectedIndex = -1;
            cmbVilla.SelectedIndex = -1;
            txtBiaya.Text = "";
            cmbStatus.SelectedIndex = -1;
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!isAddMode)
                return;

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
                        ClearInputs();
                        EnableInputs(false);
                        btnSave.Enabled = false;
                        isAddMode = false;
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
            using (SqlConnection conn = new SqlConnection(connectionString)) { 
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteKontrakSewa", conn);
                if (string.IsNullOrEmpty(txtIDKontrak.Text))
                {
                    MessageBox.Show("Pilih data kontrak yang akan diedit.");
                    return;
                }
                cmd = new SqlCommand("sp_UpdateKontrakSewa", conn);
                cmd.Parameters.AddWithValue("@KontrakID", int.Parse(txtIDKontrak.Text));
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
                        SqlCommand cmd = new SqlCommand("sp_DeleteKontrakSewa", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@KontrakID", int.Parse(txtIDKontrak.Text));
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Data berhasil dihapus.");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal hapus data: " + ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs() == false)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd;
                    if (isAddMode)
                    {
                        cmd = new SqlCommand("sp_InsertKontrakSewa", conn);
                    }
                    else if (isEditMode)
                    {
                        cmd = new SqlCommand("sp_UpdateKontrakSewa", conn);
                        cmd.Parameters.AddWithValue("@KontrakID", int.Parse(txtIDKontrak.Text));
                    }
                    else
                    {
                        MessageBox.Show("Tidak ada aksi yang dilakukan.");
                        return;
                    }

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PengunjungID", (int)cmbPengunjung.SelectedValue);
                    cmd.Parameters.AddWithValue("@VillaID", (int)cmbVilla.SelectedValue);
                    cmd.Parameters.AddWithValue("@Biaya", decimal.Parse(txtBiaya.Text));
                    cmd.Parameters.AddWithValue("@StatusPembayaran", cmbStatus.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Data berhasil disimpan.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal simpan data: " + ex.Message);
            }
        }

        private bool ValidateInputs()
        {
            if (cmbPengunjung.SelectedIndex < 0)
            {
                MessageBox.Show("Silakan pilih pengunjung.");
                return false;
            }
            if (cmbVilla.SelectedIndex < 0)
            {
                MessageBox.Show("Silakan pilih villa.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBiaya.Text) || !decimal.TryParse(txtBiaya.Text, out _))
            {
                MessageBox.Show("Biaya harus diisi dengan angka valid.");
                return false;
            }
            if (cmbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Silakan pilih status.");
                return false;
            }
            return true;
        }

        private void EnableInputs(bool enable)
        {
            cmbPengunjung.Enabled = enable;
            cmbVilla.Enabled = enable;
            txtBiaya.ReadOnly = enable;
            cmbStatus.Enabled = enable;
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

                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = false;
                isAddMode = false;
                isEditMode = false;
                EnableInputs(false);
            }
        }
    }
}
