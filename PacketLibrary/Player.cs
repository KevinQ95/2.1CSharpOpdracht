using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketLibrary
{
    [Serializable]
    public class Player : Packet
    {
        public List<CardPacket> Hand { get; set; }
        public int PlayerNumber { get; set; }

        public Player(int playerNum) : base()
        {
            PacketName = "Player";
            Hand = new List<CardPacket>();
            PlayerNumber = playerNum;
        }

        public void RemoveCardFromHand(CardPacket card)
        {
            foreach (var cardInHand in Hand)
            {
                if ((cardInHand.CardType == card.CardType) && (cardInHand.CardValue == card.CardValue))
                {
                    Hand.Remove(cardInHand);
                    break;
                }

            }
        }

        public bool FindPlayableCardInHand(CardPacket card)
        {
            foreach (var cardInHand in Hand)
            {
                if (cardInHand.CardType == card.CardType)
                    return true;
                else if (cardInHand.CardValue == card.CardValue)
                    return true;
            }

            return false;
        }

        public bool FindValueInHand(string cardValue)
        {
            foreach (var cardInHand in Hand)
            {
                if (cardInHand.CardValue == cardValue)
                    return true;
            }
            return false;
        }
    }
}
