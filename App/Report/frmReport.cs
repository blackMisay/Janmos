using App.Properties;
using Core.System.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Report
{
    public partial class frmReport : Form
    {
        PrintDialog printDialog = new PrintDialog();
        ReportRepository reportRepository;
        private Button b;
        private int currentRowIndex = 0;
        private int totalValue = 0;
        private int currentPage = 1;
        Font printFont = new Font("Arial", 10);
        Font boldFont = new Font("Arial", 10, FontStyle.Bold);
        private int lineHeight = 25;
        Image logo = Resources.j_anmos_logo;
        public frmReport()
        {
            InitializeComponent();

            dtpFrom.Value = DateTime.Now.Date;
            dtpFrom.MaxDate = DateTime.Now.Date;
            dtpTo.Value = DateTime.Now.Date;
            dtpTo.MaxDate = DateTime.Now.Date;

            lblFilter.Visible = false;
            cmbFilter.Visible = false;
            cmbFilter.SelectedIndex = 0;
        }
        private void LoadData(Button b)
        {
            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date;
            if (fromDate > toDate)
            {
                MessageBox.Show("The 'From' date cannot be later than 'To' date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (toDate > DateTime.Now)
            {
                MessageBox.Show("The 'To' date cannot be later than today.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (b == btnSales)
            {
                lblFilter.Visible = false;
                cmbFilter.Visible = false;
                LoadSalesData(fromDate, toDate);
            }
            else if (b == btnInventory)
            {
                lblFilter.Visible = true;
                cmbFilter.Visible = true;
                string filter = cmbFilter.SelectedItem.ToString();
                LoadInventoryData(fromDate.ToString("yyyy-MM-dd"), toDate.AddDays(1).ToString("yyyy-MM-dd"), filter);
            }
        }
        private void LoadSalesData(DateTime selectedDateFrom, DateTime selectedDateTo)
        {
            reportRepository = new ReportRepository();

            if (selectedDateFrom == DateTime.Now && selectedDateTo == DateTime.Now)
            {
                dgvReport.DataSource = reportRepository.LoadSalesData();
            }
            else
            {
                dgvReport.DataSource = reportRepository.LoadSalesData(selectedDateFrom.ToString("yyyy-MM-dd"), selectedDateTo.ToString("yyyy-MM-dd"));
            }
        }
        private void LoadInventoryData(string selectedDateFrom, string selectedDateTo, string selectedFilter)
        {
            reportRepository = new ReportRepository();

            if (selectedDateFrom == DateTime.Now.ToString("yyyy-MM-dd") && selectedDateTo == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                dgvReport.DataSource = reportRepository.LoadInventoryData();
            }
            else
            {
                dgvReport.DataSource = reportRepository.LoadInventoryData(selectedDateFrom, selectedDateTo, selectedFilter);
            }
        }

        class ReportPage
        {
            public string SerialNumber { get; set; } = "Serial123456";
            public string ReportDateTime { get; set; } = DateTime.Now.ToString();
            public bool Pass { get; set; } = false;
            public double ExpectedMeasurement { get; set; } = 100;
            public double ActualMeasurement { get; set; } = 50;

            public double Error
            {
                get { return ((ActualMeasurement - ExpectedMeasurement) / ExpectedMeasurement) * 100; }
            }
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            b = sender as Button;
            LoadData(b);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            b = sender as Button;
            LoadData(b);
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            LoadData(b);
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(b);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            currentRowIndex = 0;
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int marginLeft = e.MarginBounds.Left;
            int marginTop = e.MarginBounds.Top;
            int pageWidth = e.MarginBounds.Width;

            // Draw centered logo
            int logoWidth = 120;
            int logoHeight = 50;
            int logoX = marginLeft + (pageWidth - logoWidth) / 2;
            int logoY = marginTop;
            e.Graphics.DrawImage(logo, new Rectangle(logoX, logoY, logoWidth, logoHeight));

            // Draw report date
            string reportDate = "Date Generated: " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt");
            Font dateFont = new Font("Arial", 9, FontStyle.Italic);
            int dateTextWidth = (int)e.Graphics.MeasureString(reportDate, dateFont).Width;
            int dateX = marginLeft + (pageWidth - dateTextWidth) / 2;
            int dateY = logoY + logoHeight + 5;
            e.Graphics.DrawString(reportDate, dateFont, Brushes.Black, dateX, dateY);

            // Draw page number
            string pageNumberText = "Page: " + currentPage++;
            Font pageFont = new Font("Arial", 9, FontStyle.Regular);
            int pageTextWidth = (int)e.Graphics.MeasureString(pageNumberText, pageFont).Width;
            int pageX = e.MarginBounds.Right - pageTextWidth;
            e.Graphics.DrawString(pageNumberText, pageFont, Brushes.Black, pageX, dateY);

            // Draw title based on selected date range
            string reportType = b == btnInventory ? "Inventory Report" : "Sales Report";
            string titleDateRange = dtpFrom.Value.ToString("MMMM dd, yyyy") + " to " + dtpTo.Value.ToString("MMMM dd, yyyy");
            string titleText = reportType + ": " + titleDateRange;
            Font titleFont = new Font("Arial", 12, FontStyle.Bold);
            int titleWidth = (int)e.Graphics.MeasureString(titleText, titleFont).Width;
            int titleX = marginLeft + (pageWidth - titleWidth) / 2;
            int titleY = dateY + lineHeight;
            e.Graphics.DrawString(titleText, titleFont, Brushes.Black, titleX, titleY);

            int y = titleY + lineHeight;
            int totalColumnCount = dgvReport.Columns.Count;
            int spacing = 10;
            int usableWidth = pageWidth - (spacing * (totalColumnCount - 1));
            int[] colWidths = new int[totalColumnCount];
            for (int i = 0; i < totalColumnCount; i++)
            {
                colWidths[i] = usableWidth / totalColumnCount;
            }

            int usedWidth = colWidths.Take(totalColumnCount - 1).Sum();
            colWidths[totalColumnCount - 1] = pageWidth - usedWidth - (spacing * (totalColumnCount - 1));
            string[] headers = dgvReport.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText).ToArray();

            int x = marginLeft;
            for (int i = 0; i < headers.Length; i++)
            {
                e.Graphics.DrawString(headers[i], boldFont, Brushes.Black, x, y);
                x += colWidths[i] + spacing;
            }

            y += lineHeight;
            totalValue = 0;

            while (currentRowIndex < dgvReport.Rows.Count)
            {
                DataGridViewRow row = dgvReport.Rows[currentRowIndex];
                x = marginLeft;
                for (int i = 0; i < headers.Length; i++)
                {
                    string cellValue = row.Cells[i].Value?.ToString();
                    e.Graphics.DrawString(cellValue, printFont, Brushes.Black, x, y);
                    x += colWidths[i];
                }

                // Accumulate value
                if (b == btnInventory && row.Cells["Quantity"] != null && int.TryParse(row.Cells["quantity"].Value?.ToString(), out int qty))
                    totalValue += qty;
                if (b == btnSales && row.Cells["Total Price"] != null && int.TryParse(row.Cells["totalprice"].Value?.ToString(), out int sales))
                    totalValue += sales;

                y += lineHeight;
                currentRowIndex++;

                if (y + lineHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            // Draw total at the bottom
            string totalLabel = b == btnInventory ? "Total Remaining Products: " : "Total Sales: ";
            e.Graphics.DrawString(totalLabel + totalValue.ToString(), boldFont, Brushes.Black, marginLeft, y + 20);
            e.HasMorePages = false;
        }
    }
}