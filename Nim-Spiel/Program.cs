using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nim_Spiel
{
    class Program
    {
        static string input;
        static string GameMode;
        static LocalPlayer lPlayer1;
        static LocalPlayer lPlayer2;
        static NetworkPlayer nPlayer1;
        static LocalGame game;
        static string playAgain = "y";

        static void Main(string[] args)
        {
            while (playAgain.Equals("y") || playAgain.Equals("Y"))
            {
                Console.Clear();
                displayMainMenu();

                if (GameMode.Equals("local"))
                {
                    Console.WriteLine("\nLokales Spiel gewählt\n");

                    //Console.ReadKey();
                    //Console.WriteLine();
                    setSettingsLocalGame();
                    game.Start();
                    Console.WriteLine("Nochmal spielen? Gebe 'y' (Ja) oder 'n' (Nein) ein");
                    input = Console.ReadLine();

                    if (input.Equals("n") || input.Equals("N"))
                    {
                        playAgain = "n";
                    }
                }

                if (GameMode.Equals("network"))
                {
                    Console.WriteLine("Netzwerkspiel gewählt");
                    Console.ReadKey();
                }


            }

        }

        static public void displayMainMenu()
        {
            Console.WriteLine("Hallo und herzlich Willkommen zum Nim-Spiel!");
            Console.WriteLine("Bitte wähle: " +
                "\n1:\tLokales Spiel starten" +
                "\n2:\tNetzwerkspiel starten (currently not supported!)" +
                "\n3:\tSpielregeln abfragen");
            input = Console.ReadLine();


            if (input.Equals("1"))
            {
                GameMode = "local";
                //Console.ReadKey();
            }
            else if (input.Equals("2"))
            {
                GameMode = "network";
                //Console.ReadKey();
            }
            else if (input.Equals("3"))
            {
                Console.WriteLine("\nAlso, das Spiel ist relativ einfach:\nEs gibt je nach Eingabe (aber mindestens 13) eine Anzahl von Hölzchen.\n" +
                "Jeder Spieler darf maximal 3, muss aber mindestens 1 Hölzchen ziehen. \nDerjenige, der das letzte Hölzchen zieht verliert das Spiel.\nSimpel, nicht wahr?\n");
                Console.WriteLine("Drücke eine beliebige Taste");
                Console.ReadKey();
                Console.WriteLine();
                displayMainMenu();
            }

        }

        static public void setSettingsLocalGame()
        {
           
            Console.WriteLine();
            //TODO Hölzchenzahl in der Klasse Game festlegen

            Console.WriteLine("Jetzt werden die Einstellungen festgelegt." + 
                "\nSpieler 1, wie soll ich dich nennen?");
            input = Console.ReadLine();
            Console.WriteLine();
            lPlayer1 = new LocalPlayer(input);
            

            Console.WriteLine("Ok, ich werde dich ab jetzt {0} nennen \n", lPlayer1.Name);

            Console.WriteLine("Spieler 2, wie soll ich dich nennen?");
            input = Console.ReadLine();
            Console.WriteLine();
            lPlayer2 = new LocalPlayer(input);


            Console.WriteLine("Ok, ich werde dich ab jetzt {0} nennen \n", lPlayer2.Name);


            setSticksAmount();
        }

        static public void setSticksAmount()
        {
            Console.WriteLine("\nLege die Hözlchenanzahl fest. Minimum = 13");
            input = Console.ReadLine();

            if (Convert.ToInt16(input) < 13)
            {
                Console.WriteLine("\nUnzulässiger Wert! Versuch's nochmal.\n");
                setSticksAmount();
            }
            else
            {
                game = new LocalGame(Convert.ToInt16(input), lPlayer1, lPlayer2);
                Console.WriteLine("\nOk, los geht's! {0} fängt an!", lPlayer1.Name);
                //Console.ReadKey();
            }
 
        }
    }
}
