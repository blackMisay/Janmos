using Core.System.Data.Model;
using Core.System.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace App
{
    public partial class Main : Form
    {
        private readonly string username = "";
        private string userRole = "";
        private string userFullname = "";

        private static readonly string DASHBOARD = "&Dashboard";
        private static readonly string PRODUCT = "&Product";
        private static readonly string INVENTORY = "&Inventory";
        private static readonly string CUSTOMER = "&Customer";
        private static readonly string SUPPLIER = "&Supplier";
        private static readonly string REPORTS = "&Reports";
        private static readonly string ACCOUNT = "Pro&file";
        private static readonly string SETTING = "Se&ttings";
        private static readonly string CUSTOMERORDER = "Customer &Order";
        private static readonly string SUPPLIERORDER = "Supp&lier Order";
        private static readonly string USERMANAGEMENT = "&User Management";
        private static readonly string MANAGEMENTMODULE = "&Management Module";

        private static readonly int MIN_WIDTH = 70;
        private static readonly int MAX_WIDTH = 230;

        private Form activeFormModule = null;

        public Main()
        {
            InitializeComponent();
        }
        public Main(User account)
        {
            InitializeComponent();
            this.username = account.Username;
        }
        private void InitializeUserData(string Username)
        {
            UserManagementRepository umr = new UserManagementRepository();
            if (!string.IsNullOrWhiteSpace(Username))
            {
                this.userFullname = (umr.ExecuteScalar("SELECT CONCAT(ui.givenname, ' ', ui.lastname) FROM `user` u JOIN userinfo ui ON u.userinfoid = ui.id WHERE u.username = @Username;", new Dictionary<string, string>
                {
                    { "@Username", Username}
                })).ToString();
                this.userRole = (umr.ExecuteScalar("SELECT r.roletitle FROM `user` u JOIN roles r ON u.roleid = r.id WHERE u.username = @Username;", new Dictionary<string, string>
                {
                    {"@Username", Username }
                })).ToString();
            }
            if (string.IsNullOrWhiteSpace(this.userFullname))
            {
                lblUser.Text = "Welcome: Null";
            }
            lblUser.Text = "Welcome: " + this.userFullname;
            UpdateUserLastLogin(Username);
        }
        private void UpdateUserLastLogin(string Username)
        {
            string strDatetime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            UserManagementRepository umr = new UserManagementRepository();
            umr.UpdateUserLastLogin(Username, strDatetime);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            InitializeUserData(this.username);
            CheckUserRole(this.userRole);
        }
        private void CheckUserRole(string currentRole)
        {
            if (!string.IsNullOrWhiteSpace(currentRole))
            {
                if (currentRole == "Employee")
                {
                    btnSupplierOrder.Enabled = false;
                    btnSupplier.Enabled = false;
                    btnProduct.Enabled = false;
                    btnInventory.Enabled = false;
                    btnReport.Enabled = false;
                    btnUserManagement.Enabled = false;
                    btnManagementModule.Enabled = false;
                }
                else if (currentRole == "Manager")
                {
                    btnReport.Enabled = false;
                    btnUserManagement.Enabled = false;
                    btnManagementModule.Enabled = false;
                }
            }
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

        private void btnInventory_Click(object sender, EventArgs e)
        {
            this.openFormModule(new App.Inventory.frmInventory());
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            this.openFormModule(new Supplier.frmSupplier());
        }
        private void btnCustomerOrder_Click(object sender, EventArgs e)
        {
            this.openFormModule(new App.Customer_Order.frmCustomerOrder());
        }

        private void btnSupplierOrder_Click(object sender, EventArgs e)
        {
            this.openFormModule(new Supplier_Order.frmSupplierOrder());
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            this.openFormModule(new UserManagement.frmUserManagement(this.username));
        }

        private void btnManagementModule_Click(object sender, EventArgs e)
        {
            this.openFormModule(new ManagementModule.ManagementModule());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.openFormModule(new Report.frmReport());
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
                btnCustomerOrder.Text = CUSTOMERORDER;
                btnCustomer.Text = CUSTOMER;
                btnSupplierOrder.Text = SUPPLIERORDER;
                btnSupplier.Text = SUPPLIER;
                btnProduct.Text = PRODUCT;
                btnAccount.Text = ACCOUNT;
                btnSetting.Text = SETTING;
                btnInventory.Text = INVENTORY;
                btnReport.Text = REPORTS;
                btnUserManagement.Text = USERMANAGEMENT;
                btnManagementModule.Text = MANAGEMENTMODULE;
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
            if (MessageBox.Show("Are you sure you want to log out?", "Confirm to logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void tmrDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString();
            tmrDateTime.Start();
        }
    }
}
