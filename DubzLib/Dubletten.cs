
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
            List<Item> ret = BuildItems(pfad, modus);
            return null;
        }

        public List<Item> BuildItems(string pfad, Vergleichsmodi modus)
        {
            var directories = Directory.GetDirectories(pfad);
            var ret = new List<Item>();
            foreach (var subDir in directories)
            {
                string curPath = Path.Combine(pfad, subDir);
                var curItems = BuildItems(curPath, modus);
                ret.AddRange(curItems);
            }   
            var files = Directory.GetFiles(pfad);
            foreach (var file in files)
            {
                string currentKey = CreateKey(file, modus);
                string curPath = Path.Combine(pfad, file);
                var item = new Item(currentKey, curPath);
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
            else // GroesseUndName
            {
                ret += $"{fileInfo.Length}_{Path.GetFileName(filePath)}";
            }
            return ret;
        }
    }
}