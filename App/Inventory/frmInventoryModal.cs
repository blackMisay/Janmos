using Core.System.Data.Model;
using Core.System.Repository;
using Core.System.Security;
using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Forms;

namespace App.Inventory
{
    public partial class frmInventoryModal : Form
    {
        InventoryRepository inventoryRepository;
        private readonly User user = new User();
        private static Core.System.Data.Model.Inventory oldInventory = new Core.System.Data.Model.Inventory();
        private Core.System.Data.Model.Inventory updInventory = new Core.System.Data.Model.Inventory();
        private static DateTime expirationValue;

        private readonly int Id = 0;
        private readonly int _enable = 0;
        private readonly string str_exp = "";
        private int validuntil = 0;
        public frmInventoryModal(User user)
        {
            this.user = user;
            InitializeComponent();
            InitializeComponentsData();
            FieldEnabling(_enable);
        }

        public frmInventoryModal(int inventoryId, string selectedInventoryDayRemainingExpiration, User user)
        {
            this.user = user;
            InitializeComponent();
            this.Id = inventoryId;
            this.str_exp = selectedInventoryDayRemainingExpiration;
            FieldEnabling(inventoryId);
            InitializeSelectedInventoryData();
            lblResetFields.Enabled = false;

            TimeSpan _validuntil = this.dtpExpiration.Value - DateTime.Today;
            int validuntil = _validuntil.Days;

            if (validuntil <= 7 && validuntil > 0) //to see how many days left before expiration.
            {
                MessageBox.Show("The selected product will expired in " + validuntil + " day/s.", "Expiration Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (this.dtpExpiration.Value <= DateTime.Today) //message if the selected product is expired.
            {
                MessageBox.Show("The selected product is expired.", "Expiration Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnCancel.Enabled = false;
            }
        }

        private void FieldEnabling(int inventoryId)
        {
            if (inventoryId > _enable)
            {
                cmbProduct.Enabled = false;
                cmbAvailability.DataSource = Enum.GetValues(typeof(Availability));
                cmbAvailability.Enabled = true;
                dtpExpiration.Enabled = false;
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
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidate();
        }

        private void InitializeSelectedInventoryData()
        {
            InitializeComponentsData();
            inventoryRepository = new InventoryRepository();

            updInventory = inventoryRepository.FetchInventoryData(this.Id);
            oldInventory = updInventory;

            this.cmbProduct.SelectedValue = updInventory.Name.Id;
            this.txtDescription.Text = updInventory.Description;
            this.txtPrice.Text = updInventory.Price;
            this.txtQuantity.Text = ((int)updInventory.Quantity).ToString();
            this.dtpExpiration.Text = updInventory.Expiration;
            this.cmbAvailability.SelectedItem = updInventory.Availability.ToString();
        }

        private void InitializeComponentsData()
        {
            inventoryRepository = new InventoryRepository();

            DataTable dt = inventoryRepository.LoadDataList("SELECT product.id, product.`name`, product.metricValue, metricunit.symbol FROM product JOIN metricunit ON product.metricUnit = metricunit.id ORDER BY product.`name`;");

            // Add a new column to hold the combined display string
            dt.Columns.Add("DisplayText", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                string name = row["name"].ToString();
                string metricValue = row["metricValue"].ToString();
                string metricUnit = row["symbol"].ToString();

                row["DisplayText"] = $"{name} - {metricValue} {metricUnit}";
            }

            cmbProduct.DataSource = dt;
            cmbProduct.ValueMember = "id";
            cmbProduct.DisplayMember = "DisplayText";
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
            bool ValidateRichTextBox(RichTextBox txt , Label lbl)
            {
                bool invalid = string.IsNullOrWhiteSpace(txt.Text);
                lbl.Visible = invalid;
                return !invalid;
            }
            bool ValidateDateTimePicker(DateTimePicker dtp)
            {
                bool invalid = true;
                if (cmbAvailability.Enabled == true && cmbAvailability.SelectedIndex == 1 && (dtp.Value <= DateTime.Today || dtp.Value >= DateTime.Today))
                {
                    return invalid;
                }
                else if (cmbAvailability.Enabled == true && cmbAvailability.SelectedIndex == 0 && dtp.Value <= DateTime.Today)
                {
                    MessageBox.Show("The availability should be change to unavailable.", "Expiration date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (invalid = dtp.Value <= DateTime.Today)
                {
                    MessageBox.Show("The date that you selected is not acceptable for expiration.", "Expiration date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    return invalid;
                }
                return !invalid;
            }

            validated &= ValidateTextBox(txtPrice, lblRequiredPrice);
            validated &= ValidateTextBox(txtQuantity, lblRequiredQuantity);

            validated &= ValidateRichTextBox(txtDescription, lblRequiredDescription);

            validated &= ValidateComboBox(cmbProduct, lblRequiredProduct);
            validated &= ValidateComboBox(cmbAvailability, lblRequiredAvailability);

            validated &= ValidateDateTimePicker(dtpExpiration);

            if (!validated)
                return;

            inventoryRepository = new InventoryRepository();
            if (this.Id != 0)
            {
                Core.System.Data.Model.Inventory updInventory = SaveInventory();
                var (oldJson, newJson, desc) = AuditHelper.GetDifferences(oldInventory, updInventory);
                AuditLog log = new AuditLog
                {
                    UserId = new User() { Id = Convert.ToInt32(this.user.Id) },
                    ActionType = "UPDATE",
                    TableName = "Product",
                    RecordId = updInventory.Id,
                    OldValue = oldJson,
                    NewValue = newJson,
                    Description = desc
                };
                if (inventoryRepository.Save(log, SaveInventory(), this.user))
                {
                    MessageBox.Show("Save Successfully", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Inventory failed to save", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else //insert
            {
                if (inventoryRepository.Save(null, SaveInventory(), this.user))
                {
                    MessageBox.Show("Save Successfully", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Inventory failed to save", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        private Core.System.Data.Model.Inventory SaveInventory()
        {
            TimeSpan _validuntil = this.dtpExpiration.Value - DateTime.Today;
            int validuntil = _validuntil.Days;
            
            if (this.Id > 0)
            {
                updInventory.Id = this.Id;
                updInventory.Name = new Core.System.Data.Model.Product() { Id = Convert.ToInt32(this.cmbProduct.SelectedValue) };
                updInventory.Description = this.txtDescription.Text;
                updInventory.Price = this.txtPrice.Text;
                updInventory.Quantity = Convert.ToInt32(this.txtQuantity.Text);
                updInventory.EntryDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                updInventory.Expiration = this.dtpExpiration.Value.ToString("yyyy-MM-dd");
                updInventory.Day = this.str_exp;
                Availability availability = (Availability)cmbAvailability.SelectedItem;
                updInventory.Availability = availability;
                updInventory.Status = StatusRecord.Type.Active;
            }
            else
            {
                updInventory.Id = this.Id;
                updInventory.Name = new Core.System.Data.Model.Product() { Id = Convert.ToInt32(this.cmbProduct.SelectedValue) };
                updInventory.Description = this.txtDescription.Text;
                updInventory.Price = this.txtPrice.Text;
                updInventory.Quantity = Convert.ToInt32(this.txtQuantity.Text);
                updInventory.EntryDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                updInventory.Expiration = this.dtpExpiration.Value.ToString("yyyy-MM-dd");
                if (validuntil <= 0)
                {
                    updInventory.Day = Math.Abs(this.validuntil).ToString() + "day/s expired";
                }
                else
                {
                    updInventory.Day = Math.Abs(this.validuntil).ToString() + "day/s before expiration";
                }
                Availability availability = (Availability)cmbAvailability.SelectedItem;
                updInventory.Availability = availability;
                updInventory.CreatedBy = new User() { Id = Convert.ToInt32(this.user.Id)};
                updInventory.Status = StatusRecord.Type.Active;
            }
            return updInventory;
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