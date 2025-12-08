using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Spades,
        Clubs
    }

    public enum Rank
    {
        Two=2, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
        Jack, Queen, King, Ace
    }

    public class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }

        public Card(Suit suit, Rank rank)
        { 
            Suit = suit;
            Rank = rank;
        }
    }
}