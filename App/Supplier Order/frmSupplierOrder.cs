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

namespace App.Supplier_Order
{
    public partial class frmSupplierOrder : Form
    {
        SupplierOrderRepository supplierOrderRepository;
        private int selectedSupplierOrderId = 0;
        public frmSupplierOrder()
        {
            InitializeComponent();
        }

        private void frmSupplierOrder_Load(object sender, EventArgs e)
        {
            this.LoadSupplierOrderData();
            FieldEnabling();
        }

        private void LoadSupplierOrderData()
        {
            supplierOrderRepository = new SupplierOrderRepository();
            dgvSupplierOrder.DataSource = supplierOrderRepository.LoadSupplierOrderData();

            this.dgvSupplierOrder.Columns["Supplier Order Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvSupplierOrder.Columns["Order Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvSupplierOrder.Columns["Total Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvSupplierOrder.Columns["Total Price"].DefaultCellStyle.Format = "N2";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            supplierOrderRepository = new SupplierOrderRepository();
            dgvSupplierOrder.DataSource = supplierOrderRepository.LoadSupplierOrderData(txtSearch.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (frmSupplierOrderModal frmSOM = new frmSupplierOrderModal())
            {
                frmSOM.ShowDialog();
            }
            FieldEnabling();
            this.LoadSupplierOrderData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.selectedSupplierOrderId != 0)
            {
                using (frmSupplierOrderModal frmSOM = new frmSupplierOrderModal(this.selectedSupplierOrderId))
                {
                    frmSOM.ShowDialog();
                    this.selectedSupplierOrderId = 0;
                }
                this.LoadSupplierOrderData();
            }
            else
            {
                MessageBox.Show("Please select a supplier order to update.", "Update Supplier Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FieldEnabling();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.selectedSupplierOrderId != 0)
            {
                if (MessageBox.Show("Do you want to delete the selected supplier order data?", "Delete Supplier Order Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    supplierOrderRepository = new SupplierOrderRepository();
                    supplierOrderRepository.DeleteSupplierOrderData(this.selectedSupplierOrderId);
                    this.selectedSupplierOrderId = 0;
                    this.LoadSupplierOrderData();

                    MessageBox.Show("Delete Successfully.", "Delete Supplier Order Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select record to delete.", "Delete Supplier Order Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FieldEnabling();
        }

        private void dgvSupplierOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSupplierOrder.RowCount > 0)
            {
                int selectedRowIndex = dgvSupplierOrder.SelectedCells[0].RowIndex;
                this.selectedSupplierOrderId = Convert.ToInt32(dgvSupplierOrder.Rows[selectedRowIndex].Cells[0].Value?.ToString());
            }
        }
        private void FieldEnabling()
        {
            btnEdit.Enabled = btnDelete.Enabled = false;

            if (this.selectedSupplierOrderId != 0)
            {
                btnEdit.Enabled = btnDelete.Enabled = true;
            }
            else
            {
                return;
            }
        }
    }
}
