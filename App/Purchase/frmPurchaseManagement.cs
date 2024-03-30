using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Purchase
{
    public partial class frmPurchaseManagement : Form
    {
        public frmPurchaseManagement()
        {
            InitializeComponent();
        }

        private void btnNewPO_Click(object sender, EventArgs e)
        {
            using (frmPurchaseOrderModal newPurchaseOrder = new frmPurchaseOrderModal())
            {
                newPurchaseOrder.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (frmPurchaseOrderModal newPurchaseRequisition = new frmPurchaseOrderModal())
            {
                newPurchaseRequisition.ShowDialog();
            }
        }
    }
}
