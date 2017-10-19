using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketLibrary
{
    [Serializable]
    public class CardPacket : Packet
    {
        public enum CardTypes { CLUBS, SPADES, HEARTS, DIAMONDS };

        public CardTypes CardType { get; set; }
        public string CardValue { get; set; }

        public CardPacket() : base()
        {
            PacketName = "CardPacket";
            CardType = CardTypes.CLUBS;
            CardValue = "Fake";
        }

        public CardPacket(CardTypes cardType, string cardValue) : base()
        {
            PacketName = "CardPacket";
            CardType = cardType;
            CardValue = cardValue;
        }

        public override string ToString()
        {
            return CardValue.ToLower() + "_of_" + CardType.ToString().ToLower();
        }
    }
}
