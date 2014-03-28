using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nim_Spiel
{
    class Program
    {
        static string input;
        static string gameMode;
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

                if (gameMode.Equals("local"))
                {
                    Console.WriteLine("\nLokales Spiel gewählt\n");

                    //Console.ReadKey();
                    //Console.WriteLine();
                    setSettingsLocalGame();
                    Console.Clear();
                    game.Start();
                    Console.WriteLine("Nochmal spielen? Gebe 'y' (Ja) oder 'n' (Nein) ein");
                    input = Console.ReadLine();

                    if (input.Equals("n") || input.Equals("N"))
                    {
                        playAgain = "n";
                    }
                }

                if (gameMode.Equals("network"))
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
                gameMode = "local";
                //Console.ReadKey();
            }
            else if (input.Equals("2"))
            {
                gameMode = "network";
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
            else
            {
                Console.WriteLine("Ungültige Eingabe. Drücke Eingabe, um fortzufahren.");
                Console.ReadLine();
                Console.Clear();
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
            if (input.Equals(""))
            {
                input = "Spieler 1";
            }
            Console.WriteLine();
            lPlayer1 = new LocalPlayer(input);
            

            Console.WriteLine("Ok, ich werde dich ab jetzt {0} nennen \n", lPlayer1.Name);

            Console.WriteLine("Spieler 2, wie soll ich dich nennen?");
            input = Console.ReadLine();
            if (input.Equals(""))
            {
                input = "Spieler 2";
            }
            Console.WriteLine();
            lPlayer2 = new LocalPlayer(input);


            Console.WriteLine("Ok, ich werde dich ab jetzt {0} nennen \n", lPlayer2.Name);


            setSticksAmount();
        }

        static public void setSticksAmount()
        {
            Console.WriteLine("\nLege die Hözlchenanzahl fest. Minimum = 13");
            input = Console.ReadLine();


            try
            {
                if (Convert.ToInt16(input) < 13)
                {
                    Console.WriteLine("\nUnzulässiger Wert! Versuch's nochmal.");
                    setSticksAmount();
                }
                else
                {
                    game = new LocalGame(Convert.ToInt16(input), lPlayer1, lPlayer2);
                    
                    //Console.ReadKey();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Probier es nochmal.");
                setSticksAmount();
            }

        }
    }
}
