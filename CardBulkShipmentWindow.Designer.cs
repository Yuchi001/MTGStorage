using System.ComponentModel;

namespace MTGStorage
{
    partial class CardBulkShipmentWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardBulkShipmentWindow));
            this.mainLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.informationLabel = new System.Windows.Forms.Label();
            this.cardLocationPairTable = new System.Windows.Forms.DataGridView();
            this.cardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.set = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.completed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.image = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.cardLocationPairTable)).BeginInit();
            this.SuspendLayout();
            // 
            // mainLabel
            // 
            this.mainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold);
            this.mainLabel.Location = new System.Drawing.Point(12, 42);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(510, 56);
            this.mainLabel.TabIndex = 3;
            this.mainLabel.Text = "Bulk Shipment";
            this.mainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 426);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(251, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.Enabled = false;
            this.confirmButton.Location = new System.Drawing.Point(271, 426);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(251, 23);
            this.confirmButton.TabIndex = 2;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // informationLabel
            // 
            this.informationLabel.Location = new System.Drawing.Point(12, 122);
            this.informationLabel.Name = "informationLabel";
            this.informationLabel.Size = new System.Drawing.Size(360, 17);
            this.informationLabel.TabIndex = 5;
            this.informationLabel.Text = "Remove each listed card from given locations";
            // 
            // cardLocationPairTable
            // 
            this.cardLocationPairTable.AllowUserToAddRows = false;
            this.cardLocationPairTable.AllowUserToDeleteRows = false;
            this.cardLocationPairTable.AllowUserToResizeColumns = false;
            this.cardLocationPairTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.cardLocationPairTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.cardName, this.set, this.location, this.count, this.completed, this.image });
            this.cardLocationPairTable.Location = new System.Drawing.Point(12, 142);
            this.cardLocationPairTable.Name = "cardLocationPairTable";
            this.cardLocationPairTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.cardLocationPairTable.RowTemplate.Height = 23;
            this.cardLocationPairTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.cardLocationPairTable.Size = new System.Drawing.Size(510, 278);
            this.cardLocationPairTable.TabIndex = 0;
            this.cardLocationPairTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cardLocationPairTable_CellContentClick);
            this.cardLocationPairTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.cardLocationPairTable_CellEndEdit);
            this.cardLocationPairTable.CurrentCellDirtyStateChanged += new System.EventHandler(this.cardLocationPairTable_CurrentCellDirtyStateChanged);
            // 
            // cardName
            // 
            this.cardName.HeaderText = "Card Name";
            this.cardName.Name = "cardName";
            this.cardName.ReadOnly = true;
            // 
            // set
            // 
            this.set.HeaderText = "Set";
            this.set.Name = "set";
            this.set.ReadOnly = true;
            // 
            // location
            // 
            this.location.HeaderText = "Location";
            this.location.Name = "location";
            this.location.ReadOnly = true;
            this.location.Width = 75;
            // 
            // count
            // 
            this.count.HeaderText = "Count";
            this.count.Name = "count";
            this.count.ReadOnly = true;
            this.count.Width = 50;
            // 
            // completed
            // 
            this.completed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.completed.HeaderText = "Completed";
            this.completed.Name = "completed";
            // 
            // image
            // 
            this.image.HeaderText = "Image";
            this.image.Name = "image";
            this.image.ReadOnly = true;
            this.image.Text = "Check";
            this.image.UseColumnTextForButtonValue = true;
            this.image.Width = 50;
            // 
            // CardBulkShipmentWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 461);
            this.Controls.Add(this.cardLocationPairTable);
            this.Controls.Add(this.informationLabel);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.mainLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(550, 500);
            this.MinimumSize = new System.Drawing.Size(550, 500);
            this.Name = "CardBulkShipmentWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MTG Storage - bulk shipment";
            ((System.ComponentModel.ISupportInitialize)(this.cardLocationPairTable)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridViewButtonColumn image;

        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.DataGridViewCheckBoxColumn completed;

        private System.Windows.Forms.DataGridViewTextBoxColumn cardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn set;
        private System.Windows.Forms.DataGridViewTextBoxColumn location;

        private System.Windows.Forms.DataGridView cardLocationPairTable;

        private System.Windows.Forms.Label informationLabel;

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button confirmButton;

        private System.Windows.Forms.Label mainLabel;

        #endregion
    }
}