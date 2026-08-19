using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagement
{
    internal class Loan
    {
        public int LoanId { get; private set; }
        public double Amount { get; private set; }
        public Customer Customer { get; private set; }
        public Account RepaymentAccount{ get; private set; }
        public Loan(
                int loanId,
                double amount,
                Customer customer)
        {
           LoanId = loanId;
           Amount = amount;
           Customer = customer;
        }

       public void LinkRepaymentAccount(
                Account account)
       {
            RepaymentAccount = account;
        }

      public void Display()
      {
           Console.WriteLine($"Loan ID: {LoanId}");
           Console.WriteLine($"Loan Amount: {Amount}");
           Console.WriteLine($"Customer: {Customer.Name}");
            if (RepaymentAccount != null)
            {
               Console.WriteLine($"Repayment Account: {RepaymentAccount.AccountNumber}");
           }
            else { 
               Console.WriteLine("No repayment account linked.");
           }
       }
   }
}

