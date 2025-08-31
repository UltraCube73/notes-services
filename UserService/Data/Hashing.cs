using System.Security.Cryptography;
using System.Text;

namespace UserService.Data
{
    public static class Hashing
    {
        public class HashResult
        {
            public HashResult(byte[] hash, byte[] salt)
            {
                Hash = hash;
                Salt = salt;
            }
            public readonly byte[] Hash;
            public readonly byte[] Salt;
        }

        public static HashResult Create(string data)
        {
            var hmac = new HMACSHA512();
            byte[] salt = hmac.Key;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = hmac.ComputeHash(passwordBytes);
            return new HashResult(hash, salt);
        }

        public static bool Verify(string data, byte[] hash, byte[] salt)
        {
            var hmac = new HMACSHA512(salt);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] computedHash = hmac.ComputeHash(dataBytes);
            bool hashesMatch = computedHash.SequenceEqual(hash);
            return hashesMatch;
        }
    }
}