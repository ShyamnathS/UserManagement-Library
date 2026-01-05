using System;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace UserManagement.Lib.Helpers
{
    public static class PasswordHasher
    {
        public static (string hash, string salt) HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) { rng.GetBytes(salt); }

            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = 4;
                argon2.Iterations = 4;
                argon2.MemorySize = 65536;
                return (Convert.ToBase64String(argon2.GetBytes(32)), Convert.ToBase64String(salt));
            }
        }

        public static bool VerifyPassword(string password, string salt, string hash)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon2.Salt = saltBytes;
                argon2.DegreeOfParallelism = 4;
                argon2.Iterations = 4;
                argon2.MemorySize = 65536;
                return Convert.ToBase64String(argon2.GetBytes(32)) == hash;
            }
        }
    }
}