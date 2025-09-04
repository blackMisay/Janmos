using App.Product;
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

namespace App.Supplier
{
    public partial class frmSupplier : Form
    {
        private int selectedSupplierId = 0;
        private readonly int defaultRowCount = 20;
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
            this.LoadSupplierData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedSupplierId != 0)
            {
                using (frmSupplierModal editSupplier = new frmSupplierModal(this.selectedSupplierId))
                {
                    editSupplier.ShowDialog();
                }
                this.LoadSupplierData();
            }
            else
            {
                MessageBox.Show("Please select a supplier to update.", "Update supplier", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedSupplierId > 0)
            {
                SupplierRepository supplierRepository = new SupplierRepository();
                supplierRepository.DeleteSupplierData(selectedSupplierId);
                this.LoadSupplierData();
                selectedSupplierId = 0;

                MessageBox.Show("Delete Successfully.", "Delete supplier", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Please select a supplier to delete.", "Delete supplier", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            this.LoadSupplierData();
        }

        private void LoadSupplierData()
        {
            SupplierRepository supplierRepository = new SupplierRepository();
            dgvSupplier.DataSource = supplierRepository.LoadSupplierData();
            this.dgvSupplier.Columns["Supplier Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvSupplier.Columns["Mobile Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvSupplier.Columns["Phone Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void dgvSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSupplier.RowCount > 0)
            {
                int selectedRowIndex = dgvSupplier.SelectedCells[0].RowIndex;
                this.selectedSupplierId = Convert.ToInt32(dgvSupplier.Rows[selectedRowIndex].Cells[0].Value?.ToString());
            }
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            SupplierRepository supplierRepository = new SupplierRepository();
            dgvSupplier.DataSource = supplierRepository.LoadSupplierData(txtSearch.Text);
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.selectedSupplierId = 0;
        }

        private void frmSupplier_Click(object sender, EventArgs e)
        {
            this.selectedSupplierId = 0;    
        }
    }
}
