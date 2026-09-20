using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTGStorage.Database.DataObjects;
using MTGStorage.Database.Endpoints;

namespace MTGStorage
{
    public partial class RemoveCardWindow : Form
    {
        public RemoveCardWindow()
        {
            InitializeComponent();
        }

        public async Task Init()
        {
            var cards = await CardEndpoints.GetCards();
            selectCardComboBox.Items.Clear();
            foreach (var card in cards)
            {
                var item = $"{card.Name} | {card.PrintID}";
                if (selectCardComboBox.Items.Contains(item)) continue;
                
                selectCardComboBox.Items.Add(item);
                selectCardComboBox.DisplayMember = "Display";
            }
        }

        private async void selectCardComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            locationSelectComboBox.Enabled = removeCountTextBox.Enabled = false;
            locationSelectComboBox.Text = removeCountTextBox.Text = "";
            cardsInLocationLabel.Text = "Cards in location: 0";
            cardPictureBox.Image = null;
            removeCardButton.Enabled = false;
            
            if (!selectCardComboBox.Items.Contains(selectCardComboBox.Text)) return;

            locationSelectComboBox.Enabled = true;
            locationSelectComboBox.Items.Clear();

            var selectedOption = selectCardComboBox.Text.Split('|');
            var cardName = selectedOption[0].Trim();
            var setID = selectedOption.Length > 1 ? selectedOption[1].Trim() : "";

            var cards = await CardEndpoints.GetCards(cardName, setID, false);
            foreach (var card in cards)
            {
                var location = await LocationEndpoints.GetLocation(card.LocationID);
                locationSelectComboBox.Items.Add(location);
            }
            locationSelectComboBox.DisplayMember = "Code";
            
            cardPictureBox.LoadAsync(cards.First().ImageUrl);
        }

        private async void locationSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeCountTextBox.Enabled = false;
            removeCountTextBox.Text = "";
            cardsInLocationLabel.Text = "Cards in location: 0";
            removeCardButton.Enabled = false;
            
            var list = locationSelectComboBox.Items.Cast<Location>().ToList();
            if (list.All(loc => loc.Code != locationSelectComboBox.Text)) return;
            
            removeCountTextBox.Enabled = true;
            
            var selectedOption = selectCardComboBox.Text.Split('|');
            var cardName = selectedOption[0].Trim();
            var setID = selectedOption.Length > 1 ? selectedOption[1].Trim() : "";

            var current = list.First(loc => loc.Code == locationSelectComboBox.Text);
            var card = await LocationEndpoints.GetCardInLocation(current.ID, cardName, setID);
            
            cardsInLocationLabel.Text = $"Cards in location: {card.Count}";
            
            removeCardButton.Enabled = true;
        }

        private async void removeCardButton_Click(object sender, EventArgs e)
        {
            var cardsInLocation = int.Parse(cardsInLocationLabel.Text.Split(':')[1].Trim());
            if (!int.TryParse(removeCountTextBox.Text, out var quantityToRemove) || quantityToRemove <= 0 || quantityToRemove > cardsInLocation)
            {
                MessageBox.Show(
                    $"Invalid number of cards to remove!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            
            var selectedOption = selectCardComboBox.Text.Split('|');
            var cardName = selectedOption[0].Trim();
            var setID = selectedOption.Length > 1 ? selectedOption[1].Trim() : "";
            
            var list = locationSelectComboBox.Items.Cast<Location>().ToList();
            var location = list.First(loc => loc.Code == locationSelectComboBox.Text);

            var card = await LocationEndpoints.GetCardInLocation(location.ID, cardName, setID);
            await CardEndpoints.RemoveCard(card.ID, quantityToRemove);
            
            MessageBox.Show($"Removed {cardName} {setID} x{quantityToRemove} from location {location.Code}.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}