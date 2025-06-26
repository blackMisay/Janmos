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
    public partial class FindAccount : Form
    {

        public FindAccount()
        {
            InitializeComponent();
            txtFindAccount.KeyPress += new KeyPressEventHandler(txtFindAccount_KeyPress);
            txtFindAccount.KeyDown += new KeyEventHandler(txtFindAccount_KeyDown);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lblRequired.Visible = false;
            UserAuthentication ua = new UserAuthentication();
            if (!string.IsNullOrWhiteSpace(txtFindAccount.Text))
            {
                if (ua.ValidateUsername(txtFindAccount.Text))
                {
                    string user = txtFindAccount.Text;
                    string query = "SELECT ui.email FROM `user` u JOIN userinfo ui ON u.userinfoid = ui.id WHERE u.username = @User OR ui.email = @User;";
                    string account = ua.FetchAccount(query, new Dictionary<string, string>
                    {
                        { "@User", user },
                    });
                    if (!string.IsNullOrEmpty(account))
                    {
                        ResetPassword reset = new ResetPassword(account);
                        reset.ShowDialog();
                        this.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Code Sent.", "Security Code");
                    this.Close();
                }
            }
            else
            {
                lblRequired.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtFindAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender,e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void txtFindAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }
    }
}
