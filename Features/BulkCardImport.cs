using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MTGStorage.Database.DataObjects;
using MTGStorage.Database.Endpoints;

namespace MTGStorage.Features
{
    public static class BulkCardImport
    {
        public sealed class Entry
        {
            public int LineNumber { get; set; }
            public string Name { get; set; }
            public int Count { get; set; }
        }

        public sealed class Placement
        {
            public Card Card { get; set; }
            public Location Location { get; set; }
            public long Owned { get; set; }
            public int Count { get; set; }
        }

        public static List<Entry> Parse(IEnumerable<string> lines)
        {
            var result = new List<Entry>();
            int lineNumber = 0;
            foreach (var line in lines)
            {
                lineNumber++;
                if (string.IsNullOrWhiteSpace(line)) continue;
                var name = line.Trim();
                int count = 1;
                var match = Regex.Match(name, @"^(.+?)\s+([+-]?\d+)$");
                if (match.Success)
                {
                    if (!int.TryParse(match.Groups[2].Value, out count) || count <= 0)
                        throw new FormatException($"Line {lineNumber}: count must be a positive integer.");
                    name = match.Groups[1].Value.Trim();
                }
                result.Add(new Entry { LineNumber = lineNumber, Name = name, Count = count });
            }
            if (result.Count == 0) throw new FormatException("The file contains no cards.");
            return result;
        }

        public static async Task<List<Placement>> Prepare(IEnumerable<string> lines, Action<int, int, string> reportProgress = null)
        {
            var entries = Parse(lines);
            reportProgress?.Invoke(0, entries.Count, "Fetching cards.");
            var cards = new List<KeyValuePair<Card, int>>();
            var cache = new Dictionary<string, Card>(StringComparer.OrdinalIgnoreCase);
            foreach (var entry in entries)
            {
                try
                {
                    if (!cache.TryGetValue(entry.Name, out var card))
                    {
                        var found = await ScryfallEndpoints.GetCardByName(entry.Name);
                        if (found == null) throw new InvalidOperationException("Card not found: " + entry.Name);
                        if (string.IsNullOrEmpty(found.prices?.eur))
                            throw new InvalidOperationException("No EUR price available for " + found.name);
                        card = found.CreateCardObject();
                        cache.Add(entry.Name, card);
                    }
                    cards.Add(new KeyValuePair<Card, int>(card, entry.Count));
                    reportProgress?.Invoke(cards.Count, entries.Count, "Fetching cards.");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Line {entry.LineNumber}: {ex.Message}", ex);
                }
            }
            reportProgress?.Invoke(entries.Count, entries.Count, "Assigning locations.");
            return BuildPlan(cards, await LocationEndpoints.GetLocations());
        }

        public static List<Placement> BuildPlan(IEnumerable<KeyValuePair<Card, int>> cards, IEnumerable<Location> locations)
        {
            // Reserve space in a copy, without changing storage before Complete.
            var snapshot = locations.Select(l => new Location
            {
                ID = l.ID, Code = l.Code, Capacity = l.Capacity, MinPrice = l.MinPrice,
                Cards = new List<Card>(l.Cards)
            }).ToList();
            var ownedByLocation = snapshot.ToDictionary(l => l.ID,
                l => l.Cards.GroupBy(c => c.Name, StringComparer.OrdinalIgnoreCase)
                    .ToDictionary(g => g.Key, g => g.Sum(c => (long)c.Count), StringComparer.OrdinalIgnoreCase));
            var result = new List<Placement>();
            foreach (var item in cards)
            {
                if (item.Value <= 0) throw new ArgumentException("Count must be positive.");
                int remaining = item.Value;
                while (remaining > 0)
                {
                    var location = LocationEndpoints.FindLocation(snapshot, item.Key);
                    if (location == null)
                        throw new InvalidOperationException("No matching location with enough space for " + item.Key.Name);
                    int count = (int)Math.Min(remaining, LocationEndpoints.FreeSpace(location));
                    ownedByLocation[location.ID].TryGetValue(item.Key.Name, out var owned);
                    result.Add(new Placement { Card = item.Key, Location = location, Count = count, Owned = owned });
                    location.Cards.Add(new Card { Name = item.Key.Name, Count = count });
                    remaining -= count;
                }
            }
            return result;
        }
    }
}
