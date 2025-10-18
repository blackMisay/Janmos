using Core.System.Data.Model;
using Core.System.Repository;
using Core.System.Security;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace App
{
    public partial class Main : Form
    {
        private readonly User user = new User();
        private string userRole = "";
        private string userFullname = "";

        private static readonly string DASHBOARD = "&Dashboard";
        private static readonly string PRODUCT = "&Product";
        private static readonly string INVENTORY = "&Inventory";
        private static readonly string CUSTOMER = "&Customer";
        private static readonly string REPORTS = "&Reports";
        private static readonly string CUSTOMERORDER = "Customer &Order";
        private static readonly string USERMANAGEMENT = "&User Management";
        private static readonly string MANAGEMENTMODULE = "&Management Module";

        private static readonly int MIN_WIDTH = 70;
        private static readonly int MAX_WIDTH = 230;

        private Form activeFormModule = null;

        public Main()
        {
            InitializeComponent();
        }
        public Main(User user)
        {
            this.user = user;
            InitializeComponent();
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
            InitializeUserData(this.user.Username);
            CheckUserRole(this.user.RolesId.RoleTitle);
        }
        private void CheckUserRole(string currentRole)
        {
            if (!string.IsNullOrWhiteSpace(currentRole))
            {
                if (currentRole == "Employee")
                {
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
            AuditManager.Log(this.user, ActionType.VIEW, tableName: "Dashboard", description: "The user opened dashboard page.");
            this.openFormModule(new App.Dashboard.Dashboard(this.user));
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            AuditManager.Log(this.user, ActionType.VIEW, tableName: "Product", description: "The user opened product page.");
            this.openFormModule(new App.Product.frmProduct(this.user));
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            AuditManager.Log(this.user, ActionType.VIEW, tableName: "Customer", description: "The user opened customer page.");
            this.openFormModule(new App.Customer.Customer(this.user));
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            AuditManager.Log(this.user, ActionType.VIEW, tableName: "Inventory", description: "The user opened inventory page.");
            this.openFormModule(new App.Inventory.frmInventory(this.user));
        }
        private void btnCustomerOrder_Click(object sender, EventArgs e)
        {
            AuditManager.Log(this.user, ActionType.VIEW, tableName: "Customer Order", description: "The user opened customer order page.");
            this.openFormModule(new App.Customer_Order.frmCustomerOrder(this.user));
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            AuditManager.Log(this.user, ActionType.VIEW, tableName: "User Management", description: "The user opened user management page.");
            this.openFormModule(new UserManagement.frmUserManagement(this.user));
        }

        private void btnManagementModule_Click(object sender, EventArgs e)
        {
            AuditManager.Log(this.user, ActionType.VIEW, tableName: "System Administration", description: "The user opened system administration page.");
            this.openFormModule(new ManagementModule.ManagementModule(this.user));
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            AuditManager.Log(this.user, ActionType.VIEW, tableName: "Reports", description: "The user opened reports page.");
            this.openFormModule(new Report.frmReport(this.user));
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
                btnProduct.Text = PRODUCT;
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
                AuditManager.Log(this.user, ActionType.LOGOUT, description: "The user logged out of the system.");
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
