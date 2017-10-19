using ClientApplicatie;
using PacketLibrary;
using SharedProjectServerClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace ClientForm
{

    public partial class Form1 : Form
    {
        delegate void SetTextCallback(string text);
        delegate void updateCard(CustomButton cb);
        delegate void updateOppentCard(PictureBox cb);
        delegate void updateStackTop(PictureBox cb);
        delegate void updateDeckLeft(int amount);
        private GameState gameState;
        private int playerNumber;
        BinaryFormatter formatter;
        TcpClient client;

        public Form1()
        {
            InitializeComponent();
            Show();

            new Thread(() =>
            {
                gameState = new GameState();
                formatter = new BinaryFormatter();
                client = new TcpClient("127.0.0.1", 1234);
                bool closeConnection = false;

                gameState = formatter.Deserialize(client.GetStream()) as GameState;
                playerNumber = gameState.Player.PlayerNumber;
                //gameState.Player.Hand.ForEach(x => Console.WriteLine(x));
                string hand = "";
                gameState.Player.Hand.ForEach(x => hand += x.ToString() + " ");
                UpdateLabel1(hand);
                UpdateLabel2(gameState.CardsAtOtherPlayer.ToString());
                UpdateBoardState();
                Thread refreshGameState = new Thread(UpdateGameState);
                refreshGameState.Start();



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
                   // gameState = formatter.Deserialize(client.GetStream()) as GameState;
                }
            }).Start();
          
        }



        public void UpdateGameState()
        {
            gameState = formatter.Deserialize(client.GetStream()) as GameState;
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

        public void UpdateCardsPanel(CustomButton cb)
        {
            if (CardsPanel.InvokeRequired)
            {
                updateCard d = new updateCard(UpdateCardsPanel);
                Invoke(d, new object[] { cb });
            }
            else
                CardsPanel.Controls.Add(cb);
        }



        public void UpdateOpponentCardsPanel(PictureBox pb)
        {
            if (OpponentCardsPanel.InvokeRequired)
            {
                updateOppentCard d = new updateOppentCard(UpdateOpponentCardsPanel);
                Invoke(d, new object[] { pb });
            }
            else
                OpponentCardsPanel.Controls.Add(pb);
        }

        public void UpdateDeckAmount(int amount)
        {
            if (_Label1.InvokeRequired)
            {
                updateDeckLeft d = new updateDeckLeft(UpdateDeckAmount);
                Invoke(d, new object[] { amount });
            }
            else { _Label1.Text = amount + " Cards left"; }
        }
        

        public void updateTopCard(PictureBox pb)
        {
            if (TopStack.InvokeRequired)
            {
                updateStackTop d = new updateStackTop(updateTopCard);
                Invoke(d, new object[] { pb });
            }
            else
                TopStack.Controls.Add(pb);
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

        public void UpdateBoardState()
        {
            //OpponentCardsPanel.Controls.Clear();
            Image image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Cards\cardback.png"));
            for (int i = 0; i < gameState.CardsAtOtherPlayer; i++)
            {
                PictureBox pbhand = new PictureBox();
                pbhand.SizeMode = PictureBoxSizeMode.Zoom;
                pbhand.Size = new Size(CustomButton.size.Width, CustomButton.size.Height);

                pbhand.Image = image;


                UpdateOpponentCardsPanel(pbhand);
            }

            // CardsPanel.Controls.Clear();
            for (int i = 0; i < gameState.Player.Hand.Count; i++)
            {
                CustomButton cb = new CustomButton();
                cb.index = i;
                var bm = new Bitmap(Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Cards\" + gameState.Player.Hand[i].ToString() + ".png")), new Size(cb.Width, cb.Height));

                //b.Size = 
                cb.BackgroundImage = bm;
                //cb.Location = new Point(i * 100, 0);
                UpdateCardsPanel(cb);
            }

            PictureBox pbStack = new PictureBox();
            pbStack.SizeMode = PictureBoxSizeMode.Zoom;
            pbStack.Size = new Size(CustomButton.size.Width, CustomButton.size.Height);
            Image stacktop = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Cards\" + gameState.Pile[gameState.Pile.Count-1] + ".png"));
            pbStack.Image = stacktop;
            updateTopCard(pbStack);
            UpdateDeckAmount(gameState.Deck.Count);
        }
    }
}
