using System;
using System.Drawing;
using System.Windows.Forms;
using MTGStorage.Database.Endpoints;

namespace MTGStorage
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            csvExportButton.Click += csvExportButton_Click;
            csvImportButton.Click += csvImportButton_Click;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
           
        }

        private async void menu_addCardButton_Click(object sender, EventArgs e)
        {
            var hasFreeLocation = await LocationEndpoints.HasFreeLocation();
            if (!hasFreeLocation)
            {
                MessageBox.Show(
                    $"No available locations!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            
            Hide();
            var addCardWindow = new AddCardWindow();
            addCardWindow.FormClosed += (s, args) => Application.Exit();;
            addCardWindow.ShowDialog();
        }
        
        private void menu_viewStorageButton_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private async void menu_removeCardButton_Click(object sender, EventArgs e)
        {
            if ((await CardEndpoints.GetCards()).Count == 0)
            {
                MessageBox.Show(
                    $"No cards in stock!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            
            Hide();
            var removeCardWindow = new RemoveCardWindow();
            removeCardWindow.FormClosed += (s, args) => Application.Exit();;
            await removeCardWindow.Init();
            removeCardWindow.ShowDialog();
        }

        private void menu_addLocationButton_Click(object sender, EventArgs e)
        {
            Hide();
            var addLocationWindow = new AddLocationWindow();
            addLocationWindow.FormClosed += (s, args) => Application.Exit();;
            addLocationWindow.ShowDialog();
        }

        private async void menu_removeLocationButton_Click(object sender, EventArgs e)
        {
            Hide();
            var removeLocationWindow = new RemoveLocationWindow();
            removeLocationWindow.FormClosed += (s, args) => Application.Exit();;
            await removeLocationWindow.Init();
            removeLocationWindow.ShowDialog();
        }

        private async void searchCardButton_Click(object sender, EventArgs e)
        {
            Hide();
            var cardSearchWindow = new CardSearchWindow();
            await cardSearchWindow.Init();
            cardSearchWindow.FormClosed += (s, args) => Application.Exit();;
            cardSearchWindow.ShowDialog();
        }

        private void scryfallSearchButton_Click(object sender, EventArgs e)
        {
            Hide();
            var scryfallSearchWindow = new ScryfalldSearchWindow();
            scryfallSearchWindow.FormClosed += (s, args) => Application.Exit();;
            scryfallSearchWindow.ShowDialog();
        }

        private void removeCardBulkButton_Click(object sender, EventArgs e)
        {
            // TODO remove bulk button handle
        }

        private void addCardBulkButton_Click(object sender, EventArgs e)
        {
            // TODO add bulk button handle
        }

        private void csvExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ExportImportEndpoints.ExportToCSV())
                {
                    MessageBox.Show(
                        "CSV export has been saved.",
                        "CSV Export",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    "Failed to export data: " + exception.Message,
                    "CSV Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void csvImportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ExportImportEndpoints.ImportFromCSV())
                {
                    MessageBox.Show(
                        "CSV import has completed.",
                        "CSV Import",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    "Failed to import data: " + exception.Message,
                    "CSV Import Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void checkLocationsButton_Click(object sender, EventArgs e)
        {
            Hide();
            var checkLocationsWindow = new CheckLocationsWindow();
            await checkLocationsWindow.Init();
            checkLocationsWindow.FormClosed += (s, args) => Application.Exit();;
            checkLocationsWindow.ShowDialog();
        }
    }
}
