using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedProjectServerClient;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using PacketLibrary;

namespace ClientApplicatie
{
    public class Client
    {
        private GameState gameState;
        private int playerNumber;

        static void Main(string[] args)
        {
            new Client();
        }
        

        public Client()
        {
            gameState = new GameState();
            BinaryFormatter formatter = new BinaryFormatter();
            TcpClient client = new TcpClient("127.0.0.1", 1234);
            bool closeConnection = false;

            gameState = formatter.Deserialize(client.GetStream()) as GameState;
            playerNumber = gameState.Player.PlayerNumber;
            //gameState.Player.Hand.ForEach(x => Console.WriteLine(x));

            while (!closeConnection)
            {
                while (playerNumber == gameState.PlayerTurn)
                {
                    gameState.Player.Hand.ForEach(x => Console.WriteLine(x));

                    if (gameState.Player.Hand.Count == 0)
                        Console.WriteLine("You win");
                    else
                    {
                        if (gameState.Player.FindPlayableCardInHand(gameState.Pile[gameState.Pile.Count - 1]))
                        {
                            Console.WriteLine("Its your turn opponent has " + gameState.CardsAtOtherPlayer + " cards in hand " +
                           "\nCard on pile is " + gameState.Pile[gameState.Pile.Count - 1]);
                            string response = Console.ReadLine();
                            int handnumber = Int32.Parse(response);
                            SharedMethods.SendPacket(client, gameState.Player.Hand[handnumber]);
                            gameState = formatter.Deserialize(client.GetStream()) as GameState;

                        }
                        else
                        {
                            Console.WriteLine("Its your turn opponent has " + gameState.CardsAtOtherPlayer + " cards in hand " +
                           "\nCard on pile is " + gameState.Pile[gameState.Pile.Count - 1] + "\nYou dont have a playable card");
                            SharedMethods.SendPacket(client, new DrawCardPacket());
                            gameState = formatter.Deserialize(client.GetStream()) as GameState;

                        }
                    }
                }
                gameState = formatter.Deserialize(client.GetStream()) as GameState;
            }

        }
    }
}
