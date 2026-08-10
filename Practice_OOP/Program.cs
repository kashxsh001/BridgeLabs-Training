using BankManagement;

Bank bank =
    new Bank(1, "SBI");

Branch branch =
    new Branch(101, "Chandigarh");

bank.AddBranch(branch);

Account savings =
    new SavingAccount(
        1001,
        50000);

Account current =
    new CurrentAccount(
        1002,
        20000);

branch.AddAccount(savings);
branch.AddAccount(current);

Customer kashish =
    new Customer(
        1,
        "Kashish");


kashish.AddAccount(savings);
kashish.AddAccount(current);
savings.Deposit(5000);
savings.Withdraw(2000);

Console.WriteLine($"Savings Interest: {savings.CalculateInterest()}");

Console.WriteLine($"Current Interest: {current.CalculateInterest()}");

savings.DisplayTransactions();

Loan loan =
    new Loan(
        501,
        100000,
        kashish);

loan.LinkRepaymentAccount(
    savings);

kashish.AddLoan(loan);

bank.DisplayBank();

bank.DisplayBranch();

branch.DisplayAccounts();

kashish.Display();

kashish.DisplayAccounts();

loan.Display();
