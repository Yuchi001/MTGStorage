using System;
using System.Windows.Forms;
using MTGStorage.Database.DataObjects;
using MTGStorage.Features;

namespace MTGStorage.CustomUI
{
    public partial class CardElement : UserControl
    {
        private readonly Card Card;
        private readonly ShipmentManager Manager;
        
        public CardElement(Card card, ShipmentManager manager)
        {
            InitializeComponent();
            Card = card;
            Manager = manager;
            if (!string.IsNullOrEmpty(card.ImageUrl)) cardPictureBox.LoadAsync(card.ImageUrl);
            cardToShipTextbox.Maximum = Math.Max(0, card.Count);
            cardToShipTextbox.Value = Math.Min(card.Count, manager.GetCount(card));
            inStockLabel.Text = $"In stock: {card.Count}";
            priceLabel.Text = $"{card.Price}\u20ac";
        }
        
        public CardElement(string url)
        {
            InitializeComponent();
            Card = null;
            Manager = null;
            cardPictureBox.LoadAsync(url);
            inStockLabel.Enabled = false;
            priceLabel.Enabled = false;
            cardToShipTextbox.Enabled = false;
        }

        private void cardToShipTextbox_ValueChanged(object sender, EventArgs e)
        {
            var count = (int)cardToShipTextbox.Value;
            if (count < 0) cardToShipTextbox.Value = 0;
            if (count > Card.Count) cardToShipTextbox.Value = Card.Count;
            
            Manager.SetCard(Card, (int)cardToShipTextbox.Value);
        }

        private void cardPictureBox_Click(object sender, EventArgs e)
        {
            if (Card == null || Manager == null || cardToShipTextbox.Value >= cardToShipTextbox.Maximum) return;
            cardToShipTextbox.Value++;
            var count = (int)cardToShipTextbox.Value;
            if (count < 0) cardToShipTextbox.Value = 0;
            if (count > Card.Count) cardToShipTextbox.Value = Card.Count;
            
            Manager.SetCard(Card, (int)cardToShipTextbox.Value);
        }
    }
}
