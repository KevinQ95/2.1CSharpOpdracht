using ClientApplicatie;
using PacketLibrary;
using SharedProjectServerClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForm
{

    public partial class Form1 : Form
    {
        delegate void SetTextCallback(string text);
        private GameState gameState;
        private int playerNumber;

        public Form1()
        {
            InitializeComponent();
            Show();

            new Thread(() =>
            {
                gameState = new GameState();
                BinaryFormatter formatter = new BinaryFormatter();
                TcpClient client = new TcpClient("127.0.0.1", 1234);
                bool closeConnection = false;

                gameState = formatter.Deserialize(client.GetStream()) as GameState;
                playerNumber = gameState.Player.PlayerNumber;
                //gameState.Player.Hand.ForEach(x => Console.WriteLine(x));
                string hand = "";
                gameState.Player.Hand.ForEach(x => hand += x.ToString() + " ");
                UpdateLabel1(hand);
                UpdateLabel2(gameState.CardsAtOtherPlayer.ToString());

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
                                //string response = Console.ReadLine();
                                //int handnumber = Int32.Parse(response);
                                //SharedMethods.SendPacket(client, gameState.Player.Hand[handnumber]);

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
            }).Start();
          
        }

        public void UpdateLabel1(string text)
        {
            if (_Label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateLabel1);
                Invoke(d, new object[] { text });
            }
            else
                _Label1.Text = $"{text} of cards in hand";
        }


        public void UpdateLabel2(string text)
        {
            if (_Label2.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateLabel2);
                Invoke(d, new object[] { text });
            }
            else
                _Label2.Text = $"{text} of cards in hand";
        }
    }
}
