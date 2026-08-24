using System;
using System.Collections.Generic;
using System.Text;

namespace MultiPlayerGameManager
{
    internal class PlayerNode
    {
        public Player Player { get; }

        public PlayerNode Next { get; set; }
        public PlayerNode Previous { get; set; }

        public PlayerNode(Player player)
        {
            Player = player;
            
        }

    }
}
