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
    public partial class CustomerModal : Form
    {
        public CustomerModal()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CustomerModal_Load(object sender, EventArgs e)
        {
            this.txtCustomerName.Focus();
            cmbStatus.SelectedItem = "Active";
        }
    }
}
