using System;
using System.Windows.Forms;
using Core.System.Data.Model;
using Core.System.Security;

namespace App
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Authenticate();
        }

        private void Authenticate()
        {

            if (String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("The username you entered isn’t a valid account.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("The password you've entered is incorrect.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User Account = new User() { Username = txtUsername.Text, Password = txtPassword.Text };
            if (!UserAuthentication.IsAuthenticated(Account))
            {
                MessageBox.Show("The username or password you've entered is invalid.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Main main = new Main())
            {
                this.Hide();
                main.ShowDialog();
            }
            this.Show();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.Authenticate();
            }
        }
    }
}
