namespace Poker.Models
{
    /// <summary>
    /// Represents game card with suit and rank
    /// </summary>
    public class Card
    {
        public CardSuit Suit { get; }
        public CardRank Rank { get; }

        public Card(CardSuit suit, CardRank rank)
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
            return new Card((CardSuit)(hash/13),(CardRank)(hash%13+2));
        }
    }
}