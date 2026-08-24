using System;
using System.Collections.Generic;
using System.Text;

namespace MultiPlayerGameManager
{
    public class GameLog
    {
     public DateTime Time { get; }
     public string Message { get; }

        public GameLog(string message)
        {
            Time = DateTime.Now;
            Message = message;
        }

        public override string ToString()
        {
            return $"{Time} - {Message}";
        }

    }
}
