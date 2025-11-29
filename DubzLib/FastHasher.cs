using System.Security.Cryptography;

namespace DubzLib
{
    internal class FastHasher : IHasher
    {
        public string Hash(string filePath)
        {
            const int FAST_HASH_SIZE = 10000;
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filePath))
                    {
                        var buffer = new byte[FAST_HASH_SIZE];
                        int bytesRead = stream.Read(buffer, 0, FAST_HASH_SIZE);
                        if (bytesRead < FAST_HASH_SIZE)
                        {
                            Array.Resize(ref buffer, bytesRead);
                        }
                        byte[] hash = md5.ComputeHash(buffer);
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