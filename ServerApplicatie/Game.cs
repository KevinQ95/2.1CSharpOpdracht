
using PacketLibrary;
using System.Collections.Generic;

namespace ServerApplicatie
{
    public class Game
    {

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public List<CardPacket> Deck { get; set; }
        public List<CardPacket> Pile { get; set; }
        public int PlayerTurn { get; set; }

        public Game()
        {
            Player1 = new Player(1);
            Player2 = new Player(2);
            PlayerTurn = 1;

            Deck = new List<CardPacket>();
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        for (int j = 1; j < 14; j++)
                        {
                            if (j == 1)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.CLUBS, "Ace"));
                            else if (j == 11)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.CLUBS, "Jack"));
                            else if (j == 12)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.CLUBS, "Queen"));
                            else if (j == 13)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.CLUBS, "King"));
                            else
                                Deck.Add(new CardPacket(CardPacket.CardTypes.CLUBS, j.ToString()));
                        }
                        break;

                    case 1:
                        for (int j = 1; j < 14; j++)
                        {
                            if (j == 1)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.SPADES, "Ace"));
                            else if (j == 11)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.SPADES, "Jack"));
                            else if (j == 12)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.SPADES, "Queen"));
                            else if (j == 13)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.SPADES, "King"));
                            else
                                Deck.Add(new CardPacket(CardPacket.CardTypes.SPADES, j.ToString()));
                        }
                        break;
                    case 2:
                        for (int j = 1; j < 14; j++)
                        {
                            if (j == 1)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.HEARTS, "Ace"));
                            else if (j == 11)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.HEARTS, "Jack"));
                            else if (j == 12)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.HEARTS, "Queen"));
                            else if (j == 13)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.HEARTS, "King"));
                            else
                                Deck.Add(new CardPacket(CardPacket.CardTypes.HEARTS, j.ToString()));
                        }
                        break;
                    case 3:
                        for (int j = 1; j < 14; j++)
                        {
                            if (j == 1)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.DIAMONDS, "Ace"));
                            else if (j == 11)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.DIAMONDS, "Jack"));
                            else if (j == 12)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.DIAMONDS, "Queen"));
                            else if (j == 13)
                                Deck.Add(new CardPacket(CardPacket.CardTypes.DIAMONDS, "King"));
                            else
                                Deck.Add(new CardPacket(CardPacket.CardTypes.DIAMONDS, j.ToString()));
                        }
                        break;
                }
            }
            Deck.Add(new CardPacket(CardPacket.CardTypes.HEARTS, "Joker"));
            Deck.Add(new CardPacket(CardPacket.CardTypes.SPADES, "Joker"));

            Pile = new List<CardPacket>();
        }
    }
}