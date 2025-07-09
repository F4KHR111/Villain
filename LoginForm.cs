using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace Villain
{
    public partial class LoginForm : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";
        public LoginForm()
        {
            InitializeComponent();
            strKonek = kn.connectionString();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Event load form. Bisa dikosongkan.
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();


            string query = "SELECT Role FROM Users WHERE Username = @username AND Password = @password";


            using (SqlConnection conn = new SqlConnection(strKonek))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string role = result.ToString();
                    if (role == "Admin")
                    {
                        AdminForm adminForm = new AdminForm();
                        adminForm.Show();
                        this.Hide();
                    }
                    else if (role == "PemilikVilla")
                    {
                        PemilikVillaForm pemilikForm = new PemilikVillaForm();
                        pemilikForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Role tidak dikenali.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Username atau password salah.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}