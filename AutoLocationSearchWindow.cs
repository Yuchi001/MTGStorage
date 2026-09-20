using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTGStorage.Database.DataObjects;
using MTGStorage.Database.Endpoints;

namespace MTGStorage
{
    public partial class AutoLocationSearchWindow : Form
    {
        private Location _foundLocation;
        private int _addedCards;
        private int _countToAdd;
        private Card _cardToSave;
        
        public AutoLocationSearchWindow()
        {
            InitializeComponent();
        }

        public async Task FindLocation(Card cardToSave, int countToAdd)
        {
            _countToAdd = countToAdd;
            var result = await LocationEndpoints.FindLocation(cardToSave);
            _foundLocation = result.location;
            if (_foundLocation == null)
            {
                MessageBox.Show(
                   "Could not find location matching this card/s criteria. Please create new location.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                Close();
                return;
            }
            
            _addedCards = Math.Min(result.maxCount, countToAdd);
            _cardToSave = cardToSave;

            var alikeCards = await LocationEndpoints.GetCardInLocation(_foundLocation.ID, _cardToSave.Name);
            locationCodeLabel.Text = _foundLocation.Code;
            countMessageLabel.Text = $"Put x{_addedCards} {_cardToSave.Name} into listed location:";
            if (alikeCards == null || alikeCards.Count < 1) return;
            countMessageLabel.Text += $"\nThere already are {alikeCards.Count} copy/copies.";
        }

        private async void confirmButton_Click(object sender, EventArgs e)
        {
            await CardEndpoints.AddCard(_cardToSave, _foundLocation.ID, _addedCards);

            var processFinished = _addedCards == _countToAdd;

            if (processFinished)
            {
                Close();
                return;
            } 
            
            await FindLocation(_cardToSave, _countToAdd - _addedCards);
        }
    }
}