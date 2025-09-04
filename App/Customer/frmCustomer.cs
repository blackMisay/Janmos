using Core.System.Repository;
using System;
using System.Windows.Forms;

namespace App.Customer
{
    public partial class Customer : Form
    {
        CustomerRepository customerRepository;
        private int selectedCustomerId = 0;
        private readonly int defaultRowCount = 20;
        public Customer()
        {
            InitializeComponent();
            cmbRecordCount.SelectedItem = defaultRowCount.ToString();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            cmbRecordCount.SelectedItem = "20";
            this.LoadCustomerData();
            FieldEnabling();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (CustomerModal cmodal = new CustomerModal())
            {
                cmodal.ShowDialog();
            }
            FieldEnabling();
            this.LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            customerRepository = new CustomerRepository();
            dgvCustomer.DataSource = customerRepository.LoadCustomerData();
            this.dgvCustomer.Columns["Customer ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomer.Columns["Mobile Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomer.Columns["Phone Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (this.selectedCustomerId != 0)
            {
                if (MessageBox.Show("Do you want to edit the selected customer?", "Edit Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (CustomerModal cmodal = new CustomerModal(this.selectedCustomerId))
                    {
                        cmodal.ShowDialog();
                    }
                    this.LoadCustomerData();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to update.", "Update customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FieldEnabling();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        { 
            if(this.selectedCustomerId > 0)
            {
                    if (MessageBox.Show("Do you want to delete the selected customer?", "Delete Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        customerRepository = new CustomerRepository();
                        customerRepository.DeleteCustomerData(this.selectedCustomerId);
                        this.LoadCustomerData();
                        this.selectedCustomerId = 0;
                        MessageBox.Show("Delete Successfully.", "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.", "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FieldEnabling();
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            customerRepository = new CustomerRepository();
            dgvCustomer.DataSource = customerRepository.LoadCustomerData(txtSearch.Text);
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCustomer.RowCount > 0)
            {
                int selectedRowIndex = dgvCustomer.SelectedCells[0].RowIndex;
                this.selectedCustomerId = Convert.ToInt32(dgvCustomer.Rows[selectedRowIndex].Cells[0].Value?.ToString());
            }
            FieldEnabling();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.selectedCustomerId = 0;
        }

        private void Customer_Click(object sender, EventArgs e)
        {
            this.selectedCustomerId = 0;
        }
        private void FieldEnabling()
        {
            btnEdit.Enabled = btnDelete.Enabled = false;

            if (this.selectedCustomerId != 0)
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