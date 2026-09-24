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
            UpdateProgress(current, max, "Searching through pages.");
        }

        public void UpdateProgress(int current, int max, string message)
        {
            if (IsDisposed) return;
            var percentage = max > 0 ? current / (double)max : 0;
            progressBar.Value = Math.Max(0, Math.Min(100, (int)Math.Ceiling(percentage * 100)));
            progressLabel.Text = $"{message} {current}/{max}";
        }
    }
}