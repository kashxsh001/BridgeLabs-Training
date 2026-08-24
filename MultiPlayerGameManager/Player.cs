using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MultiPlayerGameManager
{
    public class Player
    {
        public int Id { get; }
        public string PlayerName { get; }

        public PlayerStats Stats { get; }

        public Player(int id, string playerName,int score = 0)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new ArgumentException("Player Cannot be empty!");
            }
            Id = id;
            PlayerName = playerName;
            Stats = new PlayerStats(score);
        }
        public override string ToString()
        {
            return $"{Id} - {PlayerName} - Score: {Stats.score}";
        }



    }
}
