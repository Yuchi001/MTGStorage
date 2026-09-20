using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MTGStorage.Database.DataObjects;

namespace MTGStorage
{
    public partial class AdvancedSearchOptions : Form
    {
        private readonly SearchOptions _options;
        private readonly Action _resetOptions;
        
        public AdvancedSearchOptions(SearchOptions options, Action resetOptions)
        {
            InitializeComponent();

            _options = options;
            _resetOptions = resetOptions;

            foreach (var option in Enum.GetNames(typeof(SearchOptions.EColorsSearchType)))
            {
                colorSearchTypeCombobox.Items.Add(option);
            }

            colorSearchTypeCombobox.SelectedIndex = 0;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _resetOptions.Invoke();
            Close();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            _options.Keywords = GetKeywords();
            _options.Types = GetTypes();
            _options.Colors = GetColors();
            _options.ColorSearchType = (SearchOptions.EColorsSearchType)colorSearchTypeCombobox.SelectedIndex;
            _options.ColorIdentity = GetColorIdentity();
            _options.Cmc = realcmcTextbox.Value;
            _options.UseCmc = realcmcCheckbox.Checked;
            Close();
        }

        private List<string> GetKeywords() =>
            keywordsTextbox.Text == "" ? new List<string>() : keywordsTextbox.Text.Split(',').ToList();
        
        private List<string> GetTypes() =>
            typesTextbox.Text == "" ? new List<string>() : typesTextbox.Text.Split(',').ToList();
        
        private List<string> GetColors()
        {
            var colors = new List<string>();
            if (whiteCheckbox.Checked) colors.Add("W");
            if (blueCheckbox.Checked) colors.Add("U");
            if (blackCheckbox.Checked) colors.Add("B");
            if (redCheckbox.Checked) colors.Add("R");
            if (greenCheckbox.Checked) colors.Add("G");
            if (colorlessCheckbox.Checked) colors.Add("{C}");
            return colors;
        }

        private List<string> GetColorIdentity()
        {
            var colors = new List<string>();
            if (whiteIdentityCheckbox.Checked) colors.Add("W");
            if (blueIdentityCheckbox.Checked) colors.Add("U");
            if (blackIdentityCheckbox.Checked) colors.Add("B");
            if (redIdentityCheckbox.Checked) colors.Add("R");
            if (greenIdentityCheckbox.Checked) colors.Add("G");
            if (colorlessIdentityCheckbox.Checked) colors.Add("{C}");
            return colors;
        }

        private void realcmcCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            realcmcTextbox.Text = realcmcCheckbox.Checked ? "0" : realcmcTextbox.Text;
            realcmcTextbox.Enabled = realcmcCheckbox.Checked;
        }
    }
}