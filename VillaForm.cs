using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Villain
{
    public partial class VillaForm : Form
    {
        private string connectionString = "Server=MSI\\RM_FAKHRI_W;Database=VillainApps;Trusted_Connection=True;";
        private bool isLoading = false;

        public VillaForm()
        {
            InitializeComponent();
            isLoading = true;
            LoadVillaData();
            ClearInputs();
            isLoading = false;
            btnSave.Enabled = false; // Disable save on start
        }

        private void ClearInputs()
        {
            txtVillaID.Clear();
            txtNamaVilla.Clear();
            txtAlamatVilla.Clear();
            txtDeskripsi.Clear();
            numericHarga.Value = numericHarga.Minimum;
            txtStatus.Clear();
        }

        private void LoadVillaData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_SelectAllVilla", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvVilla.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading villa data: " + ex.Message);
                }
            }
        }

        private void InsertVilla(string namaVilla, string alamatVilla, string deskripsi, decimal harga)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertVilla", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NamaVilla", namaVilla);
                        cmd.Parameters.AddWithValue("@AlamatVilla", alamatVilla);
                        cmd.Parameters.AddWithValue("@Deskripsi", deskripsi);
                        cmd.Parameters.AddWithValue("@Harga", harga);

                        // Jalankan perintah insert
                        cmd.ExecuteNonQuery();
                    }

                    // Jika semua perintah berhasil, commit transaksi
                    transaction.Commit();

                    MessageBox.Show("Villa berhasil ditambahkan.");
                    LoadVillaData();
                    ClearInputs();
                    btnSave.Enabled = false;
                }
                catch (Exception ex)
                {
                    // Jika error, rollback transaksi jika sudah mulai
                    try
                    {
                        transaction?.Rollback();
                    }
                    catch { /* Konfirmasi rollback agar error tidak mengganggu proses catch */ }

                    MessageBox.Show("Error inserting villa: " + ex.Message);
                }
            }
        }

        private void UpdateVilla(int villaId, string nama, string alamat, string deskripsi, decimal harga, string status)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateVilla", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@VillaID", villaId);
                        cmd.Parameters.AddWithValue("@NamaVilla", nama);
                        cmd.Parameters.AddWithValue("@AlamatVilla", alamat);
                        cmd.Parameters.AddWithValue("@Deskripsi", deskripsi);
                        cmd.Parameters.AddWithValue("@Harga", harga);
                        cmd.Parameters.AddWithValue("@StatusVilla", status);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    MessageBox.Show("Villa berhasil diupdate.");
                    LoadVillaData();
                    ClearInputs();
                    btnSave.Enabled = false;
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction?.Rollback();
                    }
                    catch { }

                    MessageBox.Show("Error updating villa: " + ex.Message);
                }
            }
        }


        private void DeleteVilla(int villaId)
        {
            var confirmResult = MessageBox.Show("Apakah Anda yakin ingin menghapus villa ini?",
                                     "Konfirmasi Hapus",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlTransaction transaction = null;

                    try
                    {
                        conn.Open();
                        transaction = conn.BeginTransaction();

                        using (SqlCommand cmd = new SqlCommand("sp_DeleteVilla", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@VillaID", villaId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        MessageBox.Show("Villa berhasil dihapus.");
                        LoadVillaData();
                        ClearInputs();
                        btnSave.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction?.Rollback();
                        }
                        catch
                        {
                            // Handle rollback exception if necessary
                        }

                        MessageBox.Show("Error deleting villa: " + ex.Message);
                    }
                }
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearInputs();
            txtVillaID.Text = "Auto generated";
            btnSave.Enabled = true;
            txtNamaVilla.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaVilla.Text))
            {
                MessageBox.Show("Nama Villa harus diisi.");
                txtNamaVilla.Focus();
                return;
            }

            int villaId;
            bool isEditMode = int.TryParse(txtVillaID.Text, out villaId);

            string nama = txtNamaVilla.Text;
            string alamat = txtAlamatVilla.Text;
            string deskripsi = txtDeskripsi.Text;
            decimal harga = numericHarga.Value;
            string status = txtStatus.Text;

            if (isEditMode)
            {
                UpdateVilla(villaId, nama, alamat, deskripsi, harga, status);
            }
            else
            {
                InsertVilla(nama, alamat, deskripsi, harga);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvVilla.SelectedRows.Count > 0)
            {
                int villaId = Convert.ToInt32(dgvVilla.SelectedRows[0].Cells["VillaID"].Value);
                DeleteVilla(villaId);
            }
            else
            {
                MessageBox.Show("Pilih data villa yang ingin dihapus.");
            }
        }

        private void dgvVilla_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading || !dgvVilla.Focused)
                return;

            if (dgvVilla.SelectedRows.Count > 0)
            {
                var row = dgvVilla.SelectedRows[0];
                txtVillaID.Text = row.Cells["VillaID"].Value.ToString();
                txtNamaVilla.Text = row.Cells["NamaVilla"].Value.ToString();
                txtAlamatVilla.Text = row.Cells["AlamatVilla"].Value.ToString();
                txtDeskripsi.Text = row.Cells["Deskripsi"].Value.ToString();
                numericHarga.Value = Convert.ToDecimal(row.Cells["Harga"].Value);

                if (dgvVilla.Columns.Contains("StatusVilla") && row.Cells["StatusVilla"].Value != null)
                    txtStatus.Text = row.Cells["StatusVilla"].Value.ToString();
                else
                    txtStatus.Text = "";

                btnSave.Enabled = true;
            }
        }
    }
}
