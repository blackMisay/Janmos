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

namespace App.Customer
{
    public partial class Customer : Form
    {
        private int selectedCustomerId = 0;
        public Customer()
        {
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            cmbRecordCount.SelectedItem = "20";
            this.LoadCustomerData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (CustomerModal cmodal = new CustomerModal())
            {
                cmodal.ShowDialog();
            }
            this.LoadCustomerData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId != 0)
            {
                using (CustomerModal cmodal = new CustomerModal())
                {
                    cmodal.ShowDialog();
                }
                this.LoadCustomerData();
            }
            else
            {
                MessageBox.Show("Please select a customer to update.", "Update customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void LoadCustomerData()
        {
            CustomerRepository customerRepository = new CustomerRepository();
            dgvCustomers.DataSource = customerRepository.LoadCustomerData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            dgvCustomers.DataSource = customerRepository.LoadCustomerData(txtSearch.Text);
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCustomers.RowCount > 0)
            {
                int selectedRowIndex = dgvCustomers.SelectedCells[0].RowIndex;
                this.selectedCustomerId = Convert.ToInt32(dgvCustomers.Rows[selectedRowIndex].Cells[0].Value?.ToString());
            }
        }
    }
}
