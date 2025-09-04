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
using System.Windows.Forms.DataVisualization.Charting;

namespace App.ManagementModule
{
    public partial class ManagementModule : Form
    {
        ManagementModuleRepository repo;
        public ManagementModule()
        {
            InitializeComponent();
            ManagementModuleData();
        }
        private void ManagementModuleData()
        {
            cmbInterval.Items.AddRange(new object[] { 5, 6, 7, 8, 9, 10 });
            cmbInterval.SelectedItem = 5;

            cmbStockFilter.Items.AddRange(new object[] { "All Items", "Overstocked Items", "Low Stock Items" });
            cmbStockFilter.SelectedItem = "All Items";

            repo = new ManagementModuleRepository();

            foreach (var dtp in new[] { dtpSaleStartDate, dtpSaleEndDate, dtpInventoryMovementStartDate, dtpInventoryMovementEndDate })
            {
                dtp.MinDate = new DateTime(1990, 1, 1);
                dtp.MaxDate = DateTime.Today;
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = "MM/dd/yyyy";
            }

            SetCurrentMonthDateRange(dtpSaleStartDate, dtpSaleEndDate);
            SetCurrentMonthDateRange(dtpInventoryMovementStartDate, dtpInventoryMovementEndDate);

            ConfigureAxisX();
            LoadManagementModule();
        }
        private void SetCurrentMonthDateRange(DateTimePicker startPicker, DateTimePicker endPicker)
        {
            DateTime today = DateTime.Today;
            DateTime firstDay = new DateTime(today.Year, today.Month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

            if (lastDay > today)
                lastDay = today;

            startPicker.Value = firstDay;
            endPicker.Value = lastDay;
        }
        private void ConfigureAxisX()
        {
            DateTime startSaleDate = dtpSaleStartDate.Value.Date;
            DateTime endSaleDate = dtpSaleEndDate.Value.Date;

            double totalDays = (endSaleDate - startSaleDate).TotalDays;
            int sections = int.Parse(cmbInterval.SelectedItem.ToString());
            double intervalValue;

            var axisX = cSales.ChartAreas[0].AxisX;

            if (totalDays <= 31)
            {
                axisX.IntervalType = DateTimeIntervalType.Days;
                axisX.LabelStyle.Format = "MM/dd";
                intervalValue = Math.Max(1, totalDays / sections);
            }
            else if (totalDays <= 180)
            {
                axisX.IntervalType = DateTimeIntervalType.Weeks;
                axisX.LabelStyle.Format = "MM/dd";
                intervalValue = Math.Max(1, totalDays / 7 / sections);
            }
            else if (totalDays <= 730)
            {
                axisX.IntervalType = DateTimeIntervalType.Months;
                axisX.LabelStyle.Format = "MMM yyyy";
                intervalValue = Math.Max(1, totalDays / 30 / sections);
            }
            else
            {
                axisX.IntervalType = DateTimeIntervalType.Years;
                axisX.LabelStyle.Format = "yyyy";
                intervalValue = Math.Max(1, totalDays / 365 / sections);
            }

            axisX.Interval = intervalValue;
            axisX.LabelStyle.Angle = -45;

            // Inventory chart uses same config
            var axisXInventory = cInventoryMovement.ChartAreas[0].AxisX;
            axisXInventory.IntervalType = axisX.IntervalType;
            axisXInventory.LabelStyle.Format = axisX.LabelStyle.Format;
            axisXInventory.Interval = intervalValue;
            axisXInventory.LabelStyle.Angle = -45;
        }

        private void ManagementModule_Load(object sender, EventArgs e)
        {
            string[] moduleNames = { "Dashboard", "CustomerOrder", "Customer", "SupplierOrder", "Supplier", "Product", "Inventory", "Report", "UserManagement", "ManagementModule" };
            int startX = 340;
            int startY = 55;
            int spacing = 25;

            for (int i = 0; i < moduleNames.Length; i++)
            {
                App.Toggle_Button.ToggleButton toggle = new App.Toggle_Button.ToggleButton();
                toggle.Name = "tb" + moduleNames[i].Replace(" ", "");
                toggle.Location = new Point(startX, startY + i * spacing);
                toggle.Checked = true;
                ModuleControlPanel.Controls.Add(toggle);
            }
        }
        private void LoadManagementModule()
        {
            DateTime dtSaleStartDate = dtpSaleStartDate.Value.Date;
            DateTime dtSaleEndDate = dtpSaleEndDate.Value.Date;
            DateTime dtInventoryStartDate = dtpInventoryMovementStartDate.Value.Date;
            DateTime dtInventoryEndDate = dtpInventoryMovementEndDate.Value.Date;

            LoadSalesChart(dtSaleStartDate, dtSaleEndDate);
            LoadFluctuationInventoryChart(dtInventoryStartDate, dtInventoryEndDate);
        }
        private void LoadSalesChart(DateTime start, DateTime end)
        {
            repo = new ManagementModuleRepository();
            DataTable dt = repo.LoadSalesChart(start, end);

            cSales.Series.Clear();
            Series series = new Series("Sales")
            {
                ChartType = SeriesChartType.Line
            };

            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                DataRow[] rows = dt.Select($"OrderDate = '{date:yyyy-MM-dd}'");
                decimal sales = rows.Length > 0 ? (decimal)rows[0]["DailySales"] : 0;

                int pointIndex = series.Points.AddXY(date, sales);

                if (sales == 0)
                {
                    series.Points[pointIndex].IsValueShownAsLabel = false;
                }
                else
                {
                    series.Points[pointIndex].IsValueShownAsLabel = true;
                }
            }

            cSales.Series.Add(series);
            cSales.Legends[0].Enabled = false;
        }

        private void LoadFluctuationInventoryChart(DateTime start, DateTime end)
        {
            repo = new ManagementModuleRepository();
            DataTable dt = repo.LoadFluctuationInventoryChart(start, end);

            cInventoryMovement.Series.Clear();
            Series series = new Series("Inventory")
            {
                ChartType = SeriesChartType.Line
            };

            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                DataRow[] rows = dt.Select($"Date = '{date:yyyy-MM-dd}'");
                decimal movement = rows.Length > 0 ? (decimal)rows[0]["NetMovement"] : 0;

                int pointIndex = series.Points.AddXY(date, movement);

                if (movement == 0)
                {
                    series.Points[pointIndex].IsValueShownAsLabel = false;
                }
                else
                {
                    series.Points[pointIndex].IsValueShownAsLabel = true;
                }
            }

            cInventoryMovement.Series.Add(series);
            cInventoryMovement.Legends[0].Enabled = false;
        }

        private void DatePickers_ValueChanged(object sender, EventArgs e)
        {
            ConfigureAxisX();
            LoadManagementModule();
        }

        private void cmbInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigureAxisX();
            LoadManagementModule();
        }
    }
}
