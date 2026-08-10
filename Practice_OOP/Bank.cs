using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagement
{
    public class Bank
    {
        public int BankId { get; private set; }
        public string Name { get; private set; }
        List<Branch> branches = new List<Branch>();

        public Bank()
        {

        }
        public Bank(int id, string name)
        {
            BankId = id;
            Name = name;
        }

        public void AddBranch(Branch branch)
        {
            branches.Add(branch);
        }

        public void DisplayBranch()
        {
            foreach (Branch branch in branches)
            {
                branch.DisplayBranch();
            }
        }

        public List<Branch> GetBranch() {
            return branches;
                }


        public void DisplayBank()
        {
            Console.WriteLine($"Bank id is {BankId} and the name is {Name}");
        }
    }
}
