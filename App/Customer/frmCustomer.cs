using App.Product;
using Core.System.Repository;
﻿using Core.System.Repository;
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
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (CustomerModal cmodal = new CustomerModal())
            {
                cmodal.ShowDialog();
            }
            this.LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            CustomerRepository customerRepository = new CustomerRepository();
            dgvCustomer.DataSource = customerRepository.LoadCustomerData();
            this.dgvCustomer.Columns["Mobile Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomer.Columns["Phone Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (selectedCustomerId != 0)
            {
                if (MessageBox.Show("Do you want to edit the selected customer?", "Edit Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    using (CustomerModal cmodal = new CustomerModal(this.selectedCustomerId))
                    {
                        cmodal.ShowDialog();
                    }
                    this.LoadCustomerData();
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

        private void btnDelete_Click(object sender, EventArgs e)
        { 
                if(selectedCustomerId > 0)
                {
                    if (MessageBox.Show("Do you want to delete the selected customer?", "Delete Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        CustomerRepository customerRepository = new CustomerRepository();
                        customerRepository.DeleteCustomerData(selectedCustomerId);
                        this.LoadCustomerData();
                        selectedCustomerId = 0;

                        MessageBox.Show("Delete Successfully.", "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a customer to delete.", "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            dgvCustomer.DataSource = customerRepository.LoadCustomerData(txtSearch.Text);
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCustomer.RowCount > 0)
            {
                int selectedRowIndex = dgvCustomer.SelectedCells[0].RowIndex;
                this.selectedCustomerId = Convert.ToInt32(dgvCustomer.Rows[selectedRowIndex].Cells[0].Value?.ToString());

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
