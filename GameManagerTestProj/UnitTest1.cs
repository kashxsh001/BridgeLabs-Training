using MultiPlayerGameManager;
namespace GameManagerTestProj
{
    public class Tests
    {
        private GameManager game;
        private Player p1;
        private Player p2;
        private Player p3;
        private Player p4;
        [SetUp]
        public void Setup()
        {
            game = new GameManager();
            p1 = new Player(1, "Kashish", 120);
            p2 = new Player(2, "Rahul", 140);
            p3 = new Player(3, "Priya", 100);
            p4 = new Player(4, "Divya", 180);

            game.AddPlayer(p1);
            game.AddPlayer(p2);
            game.AddPlayer(p3);
            game.AddPlayer(p4);
        }

        [Test]
        public void AddingPlayer_ShouldincreaseCount()
        {
            Assert.That(game.PlayerCount, Is.EqualTo(4));
        }
        [Test]
        public void currentPlayer_ShouldReturnFirstPlayer()
        {
            Player result = game.currentPlayer();
            Assert.That(result.Id, Is.EqualTo(1));
        }
        [Test]
        public void NextPlayer_ShouldReturnTheNextPlayer()
        {
            Player result = game.nextPlayer();
            Assert.That(result.Id, Is.EqualTo(2));
        }
        [Test]
        public void skipPlayer_ShouldskipPlayer()
        {
            Player result = game.SkipPlayer();
            Assert.That(result.Id, Is.EqualTo(3));
        }
        [Test]
        public void ReverseDirection_ShouldChangeDirection()
        {
            game.ReverseDirection();

            Assert.That(game.IsForward, Is.False);
        }

        [Test]
        public void MakeMove_ShouldIncreaseScore()
        {
            game.MakeMove("Bonus", 50);

            Assert.That(p1.Stats.score, Is.EqualTo(170));
        }

        [Test]
        public void EmptyQueue_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(
                () => game.MatchPlayers());
        }
        [Test]
        public void GetPlayer_ShouldReturnCorrectPlayer()
        {
            Player result = game.GetPlayer(2);

            Assert.That(result.PlayerName, Is.EqualTo("Rahul"));
        }

        [Test]
        public void GetPlayer_WhenPlayerDoesNotExist_ShouldThrowException()
        {
            Assert.Throws<KeyNotFoundException>(
                () => game.GetPlayer(999));
        }

        [Test]
        public void BinarySearchPlayer_ShouldFindPlayer()
        {
            Player result =game.BinarySearchPlayer(3);

            Assert.That(result.PlayerName, Is.EqualTo("Priya"));
        }
    }
}
