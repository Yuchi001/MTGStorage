using System.ComponentModel;

namespace MTGStorage
{
    partial class RemoveCardWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoveCardWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.selectCardComboBox = new System.Windows.Forms.ComboBox();
            this.slectCardLabel = new System.Windows.Forms.Label();
            this.cardPictureBox = new System.Windows.Forms.PictureBox();
            this.removeCardButton = new System.Windows.Forms.Button();
            this.locationSelectComboBox = new System.Windows.Forms.ComboBox();
            this.locationSelectLabel = new System.Windows.Forms.Label();
            this.cardsInLocationLabel = new System.Windows.Forms.Label();
            this.removeCountTextBox = new System.Windows.Forms.NumericUpDown();
            this.countRemoveLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cardPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.removeCountTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold);
            this.titleLabel.Location = new System.Drawing.Point(12, 42);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(360, 56);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "Remove Card";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selectCardComboBox
            // 
            this.selectCardComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectCardComboBox.FormattingEnabled = true;
            this.selectCardComboBox.Location = new System.Drawing.Point(12, 150);
            this.selectCardComboBox.Name = "selectCardComboBox";
            this.selectCardComboBox.Size = new System.Drawing.Size(360, 21);
            this.selectCardComboBox.TabIndex = 0;
            this.selectCardComboBox.SelectedIndexChanged += new System.EventHandler(this.selectCardComboBox_SelectedIndexChanged);
            this.selectCardComboBox.TextChanged += new System.EventHandler(this.selectCardComboBox_SelectedIndexChanged);
            // 
            // slectCardLabel
            // 
            this.slectCardLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.slectCardLabel.Location = new System.Drawing.Point(12, 129);
            this.slectCardLabel.Name = "slectCardLabel";
            this.slectCardLabel.Size = new System.Drawing.Size(360, 18);
            this.slectCardLabel.TabIndex = 7;
            this.slectCardLabel.Text = "Select card you wish to remove:";
            // 
            // cardPictureBox
            // 
            this.cardPictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cardPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cardPictureBox.Location = new System.Drawing.Point(12, 177);
            this.cardPictureBox.Name = "cardPictureBox";
            this.cardPictureBox.Size = new System.Drawing.Size(171, 240);
            this.cardPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cardPictureBox.TabIndex = 9;
            this.cardPictureBox.TabStop = false;
            // 
            // removeCardButton
            // 
            this.removeCardButton.Enabled = false;
            this.removeCardButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.removeCardButton.Location = new System.Drawing.Point(189, 423);
            this.removeCardButton.Name = "removeCardButton";
            this.removeCardButton.Size = new System.Drawing.Size(183, 30);
            this.removeCardButton.TabIndex = 4;
            this.removeCardButton.Text = "Remove";
            this.removeCardButton.UseVisualStyleBackColor = true;
            this.removeCardButton.Click += new System.EventHandler(this.removeCardButton_Click);
            // 
            // locationSelectComboBox
            // 
            this.locationSelectComboBox.Enabled = false;
            this.locationSelectComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.locationSelectComboBox.FormattingEnabled = true;
            this.locationSelectComboBox.Location = new System.Drawing.Point(189, 198);
            this.locationSelectComboBox.Name = "locationSelectComboBox";
            this.locationSelectComboBox.Size = new System.Drawing.Size(183, 21);
            this.locationSelectComboBox.TabIndex = 1;
            this.locationSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.locationSelectComboBox_SelectedIndexChanged);
            this.locationSelectComboBox.TextChanged += new System.EventHandler(this.locationSelectComboBox_SelectedIndexChanged);
            // 
            // locationSelectLabel
            // 
            this.locationSelectLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.locationSelectLabel.Location = new System.Drawing.Point(189, 177);
            this.locationSelectLabel.Name = "locationSelectLabel";
            this.locationSelectLabel.Size = new System.Drawing.Size(183, 18);
            this.locationSelectLabel.TabIndex = 12;
            this.locationSelectLabel.Text = "Remove from location:";
            // 
            // cardsInLocationLabel
            // 
            this.cardsInLocationLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cardsInLocationLabel.Location = new System.Drawing.Point(189, 266);
            this.cardsInLocationLabel.Name = "cardsInLocationLabel";
            this.cardsInLocationLabel.Size = new System.Drawing.Size(183, 18);
            this.cardsInLocationLabel.TabIndex = 13;
            this.cardsInLocationLabel.Text = "Cards in location: 0\r\n";
            // 
            // removeCountTextBox
            // 
            this.removeCountTextBox.Enabled = false;
            this.removeCountTextBox.Location = new System.Drawing.Point(189, 243);
            this.removeCountTextBox.Name = "removeCountTextBox";
            this.removeCountTextBox.Size = new System.Drawing.Size(183, 20);
            this.removeCountTextBox.TabIndex = 2;
            // 
            // countRemoveLabel
            // 
            this.countRemoveLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.countRemoveLabel.Location = new System.Drawing.Point(189, 222);
            this.countRemoveLabel.Name = "countRemoveLabel";
            this.countRemoveLabel.Size = new System.Drawing.Size(183, 18);
            this.countRemoveLabel.TabIndex = 16;
            this.countRemoveLabel.Text = "Select quantity:";
            // 
            // cancelButton
            // 
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(12, 423);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(171, 30);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // RemoveCardWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.countRemoveLabel);
            this.Controls.Add(this.removeCountTextBox);
            this.Controls.Add(this.cardsInLocationLabel);
            this.Controls.Add(this.locationSelectLabel);
            this.Controls.Add(this.locationSelectComboBox);
            this.Controls.Add(this.removeCardButton);
            this.Controls.Add(this.cardPictureBox);
            this.Controls.Add(this.selectCardComboBox);
            this.Controls.Add(this.slectCardLabel);
            this.Controls.Add(this.titleLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(15, 15);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 500);
            this.Name = "RemoveCardWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MTG Storage - remove card";
            ((System.ComponentModel.ISupportInitialize)(this.cardPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.removeCountTextBox)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button cancelButton;

        private System.Windows.Forms.NumericUpDown removeCountTextBox;
        private System.Windows.Forms.Label countRemoveLabel;

        private System.Windows.Forms.Label cardsInLocationLabel;

        private System.Windows.Forms.Button removeCardButton;
        private System.Windows.Forms.ComboBox locationSelectComboBox;
        private System.Windows.Forms.Label locationSelectLabel;

        private System.Windows.Forms.PictureBox cardPictureBox;

        private System.Windows.Forms.ComboBox selectCardComboBox;
        private System.Windows.Forms.Label slectCardLabel;

        private System.Windows.Forms.Label titleLabel;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}