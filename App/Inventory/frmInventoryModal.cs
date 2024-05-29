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

namespace App.Inventory
{
    public partial class frmInventoryModal : Form
    {
        InventoryRepository inventoryRepository;
        private readonly int Id = 0;
        private readonly int _enable = 0;
        public frmInventoryModal()
        {
            InitializeComponent();
            InitializeComponentsData();
            FieldEnabling(_enable);

        }

        public frmInventoryModal(int inventoryId)
        {
            InitializeComponent();
            this.Id = inventoryId;
            FieldEnabling(inventoryId);
            InitializeSelectedInventoryData();
        }

        private void FieldEnabling(int inventoryId)
        {
            if (inventoryId > _enable)
            {
                cmbProduct.Enabled = false;
                cmbStatus.DataSource = Enum.GetValues(typeof(Status));
                cmbStatus.Enabled= false;
                cmbAvailability.DataSource = Enum.GetValues(typeof(Availability));
                cmbAvailability.Enabled = true;
            }
            else
            {
                dtpExpiration.Value = DateTime.Today;
                cmbProduct.SelectedIndex = -1;
                cmbStatus.DataSource = Enum.GetValues(typeof(Status));
                cmbStatus.Enabled = false;
                cmbAvailability.DataSource = Enum.GetValues(typeof(Availability));
                cmbAvailability.Enabled = false;
            }
        }

        private void frmInventoryModal_Load(object sender, EventArgs e)
        {
            this.cmbProduct.Focus();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close this form without saving?", "Inventory", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidate();
        }

        private void InitializeSelectedInventoryData()
        {
            InitializeComponentsData();

            inventoryRepository = new InventoryRepository();

            Core.System.Data.Model.Inventory inventory = new Core.System.Data.Model.Inventory();
            inventory = inventoryRepository.FetchInventoryData(this.Id);

            this.cmbProduct.SelectedValue = inventory.Name.Id;
            this.txtDescription.Text = inventory.Description;
            this.txtPrice.Text = inventory.Price;
            this.txtQuantity.Text = ((int)inventory.Quantity).ToString();
            this.dtpExpiration.Text = inventory.Expiration;
            this.cmbAvailability.SelectedItem = inventory.Availability.ToString();
            this.cmbStatus.SelectedItem = inventory.Status.ToString();
        }

        private void InitializeComponentsData()
        {
            inventoryRepository = new InventoryRepository();

            cmbProduct.DataSource = inventoryRepository.LoadDataList("SELECT product.id, product.`name` FROM product ORDER BY product.`name`;");
            cmbProduct.ValueMember = "id";
            cmbProduct.DisplayMember = "name";
        }

        private void FieldValidate()
        {
            string str_date = this.dtpExpiration.Value.ToString("yyyy-MM-dd");
            DateTime target = DateTime.Parse(str_date);
            DateTime today = DateTime.Today;
            TimeSpan _validuntil = target - today;
            int validuntil = _validuntil.Days;

            bool validated = true;

            if (cmbProduct.SelectedIndex == -1)
            {
                validated = false;
                lblRequiredProduct.Visible = true;
            }
            else
            {
                lblRequiredProduct.Visible = false;
            }

            if (cmbAvailability.SelectedIndex == -1)
            {
                validated = false;
                lblRequiredAvailability.Visible = true;
            }
            else
            {
                lblRequiredAvailability.Visible = false;
            }

            if (string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                validated = false;
                lblRequiredPrice.Visible = true;
            }
            else
            {
                lblRequiredPrice.Visible = false;
            }

            if (string.IsNullOrEmpty(txtQuantity.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                validated = false;
                lblRequiredQuantity.Visible = true;
            }
            else
            {
                lblRequiredQuantity.Visible = false;
            }

            if (string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                validated = false;
                lblRequiredDescription.Visible = true;
            }
            else
            {
                lblRequiredDescription.Visible = false;
            }
            if (target <= today)
            {
                MessageBox.Show("The date that you selected is not acceptable for expiration.", "Expiration date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validated = false;
            }
            else if (validuntil <= today.Day)
            {
                MessageBox.Show("The product is expired.", "Expiration date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validated = false;
            }
            else if (validuntil <= 30)
            {
                MessageBox.Show("The product no. " + this.Id + " will be expire in " + validuntil + " days.");
                validated = true;
            }

            if (!validated)
                return;

            Core.System.Data.Model.Inventory inventory = new Core.System.Data.Model.Inventory();

            inventory.Id = this.Id;
            inventory.Name = new Core.System.Data.Model.Product() { Id = Convert.ToInt32(this.cmbProduct.SelectedValue) };
            inventory.Description = this.txtDescription.Text;
            inventory.Price = this.txtPrice.Text;
            inventory.Quantity = Convert.ToInt32(this.txtQuantity.Text);
            inventory.Expiration = str_date;
            Availability availability = (Availability)cmbAvailability.SelectedItem;
            inventory.Availability = availability;
            inventory.Status = new Status();

            inventoryRepository = new InventoryRepository();

            if (MessageBox.Show("Are you sure you want to save inventory record?", "Save Inventory Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                if (inventoryRepository.Save(inventory))
                {
                    MessageBox.Show("Save Successfully", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Inventory failed to save", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
            }
        }

        private void lblResetFields_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Pressing the 'Clear all fields' will clear all values in fields and dropdown menus. Are you sure do you want to proceed?", "Clear all fields", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                cmbProduct.SelectedIndex = -1;
                txtDescription.Clear();
                cmbAvailability.SelectedIndex = -1;
                txtPrice.Clear();
                txtQuantity.Clear();
                dtpExpiration.Value = DateTime.Now;
            }
        }

    }
}