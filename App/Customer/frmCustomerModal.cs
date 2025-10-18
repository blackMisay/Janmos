using Core.System.Data.Model;
using Core.System.Repository;
using Core.System.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Customer
{
    public partial class CustomerModal : Form
    {
        private readonly User user = new User();
        private static Core.System.Data.Model.Customer oldCustomer = new Core.System.Data.Model.Customer();
        private Core.System.Data.Model.Customer updCustomer = new Core.System.Data.Model.Customer();

        private readonly int Id = 0;
        CustomerRepository customerRepository;
        public CustomerModal(User user)
        {
            this.user = user;
            InitializeComponent();
            InitializeComponentsData();
            cmbEntity.DataSource = Enum.GetValues(typeof(Entity));
            cmbEntity.SelectedIndex = -1;
        }

        public CustomerModal(int customerId, User user)
        {
            this.user = user;
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

        private void InitializeComponentsData()
        {
            customerRepository = new CustomerRepository();

            cmbEntity.DataSource = Enum.GetValues(typeof(Entity)); //Populating cmbEntity with enum values
            cmbEntity.SelectedIndex = -1;
            cmbEntity.SelectedIndexChanged += cmbEntity_SelectedIndexChanged;

            if (cmbEntity.SelectedIndex == -1)
            {
                txtEntityName.Enabled = false;
            }

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

            updCustomer = customerRepository.FetchCustomerData(this.Id);
            oldCustomer = updCustomer;

            this.txtCustomerName.Text = updCustomer.Name;
            cmbEntity.SelectedItem = updCustomer.Entity;
            this.txtEntityName.Text = updCustomer.Entityname;
            this.txtMobileNumber.Text = updCustomer.Mobilenum;
            this.txtPhoneNumber.Text = updCustomer.Telenum;
            this.txtPhoneNumberExtension.Text = updCustomer.Extension;
            this.txtEmailAddress.Text = updCustomer.Email;
            this.txtSocialNetworkID.Text = updCustomer.Socialnetid;
            cmbRegion.SelectedValue = updCustomer.Region.Id;
            cmbProvince.SelectedValue = updCustomer.Province.Id;
            cmbCity.SelectedValue = updCustomer.Municipality.Id;
            cmbDistrict.SelectedValue = updCustomer.Baranggay.Id;
            this.txtAddress.Text = updCustomer.Housenum;
            this.txtPostalCode.Text = updCustomer.Postal;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            FieldValidate();
        }
        private void FieldValidate()
        {
            bool validated = true;

            bool ValidateTextBox(TextBox txt, Label lbl)
            {
                bool invalid = string.IsNullOrWhiteSpace(txt.Text);
                lbl.Visible = invalid;
                return !invalid;
            }
            bool ValidateComboBox(ComboBox cmb, Label lbl)
            {
                bool invalid = cmb.SelectedIndex == -1;
                lbl.Visible = invalid;
                return !invalid;
            }
            bool ValidateRichTextBox(RichTextBox txt, Label lbl)
            {
                bool invalid = string.IsNullOrWhiteSpace(txt.Text);
                lbl.Visible = invalid;
                return !invalid;
            }

            validated &= ValidateTextBox(txtCustomerName, lblRequireName);
            validated &= ValidateTextBox(txtMobileNumber, lblRequireMobile);

            validated &= ValidateRichTextBox(txtAddress, lblRequireAddress);

            validated &= ValidateComboBox(cmbEntity, lblRequireEntity);
            validated &= ValidateComboBox(cmbRegion, lblRequireRegion);
            validated &= ValidateComboBox(cmbProvince, lblRequireProvince);
            validated &= ValidateComboBox(cmbCity, lblRequireCity);
            validated &= ValidateComboBox(cmbDistrict, lblRequireDistrict);

            if (!validated)
                return;

            customerRepository = new CustomerRepository();
            if (this.Id != 0) //update
            {
                if (MessageBox.Show("Do you want to save the product data?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Core.System.Data.Model.Customer updCustomer = SaveCustomer();
                    var (oldJson, newJson, desc) = AuditHelper.GetDifferences(oldCustomer, updCustomer);
                    AuditLog log = new AuditLog
                    {
                        UserId = new User() { Id = Convert.ToInt32(this.user.Id) },
                        ActionType = "UPDATE",
                        TableName = "Product",
                        RecordId = updCustomer.Id,
                        OldValue = oldJson,
                        NewValue = newJson,
                        Description = desc
                    };
                    if (customerRepository.Save(log, SaveCustomer(), this.user))
                    {
                        MessageBox.Show("Record saved Successfully", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Unable to save the customer record. Please try again later or contact support for assistance.\r\n", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else //insert
            {
                if (MessageBox.Show("Do you want to save the customer data?", "Save Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (customerRepository.Save(null, SaveCustomer(), this.user))
                    {
                        MessageBox.Show("Record saved Successfully", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Unable to save the product record. Please try again later or contact support for assistance.\r\n", "Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private Core.System.Data.Model.Customer SaveCustomer()
        {
            updCustomer.Id = this.Id;
            updCustomer.Name = this.txtCustomerName.Text;
            updCustomer.Entity = new Entity();
            updCustomer.Entityname = this.txtEntityName.Text;
            updCustomer.Mobilenum = this.txtMobileNumber.Text;
            updCustomer.Telenum = this.txtPhoneNumber.Text;
            updCustomer.Extension = this.txtPhoneNumberExtension.Text;
            updCustomer.Email = this.txtEmailAddress.Text;
            updCustomer.Socialnetid = this.txtSocialNetworkID.Text;
            updCustomer.Region = new Core.System.Data.Model.Region() { Id = Convert.ToInt32(cmbRegion.SelectedValue) };
            updCustomer.Province = new Province() { Id = Convert.ToInt32(cmbProvince.SelectedValue) };
            updCustomer.Municipality = new Municipality() { Id = Convert.ToInt32(cmbCity.SelectedValue) };
            updCustomer.Baranggay = new Baranggay() { Id = Convert.ToInt32(cmbDistrict.SelectedValue) };
            updCustomer.Postal = this.txtPostalCode.Text;
            updCustomer.Housenum = this.txtAddress.Text;
            updCustomer.CreatedBy = new User() { Id = Convert.ToInt32(this.user.Id) };
            updCustomer.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            updCustomer.Status = StatusRecord.Type.Active;

            return updCustomer;
        }

        private void cmbEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.System.Data.Model.Customer customer = new Core.System.Data.Model.Customer();
            if (cmbEntity.SelectedItem != null)
            {
                if (cmbEntity.SelectedIndex == 0)
                {
                    txtEntityName.Enabled = false;
                }
                else if (cmbEntity.SelectedIndex == 1 || cmbEntity.SelectedIndex == 2)
                {
                    txtEntityName.Enabled = true;
                }

                if (customer != null)
                {
                    customer.Entity = (Entity)cmbEntity.SelectedItem;
                }
            }
        }
    }
}
