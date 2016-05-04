using System.Collections.Generic;

namespace MIUP.GameName.Domain.CardModule 
{
	public class CardModel
	{
        #region PROPERTIES
        /// <summary>
        /// Gets the rank.
        /// </summary>
        /// <value>The rank.</value>
		public CardDefinitions.CardRanks Rank { get; private set; }

        /// <summary>
        /// Gets the suit.
        /// </summary>
        /// <value>The suit.</value>
        public CardDefinitions.CardSuits Suit { get; private set; }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Initializes a new instance of the <see cref="MIUP.GameName.Domain.CardModule.CardModel"/> class.
        /// </summary>
        /// <param name="rank">Rank.</param>
        /// <param name="suit">Suit.</param>
        public CardModel(CardDefinitions.CardRanks rank, CardDefinitions.CardSuits suit)
        {
            if( (rank == CardDefinitions.CardRanks.Joker && (suit != CardDefinitions.CardSuits.Black && suit != CardDefinitions.CardSuits.Red)) ||
                (rank != CardDefinitions.CardRanks.Joker && (suit == CardDefinitions.CardSuits.Black || suit == CardDefinitions.CardSuits.Red)) )
            {
                throw new System.ArgumentException(string.Format("Inconcistent rank {0} and suit {1}", rank, suit));
            }

            this.Rank = rank;
            this.Suit = suit;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="MIUP.GameName.Domain.CardModule.CardModel"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="MIUP.GameName.Domain.CardModule.CardModel"/>.</returns>
        public override string ToString() {
            return string.Format("{0} of {1}", 
                this.Rank == CardDefinitions.CardRanks.Joker ? "Jok" : this.Rank.ToString(),
                this.Suit.ToString());
        }

        /// <summary>
        /// Determines whether this instance can match the specified otherCard.
        /// </summary>
        /// <returns><c>true</c> if this instance can match the specified otherCard; otherwise, <c>false</c>.</returns>
        /// <param name="otherCard">Other card.</param>
        public bool CanMatch(CardModel otherCard) {
            //Joker case
            if(this.Rank == CardDefinitions.CardRanks.Joker || otherCard.Rank == CardDefinitions.CardRanks.Joker) {
                return true;
            }

            //Case A and 2
            int dif = UnityEngine.Mathf.Abs(this.GetRankIndex() - otherCard.GetRankIndex());
            if(dif == (CardDefinitions.RankCount - 1)) {
                return true;
            }

            //Other cases
            return dif == 1;
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Gets the index of the rank.
        /// </summary>
        /// <returns>The rank index.</returns>
        private int GetRankIndex() {
            return (int)this.Rank;
        }
        #endregion
	}
}