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

namespace App.Paginator
{
    public partial class SimplePager : UserControl
    {
        private int _pageCount;
        private const int min_page = 1;
        public SimplePager()
        {
            InitializeComponent();
        }

        public void SetPageCount(int pageCount)
        {
            if (pageCount == _pageCount)
            {
                return;
            }

            _pageCount = pageCount;

            for (int page = min_page; page <= _pageCount; page++)
            {
                cmbPageNumber.Items.Add(page);
            }

            cmbPageNumber.SelectedItem = min_page;

            return;
        }

        public void SetPage(object sender, EventArgs e)
        {
            int page = 0;

            if (sender == btnFirstPage)
            {
                page = 1;
            }
            else if (sender == btnLastPage)
            {
                page = _pageCount;
            }
            else if (sender == btnPrevious)
            {
                page = Convert.ToInt32(cmbPageNumber.SelectedValue) - 1;
            }
            else if (sender == btnNext)
            {
                page = Convert.ToInt32(cmbPageNumber.SelectedValue) + 1;
            }

            cmbPageNumber.SelectedItem = page;
            ButtonActivation(page);
        }

        private void ButtonActivation(int currentPage)
        {
            foreach (var control in Controls)
                if (control is Button button)
                    button.Enabled = true;

            if (currentPage == 1)
            {
                btnFirstPage.Enabled = false;
                btnPrevious.Enabled = false;
            }
            else if (currentPage == _pageCount)
            {
                btnNext.Enabled = false;
                btnLastPage.Enabled = false;
            }
        }

        private void cmbPageNumber_SelectedValueChanged(object sender, EventArgs e)
        {
            int newPageNumber = Convert.ToInt32(cmbPageNumber.SelectedItem);
            ButtonActivation(newPageNumber);
        }
    }
}
