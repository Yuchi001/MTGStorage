using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTGStorage.Database.Endpoints;

namespace MTGStorage
{
    public partial class CheckLocationsWindow : Form
    {
        public CheckLocationsWindow()
        {
            InitializeComponent();
            percentage.ValueType = typeof(decimal);
            percentage.DefaultCellStyle.Format = "P2";
            locationTable.SortCompare += LocationTable_SortCompare;
            locationTable.CellContentClick += OpenLocation;
        }

        private void LocationTable_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column != percentage)
            {
                return;
            }

            e.SortResult = Convert.ToDecimal(e.CellValue1).CompareTo(Convert.ToDecimal(e.CellValue2));
            e.Handled = true;
        }

        public async Task Init()
        {
            var locations = await LocationEndpoints.GetLocations();
            locationTable.Rows.Clear();
            foreach (var location in locations)
            {
                var count = location.Cards.Sum(e => e.Count);
                var rowIndex = locationTable.Rows.Add(
                    location.Code,
                    location.MinPrice,
                    location.Capacity,
                    count,
                    location.Capacity > 0 ? count / (decimal)location.Capacity : 0m
                );
                locationTable.Rows[rowIndex].Tag = location;
            }
        }

        private async void OpenLocation(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || locationTable.Columns[e.ColumnIndex] != viewCards) return;
            var location = (Database.DataObjects.Location)locationTable.Rows[e.RowIndex].Tag;
            Enabled = false;
            try
            {
                using (var window = new LocationCardsWindow { Icon = Icon })
                {
                    await window.Init(location);
                    window.ShowDialog(this);
                }
                await Init();
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "Location error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { Enabled = true; }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
