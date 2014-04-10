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
            ComputerPlayer cp = new ComputerPlayer();
            

            if (this.Player2.Name.Equals("Computer"))
            {
                cp = (ComputerPlayer)this.Player2;
                if (cp.Difficulty.Equals("hardcore"))
                {
                    timer = new System.Timers.Timer(2000);
                }
                else
                {
                    timer = new System.Timers.Timer(12500);
                }
            }     
            else
            {
                timer = new System.Timers.Timer(12500);
            }

            timer.Elapsed += new ElapsedEventHandler(InterruptTurnEvent);

            this.ActivePlayer = DetermineRandomPlayer(this.Player1, this.Player2);
            Console.WriteLine("Ok, los geht's! {0} fängt an!", this.ActivePlayer.Name);
            Thread.Sleep(2000);

            Console.Write("\nVerbleibende Hölzchen: {0}  \n", this.Sticks);
            

            while (this.Sticks > 0)
            {
                try
                {
                    Console.SetCursorPosition(0, 3);
                    Console.Write("\r{0}, wähle deine Zahl!\t\t\t\t\n  ", this.ActivePlayer.Name);

                    if (this.ActivePlayer.Name.Equals("Computer"))
                    {
                        Console.SetCursorPosition(0, 4);
                        cp.DoATurn();
                    }
                    else
                    {
                        timer.Start();
                        Console.SetCursorPosition(0, 4);
                        TakeStickOutOfGame(Convert.ToInt16(Console.ReadLine()), this.ActivePlayer);
                        Console.Write("\r\t\t\t\t\t\n\r\t\t\t\t\t\n\r\t\t\t\t\t");
                        timer.Stop();
                    }

                    //Console.WriteLine("\nVerbleibende Hölzchen: {0}", this.Sticks);

                    SwapActivePlayer();
                }
                catch (System.FormatException)
                {
                    if (this.Sticks > 0)
                    {
                        Console.Write("\rUnzulässige Eingabe!");
                    }
                }

            }
        }
    }
}
