namespace MIUP.GameName.CardModule.Domain {
	public class CardDefinitions {
		#region ENUMS
        /// <summary>
        /// Card suits.
        /// </summary>
		public enum CardSuits {
			Hearts,
			Spades,
			Diamonds,
			Clubs,
			Black,
			Red
		}

        /// <summary>
        /// Card ranks.
        /// </summary>
		public enum CardRanks {
			C2,
			C3,
			C4,
			C5,
			C6,
			C7,
			C8,
			C9,
			C10,
			CJ,
			CQ,
			CK,
			CA,
			Joker
		}
		#endregion

        #region Variables
        /// <summary>
        /// The rank count.
        /// </summary>
        public const int RankCount = 13;
        #endregion
	}
}
