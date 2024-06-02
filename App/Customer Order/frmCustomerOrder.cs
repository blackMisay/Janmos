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

namespace App.Customer_Order
{
    public partial class frmCustomerOrder : Form
    {
        CustomerOrderRepository customerorderRepository;
        private int selectedCustomerOrderId = 0;
        private readonly int defaultRowCount = 20;
        public frmCustomerOrder()
        {
            InitializeComponent();
            cmbRecordCount.SelectedItem = defaultRowCount.ToString();
        }

        private void frmCustomerOrder_Load(object sender, EventArgs e)
        {
            this.LoadCustomerOrderData();
        }
        private void LoadCustomerOrderData()
        {
            customerorderRepository = new CustomerOrderRepository();
            dgvCustomerOrder.DataSource = customerorderRepository.LoadCustomerOrderData();

            this.dgvCustomerOrder.Columns["Date of Order"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomerOrder.Columns["Total Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (frmCustomerOrderModal frmCO = new frmCustomerOrderModal())
            {
                frmCO.ShowDialog();
            }
            this.LoadCustomerOrderData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedCustomerOrderId != 0)
            {
                if (MessageBox.Show("Do you want to edit the selected customer order data?", "Edit Customer Order Data", MessageBoxButtons.YesNo, MessageBoxIcon.Information)==DialogResult.Yes)
                {
                    using (frmCustomerOrderModal frmCO = new frmCustomerOrderModal(this.selectedCustomerOrderId))
                    {
                        frmCO.ShowDialog();
                        selectedCustomerOrderId = 0;
                    }
                    this.LoadCustomerOrderData();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer order to update.", "Update Customer Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.selectedCustomerOrderId > 0)
            {
                if (MessageBox.Show("Do you want to delete the selected customer order data?", "Delete Customer Order Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    /*customerorderRepository = new CustomerOrderRepository();
                    customerorderRepository.DeleteCustomerOrderData(selectedCustomerOrderId);
                    selectedCustomerOrderId = 0;
                    this.LoadCustomerOrderData();*/

                    MessageBox.Show("Delete Successfully.", "Delete Inventory Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select record to delete.", "Delete Customer Order Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvCustomerOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCustomerOrder.RowCount > 0)
            {
                int selectedRowIndex = dgvCustomerOrder.SelectedCells[0].RowIndex;
                this.selectedCustomerOrderId = Convert.ToInt32(dgvCustomerOrder.Rows[selectedRowIndex].Cells[0].Value?.ToString());
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            customerorderRepository = new CustomerOrderRepository();
            dgvCustomerOrder.DataSource = customerorderRepository.LoadCustomerOrderData(txtSearch.Text);
        }
    }
}
