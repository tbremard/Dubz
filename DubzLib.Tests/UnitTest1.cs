namespace DubzLib.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            var dubletten = new Dubletten(currentDirectory);

            Assert.IsNotNull(dubletten, "cannot instantiate dubletten");
        }
    }
}