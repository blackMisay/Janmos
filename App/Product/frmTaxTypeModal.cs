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

namespace App.Product
{
    public partial class frmTaxTypeModal : Form
    {
        private readonly User user = new User();
        private readonly int Id = 0;

        TaxTypeRepository taxTypeRepository;
        public frmTaxTypeModal(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            decimal rate;
            if (decimal.TryParse(this.txtRate.Text, out rate))
            {
                rate = rate / 100m;
            }
            else
            {
                MessageBox.Show("Invalid rate value.");
            }

            taxTypeRepository = new TaxTypeRepository();
            TaxType taxtype = new TaxType();
            taxtype.Id = this.Id;
            taxtype.TaxName = this.txtTaxName.Text;
            taxtype.Rate = rate;
            taxtype.CreatedBy = new User() { Id = this.user.Id};
            taxtype.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

            if (MessageBox.Show("Do you want to add a new tax type?", "Adding new tax type", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                if (taxTypeRepository.Save(taxtype, this.user))
                {
                    MessageBox.Show("Adding new tax type successful.", "Adding new tax type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
        }
    }
}
