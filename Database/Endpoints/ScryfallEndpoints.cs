using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using MTGStorage.Database.DataObjects;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace MTGStorage.Database.Endpoints
{
    public static class ScryfallEndpoints
    {
        private static readonly SemaphoreSlim NamedRequestGate = new SemaphoreSlim(1, 1);
        private static readonly Stopwatch NamedRequestInterval = new Stopwatch();

        private static async Task<HttpResponseMessage> GetNamedResponse(HttpClient client, string url)
        {
            await NamedRequestGate.WaitAsync();
            try
            {
                // Shared by exact and fuzzy lookups, including concurrent callers.
                while (NamedRequestInterval.IsRunning)
                {
                    var remaining = 600 - NamedRequestInterval.Elapsed.TotalMilliseconds;
                    if (remaining <= 0) break;
                    await Task.Delay((int)Math.Ceiling(remaining));
                }

                NamedRequestInterval.Restart();
                return await client.GetAsync(url);
            }
            finally
            {
                NamedRequestGate.Release();
            }
        }
        public static async Task<ScryfallCard> GetExactCard(string name, string set = null)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "MTGStorage/1.0");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var url = "https://api.scryfall.com/cards/named?exact=" + Uri.EscapeDataString(name);
                if (!string.IsNullOrEmpty(set)) url += "&set=" + Uri.EscapeDataString(set);
                using (var response = await GetNamedResponse(client, url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                    response.EnsureSuccessStatusCode();
                    var card = JsonConvert.DeserializeObject<ScryfallCard>(await response.Content.ReadAsStringAsync());
                    if (card.image_uris == null) card.image_uris = card.card_faces?.FirstOrDefault()?.image_uris;
                    if (card.image_uris == null) throw new InvalidOperationException("No image available for " + name);
                    return card;
                }
            }
        }
        public static async Task<List<string>> GetCardNames(string query)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(
                    "User-Agent",
                    "MTGStorage/1.0");

                client.DefaultRequestHeaders.Add(
                    "Accept",
                    "application/json");
                
                var url = "https://api.scryfall.com/cards/autocomplete?q=" +
                          Uri.EscapeDataString(query);

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ScryfallAutocomplete>(json);

                return result.Data.ToList();
            }
        }
        
        public static async Task<ScryfallCard> GetCardByName(string name)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(
                    "User-Agent",
                    "MTGStorage/1.0");

                client.DefaultRequestHeaders.Add(
                    "Accept",
                    "application/json");
                
                var url = "https://api.scryfall.com/cards/named?fuzzy=" + Uri.EscapeDataString(name);

                var response = await GetNamedResponse(client, url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var card = JsonConvert.DeserializeObject<ScryfallCard>(json);
                if (card.image_uris == null) card.image_uris = card.card_faces[0].image_uris;
                
                return card;
            }
        }

        public static async Task<ScryfallPrints> GetCardPrints(ScryfallCard card)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(
                    "User-Agent",
                    "MTGStorage/1.0");

                client.DefaultRequestHeaders.Add(
                    "Accept",
                    "application/json");
                
                var url = card.prints_search_uri;

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ScryfallPrints>(json);
                result.data = result.data.Where(e => e.image_uris != null && e.prices?.eur != null).ToArray();

                return result;
            }
        }

        public static async Task<ScryfallSearch> SearchCards(string input)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(
                    "User-Agent",
                    "MTGStorage/1.0");

                client.DefaultRequestHeaders.Add(
                    "Accept",
                    "application/json");
                
                var url = $"https://api.scryfall.com/cards/search?{input.Replace(" ", "+")}";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ScryfallSearch>(json);
                foreach (var card in result.data)
                {
                    if (card.image_uris != null) continue;
                    card.image_uris = card.card_faces[0].image_uris;
                }

                return result;
            }
        }
    }
}