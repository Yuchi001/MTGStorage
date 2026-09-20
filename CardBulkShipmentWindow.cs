using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTGStorage.CustomUI;
using MTGStorage.Database.DataObjects;
using MTGStorage.Database.Endpoints;
using MTGStorage.Features;

namespace MTGStorage
{
    public partial class CardBulkShipmentWindow : Form
    {
        private List<LocationCardPair> LocationCardPairs;
        private CardElement _hoverControl;
        private int? _sourceLocationId;
        
        public CardBulkShipmentWindow()
        {
            InitializeComponent();
        }

        public async Task Init(ShipmentManager shipmentManager, int? sourceLocationId = null)
        {
            _sourceLocationId = sourceLocationId;
            LocationCardPairs = new List<LocationCardPair>();
            while (shipmentManager.Next(out var shipmentCard))
            {
                if (shipmentCard.Count <= 0) continue;
                var pairs = await LocationCardPair.CreatePair(shipmentCard, sourceLocationId);
                LocationCardPairs.AddRange(pairs);
            }

            cardLocationPairTable.Rows.Clear();

            foreach (var pair in LocationCardPairs)
            {
                int rowIndex = cardLocationPairTable.Rows.Add(
                    pair.Card.Name,
                    pair.Card.PrintID,
                    await LoadImage(pair.Card.ImageUrl),
                    pair.Location.Code,
                    pair.Card.Count,
                    false
                );

                var row = cardLocationPairTable.Rows[rowIndex];

                row.Tag = pair.Card;
            }
            
            async Task<Image> LoadImage(string url)
            {
                if (string.IsNullOrEmpty(url)) return null;
                using (var client = new HttpClient())
                {
                    var bytes = await client.GetByteArrayAsync(url);

                    using (var stream = new MemoryStream(bytes))
                    {
                        using (var temp = Image.FromStream(stream))
                        {
                            return new Bitmap(temp);
                        }
                    }
                }
            }
        }
        
        private void cardLocationPairTable_CellMouseDoubleClick(
            object sender,
            DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (cardLocationPairTable.Columns[e.ColumnIndex].Name != "image")
                return;

            var image = cardLocationPairTable.Rows[e.RowIndex]
                .Cells[e.ColumnIndex]
                .Value as Image;

            if (image == null)
                return;

            using (var form = new Form())
            {
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.BackColor = Color.Black;
                form.Text = "MTG Storage - card image";
                form.Icon = this.Icon;
                form.MinimizeBox = false;
                form.MaximizeBox = false;

                var imageWidth = image.Width / 2;
                var imageHeight = image.Height / 2;

                var pictureBox = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    Image = image,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Cursor = Cursors.Hand
                };

                pictureBox.Click += (s, args) => form.Close();

                var borderPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(3),
                    BackColor = Color.White
                };

                borderPanel.Controls.Add(pictureBox);

                form.ClientSize = form.MinimumSize = form.MaximumSize = new Size(
                    imageWidth,
                    imageHeight);

                form.Controls.Add(borderPanel);

                form.ShowDialog();
            }
        }
        
        private void cardLocationPairTable_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = cardLocationPairTable.Rows[e.RowIndex];

            var card = row.DataBoundItem as Card;
            if (card == null)
                return;

            if (_hoverControl == null) _hoverControl = new CardElement(card.ImageUrl);

            var mousePosition = Cursor.Position;

            _hoverControl.Location = PointToClient(
                new Point(mousePosition.X + 15, mousePosition.Y + 15)
            );

            if (!_hoverControl.Visible)
            {
                Controls.Add(_hoverControl);
                _hoverControl.BringToFront();
                _hoverControl.Show();
            }
        }

        private void cardLocationPairTable_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (_hoverControl != null)
                _hoverControl.Hide();
        }

        public class LocationCardPair
        {
            public readonly Card Card;
            public readonly Location Location;

            private LocationCardPair(Card card, Location location)
            {
                Card = card;
                Location = location;
            }
            
            public static async Task<List<LocationCardPair>> CreatePair(ShipmentManager.ShipmentCard shipmentCard, int? sourceLocationId = null)
            {
                var locationCardPairList = new List<LocationCardPair>();
                List<Card> cards;
                if (sourceLocationId.HasValue)
                {
                    var selected = await CardEndpoints.GetCard(shipmentCard.Card.ID);
                    cards = selected != null && selected.LocationID == sourceLocationId.Value
                        ? new List<Card> { selected } : new List<Card>();
                }
                else
                    cards = await CardEndpoints.GetCards(shipmentCard.Card.Name, shipmentCard.Card.PrintID, false);
                var count = shipmentCard.Count;
                if (cards.Sum(c => (long)c.Count) < count)
                    throw new InvalidOperationException("The selected cards are no longer available.");

                while (count > 0)
                {
                    var firstLocationCard = cards.First();
                    var taken = Math.Min(count, firstLocationCard.Count);
                    count -= taken;
                    firstLocationCard.Count = taken;
                    locationCardPairList.Add(new LocationCardPair(firstLocationCard, firstLocationCard.Location));
                    cards.Remove(firstLocationCard);
                }

                return locationCardPairList;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (_sourceLocationId.HasValue) { Close(); return; }
            Application.Restart();
        }

        private async void confirmButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Do you want to continue? All selected cards will be removed from your storage.",
                "Question",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );
            if (result == DialogResult.No) return;

            foreach (var pair in LocationCardPairs) await CardEndpoints.RemoveCard(pair.Card.ID, pair.Card.Count);
            
            if (_sourceLocationId.HasValue) { Close(); return; }
            Application.Restart();
        }

        private void cardLocationPairTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var allCompleted = cardLocationPairTable.Rows
                .Cast<DataGridViewRow>()
                .Where(row => !row.IsNewRow)
                .All(row => Convert.ToBoolean(row.Cells["Completed"].Value ?? false));
            confirmButton.Enabled = allCompleted;
        }

        private void cardLocationPairTable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (cardLocationPairTable.IsCurrentCellDirty &&
                cardLocationPairTable.CurrentCell is DataGridViewCheckBoxCell)
            {
                cardLocationPairTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
                cardLocationPairTable_CellEndEdit(sender, null);
            }
        }
    }
}
