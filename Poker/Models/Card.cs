namespace Poker.Models
{
    /// <summary>
    /// Represents game card with suit and rank
    /// </summary>
    public class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }

        public Card(Suit suit, Rank rank)
        { 
            Suit = suit;
            Rank = rank;
        }

        /// <summary>
        /// Gets card's hash
        /// </summary>
        /// <returns>Card number</returns>
        public override int GetHashCode()
        {
            return (int)Suit * 13 + (int)Rank - 2;
        }

        /// <summary>
        /// Creates new card from hash
        /// </summary>
        /// <param name="hash">Card number</param>
        /// <returns>New card</returns>
        public static Card FromHashCode(int hash)
        {
            return new Card((Suit)(hash/13),(Rank)(hash%13+2));
        }
    }
}