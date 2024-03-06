using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Product
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (ProductModal info = new ProductModal()) { 
                info.ShowDialog();
            }
            this.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (ProductModal info = new ProductModal())
            {
                info.ShowDialog();
            }
            this.Show();
        }
    }
}
