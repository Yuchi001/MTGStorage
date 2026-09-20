using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTGStorage.Database.DataObjects;
using MTGStorage.Database.Endpoints;

namespace MTGStorage
{
    public partial class AddCardWindow : Form
    {
        private ScryfallCard _currentCard;
        private Card _cardToSave;
        private CancellationTokenSource _autocompleteCancellation;
        private bool _updatingAutocomplete;
        
        public AddCardWindow()
        {
            InitializeComponent();
            countToAddTextBox.Text = "1";
        }

        private async void addCard_cardPrintComboBoxIndexChanged(object sender, EventArgs e)
        {
            if (_currentCard == null) return;
            
            var prints = await ScryfallEndpoints.GetCardPrints(_currentCard);
            var pickedOption = (string)addCard_pickPrintComboBox.SelectedItem;
            var setName = pickedOption.Substring(0, pickedOption.LastIndexOf("/", StringComparison.Ordinal)).Trim();
            var collectorNumber = pickedOption.Substring(pickedOption.LastIndexOf("/", StringComparison.Ordinal) + 1).Trim();
            var pickedPrint = prints.data.First(p => p.set_name == setName && p.collector_number == collectorNumber);
            
            addCard_cardPictureBox.LoadAsync(pickedPrint.image_uris.normal);

            _currentCard.prices.eur = pickedPrint.prices.eur;
            
            _cardToSave.ImageUrl = pickedPrint.image_uris.normal;
            _cardToSave.PrintID = pickedOption;
            _cardToSave.Price = decimal.Parse(_currentCard.prices.eur, CultureInfo.InvariantCulture);
            
            UpdateCardInfo();
        }

        private void addCard_setPrintCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_currentCard == null) return;
            
            _cardToSave.ImageUrl = _currentCard.image_uris.normal;
            _cardToSave.PrintID = "";
            
            addCard_cardPictureBox.LoadAsync(_currentCard.image_uris.normal);

            addCard_pickPrintComboBox.Enabled = addCard_setPrintCheckBox.Checked;
        }

        private async void addCard_cardNameComboBox_TextChanged(
            object sender,
            EventArgs e)
        {
            if (addCard_cardNameComboBox.Text.Length == 0)
            {
                _currentCard = null;
                _cardToSave = null;
                addCard_cardNameComboBox.DroppedDown = false;
                addCard_setPrintCheckBox.Enabled = addCard_setPrintCheckBox.Checked = false;
                addCard_pickPrintComboBox.Text = "";
                addCard_pickPrintComboBox.Enabled = false;
                addCard_cardPictureBox.Image = null;
                UpdateCardInfo();
                return;
            }
            
            if (_updatingAutocomplete)
                return;

            _autocompleteCancellation?.Cancel();
            _autocompleteCancellation = new CancellationTokenSource();

            var token = _autocompleteCancellation.Token;
            var query = addCard_cardNameComboBox.Text.Trim();

            if (query.Length <= 1)
            {
                _updatingAutocomplete = true;
                try
                {
                    //addCard_cardNameComboBox.DataSource = null;
                    addCard_cardNameComboBox.SelectedIndex = -1;
                }
                finally
                {
                    _updatingAutocomplete = false;
                }

                return;
            }

            try
            {
                await Task.Delay(300, token);

                var cardNames = await ScryfallEndpoints.GetCardNames(query);
                if (_cardToSave != null && cardNames.Contains(_cardToSave.Name)) return;

                if (token.IsCancellationRequested)
                    return;

                _updatingAutocomplete = true;

                try
                {
                    string currentText = addCard_cardNameComboBox.Text;
                    int cursorPosition = addCard_cardNameComboBox.SelectionStart;

                    addCard_cardNameComboBox.DataSource = null;
                    addCard_cardNameComboBox.DataSource = cardNames;
                    addCard_cardNameComboBox.Update();

                    addCard_cardNameComboBox.Text = currentText;
                    addCard_cardNameComboBox.SelectionStart =
                        Math.Min(cursorPosition, currentText.Length);
                }
                finally
                {
                    _updatingAutocomplete = false;
                }

                // Pokazujemy listę
                if (cardNames.Count > 0)
                {
                    addCard_cardNameComboBox.DroppedDown = true;
                }
            }
            catch (TaskCanceledException)
            {
                // Normalne - użytkownik wpisał kolejny znak
            }
        }

        private async void addCard_cardNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedOption = addCard_cardNameComboBox.Text;
            if (selectedOption == "") return;
            
            _currentCard = await ScryfallEndpoints.GetCardByName(selectedOption);
            UpdateCardInfo();
            
            if (_currentCard?.prices.eur == null)
            {
                addCard_cardPictureBox.Image = null;
                addCard_setPrintCheckBox.Enabled = false;
                return;
            }
            
            addCard_cardPictureBox.LoadAsync(_currentCard.image_uris.normal);
            addCard_setPrintCheckBox.Enabled = true;

            var prints = await ScryfallEndpoints.GetCardPrints(_currentCard);
            addCard_pickPrintComboBox.Items.Clear();
            foreach (var data in prints.data)
            {
                var printID = $"{data.set_name} /{data.collector_number}";
                addCard_pickPrintComboBox.Items.Add(printID);
            }

            _cardToSave = _currentCard.CreateCardObject();

            addCard_confirmButton.Enabled = true;
        }
        
        private async void UpdateCardInfo()
        {
            addCard_priceLabel.Text = _currentCard == null ? "Price: 0\u20ac" : $"Price: {_currentCard.prices.eur}\u20ac";
            addCard_rankLabel.Text = _currentCard == null ? "EDHREC rank: 0" : $"EDHREC rank: {_currentCard.edhrec_rank}";

            if (_currentCard == null || _cardToSave == null)
            {
                add_cardStockDataCountLabel.Text = "Total count: 0";
                addCard_locationCountDataLabel.Text = "";
                return;
            }

            var cards = await CardEndpoints.GetCards(_cardToSave.Name, _cardToSave.PrintID);
            add_cardStockDataCountLabel.Text = $"Total count: {cards.Sum(e => e.Count)}";

            var sb = new StringBuilder();
            foreach (var card in cards)
            {
                var location = await LocationEndpoints.GetLocation(card.LocationID);
                sb.Append($"{location.Code}: {card.Count}\n");
            }
            addCard_locationCountDataLabel.Text = sb.ToString();
        }

        private async void addCard_confirmButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(countToAddTextBox.Text, out var countToAddInt) || countToAddInt <= 0)
            {
                MessageBox.Show(
                    $"Nieprawidłowa ilość kart do dodania!",
                    "Ostrzeżenie",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            
            var locationSearchWindow = new AutoLocationSearchWindow();
            addCard_cardNameComboBox.Focus();
            Hide();
            locationSearchWindow.FormClosed += (o, args) => Show();
            await locationSearchWindow.FindLocation(_cardToSave, countToAddInt);
            locationSearchWindow.ShowDialog();

            _currentCard = null;
            _cardToSave = null;

            countToAddTextBox.Value = 1;
            addCard_cardNameComboBox.Text = addCard_pickPrintComboBox.Text = "";
            addCard_cardNameComboBox.SelectedIndex = addCard_pickPrintComboBox.SelectedIndex = -1;
            addCard_pickPrintComboBox.Enabled = false;
            
            add_cardStockDataCountLabel.Text = "Total count: 0";
            addCard_priceLabel.Text = "Price: 0\u20ac";
            addCard_rankLabel.Text = "EDHC Rank: 0";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}