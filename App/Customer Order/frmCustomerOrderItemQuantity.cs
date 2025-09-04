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
    public partial class frmCustomerOrderItemQuantity : Form
    {
        public delegate void ProductQuantityHandler(int productQuantity);
        public event ProductQuantityHandler ProductQuantity;
        public frmCustomerOrderItemQuantity()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQuantity.Text) || !string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                ProductQuantity?.Invoke(int.Parse(txtQuantity.Text));
                this.Close();
            }
        }
    }
}
