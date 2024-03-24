using Core.System.Data.Model;
using Core.System.Repository;
using System;
using System.Collections;
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

        public CustomerModal(int customerId)
        {
            InitializeComponent();
            this.Id = customerId;
            InitializeSelectedCustomerData();
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
            customer.Entity = new entity();
            customer.Entityname = this.txtEntityName.Text;
            customer.Mobilenum = this.txtMobileNumber.Text;
            customer.Telenum = this.txtPhoneNumber.Text;
            customer.Extension = this.txtPhoneNumberExtension.Text;
            customer.Email = this.txtEmailAddress.Text;
            customer.Socialnetid = this.txtSocialNetworkID.Text;
            customer.Region = new Core.System.Data.Model.Region() { Id = Convert.ToInt32(cmbRegion) };
            customer.Province = new Province() { Id = Convert.ToInt32(cmbProvince) };
            customer.Municipality = new Municipality() { Id = Convert.ToInt32(cmbCity) };
            customer.Baranggay = new Baranggay() { Id = Convert.ToInt32(cmbDistrict) };
            customer.Postal = this.txtPostalCode.Text;
            customer.Housenum = this.txtAddress.Text;

            customerController = new CustomerRepository();
            if (customerController.Save(customer))
            {
                MessageBox.Show("Save Successfully", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Customer failed to save", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitializeComponentsData()
        {
            customerController = new CustomerRepository();

            cmbEntity.DataSource = Enum.GetValues(typeof(entity));

            cmbRegion.DataSource = customerController.LoadDataList("SELECT DISTINCT region.id, region.`name` FROM region;");
            cmbRegion.ValueMember = "id";
            cmbRegion.DisplayMember = "name";
        }

        private void InitializeSelectedCustomerData()
        {
            InitializeComponentsData();

            customerController = new CustomerRepository();

            Core.System.Data.Model.Customer customer = new Core.System.Data.Model.Customer();
            customer = customerController.FetchCustomerData(this.Id);

            this.txtCustomerName.Text = customer.Name;
            cmbEntity.SelectedItem = customer.Entity.ToString();
            this.txtEntityName.Text = customer.Entityname;
            this.txtMobileNumber.Text = customer.Mobilenum;
            this.txtPhoneNumber.Text = customer.Telenum;
            this.txtPhoneNumberExtension.Text = customer.Extension;
            this.txtEmailAddress.Text = customer.Email;
            this.txtSocialNetworkID.Text = customer.Socialnetid;
            cmbRegion.SelectedValue = customer.Region.Id;
            cmbProvince.SelectedValue = customer.Province.Id;
            cmbCity.SelectedValue = customer.Municipality.Id;
            cmbDistrict.SelectedValue = customer.Baranggay.Id;
            this.txtAddress.Text = customer.Housenum;
            this.txtPostalCode.Text = customer.Postal;
        }

        private void cmbRegion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbProvince.DataSource = customerController.LoadDataList("SELECT province.id, province.region, province.`name` FROM province INNER JOIN region ON province.region = region.id WHERE region.id =" + cmbRegion.SelectedValue.ToString() + " ORDER BY province.id") ;
            cmbProvince.ValueMember = "id";
            cmbProvince.DisplayMember= "name";
        }

        private void cmbProvince_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbCity.DataSource = customerController.LoadDataList("SELECT municipality.id, municipality.province, municipality.`name` FROM municipality INNER JOIN province ON municipality.province = province.id WHERE province.id =" + cmbProvince.SelectedValue.ToString() + " ORDER BY municipality.id");
            cmbCity.ValueMember = "id";
            cmbCity.DisplayMember = "name";
        }

        private void cmbCity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbDistrict.DataSource = customerController.LoadDataList("SELECT baranggay.id, baranggay.municipality, baranggay.`name` FROM baranggay INNER JOIN municipality ON baranggay.municipality = municipality.id WHERE municipality.id =" + cmbCity.SelectedValue.ToString() + " ORDER BY baranggay.id");
            cmbDistrict.ValueMember = "id";
            cmbDistrict.DisplayMember = "name";
        }
    }
}
