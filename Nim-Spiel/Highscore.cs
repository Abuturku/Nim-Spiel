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
        private string path = AppDomain.CurrentDomain.BaseDirectory + "\\highscores.xml";

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

        public void SaveHighscores()
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

        public Highscore GetHighscores()
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

        public void UpdateHighscore(string name, int wins, int losses)
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

        public void AddNewHighscore(string name, int wins, int losses)
        {
            this.GetHighscores();

            this.Players.Add(name);
            this.Wins.Add(wins);
            this.Losses.Add(losses);
        }
    }
}
