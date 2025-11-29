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
//            string targetDir = Directory.GetCurrentDirectory();
            string targetDir = "TargetFakeDir";
            Console.WriteLine($"Target Directory: {targetDir}");
            var dubletten = new Dubletten();

            var items = dubletten.BuildItems(targetDir, Vergleichsmodi.GroesseUndName);

            foreach (var item in items)
            {
                Console.WriteLine($"Item Key: {item.Key}, Path: {item.Path}");
            }

            Assert.Greater(items.Count, 10);
        }
    }
}