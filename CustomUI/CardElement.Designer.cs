using System.ComponentModel;

namespace MTGStorage.CustomUI
{
    partial class CardElement
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cardPictureBox = new System.Windows.Forms.PictureBox();
            this.cardToShipTextbox = new System.Windows.Forms.NumericUpDown();
            this.shipmentCountLabel = new System.Windows.Forms.Label();
            this.inStockLabel = new System.Windows.Forms.Label();
            this.priceLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cardPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardToShipTextbox)).BeginInit();
            this.SuspendLayout();
            // 
            // cardPictureBox
            // 
            this.cardPictureBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cardPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cardPictureBox.Location = new System.Drawing.Point(3, 24);
            this.cardPictureBox.Name = "cardPictureBox";
            this.cardPictureBox.Size = new System.Drawing.Size(184, 264);
            this.cardPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cardPictureBox.TabIndex = 0;
            this.cardPictureBox.TabStop = false;
            this.cardPictureBox.Click += new System.EventHandler(this.cardPictureBox_Click);
            // 
            // cardToShipTextbox
            // 
            this.cardToShipTextbox.Location = new System.Drawing.Point(146, 3);
            this.cardToShipTextbox.Name = "cardToShipTextbox";
            this.cardToShipTextbox.Size = new System.Drawing.Size(41, 20);
            this.cardToShipTextbox.TabIndex = 0;
            this.cardToShipTextbox.ValueChanged += new System.EventHandler(this.cardToShipTextbox_ValueChanged);
            // 
            // shipmentCountLabel
            // 
            this.shipmentCountLabel.Location = new System.Drawing.Point(95, 3);
            this.shipmentCountLabel.Name = "shipmentCountLabel";
            this.shipmentCountLabel.Size = new System.Drawing.Size(45, 20);
            this.shipmentCountLabel.TabIndex = 2;
            this.shipmentCountLabel.Text = "To ship:";
            // 
            // inStockLabel
            // 
            this.inStockLabel.Location = new System.Drawing.Point(3, 3);
            this.inStockLabel.Name = "inStockLabel";
            this.inStockLabel.Size = new System.Drawing.Size(86, 20);
            this.inStockLabel.TabIndex = 3;
            this.inStockLabel.Text = "In stock: 144";
            // 
            // priceLabel
            // 
            this.priceLabel.BackColor = System.Drawing.Color.Transparent;
            this.priceLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.priceLabel.Location = new System.Drawing.Point(3, 268);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(51, 20);
            this.priceLabel.TabIndex = 4;
            this.priceLabel.Text = "4.05e";
            // 
            // CardElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.inStockLabel);
            this.Controls.Add(this.shipmentCountLabel);
            this.Controls.Add(this.cardToShipTextbox);
            this.Controls.Add(this.cardPictureBox);
            this.MaximumSize = new System.Drawing.Size(194, 295);
            this.MinimumSize = new System.Drawing.Size(194, 295);
            this.Name = "CardElement";
            this.Size = new System.Drawing.Size(190, 291);
            ((System.ComponentModel.ISupportInitialize)(this.cardPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardToShipTextbox)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label priceLabel;

        private System.Windows.Forms.NumericUpDown cardToShipTextbox;
        private System.Windows.Forms.Label shipmentCountLabel;
        private System.Windows.Forms.Label inStockLabel;

        private System.Windows.Forms.PictureBox cardPictureBox;

        #endregion
    }
}