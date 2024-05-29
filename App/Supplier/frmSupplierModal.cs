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

namespace App.Supplier
{
    public partial class frmSupplierModal : Form
    {
        private readonly int Id = 0;
        SupplierRepository supplierRepository;
        public frmSupplierModal()
        {
            InitializeComponent();
            InitializeComponentsData();
        }

        public frmSupplierModal(int supplierId)
        {
            InitializeComponent();
            this.Id = supplierId;
            InitializeSelectedSupplierData();
        }

        private void lblResetFields_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Pressing the 'Clear all fields' will clear all values in fields and dropdown menus. Are you sure do you want to proceed?", "Clear all fields", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                txtSupplierName.Clear();
                cmbCategory.SelectedIndex = -1;
                txtContactPerson.Clear();
                txtSocialNetworkID.Clear();
                txtMobileNumber.Clear();
                txtPhoneNumber.Clear();
                txtPhoneNumberExtension.Clear();
                txtEmailAddress.Clear();
                cmbRegion.SelectedIndex = -1;
                cmbProvince.SelectedIndex = -1;
                cmbCity.SelectedIndex = -1;
                cmbDistrict.SelectedIndex = -1;
                txtAddress.Clear();
                txtPostalCode.Clear();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close this form without saving the supplier?", "Supplier", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void InitializeComponentsData()
        {
            supplierRepository = new SupplierRepository();
            cmbCategory.DataSource = supplierRepository.LoadDataList("SELECT id AS `Id`, `name` AS `name` FROM dbjanmos.suppliercategory;");
            cmbCategory.ValueMember = "id";
            cmbCategory.DisplayMember = "name";

            cmbRegion.DataSource = supplierRepository.LoadDataList("SELECT DISTINCT region.id , region.`name` FROM region;");
            cmbRegion.ValueMember = "id";
            cmbRegion.DisplayMember = "name";
        }

        private void cmbRegion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbProvince.DataSource = supplierRepository.LoadDataList("SELECT province.id, province.region, province.`name` FROM province INNER JOIN region ON province.region = region.id WHERE region.id =" + cmbRegion.SelectedValue.ToString() + " ORDER BY province.id");
            cmbProvince.ValueMember = "id";
            cmbProvince.DisplayMember = "name";
        }

        private void cmbProvince_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbCity.DataSource = supplierRepository.LoadDataList("SELECT municipality.id, municipality.province, municipality.`name` FROM municipality INNER JOIN province ON municipality.province = province.id WHERE province.id =" + cmbProvince.SelectedValue.ToString() + " ORDER BY municipality.id");
            cmbCity.ValueMember = "id";
            cmbCity.DisplayMember = "name";
        }

        private void cmbCity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbDistrict.DataSource = supplierRepository.LoadDataList("SELECT baranggay.id, baranggay.municipality, baranggay.`name` FROM baranggay INNER JOIN municipality ON baranggay.municipality = municipality.id WHERE municipality.id =" + cmbCity.SelectedValue.ToString() + " ORDER BY baranggay.id");
            cmbDistrict.ValueMember = "id";
            cmbDistrict.DisplayMember = "name";
        }

        private void InitializeSelectedSupplierData()
        {
            InitializeComponentsData();
            supplierRepository = new SupplierRepository();

            Core.System.Data.Model.Supplier supplier = new Core.System.Data.Model.Supplier();
            supplier = supplierRepository.FetchSupplierData(this.Id);
            this.txtSupplierName.Text = supplier.Name;
            cmbCategory.SelectedValue = supplier.Category.Id;
            this.txtContactPerson.Text = supplier.Contactperson;
            this.txtSocialNetworkID.Text = supplier.Socialnetid;
            this.txtMobileNumber.Text = supplier.Mobilenum;
            this.txtPhoneNumber.Text = supplier.Phonenum;
            this.txtPhoneNumberExtension.Text = supplier.Extension;
            this.txtEmailAddress.Text = supplier.Email;
            cmbRegion.SelectedValue = supplier.Region.Id;
            cmbProvince.SelectedValue = supplier.Province.Id;
            cmbCity.SelectedValue = supplier.Municipality.Id;
            cmbDistrict.SelectedValue = supplier.Baranggay.Id;
            this.txtAddress.Text = supplier.Housenum;
            this.txtPostalCode.Text = supplier.Postal;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidate();
        }

        public void FieldValidate()
        {
            bool validated = true;
            //supplier name
            if (string.IsNullOrEmpty(txtSupplierName.Text) || string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                lblRequiredName.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredName.Visible = false;
            }
            //contact person
            if (string.IsNullOrEmpty(txtContactPerson.Text) || string.IsNullOrWhiteSpace(txtContactPerson.Text))
            {
                lblRequiredContactPerson.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredContactPerson.Visible = false;
            }
            //socal network id
            if (string.IsNullOrEmpty(txtSocialNetworkID.Text) || string.IsNullOrWhiteSpace(txtSocialNetworkID.Text))
            {
                lblRequiredSocialNetId.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredSocialNetId.Visible = false;
            }
            //mobile number
            if (string.IsNullOrEmpty(txtMobileNumber.Text) || string.IsNullOrWhiteSpace(txtMobileNumber.Text))
            {
                lblRequiredMobilenum.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredMobilenum.Visible = false;
            }
            //phone number
            if (string.IsNullOrEmpty(txtPhoneNumber.Text) || string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                lblRequiredPhonenum.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredPhonenum.Visible = false;
            }
            //phone extension
            if (string.IsNullOrEmpty(txtPhoneNumberExtension.Text) || string.IsNullOrWhiteSpace(txtPhoneNumberExtension.Text))
            {
                lblRequiredExtension.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredExtension.Visible = false;
            }
            //email address
            if (string.IsNullOrEmpty(txtEmailAddress.Text) || string.IsNullOrWhiteSpace(txtEmailAddress.Text))
            {
                lblRequiredEmail.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredEmail.Visible = false;
            }
            //house number address
            if (string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                lblRequiredAddress.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredAddress.Visible = false;
            }
            //postal code
            if (string.IsNullOrEmpty(txtPostalCode.Text) || string.IsNullOrWhiteSpace(txtPostalCode.Text))
            {
                lblRequiredPostal.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredPostal.Visible = false;
            }
            //combobox supplier category
            if (cmbCategory.SelectedIndex == -1)
            {
                lblRequiredCategory.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredCategory.Visible = false;
            }
            //combobox region
            if (cmbRegion.SelectedIndex == -1)
            {
                lblRequiredRegion.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredRegion.Visible = false;
            }
            //combobox province
            if (cmbProvince.SelectedIndex == -1)
            {
                lblRequiredProvince.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredProvince.Visible = false;
            }
            //combobox municipality
            if (cmbCity.SelectedIndex == -1)
            {
                lblRequiredCity.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredCity.Visible = false;
            }
            //combobox baranggay
            if (cmbDistrict.SelectedIndex == -1)
            {
                lblRequiredDistrict.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredDistrict.Visible = false;
            }

            if (!validated)
                return;

            supplierRepository = new SupplierRepository();
            Core.System.Data.Model.Supplier supplier = new Core.System.Data.Model.Supplier();
            supplier.Id = this.Id;
            supplier.Name = this.txtSupplierName.Text;
            supplier.Category = new SupplierCategory() { Id = Convert.ToInt32(cmbCategory.SelectedValue) };
            supplier.Contactperson = this.txtContactPerson.Text;
            supplier.Socialnetid = this.txtSocialNetworkID.Text;
            supplier.Mobilenum = this.txtMobileNumber.Text;
            supplier.Phonenum = this.txtPhoneNumber.Text;
            supplier.Extension = this.txtPhoneNumberExtension.Text;
            supplier.Email = this.txtEmailAddress.Text;
            supplier.Region = new Core.System.Data.Model.Region() { Id = Convert.ToInt32(cmbRegion.SelectedValue) };
            supplier.Province = new Province() { Id = Convert.ToInt32(cmbProvince.SelectedValue) };
            supplier.Municipality = new Municipality() { Id = Convert.ToInt32(cmbCity.SelectedValue) };
            supplier.Baranggay = new Baranggay() { Id = Convert.ToInt32(cmbDistrict.SelectedValue) };
            supplier.Housenum = this.txtAddress.Text;
            supplier.Postal = this.txtPostalCode.Text;

            supplierRepository = new SupplierRepository();
            if (supplierRepository.Save(supplier))
            {
                MessageBox.Show("Record saved Successfully", "Supplier", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Unable to save the supplier record. Please try again later or contact support for assistance.\r\n", "Supplier", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSupplierModal_Load(object sender, EventArgs e)
        {
            this.txtSupplierName.Focus();
        }
    }
}
