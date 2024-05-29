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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace App.Inventory
{
    public partial class frmInventoryModal : Form
    {
        InventoryRepository inventoryRepository;
        private readonly int Id = 0;
        private readonly int _enable = 0;
        private string str_date = "yyyy-MM-dd";
        private int validuntil = 0;
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
            lblResetFields.Enabled = false;

            str_date = this.dtpExpiration.Value.ToString("yyyy-MM-dd");
            DateTime target = DateTime.Parse(str_date);
            DateTime today = DateTime.Today;
            TimeSpan _validuntil = target - today;
            int validuntil = _validuntil.Days;

            if (validuntil <= 7 && validuntil > 0) //to see how many days left before expiration.
            {
                MessageBox.Show("The selected product will expired in " + validuntil + " day/s.", "Expiration Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (target <= today) //message if the selected product is expired.
            {
                MessageBox.Show("The selected product is expired.", "Expiration Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FieldEnabling(int inventoryId)
        {
            if (inventoryId > _enable)
            {
                cmbProduct.Enabled = false;
                cmbAvailability.DataSource = Enum.GetValues(typeof(Availability));
                cmbAvailability.Enabled = true;
            }
            else
            {
                dtpExpiration.Value = DateTime.Today;
                cmbProduct.SelectedIndex = -1;
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
            this.str_date = this.dtpExpiration.Value.ToString("yyyy-MM-dd");
            DateTime target = DateTime.Parse(this.str_date);
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

            if (cmbAvailability.SelectedIndex == 1 && target <= today)
            {
                validated = true;
            }
            else if (target <= today)
            {
                MessageBox.Show("The date that you selected is not acceptable for expiration.", "Expiration date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validated = false;
            }

            if (!validated)
                return;

            Core.System.Data.Model.Inventory inventory = new Core.System.Data.Model.Inventory();

            inventory.Id = this.Id;
            inventory.Name = new Core.System.Data.Model.Product() { Id = Convert.ToInt32(this.cmbProduct.SelectedValue) };
            inventory.Description = this.txtDescription.Text;
            inventory.Price = this.txtPrice.Text;
            inventory.Quantity = Convert.ToInt32(this.txtQuantity.Text);
            inventory.Expiration = this.str_date;
            if (validuntil <= 0)
            {
                this.validuntil = Math.Abs(validuntil);
                string validUntil = this.validuntil.ToString();
                string ValidUntil = validUntil + "day/s expired";
                inventory.Day = ValidUntil;
            }
            else
            {
                this.validuntil = Math.Abs(validuntil);
                string validUntil = this.validuntil.ToString();
                string ValidUntil = validUntil + "day/s before expiration";
                inventory.Day = ValidUntil;
            }
            Availability availability = (Availability)cmbAvailability.SelectedItem;
            inventory.Availability = availability;
            inventory.Status = Status.Active;

            inventoryRepository = new InventoryRepository();

            if (MessageBox.Show("Are you sure you want to save inventory record?", "Save Inventory Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                txtPrice.Clear();
                txtQuantity.Clear();
                dtpExpiration.Value = DateTime.Now;
            }
        }
    }
}