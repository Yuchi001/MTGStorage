namespace MTGStorage
{
    partial class LocationCardsWindow
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationCardsWindow));
            this.cardPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.title = new System.Windows.Forms.Label();
            this.summary = new System.Windows.Forms.Label();
            this.pageNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.first = new System.Windows.Forms.ToolStripButton();
            this.previous = new System.Windows.Forms.ToolStripButton();
            this.separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.page = new System.Windows.Forms.ToolStripTextBox();
            this.pages = new System.Windows.Forms.ToolStripLabel();
            this.separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.next = new System.Windows.Forms.ToolStripButton();
            this.last = new System.Windows.Forms.ToolStripButton();
            this.separator3 = new System.Windows.Forms.ToolStripSeparator();
            this.close = new System.Windows.Forms.Button();
            this.ship = new System.Windows.Forms.Button();
            this.relocateSelected = new System.Windows.Forms.Button();
            this.relocateAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pageNavigator)).BeginInit();
            this.pageNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardPanel
            // 
            this.cardPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cardPanel.Location = new System.Drawing.Point(12, 167);
            this.cardPanel.Name = "cardPanel";
            this.cardPanel.Size = new System.Drawing.Size(805, 599);
            this.cardPanel.TabIndex = 2;
            // 
            // title
            // 
            this.title.AutoEllipsis = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.title.Location = new System.Drawing.Point(12, 42);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(805, 56);
            this.title.TabIndex = 0;
            this.title.Text = "Location";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // summary
            // 
            this.summary.Location = new System.Drawing.Point(12, 120);
            this.summary.Name = "summary";
            this.summary.Size = new System.Drawing.Size(805, 40);
            this.summary.TabIndex = 1;
            this.summary.Text = "Select quantities on the cards to ship or relocate them.";
            // 
            // pageNavigator
            // 
            this.pageNavigator.AddNewItem = null;
            this.pageNavigator.AutoSize = false;
            this.pageNavigator.CountItem = null;
            this.pageNavigator.DeleteItem = null;
            this.pageNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.pageNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.pageNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.first, this.previous, this.separator1, this.page, this.pages, this.separator2, this.next, this.last, this.separator3 });
            this.pageNavigator.Location = new System.Drawing.Point(12, 769);
            this.pageNavigator.MoveFirstItem = null;
            this.pageNavigator.MoveLastItem = null;
            this.pageNavigator.MoveNextItem = null;
            this.pageNavigator.MovePreviousItem = null;
            this.pageNavigator.Name = "pageNavigator";
            this.pageNavigator.PositionItem = null;
            this.pageNavigator.Size = new System.Drawing.Size(805, 24);
            this.pageNavigator.TabIndex = 3;
            // 
            // first
            // 
            this.first.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.first.Enabled = false;
            this.first.Image = ((System.Drawing.Image)(resources.GetObject("first.Image")));
            this.first.Name = "first";
            this.first.RightToLeftAutoMirrorImage = true;
            this.first.Size = new System.Drawing.Size(23, 21);
            this.first.Text = "First";
            this.first.Click += new System.EventHandler(this.First_Click);
            // 
            // previous
            // 
            this.previous.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.previous.Enabled = false;
            this.previous.Image = ((System.Drawing.Image)(resources.GetObject("previous.Image")));
            this.previous.Name = "previous";
            this.previous.RightToLeftAutoMirrorImage = true;
            this.previous.Size = new System.Drawing.Size(23, 21);
            this.previous.Text = "Previous";
            this.previous.Click += new System.EventHandler(this.Previous_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(6, 24);
            // 
            // page
            // 
            this.page.AutoSize = false;
            this.page.Enabled = false;
            this.page.Name = "page";
            this.page.Size = new System.Drawing.Size(50, 23);
            this.page.Text = "1";
            this.page.Leave += new System.EventHandler(this.Page_Leave);
            this.page.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Page_KeyDown);
            // 
            // pages
            // 
            this.pages.Name = "pages";
            this.pages.Size = new System.Drawing.Size(27, 21);
            this.pages.Text = "of 1";
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            this.separator2.Size = new System.Drawing.Size(6, 24);
            // 
            // next
            // 
            this.next.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.next.Enabled = false;
            this.next.Image = ((System.Drawing.Image)(resources.GetObject("next.Image")));
            this.next.Name = "next";
            this.next.RightToLeftAutoMirrorImage = true;
            this.next.Size = new System.Drawing.Size(23, 21);
            this.next.Text = "Next";
            this.next.Click += new System.EventHandler(this.Next_Click);
            // 
            // last
            // 
            this.last.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.last.Enabled = false;
            this.last.Image = ((System.Drawing.Image)(resources.GetObject("last.Image")));
            this.last.Name = "last";
            this.last.RightToLeftAutoMirrorImage = true;
            this.last.Size = new System.Drawing.Size(23, 21);
            this.last.Text = "Last";
            this.last.Click += new System.EventHandler(this.Last_Click);
            // 
            // separator3
            // 
            this.separator3.Name = "separator3";
            this.separator3.Size = new System.Drawing.Size(6, 24);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(12, 829);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(399, 27);
            this.close.TabIndex = 4;
            this.close.Text = "Close";
            this.close.Click += new System.EventHandler(this.Close_Click);
            // 
            // ship
            // 
            this.ship.Location = new System.Drawing.Point(418, 829);
            this.ship.Name = "ship";
            this.ship.Size = new System.Drawing.Size(399, 27);
            this.ship.TabIndex = 5;
            this.ship.Text = "Ship selected cards";
            this.ship.Click += new System.EventHandler(this.Ship_Click);
            // 
            // relocateSelected
            // 
            this.relocateSelected.Location = new System.Drawing.Point(12, 796);
            this.relocateSelected.Name = "relocateSelected";
            this.relocateSelected.Size = new System.Drawing.Size(399, 27);
            this.relocateSelected.TabIndex = 6;
            this.relocateSelected.Text = "Relocate selected cards";
            this.relocateSelected.Click += new System.EventHandler(this.RelocateSelected_Click);
            // 
            // relocateAll
            // 
            this.relocateAll.Location = new System.Drawing.Point(418, 796);
            this.relocateAll.Name = "relocateAll";
            this.relocateAll.Size = new System.Drawing.Size(399, 27);
            this.relocateAll.TabIndex = 7;
            this.relocateAll.Text = "Relocate all cards";
            this.relocateAll.Click += new System.EventHandler(this.RelocateAll_Click);
            // 
            // LocationCardsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 868);
            this.Controls.Add(this.title);
            this.Controls.Add(this.summary);
            this.Controls.Add(this.cardPanel);
            this.Controls.Add(this.pageNavigator);
            this.Controls.Add(this.close);
            this.Controls.Add(this.ship);
            this.Controls.Add(this.relocateSelected);
            this.Controls.Add(this.relocateAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LocationCardsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MTG Storage - location cards";
            ((System.ComponentModel.ISupportInitialize)(this.pageNavigator)).EndInit();
            this.pageNavigator.ResumeLayout(false);
            this.pageNavigator.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.FlowLayoutPanel cardPanel;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label summary;
        private System.Windows.Forms.BindingNavigator pageNavigator;
        private System.Windows.Forms.ToolStripButton first;
        private System.Windows.Forms.ToolStripButton previous;
        private System.Windows.Forms.ToolStripSeparator separator1;
        private System.Windows.Forms.ToolStripTextBox page;
        private System.Windows.Forms.ToolStripLabel pages;
        private System.Windows.Forms.ToolStripSeparator separator2;
        private System.Windows.Forms.ToolStripButton next;
        private System.Windows.Forms.ToolStripButton last;
        private System.Windows.Forms.ToolStripSeparator separator3;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button ship;
        private System.Windows.Forms.Button relocateSelected;
        private System.Windows.Forms.Button relocateAll;
    }
}
