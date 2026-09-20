using System.ComponentModel;

namespace MTGStorage
{
    partial class AdvancedSearchOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedSearchOptions));
            this.titleLabel = new System.Windows.Forms.Label();
            this.keywordsTextbox = new System.Windows.Forms.TextBox();
            this.typesTextbox = new System.Windows.Forms.TextBox();
            this.redCheckbox = new System.Windows.Forms.CheckBox();
            this.blueCheckbox = new System.Windows.Forms.CheckBox();
            this.greenCheckbox = new System.Windows.Forms.CheckBox();
            this.blackCheckbox = new System.Windows.Forms.CheckBox();
            this.whiteCheckbox = new System.Windows.Forms.CheckBox();
            this.colorlessCheckbox = new System.Windows.Forms.CheckBox();
            this.colorSearchTypeCombobox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.keywordsGroup = new System.Windows.Forms.GroupBox();
            this.typesGroup = new System.Windows.Forms.GroupBox();
            this.colorGroup = new System.Windows.Forms.GroupBox();
            this.identityGroup = new System.Windows.Forms.GroupBox();
            this.whiteIdentityCheckbox = new System.Windows.Forms.CheckBox();
            this.greenIdentityCheckbox = new System.Windows.Forms.CheckBox();
            this.blueIdentityCheckbox = new System.Windows.Forms.CheckBox();
            this.colorlessIdentityCheckbox = new System.Windows.Forms.CheckBox();
            this.blackIdentityCheckbox = new System.Windows.Forms.CheckBox();
            this.redIdentityCheckbox = new System.Windows.Forms.CheckBox();
            this.cmcTextbox = new System.Windows.Forms.NumericUpDown();
            this.cmcCheckbox = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.realcmcGroup = new System.Windows.Forms.GroupBox();
            this.realcmcTextbox = new System.Windows.Forms.NumericUpDown();
            this.realcmcCheckbox = new System.Windows.Forms.CheckBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.keywordsGroup.SuspendLayout();
            this.typesGroup.SuspendLayout();
            this.colorGroup.SuspendLayout();
            this.identityGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmcTextbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.realcmcGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.realcmcTextbox)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 33.75F, System.Drawing.FontStyle.Bold);
            this.titleLabel.Location = new System.Drawing.Point(12, 42);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(360, 56);
            this.titleLabel.TabIndex = 7;
            this.titleLabel.Text = "Advanced";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // keywordsTextbox
            // 
            this.keywordsTextbox.Location = new System.Drawing.Point(6, 19);
            this.keywordsTextbox.Name = "keywordsTextbox";
            this.keywordsTextbox.Size = new System.Drawing.Size(348, 20);
            this.keywordsTextbox.TabIndex = 9;
            // 
            // typesTextbox
            // 
            this.typesTextbox.Location = new System.Drawing.Point(5, 19);
            this.typesTextbox.Name = "typesTextbox";
            this.typesTextbox.Size = new System.Drawing.Size(348, 20);
            this.typesTextbox.TabIndex = 13;
            // 
            // redCheckbox
            // 
            this.redCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("redCheckbox.Image")));
            this.redCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.redCheckbox.Location = new System.Drawing.Point(174, 19);
            this.redCheckbox.Name = "redCheckbox";
            this.redCheckbox.Size = new System.Drawing.Size(50, 24);
            this.redCheckbox.TabIndex = 15;
            this.redCheckbox.Text = "R";
            this.redCheckbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.redCheckbox.UseVisualStyleBackColor = true;
            // 
            // blueCheckbox
            // 
            this.blueCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("blueCheckbox.Image")));
            this.blueCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.blueCheckbox.Location = new System.Drawing.Point(62, 19);
            this.blueCheckbox.Name = "blueCheckbox";
            this.blueCheckbox.Size = new System.Drawing.Size(50, 24);
            this.blueCheckbox.TabIndex = 16;
            this.blueCheckbox.Text = "U";
            this.blueCheckbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.blueCheckbox.UseVisualStyleBackColor = true;
            // 
            // greenCheckbox
            // 
            this.greenCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("greenCheckbox.Image")));
            this.greenCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.greenCheckbox.Location = new System.Drawing.Point(230, 19);
            this.greenCheckbox.Name = "greenCheckbox";
            this.greenCheckbox.Size = new System.Drawing.Size(50, 24);
            this.greenCheckbox.TabIndex = 17;
            this.greenCheckbox.Text = "G";
            this.greenCheckbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.greenCheckbox.UseVisualStyleBackColor = true;
            // 
            // blackCheckbox
            // 
            this.blackCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("blackCheckbox.Image")));
            this.blackCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.blackCheckbox.Location = new System.Drawing.Point(118, 19);
            this.blackCheckbox.Name = "blackCheckbox";
            this.blackCheckbox.Size = new System.Drawing.Size(50, 24);
            this.blackCheckbox.TabIndex = 18;
            this.blackCheckbox.Text = "B";
            this.blackCheckbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.blackCheckbox.UseVisualStyleBackColor = true;
            // 
            // whiteCheckbox
            // 
            this.whiteCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("whiteCheckbox.Image")));
            this.whiteCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.whiteCheckbox.Location = new System.Drawing.Point(6, 19);
            this.whiteCheckbox.Name = "whiteCheckbox";
            this.whiteCheckbox.Size = new System.Drawing.Size(50, 24);
            this.whiteCheckbox.TabIndex = 19;
            this.whiteCheckbox.Text = "W";
            this.whiteCheckbox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.whiteCheckbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.whiteCheckbox.UseVisualStyleBackColor = true;
            // 
            // colorlessCheckbox
            // 
            this.colorlessCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("colorlessCheckbox.Image")));
            this.colorlessCheckbox.Location = new System.Drawing.Point(286, 19);
            this.colorlessCheckbox.Name = "colorlessCheckbox";
            this.colorlessCheckbox.Size = new System.Drawing.Size(50, 24);
            this.colorlessCheckbox.TabIndex = 20;
            this.colorlessCheckbox.Text = "C";
            this.colorlessCheckbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.colorlessCheckbox.UseVisualStyleBackColor = true;
            // 
            // colorSearchTypeCombobox
            // 
            this.colorSearchTypeCombobox.FormattingEnabled = true;
            this.colorSearchTypeCombobox.Location = new System.Drawing.Point(64, 49);
            this.colorSearchTypeCombobox.Name = "colorSearchTypeCombobox";
            this.colorSearchTypeCombobox.Size = new System.Drawing.Size(290, 21);
            this.colorSearchTypeCombobox.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 18);
            this.label1.TabIndex = 22;
            this.label1.Text = "Filter type:";
            // 
            // keywordsGroup
            // 
            this.keywordsGroup.Controls.Add(this.keywordsTextbox);
            this.keywordsGroup.Location = new System.Drawing.Point(12, 115);
            this.keywordsGroup.Name = "keywordsGroup";
            this.keywordsGroup.Size = new System.Drawing.Size(360, 48);
            this.keywordsGroup.TabIndex = 31;
            this.keywordsGroup.TabStop = false;
            this.keywordsGroup.Text = "Keywords";
            // 
            // typesGroup
            // 
            this.typesGroup.Controls.Add(this.typesTextbox);
            this.typesGroup.Location = new System.Drawing.Point(13, 169);
            this.typesGroup.Name = "typesGroup";
            this.typesGroup.Size = new System.Drawing.Size(359, 47);
            this.typesGroup.TabIndex = 32;
            this.typesGroup.TabStop = false;
            this.typesGroup.Text = "Types";
            // 
            // colorGroup
            // 
            this.colorGroup.Controls.Add(this.whiteCheckbox);
            this.colorGroup.Controls.Add(this.blueCheckbox);
            this.colorGroup.Controls.Add(this.blackCheckbox);
            this.colorGroup.Controls.Add(this.redCheckbox);
            this.colorGroup.Controls.Add(this.greenCheckbox);
            this.colorGroup.Controls.Add(this.colorlessCheckbox);
            this.colorGroup.Controls.Add(this.colorSearchTypeCombobox);
            this.colorGroup.Controls.Add(this.label1);
            this.colorGroup.Location = new System.Drawing.Point(13, 222);
            this.colorGroup.Name = "colorGroup";
            this.colorGroup.Size = new System.Drawing.Size(360, 81);
            this.colorGroup.TabIndex = 33;
            this.colorGroup.TabStop = false;
            this.colorGroup.Text = "Colors";
            // 
            // identityGroup
            // 
            this.identityGroup.Controls.Add(this.whiteIdentityCheckbox);
            this.identityGroup.Controls.Add(this.greenIdentityCheckbox);
            this.identityGroup.Controls.Add(this.blueIdentityCheckbox);
            this.identityGroup.Controls.Add(this.colorlessIdentityCheckbox);
            this.identityGroup.Controls.Add(this.blackIdentityCheckbox);
            this.identityGroup.Controls.Add(this.redIdentityCheckbox);
            this.identityGroup.Location = new System.Drawing.Point(13, 309);
            this.identityGroup.Name = "identityGroup";
            this.identityGroup.Size = new System.Drawing.Size(360, 47);
            this.identityGroup.TabIndex = 34;
            this.identityGroup.TabStop = false;
            this.identityGroup.Text = "Color identity";
            // 
            // whiteIdentityCheckbox
            // 
            this.whiteIdentityCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("whiteIdentityCheckbox.Image")));
            this.whiteIdentityCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.whiteIdentityCheckbox.Location = new System.Drawing.Point(6, 17);
            this.whiteIdentityCheckbox.Name = "whiteIdentityCheckbox";
            this.whiteIdentityCheckbox.Size = new System.Drawing.Size(50, 24);
            this.whiteIdentityCheckbox.TabIndex = 27;
            this.whiteIdentityCheckbox.Text = "W";
            this.whiteIdentityCheckbox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.whiteIdentityCheckbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.whiteIdentityCheckbox.UseVisualStyleBackColor = true;
            // 
            // greenIdentityCheckbox
            // 
            this.greenIdentityCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("greenIdentityCheckbox.Image")));
            this.greenIdentityCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.greenIdentityCheckbox.Location = new System.Drawing.Point(230, 17);
            this.greenIdentityCheckbox.Name = "greenIdentityCheckbox";
            this.greenIdentityCheckbox.Size = new System.Drawing.Size(50, 24);
            this.greenIdentityCheckbox.TabIndex = 25;
            this.greenIdentityCheckbox.Text = "G";
            this.greenIdentityCheckbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.greenIdentityCheckbox.UseVisualStyleBackColor = true;
            // 
            // blueIdentityCheckbox
            // 
            this.blueIdentityCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("blueIdentityCheckbox.Image")));
            this.blueIdentityCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.blueIdentityCheckbox.Location = new System.Drawing.Point(62, 17);
            this.blueIdentityCheckbox.Name = "blueIdentityCheckbox";
            this.blueIdentityCheckbox.Size = new System.Drawing.Size(50, 24);
            this.blueIdentityCheckbox.TabIndex = 24;
            this.blueIdentityCheckbox.Text = "U";
            this.blueIdentityCheckbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.blueIdentityCheckbox.UseVisualStyleBackColor = true;
            // 
            // colorlessIdentityCheckbox
            // 
            this.colorlessIdentityCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("colorlessIdentityCheckbox.Image")));
            this.colorlessIdentityCheckbox.Location = new System.Drawing.Point(286, 17);
            this.colorlessIdentityCheckbox.Name = "colorlessIdentityCheckbox";
            this.colorlessIdentityCheckbox.Size = new System.Drawing.Size(50, 24);
            this.colorlessIdentityCheckbox.TabIndex = 28;
            this.colorlessIdentityCheckbox.Text = "C";
            this.colorlessIdentityCheckbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.colorlessIdentityCheckbox.UseVisualStyleBackColor = true;
            // 
            // blackIdentityCheckbox
            // 
            this.blackIdentityCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("blackIdentityCheckbox.Image")));
            this.blackIdentityCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.blackIdentityCheckbox.Location = new System.Drawing.Point(118, 17);
            this.blackIdentityCheckbox.Name = "blackIdentityCheckbox";
            this.blackIdentityCheckbox.Size = new System.Drawing.Size(50, 24);
            this.blackIdentityCheckbox.TabIndex = 26;
            this.blackIdentityCheckbox.Text = "B";
            this.blackIdentityCheckbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.blackIdentityCheckbox.UseVisualStyleBackColor = true;
            // 
            // redIdentityCheckbox
            // 
            this.redIdentityCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("redIdentityCheckbox.Image")));
            this.redIdentityCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.redIdentityCheckbox.Location = new System.Drawing.Point(174, 17);
            this.redIdentityCheckbox.Name = "redIdentityCheckbox";
            this.redIdentityCheckbox.Size = new System.Drawing.Size(50, 24);
            this.redIdentityCheckbox.TabIndex = 23;
            this.redIdentityCheckbox.Text = "R";
            this.redIdentityCheckbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.redIdentityCheckbox.UseVisualStyleBackColor = true;
            // 
            // cmcTextbox
            // 
            this.cmcTextbox.Location = new System.Drawing.Point(80, 16);
            this.cmcTextbox.Name = "cmcTextbox";
            this.cmcTextbox.Size = new System.Drawing.Size(120, 20);
            this.cmcTextbox.TabIndex = 0;
            // 
            // cmcCheckbox
            // 
            this.cmcCheckbox.Location = new System.Drawing.Point(0, 25);
            this.cmcCheckbox.Name = "cmcCheckbox";
            this.cmcCheckbox.Size = new System.Drawing.Size(104, 24);
            this.cmcCheckbox.TabIndex = 0;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(80, 16);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(274, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(68, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // realcmcGroup
            // 
            this.realcmcGroup.Controls.Add(this.realcmcTextbox);
            this.realcmcGroup.Controls.Add(this.realcmcCheckbox);
            this.realcmcGroup.Location = new System.Drawing.Point(13, 362);
            this.realcmcGroup.Name = "realcmcGroup";
            this.realcmcGroup.Size = new System.Drawing.Size(360, 46);
            this.realcmcGroup.TabIndex = 35;
            this.realcmcGroup.TabStop = false;
            this.realcmcGroup.Text = "Converted mana cost";
            // 
            // realcmcTextbox
            // 
            this.realcmcTextbox.Enabled = false;
            this.realcmcTextbox.Location = new System.Drawing.Point(88, 19);
            this.realcmcTextbox.Name = "realcmcTextbox";
            this.realcmcTextbox.Size = new System.Drawing.Size(266, 20);
            this.realcmcTextbox.TabIndex = 1;
            // 
            // realcmcCheckbox
            // 
            this.realcmcCheckbox.Location = new System.Drawing.Point(6, 19);
            this.realcmcCheckbox.Name = "realcmcCheckbox";
            this.realcmcCheckbox.Size = new System.Drawing.Size(76, 21);
            this.realcmcCheckbox.TabIndex = 0;
            this.realcmcCheckbox.Text = "use cmc";
            this.realcmcCheckbox.UseVisualStyleBackColor = true;
            this.realcmcCheckbox.CheckedChanged += new System.EventHandler(this.realcmcCheckbox_CheckedChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 423);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(174, 26);
            this.cancelButton.TabIndex = 36;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(192, 423);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(180, 26);
            this.acceptButton.TabIndex = 37;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // AdvancedSearchOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.realcmcGroup);
            this.Controls.Add(this.identityGroup);
            this.Controls.Add(this.colorGroup);
            this.Controls.Add(this.typesGroup);
            this.Controls.Add(this.keywordsGroup);
            this.Controls.Add(this.titleLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(15, 15);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 500);
            this.Name = "AdvancedSearchOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.keywordsGroup.ResumeLayout(false);
            this.keywordsGroup.PerformLayout();
            this.typesGroup.ResumeLayout(false);
            this.typesGroup.PerformLayout();
            this.colorGroup.ResumeLayout(false);
            this.identityGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmcTextbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.realcmcGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.realcmcTextbox)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox realcmcGroup;
        private System.Windows.Forms.CheckBox realcmcCheckbox;
        private System.Windows.Forms.NumericUpDown realcmcTextbox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button acceptButton;

        private System.Windows.Forms.NumericUpDown cmcTextbox;
        private System.Windows.Forms.CheckBox cmcCheckbox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;

        private System.Windows.Forms.CheckBox whiteIdentityCheckbox;
        private System.Windows.Forms.CheckBox blueIdentityCheckbox;
        private System.Windows.Forms.CheckBox blackIdentityCheckbox;
        private System.Windows.Forms.CheckBox redIdentityCheckbox;
        private System.Windows.Forms.CheckBox greenIdentityCheckbox;
        private System.Windows.Forms.CheckBox colorlessIdentityCheckbox;

        private System.Windows.Forms.GroupBox identityGroup;

        private System.Windows.Forms.GroupBox keywordsGroup;
        private System.Windows.Forms.GroupBox typesGroup;
        private System.Windows.Forms.GroupBox colorGroup;

        private System.Windows.Forms.ComboBox colorSearchTypeCombobox;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.TextBox keywordsTextbox;
        private System.Windows.Forms.TextBox typesTextbox;
        private System.Windows.Forms.CheckBox redCheckbox;
        private System.Windows.Forms.CheckBox blueCheckbox;
        private System.Windows.Forms.CheckBox greenCheckbox;
        private System.Windows.Forms.CheckBox blackCheckbox;
        private System.Windows.Forms.CheckBox whiteCheckbox;
        private System.Windows.Forms.CheckBox colorlessCheckbox;

        private System.Windows.Forms.Label titleLabel;

        #endregion
    }
}