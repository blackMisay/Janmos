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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pnlModule = new System.Windows.Forms.Panel();
            this.pnlProductContainer = new System.Windows.Forms.Panel();
            this.pnlDashboardContainer = new System.Windows.Forms.Panel();
            this.sidebarTransition = new System.Windows.Forms.Timer(this.components);
            this.pnlMenuSidebar = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnProduct = new System.Windows.Forms.Button();
            this.btnAccount = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnSupplier = new System.Windows.Forms.Button();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnToggle = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.pnlMenuSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 731);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1196, 43);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(70)))), ((int)(((byte)(162)))));
            this.panel2.Controls.Add(this.btnLogout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.panel2.Size = new System.Drawing.Size(1196, 42);
            this.panel2.TabIndex = 1;
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
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // pnlModule
            // 
            this.pnlModule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlModule.Location = new System.Drawing.Point(70, 42);
            this.pnlModule.Name = "pnlModule";
            this.pnlModule.Size = new System.Drawing.Size(1126, 689);
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
            // sidebarTransition
            // 
            this.sidebarTransition.Interval = 1;
            this.sidebarTransition.Tick += new System.EventHandler(this.sidebarTransition_Tick);
            // 
            // pnlMenuSidebar
            // 
            this.pnlMenuSidebar.Controls.Add(this.btnAccount);
            this.pnlMenuSidebar.Controls.Add(this.panel5);
            this.pnlMenuSidebar.Controls.Add(this.btnSetting);
            this.pnlMenuSidebar.Controls.Add(this.btnSupplier);
            this.pnlMenuSidebar.Controls.Add(this.panel4);
            this.pnlMenuSidebar.Controls.Add(this.btnCustomer);
            this.pnlMenuSidebar.Controls.Add(this.panel3);
            this.pnlMenuSidebar.Controls.Add(this.btnInventory);
            this.pnlMenuSidebar.Controls.Add(this.pnlProductContainer);
            this.pnlMenuSidebar.Controls.Add(this.btnProduct);
            this.pnlMenuSidebar.Controls.Add(this.pnlDashboardContainer);
            this.pnlMenuSidebar.Controls.Add(this.btnDashboard);
            this.pnlMenuSidebar.Controls.Add(this.btnToggle);
            this.pnlMenuSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenuSidebar.Location = new System.Drawing.Point(0, 42);
            this.pnlMenuSidebar.Name = "pnlMenuSidebar";
            this.pnlMenuSidebar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 5);
            this.pnlMenuSidebar.Size = new System.Drawing.Size(70, 689);
            this.pnlMenuSidebar.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(10, 629);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(13, 5, 13, 0);
            this.panel5.Size = new System.Drawing.Size(50, 10);
            this.panel5.TabIndex = 17;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(10, 265);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(13, 5, 13, 0);
            this.panel4.Size = new System.Drawing.Size(50, 10);
            this.panel4.TabIndex = 14;
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
            // btnProduct
            // 
            this.btnProduct.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProduct.FlatAppearance.BorderSize = 0;
            this.btnProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProduct.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProduct.Image = global::App.Properties.Resources.product_24p;
            this.btnProduct.Location = new System.Drawing.Point(10, 110);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Size = new System.Drawing.Size(50, 45);
            this.btnProduct.TabIndex = 1;
            this.btnProduct.UseVisualStyleBackColor = false;
            this.btnProduct.Click += new System.EventHandler(this.btnProduct_Click);
            // 
            // btnAccount
            // 
            this.btnAccount.BackColor = System.Drawing.Color.Transparent;
            this.btnAccount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAccount.FlatAppearance.BorderSize = 0;
            this.btnAccount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAccount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccount.Image = global::App.Properties.Resources.account_24px;
            this.btnAccount.Location = new System.Drawing.Point(10, 584);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(50, 45);
            this.btnAccount.TabIndex = 18;
            this.btnAccount.UseVisualStyleBackColor = false;
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.Transparent;
            this.btnSetting.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSetting.FlatAppearance.BorderSize = 0;
            this.btnSetting.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSetting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetting.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetting.Image = global::App.Properties.Resources.settings_24px;
            this.btnSetting.Location = new System.Drawing.Point(10, 639);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(50, 45);
            this.btnSetting.TabIndex = 16;
            this.btnSetting.UseVisualStyleBackColor = false;
            // 
            // btnSupplier
            // 
            this.btnSupplier.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSupplier.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSupplier.FlatAppearance.BorderSize = 0;
            this.btnSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupplier.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplier.Image = global::App.Properties.Resources.supplier_24px;
            this.btnSupplier.Location = new System.Drawing.Point(10, 275);
            this.btnSupplier.Name = "btnSupplier";
            this.btnSupplier.Size = new System.Drawing.Size(50, 45);
            this.btnSupplier.TabIndex = 2;
            this.btnSupplier.UseVisualStyleBackColor = false;
            // 
            // btnCustomer
            // 
            this.btnCustomer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCustomer.FlatAppearance.BorderSize = 0;
            this.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomer.Image = global::App.Properties.Resources.customer_24px;
            this.btnCustomer.Location = new System.Drawing.Point(10, 220);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(50, 45);
            this.btnCustomer.TabIndex = 2;
            this.btnCustomer.UseVisualStyleBackColor = false;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnInventory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInventory.FlatAppearance.BorderSize = 0;
            this.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventory.Image = global::App.Properties.Resources.inventory_24px;
            this.btnInventory.Location = new System.Drawing.Point(10, 165);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(50, 45);
            this.btnInventory.TabIndex = 12;
            this.btnInventory.UseVisualStyleBackColor = false;
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.Black;
            this.btnDashboard.Image = global::App.Properties.Resources.dashboard_24px;
            this.btnDashboard.Location = new System.Drawing.Point(10, 50);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(50, 50);
            this.btnDashboard.TabIndex = 0;
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
            this.btnToggle.UseVisualStyleBackColor = false;
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1196, 774);
            this.Controls.Add(this.pnlModule);
            this.Controls.Add(this.pnlMenuSidebar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            this.pnlMenuSidebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlModule;
        private System.Windows.Forms.Button btnProduct;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Panel pnlDashboardContainer;
        private System.Windows.Forms.Panel pnlProductContainer;
        private System.Windows.Forms.Button btnToggle;
        private System.Windows.Forms.Timer sidebarTransition;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlMenuSidebar;
        private System.Windows.Forms.Button btnSupplier;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnAccount;
    }
}