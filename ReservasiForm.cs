using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Villain
{
    public partial class ReservasiForm : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";

        private bool isEditMode = false;

        public ReservasiForm()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
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
            using (SqlConnection conn = new SqlConnection(strKonek))
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
            using (SqlConnection conn = new SqlConnection(strKonek))
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
            using (SqlConnection conn = new SqlConnection(strKonek))
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

        private bool ValidasiInput()
        {
            if (cmbPengunjung.SelectedIndex == -1)
            {
                MessageBox.Show("Pengunjung harus dipilih.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPengunjung.Focus();
                return false;
            }

            if (cmbVilla.SelectedIndex == -1)
            {
                MessageBox.Show("Villa harus dipilih.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbVilla.Focus();
                return false;
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Status reservasi harus dipilih.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
                return false;
            }

            if (dtpCheckOut.Value.Date <= dtpCheckIn.Value.Date)
            {
                MessageBox.Show("Tanggal check-out harus lebih besar dari check-in.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpCheckOut.Focus();
                return false;
            }

            return true;
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
            if (!ValidasiInput())
                return;

            using (SqlConnection conn = new SqlConnection(strKonek))
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

            if (!ValidasiInput())
                return;

            using (SqlConnection conn = new SqlConnection(strKonek))
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
                using (SqlConnection conn = new SqlConnection(strKonek))
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
        private void btnReport_Click(object sender, EventArgs e)
        {
            ReportReservasi formReportReservasi = new ReportReservasi();
            formReportReservasi.Show(); // Menampilkan FormKegiatan
            this.Hide(); // Menyembunyikan Form1
        }
    }
}
