using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;

namespace Nim_Spiel
{
    class LocalGame:Game
    {

        public LocalGame(int sticks, Player player1, Player player2)
        {
            if (sticks < 13)
            {
                this.Sticks = this.defaultValue;
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

            timer = new System.Timers.Timer(30000);
            timer.Elapsed += new ElapsedEventHandler(InterruptTurnEvent);

            this.ActivePlayer = DetermineRandomPlayer(this.Player1, this.Player2);
            Console.WriteLine("\nOk, los geht's! {0} fängt an!", this.ActivePlayer.Name);
            Thread.Sleep(2000);

            Console.WriteLine("\nVerbleibende Hölzchen: {0}", this.Sticks);
            

            while (this.Sticks > 0)
            {
                try
                {
                    Console.WriteLine("{0}, wähle deine Zahl!", this.ActivePlayer.Name);

                    timer.Start();
                    TakeStickOutOfGame(Convert.ToInt16(Console.ReadLine()), this.ActivePlayer);

                    if (this.Sticks > 0)
                    {
                        
                        timer.Stop();
                    }


                    SwapActivePlayer();
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Unzulässige Eingabe!");
                }

            }
        }
    }
}
