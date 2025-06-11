using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Villain
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Event load form. Bisa dikosongkan.
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            string connectionString = "Data Source=MSI\\RM_FAKHRI_W;Initial Catalog=VillainApps;Integrated Security=True;";

            string query = "SELECT Role FROM Users WHERE Username = @username AND Password = @password";

            using (SqlConnection conn = new SqlConnection(connectionString))
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