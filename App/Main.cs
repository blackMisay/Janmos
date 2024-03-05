using System;
using System.Windows.Forms;

namespace App
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private Form activeFormModule = null;
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
            this.openFormModule(new App.Customer());
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.openFormModule(new App.Dashboard.Dashboard());
        }
    }
}
