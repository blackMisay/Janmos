using App.Customer_Order_Details;
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

namespace App.Customer_Order
{
    public partial class frmCustomerOrder : Form
    {
        CustomerOrderRepository customerorderRepository;
        private readonly User user = new User();
        private int selectedCustomerOrderId = 0;
        private string selectedCustomerOrderNumber = "";
        private string selectedCustomer = "";
        private string selectedItem = "";
        private readonly int defaultRowCount = 20;
        //private string userName = "";
        public frmCustomerOrder(User user)
        {
            this.user = user;
            InitializeComponent();
            cmbRecordCount.SelectedItem = defaultRowCount.ToString();
            InitializeComponentsData();
        }

        //FORM LOAD
        private void frmCustomerOrder_Load(object sender, EventArgs e)
        {
            this.LoadCustomerOrderData();
            FieldEnabling();
        }

        //LOAD CUSTOMER ORDER DATA
        private void LoadCustomerOrderData()
        {
            customerorderRepository = new CustomerOrderRepository();
            dgvCustomerOrder.DataSource = customerorderRepository.LoadCustomerOrderData();

            this.dgvCustomerOrder.Columns["Customer Order Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomerOrder.Columns["Customer Order Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomerOrder.Columns["Order Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomerOrder.Columns["Total Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvCustomerOrder.Columns["Total Price"].DefaultCellStyle.Format = "N2";
        }
        private void InitializeComponentsData()
        {
            customerorderRepository = new CustomerOrderRepository();

            cmbOrderStatus.DataSource = customerorderRepository.LoadDataList("SELECT `cos`.id, `cos`.orderstatus FROM customerorderstatus `cos`;");
            cmbOrderStatus.DisplayMember = "orderstatus";
            cmbOrderStatus.ValueMember = "id";
            cmbOrderStatus.SelectedIndex = -1;
        }

        //CUSTOMER ORDER ADD BUTTON
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //FOR EXISTING CUSTOMER ORDER ID
            //ADD ORDER IN SELECTED CUSTOMER ORDER ID
            if (this.selectedCustomerOrderId != 0)
            {
                if (MessageBox.Show("Do you want to add order in selected record?", "Add Customer Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    //DIRECTING FRMCUSTOMERORDERMODAL FORM
                    using (frmCustomerOrderModal frmCOM = new frmCustomerOrderModal(this.selectedCustomerOrderId))
                    {
                        frmCOM.ShowDialog();
                    }
                    this.LoadCustomerOrderData();
                    return;
                }
            }
            //FOR NEW CUSTOMER ORDER
            //DIRECTING FRMCUSTOMERORDERMODAL FORM
            using (frmCustomerOrderModal frmCOM = new frmCustomerOrderModal(this.user))
            {
                frmCOM.ShowDialog();
            }
            FieldEnabling();
            this.LoadCustomerOrderData();
        }

        //CUSTOMER ORDER EDIT BUTTON
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //FOR EXISTING CUSTOMER ORDER ID
            //EDIT ORDER BY SELECTED CUSTOMER ORDER ID
            if (selectedCustomerOrderId != 0)
            {
                //DIRECTING FRMCUSTOMERORDERMODAL FORM
                using (frmCustomerOrderModal frmCO = new frmCustomerOrderModal(this.selectedCustomerOrderId))
                {
                    frmCO.ShowDialog();
                    this.selectedCustomerOrderId = 0;
                }
                this.LoadCustomerOrderData();
            }
            else
            {
                MessageBox.Show("Please select a customer order to update.", "Update Customer Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FieldEnabling();
        }

        //CUSTOMER ORDER DELETE BUTTON
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //FR EXISTING CUSTOMER ORDER ID
            //DELETE ORDER BY SELECTED CUSTOMER ORDER ID
            if (this.selectedCustomerOrderId != 0)
            {
                if (MessageBox.Show("Do you want to delete the selected customer order data?", "Delete Customer Order Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    customerorderRepository = new CustomerOrderRepository();
                    customerorderRepository.DeleteCustomerOrderData(this.selectedCustomerOrderId);
                    this.selectedCustomerOrderId = 0;
                    this.LoadCustomerOrderData();

                    MessageBox.Show("Delete Successfully.", "Delete Customer Order Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select record to delete.", "Delete Customer Order Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FieldEnabling();
        }

        //CUSTOMER ORDER DATAGRIDVIEW CELLCLICK EVENT
        private void dgvCustomerOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCustomerOrder.RowCount > 0)
            {
                int selectedRowIndex = dgvCustomerOrder.SelectedCells[0].RowIndex;
                this.selectedCustomerOrderId = Convert.ToInt32(dgvCustomerOrder.Rows[selectedRowIndex].Cells[0].Value?.ToString());
            }
            FieldEnabling();
        }

        //CUSTOMER ORDER SEARCHBAR TEXTCHANGED EVENT
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbOrderStatus.SelectedItem.ToString()))
            {
                customerorderRepository = new CustomerOrderRepository();
                dgvCustomerOrder.DataSource = customerorderRepository.LoadCustomerOrderData(txtSearch.Text);
            }
            else
            {
                customerorderRepository = new CustomerOrderRepository();
                //dgvCustomerOrder.DataSource = customerorderRepository.LoadCustomerOrderDataViaOrderStatusAndSearchBox(txtSearch.Text, this.selectedItem);
                dgvCustomerOrder.DataSource = customerorderRepository.LoadCustomerOrderData(txtSearch.Text, this.selectedItem);
            }
        }

        //CUSTOMER ORDER DATAGRIDVIEW CELLDOUBLECLICK EVENT
        private void dgvCustomerOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCustomerOrder.RowCount > 0)
            {
                int selectedRowIndex = dgvCustomerOrder.SelectedCells[0].RowIndex;
                this.selectedCustomerOrderNumber = dgvCustomerOrder.Rows[selectedRowIndex].Cells[1].Value?.ToString();
                this.selectedCustomer = dgvCustomerOrder.Rows[selectedRowIndex].Cells[2].Value?.ToString();

                using (frmCustomerOrderDetails frmCOD = new frmCustomerOrderDetails(this.selectedCustomerOrderId, this.selectedCustomerOrderNumber, this.selectedCustomer))
                {
                    frmCOD.ShowDialog();
                    this.selectedCustomerOrderId = 0;
                    this.selectedCustomerOrderNumber = "";
                }
            }
        }
        private void FieldEnabling()
        {
            btnEdit.Enabled = btnDelete.Enabled = false;

            if (this.selectedCustomerOrderId != 0)
            {
                btnEdit.Enabled = btnDelete.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void frmCustomerOrder_Click(object sender, EventArgs e)
        {
            this.selectedCustomerOrderId = 0;
        }

        private void cmbOrderStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbOrderStatus.Items != null)
            {
                if (cmbOrderStatus.SelectedItem != null)
                {
                    this.selectedItem = cmbOrderStatus.SelectedItem.ToString();

                    customerorderRepository = new CustomerOrderRepository();
                    dgvCustomerOrder.DataSource = customerorderRepository.LoadCustomerOrderDataViaOrderStatus(this.selectedItem);
                }
            }
        }
    }
}
