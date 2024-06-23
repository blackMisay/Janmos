using Core.System.Data.Model;
using Core.System.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace App.Customer_Order
{
    public partial class frmCustomerOrderModal : Form
    {
        CustomerOrderRepository customerOrderRepository;
        CustomerOrderDetailsRepository customerOrderDetailsRepository;
        private readonly int Id = 0;
        private readonly int enable = 0;
        private double totalValue = 0;
        private int selectedProductId = 0;
        private int result = 0;
        private string str_order_date = "yyyy-MM-dd";
        private double sum = 0;
        private double unitPrice = 0;
        private int quantity = 0;
        private double runningTotal = 0;

        public frmCustomerOrderModal()
        {
            InitializeComponent();
            InitializeComponentsData();
            FieldEnabling(enable);
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
            txtPrice.Enabled = false;
            if (customerOrderId > enable)
            {
                cmbCustomer.Enabled = false;
            }
            else
            {
                btnSaveItem.Enabled = false;
                btnEdit.Enabled = false;
                cmbCustomer.SelectedIndex = -1;
                cmbStatus.SelectedIndex = -1;
                cmbProduct.SelectedIndex = -1;
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

            cmbProduct.DataSource = customerOrderRepository.LoadDataList("SELECT product.id, product.`name` FROM product ORDER BY product.`name`;");
            cmbProduct.ValueMember = "id";
            cmbProduct.DisplayMember = "name";
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
            this.lblTotalValue.Text = customerOrder.TotalPrice;

            lvOrderList.View = View.Details;

            customerOrderDetailsRepository = new CustomerOrderDetailsRepository();

            List<CustomerOrderDetails> customerOrderList;
            //customerOrderList = customerOrderDetailsRepository.FetchCustomerOrderDetailsData(this.Id);

            
        }

        private void frmCustomerOrderModal_Load(object sender, EventArgs e)
        {
            this.LoadCustomerOrderModalData();
        }

        private void LoadCustomerOrderModalData()
        {
            this.cmbCustomer.Focus();
            cmbProduct.SelectionChangeCommitted += cmbProduct_SelectionChangeCommitted;
            Button btn = new Button();
            ProductOrderButtonEnabling(btn);
        }

        public List<CustomerOrderDetails> orderListFromListView(ListView listview, int _result)
        {
            List<CustomerOrderDetails> orderDetailsItemList = new List<CustomerOrderDetails>();

            foreach (ListViewItem item in listview.Items)
            {
                CustomerOrderDetails orderDetailsListItem = new CustomerOrderDetails();

                orderDetailsListItem.Id = this.Id;
                orderDetailsListItem.CustomerOrderID = new CustomerOrder() { Id = Convert.ToInt32(_result)};
                orderDetailsListItem.ProductID = new Core.System.Data.Model.Product() { Id = Convert.ToInt32(item.SubItems[0].Text) };
                orderDetailsListItem.Quantity = int.Parse(item.SubItems[2].Text);
                orderDetailsListItem.UnitPrice = double.Parse(item.SubItems[3].Text);
                orderDetailsListItem.TotalPrice = double.Parse(item.SubItems[4].Text);

                orderDetailsItemList.Add(orderDetailsListItem);
            }

            return orderDetailsItemList;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidate();
        }

        private void FieldValidate()
        {
            this.str_order_date = this.dtpOrderDate.Value.ToString("yyyy-MM-dd");

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

            if (!validated)
                return;

            CustomerOrder customerOrder = new CustomerOrder();

            customerOrder.Id = this.Id;
            customerOrder.Customer = new Core.System.Data.Model.Customer() { Id = Convert.ToInt32(this.cmbCustomer.SelectedValue)};
            StatusType statusType = (StatusType)cmbStatus.SelectedValue;
            customerOrder.Status = statusType;
            customerOrder.OrderDate = this.str_order_date;
            customerOrder.TotalPrice = this.lblTotalValue.Text;

            customerOrderRepository = new CustomerOrderRepository();

            if (MessageBox.Show("Are you sure you want to save customer order record?", "Save Customer Order Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.result = customerOrderRepository.Save(customerOrder);
                if (this.result > 0)
                {
                    List<CustomerOrderDetails> orderDetailsList = orderListFromListView(lvOrderList, this.result);
                    foreach (CustomerOrderDetails orderDetails in orderDetailsList)
                    {
                        customerOrderDetailsRepository = new CustomerOrderDetailsRepository();
                        customerOrderDetailsRepository.Save(orderDetails);
                    }

                    MessageBox.Show("Save Successfully", "Customer Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Customer order failed to save", "Customer Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                cmbProduct.SelectedIndex = -1;
                txtQuantity.Clear();
                txtPrice.Clear();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)//Button Add for ListView
        {
            if (cmbProduct.SelectedIndex == -1 || string.IsNullOrEmpty(txtQuantity.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text))//Validation if the field is empty or null or whitespace
            {
                MessageBox.Show("Please fill in all frields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRowView rowView = cmbProduct.SelectedItem as DataRowView;

            if (rowView == null)
            {
                return;
            }

            string product = rowView["name"].ToString();

            ListViewItem item = new ListViewItem(this.selectedProductId.ToString());
            item.SubItems.Add(product);
            item.SubItems.Add(txtQuantity.Text);
            item.SubItems.Add(txtPrice.Text);
            if (!double.TryParse(txtPrice.Text, out unitPrice) || !int.TryParse(txtQuantity.Text, out quantity))
            {
                MessageBox.Show("Please enter valid data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            totalValue = unitPrice * quantity;
            item.SubItems.Add(totalValue.ToString());
            CalculateTotalValue(totalValue);
            lvOrderList.Items.Add(item);

            ClearInputFields();
            this.ProductOrderButtonEnabling(sender);
            this.LoadCustomerOrderModalData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvOrderList.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvOrderList.SelectedItems[0]; // Selected Item in ListView
                string str_productID = selectedItem.SubItems[0].Text; // String Product Id
                string str_quantity = selectedItem.SubItems[2].Text; // String Quantity
                string str_unitPrice = selectedItem.SubItems[3].Text; // String Unit Price

                txtQuantity.Text = str_quantity;
                txtPrice.Text = str_unitPrice;

                bool productFound = false;
                cmbProduct.SelectedValue = str_productID;

                if (cmbProduct.SelectedValue != null && cmbProduct.SelectedValue.ToString() == str_productID)
                {
                    productFound = true;
                }

                if (!productFound)
                {
                    MessageBox.Show("Product not found in ComboBox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbProduct.SelectedItem = null;
                }
                btnSaveItem.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please select an item to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.ProductOrderButtonEnabling(sender);
        }

        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            if (lvOrderList.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvOrderList.SelectedItems[0];
                string updatedProduct = cmbProduct.Text; // Updated String Product
                string updatedUnitPrice = txtPrice.Text; // Updated String Unit Price
                string updatedQuantity = txtQuantity.Text; // Updated String Quantity

                if (!double.TryParse(updatedUnitPrice, out unitPrice) || !int.TryParse(updatedQuantity, out quantity)) // If unitprice value not double and quatity not integer
                {
                    MessageBox.Show("Please enter valid data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double newTotalValue = unitPrice * quantity;

                double oldTotalValue = double.Parse(selectedItem.SubItems[4].Text);

                runningTotal -= oldTotalValue;

                selectedItem.SubItems[1].Text = updatedProduct; // Product Name
                selectedItem.SubItems[2].Text = quantity.ToString(); // Quantity
                selectedItem.SubItems[3].Text = unitPrice.ToString(); // Unit Price
                selectedItem.SubItems[4].Text = newTotalValue.ToString(); // Total Price

                runningTotal += newTotalValue;
                CalculateTotalValue(runningTotal);
                runningTotal = 0;
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please select an item to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.ProductOrderButtonEnabling(sender);
            lvOrderList.SelectedItems.Clear();
        }
        
        private void ProductOrderButtonEnabling(object sender)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            if (lvOrderList.Items.Count >= 0) // for add button enabling
            {
                btnAdd.Enabled = true;
            }

            if (lvOrderList.Items.Count == 0) // for delete and edit button enabling
            {
                btnDelete.Enabled = btnEdit.Enabled = false;
            }
            else if(lvOrderList.SelectedItems.Count == 0)
            {
                btnDelete.Enabled = btnEdit.Enabled = false;
            }
            else if (lvOrderList.SelectedItems.Count > 0)
            {
                btnDelete.Enabled = btnEdit.Enabled = true;
            }

            if (btn == btnEdit) // for save button enabling
            {
                btnSaveItem.Enabled = true;
                btnAdd.Enabled = btnDelete.Enabled = btnEdit.Enabled = false;
            }

            if (btn == btnSaveItem) // for save button disabling
            {
                btnSaveItem.Enabled = false;
            }
        }

        private void CalculateTotalValue(double _totalValue)
        {
            sum += _totalValue;
            lblTotalValue.Text = sum.ToString();
        }

        private void ClearInputFields()
        {
            cmbProduct.SelectedItem = null;
            txtPrice.Clear();
            txtQuantity.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvOrderList.SelectedItems.Count > 0)
            {
                lvOrderList.Items.Remove(lvOrderList.SelectedItems[0]);
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.ProductOrderButtonEnabling(sender);
            this.LoadCustomerOrderModalData();
        }

        private void cmbProduct_SelectionChangeCommitted(object sender, EventArgs e)//Selection Change Commited to the Product Combobox
        {
            if (cmbProduct.SelectedValue != null)//Fired if the condition met
            {
                this.selectedProductId = (int)cmbProduct.SelectedValue;//Setting the Id based on the selected value
                UpdateUnitPriceTextBox(selectedProductId);//The Id Pass to the function
            }
        }

        private void UpdateUnitPriceTextBox(int productId)//Value Update everytime the Product Combobox was Change
        {
            customerOrderDetailsRepository = new CustomerOrderDetailsRepository();//Connection to the repository
            string query = "SELECT inventory.price FROM inventory WHERE inventory.`name` = '" + productId + "';";
            txtPrice.Text = customerOrderDetailsRepository.ExecuteScalar(query).ToString();//Getting the value of TextBox Price using the id of Product
        }

        private void lvOrderList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.LoadCustomerOrderModalData();
        }
    }
}
