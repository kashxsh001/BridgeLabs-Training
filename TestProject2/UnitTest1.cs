using BankManagement;
namespace TestProject2

{
    public class Tests
    {
        private Bank bank;
        [SetUp]
        public void Setup()
        {
            bank = new Bank();
        }

        [Test]
        public void Test1()
        {
            Branch branch = new Branch(1002, "Mumbai");
            bank.AddBranch(branch);

            Assert.That(bank.GetBranch,Does.Contain(branch));
        }
    }
}
