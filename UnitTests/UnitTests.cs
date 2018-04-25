namespace Learning
{
    using AlphabetWar;
    using NUnit.Framework;

    [TestFixture]
    public class AlphabetWarTest
    {
        [TestCase("z", "Right side wins!")]
        [TestCase("ts", "Left side wins!")]
        [TestCase("jz", "Right side wins!")]
        [TestCase("st", "Left side wins!")]
        [TestCase("ast", "Left side wins!")]
        [TestCase("tzj", "Right side wins!")]
        [TestCase("dtbeqjwteqqt", "Left side wins!")]
        [TestCase("sedtqwt", "Left side wins!")]
        [TestCase("stjw", "Right side wins!")]
        public void BasicTest(string fight, string result)
        {
            Assert.AreEqual(result, Kata.AlphabetWar(fight));
        }
    }
}