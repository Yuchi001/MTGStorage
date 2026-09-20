using System.Collections.Generic;
using System.Linq;
using MTGStorage.Database.DataObjects;

namespace MTGStorage.Features
{
    public class ShipmentManager
    {
        private readonly Dictionary<Card, int> _cards = new Dictionary<Card, int>();

        public void SetCard(Card card, int count)
        {
            _cards[card] = count;
        }
        
        public void AddCard(Card card)
        {
            if (_cards.ContainsKey(card)) _cards[card]++;
            else _cards.Add(card, 1);
        }

        public void RemoveCard(Card card)
        {
            _cards[card]--;
        }

        public void Clear() => _cards.Clear();
        
        public bool CreatedShipment() => _cards.Sum(e => e.Value) > 0;

        public bool Next(out ShipmentCard shipmentCard)
        {
            shipmentCard = null;
            if (!_cards.Any()) return false;

            var card = _cards.First();
            shipmentCard = new ShipmentCard(card.Key, card.Value);
            _cards.Remove(card.Key);
            return true;
        }

        public class ShipmentCard
        {
            public Card Card { get; private set; }
            public int Count { get; private set; }

            public ShipmentCard(Card card, int count)
            {
                Card = card;
                Count = count;
            }
        }
    }
}