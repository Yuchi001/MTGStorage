namespace MTGStorage
{
    partial class RelocateCardsWindow
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelocateCardsWindow));
            this.label = new System.Windows.Forms.Label();
            this.pick = new System.Windows.Forms.CheckBox();
            this.locations = new System.Windows.Forms.ComboBox();
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.label.Name = "label";
            this.pick.Name = "pick";
            this.locations.Name = "locations";
            this.confirm.Name = "confirm";
            this.cancel.Name = "cancel";
            this.label.Location = new System.Drawing.Point(12, 12);
            this.label.Size = new System.Drawing.Size(446, 42);
            this.label.Text = "Move cards using automatic location matching.\nCheck 'Pick location' to choose a matching destination.";
            this.pick.Location = new System.Drawing.Point(12, 65);
            this.pick.Size = new System.Drawing.Size(120, 24);
            this.pick.Text = "Pick location";
            this.locations.Location = new System.Drawing.Point(140, 65);
            this.locations.Size = new System.Drawing.Size(318, 24);
            this.confirm.Location = new System.Drawing.Point(240, 125);
            this.confirm.Size = new System.Drawing.Size(105, 30);
            this.confirm.Text = "Relocate";
            this.cancel.Location = new System.Drawing.Point(353, 125);
            this.cancel.Size = new System.Drawing.Size(105, 30);
            this.cancel.Text = "Cancel";
            this.locations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.locations.Enabled = false;
            this.locations.FormattingEnabled = true;
            this.confirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.locations.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.Locations_Format);
            this.pick.CheckedChanged += new System.EventHandler(this.Pick_CheckedChanged);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 175);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "RelocateCardsWindow";
            this.Text = "Relocate cards";
            this.AcceptButton = this.confirm;
            this.CancelButton = this.cancel;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Controls.Add(this.label);
            this.Controls.Add(this.pick);
            this.Controls.Add(this.locations);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.cancel);
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.CheckBox pick;
        private System.Windows.Forms.ComboBox locations;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancel;
    }
}
