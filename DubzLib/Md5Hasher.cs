using System.Security.Cryptography;

namespace DubzLib
{
    internal class Md5Hasher : IHasher
    {
        public string Hash(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hash = md5.ComputeHash(stream);
                    return Convert.ToHexString(hash);
                }
            }
        }
    }
}