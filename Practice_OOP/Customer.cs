using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagement
{
    internal class Customer
    {
        public int CustomerId { get; private set; }

        public string Name { get; private set; }

        private List<Account> accounts =
            new List<Account>();

        private List<Loan> loans =
            new List<Loan>();

        public Customer(
            int customerId,
            string name)
        {
            CustomerId = customerId;
            Name = name;
        }

        public void AddAccount(Account account)
        {
            if (!accounts.Contains(account))
            {
                accounts.Add(account);
            }
        }

        public void AddLoan(Loan loan)
        {
            loans.Add(loan);
        }

        public void DisplayAccounts()
        {
            foreach (Account account in accounts)
            {
                account.Display();
            }
        }

        public void DisplayLoans()
        {
            foreach (Loan loan in loans)
            {
                loan.Display();
            }
        }

        public void Display()
        {
            Console.WriteLine(
                $"Customer ID: {CustomerId}");

            Console.WriteLine(
                $"Customer Name: {Name}");
        }
    }
}
