using System.ComponentModel;

namespace MTGStorage
{
    partial class AutoLocationSearchWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoLocationSearchWindow));
            this.titleLabel = new System.Windows.Forms.Label();
            this.countMessageLabel = new System.Windows.Forms.Label();
            this.moreCardsButton = new System.Windows.Forms.Button();
            this.locationCodeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold);
            this.titleLabel.Location = new System.Drawing.Point(12, 42);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(360, 56);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "Putaway";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // countMessageLabel
            // 
            this.countMessageLabel.Location = new System.Drawing.Point(12, 98);
            this.countMessageLabel.Name = "countMessageLabel";
            this.countMessageLabel.Size = new System.Drawing.Size(360, 30);
            this.countMessageLabel.TabIndex = 4;
            this.countMessageLabel.Text = "Found location";
            this.countMessageLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // moreCardsButton
            // 
            this.moreCardsButton.Location = new System.Drawing.Point(12, 176);
            this.moreCardsButton.Name = "moreCardsButton";
            this.moreCardsButton.Size = new System.Drawing.Size(360, 23);
            this.moreCardsButton.TabIndex = 1;
            this.moreCardsButton.Text = "Continue";
            this.moreCardsButton.UseVisualStyleBackColor = true;
            this.moreCardsButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // locationCodeLabel
            // 
            this.locationCodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.locationCodeLabel.Location = new System.Drawing.Point(12, 128);
            this.locationCodeLabel.Name = "locationCodeLabel";
            this.locationCodeLabel.Size = new System.Drawing.Size(360, 45);
            this.locationCodeLabel.TabIndex = 7;
            this.locationCodeLabel.Text = "Found location";
            this.locationCodeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AutoLocationSearchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.locationCodeLabel);
            this.Controls.Add(this.moreCardsButton);
            this.Controls.Add(this.countMessageLabel);
            this.Controls.Add(this.titleLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "AutoLocationSearchWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MTG Storage - putaway";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label locationCodeLabel;

        private System.Windows.Forms.Label countMessageLabel;
        private System.Windows.Forms.Button moreCardsButton;

        private System.Windows.Forms.Label titleLabel;

        #endregion
    }
}