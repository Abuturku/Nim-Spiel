using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nim_Spiel
{
    abstract class Game
    {

        private int defaultValue = 13;
        private Player player1;
        private Player player2;
        private int sticks;
        private Player activePlayer;

        public int DefaultValue
        {
            get;
            private set;
        }

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
                    Console.WriteLine("Drücke eine beliebige Taste");
                    Console.ReadLine();
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

        public void InterruptTurn(Player currentPlayer)
        {

        }

        public void SaveHighscore(float score)
        {

        }

        

    }
}
