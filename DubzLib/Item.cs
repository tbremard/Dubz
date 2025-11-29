namespace DubzLib
{
    public class Item
    {
        public string Key { get; set; }

        public Item(string key, string path)
        {
            Key = key;
            Path = path;
        }

        public string Path { get; set; }
    }
}