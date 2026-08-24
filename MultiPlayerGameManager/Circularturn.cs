using System;
using System.Collections.Generic;
using System.Text;

namespace MultiPlayerGameManager
{
    internal class Circularturn
    {
        private PlayerNode head;
        private PlayerNode current;
        public int Count { get; private set;}
        public bool isEmpty => Count == 0;
        public Player currentPlayer { get
            {
                if (current == null)
                {
                    throw new InvalidOperationException("No players in Game");
                }

                return current.Player;
            }
        }
        public void Add(Player player)
        {
            PlayerNode node = new PlayerNode(player);
            if (head == null)
            {
                head = node;
                current = node;
                node.Next = node;
                node.Previous = node;

            }
            else
            {
                PlayerNode tail = head.Previous;

                tail.Next = node;
                node.Previous = tail;

                node.Next = head;
                head.Previous = node;

            }
            Count++;
        }

        public Player MoveNext(bool forward)
        {
            if (current == null)
            {
                throw new InvalidOperationException("No players in team");

            }

            current = forward ? current.Next : current.Previous;

            return current.Player;
        }

        public Player skip(bool forward)
        {
            if (current == null)
                throw new InvalidOperationException("No players available.");

            current = forward
                ? current.Next
                : current.Previous;

            current = forward
                ? current.Next
                : current.Previous;

            return current.Player;

        }

        public List<Player> GetPlayers()
        {
            List<Player> players = new();
            if (head == null)
            {
                return players;
            }

            PlayerNode temp = head;
            while (temp != null)
            {
                players.Add(temp.Player);
                temp = temp.Next;
            }
            return players;
        }

       


    }
}
