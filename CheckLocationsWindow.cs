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
        }

        public async Task Init()
        {
            var locations = await LocationEndpoints.GetLocations();
            foreach (var location in locations)
            {
                var count = location.Cards.Sum(e => e.Count);
                locationTable.Rows.Add(
                    location.Code,
                    location.MinPrice,
                    location.Capacity,
                    count,
                    $"{count / (float)location.Capacity * 100:F2}%"
                );
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}