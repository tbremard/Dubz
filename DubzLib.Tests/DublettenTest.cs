namespace DubzLib.Tests
{
    public class DublettenTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BuildItems()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current Directory: {currentDirectory}");
            var dubletten = new Dubletten();

            var items = dubletten.BuildItems(currentDirectory, Vergleichsmodi.GroesseUndName);

            foreach (var item in items)
            {
                Console.WriteLine($"Item Key: {item.Key}, Path: {item.Path}");
            }

            Assert.Greater(items.Count, 10);
        }
    }
}