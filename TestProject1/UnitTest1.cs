using ClassLibrary1;


namespace TestProject1
{
    public class Tests
    {
        private Class1 class1;
        private StringUtils utils;
        private ListManager manager;
        List<int> list;
        DatabaseConnection database;
        NumberUtils Utils;
       
        [SetUp]
        public void Setup()
        {
            class1 = new Class1(); //run before any test runs. AAA - Arrange 
           utils = new StringUtils();
            manager = new ListManager();
            list = new List<int>();
            database = new DatabaseConnection();
            database.Connect();
            Utils = new NumberUtils();

        }

        [Test]
        public void Test1()
        {
           int result = class1.Addition(2, 3);
            Assert.That(result,Is.EqualTo(5));
        }

        [Test]
        public void Reverse_ShouldReturnReversedString()
        {
            string result = utils.Reverse("Hello");

            Assert.That(result, Is.EqualTo("olleH"));
        }

        [Test]
        public void IsPalindrome_ShouldReturnTrue()
        {
            bool result = utils.IsPalindrome("madam");

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsPalindrome_ShouldReturnFalse()
        {
            bool result = utils.IsPalindrome("hello");

            Assert.That(result, Is.False);
        }

        [Test]
        public void ToUpperCase_ShouldConvertString()
        {
            string result = utils.ToUpperCase("hello");

            Assert.That(result, Is.EqualTo("HELLO"));
        }

        [Test]
        public void AddElement_ShouldAddElement()
        {
            manager.AddElement(list, 10);

            Assert.That(list, Does.Contain(10));
        }

        [Test]
        public void RemoveElement_ShouldRemoveElement()
        {
            list.Add(10);
            list.Add(20);

            manager.RemoveElement(list, 10);

            Assert.That(list, Does.Not.Contain(10));
        }

        [Test]
        public void GetSize_ShouldReturnCorrectSize()
        {
            list.Add(10);
            list.Add(20);

            int size = manager.GetSize(list);

            Assert.That(size, Is.EqualTo(2));
        }

        [Test]
        public void Divide_ByZero_ShouldThrowException()
        {
            Calculator calculator = new Calculator();

            Assert.Throws<ArithmeticException>(() =>
                calculator.Divide(10, 0));
        }

        [TearDown]
        public void Cleanup()
        {
            database.Disconnect();
        }

        [Test]
        public void Connection_ShouldBeEstablished()
        {
            Assert.That(database.IsConnected, Is.True);
        }

        [Test]
        public void Connection_ShouldBeClosedAfterCleanup()
        {
            database.Disconnect();

            Assert.That(database.IsConnected, Is.False);
        }

       

        [TestCase(2, true)]
        [TestCase(4, true)]
        [TestCase(6, true)]
        [TestCase(7, false)]
        [TestCase(9, false)]
        public void IsEven_ShouldReturnCorrectResult(int number, bool expected)
        {
            bool result = Utils.IsEven(number);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Timeout(2000)]
        public void LongRunningTask_ShouldCompleteWithinTwoSeconds()
        {
            LongRunningService service = new LongRunningService();

            string result = service.LongRunningTask();

            Assert.That(result, Is.EqualTo("Task Completed"));
        }

    }
}

