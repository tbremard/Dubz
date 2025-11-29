
using System.Reflection;
using System.IO;
using System.Security.Principal;

namespace DubzLib
{

    public class Dubletten: IDublettenpruefung
    {
        public IReadOnlyCollection<IDublette> PruefeKandidaten(IEnumerable<IDublette> kandidaten)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<IDublette> SammleKandidaten(string pfad)
        {
            var ret = SammleKandidaten(pfad, Vergleichsmodi.GroesseUndName);
            return ret;
        }

        public IReadOnlyCollection<IDublette> SammleKandidaten(string pfad, Vergleichsmodi modus)
        {
            List<Item> items = BuildItems(pfad, modus);            
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
            var directories = Directory.GetDirectories(pfad);
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