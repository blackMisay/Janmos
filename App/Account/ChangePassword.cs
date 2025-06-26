using Core.System.Data.Model;
using Core.System.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Account
{
    public partial class ChangePassword : Form
    {
        private bool isNewPasswordHidden = true;
        private bool isReenterPasswordHidden = true;
        private protected string user = "";
        public ChangePassword(string account)
        {
            this.user = account;
            InitializeComponent();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            LoadChangePassword();
        }

        private void LoadChangePassword()
        {
            this.ActiveControl = lblConfirmPassword;
            txtNewPassword.Text = "Enter Password";
            txtNewPassword.ForeColor = Color.Gray;
            txtNewPassword.UseSystemPasswordChar = isNewPasswordHidden;

            txtReenterPassword.Text = "Reenter Password";
            txtReenterPassword.ForeColor = Color.Gray;
            txtReenterPassword.UseSystemPasswordChar = isReenterPasswordHidden;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtReenterPassword.Text))
            {
                return;
            }
            if (txtReenterPassword.Text.Equals(txtNewPassword.Text))
            {
                UserAuthentication ua = new UserAuthentication();
                string query = "SELECT `user`.username FROM user WHERE `user`.username = @User OR `user`.email = @User;";
                string username = ua.FetchAccount(query, new Dictionary<string, string>
                {
                    { "@User", this.user },
                });
                User account = new User() { Username = username, Password = txtNewPassword.Text };
                if (ua.ResetPassword(account))
                {
                    MessageBox.Show("Change Password Successful.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Error Changing Password.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Password not match.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtNewPassword_Enter(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == "Enter Password")
            {
                txtNewPassword.Text = "";
                txtNewPassword.ForeColor = Color.Black;
                txtNewPassword.PasswordChar = '*';
                txtNewPassword.UseSystemPasswordChar = !isNewPasswordHidden;
            }
        }

        private void txtNewPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                txtNewPassword.Text = "Enter Password";
                txtNewPassword.ForeColor = Color.Gray;
                txtNewPassword.UseSystemPasswordChar = isNewPasswordHidden;
            }
        }

        private void txtReenterPassword_Enter(object sender, EventArgs e)
        {
            if (txtReenterPassword.Text == "Reenter Password")
            {
                txtReenterPassword.Text = "";
                txtReenterPassword.ForeColor = Color.Black;
                txtReenterPassword.PasswordChar = '*';
                txtReenterPassword.UseSystemPasswordChar = !isReenterPasswordHidden;
            }
        }

        private void txtReenterPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReenterPassword.Text))
            {
                txtReenterPassword.Text = "Reenter Password";
                txtReenterPassword.ForeColor = Color.Gray;
                txtReenterPassword.UseSystemPasswordChar = isReenterPasswordHidden;
            }
        }

        private void btnEPShowHide_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == "Enter Password")
            {
                return;
            }
            else
            {
                if (txtNewPassword.UseSystemPasswordChar == !isNewPasswordHidden)
                {
                    txtNewPassword.UseSystemPasswordChar = isNewPasswordHidden;
                }
                else
                {
                    txtNewPassword.UseSystemPasswordChar = !isNewPasswordHidden;
                }
            }
        }

        private void btnRPShowHide_Click(object sender, EventArgs e)
        {
            if (txtReenterPassword.Text == "Reenter Password")
            {
                return;
            }
            else
            {
                if (txtReenterPassword.UseSystemPasswordChar == !isReenterPasswordHidden)
                {
                    txtReenterPassword.UseSystemPasswordChar = isReenterPasswordHidden;
                }
                else
                {
                    txtReenterPassword.UseSystemPasswordChar = !isReenterPasswordHidden;
                }
            }
        }

        private void txtNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConfirm_Click(sender, e);
            }
        }
    }
}
