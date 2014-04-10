using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Nim_Spiel
{
    abstract class Game
    {

        public int defaultValue = 13;
        private int sticks;

        public Timer timer;


        public Player Player1
        {
            get;
            set;
        }

        public Player Player2
        {
            get;
            set;
        }

        public Player ActivePlayer
        {
            get;
            set;
        }

        public int Sticks
        {
            get { return sticks; }
            set { sticks = value; }
        }

        public void TakeStickOutOfGame(int amount, Player player)
        {
            if (amount >=1 && amount <= 3)
            {
                this.Sticks = this.Sticks - amount;

                if (this.Sticks <= 0 )
                {
                    this.Sticks = 0;
                    Console.SetCursorPosition(0, 2);
                    Console.Write("\rVerbleibende Hölzchen: {0}\n", this.Sticks);
                    Console.SetCursorPosition(0, 8);
                    Console.Write("\n\n\r{0}, du ziehst das letzte Hölzchen, du hast leider verloren!\n", this.ActivePlayer.Name);
                    timer.Stop();
                    SaveHighscores(this.ActivePlayer.Name, 0, 1);       //Einmal Highscore speichern für den Verlierer
                    SwapActivePlayer();
                    SaveHighscores(this.ActivePlayer.Name, 1, 0);       //Und den Gewinner
                    
                }
                else
                {
                    Console.SetCursorPosition(0, 2);
                    Console.Write("\rVerbleibende Hölzchen: {0}  \n", this.Sticks);
                    //Console.SetCursorPosition(0, 5);
                    ////Console.Write("\r\t\t\t\t\t\n\r\t\t\t\t\t\n\r\t\t\t\t\t");
                    timer.Stop();
                }              
            }
            else
            {
                Console.Write("\rUnzulässige Anzahl, bitte nochmal versuchen");
                amount = Convert.ToInt16(Console.ReadLine());
                this.TakeStickOutOfGame(amount, player);
            }   
        }

        public bool CheckTimeLimit(Player currentPlayer) 
        {
            return false;
        }

        public void InterruptTurnEvent(object source, ElapsedEventArgs e)
        {
            timer.Stop();

            int random;
            Random rnd = new Random();
            random = rnd.Next(1, 4);

            Console.Write("\n\n\rDu hast leider zu lange gebraucht, {0}.\nDu ziehst automatisch {1} Hölzchen.  \n", this.ActivePlayer.Name, random);


            TakeStickOutOfGame(random, this.ActivePlayer);
            
            if (this.Sticks > 0)
            {
                SwapActivePlayer();

                if (this.ActivePlayer.Name.Equals("Computer"))
                {
                    ComputerPlayer cp = (ComputerPlayer)this.Player2;
                    Console.SetCursorPosition(0, 3);
                    Console.Write("\r{0}, wähle deine Zahl!  ", this.ActivePlayer.Name);
                    cp.DoATurn();
                    SwapActivePlayer();
                    Console.SetCursorPosition(0, 3);
                    Console.Write("\r{0}, wähle deine Zahl!  ", this.ActivePlayer.Name);
                    timer.Start();
                }
                else
                {
                    Console.SetCursorPosition(0, 3);

                    Console.Write("\r{0}, wähle deine Zahl." , this.ActivePlayer.Name);
                    //Console.Write("\r{0}, wähle deine Zahl.  \r\t\t\t\t\n\r\t\t\t\t\t\n\r\t\t\t\t\t\n\r\t\t\t\t\t\n", this.ActivePlayer.Name);
                    Console.SetCursorPosition(0, 4);
                    timer.Start();
                }

            }
            else
            {
                Console.WriteLine("Drücke eine beliebige Taste.");
            }

        }

        
        public void SwapActivePlayer()
        {
            if (this.ActivePlayer == this.Player1)
            {
                this.ActivePlayer = this.Player2;
            }
            else //if (this.ActivePlayer == this.Player2)
            {
                this.ActivePlayer = this.Player1;
            }
        }


        public void SaveHighscores(string player, int wins, int losses)
        {
            Highscore hs = new Highscore();
            hs = hs.GetHighscores();
            //hs.SaveHighscores();
            

            if (hs.Players.Count == 0)
            {
                hs.AddNewHighscore(player, wins, losses);
            }
            else
            {
                for (int k = 0; k <= hs.Players.Count; k++)
                {
                    try
                    {
                        if (player.Equals(hs.Players[k]))   //Durchsuchen der Liste nach dem Namen, falls vorhanden werden Wins und Losses aktualisiert
                        {
                            hs.UpdateHighscore(player, wins, losses);
                            break;
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        hs.AddNewHighscore(player, wins, losses);
                    }
                    
                    //if (k == hs.Players.Count)         //wenn der Name nicht in der Liste ist, dann neuen Name anlegen mit Wins und Losses
                    //{
                        
                    //}
                }
            }

            hs.SaveHighscores();
        }


        public Player DetermineRandomPlayer(Player player1, Player player2)
        {
            Player determinedPlayer = player1;

            Random rnd = new Random();
            int x = rnd.Next(101);

            if (x < 50)
            {
                
            }
            else
            {
                determinedPlayer = player2;
            }

            return determinedPlayer;
        }

    }
}
