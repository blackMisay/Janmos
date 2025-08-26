using App.Customer;
using App.Inventory;
using Core.System.Data.Model;
using Core.System.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace App.Customer_Order
{
    public partial class frmCustomerOrderModal : Form
    {
        CustomerOrderRepository customerOrderRepository;
        CustomerOrderDetailsRepository customerOrderDetailsRepository;
        private List<CustomerOrderDetails> tempOrderDetails;
        private readonly int Id = 0;
        private readonly int enable = 0;
        private int selectedCustomerId = 0;
        private string selectedOrderNumber = "";
        private int selectedInventoryId = 0;
        private string str_order_date = "yyyy-MM-dd";
        private decimal sum = 0;
        private double unitPrice = 0;
        private int quantity = 0;
        private string lastInsertedOrderNumber = "00000000000";
        private string firstEightDigits = "00000000";
        private string lastThreeDigits = "000";
        private int numericalValue = 0;
        private bool productOrderValidate = true;
        private string userName = "";

        //FOR NEW CUSTOMER ORDER
        public frmCustomerOrderModal(string username) 
        {
            this.userName = username;
            InitializeComponent();
            InitializeComponentsData();
            FieldEnabling(enable);
        }

        //FOR EXISTING CUSTOMER ORDER
        public frmCustomerOrderModal(int customerOrderId) 
        {
            InitializeComponent();
            this.Id = customerOrderId;
            InitializeSelectedCustomerOrderData();
            FieldEnabling(customerOrderId);
            UpdateTotalPrice();
            lblResetFields.Enabled = false;
        }

        //FOR ENABLING FORM FIELDS
        private void FieldEnabling(int customerOrderId) 
        {
            txtCustomerSelected.Enabled = false;
            txtProductSelected.Enabled = false;
            txtUnitPrice.Enabled = false;
            txtQuantity.Enabled = false;
            if (customerOrderId > enable)
            {
                btnSaveItem.Enabled = false;
                btnSelectCustomer.Enabled = false;
                btnSelectCustomer.SendToBack();
                txtCustomerSelected.Enabled = false;
            }
            else
            {
                lvOrderList.Items.Clear();
                btnSaveItem.Enabled = false;
                btnEdit.Enabled = false;
                cmbStatus.SelectedIndex = -1;
                lblTotalPrice.Text = "0.00";
            }
        }

        //INITIALIZING COMPONENTS DATA OF CUSTOMER ORDER MODAL FORM
        private void InitializeComponentsData()
        {
            customerOrderRepository = new CustomerOrderRepository();

            dtpOrderDate.Value = DateTime.Today;
            dtpOrderDate.Enabled = false;

            //For Default Order Number in Customer Order
            // GETTING THE LAST INSERTED ORDER NUMBER IN THE DATABASE
            // LAST INSERTED NUMBER = "YYYYMMDD000"
            string query = "SELECT customerorder.ordernumber FROM customerorder ORDER BY customerorder.id DESC LIMIT 1;";
            this.lastInsertedOrderNumber = customerOrderRepository.GetLastInsertedOrderNumber(query);

            // GETTING THE FIRST EIGHT DIGITS FROM THE LAST INSERTED NUMBER
            // CONVERTING STRING LONG
            // FIRST EIGHT DIGITS = YYYYMMDD
            if (long.TryParse(this.lastInsertedOrderNumber, out long longEightDigits))
            {
                long.TryParse(this.firstEightDigits, out long longFirstEightDigits);
                longFirstEightDigits = longEightDigits / 1000;

                this.firstEightDigits = longFirstEightDigits.ToString();
            }

            // GETTING THE LAST 3 DIGITS IN THE LAST INSERTED ORDER NUMBER
            // CONVERTING STRING TO LONG
            // LAST THREE DIGITS = "000"
            if (long.TryParse(this.lastInsertedOrderNumber, out long longThreeDigits))
            {
                long.TryParse(this.lastThreeDigits, out long longLastThreeDigits);
                longLastThreeDigits = longThreeDigits % 1000;

                this.lastThreeDigits = longLastThreeDigits.ToString("D3");
            }

            // GETTING THE CURRENT DATE IN YEAR/MONTH/DAY FORMAT
            this.str_order_date = dtpOrderDate.Value.ToString("yyyyMMdd");

            // GENERATING NEW ORDER NUMBER
            // IF THE LAST INSERTED ORDER NUMBER IS LESS THAN THE CURRENT DATE
            // LAST THREE DIGIT NUMBER START IN "000"
            if (int.Parse(this.str_order_date) > int.Parse(this.firstEightDigits))
            {
                this.lastThreeDigits = "000";
            }
            // ELSE IF LAST INSERTED ORDER NUMBER IS EQUAL TO THE CURRENT DATE
            // LAST THREE DIGIT NUMBER WILL BE INCREMENT BY 1
            // EX. FROM "000" TO "001"
            else if (int.Parse(this.str_order_date) == int.Parse(this.firstEightDigits))
            {
                this.numericalValue = int.Parse(this.lastThreeDigits);
                this.numericalValue++;

                this.lastThreeDigits = numericalValue.ToString("D3");
            }

            // COMBINING THE CURRENT DATE AND THE LAST THREE DIGIT GENERATED
            lblOrderNumber.Text = this.str_order_date + this.lastThreeDigits;

            //Status Combobox
            //cmbStatus.DataSource = Enum.GetValues(typeof(StatusType));
            cmbStatus.DataSource = customerOrderRepository.LoadDataList("SELECT `cos`.id, `cos`.orderstatus FROM dbjanmos.customerorderstatus `cos`;");
            cmbStatus.ValueMember = "id";
            cmbStatus.DisplayMember = "orderstatus";
        }

        private void InitializeSelectedCustomerOrderData()
        {
            InitializeComponentsData();

            customerOrderRepository = new CustomerOrderRepository();

            CustomerOrder customerOrder = new CustomerOrder();
            customerOrder = customerOrderRepository.FetchCustomerOrderData(this.Id); //FETCHING CUSTOMER ORDER DATA

            this.lblOrderNumber.Text = customerOrder.OrderNumber; //LABEL ORDER NUMBER VALUE = CUSTOMER ORDER 'ORDER NUMBER'

            this.selectedCustomerId = customerOrder.Customer.Id; //AFTER GETTING THE CUSTOMER ID SAVED FROM THE DATABASE
            string query = "SELECT customer.`name` FROM customer WHERE customer.id = @CustomerId;"; //CUSTOMER ID WILL USE TO GET THE CUSTOMER NAME
            this.txtCustomerSelected.Text = customerOrderRepository.GetStringValue(query, new Dictionary<string, string>
            {
                { "@CustomerId", this.selectedCustomerId.ToString() }
            }); //AND RETURN ITS VALUE TO THE TXTCUSTOMERSELECTED TEXTBOX

            this.dtpOrderDate.Text = customerOrder.OrderDate; //DATE TIME PICKER VALUE = CUSTOMER ORDER 'ORDER DATE'
            this.cmbStatus.SelectedItem = customerOrder.OrderStatus.ToString(); //COMBO BOX SELECTED ITEM = CUSTOMER ORDER 'ORDER STATUS'
            lblTotalPrice.Text = customerOrder.TotalPrice; //LABEL TOTAL PRICE = CUSTOMER ORDER 'TOTAL PRICE'

            //FETCHING CUSTOMER ORDER ITEMS IN LISTVIEW
            customerOrderDetailsRepository = new CustomerOrderDetailsRepository();
            List<CustomerOrderDetails> COD = customerOrderDetailsRepository.GetCustomerOrder(customerOrder.OrderNumber);

            lvOrderList.Items.Clear();

            foreach (var order in COD)
            {
                ListViewItem item = new ListViewItem(order.InventoryID.Id.ToString());
                item.Tag = order.InventoryID.Id;
                item.SubItems.Add(order.InventoryID?.Name?.Name?? "N/A");
                item.SubItems.Add(order.Quantity.ToString() ?? "0");
                item.SubItems.Add(order.UnitPrice.ToString("N2") ?? "0.00");
                item.SubItems.Add(order.TotalAmount.ToString("N2") ?? "0.00");
                lvOrderList.Items.Add(item);
            }
        }

        //CUSTOMER ORDER MODAL FORM LOAD
        private void frmCustomerOrderModal_Load(object sender, EventArgs e)
        {
            this.LoadCustomerOrderModalData();
        }

        //LOAD CUSTOMER ORDER MODAL DATA
        private void LoadCustomerOrderModalData()
        {
            this.btnSelectCustomer.Focus();
            System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
            ProductOrderButtonEnabling(btn);
        }

        // LISTVIEW SUBITEMS
        public List<CustomerOrderDetails> orderListFromListView(System.Windows.Forms.ListView listview, string orderNumber)
        {
            List<CustomerOrderDetails> orderDetailsItemList = new List<CustomerOrderDetails>();

            foreach (ListViewItem item in listview.Items)
            {
                CustomerOrderDetails orderDetailsListItem = new CustomerOrderDetails();
                orderDetailsListItem.CustomerOrderNumber = new CustomerOrder() { OrderNumber = orderNumber };
                orderDetailsListItem.InventoryID = new Core.System.Data.Model.Inventory() { Id = Convert.ToInt32(item.SubItems[0].Text.Split(' ').Last()) };
                orderDetailsListItem.Quantity = int.Parse(item.SubItems[2].Text);
                orderDetailsListItem.UnitPrice = double.Parse(item.SubItems[3].Text);
                orderDetailsListItem.TotalAmount = double.Parse(item.SubItems[4].Text);
                orderDetailsListItem.Status = item.SubItems.Count > 5 ? (StatusRecord.Type)Enum.Parse(typeof(StatusRecord.Type), item.SubItems[5].Text) : StatusRecord.Type.Active;
                orderDetailsItemList.Add(orderDetailsListItem);
            }
            return orderDetailsItemList;
        }

        //CUSTOMER ORDER MODAL SAVE BUTTON
        private void btnSave_Click(object sender, EventArgs e)
        {
            //FIELD VALIDATION
            FieldValidate();
        }

        //CUSTOMER ORDER MODAL FIELD VALIDATION
        private void FieldValidate()
        {
            this.str_order_date = this.dtpOrderDate.Value.ToString("yyyy-MM-dd");

            bool validated = true;

            if (string.IsNullOrEmpty(txtCustomerSelected.Text) || string.IsNullOrWhiteSpace(txtCustomerSelected.Text))
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
            customerOrder.OrderNumber = lblOrderNumber.Text;
            customerOrder.Customer = new Core.System.Data.Model.Customer() { Id = Convert.ToInt32(this.selectedCustomerId)};
            //StatusType orderStatusType = (StatusType)cmbStatus.SelectedValue;
            customerOrder.OrderStatus = new CustomerOrderStatus() { Id = Convert.ToInt32(this.cmbStatus.SelectedValue) };
            customerOrder.OrderDate = this.str_order_date;
            //customerOrder.OrderStatus = orderStatusType;
            customerOrder.TotalPrice = this.lblTotalPrice.Text;
            customerOrder.Status = StatusRecord.Type.Active;

            customerOrderRepository = new CustomerOrderRepository();

            if (MessageBox.Show("Are you sure you want to save customer order record?", "Save Customer Order Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (customerOrderRepository.Save(customerOrder))
                {
                    List<CustomerOrderDetails> orderDetailsList = orderListFromListView(lvOrderList, customerOrder.OrderNumber);
                    foreach (CustomerOrderDetails orderDetails in orderDetailsList)
                    {
                        customerOrderDetailsRepository = new CustomerOrderDetailsRepository();
                        if (orderDetails.Status == StatusRecord.Type.Active)
                        {
                            customerOrderDetailsRepository.Save(orderDetails);
                        }
                        else if (orderDetails.Status == StatusRecord.Type.Deleted)
                        {
                            customerOrderDetailsRepository.Delete(orderDetails);
                        }
                    }

                    InventoryRepository inventoryRepo = new InventoryRepository();
                    InventoryMovementRepository inventoryMovementRepo = new InventoryMovementRepository();

                    int currentUserId = GetLoggedInUserId();

                    foreach (CustomerOrderDetails orderDetails in orderDetailsList)
                    {
                        int productId = orderDetails.InventoryID.Id;
                        int qty = orderDetails.Quantity;

                        bool stockUpdated = inventoryRepo.UpdateInventoryStock(productId, qty);

                        bool movementLogged = inventoryMovementRepo.InsertInventoryMovement(
                            productId,
                            qty,
                            "OUT",
                            $"Customer Order #{customerOrder.OrderNumber}",
                            currentUserId
                        );

                        if (!stockUpdated || !movementLogged)
                        {
                            MessageBox.Show("Inventory update or movement log failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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

        //CUSTOMER ORDER MODAL CANCEL BUTTON
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close this form without saving?", "Customer Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        //CLEAR CUSTOMER ORDER MODAL FIELDS
        private void lblResetFields_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Pressing the 'Clear all fields' will clear all values in fields and dropdown menus. Are you sure do you want to proceed?", "Clear all fields", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                cmbStatus.SelectedIndex = -1;
                dtpOrderDate.Value = DateTime.Now;
                btnSelectProduct.BringToFront();
                txtCustomerSelected.Clear();
                txtProductSelected.Clear();
                txtQuantity.Clear();
                txtUnitPrice.Clear();
            }
        }

        // PRODUCT ORDER FIELD VALIDATE
        private void ProductOrderFieldValidate()
        {
            productOrderValidate = true;
            // FIELD VALIDATION IF THE FIELDS ARE NOT NULL, EMPTY, OR WHITESPACES
            if (string.IsNullOrEmpty(txtProductSelected.Text) || string.IsNullOrEmpty(txtUnitPrice.Text) || string.IsNullOrEmpty(txtQuantity.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Product Order Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (string.IsNullOrEmpty(txtProductSelected.Text))
                {
                    productOrderValidate = false;
                    lblProduct.Visible = true;
                }
                else
                {
                    lblProduct.Visible = false;
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text) || string.IsNullOrWhiteSpace(txtUnitPrice.Text))
                {
                    productOrderValidate = false;
                    lblPrice.Visible = true;
                }
                else
                {
                    lblPrice.Visible = false;
                }
                if (string.IsNullOrEmpty(txtQuantity.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text))
                {
                    productOrderValidate = false;
                    lblQuantity.Visible = true;
                }
                else
                {
                    lblQuantity.Visible = false;
                }
                return;
            }
                
            //IF THE PRICE IS NOT DOUBLE OR THE QUANTITY NOT INTEGER
            if (!double.TryParse(txtUnitPrice.Text, out unitPrice) || !int.TryParse(txtQuantity.Text, out quantity))
            {
                //IF PRICE VALUE IS NOT DOUBLE
                if (!double.TryParse(txtUnitPrice.Text, out unitPrice))
                {
                    productOrderValidate = false;
                    lblPrice.Visible = true;
                }
                else
                {
                    lblPrice.Visible = false;
                }
            
                //IF QUANTITY VALUE IS NOT INTEGER
                if (!int.TryParse(txtQuantity.Text, out quantity))
                {
                    productOrderValidate = false;
                    lblQuantity.Visible = true;
                }
                else
                {
                    lblQuantity.Visible = false;
                }
                MessageBox.Show("Please enter valid data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        // PRODUCT ORDER FIELD ADD BUTTON FOR ADDING PRODUCT TO THE LISTVIEW
        private void btnAdd_Click(object sender, EventArgs e)//Button Add for ListView
        {
            ProductOrderFieldValidate();
            if (!productOrderValidate)
                return;

            bool itemExists = false;

            this.unitPrice = double.Parse(txtUnitPrice.Text);
            this.quantity = int.Parse(txtQuantity.Text);

            foreach (ListViewItem item in lvOrderList.Items)
            {
                if (item.Tag != null && (int)item.Tag == this.selectedInventoryId)
                {
                    int existingQuantity = int.Parse(item.SubItems[2].Text);

                    int newQuantity = existingQuantity + this.quantity;
                    double newAmount = newQuantity * this.unitPrice;

                    item.SubItems[2].Text = newQuantity.ToString();
                    item.SubItems[4].Text = newAmount.ToString("N2");

                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                ListViewItem item = new ListViewItem(this.selectedInventoryId.ToString());
                item.Tag = this.selectedInventoryId;
                item.SubItems.Add(txtProductSelected.Text);
                item.SubItems.Add(txtQuantity.Text);
                item.SubItems.Add(txtUnitPrice.Text);
                item.SubItems.Add((this.unitPrice * this.quantity).ToString("F2"));

                lvOrderList.Items.Add(item);
            }

            UpdateTotalPrice();
            this.selectedInventoryId = 0;
            ClearInputFields();
            this.ProductOrderButtonEnabling(sender);
            this.LoadCustomerOrderModalData();
        }

        private void btnDelete_Click(object sender, EventArgs e) //PROBLEM -------------------------------------------------------
        {
            tempOrderDetails = new List<CustomerOrderDetails>();
            customerOrderRepository = new CustomerOrderRepository();
            ListViewItem selectedItem = lvOrderList.SelectedItems[0];
            if (lvOrderList.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Do you want to delete the selected item?", "Delete Item", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (!int.TryParse(selectedItem.SubItems[0].Text, out this.selectedInventoryId))
                    {
                        MessageBox.Show("Invalid inventory ID. Deletion failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (customerOrderRepository.VerifyCustomerOrder(lblOrderNumber.Text) != 0)
                    {
                        customerOrderDetailsRepository = new CustomerOrderDetailsRepository();
                        if (customerOrderDetailsRepository.VerifyCustomerOrderDetailsIfExisted(lblOrderNumber.Text, this.selectedInventoryId) != 0)
                        {
                            if (tempOrderDetails == null || !tempOrderDetails.Any())
                            {
                                MessageBox.Show($"temporderdetails is empty or null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            foreach (var detail in tempOrderDetails)
                            {
                                MessageBox.Show($"Debug: InventoryID in tempOrderDetails = {detail.InventoryID.Id}");
                            }
                            // Mark the item as deleted in tempOrderDetails
                            var orderDetail = tempOrderDetails.FirstOrDefault(o => o.InventoryID.Id == selectedInventoryId);
                            if (orderDetail == null)
                            {
                                MessageBox.Show("No matching order detail found in temporderdetails.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                                /*orderDetail.Status = StatusRecord.Type.Deleted; // Mark as deleted

                                // Visually indicate deletion
                                selectedItem.ForeColor = Color.Gray;
                                selectedItem.Text = "[Deleted] " + selectedItem.Text;
                                selectedItem.Tag = "Deleted"; // Prevent duplicate processing*/
                            }
                            orderDetail.Status = StatusRecord.Type.Deleted;
                            MessageBox.Show("Item marked as deleted successfully.");
                        }
                        else
                        {
                            lvOrderList.Items.Remove(selectedItem);
                        }
                    }
                    else
                    {
                        lvOrderList.Items.Remove(selectedItem);
                    }
                }
                selectedItem.Selected = false;
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateTotalPrice();
            this.ProductOrderButtonEnabling(sender);
            this.LoadCustomerOrderModalData();
        }

        // PRODUCT ORDER FIELD EDIT BUTTON FOR EDITING SELECTED PRODUCT ITEM IN THE LISTVIEW
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvOrderList.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvOrderList.SelectedItems[0]; // Selected Item in ListView
                string str_inventoryIDText = selectedItem.SubItems[0].Text; // String Product Id
                string str_inventoryID = str_inventoryIDText.Split(' ').Last();
                txtProductSelected.Text = selectedItem.SubItems[1].Text; // String Product Name
                txtQuantity.Text = selectedItem.SubItems[2].Text; // String Quantity
                txtUnitPrice.Text = selectedItem.SubItems[3].Text; // String Unit Price

                txtProductSelected.BringToFront();

                bool productFound = false;
                this.selectedInventoryId = int.Parse(str_inventoryID);
                lvOrderList.Enabled = false;

                if (this.selectedInventoryId != 0 && this.selectedInventoryId.ToString() == str_inventoryID)
                {
                    productFound = true;
                }

                if (!productFound)
                {
                    MessageBox.Show("Product not found in Inventory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.selectedInventoryId = 0;
                }
                btnSaveItem.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please select an item to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.ProductOrderButtonEnabling(sender);
        }

        // PRODUCT ORDER SAVE ITEM BUTTON FOR SAVING PRODUCT CHANGES
        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            if (lvOrderList.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvOrderList.SelectedItems[0];
                string updatedProduct = txtProductSelected.Text; // Updated String Product
                string updatedUnitPrice = txtUnitPrice.Text; // Updated String Unit Price
                string updatedQuantity = txtQuantity.Text; // Updated String Quantity

                if (!double.TryParse(updatedUnitPrice, out unitPrice) || !int.TryParse(updatedQuantity, out quantity)) // If unitprice value not double and quatity not integer
                {
                    MessageBox.Show("Please enter valid data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double newTotalAmount = unitPrice * quantity; // getting the new total amount of unitprice & quantity

                if (this.selectedInventoryId == Convert.ToInt32(lvOrderList.SelectedItems[0].SubItems[0].Text))
                {
                    selectedItem.SubItems[1].Text = updatedProduct; // Product Name
                    selectedItem.SubItems[2].Text = quantity.ToString(); // Quantity
                    selectedItem.SubItems[3].Text = unitPrice.ToString("N2"); // Unit Price
                    selectedItem.SubItems[4].Text = newTotalAmount.ToString("N2"); // Total Amount
                }
                else
                {
                    selectedItem.SubItems[0].Text = this.selectedInventoryId.ToString(); // Inventory Id
                    selectedItem.SubItems[1].Text = updatedProduct; // Product Name
                    selectedItem.SubItems[2].Text = quantity.ToString(); // Quantity
                    selectedItem.SubItems[3].Text = unitPrice.ToString("N2"); // Unit Price
                    selectedItem.SubItems[4].Text = newTotalAmount.ToString("N2"); // Total Amount
                }

                if (selectedItem.SubItems.Count < 6)
                {
                    selectedItem.SubItems.Add(StatusRecord.Type.Active.ToString());
                }
                else
                {
                    selectedItem.SubItems[5].Text = StatusRecord.Type.Active.ToString();
                }

                lvOrderList.Enabled = true;

                UpdateTotalPrice();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please select an item to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.selectedInventoryId = 0;
            this.ProductOrderButtonEnabling(sender);
            lvOrderList.SelectedItems.Clear();
        }
        
        // PRODUCT ORDER BUTTON FIELD ENABLING
        private void ProductOrderButtonEnabling(object sender)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            btn.Enabled = false;
            if (lvOrderList.Items.Count >= 0) // for add button enabling
            {
                btnAdd.Enabled = true;
            }

            if (lvOrderList.Items.Count == 0) // if listview list count is equal to 0
            {
                btnDelete.Enabled = btnEdit.Enabled = false; // button delete & edit not enabled
            }
            else if(lvOrderList.SelectedItems.Count == 0) // if listview selecteditem count is equal to 0
            {
                txtQuantity.Enabled = true;
                btnSelectProduct.Enabled = true;
                btnDelete.Enabled = btnEdit.Enabled = false; // button delete & edit not enabled
            }
            else if (lvOrderList.SelectedItems.Count > 0) // if listview selecteditem count is greater than 0
            {
                txtQuantity.Enabled = false;
                btnAdd.Enabled = false; // button add not enabled
                btnDelete.Enabled = btnEdit.Enabled = true; // button delete & edit enabled
                btnSelectProduct.Enabled = false;
            }

            if (btn == btnAdd) // for add button condition
            {
                btnDelete.Enabled = btnEdit.Enabled = false; // button delete & edit not enabled
            }

            if (btn == btnEdit) // for save button condition
            {
                txtQuantity.Enabled = btnSaveItem.Enabled = true; // button save item enabled
                btnAdd.Enabled = btnDelete.Enabled = btnEdit.Enabled = false; // button add, delete & edit not enabled
            }

            if (btn == btnSaveItem) // for save item button condition
            {
                btnSaveItem.Enabled = false; // button save not enabled
            }
        }

        // FOR CALCULATING TOTAL VALUE
        private void UpdateTotalPrice()
        {
            sum = 0m;
            foreach (ListViewItem item in lvOrderList.Items)
            {
                if (item.Text.StartsWith("[Deleted]"))
                {
                    continue;
                }
                if (decimal.TryParse(item.SubItems[4].Text, out decimal totalAmount))
                {
                    sum += totalAmount;
                }
                else
                {
                    MessageBox.Show($"Invalid amount in order list: {item.SubItems[4].Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            lblTotalPrice.Text = sum.ToString("N2");
        }

        //CLEAR FIELD DATA
        private void ClearInputFields()
        {
            txtProductSelected.Clear();
            txtProductSelected.SendToBack();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            unitPrice = 0;
            quantity = 0;
            lblProduct.Visible = false;
            lblPrice.Visible = false;
            lblQuantity.Visible = false;
        }

        // LISTVIEW ITEM SELECTION CHANGED EVENT
        private void lvOrderList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.LoadCustomerOrderModalData();
        }

        //CUSTOMER ORDER MODAL SELECT CUSTOMER BUTTON
        //FOR SELECTING CUSTOMER FROM CUSTOMER TABLE
        private void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            //REDIRECTING TO FRMCUSTOMERBRANCH FORM
            using (frmCustomerBranch frmCB = new frmCustomerBranch())
            {
                frmCB.CustomerIDSelected += frmCustomerBranch_CustomerIDSelected; //for retrieving selected customer id from customer branch
                frmCB.CustomerNameSelected += frmCustomerBranch_CustomerNameSelected; //for retrieving customer name from customer branch
                frmCB.ShowDialog();
            }
            if (string.IsNullOrEmpty(txtCustomerSelected.Text) || string.IsNullOrWhiteSpace(txtCustomerSelected.Text))
            {
                txtCustomerSelected.SendToBack();
            }
            else
            {
                txtCustomerSelected.BringToFront();
            }
        }

        //EVENT HANDLER THAT WILL RECEIVE CUSTOMER ID FROM FRMCUSTOMERBRANCH
        private void frmCustomerBranch_CustomerIDSelected(int customerID)
        {
            this.selectedCustomerId = customerID;
        }

        //EVENT HANDLER THAT WILL RECEIVE CUSTOMER NAME FROM FRMCUSTOMERBRANCH
        private void frmCustomerBranch_CustomerNameSelected(string customerName)
        {
            txtCustomerSelected.Text = customerName;
        }

        //CUSTOMER ORDER MODAL SELECT PRODUCT BUTTON
        //FOR SELECTING PRODUCT FROM INVENTORY
        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            //REDIRECTING TO FRMINVENTORYBRANCH FORM
            using (frmInventoryBranch frmIB = new frmInventoryBranch())
            {
                frmIB.InventoryIdSelected += frmInventoryBranch_InventoryIdSelected; //for retrieving selected inventory id from inventory branch
                frmIB.ProductSelected += frmInventoryBranch_ProductSelected; //for retrieving product name from inventory branch
                frmIB.ProductPrice += frmInventoryBranch_ProductPrice; //for retrieving product price from inventory branch
                frmIB.ShowDialog();
            }
            using(frmCustomerOrderItemQuantity frmCOIQ = new frmCustomerOrderItemQuantity())
            {
                frmCOIQ.ProductQuantity += frmInventoryBranch_ProductQuantity;
                frmCOIQ.ShowDialog();
            }

            if (string.IsNullOrEmpty(txtProductSelected.Text) || string.IsNullOrWhiteSpace(txtProductSelected.Text))
            {
                txtProductSelected.SendToBack();
            }
            else
            {
                txtProductSelected.BringToFront();
            }
        }

        //EVENT HANDLER THAT WILL RECEIVE INVENTORY ID FROM FRMINVENTORYBRANCH
        private void frmInventoryBranch_InventoryIdSelected(int inventoryId)
        {
            this.selectedInventoryId = inventoryId;
        }

        //EVENT HANDLER THAT WILL RECEIVE PRODUCT NAME FROM FRMINVENTORYBRANCH
        private void frmInventoryBranch_ProductSelected(string productName)
        {
            txtProductSelected.Text = productName;
        }

        //EVENT HANDLER THAT WILL RECEIVE PRODUCT PRICE OF SELECTED PRODUCT FROM FRMINVENTORYBRANCH
        private void frmInventoryBranch_ProductPrice(double productPrice)
        {
            txtUnitPrice.Text = productPrice.ToString("N2");
        }
        private void frmInventoryBranch_ProductQuantity(int productQuantity)
        {
            txtQuantity.Text = productQuantity.ToString();
        }
    }
}
