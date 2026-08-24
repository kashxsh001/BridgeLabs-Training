using System;
using System.Collections.Generic;
using System.Text;

namespace MultiPlayerGameManager
{
    internal class Program
    {
        static void DisplayPlayer(Player player)
        {
            Console.WriteLine(
                $"\nID: {player.Id}");

            Console.WriteLine(
                $"Name: {player.PlayerName}");

            Console.WriteLine(
                $"Score: {player.Stats.score}");

            Console.WriteLine(
                $"Games Played: {player.Stats.GamesPlayed}");

            Console.WriteLine(
                $"Games Won: {player.Stats.GamesWon}");

            Console.WriteLine("Inventory:");

            if (player.Stats.Inventory.Count == 0)
            {
                Console.WriteLine("- Empty");
            }
            else
            {
                foreach (string item in player.Stats.Inventory)
                {
                    Console.WriteLine($"- {item}");
                }
            }
        }
        static void Main(String[] args)
        {
            GameManager game = new GameManager();


            Player p1 = new Player(101, "Kashish", 100);
            Player p2 = new Player(102, "Rahul", 80);
            Player p3 = new Player(103, "Priya", 120);
            Player p4 = new Player(104, "Divya", 90);

            game.AddPlayer(p1);
            game.AddPlayer(p2);
            game.AddPlayer(p3);
            game.AddPlayer(p4);

            Console.WriteLine("Players added successfully.");

           Console.WriteLine($"Current Turn: {game.currentPlayer().PlayerName}");

            game.MakeMove("Collected 50 coins", 50);

            Console.WriteLine($"{p1.PlayerName}'s Score: {p1.Stats.score}");


            // Move to next player
            Player nextPlayer = game.nextPlayer();

            Console.WriteLine($"\nNext Turn: {nextPlayer.PlayerName}");

            game.MakeMove("Defeated an enemy", 30);

            Console.WriteLine($"{p2.PlayerName}'s Score: {p2.Stats.score}");


            nextPlayer = game.nextPlayer();

            Console.WriteLine($"\nNext Turn: {nextPlayer.PlayerName}");

            game.MakeMove("Found a treasure", 40);

            Console.WriteLine($"{p3.PlayerName}'s Score: {p3.Stats.score}");

    
            Console.WriteLine($"Current Player Before Skip: " +$"{game.currentPlayer().PlayerName}");

            Player playerAfterSkip = game.SkipPlayer();

            Console.WriteLine($"Player After Skip: {playerAfterSkip.PlayerName}");



            Console.WriteLine($"Current Player: {game.currentPlayer().PlayerName}");

            game.ReverseDirection();

            Console.WriteLine("Direction reversed.");

            nextPlayer = game.nextPlayer();

            Console.WriteLine($"Next Player After Reverse: {nextPlayer.PlayerName}");

Console.WriteLine(  $"Charlie's Score Before Undo: " +$"{p3.Stats.score}");

            Move undoneMove = game.UndoMove();

            Console.WriteLine(
                $"Undone Move: {undoneMove.Description}");

            Console.WriteLine(
                $"Charlie's Score After Undo: " +
                $"{p3.Stats.score}");

            


            Move replayedMove = game.ReplayMove();

            Console.WriteLine(
                $"Replayed Move: {replayedMove.Description}");

            Console.WriteLine(
                $"Charlie's Score After Replay: " +
                $"{p3.Stats.score}");

            

            Console.WriteLine("7. MATCHMAKING\n");

            game.JoinMatchmaking(p1);
            game.JoinMatchmaking(p2);
            game.JoinMatchmaking(p3);
            game.JoinMatchmaking(p4);

            Console.WriteLine(
                "Players joined matchmaking queue.");

            var match = game.MatchPlayers();

            Console.WriteLine(
                $"Match Created: {match.Player1.PlayerName} VS " +
                $"{match.Player2.PlayerName}");



            Console.WriteLine("8. PLAYER INVENTORY\n");

            game.AddInventoryItem(101, "Sword");
            game.AddInventoryItem(101, "Shield");

            Console.WriteLine(
                $"{p1.PlayerName}'s Inventory:");

            foreach (string item in p1.Stats.Inventory)
            {
                Console.WriteLine($"- {item}");
            }


            Console.WriteLine("9. SEARCH PLAYER BY ID\n");

            Player foundPlayer = game.GetPlayer(102);

            Console.WriteLine(
                $"Player Found: {foundPlayer.PlayerName}");

            Console.WriteLine(
                $"Score: {foundPlayer.Stats.score}");

         

            Console.WriteLine("10. LEADERBOARD\n");

            var leaderboard = game.GetLeaderBoard();

            int rank = 1;

            foreach (Player player in leaderboard)
            {
                Console.WriteLine(
                    $"{rank}. {player.PlayerName} - " +
                    $"Score: {player.Stats.score}");

                rank++;
            }


            Console.WriteLine("11. BINARY SEARCH PLAYER\n");

            Player searchedPlayer =game.BinarySearchPlayer(101);

            if (searchedPlayer != null)
            {
                Console.WriteLine($"Player Found: {searchedPlayer.PlayerName}");
            }
            else
            {
                Console.WriteLine("Player not found.");
            }

            

            Console.WriteLine("12. GAME LOG REPLAY\n");

            try
            {
                GameLog log = game.StartReplay();

                Console.WriteLine(
                    $"First Log: {log.Message}");

                Console.WriteLine("\nMoving Forward Through Logs:\n");

                while (true)
                {
                    log = game.ReplayForward();

                    Console.WriteLine(
                        $"{log.Time:HH:mm:ss} - " +
                        $"{log.Message}");
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine(
                    "\nReached end of replay history.");
            }

            Console.WriteLine("\n------------------------------------\n");



            Console.WriteLine("13. REPLAY BACKWARD\n");

            try
            {
                while (true)
                {
                    GameLog log = game.ReplayBackward();

                    Console.WriteLine(
                        $"{log.Time:HH:mm:ss} - " +
                        $"{log.Message}");
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine(
                    "\nReached beginning of replay history.");
            }

            Console.WriteLine("\n====================================");
            Console.WriteLine(" FINAL PLAYER STATISTICS ");
            Console.WriteLine("====================================");

            DisplayPlayer(p1);
            DisplayPlayer(p2);
            DisplayPlayer(p3);
            DisplayPlayer(p4);


            // ==================================================
            // 15. EDGE CASE EXAMPLES
            // ==================================================

            Console.WriteLine("\n====================================");
            Console.WriteLine(" EDGE CASE TESTING ");
            Console.WriteLine("====================================\n");

            // Player not found
            try
            {
                game.GetPlayer(999);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Player Search Error: {ex.Message}");
            }
        }
    }
}
