using System.Collections.Concurrent;

namespace DubzLib
{
    public class Dubletten: IDublettenpruefung
    {
        public IReadOnlyCollection<IDublette> PruefeKandidaten(IEnumerable<IDublette> kandidaten)
        {
            Console.WriteLine($"[+] FastFilter on {kandidaten.Count()} groups...");
            var fastHasher = new FastHasher();
            List<IDublette> fastFiltered = ParallelFilter(kandidaten, fastHasher); // this is Expert French touch (not in spec, but 90% of reliability in 10% of time)
            Console.WriteLine($"[+] Md5Filter on {fastFiltered.Count()} groups...");
            var md5Hasher = new Md5Hasher();
            List<IDublette> ret = ParallelFilter(fastFiltered, md5Hasher);
            return ret;
        }

        private List<IDublette> ParallelFilter(IEnumerable<IDublette> kandidaten, IHasher hasher)
        {
            var ret = new List<IDublette>();
            foreach (var kandidat in kandidaten)
            {
                var hashedBag = new ConcurrentBag<FileHashed>();                
                Parallel.ForEach(kandidat.Dateipfade, filePath =>
                {
                    string hash = hasher.Hash(filePath);
                    if (hash != null)
                    {
                        hashedBag.Add(new FileHashed(filePath, hash));
                    }
                });                
                var hashGroups = new Dictionary<string, List<string>>();
                foreach (var item in hashedBag)
                {
                    if (!hashGroups.ContainsKey(item.Hash))
                    {
                        hashGroups[item.Hash] = new List<string>();
                    }
                    hashGroups[item.Hash].Add(item.FilePath);
                }
                foreach (var group in hashGroups.Values)
                {
                    if (group.Count > 1)
                    {
                        ret.Add(new DubletteImpl(group));
                    }
                }
            }
            return ret;
        }

        public IReadOnlyCollection<IDublette> SammleKandidaten(string pfad)
        {
            var ret = SammleKandidaten(pfad, Vergleichsmodi.GroesseUndName);
            return ret;
        }

        public IReadOnlyCollection<IDublette> SammleKandidaten(string pfad, Vergleichsmodi modus)
        {
            Console.WriteLine($"[+] Enumerate all files recursively...");
            List<Item> items = BuildItems(pfad, modus);
            Console.WriteLine($"    ... found {items.Count} files");
            var groups = new Dictionary<string, List<string>>();
            foreach (var item in items)
            {
                if (!groups.ContainsKey(item.Key))
                {
                    groups[item.Key] = new List<string>();
                }
                groups[item.Key].Add(item.Path);
            }
            var ret = new List<IDublette>();
            foreach (var group in groups.Values)
            {
                if (group.Count > 1)
                {
                    ret.Add(new DubletteImpl(group));
                }
            }
            return ret;
        }

        public List<Item> BuildItems(string pfad, Vergleichsmodi modus)
        {
            if (!Directory.Exists(pfad))
            {
                throw new DirectoryNotFoundException(pfad);

            }
            string[] directories;
            try
            {
                directories = Directory.GetDirectories(pfad);
            }
            catch (UnauthorizedAccessException)
            {
                return new List<Item>();
            }
            var ret = new List<Item>();
            foreach (var subDir in directories)
            {
                var curItems = BuildItems(subDir, modus);
                ret.AddRange(curItems);
            }   
            var files = Directory.GetFiles(pfad);
            foreach (var filePath in files)
            {
                string currentKey = CreateKey(filePath, modus);
                var item = new Item(currentKey, filePath);
                ret.Add(item);
            }
            return ret;
        }

        private string CreateKey(string filePath, Vergleichsmodi modus)
        {
            var fileInfo = new FileInfo(filePath);
            string ret = modus.ToString()+"::";
            if (modus == Vergleichsmodi.Groesse)
            {
                ret +=  fileInfo.Length.ToString();
            }
            else
            {
                ret += $"{fileInfo.Length}::{fileInfo.Name}";
            }
            return ret;
        }
    }
}