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
                Console.WriteLine("Usage: Dubz <Directory>");
                Console.WriteLine("      search for similar file recursively in <Directory>");
                Console.WriteLine("      display all matching files");
                return;
            }
            string currentDirectory = Directory.GetCurrentDirectory();
            var dubletten = new Dubletten();
        }
    }
}
