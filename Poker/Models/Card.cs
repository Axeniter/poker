namespace Poker.Models
{
    public enum Suit
    {
        Hearts=0,
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

        public override int GetHashCode()
        {
            return (int)Suit * 13 + (int)Rank - 2;
        }
    }
}