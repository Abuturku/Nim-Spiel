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
            Game game = Program.game;
            int x;
            int remainingSticks = game.Sticks;
            if ((game.Sticks % 4) == 1)
            {
                Random rnd = new Random();
                x = rnd.Next(1, 4);

                game.TakeStickOutOfGame(x, game.Player2);
            }
            else
            {
                if (true)
                {
                    
                }
            }
        }

    }
}
