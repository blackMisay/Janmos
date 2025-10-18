namespace App
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pnlModule = new System.Windows.Forms.Panel();
            this.pnlProductContainer = new System.Windows.Forms.Panel();
            this.pnlDashboardContainer = new System.Windows.Forms.Panel();
            this.pnlMenuSidebar = new System.Windows.Forms.Panel();
            this.btnManagementModule = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnUserManagement = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnReport = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnInventory = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnProduct = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnCustomerOrder = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnToggle = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tpMain = new System.Windows.Forms.ToolTip(this.components);
            this.panel11 = new System.Windows.Forms.Panel();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.tmrDateTime = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            this.pnlMenuSidebar.SuspendLayout();
            this.panel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(70)))), ((int)(((byte)(162)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnLogout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.panel2.Size = new System.Drawing.Size(1196, 42);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label1.Size = new System.Drawing.Size(334, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "J-anmos - Sales and Inventory Management System";
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(82)))), ((int)(((byte)(189)))));
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(1112, 5);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(79, 32);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "&Log out";
            this.tpMain.SetToolTip(this.btnLogout, "Log out\r\n\r\nTerminate your session in the application.");
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // pnlModule
            // 
            this.pnlModule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlModule.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlModule.Location = new System.Drawing.Point(70, 92);
            this.pnlModule.Name = "pnlModule";
            this.pnlModule.Size = new System.Drawing.Size(1126, 642);
            this.pnlModule.TabIndex = 3;
            // 
            // pnlProductContainer
            // 
            this.pnlProductContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProductContainer.Location = new System.Drawing.Point(10, 155);
            this.pnlProductContainer.Name = "pnlProductContainer";
            this.pnlProductContainer.Padding = new System.Windows.Forms.Padding(13, 5, 13, 0);
            this.pnlProductContainer.Size = new System.Drawing.Size(50, 10);
            this.pnlProductContainer.TabIndex = 6;
            // 
            // pnlDashboardContainer
            // 
            this.pnlDashboardContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDashboardContainer.Location = new System.Drawing.Point(10, 100);
            this.pnlDashboardContainer.Name = "pnlDashboardContainer";
            this.pnlDashboardContainer.Padding = new System.Windows.Forms.Padding(13, 5, 13, 0);
            this.pnlDashboardContainer.Size = new System.Drawing.Size(50, 10);
            this.pnlDashboardContainer.TabIndex = 5;
            // 
            // pnlMenuSidebar
            // 
            this.pnlMenuSidebar.Controls.Add(this.btnManagementModule);
            this.pnlMenuSidebar.Controls.Add(this.panel10);
            this.pnlMenuSidebar.Controls.Add(this.btnUserManagement);
            this.pnlMenuSidebar.Controls.Add(this.panel9);
            this.pnlMenuSidebar.Controls.Add(this.btnReport);
            this.pnlMenuSidebar.Controls.Add(this.panel8);
            this.pnlMenuSidebar.Controls.Add(this.btnInventory);
            this.pnlMenuSidebar.Controls.Add(this.panel7);
            this.pnlMenuSidebar.Controls.Add(this.btnProduct);
            this.pnlMenuSidebar.Controls.Add(this.panel3);
            this.pnlMenuSidebar.Controls.Add(this.btnCustomer);
            this.pnlMenuSidebar.Controls.Add(this.pnlProductContainer);
            this.pnlMenuSidebar.Controls.Add(this.btnCustomerOrder);
            this.pnlMenuSidebar.Controls.Add(this.pnlDashboardContainer);
            this.pnlMenuSidebar.Controls.Add(this.btnDashboard);
            this.pnlMenuSidebar.Controls.Add(this.btnToggle);
            this.pnlMenuSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenuSidebar.Location = new System.Drawing.Point(0, 42);
            this.pnlMenuSidebar.Name = "pnlMenuSidebar";
            this.pnlMenuSidebar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 5);
            this.pnlMenuSidebar.Size = new System.Drawing.Size(70, 707);
            this.pnlMenuSidebar.TabIndex = 4;
            // 
            // btnManagementModule
            // 
            this.btnManagementModule.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnManagementModule.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnManagementModule.FlatAppearance.BorderSize = 0;
            this.btnManagementModule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManagementModule.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManagementModule.Image = ((System.Drawing.Image)(resources.GetObject("btnManagementModule.Image")));
            this.btnManagementModule.Location = new System.Drawing.Point(10, 440);
            this.btnManagementModule.Name = "btnManagementModule";
            this.btnManagementModule.Size = new System.Drawing.Size(50, 45);
            this.btnManagementModule.TabIndex = 28;
            this.tpMain.SetToolTip(this.btnManagementModule, "Management Module\r\n\r\nOversee business operations \r\nand configure system settings." +
        "");
            this.btnManagementModule.UseVisualStyleBackColor = false;
            this.btnManagementModule.Click += new System.EventHandler(this.btnManagementModule_Click);
            // 
            // panel10
            // 
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(10, 430);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(13, 5, 13, 0);
            this.panel10.Size = new System.Drawing.Size(50, 10);
            this.panel10.TabIndex = 27;
            // 
            // btnUserManagement
            // 
            this.btnUserManagement.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnUserManagement.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUserManagement.FlatAppearance.BorderSize = 0;
            this.btnUserManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserManagement.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserManagement.Image = ((System.Drawing.Image)(resources.GetObject("btnUserManagement.Image")));
            this.btnUserManagement.Location = new System.Drawing.Point(10, 385);
            this.btnUserManagement.Name = "btnUserManagement";
            this.btnUserManagement.Size = new System.Drawing.Size(50, 45);
            this.btnUserManagement.TabIndex = 26;
            this.tpMain.SetToolTip(this.btnUserManagement, "User Management\r\n\r\nManage user accounts, roles, and\r\npermissions for secure acces" +
        "s.");
            this.btnUserManagement.UseVisualStyleBackColor = false;
            this.btnUserManagement.Click += new System.EventHandler(this.btnUserManagement_Click);
            // 
            // panel9
            // 
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(10, 375);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(13, 5, 13, 0);
            this.panel9.Size = new System.Drawing.Size(50, 10);
            this.panel9.TabIndex = 25;
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Image = ((System.Drawing.Image)(resources.GetObject("btnReport.Image")));
            this.btnReport.Location = new System.Drawing.Point(10, 330);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(50, 45);
            this.btnReport.TabIndex = 24;
            this.tpMain.SetToolTip(this.btnReport, "Data Reports\r\n\r\nGenerate data reports to display \r\ninformation, forecasts, and an" +
        "alyses \r\nfor making business decisions.");
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // panel8
            // 
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(10, 320);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(13, 5, 13, 0);
            this.panel8.Size = new System.Drawing.Size(50, 10);
            this.panel8.TabIndex = 23;
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnInventory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInventory.FlatAppearance.BorderSize = 0;
            this.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventory.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventory.Image = ((System.Drawing.Image)(resources.GetObject("btnInventory.Image")));
            this.btnInventory.Location = new System.Drawing.Point(10, 275);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(50, 45);
            this.btnInventory.TabIndex = 22;
            this.tpMain.SetToolTip(this.btnInventory, "Inventory\r\n\r\nManage and track stock of items");
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(10, 265);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(13, 5, 13, 0);
            this.panel7.Size = new System.Drawing.Size(50, 10);
            this.panel7.TabIndex = 21;
            // 
            // btnProduct
            // 
            this.btnProduct.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProduct.FlatAppearance.BorderSize = 0;
            this.btnProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProduct.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProduct.Image = ((System.Drawing.Image)(resources.GetObject("btnProduct.Image")));
            this.btnProduct.Location = new System.Drawing.Point(10, 220);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Size = new System.Drawing.Size(50, 45);
            this.btnProduct.TabIndex = 20;
            this.tpMain.SetToolTip(this.btnProduct, "Product\r\n\r\nView, Search, Add, and Modify product records.");
            this.btnProduct.UseVisualStyleBackColor = false;
            this.btnProduct.Click += new System.EventHandler(this.btnProduct_Click);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 210);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(13, 5, 13, 0);
            this.panel3.Size = new System.Drawing.Size(50, 10);
            this.panel3.TabIndex = 13;
            // 
            // btnCustomer
            // 
            this.btnCustomer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCustomer.FlatAppearance.BorderSize = 0;
            this.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomer.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomer.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomer.Image")));
            this.btnCustomer.Location = new System.Drawing.Point(10, 165);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(50, 45);
            this.btnCustomer.TabIndex = 12;
            this.tpMain.SetToolTip(this.btnCustomer, "Customer\r\n\r\nFacilitates customer, and order information.  ");
            this.btnCustomer.UseVisualStyleBackColor = false;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // btnCustomerOrder
            // 
            this.btnCustomerOrder.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCustomerOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCustomerOrder.FlatAppearance.BorderSize = 0;
            this.btnCustomerOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomerOrder.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomerOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomerOrder.Image")));
            this.btnCustomerOrder.Location = new System.Drawing.Point(10, 110);
            this.btnCustomerOrder.Name = "btnCustomerOrder";
            this.btnCustomerOrder.Size = new System.Drawing.Size(50, 45);
            this.btnCustomerOrder.TabIndex = 1;
            this.tpMain.SetToolTip(this.btnCustomerOrder, "Customer Order\r\n\r\nDisplay and manage customer orders,\r\ntrack order status, and vi" +
        "ew order\r\ndetails.");
            this.btnCustomerOrder.UseVisualStyleBackColor = false;
            this.btnCustomerOrder.Click += new System.EventHandler(this.btnCustomerOrder_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.Black;
            this.btnDashboard.Image = ((System.Drawing.Image)(resources.GetObject("btnDashboard.Image")));
            this.btnDashboard.Location = new System.Drawing.Point(10, 50);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(50, 50);
            this.btnDashboard.TabIndex = 0;
            this.tpMain.SetToolTip(this.btnDashboard, "Dashboard\r\n\r\nDisplay a summary of insights \r\nand statistics about sales and \r\ninv" +
        "entory.");
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnToggle
            // 
            this.btnToggle.BackColor = System.Drawing.Color.Transparent;
            this.btnToggle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnToggle.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnToggle.FlatAppearance.BorderSize = 0;
            this.btnToggle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnToggle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggle.Image = ((System.Drawing.Image)(resources.GetObject("btnToggle.Image")));
            this.btnToggle.Location = new System.Drawing.Point(10, 0);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(50, 50);
            this.btnToggle.TabIndex = 3;
            this.tpMain.SetToolTip(this.btnToggle, "Toggle Menu\r\n\r\nShow or hide application modules.");
            this.btnToggle.UseVisualStyleBackColor = false;
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(70, 734);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1126, 15);
            this.panel1.TabIndex = 5;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.lblDateTime);
            this.panel11.Controls.Add(this.lblUser);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(70, 42);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1126, 50);
            this.panel11.TabIndex = 6;
            // 
            // lblDateTime
            // 
            this.lblDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.Location = new System.Drawing.Point(940, 15);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(54, 21);
            this.lblDateTime.TabIndex = 1;
            this.lblDateTime.Text = "label2";
            // 
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(18, 15);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(51, 21);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "label1";
            // 
            // tmrDateTime
            // 
            this.tmrDateTime.Enabled = true;
            this.tmrDateTime.Tick += new System.EventHandler(this.tmrDateTime_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1196, 749);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.pnlModule);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlMenuSidebar);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlMenuSidebar.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlModule;
        private System.Windows.Forms.Button btnCustomerOrder;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Panel pnlDashboardContainer;
        private System.Windows.Forms.Panel pnlProductContainer;
        private System.Windows.Forms.Button btnToggle;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlMenuSidebar;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip tpMain;
        private System.Windows.Forms.Button btnProduct;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnManagementModule;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button btnUserManagement;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Timer tmrDateTime;
    }
}