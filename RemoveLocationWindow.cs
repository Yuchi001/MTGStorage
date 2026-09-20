using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTGStorage.Database.DataObjects;
using MTGStorage.Database.Endpoints;

namespace MTGStorage
{
    public partial class RemoveLocationWindow : Form
    {
        public RemoveLocationWindow()
        {
            InitializeComponent();
        }

        public async Task Init()
        {
            var locations = await LocationEndpoints.GetLocations();
            foreach (var location in locations) locationComboBox.Items.Add(location);
            locationComboBox.DisplayMember = "Code";
        }

        private void locationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            confirmButton.Enabled = false;
            var list = locationComboBox.Items.Cast<Location>();
            if (list.All(loc => loc.Code != locationComboBox.Text)) return;

            confirmButton.Enabled = true;
        }

        private async void confirmButton_Click(object sender, EventArgs e)
        {
            var location = (Location)locationComboBox.SelectedItem;
            var cardsInLocation = await LocationEndpoints.GetCardsInLocation(location.ID);
            var additionalInfo = cardsInLocation.Any()
                ? $"\nThere are still {cardsInLocation.Sum(card => card.Count)} card(s) in this location."
                : "";            
            var result = MessageBox.Show(
                $"Are you sure you want to delete the location {location.Code}?{additionalInfo}",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result != DialogResult.Yes) return;

            await LocationEndpoints.RemoveLocation(location.ID);
            
            MessageBox.Show(
                $"Location {location.Code} has been deleted.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}