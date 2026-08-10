using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagement
{
    public abstract class Account
    {
        public int AccountNumber {  get; private set; }
        public double Balance { get; set;  }

        private List<Transaction> transactions =
       new List<Transaction>();

        public Account(int accountNumber, double balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        protected void AddTransaction(
          double amount,
          string type)
        {
            Transaction transaction =
                new Transaction(
                    transactions.Count + 1,
                    amount,
                    type);

            transactions.Add(transaction);
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount");
                return;
            }

            Balance += amount;

           

            AddTransaction(amount,"Deposit");

            Console.WriteLine(
                $"Deposited {amount}");
        }

        public abstract void Withdraw(double amount);

        public abstract double CalculateInterest();

      

        public void DisplayTransactions()
        {
            Console.WriteLine(
                $"\nTransactions for Account {AccountNumber}:");

            foreach (Transaction transaction in transactions)
            {
                transaction.DisplayTransaction();
            }
        }

        public void Display()
        {
            Console.WriteLine(
                $"Account Number: {AccountNumber}");

            Console.WriteLine(
                $"Balance: {Balance}");
        }

    }
}
