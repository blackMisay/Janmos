using Core.System.Data.Model;
using Core.System.Repository;
using System;
using System.Windows.Forms;

namespace App.Customer_Order_Details
{
    public partial class frmCustomerOrderDetails : Form
    {
        CustomerOrderDetailsRepository customerOrderDetailsRepository;
        private readonly int OrderID = 0;
        private int unitPrice = 0;
        public int quantity = 0;
        private int _totalPrice = 0;
        public frmCustomerOrderDetails()
        {
            InitializeComponent();
        }
        public frmCustomerOrderDetails(int customerOrderID)
        {
            InitializeComponent();
            this.OrderID = customerOrderID;
            InitializeSelectedCustomerOrderData();
        }

        private void InitializeSelectedCustomerOrderData()
        {
            customerOrderDetailsRepository = new CustomerOrderDetailsRepository();

            CustomerOrderDetails customerOrderDetails = new CustomerOrderDetails();
            customerOrderDetails = customerOrderDetailsRepository.FetchCustomerOrderDetailsData(this.OrderID);
        }

        private void frmCustomerOrderDetails_Load(object sender, EventArgs e)
        {
            this.LoadCustomerOrderDetailsData();
            GetTotalPrice();
        }

        private void LoadCustomerOrderDetailsData()
        {
            customerOrderDetailsRepository = new CustomerOrderDetailsRepository();
            dgvCustomerOrderDetails.DataSource = customerOrderDetailsRepository.LoadCustomerOrderDetailsData(this.OrderID);

            this.dgvCustomerOrderDetails.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomerOrderDetails.Columns["Unit Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomerOrderDetails.Columns["Total Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewRow row in dgvCustomerOrderDetails.Rows)
            {
                this.quantity = Convert.ToInt32(row.Cells["Quantity"].Value.ToString());
                unitPrice = Convert.ToInt32(row.Cells["Unit Price"].Value.ToString());
                _totalPrice = unitPrice * quantity;
                row.Cells["Total Price"].Value = _totalPrice;
            }
        }

        public double GetTotalPrice()
        {
            double sum = 0;
            for (int i = 0; i < dgvCustomerOrderDetails.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dgvCustomerOrderDetails.Rows[i].Cells["Total Price"].Value);
            }
            return sum;
        }
    }
}
