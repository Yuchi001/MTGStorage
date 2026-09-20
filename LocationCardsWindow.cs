using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTGStorage.CustomUI;
using MTGStorage.Database.DataObjects;
using MTGStorage.Database.Endpoints;
using MTGStorage.Features;

namespace MTGStorage
{
    public partial class LocationCardsWindow : Form
    {
        private readonly ShipmentManager manager = new ShipmentManager();
        private int currentPage = 1;
        private int MaxPage => Math.Max(1, (cards.Count + 7) / 8);
        private List<Card> cards = new List<Card>();
        private Location location;
        public LocationCardsWindow()
        {
            InitializeComponent();
        }

        private void First_Click(object sender, EventArgs e) => GoToPage(1);
        private void Previous_Click(object sender, EventArgs e) => GoToPage(currentPage - 1);
        private void Next_Click(object sender, EventArgs e) => GoToPage(currentPage + 1);
        private void Last_Click(object sender, EventArgs e) => GoToPage(MaxPage);
        private void Page_Leave(object sender, EventArgs e) => CommitPage();
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            CommitPage();
            e.SuppressKeyPress = true;
        }
        private void Close_Click(object sender, EventArgs e) => Close();
        private async void Ship_Click(object sender, EventArgs e) => await Ship();
        private async void RelocateSelected_Click(object sender, EventArgs e) => await Relocate(false);
        private async void RelocateAll_Click(object sender, EventArgs e) => await Relocate(true);

        public async Task Init(Location selectedLocation)
        {
            location = selectedLocation ?? throw new ArgumentNullException(nameof(selectedLocation));
            await RefreshCards();
        }
        private async Task RefreshCards()
        {
            cards = await LocationEndpoints.GetCardsInLocation(location.ID);
            foreach (var card in cards) card.Location = location;
            manager.Clear();
            title.Text = "Location: " + location.Code;
            summary.Text = $"{cards.Sum(c => (long)c.Count)} cards / {location.Capacity} capacity\nSelect quantities on the cards to ship or relocate them.";
            currentPage = 1;
            ship.Enabled = relocateSelected.Enabled = relocateAll.Enabled = cards.Any(c => c.Count > 0);
            LoadPage();
        }
        private void LoadPage()
        {
            while (cardPanel.Controls.Count > 0) cardPanel.Controls[0].Dispose();
            foreach (var card in cards.Skip((currentPage - 1) * 8).Take(8))
                cardPanel.Controls.Add(new CardElement(card, manager));
            first.Enabled = previous.Enabled = currentPage > 1;
            last.Enabled = next.Enabled = currentPage < MaxPage;
            page.Enabled = MaxPage > 1;
            page.Text = currentPage.ToString();
            pages.Text = "of " + MaxPage;
        }
        private void GoToPage(int number) { currentPage = Math.Max(1, Math.Min(MaxPage, number)); LoadPage(); }
        private void CommitPage() { int number; GoToPage(int.TryParse(page.Text, out number) ? number : currentPage); }
        private async Task Ship()
        {
            if (!manager.CreatedShipment()) { MessageBox.Show(this,"No cards selected to ship."); return; }
            Enabled = false;
            try
            {
                using (var window = new CardBulkShipmentWindow())
                {
                    await window.Init(manager, location.ID);
                    window.ShowDialog(this);
                }
                await RefreshCards();
            }
            catch (Exception ex) { MessageBox.Show(this,ex.Message,"Shipment error",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            finally { Enabled = true; }
        }
        private async Task Relocate(bool all)
        {
            var selected = all ? cards.Where(c => c.Count > 0).ToDictionary(c => c.ID,c => c.Count)
                : manager.GetSelectedCards().ToDictionary(c => c.Card.ID,c => c.Count);
            if (selected.Count == 0) { MessageBox.Show(this,"No cards selected to relocate."); return; }
            Enabled = false;
            try
            {
                var count = selected.Sum(p => (long)p.Value);
                var destinations = RelocationEndpoints.ManualDestinations(await LocationEndpoints.GetLocations(), location.ID, selected);
                using (var dialog = new RelocateCardsWindow(destinations,count))
                {
                    if (dialog.ShowDialog(this) != DialogResult.OK) return;
                    var moved = await RelocationEndpoints.Relocate(location.ID,dialog.SelectedLocation?.ID ?? 0,selected,all);
                    using (var report = new Form { Text = "Relocation completed — put cards in these locations", ClientSize = new Size(720,420), StartPosition = FormStartPosition.CenterParent })
                    {
                        var grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
                            AllowUserToDeleteRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, RowHeadersVisible = false };
                        grid.Columns.Add("card", "Card");
                        grid.Columns.Add("print", "Print");
                        grid.Columns.Add("count", "Quantity");
                        grid.Columns.Add("from", "From");
                        grid.Columns.Add("to", "Put in location");
                        foreach (var item in moved) grid.Rows.Add(item.Name,item.PrintID,item.Count,location.Code,item.TargetCode);
                        report.Controls.Add(grid);
                        report.ShowDialog(this);
                    }
                }
                await RefreshCards();
            }
            catch (Exception ex) { MessageBox.Show(this,ex.Message,"Relocation error",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            finally { Enabled = true; }
        }
    }
}
