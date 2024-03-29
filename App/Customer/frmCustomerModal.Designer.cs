﻿namespace App.Customer
{
    partial class CustomerModal
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
            this.lblModule = new System.Windows.Forms.Label();
            this.gbxMain = new System.Windows.Forms.GroupBox();
            this.lblEntityName = new System.Windows.Forms.Label();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblEntity = new System.Windows.Forms.Label();
            this.cmbEntity = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.gbxContactInformation = new System.Windows.Forms.GroupBox();
            this.lblSocialNetworkID = new System.Windows.Forms.Label();
            this.txtSocialNetworkID = new System.Windows.Forms.TextBox();
            this.lblPhoneNumberExtension = new System.Windows.Forms.Label();
            this.txtPhoneNumberExtension = new System.Windows.Forms.TextBox();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblMobileNumber = new System.Windows.Forms.Label();
            this.txtMobileNumber = new System.Windows.Forms.TextBox();
            this.lblPostalCode = new System.Windows.Forms.Label();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.RichTextBox();
            this.lblBrgy = new System.Windows.Forms.Label();
            this.cmbDistrict = new System.Windows.Forms.ComboBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.cmbCity = new System.Windows.Forms.ComboBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.cmbRegion = new System.Windows.Forms.ComboBox();
            this.lblEmailAddress = new System.Windows.Forms.Label();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tipCustomerModal = new System.Windows.Forms.ToolTip(this.components);
            this.gbxMain.SuspendLayout();
            this.gbxContactInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblModule
            // 
            this.lblModule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblModule.AutoSize = true;
            this.lblModule.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModule.Location = new System.Drawing.Point(23, 25);
            this.lblModule.Name = "lblModule";
            this.lblModule.Size = new System.Drawing.Size(198, 32);
            this.lblModule.TabIndex = 94;
            this.lblModule.Text = "Create Customer";
            // 
            // gbxMain
            // 
            this.gbxMain.Controls.Add(this.lblEntityName);
            this.gbxMain.Controls.Add(this.txtEntityName);
            this.gbxMain.Controls.Add(this.lblStatus);
            this.gbxMain.Controls.Add(this.cmbStatus);
            this.gbxMain.Controls.Add(this.lblEntity);
            this.gbxMain.Controls.Add(this.cmbEntity);
            this.gbxMain.Controls.Add(this.lblName);
            this.gbxMain.Controls.Add(this.txtCustomerName);
            this.gbxMain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxMain.Location = new System.Drawing.Point(54, 102);
            this.gbxMain.Name = "gbxMain";
            this.gbxMain.Size = new System.Drawing.Size(830, 228);
            this.gbxMain.TabIndex = 95;
            this.gbxMain.TabStop = false;
            this.gbxMain.Text = "Main";
            // 
            // lblEntityName
            // 
            this.lblEntityName.AutoSize = true;
            this.lblEntityName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblEntityName.Location = new System.Drawing.Point(249, 141);
            this.lblEntityName.Name = "lblEntityName";
            this.lblEntityName.Size = new System.Drawing.Size(104, 23);
            this.lblEntityName.TabIndex = 7;
            this.lblEntityName.Text = "Entity Name";
            // 
            // txtEntityName
            // 
            this.txtEntityName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntityName.Location = new System.Drawing.Point(253, 167);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(279, 30);
            this.txtEntityName.TabIndex = 2;
            this.tipCustomerModal.SetToolTip(this.txtEntityName, "Entity name\r\n\r\nIndividual - leave as blank. (Not required)\r\nBusiness - name of bu" +
        "siness. (Required)\r\nOrganization - name of organization. (Required)\r\n\r\n");
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblStatus.Location = new System.Drawing.Point(645, 61);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 23);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Enabled = false;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Active",
            "Inactive"});
            this.cmbStatus.Location = new System.Drawing.Point(649, 86);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(131, 31);
            this.cmbStatus.TabIndex = 15;
            this.tipCustomerModal.SetToolTip(this.cmbStatus, "Customer Status");
            // 
            // lblEntity
            // 
            this.lblEntity.AutoSize = true;
            this.lblEntity.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntity.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblEntity.Location = new System.Drawing.Point(47, 140);
            this.lblEntity.Name = "lblEntity";
            this.lblEntity.Size = new System.Drawing.Size(53, 23);
            this.lblEntity.TabIndex = 3;
            this.lblEntity.Text = "Entity";
            // 
            // cmbEntity
            // 
            this.cmbEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntity.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cmbEntity.FormattingEnabled = true;
            this.cmbEntity.Items.AddRange(new object[] {
            "Individual",
            "Business",
            "Organization"});
            this.cmbEntity.Location = new System.Drawing.Point(51, 166);
            this.cmbEntity.Name = "cmbEntity";
            this.cmbEntity.Size = new System.Drawing.Size(183, 31);
            this.cmbEntity.TabIndex = 1;
            this.tipCustomerModal.SetToolTip(this.cmbEntity, "Customer entity (Required)\r\n\r\nThis serve as the customer classification.");
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblName.Location = new System.Drawing.Point(47, 60);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 23);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerName.Location = new System.Drawing.Point(51, 86);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(481, 30);
            this.txtCustomerName.TabIndex = 1;
            this.tipCustomerModal.SetToolTip(this.txtCustomerName, "Customer name (Required)");
            // 
            // gbxContactInformation
            // 
            this.gbxContactInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxContactInformation.Controls.Add(this.lblSocialNetworkID);
            this.gbxContactInformation.Controls.Add(this.txtSocialNetworkID);
            this.gbxContactInformation.Controls.Add(this.lblPhoneNumberExtension);
            this.gbxContactInformation.Controls.Add(this.txtPhoneNumberExtension);
            this.gbxContactInformation.Controls.Add(this.lblPhoneNumber);
            this.gbxContactInformation.Controls.Add(this.txtPhoneNumber);
            this.gbxContactInformation.Controls.Add(this.lblMobileNumber);
            this.gbxContactInformation.Controls.Add(this.txtMobileNumber);
            this.gbxContactInformation.Controls.Add(this.lblPostalCode);
            this.gbxContactInformation.Controls.Add(this.txtPostalCode);
            this.gbxContactInformation.Controls.Add(this.lblAddress);
            this.gbxContactInformation.Controls.Add(this.txtAddress);
            this.gbxContactInformation.Controls.Add(this.lblBrgy);
            this.gbxContactInformation.Controls.Add(this.cmbDistrict);
            this.gbxContactInformation.Controls.Add(this.lblCity);
            this.gbxContactInformation.Controls.Add(this.cmbCity);
            this.gbxContactInformation.Controls.Add(this.lblRegion);
            this.gbxContactInformation.Controls.Add(this.cmbRegion);
            this.gbxContactInformation.Controls.Add(this.lblEmailAddress);
            this.gbxContactInformation.Controls.Add(this.txtEmailAddress);
            this.gbxContactInformation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxContactInformation.Location = new System.Drawing.Point(54, 345);
            this.gbxContactInformation.Name = "gbxContactInformation";
            this.gbxContactInformation.Size = new System.Drawing.Size(830, 372);
            this.gbxContactInformation.TabIndex = 96;
            this.gbxContactInformation.TabStop = false;
            this.gbxContactInformation.Text = "Contact Information";
            // 
            // lblSocialNetworkID
            // 
            this.lblSocialNetworkID.AutoSize = true;
            this.lblSocialNetworkID.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSocialNetworkID.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSocialNetworkID.Location = new System.Drawing.Point(546, 61);
            this.lblSocialNetworkID.Name = "lblSocialNetworkID";
            this.lblSocialNetworkID.Size = new System.Drawing.Size(145, 23);
            this.lblSocialNetworkID.TabIndex = 19;
            this.lblSocialNetworkID.Text = "Social Network ID";
            // 
            // txtSocialNetworkID
            // 
            this.txtSocialNetworkID.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSocialNetworkID.Location = new System.Drawing.Point(550, 87);
            this.txtSocialNetworkID.Name = "txtSocialNetworkID";
            this.txtSocialNetworkID.Size = new System.Drawing.Size(230, 30);
            this.txtSocialNetworkID.TabIndex = 5;
            this.tipCustomerModal.SetToolTip(this.txtSocialNetworkID, "Customer Social Network ID (Optional)\r\n\r\nThis your name or identification \r\nin an" +
        "y social media platform.");
            // 
            // lblPhoneNumberExtension
            // 
            this.lblPhoneNumberExtension.AutoSize = true;
            this.lblPhoneNumberExtension.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhoneNumberExtension.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblPhoneNumberExtension.Location = new System.Drawing.Point(295, 133);
            this.lblPhoneNumberExtension.Name = "lblPhoneNumberExtension";
            this.lblPhoneNumberExtension.Size = new System.Drawing.Size(83, 23);
            this.lblPhoneNumberExtension.TabIndex = 17;
            this.lblPhoneNumberExtension.Text = "Extension";
            // 
            // txtPhoneNumberExtension
            // 
            this.txtPhoneNumberExtension.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNumberExtension.Location = new System.Drawing.Point(299, 159);
            this.txtPhoneNumberExtension.Name = "txtPhoneNumberExtension";
            this.txtPhoneNumberExtension.Size = new System.Drawing.Size(230, 30);
            this.txtPhoneNumberExtension.TabIndex = 7;
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhoneNumber.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblPhoneNumber.Location = new System.Drawing.Point(47, 133);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(127, 23);
            this.lblPhoneNumber.TabIndex = 15;
            this.lblPhoneNumber.Text = "Phone Number";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNumber.Location = new System.Drawing.Point(51, 159);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(230, 30);
            this.txtPhoneNumber.TabIndex = 6;
            this.tipCustomerModal.SetToolTip(this.txtPhoneNumber, "Primary phone number (Optional)");
            // 
            // lblMobileNumber
            // 
            this.lblMobileNumber.AutoSize = true;
            this.lblMobileNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMobileNumber.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblMobileNumber.Location = new System.Drawing.Point(47, 61);
            this.lblMobileNumber.Name = "lblMobileNumber";
            this.lblMobileNumber.Size = new System.Drawing.Size(130, 23);
            this.lblMobileNumber.TabIndex = 13;
            this.lblMobileNumber.Text = "Mobile Number";
            // 
            // txtMobileNumber
            // 
            this.txtMobileNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNumber.Location = new System.Drawing.Point(51, 87);
            this.txtMobileNumber.Name = "txtMobileNumber";
            this.txtMobileNumber.Size = new System.Drawing.Size(230, 30);
            this.txtMobileNumber.TabIndex = 3;
            this.tipCustomerModal.SetToolTip(this.txtMobileNumber, "Customer mobile number (Required)");
            // 
            // lblPostalCode
            // 
            this.lblPostalCode.AutoSize = true;
            this.lblPostalCode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostalCode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblPostalCode.Location = new System.Drawing.Point(546, 283);
            this.lblPostalCode.Name = "lblPostalCode";
            this.lblPostalCode.Size = new System.Drawing.Size(100, 23);
            this.lblPostalCode.TabIndex = 11;
            this.lblPostalCode.Text = "Postal Code";
            // 
            // txtPostalCode
            // 
            this.txtPostalCode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPostalCode.Location = new System.Drawing.Point(550, 309);
            this.txtPostalCode.Name = "txtPostalCode";
            this.txtPostalCode.Size = new System.Drawing.Size(230, 30);
            this.txtPostalCode.TabIndex = 12;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblAddress.Location = new System.Drawing.Point(47, 208);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(70, 23);
            this.lblAddress.TabIndex = 9;
            this.lblAddress.Text = "Address";
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtAddress.Location = new System.Drawing.Point(51, 234);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(226, 105);
            this.txtAddress.TabIndex = 8;
            this.txtAddress.Text = "";
            // 
            // lblBrgy
            // 
            this.lblBrgy.AutoSize = true;
            this.lblBrgy.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrgy.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblBrgy.Location = new System.Drawing.Point(295, 283);
            this.lblBrgy.Name = "lblBrgy";
            this.lblBrgy.Size = new System.Drawing.Size(151, 23);
            this.lblBrgy.TabIndex = 7;
            this.lblBrgy.Text = "Barangay / District";
            // 
            // cmbDistrict
            // 
            this.cmbDistrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistrict.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cmbDistrict.FormattingEnabled = true;
            this.cmbDistrict.Location = new System.Drawing.Point(299, 309);
            this.cmbDistrict.Name = "cmbDistrict";
            this.cmbDistrict.Size = new System.Drawing.Size(230, 31);
            this.cmbDistrict.TabIndex = 11;
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCity.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblCity.Location = new System.Drawing.Point(546, 208);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(230, 23);
            this.lblCity.TabIndex = 5;
            this.lblCity.Text = "City / Municipality / Province";
            // 
            // cmbCity
            // 
            this.cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCity.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cmbCity.FormattingEnabled = true;
            this.cmbCity.Location = new System.Drawing.Point(550, 234);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(230, 31);
            this.cmbCity.TabIndex = 10;
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegion.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblRegion.Location = new System.Drawing.Point(295, 208);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(63, 23);
            this.lblRegion.TabIndex = 3;
            this.lblRegion.Text = "Region";
            // 
            // cmbRegion
            // 
            this.cmbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegion.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cmbRegion.FormattingEnabled = true;
            this.cmbRegion.Location = new System.Drawing.Point(299, 234);
            this.cmbRegion.Name = "cmbRegion";
            this.cmbRegion.Size = new System.Drawing.Size(230, 31);
            this.cmbRegion.TabIndex = 9;
            // 
            // lblEmailAddress
            // 
            this.lblEmailAddress.AutoSize = true;
            this.lblEmailAddress.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailAddress.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblEmailAddress.Location = new System.Drawing.Point(295, 61);
            this.lblEmailAddress.Name = "lblEmailAddress";
            this.lblEmailAddress.Size = new System.Drawing.Size(116, 23);
            this.lblEmailAddress.TabIndex = 1;
            this.lblEmailAddress.Text = "Email Address";
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailAddress.Location = new System.Drawing.Point(299, 87);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(230, 30);
            this.txtEmailAddress.TabIndex = 4;
            this.tipCustomerModal.SetToolTip(this.txtEmailAddress, "Customer email address (Optional)");
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(195)))), ((int)(((byte)(112)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(661, 739);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 49);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "&Submit";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCancel.Location = new System.Drawing.Point(779, 739);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 49);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // CustomerModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(935, 809);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbxContactInformation);
            this.Controls.Add(this.gbxMain);
            this.Controls.Add(this.lblModule);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "CustomerModal";
            this.Padding = new System.Windows.Forms.Padding(20, 25, 20, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Form";
            this.Load += new System.EventHandler(this.CustomerModal_Load);
            this.gbxMain.ResumeLayout(false);
            this.gbxMain.PerformLayout();
            this.gbxContactInformation.ResumeLayout(false);
            this.gbxContactInformation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblModule;
        private System.Windows.Forms.GroupBox gbxMain;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.ComboBox cmbEntity;
        private System.Windows.Forms.Label lblEntity;
        private System.Windows.Forms.GroupBox gbxContactInformation;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.ComboBox cmbRegion;
        private System.Windows.Forms.Label lblEmailAddress;
        private System.Windows.Forms.TextBox txtEmailAddress;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.ComboBox cmbCity;
        private System.Windows.Forms.Label lblBrgy;
        private System.Windows.Forms.ComboBox cmbDistrict;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.RichTextBox txtAddress;
        private System.Windows.Forms.Label lblPostalCode;
        private System.Windows.Forms.TextBox txtPostalCode;
        private System.Windows.Forms.Label lblMobileNumber;
        private System.Windows.Forms.TextBox txtMobileNumber;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label lblPhoneNumberExtension;
        private System.Windows.Forms.TextBox txtPhoneNumberExtension;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblEntityName;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.Label lblSocialNetworkID;
        private System.Windows.Forms.TextBox txtSocialNetworkID;
        private System.Windows.Forms.ToolTip tipCustomerModal;
    }
}