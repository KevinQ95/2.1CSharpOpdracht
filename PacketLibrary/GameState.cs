using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketLibrary
{
    [Serializable]
    public class GameState : Packet
    {
        public Player Player { get; set; }
        public int CardsAtOtherPlayer { get; set; }
        public int PlayerTurn { get; set; }
        public List<CardPacket> Deck { get; set; }
        public List<CardPacket> Pile { get; set; }

        public GameState(Player player, int cardsAtOther, int playerTurn, List<CardPacket> deck, List<CardPacket> pile) : base()
        {
            PacketName = "GameState";
            Player = player;
            CardsAtOtherPlayer = cardsAtOther;
            PlayerTurn = playerTurn;
            Deck = deck;
            Pile = pile;
        }

        public GameState() : base()
        {
            PacketName = "GameState";
            Player = new Player(0);
            CardsAtOtherPlayer = 0;
            PlayerTurn = 0;
            Deck = new List<CardPacket>();
            Pile = new List<CardPacket>();
        }
    }
}
