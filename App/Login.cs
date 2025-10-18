using System;
using System.Windows.Forms;
using App.Account;
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
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            User Account = new User() { Username = txtUsername.Text, Password = txtPassword.Text };
            
            //UserAuthentication ua = new UserAuthentication();
            //ua.CreateUserAccountHardCoded(Account); This is only use to create new account hardcodedly.

            if (!UserAuthentication.IsAuthenticated(Account))
            {
                MessageBox.Show("The username or password you've entered is invalid.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            User user = new User();
            UserAuthentication ua = new UserAuthentication();
            user = ua.FetchUserData(Account.Username);

            using (Main main = new Main(user))
            {
                this.Hide();
                AuditManager.Log(user, ActionType.LOGIN, description: "The user logged into system.");
                main.ShowDialog();
                txtPassword.Clear();
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

        private void Login_Load(object sender, EventArgs e)
        {
            LoadLoginForm();
        }
        private void LoadLoginForm()
        {
            txtPassword.Clear();
        }

        private void linkResetPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FindAccount find = new FindAccount();
            find.ShowDialog();
            LoadLoginForm();
        }

        private void btnLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
            }
        }
    }
}
