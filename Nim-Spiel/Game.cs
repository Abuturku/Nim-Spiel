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


                if (this.sticks <= 0 )
                {
                    
                    Console.WriteLine("\n{0}, du ziehst das letzte Hölzchen, du hast leider verloren!\n", this.ActivePlayer.Name);
                    timer.Stop();
                    
                }
            }
            else
            {
                Console.WriteLine("\nUnzulässige Anzahl, bitte nochmal versuchen\n");
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
            Console.WriteLine("Du hast leider zu lange gebraucht, {0}\n", this.ActivePlayer.Name);
            timer.Stop();

            SwapActivePlayer();

            Console.WriteLine("{0}, du bist dran.\nWähle deine Zahl", this.ActivePlayer.Name);
            timer.Start();
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


        public void SaveHighscore(float score)
        {

        }

        public Player DetermineRandomPlayer(Player player1, Player player2)
        {
            Player determinedPlayer = player1;

            Random rnd = new Random();
            int x = rnd.Next(100);

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
