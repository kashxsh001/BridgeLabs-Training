using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagement
{
    internal class Transaction
    {
        public int TransactionId { get; private set; }
        public double Amount { get; private set; }

        public string Type { get; private set; }

        public DateTime Date { get; private set;  }

        public Transaction(int id, double amount, string type)
        {
            TransactionId = id;
            Amount = amount;
            Type = type;
            Date = DateTime.Now;
        }

        public void DisplayTransaction()
        {
            Console.WriteLine( $"Transaction: {TransactionId}, Amount: {Amount}, Type: {Type}, Date: {Date}");
        }

    }
}
