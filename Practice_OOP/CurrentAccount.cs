using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagement
{
    internal class CurrentAccount : Account
    {
        public CurrentAccount(int accountNumber, double balance) : base(accountNumber, balance)
        {
        }

        public override double CalculateInterest()
        {
            return Balance * 0.01;
        }

        public override void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid amount");
                return;
            }


            if (Balance - amount >= 3000)
            {
                Balance -= amount;

                AddTransaction(
                    amount,
                    "Withdrawal");

                Console.WriteLine(
                    $"Withdrawn {amount}");
            }
            else
            {
                Console.WriteLine(
                    "Withdrawal denied. " +
                    "limit exceeded.");
            }
        }
    }
}
