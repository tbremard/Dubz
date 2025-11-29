using DubzLib;
using System;
using System.IO;

namespace Dubz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: Dubz FilePath");
                Console.WriteLine("      search for similar file recursively from current directory");
                Console.WriteLine("      display all matching files");
                return;
            }
            string currentDirectory = Directory.GetCurrentDirectory();
            var dubletten = new Dubletten(currentDirectory);
        }
    }
}
