using System;
using System.Collections.Generic;
using System.Text;

namespace MultiPlayerGameManager
{
    public class GameManager
    {
        private readonly Circularturn turns = new();

        // 2. Stack
        private readonly Stack<Move> moveHistory = new();

        // Stores undone moves for replay
        private readonly Stack<Move> undoneMoves = new();

        // 3. Queue
        private readonly Queue<Player> matchmakingQueue = new();

        // 4. Doubly Linked List
        private readonly LinkedList<GameLog> gameLogs = new();

        private LinkedListNode<GameLog> replayPosition;

        // 5. HashMap
        private readonly Dictionary<int, Player> players = new();

        // Direction
        public  bool IsForward = true;

        public int PlayerCount => players.Count;

        public int MatchmakingCount => matchmakingQueue.Count;

        private void AddLog(string message)
        {
            gameLogs.AddLast(new GameLog(message));

            replayPosition = gameLogs.Last;
        }

        public void AddPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (players.ContainsKey(player.Id))
                throw new ArgumentException("Player ID already exists.");

            players.Add(player.Id, player);

            turns.Add(player);

           AddLog($"Player added: {player.PlayerName}");
        }
        public Player currentPlayer()
        {
            return turns.currentPlayer;
        }

        public Player nextPlayer()
        {
            Player player = turns.MoveNext(IsForward);
            return player;
        }

        public Player SkipPlayer()
        {
            Player player = turns.skip(IsForward);

            AddLog($"Player skipped. Current player is now: {player.PlayerName}");

            return player;
        }

        public void ReverseDirection()
        {
            IsForward = !IsForward;

            string direction = IsForward
                ? "Forward"
                : "Backward";

            AddLog($"Direction reversed: {direction}");
        }

        public void MakeMove(string description,int scoreChange)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException(
                    "Move description cannot be empty.");

            Player currentPlayer = turns.currentPlayer;

            Move move = new Move(currentPlayer.Id,description,scoreChange);

            moveHistory.Push(move);

            // A new move means old redo history is no longer valid
            undoneMoves.Clear();

            currentPlayer.Stats.score += scoreChange;

            AddLog(
                $"{currentPlayer.PlayerName} made move: {description}, " +
                $"Score change: {scoreChange}");
        }

        public Move UndoMove()
        {
            if (moveHistory.Count == 0)
                throw new InvalidOperationException(
                    "No moves available to undo.");

            Move move = moveHistory.Pop();

            Player player = players[move.PlayerId];

            // Reverse the score change
            player.Stats.score -= move.ScoreChange;

            undoneMoves.Push(move);

            AddLog(
                $"Move undone: {move.Description}");

            return move;
        }

        public Move ReplayMove()
        {
            if (undoneMoves.Count == 0)
                throw new InvalidOperationException(
                    "No moves available to replay.");

            Move move = undoneMoves.Pop();

            Player player = players[move.PlayerId];

            player.Stats.score += move.ScoreChange;

            moveHistory.Push(move);

            AddLog(
                $"Move replayed: {move.Description}");

            return move;
        }
        public void JoinMatchmaking(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            matchmakingQueue.Enqueue(player);

            AddLog(
                $"{player.PlayerName} joined matchmaking queue");
        }

        public (Player Player1, Player Player2) MatchPlayers()
        {
            if (matchmakingQueue.Count < 2)
            {
                throw new InvalidOperationException(
                    "Not enough players for matchmaking.");
            }

            Player player1 = matchmakingQueue.Dequeue();
            Player player2 = matchmakingQueue.Dequeue();

            AddLog(
                $"Match created: {player1.PlayerName} VS {player2.PlayerName}");

            return (player1, player2);
        }
        public Player GetPlayer(int playerId)
        {
            if (!players.TryGetValue(playerId, out Player player))
                throw new KeyNotFoundException(
                    $"Player {playerId} not found.");

            return player;
        }
        public void AddInventoryItem(int playerId,string item)
        {
            Player player = GetPlayer(playerId);

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentException(
                    "Item cannot be empty.");

            player.Stats.Inventory.Add(item);

            AddLog(
                $"{item} added to {player.PlayerName}'s inventory");
        }

        public List<Player> GetLeaderBoard()
        {
            List<Player> leaderboard = players.Values.ToList();
            leaderboard.Sort(
           (a, b) =>
               b.Stats.score.CompareTo(a.Stats.score));

            return leaderboard;

        }
        
        public Player BinarySearchPlayer(int playerId)
        {
            List<Player> SortedPlayers = players.Values
                                        .OrderBy(p => p.Id)
                                        .ToList();
            int start = 0;
            int end = SortedPlayers.Count-1;

            while (start <= end)
            {
                int mid = start + (end - start) / 2;
                Player midPlayer = SortedPlayers[mid];
                if (midPlayer.Id == playerId) return midPlayer;
                else if (midPlayer.Id < playerId) start = mid + 1;
                else end = mid - 1;
            }
            throw new KeyNotFoundException($"Player {playerId} not found.");
        }

        public GameLog StartReplay()
        {
            if (gameLogs.Count == 0)
                throw new InvalidOperationException(
                    "No logs available.");

            replayPosition = gameLogs.First;

            return replayPosition.Value;
        }

        public GameLog ReplayForward()
        {
            if (replayPosition == null)
                throw new InvalidOperationException(
                    "Replay not started.");

            if (replayPosition.Next == null)
                throw new InvalidOperationException(
                    "Already at latest log.");

            replayPosition = replayPosition.Next;

            return replayPosition.Value;
        }

        public GameLog ReplayBackward()
        {
            if (replayPosition == null)
                throw new InvalidOperationException(
                    "Replay not started.");

            if (replayPosition.Previous == null)
                throw new InvalidOperationException(
                    "Already at first log.");

            replayPosition = replayPosition.Previous;

            return replayPosition.Value;
        }

        public List<Player> GetTurnOrder()
        {
            return turns.GetPlayers();
        }

    }
}
