using System.ComponentModel;

namespace MTGStorage
{
    partial class CardSearchWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardSearchWindow));
            this.titleLabel = new System.Windows.Forms.Label();
            this.cardLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.advancedButton = new System.Windows.Forms.Button();
            this.searchCombobox = new System.Windows.Forms.ComboBox();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.pageNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.shipButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pageNavigator)).BeginInit();
            this.pageNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 33.75F, System.Drawing.FontStyle.Bold);
            this.titleLabel.Location = new System.Drawing.Point(12, 42);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(805, 56);
            this.titleLabel.TabIndex = 6;
            this.titleLabel.Text = "Card search";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardLayoutPanel
            // 
            this.cardLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardLayoutPanel.Location = new System.Drawing.Point(12, 167);
            this.cardLayoutPanel.Name = "cardLayoutPanel";
            this.cardLayoutPanel.Size = new System.Drawing.Size(805, 605);
            this.cardLayoutPanel.TabIndex = 3;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 803);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(399, 27);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(910, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "Use \"Advanced\" to gain access to more detailed search options.";
            // 
            // searchButton
            // 
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.searchButton.Location = new System.Drawing.Point(609, 140);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(101, 21);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // advancedButton
            // 
            this.advancedButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.advancedButton.Location = new System.Drawing.Point(716, 142);
            this.advancedButton.Name = "advancedButton";
            this.advancedButton.Size = new System.Drawing.Size(101, 21);
            this.advancedButton.TabIndex = 2;
            this.advancedButton.Text = "Advanced";
            this.advancedButton.UseVisualStyleBackColor = true;
            this.advancedButton.Click += new System.EventHandler(this.advancedButton_Click);
            // 
            // searchCombobox
            // 
            this.searchCombobox.FormattingEnabled = true;
            this.searchCombobox.Location = new System.Drawing.Point(12, 141);
            this.searchCombobox.Name = "searchCombobox";
            this.searchCombobox.Size = new System.Drawing.Size(591, 21);
            this.searchCombobox.TabIndex = 0;
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Enabled = false;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 21);
            this.bindingNavigatorMoveFirstItem.Text = "Przenieś pierwszy";
            this.bindingNavigatorMoveFirstItem.Click += new System.EventHandler(this.bindingNavigatorMoveFirstItem_Click);
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Enabled = false;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 21);
            this.bindingNavigatorMovePreviousItem.Text = "Przenieś poprzedni";
            this.bindingNavigatorMovePreviousItem.Click += new System.EventHandler(this.bindingNavigatorMovePreviousItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 24);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Pozycja";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Enabled = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Bieżąca pozycja";
            this.bindingNavigatorPositionItem.Click += new System.EventHandler(this.bindingNavigatorPositionItem_TextChanged);
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(29, 21);
            this.bindingNavigatorCountItem.Text = "z {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Suma elementów";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 24);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Enabled = false;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 21);
            this.bindingNavigatorMoveNextItem.Text = "Przenieś następny";
            this.bindingNavigatorMoveNextItem.Click += new System.EventHandler(this.bindingNavigatorMoveNextItem_Click);
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Enabled = false;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 21);
            this.bindingNavigatorMoveLastItem.Text = "Przenieś ostatni";
            this.bindingNavigatorMoveLastItem.Click += new System.EventHandler(this.bindingNavigatorMoveLastItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 24);
            // 
            // pageNavigator
            // 
            this.pageNavigator.AddNewItem = null;
            this.pageNavigator.AutoSize = false;
            this.pageNavigator.CountItem = null;
            this.pageNavigator.DeleteItem = null;
            this.pageNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.pageNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.pageNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.bindingNavigatorMoveFirstItem, this.bindingNavigatorMovePreviousItem, this.toolStripSeparator1, this.bindingNavigatorPositionItem, this.bindingNavigatorCountItem, this.toolStripSeparator2, this.bindingNavigatorMoveNextItem, this.bindingNavigatorMoveLastItem, this.toolStripSeparator3 });
            this.pageNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.pageNavigator.Location = new System.Drawing.Point(12, 776);
            this.pageNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.pageNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.pageNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.pageNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.pageNavigator.Name = "pageNavigator";
            this.pageNavigator.PositionItem = null;
            this.pageNavigator.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pageNavigator.Size = new System.Drawing.Size(805, 24);
            this.pageNavigator.TabIndex = 3;
            this.pageNavigator.Text = "bindingNavigator1";
            // 
            // shipButton
            // 
            this.shipButton.Location = new System.Drawing.Point(418, 803);
            this.shipButton.Name = "shipButton";
            this.shipButton.Size = new System.Drawing.Size(399, 27);
            this.shipButton.TabIndex = 18;
            this.shipButton.Text = "Ship selected cards";
            this.shipButton.UseVisualStyleBackColor = true;
            this.shipButton.Click += new System.EventHandler(this.shipButton_Click);
            // 
            // CardSearchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(829, 841);
            this.Controls.Add(this.shipButton);
            this.Controls.Add(this.pageNavigator);
            this.Controls.Add(this.searchCombobox);
            this.Controls.Add(this.advancedButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.cardLayoutPanel);
            this.Controls.Add(this.titleLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(15, 15);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(845, 880);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(845, 880);
            this.Name = "CardSearchWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pageNavigator)).EndInit();
            this.pageNavigator.ResumeLayout(false);
            this.pageNavigator.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button shipButton;

        private System.Windows.Forms.BindingNavigator pageNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

        private System.Windows.Forms.ComboBox searchCombobox;

        private System.Windows.Forms.Button advancedButton;

        private System.Windows.Forms.Button searchButton;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Button cancelButton;

        private System.Windows.Forms.FlowLayoutPanel cardLayoutPanel;

        private System.Windows.Forms.Label titleLabel;

        #endregion
    }
}