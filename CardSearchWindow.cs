using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTGStorage.CustomUI;
using MTGStorage.Database.DataObjects;
using MTGStorage.Database.Endpoints;
using MTGStorage.Features;

namespace MTGStorage
{
    public partial class CardSearchWindow : Form
    {
        private const int PAGE_COUNT = 8;
        
        private List<Card> _cards = new List<Card>();
        private int _currentPage = 0;
        private int _maxPage;

        private SearchOptions _options;
        
        private readonly ShipmentManager ShipmentManager;
        
        public CardSearchWindow()
        {
            ShipmentManager = new ShipmentManager();
            InitializeComponent();
            searchCombobox.Text = "";
        }

        public async Task Init()
        {
            searchCombobox.DataSource = (await CardEndpoints.GetCardsDistinct()).Select(e => e.Name + $"{(e.PrintID != "" ? " " : "")}{e.PrintID}").ToList();
            searchCombobox.SelectedIndex = -1;
            searchCombobox.Text = "";
        }
        
        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            bindingNavigatorMoveFirstItem.Enabled = false;
            bindingNavigatorMovePreviousItem.Enabled = false;
            bindingNavigatorMoveLastItem.Enabled = true;
            bindingNavigatorMoveNextItem.Enabled = true;
            LoadCards();
        }
        
        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            _currentPage = _maxPage;
            bindingNavigatorMoveLastItem.Enabled = false;
            bindingNavigatorMoveNextItem.Enabled = false;
            bindingNavigatorMoveFirstItem.Enabled = true;
            bindingNavigatorMovePreviousItem.Enabled = true;
            LoadCards();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            _currentPage--;
            LoadCards();
            
            bindingNavigatorMoveNextItem.Enabled = true;
            if (_currentPage != 1) return;
            
            bindingNavigatorMoveFirstItem.Enabled = bindingNavigatorMovePreviousItem.Enabled = false;
            bindingNavigatorMoveLastItem.Enabled = true;
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            _currentPage++;
            LoadCards();
            
            bindingNavigatorMovePreviousItem.Enabled = true;
            if (_currentPage != _maxPage) return;
            
            bindingNavigatorMoveLastItem.Enabled = bindingNavigatorMoveNextItem.Enabled = false;
            bindingNavigatorMoveFirstItem.Enabled = true;
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            _currentPage = int.Parse(bindingNavigatorPositionItem.Text);
            if (_currentPage < 1) _currentPage = 1;
            if (_currentPage > _maxPage) _currentPage = _maxPage;
            
            LoadCards();
        }

        private void LoadCards()
        {
            bindingNavigatorPositionItem.Text = $"{_currentPage}";
            cardLayoutPanel.Controls.Clear();

            var list = _cards.Skip(PAGE_COUNT * (_currentPage - 1)).Take(PAGE_COUNT);
            foreach (var card in list)
            {
                var cardElement = new CardElement(card, ShipmentManager);
                cardLayoutPanel.Controls.Add(cardElement);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private async void shipButton_Click(object sender, EventArgs e)
        {
            if (!ShipmentManager.CreatedShipment())
            {
                MessageBox.Show(
                    $"No cards selected to ship.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            Cursor = Cursors.WaitCursor;
            Enabled = false;

            var cardBulkShipmentWindow = new CardBulkShipmentWindow();
            cardBulkShipmentWindow.FormClosed += (o, args) => Close();
            await cardBulkShipmentWindow.Init(ShipmentManager);

            Enabled = true;
            Cursor = Cursors.Default;
            
            Hide();
            cardBulkShipmentWindow.ShowDialog();
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            ShipmentManager.Clear();
            
            _currentPage = 1;
            
            var loadingForm = new SryfallSearchLoadingWindow();
            loadingForm.Show(this);

            try
            {
                _cards = _options == null ? await CardEndpoints.GetCards(searchCombobox.Text) : await CardEndpoints.GetCards(_options);

                _maxPage = (int)Math.Ceiling(_cards.Count / (float)PAGE_COUNT);
                bindingNavigatorCountItem.Text = $"{_maxPage}";

                bindingNavigatorMoveNextItem.Enabled =
                    bindingNavigatorMoveLastItem.Enabled =
                        bindingNavigatorPositionItem.Enabled = 
                            bindingNavigatorPositionItem.Enabled = _maxPage > 1;
                
                LoadCards();
            }
            finally
            {
                loadingForm.Close();
                loadingForm.Dispose();
                _options = null;
                searchCombobox.Text = "";
            }
            
            if (_cards.Any()) return;
            
            MessageBox.Show(
                "No cards found that match your criteria.",
                "Information!",
                MessageBoxButtons.OK,
                 MessageBoxIcon.Warning
            );
        }

        private void ResetOptions() => _options = null;

        private void advancedButton_Click(object sender, EventArgs e)
        {
            _options = new SearchOptions();
            var advancedSearchOptionWindow = new AdvancedSearchOptions(_options, ResetOptions);
            advancedSearchOptionWindow.FormClosed += HandleFormClosed;

            advancedSearchOptionWindow.ShowDialog();
            return;

            void HandleFormClosed(object o, FormClosedEventArgs formClosedEventArgs)
            {
                if (_options == null) return;
                searchButton_Click(sender, e);
            }
        }
    }
}