using System;
using System.Windows.Forms;

namespace MTGStorage
{
    public partial class SryfallSearchLoadingWindow : Form
    {
        public SryfallSearchLoadingWindow()
        {
            InitializeComponent();
        }

        public void UpdateProgress(int current, int max)
        {
            var percentage = current / (float)max;
            progressBar.Value = (int)Math.Ceiling(percentage * 100);
            progressLabel.Text = $"Searching through pages. {current}/{max}";
        }
    }
}