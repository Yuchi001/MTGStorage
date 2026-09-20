using System.ComponentModel;

namespace MTGStorage
{
    partial class AddLocationWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddLocationWindow));
            this.titleLabel = new System.Windows.Forms.Label();
            this.confirmButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.codeLabel = new System.Windows.Forms.Label();
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.capacityLabel = new System.Windows.Forms.Label();
            this.capacityTextBox = new System.Windows.Forms.NumericUpDown();
            this.minPriceLabel = new System.Windows.Forms.Label();
            this.minimumPriceTextBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.capacityTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumPriceTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 33.75F, System.Drawing.FontStyle.Bold);
            this.titleLabel.Location = new System.Drawing.Point(12, 42);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(360, 56);
            this.titleLabel.TabIndex = 4;
            this.titleLabel.Text = "Create location";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // confirmButton
            // 
            this.confirmButton.Enabled = false;
            this.confirmButton.Location = new System.Drawing.Point(197, 226);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(175, 23);
            this.confirmButton.TabIndex = 4;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 226);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(175, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // codeLabel
            // 
            this.codeLabel.Location = new System.Drawing.Point(12, 107);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(360, 19);
            this.codeLabel.TabIndex = 9;
            this.codeLabel.Text = "Location code";
            // 
            // codeTextBox
            // 
            this.codeTextBox.Location = new System.Drawing.Point(12, 129);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(360, 20);
            this.codeTextBox.TabIndex = 0;
            this.codeTextBox.TextChanged += new System.EventHandler(this.codeTextBox_TextChanged);
            // 
            // capacityLabel
            // 
            this.capacityLabel.Location = new System.Drawing.Point(12, 164);
            this.capacityLabel.Name = "capacityLabel";
            this.capacityLabel.Size = new System.Drawing.Size(175, 19);
            this.capacityLabel.TabIndex = 11;
            this.capacityLabel.Text = "Location capacity";
            // 
            // capacityTextBox
            // 
            this.capacityTextBox.Location = new System.Drawing.Point(12, 186);
            this.capacityTextBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.capacityTextBox.Name = "capacityTextBox";
            this.capacityTextBox.Size = new System.Drawing.Size(175, 20);
            this.capacityTextBox.TabIndex = 1;
            this.capacityTextBox.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // minPriceLabel
            // 
            this.minPriceLabel.Location = new System.Drawing.Point(193, 164);
            this.minPriceLabel.Name = "minPriceLabel";
            this.minPriceLabel.Size = new System.Drawing.Size(179, 19);
            this.minPriceLabel.TabIndex = 15;
            this.minPriceLabel.Text = "Minimum price filter";
            // 
            // minimumPriceTextBox
            // 
            this.minimumPriceTextBox.DecimalPlaces = 2;
            this.minimumPriceTextBox.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.minimumPriceTextBox.Location = new System.Drawing.Point(197, 186);
            this.minimumPriceTextBox.Name = "minimumPriceTextBox";
            this.minimumPriceTextBox.Size = new System.Drawing.Size(175, 20);
            this.minimumPriceTextBox.TabIndex = 2;
            // 
            // AddLocationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.minimumPriceTextBox);
            this.Controls.Add(this.minPriceLabel);
            this.Controls.Add(this.capacityTextBox);
            this.Controls.Add(this.capacityLabel);
            this.Controls.Add(this.codeTextBox);
            this.Controls.Add(this.codeLabel);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.titleLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "AddLocationWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MTG Storage - create location";
            ((System.ComponentModel.ISupportInitialize)(this.capacityTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumPriceTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label minPriceLabel;
        private System.Windows.Forms.NumericUpDown minimumPriceTextBox;

        private System.Windows.Forms.NumericUpDown capacityTextBox;

        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label codeLabel;
        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.Label capacityLabel;

        private System.Windows.Forms.Label titleLabel;

        #endregion
    }
}