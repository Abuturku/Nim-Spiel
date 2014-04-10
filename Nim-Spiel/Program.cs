using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nim_Spiel
{
    class Program
    {
        static string input;
        static string gameMode;
        static LocalPlayer lPlayer1;
        static Player player2;
        static NetworkPlayer nPlayer1;
        public static LocalGame game;
        static string playAgain = "y";

        static void Main(string[] args)
        {
            Console.Clear();

            while (playAgain.Equals("y") || playAgain.Equals("Y"))
            {
                Console.Clear();
                displayMainMenu();

                if (gameMode.Equals("local"))
                {
                    Console.WriteLine("\nLokales Spiel gewählt\n");
                    Thread.Sleep(500);
                    //Console.ReadKey();
                    //Console.WriteLine();
                    setSettingsLocalGame();
                    Console.Clear();
                    game.Start();
                    Thread.Sleep(1000);
                    Console.WriteLine("\nNochmal spielen? Gebe 'y' (Ja) oder 'n' (Nein) ein");
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
                "\n3:\tSpielregeln abfragen" + 
                "\n4: \tHighscores abrufen" + 
                "\n5: \tBeenden");
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
                "Jeder Spieler darf maximal 3, muss aber mindestens 1 Hölzchen ziehen. Dafür hat jeder 12,5 Sekunden Zeit. " + 
                "\nDer Hardcore-Schwierigkeitsgrad gibt dir nur 2 Sekunden zum Überlegen." + 
                "\nDerjenige, der das letzte Hölzchen zieht verliert das Spiel. " + 
                "\nSimpel, nicht wahr?\n");
                Console.WriteLine("Drücke eine beliebige Taste");
                Console.ReadKey();
                Console.Clear();
                displayMainMenu();
            }
            else if (input.Equals("4"))
            {
                Highscore hs = new Highscore();
                hs = hs.GetHighscores();
                Console.Write("\n\nName\t\tSiege");
                Console.SetCursorPosition(Console.CursorLeft + 6, Console.CursorTop);
                Console.Write("Niederlagen");
                Console.SetCursorPosition(Console.CursorLeft + 4, Console.CursorTop);
                Console.Write("Quote");
                for (int i = 0; i < hs.Players.Count; i++)
                {
                    Console.Write("\n{0}", hs.Players[i]);
                    Console.SetCursorPosition(16, Console.CursorTop);
                    Console.Write("{0}", hs.Wins[i]);
                    Console.SetCursorPosition(Console.CursorLeft + 10, Console.CursorTop);
                    Console.Write("{0}", hs.Losses[i]);
                    Console.SetCursorPosition(Console.CursorLeft + 14, Console.CursorTop);
                    try
                    {
                        Console.Write("{0}", (hs.Wins[i] / hs.Losses[i]));
                    }
                    catch (DivideByZeroException)
                    {
                        Console.Write("100");
                    }
                    
                }
                Console.WriteLine("\n\n");
                Console.WriteLine("Drücke eine beliebige Taste.");
                Console.ReadLine();
                Console.Clear();
                displayMainMenu();
            }
            else if (input.Equals("5"))
            {
                Environment.Exit(0);
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
            

            Console.WriteLine("Ok, ich werde dich ab jetzt {0} nennen. \n", lPlayer1.Name);
            Thread.Sleep(1000);

            Console.WriteLine("Spieler 2, wie soll ich dich nennen? (Eingabe drücken, um gegen den Computer zu spielen)");
            input = Console.ReadLine();
            if (input.Equals(""))
            {
                player2 = new ComputerPlayer();
                player2.Name = "Computer";

                Console.WriteLine("Schwierigkeit auswählen:" + 
                    "\n1: \tEinfach" + 
                    "\n2: \tMittel" + 
                    "\n3: \tSchwer" +
                    "\n4: \tHardcore");

                ComputerPlayer cp = (ComputerPlayer)player2;

                while (input.Equals("") || !input.Equals("1") || !input.Equals("2")|| !input.Equals("3") || !input.Equals("4")) 
                {
                    input = Console.ReadLine();
                    if (input.Equals("1"))
                    {
                        Console.WriteLine("Einfach ausgewählt");
                        cp.Difficulty = "easy";
                        break;
                    }
                    else if (input.Equals("2"))
                    {
                        Console.WriteLine("Mittel ausgewählt");
                        cp.Difficulty = "medium";
                        break;
                    }
                    else if (input.Equals("3"))
                    {
                        Console.WriteLine("Schwer ausgewählt");
                        cp.Difficulty = "hard";
                        break;
                    }
                    else if (input.Equals("4"))
                    {
                        Console.WriteLine("Hardcore ausgewählt");
                        cp.Difficulty = "hardcore";
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Unzulässige Eingabe");
                    }
                }
            }
            else
            {
                player2 = new LocalPlayer(input);
                Console.WriteLine("Ok, ich werde dich ab jetzt {0} nennen. \n", player2.Name);
            }

            Console.WriteLine();
            
            Thread.Sleep(1000);

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
                    game = new LocalGame(Convert.ToInt16(input), lPlayer1, player2);
                    Console.WriteLine("Hölzchen: {0}", game.Sticks);
                    Thread.Sleep(800);
                    //Console.ReadKey();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Probier es nochmal.");
                setSticksAmount();
            }
            catch (OverflowException)
            {
                game = new LocalGame(32767, lPlayer1, player2);
                Console.WriteLine("Wert war zu hoch; wird automatisch auf 32767 gesetzt.");
                Thread.Sleep(1500);
            }

        }
    }
}
