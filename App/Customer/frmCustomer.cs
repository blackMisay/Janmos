using App.Customer;
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
        public Customer()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (CustomerModal cmodal = new CustomerModal())
            {
                cmodal.ShowDialog();
            }
            this.Show();
        }

        private void Customer_Load(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (CustomerModal cmodal = new CustomerModal())
            {
                cmodal.ShowDialog();
            }
            this.Show();
        }
    }
}
