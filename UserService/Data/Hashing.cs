using System.Security.Cryptography;
using System.Text;

namespace UserService.Data
{
    public static class Hashing
    {
        public static void Create(string data, out byte[] hash, out byte[] salt)
        {
            var hmac = new HMACSHA512();
            salt = hmac.Key;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(data);
            hash = hmac.ComputeHash(passwordBytes);
        }

        public static bool Verify(string password, byte[] storedHash, byte[] storedSalt)
        {
            var hmac = new HMACSHA512(storedSalt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] computedHash = hmac.ComputeHash(passwordBytes);
            bool hashesMatch = computedHash.SequenceEqual(storedHash);
            return hashesMatch;
        }
    }
}