using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Poker.Models
{
    /// <summary>
    /// Represents a deck of cards
    /// <summary/>
    public class Deck
    {
        private static readonly IEnumerable<Card> _cards;
        private static readonly RandomNumberGenerator _rng;

        private Card[] _deck;
        private int _takeIndex;
        static Deck()
        {
            var cards = new List<Card>();
            for (int i = 0; i < 52; i++)
            {
                cards.Add(Card.FromHashCode(i));
            }
            _cards= cards.AsReadOnly();
            _rng = RandomNumberGenerator.Create();
        }
        /// <summary>
        /// Creates shuffled deck
        /// </summary>
        public Deck()
        {
            _deck = Shuffle();
            _takeIndex = _deck.Length;
        }

        private static Card[] Shuffle()
        {
            var shuffled = _cards.ToArray();

            for (int i = shuffled.Length - 1; i > 0; i--)
            {
                byte[] randomBytes = new byte[4];
                _rng.GetBytes(randomBytes);
                int randomNumber = Math.Abs(BitConverter.ToInt32(randomBytes, 0)) % (i + 1);
                (shuffled[i], shuffled[randomNumber]) = (shuffled[randomNumber], shuffled[i]);
            }

            return shuffled;
        }

        /// <summary>
        /// Takes top card of the deck
        /// </summary>
        /// <returns>Top card of the deck</returns>
        /// <exception cref="InvalidOperationException">Throws when deck is empty</exception>
        public Card TakeCard()
        {
            if (_takeIndex == 0) throw new InvalidOperationException();

            _takeIndex--;
            return _deck[_takeIndex];
        }
    }
}
