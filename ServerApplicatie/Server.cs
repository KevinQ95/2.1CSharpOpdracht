using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharedProjectServerClient;
using System.Runtime.Serialization.Formatters.Binary;
using PacketLibrary;
using System.Security.Cryptography;

namespace ServerApplicatie
{
    public class Server
    {
        public delegate void ParameterizedThreadStart(Object o);

        public BinaryFormatter serializer;

        public Game game;

        public int cardsToDraw = 0;


        static void Main(string[] args)
        {
            new Server();
        }

        public Server()
        {
            serializer = new BinaryFormatter();
            IPAddress localhost = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(localhost, 1234);

            listener.Start();

            while (true)
            {
                Console.WriteLine("Waiting for client");

                TcpClient client1 = listener.AcceptTcpClient();
                TcpClient client2 = listener.AcceptTcpClient();

                new Thread(() => HandleAGameThread(client1, client2)).Start();

            }

        }

        public void HandleClientThread(Object o)
        {
            TcpClient client = o as TcpClient;

            bool closeClient = false;
            while (!closeClient)
            {
                CardPacket cardPacket = serializer.Deserialize(client.GetStream()) as CardPacket;
                Console.WriteLine(cardPacket);
            }
            client.Close();
            Console.WriteLine("Connection closed");
        }

        public void HandleAGameThread(Object o1, Object o2)
        {
            TcpClient client1 = o1 as TcpClient;
            TcpClient client2 = o2 as TcpClient;
            SetupGame(client1, client2);

            while (true)
            {
                Packet packet = new Packet();
                if (game.PlayerTurn == 1)
                    packet = serializer.Deserialize(client1.GetStream()) as Packet;
                else
                    packet = serializer.Deserialize(client2.GetStream()) as Packet;

                Console.WriteLine("Received " + packet.PacketName);

                bool anotherTurn = false;

                switch (packet.PacketName)
                {
                    case "CardPacket":
                        CardPacket cardPacket = packet as CardPacket;
                        switch (cardPacket.CardValue)
                        {
                            case "2":
                                if ((game.Pile[game.Pile.Count - 1].CardValue == "2" || game.Pile[game.Pile.Count - 1].CardValue == "Joker") && game.Pile.Count > 1)
                                    cardsToDraw += 2;
                                else
                                    cardsToDraw = 2;
                                break;
                            case "7":
                                anotherTurn = true;
                                break;
                            case "8":
                                anotherTurn = true;
                                break;
                            case "Jack":
                                break;
                            case "King":
                                anotherTurn = true;
                                break;
                            case "Joker":
                                if (game.Pile[game.Pile.Count - 1].CardValue == "2" || game.Pile[game.Pile.Count - 1].CardValue == "Joker")
                                    cardsToDraw += 5;
                                else
                                    cardsToDraw = 5;
                                break;
                        }

                        Console.WriteLine(game.PlayerTurn);

                        if (game.PlayerTurn == 1)
                        {
                            if (!anotherTurn)
                                game.PlayerTurn = 2;
                            game.Player1.RemoveCardFromHand(cardPacket);
                            game.Pile.Add(cardPacket);

                            if (!(game.Player2.FindValueInHand("2") || game.Player2.FindValueInHand("Joker")))
                            {
                                game.Player2.Hand.AddRange(game.Deck.GetRange(0, cardsToDraw));
                                game.Deck.RemoveRange(0, cardsToDraw);
                                cardsToDraw = 0;
                            }

                            SharedMethods.SendPacket(client1, new GameState(game.Player1, game.Player2.Hand.Count, game.PlayerTurn, game.Deck, game.Pile));
                            SharedMethods.SendPacket(client2, new GameState(game.Player2, game.Player1.Hand.Count, game.PlayerTurn, game.Deck, game.Pile));
                        }
                        else
                        {
                            if (!anotherTurn)
                                game.PlayerTurn = 1;
                            game.Player2.RemoveCardFromHand(cardPacket);
                            game.Pile.Add(cardPacket);

                            if (!(game.Player1.FindValueInHand("2") || game.Player1.FindValueInHand("Joker")))
                            {
                                game.Player1.Hand.AddRange(game.Deck.GetRange(0, cardsToDraw));
                                game.Deck.RemoveRange(0, cardsToDraw);
                                cardsToDraw = 0;
                            }

                            SharedMethods.SendPacket(client1, new GameState(game.Player1, game.Player2.Hand.Count, game.PlayerTurn, game.Deck, game.Pile));
                            SharedMethods.SendPacket(client2, new GameState(game.Player2, game.Player1.Hand.Count, game.PlayerTurn, game.Deck, game.Pile));
                        }


                        break;
                    case "DrawCardPacket":
                        DrawCardPacket drawCard = packet as DrawCardPacket;
                        if (game.PlayerTurn == 1)
                        {
                            game.PlayerTurn = 1;
                            game.Player1.Hand.Add(game.Deck[0]);
                            game.Deck.Remove(game.Deck[0]);
                            SharedMethods.SendPacket(client1, new GameState(game.Player1, game.Player2.Hand.Count, game.PlayerTurn, game.Deck, game.Pile));
                            SharedMethods.SendPacket(client2, new GameState(game.Player2, game.Player1.Hand.Count, game.PlayerTurn, game.Deck, game.Pile));
                        }
                        else
                        {
                            game.PlayerTurn = 2;
                            game.Player2.Hand.Add(game.Deck[0]);
                            game.Deck.Remove(game.Deck[0]);
                            SharedMethods.SendPacket(client1, new GameState(game.Player1, game.Player2.Hand.Count, game.PlayerTurn, game.Deck, game.Pile));
                            SharedMethods.SendPacket(client2, new GameState(game.Player2, game.Player1.Hand.Count, game.PlayerTurn, game.Deck, game.Pile));
                        }
                        break;
                }
            }
        }

        private void SetupGame(TcpClient o1, TcpClient o2)
        {
            game = new Game();

            game.Deck.Shuffle();

            for (int i = 0; i < 7; i++)
            {
                game.Player1.Hand.Add(game.Deck[i]);
            }

            game.Deck.RemoveRange(0, 7);

            for (int i = 0; i < 7; i++)
            {
                game.Player2.Hand.Add(game.Deck[i]);
            }

            game.Deck.RemoveRange(0, 7);

            game.Pile.Add(game.Deck[0]);
            game.Deck.Remove(game.Deck[0]);

            SharedMethods.SendPacket(o1, new GameState(game.Player1, game.Player2.Hand.Count, game.PlayerTurn, game.Deck, game.Pile));
            SharedMethods.SendPacket(o2, new GameState(game.Player2, game.Player1.Hand.Count, game.PlayerTurn, game.Deck, game.Pile));
        }

    }












    static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

