using System.ComponentModel;

namespace MTGStorage
{
    partial class AddCardWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCardWindow));
            this.addCard_mainLabel = new System.Windows.Forms.Label();
            this.addCard_confirmButton = new System.Windows.Forms.Button();
            this.addCard_cardNameReqLabel = new System.Windows.Forms.Label();
            this.addCard_pickPrintComboBox = new System.Windows.Forms.ComboBox();
            this.addCard_cardPictureBox = new System.Windows.Forms.PictureBox();
            this.addCard_setPrintCheckBox = new System.Windows.Forms.CheckBox();
            this.addCard_cardNameComboBox = new MTGStorage.CustomUI.NoAutoSelectComboBox();
            this.addCard_priceLabel = new System.Windows.Forms.Label();
            this.addCard_rankLabel = new System.Windows.Forms.Label();
            this.add_cardStockDataCountLabel = new System.Windows.Forms.Label();
            this.addCard_locationCountDataLabel = new System.Windows.Forms.Label();
            this.countToAddLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.countToAddTextBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.addCard_cardPictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countToAddTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // addCard_mainLabel
            // 
            this.addCard_mainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold);
            this.addCard_mainLabel.Location = new System.Drawing.Point(12, 42);
            this.addCard_mainLabel.Name = "addCard_mainLabel";
            this.addCard_mainLabel.Size = new System.Drawing.Size(360, 56);
            this.addCard_mainLabel.TabIndex = 2;
            this.addCard_mainLabel.Text = "Add Card";
            this.addCard_mainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addCard_confirmButton
            // 
            this.addCard_confirmButton.Enabled = false;
            this.addCard_confirmButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.addCard_confirmButton.Location = new System.Drawing.Point(187, 422);
            this.addCard_confirmButton.Name = "addCard_confirmButton";
            this.addCard_confirmButton.Size = new System.Drawing.Size(185, 30);
            this.addCard_confirmButton.TabIndex = 5;
            this.addCard_confirmButton.Text = "Search location";
            this.addCard_confirmButton.UseVisualStyleBackColor = true;
            this.addCard_confirmButton.Click += new System.EventHandler(this.addCard_confirmButton_Click);
            // 
            // addCard_cardNameReqLabel
            // 
            this.addCard_cardNameReqLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.addCard_cardNameReqLabel.Location = new System.Drawing.Point(12, 129);
            this.addCard_cardNameReqLabel.Name = "addCard_cardNameReqLabel";
            this.addCard_cardNameReqLabel.Size = new System.Drawing.Size(360, 18);
            this.addCard_cardNameReqLabel.TabIndex = 4;
            this.addCard_cardNameReqLabel.Text = "Enter card name:";
            // 
            // addCard_pickPrintComboBox
            // 
            this.addCard_pickPrintComboBox.Enabled = false;
            this.addCard_pickPrintComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.addCard_pickPrintComboBox.FormattingEnabled = true;
            this.addCard_pickPrintComboBox.Location = new System.Drawing.Point(190, 198);
            this.addCard_pickPrintComboBox.Name = "addCard_pickPrintComboBox";
            this.addCard_pickPrintComboBox.Size = new System.Drawing.Size(182, 21);
            this.addCard_pickPrintComboBox.TabIndex = 2;
            this.addCard_pickPrintComboBox.SelectedIndexChanged += new System.EventHandler(this.addCard_cardPrintComboBoxIndexChanged);
            // 
            // addCard_cardPictureBox
            // 
            this.addCard_cardPictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.addCard_cardPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.addCard_cardPictureBox.Location = new System.Drawing.Point(12, 176);
            this.addCard_cardPictureBox.Name = "addCard_cardPictureBox";
            this.addCard_cardPictureBox.Size = new System.Drawing.Size(171, 240);
            this.addCard_cardPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.addCard_cardPictureBox.TabIndex = 7;
            this.addCard_cardPictureBox.TabStop = false;
            // 
            // addCard_setPrintCheckBox
            // 
            this.addCard_setPrintCheckBox.Enabled = false;
            this.addCard_setPrintCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.addCard_setPrintCheckBox.Location = new System.Drawing.Point(190, 176);
            this.addCard_setPrintCheckBox.Name = "addCard_setPrintCheckBox";
            this.addCard_setPrintCheckBox.Size = new System.Drawing.Size(182, 21);
            this.addCard_setPrintCheckBox.TabIndex = 1;
            this.addCard_setPrintCheckBox.Text = "specify set";
            this.addCard_setPrintCheckBox.UseVisualStyleBackColor = true;
            this.addCard_setPrintCheckBox.CheckedChanged += new System.EventHandler(this.addCard_setPrintCheckBox_CheckedChanged);
            // 
            // addCard_cardNameComboBox
            // 
            this.addCard_cardNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.addCard_cardNameComboBox.FormattingEnabled = true;
            this.addCard_cardNameComboBox.Location = new System.Drawing.Point(12, 149);
            this.addCard_cardNameComboBox.Name = "addCard_cardNameComboBox";
            this.addCard_cardNameComboBox.Size = new System.Drawing.Size(360, 21);
            this.addCard_cardNameComboBox.TabIndex = 0;
            this.addCard_cardNameComboBox.SelectedIndexChanged += new System.EventHandler(this.addCard_cardNameComboBox_SelectedIndexChanged);
            this.addCard_cardNameComboBox.TextChanged += new System.EventHandler(this.addCard_cardNameComboBox_TextChanged);
            // 
            // addCard_priceLabel
            // 
            this.addCard_priceLabel.Location = new System.Drawing.Point(6, 16);
            this.addCard_priceLabel.Name = "addCard_priceLabel";
            this.addCard_priceLabel.Size = new System.Drawing.Size(170, 16);
            this.addCard_priceLabel.TabIndex = 10;
            this.addCard_priceLabel.Text = "Price: 0€";
            // 
            // addCard_rankLabel
            // 
            this.addCard_rankLabel.Location = new System.Drawing.Point(6, 32);
            this.addCard_rankLabel.Name = "addCard_rankLabel";
            this.addCard_rankLabel.Size = new System.Drawing.Size(170, 14);
            this.addCard_rankLabel.TabIndex = 11;
            this.addCard_rankLabel.Text = "EDHC Rank: 0";
            // 
            // add_cardStockDataCountLabel
            // 
            this.add_cardStockDataCountLabel.Location = new System.Drawing.Point(6, 16);
            this.add_cardStockDataCountLabel.Name = "add_cardStockDataCountLabel";
            this.add_cardStockDataCountLabel.Size = new System.Drawing.Size(170, 16);
            this.add_cardStockDataCountLabel.TabIndex = 14;
            this.add_cardStockDataCountLabel.Text = "In stock: 0";
            // 
            // addCard_locationCountDataLabel
            // 
            this.addCard_locationCountDataLabel.Location = new System.Drawing.Point(6, 32);
            this.addCard_locationCountDataLabel.Name = "addCard_locationCountDataLabel";
            this.addCard_locationCountDataLabel.Size = new System.Drawing.Size(170, 59);
            this.addCard_locationCountDataLabel.TabIndex = 15;
            // 
            // countToAddLabel
            // 
            this.countToAddLabel.Location = new System.Drawing.Point(187, 222);
            this.countToAddLabel.Name = "countToAddLabel";
            this.countToAddLabel.Size = new System.Drawing.Size(185, 17);
            this.countToAddLabel.TabIndex = 17;
            this.countToAddLabel.Text = "Count to add";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.add_cardStockDataCountLabel);
            this.groupBox1.Controls.Add(this.addCard_locationCountDataLabel);
            this.groupBox1.Location = new System.Drawing.Point(190, 322);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 94);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stock data";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.addCard_priceLabel);
            this.groupBox2.Controls.Add(this.addCard_rankLabel);
            this.groupBox2.Location = new System.Drawing.Point(189, 267);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(183, 52);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scryfall data";
            // 
            // cancelButton
            // 
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(12, 422);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(169, 30);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // countToAddTextBox
            // 
            this.countToAddTextBox.Location = new System.Drawing.Point(190, 241);
            this.countToAddTextBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.countToAddTextBox.Name = "countToAddTextBox";
            this.countToAddTextBox.Size = new System.Drawing.Size(182, 20);
            this.countToAddTextBox.TabIndex = 3;
            this.countToAddTextBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // AddCardWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.countToAddTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.countToAddLabel);
            this.Controls.Add(this.addCard_cardNameComboBox);
            this.Controls.Add(this.addCard_setPrintCheckBox);
            this.Controls.Add(this.addCard_cardPictureBox);
            this.Controls.Add(this.addCard_pickPrintComboBox);
            this.Controls.Add(this.addCard_cardNameReqLabel);
            this.Controls.Add(this.addCard_confirmButton);
            this.Controls.Add(this.addCard_mainLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 500);
            this.Name = "AddCardWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MTG Storage - add card";
            ((System.ComponentModel.ISupportInitialize)(this.addCard_cardPictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.countToAddTextBox)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.NumericUpDown countToAddTextBox;

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;

        private System.Windows.Forms.Label countToAddLabel;

        private System.Windows.Forms.Label add_cardStockDataCountLabel;

        private System.Windows.Forms.Label addCard_priceLabel;
        private System.Windows.Forms.Label addCard_rankLabel;
        private System.Windows.Forms.Label addCard_locationCountDataLabel;

        private MTGStorage.CustomUI.NoAutoSelectComboBox addCard_cardNameComboBox;

        private System.Windows.Forms.CheckBox addCard_setPrintCheckBox;

        private System.Windows.Forms.PictureBox addCard_cardPictureBox;

        private System.Windows.Forms.ComboBox addCard_pickPrintComboBox;

        private System.Windows.Forms.Label addCard_cardNameReqLabel;

        private System.Windows.Forms.Button addCard_confirmButton;

        private System.Windows.Forms.Label addCard_mainLabel;

        #endregion
    }
}