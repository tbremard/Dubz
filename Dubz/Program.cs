using DubzLib;
using System;
using System.IO;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace Dubz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: Dubz <Directory>");
                Console.WriteLine("      search for similar file recursively in <Directory>");
                Console.WriteLine("      display all matching files");
                return;
            }
            string targetDir = args[0];
            if (!Directory.Exists(targetDir))
            {
                Console.WriteLine($"Error: Directory '{targetDir}' does not exist.");
                return;
            }
            var dubletten = new Dubletten();
            Console.WriteLine("[+] Grouping by size all files...");
            var kandidaten = dubletten.SammleKandidaten(targetDir, Vergleichsmodi.Groesse);
            Console.WriteLine("[+] Calculate hashes for exact matching...");
            var found = dubletten.PruefeKandidaten(kandidaten);
            if (found.Count==0)
            {
                Console.WriteLine("No duplicate files found.");
                return;
            }
            foreach (var dublette in found)
            {
                Console.WriteLine("------------------");
                foreach (var pfad in dublette.Dateipfade)
                {
                    Console.WriteLine($"  - {pfad}");
                }
            }
            Console.WriteLine($"Found {found.Count} duplicate group of files");
        }
    }
}
