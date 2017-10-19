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
        delegate void clearAll();
        delegate void stateCardsPanel(bool value);
        private GameState gameState;
        private int playerNumber;
        BinaryFormatter formatter;
        static TcpClient client;
        static int index = -1;

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
                UpdateBoardState();
                UpdatePlayerNumberLabel(gameState.Player.PlayerNumber.ToString());
                EnableCardsPanel(false);

                while (!closeConnection)
                {
                    while (playerNumber == gameState.PlayerTurn)
                    {
                        gameState.Player.Hand.ForEach(x => Console.WriteLine(x));
                        EnableCardsPanel(true);

                        if (gameState.Player.Hand.Count == 0)
                            Console.WriteLine("You win");
                        else
                        {
                            if (gameState.Player.FindPlayableCardInHand(gameState.Pile[gameState.Pile.Count - 1]))
                            {
                                Console.WriteLine("Its your turn opponent has " + gameState.CardsAtOtherPlayer + " cards in hand " +
                               "\nCard on pile is " + gameState.Pile[gameState.Pile.Count - 1]);

                                while (index == -1)
                                {

                                }

                                SharedMethods.SendPacket(client, gameState.Player.Hand[index]);
                                gameState = formatter.Deserialize(client.GetStream()) as GameState;
                                UpdateBoardState();
                                index = -1;
                                EnableCardsPanel(false);
                            }
                            else
                            {
                                Console.WriteLine("Its your turn opponent has " + gameState.CardsAtOtherPlayer + " cards in hand " +
                               "\nCard on pile is " + gameState.Pile[gameState.Pile.Count - 1] + "\nYou dont have a playable card");
                                SharedMethods.SendPacket(client, new DrawCardPacket());
                                gameState = formatter.Deserialize(client.GetStream()) as GameState;
                                UpdateBoardState();
                                index = -1;
                                EnableCardsPanel(false);
                            }
                        }
                    }
                    gameState = formatter.Deserialize(client.GetStream()) as GameState;
                    UpdateBoardState();
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

        public void UpdatePlayerNumberLabel(string text)
        {
            if (_playerNumber.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdatePlayerNumberLabel);
                Invoke(d, new object[] { text });
            }
            else
                _playerNumber.Text = "Player : " + text;

        }

        public void EnableCardsPanel(bool value)
        {
            if (CardsPanel.InvokeRequired)
            {
                stateCardsPanel d = new stateCardsPanel(EnableCardsPanel);
                Invoke(d, new object[] { value });
            }
            else
                CardsPanel.Enabled = value;
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

        public void ClearEverything()
        {
            if (CardsPanel.InvokeRequired)
            {
                clearAll d = new clearAll(ClearEverything);
                Invoke(d, new object[] { });
            }
            else
                CardsPanel.Controls.Clear();

            if (OpponentCardsPanel.InvokeRequired)
            {
                clearAll d = new clearAll(ClearEverything);
                Invoke(d, new object[] { });
            }
            else
                OpponentCardsPanel.Controls.Clear();

            if (_Label1.InvokeRequired)
            {
                clearAll d = new clearAll(ClearEverything);
                Invoke(d, new object[] { });
            }
            else
                _Label1.Controls.Clear();

            if (TopStack.InvokeRequired)
            {
                clearAll d = new clearAll(ClearEverything);
                Invoke(d, new object[] { });
            }
            else
                TopStack.Controls.Clear();

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


        public void UpdateTopCard(PictureBox pb)
        {
            if (TopStack.InvokeRequired)
            {
                updateStackTop d = new updateStackTop(UpdateTopCard);
                Invoke(d, new object[] { pb });
            }
            else
                TopStack.Controls.Add(pb);
        }

        public static void SendCardToServer(int i)
        {
            index = i;
        }

        public void UpdateBoardState()
        {
            ClearEverything();
            Image image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Cards\cardback.png"));

            for (int i = 0; i < gameState.CardsAtOtherPlayer; i++)
            {
                PictureBox pbhand = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(67, 100),
                    Image = image
                };


                UpdateOpponentCardsPanel(pbhand);
            }

            for (int i = 0; i < gameState.Player.Hand.Count; i++)
            {
                CustomButton cb = new CustomButton
                {
                    Index = i
                };

                var bm = new Bitmap(Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Cards\" + gameState.Player.Hand[i].ToString() + ".png")), new Size(cb.Width, cb.Height));
                cb.BackgroundImage = bm;
                UpdateCardsPanel(cb);
            }

            PictureBox pbStack = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(67, 100)
            };

            Image stacktop = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Cards\" + gameState.Pile[gameState.Pile.Count - 1] + ".png"));
            pbStack.Image = stacktop;
            UpdateTopCard(pbStack);
            UpdateDeckAmount(gameState.Deck.Count);
        }
    }
}
