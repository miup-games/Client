using UnityEngine;
using System.Collections;
using MIUP.GameName.CardModule.Domain;

namespace MIUP.GameName.CardModule.Application
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] bool randomCard = false;
        [SerializeField] CardDefinitions.CardSuits cardSuit;
        [SerializeField] CardDefinitions.CardRanks cardRank;
    }

}
