namespace MTGStorage
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menu_addCardButton = new System.Windows.Forms.Button();
            this.main_mainLabel = new System.Windows.Forms.Label();
            this.menu_removeCardButton = new System.Windows.Forms.Button();
            this.menu_addLocationButton = new System.Windows.Forms.Button();
            this.menu_removeLocationButton = new System.Windows.Forms.Button();
            this.searchCardButton = new System.Windows.Forms.Button();
            this.checkLocationsButton = new System.Windows.Forms.Button();
            this.cardManagementGroupBox = new System.Windows.Forms.GroupBox();
            this.addCardBulkButton = new System.Windows.Forms.Button();
            this.scryfallSearchButton = new System.Windows.Forms.Button();
            this.locationManagementGroupBox = new System.Windows.Forms.GroupBox();
            this.exportOptionsManagement = new System.Windows.Forms.GroupBox();
            this.csvExportButton = new System.Windows.Forms.Button();
            this.defaultExportButton = new System.Windows.Forms.Button();
            this.csvImportButton = new System.Windows.Forms.Button();
            this.cardManagementGroupBox.SuspendLayout();
            this.locationManagementGroupBox.SuspendLayout();
            this.exportOptionsManagement.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu_addCardButton
            // 
            this.menu_addCardButton.Image = ((System.Drawing.Image)(resources.GetObject("menu_addCardButton.Image")));
            this.menu_addCardButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.menu_addCardButton.Location = new System.Drawing.Point(6, 21);
            this.menu_addCardButton.Name = "menu_addCardButton";
            this.menu_addCardButton.Size = new System.Drawing.Size(112, 53);
            this.menu_addCardButton.TabIndex = 0;
            this.menu_addCardButton.Text = "Add card";
            this.menu_addCardButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.menu_addCardButton.UseMnemonic = false;
            this.menu_addCardButton.UseVisualStyleBackColor = true;
            this.menu_addCardButton.Click += new System.EventHandler(this.menu_addCardButton_Click);
            // 
            // main_mainLabel
            // 
            this.main_mainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Bold);
            this.main_mainLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.main_mainLabel.Location = new System.Drawing.Point(12, 42);
            this.main_mainLabel.Name = "main_mainLabel";
            this.main_mainLabel.Size = new System.Drawing.Size(360, 56);
            this.main_mainLabel.TabIndex = 1;
            this.main_mainLabel.Text = "Welcome back!";
            this.main_mainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menu_removeCardButton
            // 
            this.menu_removeCardButton.Image = ((System.Drawing.Image)(resources.GetObject("menu_removeCardButton.Image")));
            this.menu_removeCardButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.menu_removeCardButton.Location = new System.Drawing.Point(124, 21);
            this.menu_removeCardButton.Name = "menu_removeCardButton";
            this.menu_removeCardButton.Size = new System.Drawing.Size(112, 53);
            this.menu_removeCardButton.TabIndex = 1;
            this.menu_removeCardButton.Text = "Remove card";
            this.menu_removeCardButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.menu_removeCardButton.UseMnemonic = false;
            this.menu_removeCardButton.UseVisualStyleBackColor = true;
            this.menu_removeCardButton.Click += new System.EventHandler(this.menu_removeCardButton_Click);
            // 
            // menu_addLocationButton
            // 
            this.menu_addLocationButton.Image = ((System.Drawing.Image)(resources.GetObject("menu_addLocationButton.Image")));
            this.menu_addLocationButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.menu_addLocationButton.Location = new System.Drawing.Point(6, 19);
            this.menu_addLocationButton.Name = "menu_addLocationButton";
            this.menu_addLocationButton.Size = new System.Drawing.Size(112, 53);
            this.menu_addLocationButton.TabIndex = 6;
            this.menu_addLocationButton.Text = "Create location";
            this.menu_addLocationButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.menu_addLocationButton.UseMnemonic = false;
            this.menu_addLocationButton.UseVisualStyleBackColor = true;
            this.menu_addLocationButton.Click += new System.EventHandler(this.menu_addLocationButton_Click);
            // 
            // menu_removeLocationButton
            // 
            this.menu_removeLocationButton.Image = ((System.Drawing.Image)(resources.GetObject("menu_removeLocationButton.Image")));
            this.menu_removeLocationButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.menu_removeLocationButton.Location = new System.Drawing.Point(124, 19);
            this.menu_removeLocationButton.Name = "menu_removeLocationButton";
            this.menu_removeLocationButton.Size = new System.Drawing.Size(112, 53);
            this.menu_removeLocationButton.TabIndex = 7;
            this.menu_removeLocationButton.Text = "Delete location";
            this.menu_removeLocationButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.menu_removeLocationButton.UseMnemonic = false;
            this.menu_removeLocationButton.UseVisualStyleBackColor = true;
            this.menu_removeLocationButton.Click += new System.EventHandler(this.menu_removeLocationButton_Click);
            // 
            // searchCardButton
            // 
            this.searchCardButton.Image = ((System.Drawing.Image)(resources.GetObject("searchCardButton.Image")));
            this.searchCardButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.searchCardButton.Location = new System.Drawing.Point(242, 21);
            this.searchCardButton.Name = "searchCardButton";
            this.searchCardButton.Size = new System.Drawing.Size(112, 53);
            this.searchCardButton.TabIndex = 2;
            this.searchCardButton.Text = "Search cards";
            this.searchCardButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.searchCardButton.UseMnemonic = false;
            this.searchCardButton.UseVisualStyleBackColor = true;
            this.searchCardButton.Click += new System.EventHandler(this.searchCardButton_Click);
            // 
            // checkLocationsButton
            // 
            this.checkLocationsButton.Image = ((System.Drawing.Image)(resources.GetObject("checkLocationsButton.Image")));
            this.checkLocationsButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.checkLocationsButton.Location = new System.Drawing.Point(242, 19);
            this.checkLocationsButton.Name = "checkLocationsButton";
            this.checkLocationsButton.Size = new System.Drawing.Size(112, 53);
            this.checkLocationsButton.TabIndex = 8;
            this.checkLocationsButton.Text = "Check locations";
            this.checkLocationsButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkLocationsButton.UseMnemonic = false;
            this.checkLocationsButton.UseVisualStyleBackColor = true;
            this.checkLocationsButton.Click += new System.EventHandler(this.checkLocationsButton_Click);
            // 
            // cardManagementGroupBox
            // 
            this.cardManagementGroupBox.Controls.Add(this.addCardBulkButton);
            this.cardManagementGroupBox.Controls.Add(this.scryfallSearchButton);
            this.cardManagementGroupBox.Controls.Add(this.menu_addCardButton);
            this.cardManagementGroupBox.Controls.Add(this.menu_removeCardButton);
            this.cardManagementGroupBox.Controls.Add(this.searchCardButton);
            this.cardManagementGroupBox.Location = new System.Drawing.Point(12, 121);
            this.cardManagementGroupBox.Name = "cardManagementGroupBox";
            this.cardManagementGroupBox.Size = new System.Drawing.Size(360, 142);
            this.cardManagementGroupBox.TabIndex = 8;
            this.cardManagementGroupBox.TabStop = false;
            this.cardManagementGroupBox.Text = "Card management";
            // 
            // addCardBulkButton
            // 
            this.addCardBulkButton.Image = ((System.Drawing.Image)(resources.GetObject("addCardBulkButton.Image")));
            this.addCardBulkButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.addCardBulkButton.Location = new System.Drawing.Point(6, 80);
            this.addCardBulkButton.Name = "addCardBulkButton";
            this.addCardBulkButton.Size = new System.Drawing.Size(112, 53);
            this.addCardBulkButton.TabIndex = 3;
            this.addCardBulkButton.Text = "Add card bulk";
            this.addCardBulkButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.addCardBulkButton.UseMnemonic = false;
            this.addCardBulkButton.UseVisualStyleBackColor = true;
            this.addCardBulkButton.Click += new System.EventHandler(this.addCardBulkButton_Click);
            // 
            // scryfallSearchButton
            // 
            this.scryfallSearchButton.Image = ((System.Drawing.Image)(resources.GetObject("scryfallSearchButton.Image")));
            this.scryfallSearchButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.scryfallSearchButton.Location = new System.Drawing.Point(124, 80);
            this.scryfallSearchButton.Name = "scryfallSearchButton";
            this.scryfallSearchButton.Size = new System.Drawing.Size(112, 53);
            this.scryfallSearchButton.TabIndex = 5;
            this.scryfallSearchButton.Text = "Scryfall search";
            this.scryfallSearchButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.scryfallSearchButton.UseMnemonic = false;
            this.scryfallSearchButton.UseVisualStyleBackColor = true;
            this.scryfallSearchButton.Click += new System.EventHandler(this.scryfallSearchButton_Click);
            // 
            // locationManagementGroupBox
            // 
            this.locationManagementGroupBox.Controls.Add(this.menu_addLocationButton);
            this.locationManagementGroupBox.Controls.Add(this.menu_removeLocationButton);
            this.locationManagementGroupBox.Controls.Add(this.checkLocationsButton);
            this.locationManagementGroupBox.Location = new System.Drawing.Point(12, 269);
            this.locationManagementGroupBox.Name = "locationManagementGroupBox";
            this.locationManagementGroupBox.Size = new System.Drawing.Size(360, 87);
            this.locationManagementGroupBox.TabIndex = 9;
            this.locationManagementGroupBox.TabStop = false;
            this.locationManagementGroupBox.Text = "Location management";
            // 
            // exportOptionsManagement
            // 
            this.exportOptionsManagement.Controls.Add(this.csvImportButton);
            this.exportOptionsManagement.Controls.Add(this.csvExportButton);
            this.exportOptionsManagement.Controls.Add(this.defaultExportButton);
            this.exportOptionsManagement.Location = new System.Drawing.Point(12, 362);
            this.exportOptionsManagement.Name = "exportOptionsManagement";
            this.exportOptionsManagement.Size = new System.Drawing.Size(360, 87);
            this.exportOptionsManagement.TabIndex = 10;
            this.exportOptionsManagement.TabStop = false;
            this.exportOptionsManagement.Text = "Import/export options";
            // 
            // csvExportButton
            // 
            this.csvExportButton.Image = ((System.Drawing.Image)(resources.GetObject("csvExportButton.Image")));
            this.csvExportButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.csvExportButton.Location = new System.Drawing.Point(124, 19);
            this.csvExportButton.Name = "csvExportButton";
            this.csvExportButton.Size = new System.Drawing.Size(112, 53);
            this.csvExportButton.TabIndex = 10;
            this.csvExportButton.Text = "CSV export";
            this.csvExportButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.csvExportButton.UseMnemonic = false;
            this.csvExportButton.UseVisualStyleBackColor = true;
            // 
            // defaultExportButton
            // 
            this.defaultExportButton.Image = ((System.Drawing.Image)(resources.GetObject("defaultExportButton.Image")));
            this.defaultExportButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.defaultExportButton.Location = new System.Drawing.Point(6, 19);
            this.defaultExportButton.Name = "defaultExportButton";
            this.defaultExportButton.Size = new System.Drawing.Size(112, 53);
            this.defaultExportButton.TabIndex = 9;
            this.defaultExportButton.Text = "Default export";
            this.defaultExportButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.defaultExportButton.UseMnemonic = false;
            this.defaultExportButton.UseVisualStyleBackColor = true;
            // 
            // csvImportButton
            // 
            this.csvImportButton.Image = ((System.Drawing.Image)(resources.GetObject("csvImportButton.Image")));
            this.csvImportButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.csvImportButton.Location = new System.Drawing.Point(242, 19);
            this.csvImportButton.Name = "csvImportButton";
            this.csvImportButton.Size = new System.Drawing.Size(112, 53);
            this.csvImportButton.TabIndex = 11;
            this.csvImportButton.Text = "CSV import";
            this.csvImportButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.csvImportButton.UseMnemonic = false;
            this.csvImportButton.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.exportOptionsManagement);
            this.Controls.Add(this.locationManagementGroupBox);
            this.Controls.Add(this.cardManagementGroupBox);
            this.Controls.Add(this.main_mainLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 500);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MTG Storage v.0.0.0";
            this.cardManagementGroupBox.ResumeLayout(false);
            this.locationManagementGroupBox.ResumeLayout(false);
            this.exportOptionsManagement.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button csvImportButton;

        private System.Windows.Forms.Button defaultExportButton;
        private System.Windows.Forms.Button csvExportButton;

        private System.Windows.Forms.GroupBox cardManagementGroupBox;

        private System.Windows.Forms.Button addCardBulkButton;

        private System.Windows.Forms.Button scryfallSearchButton;

        private System.Windows.Forms.GroupBox locationManagementGroupBox;

        private System.Windows.Forms.GroupBox exportOptionsManagement;

        private System.Windows.Forms.Button checkLocationsButton;

        private System.Windows.Forms.Button searchCardButton;

        private System.Windows.Forms.Button menu_removeLocationButton;

        private System.Windows.Forms.Button menu_addLocationButton;

        private System.Windows.Forms.Button menu_removeCardButton;

        private System.Windows.Forms.Label main_mainLabel;

        private System.Windows.Forms.Button menu_addCardButton;

        #endregion
    }
}