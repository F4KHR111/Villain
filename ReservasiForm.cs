using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Villain
{
    public partial class ReservasiForm : Form
    {
        private string connectionString = "Server=MSI\\RM_FAKHRI_W;Database=VillainApps;Trusted_Connection=True;";
        
        private bool isEditMode = false;

        public ReservasiForm()
        {
            InitializeComponent();
        }

        private void ReservasiForm_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboPengunjung();
            LoadComboVilla();
            LoadStatusCombo();
            ClearInputs();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("EXEC sp_SelectAllReservasi", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvReservasi.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal load data: " + ex.Message);
                }
            }
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

        private void ClearInputs()
        {
            txtIDReservasi.Text = "";
            cmbPengunjung.SelectedIndex = -1;
            cmbVilla.SelectedIndex = -1;
            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.Value = DateTime.Today.AddDays(1);
            cmbStatus.SelectedIndex = -1;
            isEditMode = false;
        }

        private void dgvReservasi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvReservasi.Rows[e.RowIndex];

                txtIDReservasi.Text = row.Cells["ReservasiID"].Value.ToString();
                cmbPengunjung.SelectedValue = Convert.ToInt32(row.Cells["PengunjungID"].Value);
                cmbVilla.SelectedValue = Convert.ToInt32(row.Cells["VillaID"].Value);
                dtpCheckIn.Value = Convert.ToDateTime(row.Cells["TanggalCheckIn"].Value);
                dtpCheckOut.Value = Convert.ToDateTime(row.Cells["TanggalCheckOut"].Value);
                cmbStatus.SelectedItem = row.Cells["StatusReservasi"].Value.ToString();

                isEditMode = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbPengunjung.SelectedIndex == -1 || cmbVilla.SelectedIndex == -1)
            {
                MessageBox.Show("Harap pilih Pengunjung dan Villa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction(); // Mulai transaksi

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (isEditMode)
                    {
                        // Gunakan SP update jika mode edit
                        cmd.CommandText = "sp_UpdateReservasi";
                        cmd.Parameters.AddWithValue("@ReservasiID", Convert.ToInt32(txtIDReservasi.Text));
                        cmd.Parameters.AddWithValue("@PengunjungID", cmbPengunjung.SelectedValue);
                        cmd.Parameters.AddWithValue("@VillaID", cmbVilla.SelectedValue);
                        cmd.Parameters.AddWithValue("@TanggalCheckIn", dtpCheckIn.Value);
                        cmd.Parameters.AddWithValue("@TanggalCheckOut", dtpCheckOut.Value);
                        cmd.Parameters.AddWithValue("@StatusReservasi", cmbStatus.SelectedItem.ToString());
                    }
                    else
                    {
                        // Gunakan SP insert yang ada
                        cmd.CommandText = "sp_InsertReservasi";
                        cmd.Parameters.AddWithValue("@PengunjungID", cmbPengunjung.SelectedValue);
                        cmd.Parameters.AddWithValue("@VillaID", cmbVilla.SelectedValue);
                        cmd.Parameters.AddWithValue("@TanggalCheckIn", dtpCheckIn.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalCheckOut", dtpCheckOut.Value.Date);
                        cmd.Parameters.AddWithValue("@StatusReservasi", cmbStatus.SelectedItem.ToString());
                    }

                    cmd.ExecuteNonQuery(); // Jalankan SP
                    transaction.Commit(); // Commit transaksi

                    MessageBox.Show("Data berhasil disimpan.");
                    LoadData();
                    ClearInputs();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback(); // Rollback jika gagal

                    if (ex.Number == 50000) // Custom error dari RAISERROR
                    {
                        MessageBox.Show("Gagal simpan data: " + ex.Message);
                    }
                    else
                    {
                        MessageBox.Show("Terjadi kesalahan SQL: " + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Kesalahan umum: " + ex.Message);
                }
            }
        }

        private void LoadStatusCombo()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("Selesai");
            cmbStatus.Items.Add("Batal");
            cmbStatus.SelectedIndex = 0; // Atur default ke Pending
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!isEditMode)
            {
                MessageBox.Show("Pilih data dari tabel untuk diedit.");
                return;
            }

            if (cmbPengunjung.SelectedIndex == -1 || cmbVilla.SelectedIndex == -1)
            {
                MessageBox.Show("Harap pilih Pengunjung dan Villa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction(); // Mulai transaksi

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateReservasi", conn, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ReservasiID", Convert.ToInt32(txtIDReservasi.Text));
                    cmd.Parameters.AddWithValue("@PengunjungID", cmbPengunjung.SelectedValue);
                    cmd.Parameters.AddWithValue("@VillaID", cmbVilla.SelectedValue);
                    cmd.Parameters.AddWithValue("@TanggalCheckIn", dtpCheckIn.Value.Date);
                    cmd.Parameters.AddWithValue("@TanggalCheckOut", dtpCheckOut.Value.Date);
                    cmd.Parameters.AddWithValue("@StatusReservasi", cmbStatus.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                    MessageBox.Show("Data berhasil diperbarui.");
                    LoadData();
                    ClearInputs();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Gagal update data (SQL): " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Kesalahan umum: " + ex.Message);
                }
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDReservasi.Text))
            {
                MessageBox.Show("Pilih data terlebih dahulu untuk dihapus.");
                return;
            }

            var confirmResult = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?",
                                                "Konfirmasi Hapus",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        SqlCommand cmd = new SqlCommand("sp_DeleteReservasi", conn, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ReservasiID", Convert.ToInt32(txtIDReservasi.Text));

                        cmd.ExecuteNonQuery();
                        transaction.Commit();

                        MessageBox.Show("Data berhasil dihapus.");
                        LoadData();
                        ClearInputs();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Gagal menghapus data (SQL Error): " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Kesalahan umum saat menghapus: " + ex.Message);
                    }
                }
            }
        }
    }
}
