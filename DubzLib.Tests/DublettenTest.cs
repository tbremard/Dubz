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

        [Test]
        public void SammleKandidaten_WenGroupByGroesse_ThenGrouped()
        {
            string targetDir = "TargetFakeDir";
            Console.WriteLine($"Target Directory: {targetDir}");
            var dubletten = new Dubletten();

            var kandidaten = dubletten.SammleKandidaten(targetDir, Vergleichsmodi.Groesse);

            Console.WriteLine($"Found {kandidaten?.Count ?? 0} duplicate groups");
            
            if (kandidaten != null)
            {
                foreach (var dublette in kandidaten)
                {
                    Console.WriteLine($"Duplicate group with {dublette.Dateipfade.Count} files:");
                    foreach (var pfad in dublette.Dateipfade)
                    {
                        Console.WriteLine($"  - {pfad}");
                    }
                    Console.WriteLine();
                }
            }

            // Based on TargetFakeDir content, we should find exactly 5 duplicate groups by size:
            // 33 bytes: small.txt (2 files)
            // 100 bytes: exactcopy.txt x2, file2.txt, unique.txt (4 files) 
            // 103 bytes: different_name.txt, identical_content.txt (2 files)
            // 104 bytes: duplicate.txt x3, B/file1.txt (4 files)
            // 105 bytes: samesize.txt x2 (2 files)
            Assert.IsNotNull(kandidaten, "SammleKandidaten should not return null");
            Assert.AreEqual(5, kandidaten.Count, "Should find exactly 5 groups of files with same size");
        }

        [Test]
        public void SammleKandidaten_WenGroupByGroesseUndName_ThenGrouped()
        {
            string targetDir = "TargetFakeDir";
            Console.WriteLine($"Target Directory: {targetDir}");
            var dubletten = new Dubletten();

            var kandidaten = dubletten.SammleKandidaten(targetDir, Vergleichsmodi.GroesseUndName);

            Console.WriteLine($"Found {kandidaten?.Count ?? 0} duplicate groups");
            
            if (kandidaten != null)
            {
                foreach (var dublette in kandidaten)
                {
                    Console.WriteLine($"Duplicate group with {dublette.Dateipfade.Count} files:");
                    foreach (var pfad in dublette.Dateipfade)
                    {
                        Console.WriteLine($"  - {pfad}");
                    }
                    Console.WriteLine();
                }
            }

            // Based on TargetFakeDir content, should find exactly 4 groups by size AND name:
            // duplicate.txt: 3 copies (same name + same size)
            // small.txt: 2 copies (same name + same size) 
            // exactcopy.txt: 2 copies (same name + same size)
            // samesize.txt: 2 copies (same name + same size, different content)
            Assert.IsNotNull(kandidaten, "SammleKandidaten should not return null");
            Assert.AreEqual(4, kandidaten.Count, "Should find exactly 4 groups of files with same name and size");
        }
    }
}