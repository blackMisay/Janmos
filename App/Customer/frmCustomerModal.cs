using Core.System.Data.Model;
using Core.System.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Customer
{
    public partial class CustomerModal : Form
    {
        private readonly int Id = 0;
        CustomerRepository customerController;
        public CustomerModal()
        {
            InitializeComponent();
            InitializeComponentsData();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CustomerModal_Load(object sender, EventArgs e)
        {
            this.txtCustomerName.Focus();
            cmbStatus.SelectedItem = "Active";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Core.System.Data.Model.Customer customer = new Core.System.Data.Model.Customer();
            customer.Id = this.Id;
            customer.Name = this.txtCustomerName.Text;
            customer.EntityName = this.txtEntityName.Text;
            customer.MobileNum = this.txtMobileNumber.Text;
            customer.TeleNum = this.txtPhoneNumber.Text;
            customer.Extension = this.txtPhoneNumberExtension.Text;
            customer.Email = this.txtEmailAddress.Text;
            customer.SocialNetId= this.txtSocialNetworkID.Text;
            customer.Housenum = this.txtAddress.Text;
            customer.Category = new Category() { Id = Convert.ToInt32(cmbCategory.SelectedValue) };
            customer.MetricUnit = new MetricUnit() { Id = Convert.ToInt32(cmbUnit.SelectedValue) };
            customer.MetricValue = this.txtValue.Text;

            customerController = new CustomerRepository();
            if (productController.Save(product))
            {
                MessageBox.Show("Save Successfully", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Product failed to save", "Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitializeComponentsData()
        {
            customerController = new CustomerRepository();
            cmbCategory.DataSource = productController.LoadDataList("SELECT id, CONCAT(`name`,'-',`description`) AS `name` FROM dbjanmos.category;");
            cmbCategory.ValueMember = "id";
            cmbCategory.DisplayMember = "name";

            cmbUnit.DataSource = productController.LoadDataList("SELECT id, CONCAT(`name`,' (',`symbol`,')') AS `name` FROM dbjanmos.metricunit;");
            cmbUnit.ValueMember = "id";
            cmbUnit.DisplayMember = "name";
        }
    }
}
