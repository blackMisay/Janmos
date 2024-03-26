using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Supplier
{
    public partial class frmSupplier : Form
    {
        public frmSupplier()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            using (frmSupplierModal newSupplier = new frmSupplierModal())
            {
                newSupplier.ShowDialog();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // TODO: Pass selected Id
            using (frmSupplierModal newSupplier = new frmSupplierModal())
            {
                newSupplier.ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete the selected supplier?","Delete supplier", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // TODO: Delete function
            }
        }
    }
}
