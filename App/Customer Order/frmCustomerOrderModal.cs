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
    public partial class frmCustomerOrderModal : Form
    {
        CustomerOrderRepository customerOrderRepository;
        private readonly int Id = 0;
        private readonly int _enable = 0;
        private string str_order_date = "yyyy-MM-dd";
        public frmCustomerOrderModal()
        {
            InitializeComponent();
            InitializeComponentsData();
            FieldEnabling(_enable);
        }
        public frmCustomerOrderModal(int customerOrderId)
        {
            InitializeComponent();
            this.Id = customerOrderId;
            InitializeSelectedInventoryData();
            FieldEnabling(customerOrderId);
            lblResetFields.Enabled = false;
        }
        private void FieldEnabling(int customerOrderId)
        {
            if (customerOrderId > _enable)
            {
                cmbCustomer.Enabled = false;
            }
            else
            {
                cmbCustomer.SelectedIndex = -1;
                cmbStatus.SelectedIndex = -1;
            }
        }
        private void InitializeComponentsData()
        {
            customerOrderRepository = new CustomerOrderRepository();

            cmbStatus.DataSource = Enum.GetValues(typeof(StatusType));

            dtpOrderDate.Value = DateTime.Today;
            dtpOrderDate.Enabled = false;

            cmbCustomer.DataSource = customerOrderRepository.LoadDataList("SELECT customer.id, customer.`name` FROM customer ORDER BY customer.`name`;");
            cmbCustomer.ValueMember = "id";
            cmbCustomer.DisplayMember = "name";
        }
        private void InitializeSelectedInventoryData()
        {
            InitializeComponentsData();

            customerOrderRepository = new CustomerOrderRepository();

            CustomerOrder customerOrder = new CustomerOrder();
            customerOrder = customerOrderRepository.FetchCustomerOrderData(this.Id);

            this.cmbCustomer.SelectedValue = customerOrder.Customer.Id;
            this.dtpOrderDate.Text = customerOrder.OrderDate;
            this.cmbStatus.SelectedItem = customerOrder.Status.ToString();
            this.txtTotalPrice.Text = customerOrder.TotalPrice.ToString();
        }

        private void frmCustomerOrderModal_Load(object sender, EventArgs e)
        {
            this.cmbCustomer.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidate();
        }
        private void FieldValidate()
        {
            this.str_order_date = this.dtpOrderDate.Value.ToString("yyyy-MM-dd");
            DateTime orderDateValue = DateTime.Parse(this.str_order_date);

            bool validated = true;

            if (cmbCustomer.SelectedIndex == -1)
            {
                validated = false;
                lblRequiredCustomer.Visible = true;
            }
            else
            {
                lblRequiredCustomer.Visible = false;
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                validated = false;
                lblRequiredStatus.Visible = true;
            }
            else
            {
                lblRequiredStatus.Visible = false;
            }

            if (string.IsNullOrEmpty(txtTotalPrice.Text) || string.IsNullOrWhiteSpace(txtTotalPrice.Text))
            {
                validated = false;
                lblRequiredTotalPrice.Visible = true;
            }
            else
            {
                lblRequiredTotalPrice.Visible = false;
            }

            if (!validated)
                return;

            CustomerOrder customerOrder = new CustomerOrder();

            customerOrder.Id = this.Id;
            customerOrder.Customer = new Core.System.Data.Model.Customer() { Id = Convert.ToInt32(this.cmbCustomer.SelectedValue)};
            StatusType statusType = (StatusType)cmbStatus.SelectedValue;
            customerOrder.Status = statusType;
            customerOrder.OrderDate = this.str_order_date;
            customerOrder.TotalPrice = decimal.Parse(this.txtTotalPrice.Text);

            customerOrderRepository = new CustomerOrderRepository();

            if (MessageBox.Show("Are you sure you want to save customer order record?", "Save Customer Order Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (customerOrderRepository.Save(customerOrder))
                {
                    MessageBox.Show("Save Successfully", "Customer Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Customer order failed to save", "Customer Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close this form without saving?", "Customer Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void lblResetFields_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Pressing the 'Clear all fields' will clear all values in fields and dropdown menus. Are you sure do you want to proceed?", "Clear all fields", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                cmbCustomer.SelectedIndex = -1;
                cmbStatus.SelectedIndex = -1;
                dtpOrderDate.Value = DateTime.Now;
                txtTotalPrice.Clear();
            }
        }
    }
}
