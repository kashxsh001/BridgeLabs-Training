using System;
using System.Collections.Generic;
using System.Text;

namespace MultiPlayerGameManager
{
    public class Move
    {
        public int PlayerId { get; }
        public string Description {  get; }
        public int ScoreChange { get; }

        public Move(int  playerId, string description, int scoreChange)
        {
            PlayerId = playerId;
            Description = description;
            ScoreChange = scoreChange;
        }
        public override string ToString()
        {
            return $"{PlayerId} - {Description} - {ScoreChange}";
        }
    }
}
