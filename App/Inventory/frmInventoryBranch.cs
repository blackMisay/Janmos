using App.Customer_Order;
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
    public partial class frmInventoryBranch : Form
    {
        public delegate void InventorySelectedHandler(int inventoryId);
        public event InventorySelectedHandler InventoryIdSelected;
        public delegate void ProductSelectedHandler(string productName);
        public event ProductSelectedHandler ProductSelected;
        public delegate void ProductPriceHandler(double productPrice);
        public event ProductPriceHandler ProductPrice;
        InventoryBranchRepository _inventoryBranchRepository;
        private int _selectedInventoryBranchId = 0;
        private string _selectedInventoryBranchProductName = "";
        private double _selectedInventoryBranchProductPrice = 0;
        private int expired = 0;

        //FORM LOAD
        public frmInventoryBranch()
        {
            InitializeComponent();
            LoadInventoryBranchData();
            dgvInventoryBranch.CellClick += dgvInventoryBranch_CellClick;
        }

        //LOAD INVENTORY BRANCH DATA
        private void LoadInventoryBranchData()
        {
            _inventoryBranchRepository = new InventoryBranchRepository();
            dgvInventoryBranch.DataSource = _inventoryBranchRepository.LoadInventoryBranchData();
            this.dgvInventoryBranch.Columns["Inventory ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvInventoryBranch.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvInventoryBranch.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvInventoryBranch.Columns["Expiration"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //NUMBER FORMAT
            this.dgvInventoryBranch.Columns["Price"].DefaultCellStyle.Format = "N2";

            foreach (DataGridViewRow row in dgvInventoryBranch.Rows)
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

        //INVENTORY BRANCH SELECT BUTTON
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this._selectedInventoryBranchId > 0)
            {
                InventoryIdSelected?.Invoke(_selectedInventoryBranchId);
                ProductSelected?.Invoke(_selectedInventoryBranchProductName);
                ProductPrice?.Invoke(_selectedInventoryBranchProductPrice);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a product", "Select Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //INVENTORY BRANCH DATAGRIDVIEW CELLCLICK EVENT
        private void dgvInventoryBranch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //SELECTING ROW COUNT FROM DATAGRIDVIEW
            if(dgvInventoryBranch.RowCount > 0)
            {
                int _selectedRowIndex = dgvInventoryBranch.SelectedCells[0].RowIndex;
                this._selectedInventoryBranchId = Convert.ToInt32(dgvInventoryBranch.Rows[_selectedRowIndex].Cells[0].Value?.ToString());
                this._selectedInventoryBranchProductName = dgvInventoryBranch.Rows[_selectedRowIndex].Cells[1].Value?.ToString();
                this._selectedInventoryBranchProductPrice = Convert.ToDouble(dgvInventoryBranch.Rows[_selectedRowIndex].Cells[3].Value?.ToString());
            }
        }

        //INVENTORY BRANCH SEARCH BAR
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _inventoryBranchRepository = new InventoryBranchRepository();
            dgvInventoryBranch.DataSource = _inventoryBranchRepository.LoadInventoryBranchData(txtSearch.Text);
        }
    }
}
