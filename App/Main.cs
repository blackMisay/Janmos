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

        private Form activeFormModule = null;
        bool sidebarExpand = false;

        public Main()
        {
            InitializeComponent();
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

        private void btnProduct_Click(object sender, EventArgs e)
        {
            this.openFormModule(new App.Product.Product());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            this.openFormModule(new App.Customer.Customer());
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.openFormModule(new App.Dashboard.Dashboard());
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            
            if (sidebarExpand == false)
            {
                pnlMenuSidebar.Width += 10;
                
                if (pnlMenuSidebar.Width == 120)
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
                }
                
                if (pnlMenuSidebar.Width >= 300)
                {
                    sidebarTransition.Stop();
                    sidebarExpand = true;
                }
            }
            else
            {
                pnlMenuSidebar.Width -= 10;
                
                if (pnlMenuSidebar.Width == 120)
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

                if (pnlMenuSidebar.Width <= 70)
                {
                    sidebarTransition.Stop();
                    sidebarExpand = false;
                }
            }
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
