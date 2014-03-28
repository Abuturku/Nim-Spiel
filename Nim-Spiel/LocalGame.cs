using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nim_Spiel
{
    class LocalGame:Game
    {

        public LocalGame(int sticks, Player player1, Player player2)
        {
            if (sticks < 13)
            {
                this.Sticks = this.DefaultValue;
            }
            else
            {
                this.Sticks = sticks;
            }

            this.Player1 = player1;
            this.Player2 = player2;
        }

        public void Start()
        {
            this.ActivePlayer = DetermineRandomPlayer(this.Player1, this.Player2);
            Console.WriteLine("\nOk, los geht's! {0} fängt an!", this.ActivePlayer.Name);

            while (this.Sticks > 0)
            {
                try
                {
                    Console.WriteLine("{0}, wähle deine Zahl!", this.ActivePlayer.Name);

                    TakeStickOutOfGame(Convert.ToInt16(Console.ReadLine()), this.ActivePlayer);

                    if (this.Sticks > 0)
                    {
                        Console.WriteLine("\nVerbleibende Hölzchen: {0}", this.Sticks);
                    }
                    

                    if (this.ActivePlayer == this.Player1)
                    {
                        this.ActivePlayer = this.Player2;
                    }
                    else //if (this.ActivePlayer == this.Player2)
                    {
                        this.ActivePlayer = this.Player1;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Unzulässige Eingabe!");
                }

            }
        }
    }
}
