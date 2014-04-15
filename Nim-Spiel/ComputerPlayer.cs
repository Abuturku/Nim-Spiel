using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nim_Spiel
{
    class ComputerPlayer:Player
    {

        public string Difficulty { get; set; }

        public void DoATurn()
        {
            Random rnd = new Random();
            Game game = Program.game;
            int x = 0;
            int remainingSticks = game.Sticks;

            System.Threading.Thread.Sleep(1000);

            if (this.Difficulty.Equals("hard") || this.Difficulty.Equals("hardcore"))       //Algorithmus, mit dem KI immer gewinnt, solange sie nicht anfängt
            {
               
                //Console.WriteLine("'Hm, lass mich mal überlegen...'");
                

                if ((remainingSticks % 4) == 1)
                {
                    
                    x = rnd.Next(1, 4);

                }
                else if ((remainingSticks % 4) == 2)
                {
                    x = 1;
                }
                else if ((remainingSticks % 4) == 3)
                {
                    x = 2;
                }
                else if ((remainingSticks % 4) == 0)
                {
                    x = 3;
                }
            }
            else if (this.Difficulty.Equals("medium"))
            {
                //TODO Mittlere Schwierigkeit, d.h. dass zufällig entschieden wird, ob KI einen "guten" (also einen zum Sieg verhelfenden) Turn macht oder nicht; Zur Zeit noch einfache Schwierigkeit
                x = rnd.Next(1, 4);
            }
            else if (this.Difficulty.Equals("easy"))
            {
                x = rnd.Next(1, 4);
            }

            Console.SetCursorPosition(0, 6);
            Console.Write("\rComputer hat {0} Hölzchen gezogen\n\t\t\t\t\t\t\n\t\t\t\t\t\t\t\t\n\t\t\t\t\t\t\t", x);        //Feedback für den User
            //System.Threading.Thread.Sleep(1000);

            game.TakeStickOutOfGame(x, game.Player2);
        }

    }
}
