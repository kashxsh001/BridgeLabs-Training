namespace MultiPlayerGameManager
{
    public class PlayerStats
    {
        public int score { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }

        public List<string> Inventory { get; set; } = new();
        public PlayerStats()
        {

        }

        public PlayerStats(int score)
        {
            this.score = score;
        }


    }

}
