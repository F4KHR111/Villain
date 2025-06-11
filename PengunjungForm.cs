using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Villain
{
    public partial class PengunjungForm : Form
    {
        private string connectionString = "Server=MSI\\RM_FAKHRI_W;Database=VillainApps;Trusted_Connection=True;";
        public PengunjungForm()
        {
            InitializeComponent();
        }

        private void PengunjungForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Pakai schema dbo jika perlu dan pastikan stored procedure ada
                    string query = "EXEC sp_SelectAllPengunjung";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPengunjung.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data Pengunjung: " + ex.Message);
            }
        }

        private void ClearInput()
        {
            txtIDPengunjung.Text = "";
            txtNamaPengunjung.Text = "";
            txtNomorHP.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaPengunjung.Text) || string.IsNullOrWhiteSpace(txtNomorHP.Text))
            {
                MessageBox.Show("Nama dan Nomor HP wajib diisi.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;

                        if (string.IsNullOrEmpty(txtIDPengunjung.Text))
                        {
                            // Insert mode
                            cmd.CommandText = "dbo.SP_InsertPengunjung";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@NamaPengunjung", txtNamaPengunjung.Text);
                            cmd.Parameters.AddWithValue("@Kontak", txtNomorHP.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data pengunjung berhasil disimpan.");
                        }
                        else
                        {
                            // Update mode
                            cmd.CommandText = "dbo.SP_UpdatePengunjung";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDPengunjung", int.Parse(txtIDPengunjung.Text));
                            cmd.Parameters.AddWithValue("@NamaPengunjung", txtNamaPengunjung.Text);
                            cmd.Parameters.AddWithValue("@Kontak", txtNomorHP.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data pengunjung berhasil diperbarui.");
                        }
                    }
                }

                LoadData();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan data: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDPengunjung.Text))
            {
                MessageBox.Show("Pilih data pengunjung yang ingin diedit terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNamaPengunjung.Text) || string.IsNullOrWhiteSpace(txtNomorHP.Text))
            {
                MessageBox.Show("Nama dan Nomor HP wajib diisi.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "sp_UpdatePengunjung";

                            cmd.Parameters.AddWithValue("@PengunjungID", int.Parse(txtIDPengunjung.Text));
                            cmd.Parameters.AddWithValue("@NamaPengunjung", txtNamaPengunjung.Text);
                            cmd.Parameters.AddWithValue("@Kontak", txtNomorHP.Text);

                            try
                            {
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                                MessageBox.Show("Data pengunjung berhasil diperbarui.");

                                // Refresh data grid dan clear input
                                LoadData();
                                ClearInput();

                                // Nonaktifkan input setelah edit selesai
                                txtNamaPengunjung.Enabled = false;
                                txtNomorHP.Enabled = false;
                            }
                            catch (SqlException ex)
                            {
                                // Rollback jika error
                                transaction.Rollback();

                                // Cek error dari SP untuk duplikasi nomor HP
                                if (ex.Number == 50000) // RAISERROR user-defined error
                                {
                                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    MessageBox.Show("Gagal memperbarui data: " + ex.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memperbarui data: " + ex.Message);
            }
        }




        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDPengunjung.Text))
            {
                MessageBox.Show("Pilih data yang ingin dihapus.");
                return;
            }

            var confirm = MessageBox.Show("Yakin mau hapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("dbo.sp_DeletePengunjung", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PengunjungID", int.Parse(txtIDPengunjung.Text));
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data pengunjung berhasil dihapus.");
                        }
                    }

                    LoadData();
                    ClearInput();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus data: " + ex.Message);
                }
            }
        }

        private void dgvPengunjung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvPengunjung.Rows.Count > e.RowIndex)
            {
                DataGridViewRow row = dgvPengunjung.Rows[e.RowIndex];
                txtIDPengunjung.Text = row.Cells["PengunjungID"].Value?.ToString() ?? "";
                txtNamaPengunjung.Text = row.Cells["NamaPengunjung"].Value?.ToString() ?? "";
                txtNomorHP.Text = row.Cells["Kontak"].Value?.ToString() ?? "";
            }
        }
    }
}
