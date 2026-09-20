using System;
using System.Windows.Forms;
using MTGStorage.Database.DataObjects;
using MTGStorage.Database.Endpoints;
using static System.Int32;

namespace MTGStorage
{
    public partial class AddLocationWindow : Form
    {
        private Location _currentLocation;
        
        public AddLocationWindow()
        {
            InitializeComponent();
            _currentLocation = new Location
            {
                Capacity = -1,
                Code = "",
            };
        }

        private async void confirmButton_Click(object sender, EventArgs e)
        {
            if (TryParse(capacityTextBox.Text, out var capacity)) _currentLocation.Capacity = capacity;
            else _currentLocation.Capacity = -1;
            
            if (decimal.TryParse(minimumPriceTextBox.Text, out var minPrice)) _currentLocation.MinPrice = minPrice;
            else _currentLocation.MinPrice = -1;
            
            if (_currentLocation.Code.Length <= 0)
            {
                MessageBox.Show(
                    $"Invalid location code.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            
            if (!await LocationEndpoints.CanAddLocation(_currentLocation.Code))
            {
                MessageBox.Show(
                    $"A location with code {codeTextBox.Text} already exists.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            
            if (_currentLocation.Capacity <= 0)
            {
                MessageBox.Show(
                    $"Invalid location capacity.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            
            if (_currentLocation.MinPrice < 0)
            {
                MessageBox.Show(
                    $"Invalid location minimum price.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            await LocationEndpoints.AddLocation(_currentLocation);
            
            var result = MessageBox.Show(
                $"Location {_currentLocation.Code} has been added.\nWould you like to continue adding locations?",
                "Information",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );
            
            if (result == DialogResult.No) Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void codeTextBox_TextChanged(object sender, EventArgs e)
        {
            _currentLocation.Code = codeTextBox.Text;
            confirmButton.Enabled = _currentLocation.Code != "";
        }
    }
}