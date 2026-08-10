using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagement
{
    public class Branch
    {
        public int BranchID {  get; private set; }
        public string Name { get; private set; }
        List<Account> accounts = new List<Account>();
        public Branch(int branchID, string name)
        {
            BranchID = branchID;
            Name = name;
        }

        public void AddAccount(Account account) {
            accounts.Add(account);
        }

        public void DisplayAccounts()
        {
            foreach (Account account in accounts)
            {
                account.Display();

            }
        }
        public void DisplayBranch()
        {
            Console.WriteLine($"Branch ID : {BranchID} and Branch Name : {Name}.");
        }
    }
}
