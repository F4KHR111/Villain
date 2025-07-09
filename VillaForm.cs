using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Villain
{
    public partial class VillaForm : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";
        private bool isLoading = false;

        public VillaForm()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
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
            using (SqlConnection conn = new SqlConnection(strKonek))
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
            using (SqlConnection conn = new SqlConnection(strKonek))
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
            using (SqlConnection conn = new SqlConnection(strKonek))
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
                using (SqlConnection conn = new SqlConnection(strKonek))
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
            // Validasi input
            if (string.IsNullOrWhiteSpace(txtNamaVilla.Text))
            {
                MessageBox.Show("Nama Villa harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamaVilla.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAlamatVilla.Text))
            {
                MessageBox.Show("Alamat Villa harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAlamatVilla.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDeskripsi.Text))
            {
                MessageBox.Show("Deskripsi Villa harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDeskripsi.Focus();
                return;
            }

            if (numericHarga.Value <= 0)
            {
                MessageBox.Show("Harga Villa harus lebih dari 0.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericHarga.Focus();
                return;
            }

            // Proses simpan (insert atau update)
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

        // Method untuk menampilkan preview data di DataGridView
        private void PreviewData(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("File tidak ditemukan. Pastikan file path benar.", "File Tidak Ditemukan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new XSSFWorkbook(fs);
                    ISheet sheet = workbook.GetSheetAt(0);
                    DataTable dt = new DataTable();

                    if (sheet.PhysicalNumberOfRows == 0)
                    {
                        MessageBox.Show("File Excel kosong. Tidak ada data untuk ditampilkan.", "Kosong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    IRow headerRow = sheet.GetRow(0);
                    if (headerRow == null || headerRow.Cells.Count == 0)
                    {
                        MessageBox.Show("Baris header (baris pertama) kosong atau tidak terbaca.", "Header Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Baca nama kolom
                    foreach (var cell in headerRow.Cells)
                    {
                        string columnName = cell?.ToString()?.Trim();
                        if (string.IsNullOrEmpty(columnName))
                            columnName = $"Kolom_{dt.Columns.Count + 1}";
                        dt.Columns.Add(columnName);
                    }

                    // Baca data isi
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null || row.Cells.All(c => string.IsNullOrWhiteSpace(c?.ToString())))
                            continue; // Lewati baris kosong

                        if (row.Cells.Count > dt.Columns.Count)
                        {
                            MessageBox.Show($"Baris ke-{i + 1} memiliki lebih banyak kolom daripada header. Periksa kembali struktur file Excel.", "Jumlah Kolom Tidak Sesuai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        DataRow dataRow = dt.NewRow();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            ICell cell = row.GetCell(j);
                            dataRow[j] = cell != null ? cell.ToString() : "";
                        }

                        dt.Rows.Add(dataRow);
                    }

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Tidak ada data yang valid ditemukan setelah membaca file.", "Data Kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Tampilkan Preview
                    FormPreviewData previewForm = new FormPreviewData(dt);
                    previewForm.ShowDialog();
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show("Gagal membuka file. Pastikan file tidak sedang digunakan oleh program lain.\n\nDetail: " + ioEx.Message,
                                "Kesalahan Akses File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                MessageBox.Show("Tidak memiliki izin untuk mengakses file. Jalankan aplikasi sebagai administrator.\n\nDetail: " + uaEx.Message,
                                "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidDataException idEx)
            {
                MessageBox.Show("Format file tidak valid. Pastikan file adalah file Excel (.xlsx).\n\nDetail: " + idEx.Message,
                                "Format File Salah", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat memproses file.\n\nDetail: " + ex.Message,
                                "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ReportVilla formReportVilla = new ReportVilla();
            formReportVilla.Show(); // Menampilkan FormKegiatan
            this.Hide(); // Menyembunyikan Form1
        }
    }
}
