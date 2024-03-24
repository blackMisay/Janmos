using System;
using System.Drawing;
using System.Windows.Forms;

namespace App
{
    public partial class Main : Form
    {

        private static readonly string DASHBOARD = "&Dashboard";
        private static readonly string PRODUCT = "&Product";
        private static readonly string INVENTORY = "&Inventory";
        private static readonly string CUSTOMER = "&Customer";
        private static readonly string SUPPLIER = "&Supplier";
        private static readonly string REPORTS = "&Reports";
        private static readonly string ACCOUNT = "Pro&file";
        private static readonly string SETTING = "Se&ttings";

        private static readonly int MIN_WIDTH = 70;
        private static readonly int MAX_WIDTH = 230;

        private Form activeFormModule = null;

        public Main()
        {
            InitializeComponent();
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            this.ToggleMenu();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.openFormModule(new App.Dashboard.Dashboard());
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            this.openFormModule(new App.Product.frmProduct());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            this.openFormModule(new App.Customer.Customer());
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            this.openFormModule(new Supplier.frmSupplier());
        }

        private void openFormModule(Form formModule)
        {
            if (!(activeFormModule == null))
            {
                activeFormModule.Close();
            }

            activeFormModule = formModule;
            formModule.TopLevel = false;
            formModule.FormBorderStyle = FormBorderStyle.None;
            formModule.Dock = DockStyle.Fill;
            pnlModule.Controls.Add(formModule);
            pnlModule.Tag = formModule;
            formModule.BringToFront();
            formModule.Show();
        }

        private void ToggleMenu()
        {
            if (pnlMenuSidebar.Width == MIN_WIDTH)
            {
                foreach (Control control in this.pnlMenuSidebar.Controls)
                {
                    if (control is Button button)
                    {
                        button.ImageAlign = ContentAlignment.MiddleLeft;
                    }
                }

                btnDashboard.Text = DASHBOARD;
                btnProduct.Text = PRODUCT;
                btnInventory.Text = INVENTORY;
                btnCustomer.Text = CUSTOMER;
                btnSupplier.Text = SUPPLIER;
                btnReports.Text = REPORTS;
                btnAccount.Text = ACCOUNT;
                btnSetting.Text = SETTING;
            }

            if (pnlMenuSidebar.Width == MAX_WIDTH)
            {
                foreach (Control control in this.pnlMenuSidebar.Controls)
                {
                    if (control is Button button)
                    {
                        button.Text = string.Empty;
                        button.ImageAlign = ContentAlignment.MiddleCenter;
                    }
                }
            }

            pnlMenuSidebar.Width = pnlMenuSidebar.Width == MIN_WIDTH ? MAX_WIDTH : MIN_WIDTH;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?","Confirm to logout",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }
    }
}
