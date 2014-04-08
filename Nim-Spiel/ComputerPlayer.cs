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
            int x = 0;
            int remainingSticks = game.Sticks;

            if ((remainingSticks % 4) == 1)
            {
                Random rnd = new Random();
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

            game.TakeStickOutOfGame(x, game.Player2);
        }

    }
}
