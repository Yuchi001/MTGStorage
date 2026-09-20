using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using MTGStorage;
class PercentageSortRegression
{
    [STAThread]
    static void Main()
    {
        using (var form = new CheckLocationsWindow())
        {
            var grid = (DataGridView)typeof(CheckLocationsWindow).GetField("locationTable", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form);
            decimal[] values = { .91m, .09m, 1m, .1m, .905m, 0m, .099m };
            foreach (var value in values) grid.Rows.Add("Box", 0m, 1000, (int)(value * 1000), value);
            foreach (var direction in new[] { ListSortDirection.Ascending, ListSortDirection.Descending })
            {
                grid.Sort(grid.Columns["percentage"], direction);
                for (int i = 1; i < grid.Rows.Count; i++)
                {
                    decimal previous = (decimal)grid.Rows[i-1].Cells["percentage"].Value;
                    decimal current = (decimal)grid.Rows[i].Cells["percentage"].Value;
                    if (direction == ListSortDirection.Ascending ? previous > current : previous < current)
                        throw new Exception("Incorrect numeric sorting");
                }
                Console.WriteLine("PASS: " + direction + " numeric percentage sorting");
            }
            if (!grid.Rows[0].Cells["percentage"].FormattedValue.ToString().Contains("%"))
                throw new Exception("Missing percent format");
            Console.WriteLine("PASS: percentage display preserved");
        }
    }
}
