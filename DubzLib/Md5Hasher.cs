using System.Security.Cryptography;

namespace DubzLib
{
    internal class Md5Hasher : IHasher
    {
        public string Hash(string filePath)
        {
            try
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
            catch (Exception)
            {
                return null; //System.IO.IOException: The process cannot access the file 'E:\xxxxxxxxxx\2025\~WRL2822.tmp' because it is being used by another process.
            }
        }
    }
}