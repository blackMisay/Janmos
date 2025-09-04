using Core.System.Repository;
using System;
using System.Windows.Forms;

namespace App.Inventory
{
    public partial class frmInventory : Form
    {
        InventoryRepository inventoryRepository;
        private int selectedInventoryId = 0;
        private string selectedInventoryDayRemainingExpiration = "";
        private int expired = 0;
        private readonly int defaultRowCount = 20;
        public frmInventory()
        {
            InitializeComponent();
            cmbRecordCount.SelectedItem = defaultRowCount.ToString();
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            this.LoadInventoryData();
            FieldEnabling();
        }

        private void LoadInventoryData()
        {
            inventoryRepository = new InventoryRepository();
            dgvInventory.DataSource = inventoryRepository.LoadInventoryData();

            this.dgvInventory.Columns["Inventory ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvInventory.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvInventory.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvInventory.Columns["Expiration"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvInventory.Columns["Day/s Remaining"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvInventory.Columns["Price"].DefaultCellStyle.Format = "N2";

            foreach (DataGridViewRow row in dgvInventory.Rows)
            {
                DateTime expirationValue = DateTime.Parse(row.Cells["Expiration"].Value.ToString());
                DateTime dateToday = DateTime.Today;
                TimeSpan dayRemaining = expirationValue - dateToday;
                this.expired = dayRemaining.Days;

                if (expired <= 0)
                {
                    row.Cells["Day/s Remaining"].Value = (Math.Abs(this.expired)) + " day expired";
                }
                else
                {
                    row.Cells["Day/s Remaining"].Value = (Math.Abs(this.expired)) + " day remaining";
                }
            }
        }

        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvInventory.RowCount > 0)
            {
                int selectedRowIndex = dgvInventory.SelectedCells[0].RowIndex;
                this.selectedInventoryId = Convert.ToInt32(dgvInventory.Rows[selectedRowIndex].Cells[0].Value?.ToString());
                this.selectedInventoryDayRemainingExpiration = dgvInventory.Rows[selectedRowIndex].Cells["Day/s Remaining"].Value?.ToString();
            }
            FieldEnabling();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (frmInventoryModal frmIM = new frmInventoryModal())
            {
                frmIM.ShowDialog();
            }
            FieldEnabling();
            this.LoadInventoryData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.selectedInventoryId != 0)
            {
                if (MessageBox.Show("Do you want to edit the selected inventory data?", "Edit Inventory Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (frmInventoryModal frmIM = new frmInventoryModal(this.selectedInventoryId, this.selectedInventoryDayRemainingExpiration))
                    {
                        frmIM.ShowDialog();
                    }
                    this.LoadInventoryData();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to update.", "Update Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FieldEnabling();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.selectedInventoryId > 0)
            {
                if (MessageBox.Show("Do you want to delete the selected inventory data?", "Delete Inventory Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    inventoryRepository = new InventoryRepository();
                    inventoryRepository.DeleteInventoryData(this.selectedInventoryId);
                    this.selectedInventoryId = 0;
                    this.LoadInventoryData();

                    MessageBox.Show("Delete Successfully.", "Delete Inventory Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select record to delete.", "Delete Inventory Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FieldEnabling();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            inventoryRepository = new InventoryRepository();
            dgvInventory.DataSource = inventoryRepository.LoadInventoryData(txtSearch.Text);
        }
        private void FieldEnabling()
        {
            btnEdit.Enabled = btnDelete.Enabled = false;

            if (this.selectedInventoryId != 0)
            {
                btnEdit.Enabled = btnDelete.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.selectedInventoryId = 0;
        }

        private void frmInventory_Click(object sender, EventArgs e)
        {
            this.selectedInventoryId = 0;
        }
    }
}
