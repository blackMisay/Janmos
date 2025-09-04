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
    public partial class frmCustomerBranch : Form
    {
        public delegate void CustomerIDSelectedHandler(int customerID);
        public event CustomerIDSelectedHandler CustomerIDSelected;
        public delegate void CustomerNameSelectedHandler(string customerName);
        public event CustomerNameSelectedHandler CustomerNameSelected;
        CustomerBranchRepository customerBranchRepository;
        private int selectedCustomerId = 0;
        private string selectedCustomerName = "";

        //FORM LOAD
        public frmCustomerBranch()
        {
            InitializeComponent();
            LoadCustomerBranchData();
            dgvCustomerBranch.CellClick += dgvCustomerBranch_CellClick;
        }

        //LOAD CUSTOMER BRANCH DATA
        private void LoadCustomerBranchData()
        {
            customerBranchRepository = new CustomerBranchRepository();
            dgvCustomerBranch.DataSource = customerBranchRepository.LoadCustomerBranchData();
            this.dgvCustomerBranch.Columns["Customer ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomerBranch.Columns["Mobile Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomerBranch.Columns["Phone Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        //CUSTOMER BRANCH SELECT BUTTON
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.selectedCustomerId > 0)
            {
                if (MessageBox.Show("Are you sure in selected product?", "Select Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CustomerIDSelected?.Invoke(this.selectedCustomerId);
                    CustomerNameSelected?.Invoke(this.selectedCustomerName);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer", "Select Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //CUSTOMER BRANCH SEARCH BAR
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            customerBranchRepository = new CustomerBranchRepository();
            dgvCustomerBranch.DataSource = customerBranchRepository.LoadCustomerBranchData(txtSearch.Text);
        }

        //CUSTOMER BRANCH DATAGRIDVIEW CELLCLICK EVENT
        private void dgvCustomerBranch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //SELECTING ROW COUNT FROM DATAGRIDVIEW
            if (dgvCustomerBranch.RowCount > 0)
            {
                int _selectedRowIndex = dgvCustomerBranch.SelectedCells[0].RowIndex;
                this.selectedCustomerId = Convert.ToInt32(dgvCustomerBranch.Rows[_selectedRowIndex].Cells[0].Value?.ToString());
                this.selectedCustomerName = dgvCustomerBranch.Rows[_selectedRowIndex].Cells[1].Value?.ToString();
            }
        }
    }
}
