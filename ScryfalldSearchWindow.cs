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
    public partial class ScryfalldSearchWindow : Form
    {
        private const int PAGE_COUNT = 8;
        
        private List<Card> _cards = new List<Card>();
        private int _currentPage = 0;
        private int _maxPage;

        private readonly SearchOptions SearchOptions;
        private readonly ShipmentManager ShipmentManager;
        
        public ScryfalldSearchWindow()
        {
            ShipmentManager = new ShipmentManager();
            InitializeComponent();
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

        private async void loadCardsButton_Click(object sender, EventArgs e)
        {
            ShipmentManager.Clear();
            
            _currentPage = 1;
            
            var loadingForm = new SryfallSearchLoadingWindow();
            loadingForm.Show(this);

            try
            {
                _cards = await CardEndpoints.SearchCards(loadCardTextBox.Text, loadingForm.UpdateProgress);

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
            }

            if (_cards.Any()) return;
            
            MessageBox.Show(
                "No cards found that match your criteria.",
                "Information!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
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

            var cardBulkShipmentWindow = new CardBulkShipmentWindow();
            cardBulkShipmentWindow.FormClosed += (o, args) => Close();
            await cardBulkShipmentWindow.Init(ShipmentManager);
            
            Hide();
            cardBulkShipmentWindow.ShowDialog();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}