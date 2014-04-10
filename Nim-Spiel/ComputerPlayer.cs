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

            if (this.Difficulty.Equals("hard") || this.Difficulty.Equals("hardcore"))
            {
               
                //Console.WriteLine("'Hm, lass mich mal überlegen...'");
                //System.Threading.Thread.Sleep(1000);

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
                
            }
            else if (this.Difficulty.Equals("easy"))
            {
                x = rnd.Next(1, 4);
            }

            Console.SetCursorPosition(0, 6);
            Console.Write("\rComputer hat {0} Hölzchen gezogen", x);

            game.TakeStickOutOfGame(x, game.Player2);
        }

    }
}
