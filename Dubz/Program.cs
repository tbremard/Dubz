using DubzLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
            Print("Searching for duplicates in directory:");
            Print(targetDir);
            var stopwatch = Stopwatch.StartNew();
            var kandidaten = dubletten.SammleKandidaten(targetDir, Vergleichsmodi.Groesse);
            var found = dubletten.PruefeKandidaten(kandidaten);
            stopwatch.Stop();
            int durationSec = (int)stopwatch.Elapsed.TotalSeconds;
            SaveResults(found, durationSec);
        }

        private static void SaveResults(IReadOnlyCollection<IDublette> found, int durationSec)
        {
            if (found.Count == 0)
            {
                Print("No duplicate files found.");
                return;
            }
            foreach (var dublette in found)
            {
                Print("------------------");
                foreach (var pfad in dublette.Dateipfade)
                {
                    Print($"{pfad}");
                }
            }
            Print("------------------");
            Print($"Found {found.Count} groups of files duplicates in: " + durationSec + " sec");
            return;
        }

        private static void Print(string v)
        {
            Console.WriteLine(v);
            File.AppendAllText("result.txt", v + Environment.NewLine);
        }
    }
}
