using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Villain
{
    public partial class PengunjungForm : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";
        public PengunjungForm()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
        }

        private void PengunjungForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strKonek))
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string nama = txtNamaPengunjung.Text.Trim();
            string noHP = txtNomorHP.Text.Trim();

            // Validasi nama tidak kosong dan hanya huruf serta spasi
            if (string.IsNullOrWhiteSpace(nama))
            {
                MessageBox.Show("Nama tidak boleh kosong.");
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(nama, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Nama hanya boleh berisi huruf.");
                return;
            }

            // Validasi nomor HP
            if (string.IsNullOrWhiteSpace(noHP))
            {
                MessageBox.Show("Nomor HP tidak boleh kosong.");
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(noHP, @"^\d+$"))
            {
                MessageBox.Show("Nomor HP hanya boleh berisi angka.");
                return;
            }
            if (noHP.Length < 10 || noHP.Length > 13)
            {
                MessageBox.Show("Nomor HP harus terdiri dari 10 hingga 13 digit angka.");
                return;
            }
            if (!noHP.StartsWith("08"))
            {
                MessageBox.Show("Nomor HP harus diawali dengan angka 08 (bukan +62).");
                return;
            }

            // Jika semua validasi lolos, lanjut simpan ke database
            try
            {
                using (SqlConnection conn = new SqlConnection(strKonek))
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
                            cmd.Parameters.AddWithValue("@NamaPengunjung", nama);
                            cmd.Parameters.AddWithValue("@Kontak", noHP);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data pengunjung berhasil disimpan.");
                        }
                        else
                        {
                            // Update mode
                            cmd.CommandText = "dbo.SP_UpdatePengunjung";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDPengunjung", int.Parse(txtIDPengunjung.Text));
                            cmd.Parameters.AddWithValue("@NamaPengunjung", nama);
                            cmd.Parameters.AddWithValue("@Kontak", noHP);
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

            string nama = txtNamaPengunjung.Text.Trim();
            string noHP = txtNomorHP.Text.Trim();

            // Validasi nama tidak kosong dan hanya huruf serta spasi
            if (string.IsNullOrWhiteSpace(nama))
            {
                MessageBox.Show("Nama tidak boleh kosong.");
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(nama, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Nama hanya boleh berisi huruf.");
                return;
            }

            // Validasi nomor HP
            if (string.IsNullOrWhiteSpace(noHP))
            {
                MessageBox.Show("Nomor HP tidak boleh kosong.");
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(noHP, @"^\d+$"))
            {
                MessageBox.Show("Nomor HP hanya boleh berisi angka.");
                return;
            }
            if (noHP.Length < 10 || noHP.Length > 13)
            {
                MessageBox.Show("Nomor HP harus terdiri dari 10 hingga 13 digit angka.");
                return;
            }
            if (!noHP.StartsWith("08"))
            {
                MessageBox.Show("Nomor HP harus diawali dengan angka 08 (bukan +62).");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strKonek))
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
                            cmd.Parameters.AddWithValue("@NamaPengunjung", nama);
                            cmd.Parameters.AddWithValue("@Kontak", noHP);

                            try
                            {
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                                MessageBox.Show("Data pengunjung berhasil diperbarui.");

                                LoadData();
                                ClearInput();

                                txtNamaPengunjung.Enabled = false;
                                txtNomorHP.Enabled = false;
                            }
                            catch (SqlException ex)
                            {
                                transaction.Rollback();

                                if (ex.Number == 50000) // Error dari stored procedure
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
                    using (SqlConnection conn = new SqlConnection(strKonek))
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

        // Method untuk menampilkan preview data di DataGridView
        private void PreviewData(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new XSSFWorkbook(fs); // Membuka workbook Excel
                    ISheet sheet = workbook.GetSheetAt(0);      // Mendapatkan worksheet pertama
                    DataTable dt = new DataTable();

                    // Membaca header kolom
                    IRow headerRow = sheet.GetRow(0);
                    foreach (var cell in headerRow.Cells)
                    {
                        dt.Columns.Add(cell.ToString());
                    }

                    // Membaca sisa data
                    for (int i = 1; i <= sheet.LastRowNum; i++) // Lewati baris header
                    {
                        IRow dataRow = sheet.GetRow(i);
                        DataRow newRow = dt.NewRow();
                        int cellIndex = 0;
                        foreach (var cell in dataRow.Cells)
                        {
                            newRow[cellIndex] = cell.ToString();
                            cellIndex++;
                        }
                        dt.Rows.Add(newRow);
                    }

                    // Membuka PreviewForm dan mengirimkan DataTable ke form tersebut
                    FormPreviewData previewForm = new FormPreviewData(dt);
                    previewForm.ShowDialog(); // Tampilkan PreviewForm
                    
                }
            }
            catch (Exception ex)
            {
                string userFriendlyMessage = "Terjadi kesalahan saat membaca file Excel. Pastikan file tidak kosong dan memiliki data pada baris pertama.";

                if (ex is NullReferenceException)
                {
                    MessageBox.Show(userFriendlyMessage + "\n\nDetail: " + ex.Message, "Kesalahan Baca File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        // Event untuk memilih file dan mempreview data
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xlsx;*.xlsm)|*.xlsx;*.xlsm|All Files (*.*)|*.*";
            openFileDialog.Title = "Pilih File Excel";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                PreviewData(filePath);  // Display preview before importing
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportPengunjung formReportPengunjung = new ReportPengunjung();
            formReportPengunjung.Show(); // Menampilkan FormKegiatan
            this.Hide(); // Menyembunyikan Form1

        }
    }
}
