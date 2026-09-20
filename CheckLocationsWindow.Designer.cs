using System.ComponentModel;

namespace MTGStorage
{
    partial class CheckLocationsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckLocationsWindow));
            this.mainLabel = new System.Windows.Forms.Label();
            this.locationTable = new System.Windows.Forms.DataGridView();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capacity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cards = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.locationTable)).BeginInit();
            this.SuspendLayout();
            // 
            // mainLabel
            // 
            this.mainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold);
            this.mainLabel.Location = new System.Drawing.Point(12, 42);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(510, 56);
            this.mainLabel.TabIndex = 4;
            this.mainLabel.Text = "Locations";
            this.mainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // locationTable
            // 
            this.locationTable.AllowUserToAddRows = false;
            this.locationTable.AllowUserToDeleteRows = false;
            this.locationTable.AllowUserToResizeColumns = false;
            this.locationTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.locationTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.code, this.minPrice, this.capacity, this.cards, this.percentage });
            this.locationTable.Location = new System.Drawing.Point(12, 142);
            this.locationTable.Name = "locationTable";
            this.locationTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.locationTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.locationTable.Size = new System.Drawing.Size(510, 278);
            this.locationTable.TabIndex = 5;
            // 
            // code
            // 
            this.code.HeaderText = "Code";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            // 
            // minPrice
            // 
            this.minPrice.HeaderText = "Min. Price";
            this.minPrice.Name = "minPrice";
            this.minPrice.ReadOnly = true;
            // 
            // capacity
            // 
            this.capacity.HeaderText = "Capacity";
            this.capacity.Name = "capacity";
            this.capacity.ReadOnly = true;
            // 
            // cards
            // 
            this.cards.HeaderText = "Cards";
            this.cards.Name = "cards";
            this.cards.ReadOnly = true;
            // 
            // percentage
            // 
            this.percentage.HeaderText = "Percentage";
            this.percentage.Name = "percentage";
            this.percentage.ReadOnly = true;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(12, 426);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(510, 23);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // CheckLocationsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 461);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.locationTable);
            this.Controls.Add(this.mainLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 500);
            this.Name = "CheckLocationsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MTG Storage - check locations";
            ((System.ComponentModel.ISupportInitialize)(this.locationTable)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button closeButton;

        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn minPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn capacity;
        private System.Windows.Forms.DataGridViewTextBoxColumn cards;
        private System.Windows.Forms.DataGridViewTextBoxColumn percentage;

        private System.Windows.Forms.DataGridView locationTable;

        private System.Windows.Forms.Label mainLabel;

        #endregion
    }
}