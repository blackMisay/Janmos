using App.Customer;
using App.Product;
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
    public partial class frmInventory : Form
    {
        InventoryRepository inventoryRepository;
        private int selectedInventoryId = 0;
        private readonly int defaultRowCount = 20;
        public frmInventory()
        {
            InitializeComponent();
            cmbRecordCount.SelectedItem = defaultRowCount.ToString();
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            this.LoadInventoryData();
        }

        private void LoadInventoryData()
        {
            inventoryRepository = new InventoryRepository();
            dgvInventory.DataSource = inventoryRepository.LoadInventoryData();

            this.dgvInventory.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvInventory.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvInventory.Columns["Expiration"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvInventory.Columns["Day/s Remaining"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvInventory.RowCount > 0)
            {
                int selectedRowIndex = dgvInventory.SelectedCells[0].RowIndex;
                this.selectedInventoryId = Convert.ToInt32(dgvInventory.Rows[selectedRowIndex].Cells[0].Value?.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (frmInventoryModal frmIM = new frmInventoryModal())
            {
                frmIM.ShowDialog();
            }
            this.LoadInventoryData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedInventoryId != 0)
            {
                if (MessageBox.Show("Do you want to edit the selected inventory data?", "Edit Inventory Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (frmInventoryModal frmIM = new frmInventoryModal(this.selectedInventoryId))
                    {
                        frmIM.ShowDialog();
                        selectedInventoryId = 0;
                    }
                    this.LoadInventoryData();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to update.", "Update Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.selectedInventoryId > 0)
            {
                if (MessageBox.Show("Do you want to delete the selected inventory data?", "Delete Inventory Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    inventoryRepository = new InventoryRepository();
                    inventoryRepository.DeleteInventoryData(selectedInventoryId);
                    selectedInventoryId = 0;
                    this.LoadInventoryData();

                    MessageBox.Show("Delete Successfully.", "Delete Inventory Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select record to delete.", "Delete Inventory Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            inventoryRepository = new InventoryRepository();
            dgvInventory.DataSource = inventoryRepository.LoadInventoryData(txtSearch.Text);
        }
    }
}
