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
        CustomerRepository customerRepository;
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
            if (MessageBox.Show("Are you sure you want to close this form without saving the customer?", "Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void CustomerModal_Load(object sender, EventArgs e)
        {
            this.txtCustomerName.Focus();
            cmbStatus.SelectedItem = "Active";
        }

        private void InitializeComponentsData()
        {
            customerRepository = new CustomerRepository();

            cmbEntity.DataSource = Enum.GetValues(typeof(Entity));
            cmbEntity.SelectedIndex = -1;

            cmbRegion.DataSource = customerRepository.LoadDataList("SELECT DISTINCT region.id, region.`name`, region.`description` FROM region;");
            cmbRegion.ValueMember = "id";
            cmbRegion.DisplayMember = "name";
            cmbRegion.SelectedIndex = -1;
        }

        private void cmbRegion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbProvince.DataSource = customerRepository.LoadDataList("SELECT province.id, province.region, province.`name` FROM province INNER JOIN region ON province.region = region.id WHERE region.id =" + cmbRegion.SelectedValue.ToString() + " ORDER BY province.id");
            cmbProvince.ValueMember = "id";
            cmbProvince.DisplayMember = "name";
            cmbProvince.SelectedIndex = -1;
        }

        private void cmbProvince_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbCity.DataSource = customerRepository.LoadDataList("SELECT municipality.id, municipality.province, municipality.`name` FROM municipality INNER JOIN province ON municipality.province = province.id WHERE province.id =" + cmbProvince.SelectedValue.ToString() + " ORDER BY municipality.id");
            cmbCity.ValueMember = "id";
            cmbCity.DisplayMember = "name";
            cmbCity.SelectedIndex = -1;
        }

        private void cmbCity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbDistrict.DataSource = customerRepository.LoadDataList("SELECT baranggay.id, baranggay.municipality, baranggay.`name` FROM baranggay INNER JOIN municipality ON baranggay.municipality = municipality.id WHERE municipality.id =" + cmbCity.SelectedValue.ToString() + " ORDER BY baranggay.id");
            cmbDistrict.ValueMember = "id";
            cmbDistrict.DisplayMember = "name";
            cmbDistrict.SelectedIndex = -1;
        }

        private void InitializeSelectedCustomerData()
        {
            InitializeComponentsData();
            customerRepository = new CustomerRepository();

            Core.System.Data.Model.Customer customer = new Core.System.Data.Model.Customer();
            customer = customerRepository.FetchCustomerData(this.Id);

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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            FieldValidate();
        }
        private void FieldValidate()
        {
            bool validate = true;

            //customer name
            if (string.IsNullOrEmpty(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                lblRequireName.Visible = true;
                validate = false;
            }
            else
            {
                lblRequireName.Visible = false;
            }

            //entity
            if (cmbEntity.SelectedIndex == -1)
            {
                lblRequireEntity.Visible = true;
                validate = false;
            }
            else
            {
                lblRequireEntity.Visible = false;
            }

            //mobile
            if (string.IsNullOrEmpty(txtMobileNumber.Text) || string.IsNullOrWhiteSpace(txtMobileNumber.Text))
            {
                lblRequireMobile.Visible = true;
                validate = false;
            }
            else
            {
                lblRequireMobile.Visible = false;
            }

            //phone
            if (string.IsNullOrEmpty(txtPhoneNumber.Text) || string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                lblRequirePhone.Visible = true;
                validate = false;
            }
            else
            {
                lblRequirePhone.Visible = false;
            }

            //extension
            if (string.IsNullOrEmpty(txtPhoneNumberExtension.Text) || string.IsNullOrWhiteSpace(txtPhoneNumberExtension.Text))
            {
                lblRequireExtension.Visible = true;
                validate = false;
            }
            else
            {
                lblRequireExtension.Visible = false;
            }

            //email
            if (string.IsNullOrEmpty(txtEmailAddress.Text) || string.IsNullOrWhiteSpace(txtEmailAddress.Text))
            {
                lblRequireEmail.Visible = true;
                validate = false;
            }
            else
            {
                lblRequireEmail.Visible = false;
            }

            //socialnetid
            if (string.IsNullOrEmpty(txtSocialNetworkID.Text) || string.IsNullOrWhiteSpace(txtSocialNetworkID.Text))
            {
                lblRequireSocial.Visible = true;
                validate = false;
            }
            else
            {
                lblRequireSocial.Visible = false;
            }

            //region
            if (cmbRegion.SelectedIndex == -1)
            {
                lblRequireRegion.Visible = true;
                validate = false;
            }
            else
            {
                lblRequireRegion.Visible = false;
            }

            //province
            if (cmbProvince.SelectedIndex == -1)
            {
                lblRequireProvince.Visible = true;
                validate = false;
            }
            else
            {
                lblRequireProvince.Visible = false;
            }

            //municipality
            if (cmbCity.SelectedIndex == -1)
            {
                lblRequireCity.Visible = true;
                validate = false;
            }
            else
            {
                lblRequireCity.Visible = false;
            }

            //baranggay
            if (cmbDistrict.SelectedIndex == -1)
            {
                lblRequireDistrict.Visible = true;
                validate = false;
            }
            else
            {
                lblRequireDistrict.Visible = false;
            }

            //housenum
            if (string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                lblRequireAddress.Visible = true;
                validate = false;
            }
            else
            {
                lblRequireAddress.Visible = false;
            }

            //postal
            if (string.IsNullOrEmpty(txtPostalCode.Text) || string.IsNullOrWhiteSpace(txtPostalCode.Text))
            {
                lblRequirePostal.Visible = true;
                validate = false;
            }
            else
            {
                lblRequirePostal.Visible = false;
            }

            if (!validate)
            {
                return;
            }

            Core.System.Data.Model.Customer customer = new Core.System.Data.Model.Customer();
            customer.Id = this.Id;
            customer.Name = this.txtCustomerName.Text;
            customer.Entity = new Entity();
            customer.Entityname = this.txtEntityName.Text;
            customer.Mobilenum = this.txtMobileNumber.Text;
            customer.Telenum = this.txtPhoneNumber.Text;
            customer.Extension = this.txtPhoneNumberExtension.Text;
            customer.Email = this.txtEmailAddress.Text;
            customer.Socialnetid = this.txtSocialNetworkID.Text;
            customer.Region = new Core.System.Data.Model.Region() { Id = Convert.ToInt32(cmbRegion.SelectedValue) };
            customer.Province = new Province() { Id = Convert.ToInt32(cmbProvince.SelectedValue) };
            customer.Municipality = new Municipality() { Id = Convert.ToInt32(cmbCity.SelectedValue) };
            customer.Baranggay = new Baranggay() { Id = Convert.ToInt32(cmbDistrict.SelectedValue) };
            customer.Postal = this.txtPostalCode.Text;
            customer.Housenum = this.txtAddress.Text;
            customer.Status = Status.Active;

            customerRepository = new CustomerRepository();
            if (MessageBox.Show("Do you want to save the customer data?", "Save Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (customerRepository.Save(customer))
                {
                    MessageBox.Show("Save Successfully", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Customer failed to save", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
