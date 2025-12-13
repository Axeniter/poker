using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Models
{
    public class Hand : IComparable<Hand>
    {
        public PokerCombination Combination { get; }
        public List<Card> CombinedCards { get; }
        public List<Card> Kickers { get; }

        public Hand(PokerCombination combination, List<Card> combinatedCards, List<Card>? kickers = null) 
        {
            if (combinatedCards == null) throw new ArgumentNullException();
            if (combinatedCards.Count + (kickers == null ? 0 : kickers.Count) != 5) 
                throw new ArgumentException();

            Combination = combination;
            CombinedCards = combinatedCards.OrderByDescending(c => c.Rank).ToList();
            Kickers = kickers == null ? new List<Card>() : kickers.OrderByDescending(c => c.Rank).ToList();
        }

        public int CompareTo(Hand? other)
        {
            if (other == null) throw new ArgumentNullException();

            if (Combination > other.Combination) return 1;
            if (Combination < other.Combination) return -1;

            switch(Combination)
            {
                case PokerCombination.StraightFlush:
                case PokerCombination.Straight:
                    return CompareStraight(other);
                case PokerCombination.FourOfKind:
                case PokerCombination.FullHouse:
                case PokerCombination.ThreeOfAKind:
                case PokerCombination.TwoPairs:
                case PokerCombination.Pair:
                    return CompareOfKind(other);
                case PokerCombination.Flush:
                case PokerCombination.HighCard:
                    return CompareSequentially(other);
                default:
                    throw new ArgumentException();
            }
        }
        private int CompareStraight(Hand other)
        {
            bool isWheel1 = IsWheel(this);
            bool isWheel2 = IsWheel(other);

            if (isWheel1 && isWheel2) return 0;
            if (isWheel1) return -1;
            if (isWheel2) return 1;

            if (CombinedCards[0].Rank > other.CombinedCards[0].Rank)
                return 1;
            if (CombinedCards[0].Rank < other.CombinedCards[0].Rank)
                return -1;
            return 0;
        }
        private static bool IsWheel(Hand hand)
        {
            var ranks = hand.CombinedCards.Select(c => c.Rank).OrderBy(r => r).ToList();
            return ranks.SequenceEqual(new List<CardRank> { CardRank.Two, CardRank.Three, CardRank.Four, CardRank.Five, CardRank.Ace });
        }

        private int CompareKickers(Hand other)
        {
            for (int i = 0; i < Kickers.Count; i++)
            {
                if (Kickers[i].Rank > other.Kickers[i].Rank) return 1;
                if (Kickers[i].Rank < other.Kickers[i].Rank) return -1;
            }
            return 0;
        }

        private int CompareOfKind(Hand other)
        {
            var groups1 = CombinedCards.GroupBy(c => c.Rank)
            .OrderByDescending(g => g.Count())
            .ThenByDescending(g => g.Key)
            .ToList();

            var groups2 = other.CombinedCards.GroupBy(c => c.Rank)
            .OrderByDescending(g => g.Count())
            .ThenByDescending(g => g.Key)
            .ToList();

            for (int i = 0; i < groups1.Count; i++)
            {
                if (groups1[i].Key > groups2[i].Key) return 1;
                if (groups1[i].Key < groups2[i].Key) return -1;
            }
            return CompareKickers(other);
        }

        private int CompareSequentially(Hand other)
        {
            for (int i = 0; i < CombinedCards.Count; i++)
            {
                if (CombinedCards[i].Rank > other.CombinedCards[i].Rank) return 1;
                if (CombinedCards[i].Rank < other.CombinedCards[i].Rank) return -1;
            }
            return CompareKickers(other);
        }
    }
}
