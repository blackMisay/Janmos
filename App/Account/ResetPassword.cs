using Core.System.Security;
using System;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using System.Timers;

namespace App.Account
{
    public partial class ResetPassword : Form
    {
        private protected string account = "";
        public ResetPassword(string user)
        {
            InitializeComponent();
            this.account = user;
        }

        UserAuthentication security = new UserAuthentication();
        private protected int generatedSecurityCode = 0;



        private void ResetPassword_Load(object sender, EventArgs e)
        {
            CreateOneTimeSecurityCode();
        }

        private void CreateOneTimeSecurityCode()
        {
            this.generatedSecurityCode = security.GenerateSecurityCode();
            EmailSecurityCode();

            StartSecurityCodeTimer();
        }

        int seconds = 30;

        System.Timers.Timer timer = new System.Timers.Timer();
        private void StartSecurityCodeTimer()
        {
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
                {
                    seconds--;
                    lblCountdownTimer.Text = "00:" + seconds;
                    if (seconds < 10)
                    {
                        lblCountdownTimer.Text = "00:0" + seconds;
                        if (seconds == 0)
                        {
                            btnResendCode.Visible = true;
                            timer.Stop();
                        }
                    }
                }
                ));
        }

        private void EmailSecurityCode()
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("userbackup01@gmail.com", "gtae xinf spwd sdmn"),
                EnableSsl = true
            };
            client.Send("userbackup01@gmail.com", this.account, "Account Reset Code", "This is your security code: " + this.generatedSecurityCode.ToString());
            client.Dispose();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtSecurityCode.Text.Equals(this.generatedSecurityCode.ToString()))
            {
                timer.Stop();
                using (ChangePassword changePassword = new ChangePassword(this.account))
                {
                    changePassword.ShowDialog();
                }
                // Reset Password
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Invalid Code", "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            txtSecurityCode.Focus();
        }

        private void btnResendCode_Click(object sender, EventArgs e)
        {
            this.generatedSecurityCode = security.GenerateSecurityCode();
            EmailSecurityCode();

            this.seconds = 30;
            timer.Start();

            btnResendCode.Visible = false;
            txtSecurityCode.Focus();
        }

        private void txtSecurityCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnVerify_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                btnResendCode_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
