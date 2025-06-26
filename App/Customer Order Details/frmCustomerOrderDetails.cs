using App.Customer_Order;
using Core.System.Data.Model;
using Core.System.Repository;
using System;
using System.Windows.Forms;

namespace App.Customer_Order_Details
{
    public partial class frmCustomerOrderDetails : Form
    {
        CustomerOrderDetailsRepository customerOrderDetailsRepository;
        private readonly int OrderId = 0;
        private readonly string OrderNumber = "";
        private readonly string Customer = "";
        private int unitPrice = 0;
        public int quantity = 0;
        private int totalAmount = 0;
        public frmCustomerOrderDetails()
        {
            InitializeComponent();
        }
        //FOR EXISTING CUSTOMER ORDER ID
        public frmCustomerOrderDetails(int customerOrderId, string customerOrderNumber, string selectedCustomer)
        {
            InitializeComponent();
            this.OrderId = customerOrderId;
            this.OrderNumber = customerOrderNumber;
            //this.Customer = selectedCustomer;
            lblCustomerName.Text = selectedCustomer;
            lblOrderNumber.Text = customerOrderNumber.ToString();
        }


        //FORM LOAD DATA
        private void frmCustomerOrderDetails_Load(object sender, EventArgs e)
        {
            this.LoadCustomerOrderDetailsData();
            lblTotalPrice.Text = GetTotalPrice().ToString("F2");
        }

        //CUSTOMER ORDER DETAILS LOAD DATA
        private void LoadCustomerOrderDetailsData()
        {
            customerOrderDetailsRepository = new CustomerOrderDetailsRepository();
            dgvCustomerOrderDetails.DataSource = customerOrderDetailsRepository.LoadCustomerOrderDetailsData(this.OrderNumber);

            this.dgvCustomerOrderDetails.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomerOrderDetails.Columns["Unit Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomerOrderDetails.Columns["Total Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            /*foreach (DataGridViewRow row in dgvCustomerOrderDetails.Rows)
            {
                this.quantity = Convert.ToInt32(row.Cells["Quantity"].Value.ToString());
                unitPrice = Convert.ToInt32(row.Cells["Unit Price"].Value.ToString());
                totalAmount = unitPrice * quantity;
                row.Cells["Total Amount"].Value = totalAmount;
            }*/
            this.dgvCustomerOrderDetails.Columns["Unit Price"].DefaultCellStyle.Format = "N2";
            this.dgvCustomerOrderDetails.Columns["Total Amount"].DefaultCellStyle.Format = "N2";
        }

        //GETTING TOTAL PRICE OF ADDED PRODUCTS
        public double GetTotalPrice()
        {
            double sum = 0;

            foreach (DataGridViewRow row in dgvCustomerOrderDetails.Rows)
            {
                if (row.Cells["Total Amount"].Value != null)
                {
                    double value;
                    if (double.TryParse(row.Cells["Total Amount"].Value.ToString(), out value))
                    {
                        sum += value;
                    }
                }
            }
            return sum;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (frmCustomerOrderModal frmCOM = new frmCustomerOrderModal(this.OrderId))
            {
                frmCOM.ShowDialog();
                this.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            customerOrderDetailsRepository = new CustomerOrderDetailsRepository();
            dgvCustomerOrderDetails.DataSource = customerOrderDetailsRepository.LoadCustomerOrderDetailsData(this.OrderNumber, txtSearch.Text);
        }
    }
}
