using System.Collections.Generic;
using NUnit.Framework;

namespace MIUP.GameName.CardModule.Domain.Tests
{
    [TestFixture]
    public class CardModelTests 
    {
        [TestCase(CardDefinitions.CardRanks.Joker, CardDefinitions.CardSuits.Black)]
        [TestCase(CardDefinitions.CardRanks.Joker, CardDefinitions.CardSuits.Red)]
        [TestCase(CardDefinitions.CardRanks.CA, CardDefinitions.CardSuits.Clubs)]
        [TestCase(CardDefinitions.CardRanks.C10, CardDefinitions.CardSuits.Diamonds)]
        [TestCase(CardDefinitions.CardRanks.CJ, CardDefinitions.CardSuits.Hearts)]
        [TestCase(CardDefinitions.CardRanks.C2, CardDefinitions.CardSuits.Spades)]
        public void Constructor_ValidParameters_SetRightValue(CardDefinitions.CardRanks rank, CardDefinitions.CardSuits suit) 
        {
            CardModel cardModel = new CardModel(rank, suit);

            Assert.AreEqual(cardModel.Rank, rank);
            Assert.AreEqual(cardModel.Suit, suit);
        }

        [TestCase(CardDefinitions.CardRanks.C2, CardDefinitions.CardSuits.Red)]
        [TestCase(CardDefinitions.CardRanks.CA, CardDefinitions.CardSuits.Black)]
        [TestCase(CardDefinitions.CardRanks.Joker, CardDefinitions.CardSuits.Clubs)]
        [TestCase(CardDefinitions.CardRanks.Joker, CardDefinitions.CardSuits.Diamonds)]
        [TestCase(CardDefinitions.CardRanks.Joker, CardDefinitions.CardSuits.Hearts)]
        [TestCase(CardDefinitions.CardRanks.Joker, CardDefinitions.CardSuits.Spades)]
        public void Constructor_WrongParameters_ThrowsRightException(CardDefinitions.CardRanks rank, CardDefinitions.CardSuits suit) 
        {
            Assert.Throws<System.ArgumentException>(() => new CardModel(rank, suit));
        }

        [TestCase(CardDefinitions.CardRanks.Joker, CardDefinitions.CardSuits.Black,
            CardDefinitions.CardRanks.Joker, CardDefinitions.CardSuits.Red, Result = true)]

        [TestCase(CardDefinitions.CardRanks.Joker, CardDefinitions.CardSuits.Black,
            CardDefinitions.CardRanks.C3, CardDefinitions.CardSuits.Clubs, Result = true)]

        [TestCase(CardDefinitions.CardRanks.Joker, CardDefinitions.CardSuits.Red,
            CardDefinitions.CardRanks.CA, CardDefinitions.CardSuits.Diamonds, Result = true)]

        [TestCase(CardDefinitions.CardRanks.CA, CardDefinitions.CardSuits.Diamonds,
            CardDefinitions.CardRanks.C2, CardDefinitions.CardSuits.Spades, Result = true)]

        [TestCase(CardDefinitions.CardRanks.CA, CardDefinitions.CardSuits.Diamonds,
            CardDefinitions.CardRanks.CK, CardDefinitions.CardSuits.Spades, Result = true)]

        [TestCase(CardDefinitions.CardRanks.C2, CardDefinitions.CardSuits.Diamonds,
            CardDefinitions.CardRanks.C3, CardDefinitions.CardSuits.Spades, Result = true)]

        [TestCase(CardDefinitions.CardRanks.C5, CardDefinitions.CardSuits.Diamonds,
            CardDefinitions.CardRanks.C6, CardDefinitions.CardSuits.Spades, Result = true)]

        [TestCase(CardDefinitions.CardRanks.CJ, CardDefinitions.CardSuits.Diamonds,
            CardDefinitions.CardRanks.C10, CardDefinitions.CardSuits.Spades, Result = true)]

        [TestCase(CardDefinitions.CardRanks.C10, CardDefinitions.CardSuits.Diamonds,
            CardDefinitions.CardRanks.C10, CardDefinitions.CardSuits.Spades, Result = false)]

        [TestCase(CardDefinitions.CardRanks.C5, CardDefinitions.CardSuits.Diamonds,
            CardDefinitions.CardRanks.C7, CardDefinitions.CardSuits.Spades, Result = false)]

        [TestCase(CardDefinitions.CardRanks.CK, CardDefinitions.CardSuits.Diamonds,
            CardDefinitions.CardRanks.C2, CardDefinitions.CardSuits.Spades, Result = false)]

        public bool CanMatch_WithOtherValidCard_ReturnsRightResult(CardDefinitions.CardRanks rank1, CardDefinitions.CardSuits suit1,
            CardDefinitions.CardRanks rank2, CardDefinitions.CardSuits suit2) 
        {
            CardModel cardModel1 = new CardModel(rank1, suit1);
            CardModel cardModel2 = new CardModel(rank2, suit2);

            Assert.AreEqual(cardModel1.CanMatch(cardModel2), cardModel2.CanMatch(cardModel1));
            return cardModel1.CanMatch(cardModel2);
        }
    }
}
