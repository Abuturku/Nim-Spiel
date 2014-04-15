using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Nim_Spiel
{
    [Serializable()]
    public class Highscore
    {
        private string path = AppDomain.CurrentDomain.BaseDirectory + "\\highscores.xml";       //Ursprungsort der Anwendung; Dort wird die XML angelegt

        public List<string> Players { get; set; }

        public List<int> Wins { get; set;}

        public List<int> Losses { get; set; }


        //public Highscore(List<string> players, List<int> wins, List<int> losses)
        //{
        //    this.Players = players;
        //    this.Wins = wins;
        //    this.Losses = losses;
        //}

        public Highscore()
        {
            this.Players = new List<string>();
            this.Wins = new List<int>();
            this.Losses = new List<int>();
        }

        public void SaveHighscores()        //Serialisieren; speichern der Highscores in die XML und sie "dauerhaft" verfügbar machen
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Highscore));

            //FileStream file = new FileStream(this.path, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(@path))
            {
                serializer.Serialize(writer, this);
            }

            //serializer.Serialize(file, this);
            //file.Close();
        }

        public Highscore GetHighscores()        //Deserialisieren; holen der Highscores aus der XML und sie für die Anwendung verfügbar machen
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Highscore));
            Highscore hs = new Highscore();
            FileStream file;
            try
            {
                file = new FileStream(this.path, FileMode.Open);
                file.Close();
            }
            catch (FileNotFoundException)
            {
                file = new FileStream(this.path, FileMode.Create);
                file.Close();
            }
            file.Close();
            file = new FileStream(this.path, FileMode.Open);

            try
            {
                hs = serializer.Deserialize(file) as Highscore;
            }
            catch (Exception)
            {
                file.Close();
                SaveHighscores();
            }
            file.Close();

            this.Players = hs.Players;
            this.Wins = hs.Wins;
            this.Losses = hs.Losses;

            return hs;
            
        }

        public void UpdateHighscore(string name, int wins, int losses)      //Wins und Losses des jeweiligen Spieler aktualisieren
        {
            for (int i = 0; i < this.Players.Count; i++)
            {
                if (this.Players[i].Equals(name))
                {
                    this.Wins[i] =  this.Wins[i] + wins;
                    this.Losses[i] = this.Losses[i] + losses;
                }
            }
        }

        public void AddNewHighscore(string name, int wins, int losses)      //Falls Spieler noch nicht in der Highscore-Liste aufgetaucht ist hinzufügen
        {
            this.GetHighscores();

            this.Players.Add(name);
            this.Wins.Add(wins);
            this.Losses.Add(losses);
        }

        public void Sort()
        {
            this.Quicksort(this.Players, this.Wins, this.Losses, 0, this.Players.Count-1);
        }

        public void Quicksort(List<string> players, List<int> wins, List<int> losses, int left, int right)      //Quicksort auf die Highscore-Listen jagen
        {
            int i = left;
            int j = right;
            double pivot = Convert.ToDouble(wins[(left+right) / 2]) / (Convert.ToDouble(wins[(left+right) / 2]) + Convert.ToDouble(losses[(left+right) / 2]));

            while (i <= j)
            {


                while (Convert.ToDouble(wins[i]) / (Convert.ToDouble(wins[i]) + Convert.ToDouble(losses[i])) > pivot)
                {
                    i++;
                }

                while (Convert.ToDouble(wins[j]) / (Convert.ToDouble(wins[j]) + Convert.ToDouble(losses[j])) < pivot)
                {
                    j--;
                }

                if (i <= j)
                {
                    int tmp = wins[i];
                    wins[i] = wins[j];
                    wins[j] = tmp;

                    tmp = losses[i];
                    losses[i] = losses[j];
                    losses[j] = tmp;

                    string tmpP = players[i];
                    players[i] = players[j];
                    players[j] = tmpP;

                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                Quicksort(players, wins, losses, left, j);
            }

            if (i < right)
            {
                Quicksort(players, wins, losses, i, right);
            }

        }
    }
}
