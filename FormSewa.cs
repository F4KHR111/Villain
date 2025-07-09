using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
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

namespace Villain
{
    public partial class FormSewa : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";

        public FormSewa()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
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

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strKonek))
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
                using (SqlConnection conn = new SqlConnection(strKonek))
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
                using (SqlConnection conn = new SqlConnection(strKonek))
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
                    using (SqlConnection conn = new SqlConnection(strKonek))
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
            ReportSewa formReportSewa = new ReportSewa();
            formReportSewa.Show(); // Menampilkan FormKegiatan
            this.Hide(); // Menyembunyikan Form1
        }

    }
}
