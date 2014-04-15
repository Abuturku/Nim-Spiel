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
                    timer = new System.Timers.Timer(2000);      //Zeit im Hardcore-Modus: 2 Sek
                }
                else
                {
                    timer = new System.Timers.Timer(12500);
                }
            }     
            else
            {
                timer = new System.Timers.Timer(12500);         //Sonst 12,5 Sekunden (Warum gerade 12,5? Weil wir's einfach können.)
            }

            timer.Elapsed += new ElapsedEventHandler(InterruptTurnEvent);

            this.ActivePlayer = DetermineRandomPlayer(this.Player1, this.Player2);
            Console.WriteLine("Ok, los geht's! {0} fängt an!", this.ActivePlayer.Name);
            Thread.Sleep(2000);

            Console.Write("\nVerbleibende Hölzchen: {0}  \n", this.Sticks);
            

            while (this.Sticks > 0)     //Solange noch Hölzchen im Spiel sind,
            {
                try
                {
                    Console.SetCursorPosition(0, 3);
                    Console.Write("\r{0}, wähle deine Zahl!\t\t\t\t\t\t\n", this.ActivePlayer.Name);

                    if (this.ActivePlayer.Name.Equals("Computer"))
                    {
                        Console.SetCursorPosition(0, 4);
                        cp.DoATurn();
                    }
                    else
                    {
                        timer.Start();
                        Console.SetCursorPosition(0, 4);
                        TakeStickOutOfGame(Convert.ToInt16(Console.ReadLine()), this.ActivePlayer);     //Spieler eine Anzahl von Hölzchen ziehen lassen
                        Console.Write("\r\t\t\t\t\t\n\r\t\t\t\t\t\n\r\t\t\t\t\t");
                        timer.Stop();
                    }

                    //Console.WriteLine("\nVerbleibende Hölzchen: {0}", this.Sticks);

                    SwapActivePlayer();         // und Gegenspieler ziehen lassen
                }
                catch (System.FormatException)      //Für den Fall, dass nur Eingabe gedrückt wurde; Merke: kein timer.Stop(), 
                {                                   //weil Spieler selbst für seine Zeit verantwortlich ist, man könnte sich ja sonst Zeit "erkaufen"
                    if (this.Sticks > 0)
                    {
                        Console.Write("\rUnzulässige Eingabe!");
                    }
                }

            }
        }
    }
}
