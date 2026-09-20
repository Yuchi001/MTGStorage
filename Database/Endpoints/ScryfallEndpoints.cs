using System;
using System.Collections.Generic;
using System.Linq;
using MTGStorage.Database.DataObjects;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace MTGStorage.Database.Endpoints
{
    public static class ScryfallEndpoints
    {
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
                
                var url = "https://api.scryfall.com/cards/named?fuzzy=" + name.Replace(" ", "+");

                var response = await client.GetAsync(url);
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