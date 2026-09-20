using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MTGStorage.Database.DataObjects;

namespace MTGStorage
{
    public partial class RelocateCardsWindow : Form
    {
        public Location SelectedLocation => pick.Checked ? (Location)locations.SelectedItem : null;

        public RelocateCardsWindow()
        {
            InitializeComponent();
        }

        public RelocateCardsWindow(List<Location> destinations, long count) : this()
        {
            label.Text = $"Move {count} cards using automatic location matching.\nCheck 'Pick location' to choose a matching destination.";
            pick.Enabled = destinations.Count > 0;
            locations.DataSource = destinations;
        }

        private void Locations_Format(object sender, ListControlConvertEventArgs e)
        {
            var location = e.ListItem as Location;
            if (location == null) return;
            e.Value = location.Code + " - " + (location.Capacity - location.Cards.Sum(c => (long)c.Count)) + " free";
        }

        private void Pick_CheckedChanged(object sender, EventArgs e)
        {
            locations.Enabled = pick.Checked;
            if (!pick.Checked && locations.Items.Count > 0) locations.SelectedIndex = 0;
        }
    }
}
