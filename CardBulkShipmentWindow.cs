using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTGStorage.Database.DataObjects;
using MTGStorage.Database.Endpoints;
using MTGStorage.Features;

namespace MTGStorage
{
    public partial class CardBulkShipmentWindow : Form
    {
        private List<LocationCardPair> LocationCardPairs;
        private bool _isLoadingImage;
        private List<BulkCardImport.Placement> _importPlacements;
        private bool _isSaving;
        private int? _sourceLocationId;
        
        public CardBulkShipmentWindow()
        {
            InitializeComponent();
        }

        public void InitImport(List<BulkCardImport.Placement> placements)
        {
            _importPlacements = placements;
            Text = "MTG Storage - bulk add";
            mainLabel.Text = "Bulk Add";
            informationLabel.Text = "Put each listed card into the given location";
            confirmButton.Text = "Complete";
            set.Name = "owned";
            set.HeaderText = "owned";
            set.ValueType = typeof(long);
            var priceColumn = new DataGridViewTextBoxColumn
            {
                Name = "price", HeaderText = "Price (EUR)", ReadOnly = true, Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
            };
            cardLocationPairTable.Columns.Insert(completed.Index, priceColumn);
            cardLocationPairTable.Columns.Insert(image.Index, new DataGridViewCheckBoxColumn
            {
                Name = "remove", HeaderText = "Remove", Width = 60
            });
            // Keep the shipment layout, with extra room for Price and Remove.
            MaximumSize = new Size(690, 500);
            MinimumSize = MaximumSize;
            cardLocationPairTable.Width += 140;
            mainLabel.Width += 140;
            informationLabel.Width += 140;
            confirmButton.Left += 140;
            cancelButton.Width += 140;
            foreach (var placement in placements)
            {
                int index = cardLocationPairTable.Rows.Add(placement.Card.Name, placement.Owned,
                    placement.Location.Code, placement.Count, placement.Card.Price, false, false);
                cardLocationPairTable.Rows[index].Tag = placement;
            }
            confirmButton.Enabled = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_isSaving) e.Cancel = true;
            base.OnFormClosing(e);
        }

        private async Task CompleteImport()
        {
            if (_isSaving || !AllCompleted()) return;
            _isSaving = true;
            confirmButton.Enabled = cancelButton.Enabled = cardLocationPairTable.Enabled = false;
            try
            {
                var selected = GetImportPlacementsToSave();
                if (selected.Count > 0) await CardEndpoints.AddCardsBulk(selected);
                var addedCount = selected.Sum(placement => (long)placement.Count);
                MessageBox.Show(this, $"Added {addedCount} card(s) to storage.",
                    "Bulk add completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isSaving = false;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No cards were added. " + ex.Message,
                    "Bulk add", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isSaving = false;
                if (!IsDisposed)
                {
                    cancelButton.Enabled = cardLocationPairTable.Enabled = true;
                    confirmButton.Enabled = AllCompleted();
                }
            }
        }

        private bool AllCompleted() => cardLocationPairTable.Rows.Count > 0 &&
            cardLocationPairTable.Rows.Cast<DataGridViewRow>()
                .All(row => IsRemoved(row) || Convert.ToBoolean(row.Cells["Completed"].Value ?? false));

        private bool IsRemoved(DataGridViewRow row) => _importPlacements != null &&
            Convert.ToBoolean(row.Cells["remove"].Value ?? false);

        private List<BulkCardImport.Placement> GetImportPlacementsToSave() =>
            cardLocationPairTable.Rows.Cast<DataGridViewRow>()
                .Where(row => !IsRemoved(row))
                .Select(row => (BulkCardImport.Placement)row.Tag).ToList();
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
                    pair.Location.Code,
                    pair.Card.Count,
                    false
                );

                var row = cardLocationPairTable.Rows[rowIndex];

                row.Tag = pair.Card;
            }
        }

        private async void cardLocationPairTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != image.Index || _isLoadingImage)
                return;

            var tag = cardLocationPairTable.Rows[e.RowIndex].Tag;
            var card = (tag as BulkCardImport.Placement)?.Card ?? tag as Card;
            if (string.IsNullOrEmpty(card?.ImageUrl))
                return;

            _isLoadingImage = true;
            try
            {
                using (var client = new HttpClient())
                {
                    var bytes = await client.GetByteArrayAsync(card.ImageUrl);
                    if (IsDisposed || Disposing) return;

                    using (var stream = new MemoryStream(bytes))
                    using (var cardImage = Image.FromStream(stream))
                    {
                        ShowCardImage(cardImage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!IsDisposed && !Disposing)
                    MessageBox.Show(this, "Unable to load card image: " + ex.Message,
                        "Card image", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingImage = false;
            }
        }

        private void ShowCardImage(Image image)
        {
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

                form.ShowDialog(this);
            }
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
            if (_importPlacements != null || _sourceLocationId.HasValue) { Close(); return; }
            Application.Restart();
        }

        private async void confirmButton_Click(object sender, EventArgs e)
        {
            if (_importPlacements != null) { await CompleteImport(); return; }

            var result = MessageBox.Show(
                "Do you want to continue? All selected cards will be removed from your storage.",
                "Question",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );
            if (result == DialogResult.No) return;

            foreach (var pair in LocationCardPairs) await CardEndpoints.RemoveCard(pair.Card.ID, pair.Card.Count);
            
            if (_importPlacements != null || _sourceLocationId.HasValue) { Close(); return; }
            Application.Restart();
        }

        private void cardLocationPairTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            confirmButton.Enabled = !_isSaving && AllCompleted();
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
