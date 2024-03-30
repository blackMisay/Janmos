namespace App.Purchase
{
    partial class frmPurchaseManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurchaseManagement));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgPurchaseOrder = new System.Windows.Forms.TabPage();
            this.tpgPurchaseRequisition = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvPurchaseOrderList = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbRecordCount = new System.Windows.Forms.ComboBox();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnNewPO = new System.Windows.Forms.Button();
            this.txtSearchPONumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dgvPurchaseRequisitionList = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpgPurchaseOrder.SuspendLayout();
            this.tpgPurchaseRequisition.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseOrderList)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseRequisitionList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1127, 106);
            this.panel2.TabIndex = 70;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Purchase Management";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpgPurchaseOrder);
            this.tabControl1.Controls.Add(this.tpgPurchaseRequisition);
            this.tabControl1.Location = new System.Drawing.Point(20, 137);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1127, 622);
            this.tabControl1.TabIndex = 71;
            // 
            // tpgPurchaseOrder
            // 
            this.tpgPurchaseOrder.Controls.Add(this.dgvPurchaseOrderList);
            this.tpgPurchaseOrder.Controls.Add(this.panel3);
            this.tpgPurchaseOrder.Controls.Add(this.panel1);
            this.tpgPurchaseOrder.Location = new System.Drawing.Point(4, 32);
            this.tpgPurchaseOrder.Name = "tpgPurchaseOrder";
            this.tpgPurchaseOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPurchaseOrder.Size = new System.Drawing.Size(1119, 586);
            this.tpgPurchaseOrder.TabIndex = 0;
            this.tpgPurchaseOrder.Text = "Orders";
            this.tpgPurchaseOrder.UseVisualStyleBackColor = true;
            // 
            // tpgPurchaseRequisition
            // 
            this.tpgPurchaseRequisition.Controls.Add(this.dgvPurchaseRequisitionList);
            this.tpgPurchaseRequisition.Controls.Add(this.panel5);
            this.tpgPurchaseRequisition.Controls.Add(this.panel4);
            this.tpgPurchaseRequisition.Location = new System.Drawing.Point(4, 32);
            this.tpgPurchaseRequisition.Name = "tpgPurchaseRequisition";
            this.tpgPurchaseRequisition.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPurchaseRequisition.Size = new System.Drawing.Size(1119, 586);
            this.tpgPurchaseRequisition.TabIndex = 1;
            this.tpgPurchaseRequisition.Text = "Requisitions";
            this.tpgPurchaseRequisition.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSearchPONumber);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnNewPO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1113, 56);
            this.panel1.TabIndex = 0;
            // 
            // dgvPurchaseOrderList
            // 
            this.dgvPurchaseOrderList.AllowUserToAddRows = false;
            this.dgvPurchaseOrderList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchaseOrderList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvPurchaseOrderList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseOrderList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchaseOrderList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPurchaseOrderList.Location = new System.Drawing.Point(3, 59);
            this.dgvPurchaseOrderList.Name = "dgvPurchaseOrderList";
            this.dgvPurchaseOrderList.RowHeadersVisible = false;
            this.dgvPurchaseOrderList.RowHeadersWidth = 51;
            this.dgvPurchaseOrderList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchaseOrderList.Size = new System.Drawing.Size(1113, 479);
            this.dgvPurchaseOrderList.TabIndex = 71;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnLastPage);
            this.panel3.Controls.Add(this.btnFirstPage);
            this.panel3.Controls.Add(this.btnPrevious);
            this.panel3.Controls.Add(this.btnNext);
            this.panel3.Controls.Add(this.cmbRecordCount);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 538);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1113, 45);
            this.panel3.TabIndex = 72;
            // 
            // cmbRecordCount
            // 
            this.cmbRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRecordCount.BackColor = System.Drawing.Color.LightGray;
            this.cmbRecordCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecordCount.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRecordCount.FormattingEnabled = true;
            this.cmbRecordCount.ItemHeight = 25;
            this.cmbRecordCount.Items.AddRange(new object[] {
            "5",
            "10",
            "20",
            "50",
            "100",
            "200"});
            this.cmbRecordCount.Location = new System.Drawing.Point(1049, 6);
            this.cmbRecordCount.Name = "cmbRecordCount";
            this.cmbRecordCount.Size = new System.Drawing.Size(61, 33);
            this.cmbRecordCount.TabIndex = 82;
            // 
            // btnLastPage
            // 
            this.btnLastPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLastPage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLastPage.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLastPage.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLastPage.Image = global::App.Properties.Resources.lastpage_16px;
            this.btnLastPage.Location = new System.Drawing.Point(607, 7);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(30, 30);
            this.btnLastPage.TabIndex = 86;
            this.btnLastPage.UseVisualStyleBackColor = false;
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFirstPage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnFirstPage.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnFirstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirstPage.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirstPage.Image = global::App.Properties.Resources.firstpage_16px;
            this.btnFirstPage.Location = new System.Drawing.Point(475, 7);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(30, 30);
            this.btnFirstPage.TabIndex = 85;
            this.btnFirstPage.UseVisualStyleBackColor = false;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrevious.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPrevious.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.Location = new System.Drawing.Point(519, 7);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(30, 30);
            this.btnPrevious.TabIndex = 84;
            this.btnPrevious.UseVisualStyleBackColor = false;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNext.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(563, 7);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 30);
            this.btnNext.TabIndex = 83;
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // btnNewPO
            // 
            this.btnNewPO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewPO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(195)))), ((int)(((byte)(112)))));
            this.btnNewPO.FlatAppearance.BorderSize = 0;
            this.btnNewPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewPO.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewPO.ForeColor = System.Drawing.Color.White;
            this.btnNewPO.Location = new System.Drawing.Point(3, 5);
            this.btnNewPO.Name = "btnNewPO";
            this.btnNewPO.Size = new System.Drawing.Size(165, 45);
            this.btnNewPO.TabIndex = 65;
            this.btnNewPO.Text = "&New Purchase Order";
            this.btnNewPO.UseVisualStyleBackColor = false;
            this.btnNewPO.Click += new System.EventHandler(this.btnNewPO_Click);
            // 
            // txtSearchPONumber
            // 
            this.txtSearchPONumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchPONumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchPONumber.Location = new System.Drawing.Point(910, 13);
            this.txtSearchPONumber.Name = "txtSearchPONumber";
            this.txtSearchPONumber.Size = new System.Drawing.Size(200, 30);
            this.txtSearchPONumber.TabIndex = 67;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(769, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 23);
            this.label2.TabIndex = 66;
            this.label2.Text = "Search by P.O. #";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1113, 56);
            this.panel4.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(910, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 30);
            this.textBox1.TabIndex = 67;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(772, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 23);
            this.label3.TabIndex = 66;
            this.label3.Text = "Search by P.R. #";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(195)))), ((int)(((byte)(112)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(3, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 45);
            this.button1.TabIndex = 65;
            this.button1.Text = "&New Purchase Requisition";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button2);
            this.panel5.Controls.Add(this.button3);
            this.panel5.Controls.Add(this.button4);
            this.panel5.Controls.Add(this.button5);
            this.panel5.Controls.Add(this.comboBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(3, 538);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1113, 45);
            this.panel5.TabIndex = 73;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::App.Properties.Resources.lastpage_16px;
            this.button2.Location = new System.Drawing.Point(607, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 86;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::App.Properties.Resources.firstpage_16px;
            this.button3.Location = new System.Drawing.Point(475, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 30);
            this.button3.TabIndex = 85;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(519, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(30, 30);
            this.button4.TabIndex = 84;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(563, 7);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(30, 30);
            this.button5.TabIndex = 83;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.BackColor = System.Drawing.Color.LightGray;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 25;
            this.comboBox1.Items.AddRange(new object[] {
            "5",
            "10",
            "20",
            "50",
            "100",
            "200"});
            this.comboBox1.Location = new System.Drawing.Point(1049, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(61, 33);
            this.comboBox1.TabIndex = 82;
            // 
            // dgvPurchaseRequisitionList
            // 
            this.dgvPurchaseRequisitionList.AllowUserToAddRows = false;
            this.dgvPurchaseRequisitionList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchaseRequisitionList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvPurchaseRequisitionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseRequisitionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchaseRequisitionList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPurchaseRequisitionList.Location = new System.Drawing.Point(3, 59);
            this.dgvPurchaseRequisitionList.Name = "dgvPurchaseRequisitionList";
            this.dgvPurchaseRequisitionList.RowHeadersVisible = false;
            this.dgvPurchaseRequisitionList.RowHeadersWidth = 51;
            this.dgvPurchaseRequisitionList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchaseRequisitionList.Size = new System.Drawing.Size(1113, 479);
            this.dgvPurchaseRequisitionList.TabIndex = 74;
            // 
            // frmPurchaseManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1167, 777);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmPurchaseManagement";
            this.Padding = new System.Windows.Forms.Padding(20, 25, 20, 0);
            this.Text = "frmPurchaseManagement";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpgPurchaseOrder.ResumeLayout(false);
            this.tpgPurchaseRequisition.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseOrderList)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseRequisitionList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgPurchaseOrder;
        private System.Windows.Forms.TabPage tpgPurchaseRequisition;
        private System.Windows.Forms.DataGridView dgvPurchaseOrderList;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbRecordCount;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnNewPO;
        private System.Windows.Forms.TextBox txtSearchPONumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvPurchaseRequisitionList;
    }
}