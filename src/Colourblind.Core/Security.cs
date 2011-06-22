using System;
using System.Security.Cryptography;
using System.Text;

namespace Colourblind.Core
{
    public class Security
    {
        // Just for reference - 'LQ4/uCiyHKMAL4i6shtWMcWiOuvz4Vs=' is 'password' ;  )

        const int SALT_SIZE = 3;

        public static string GenerateHash(string password)
        {
            return GenerateHash(password, GetRandomString(SALT_SIZE));
        }
        
        public static string GenerateHash(string password, string salt)
        {
            string saltedPassword = salt + password;

            byte[] saltedPasswordBytes = Encoding.ASCII.GetBytes(saltedPassword);

            // SHA256 and SHA512 throw an exception, so we're using HoboCrypto. Huzzah.
            HashAlgorithm hasher = new SHA1CryptoServiceProvider();
            byte[] hash = hasher.ComputeHash(saltedPasswordBytes);
            byte[] finalHash = new byte[hash.Length + salt.Length];

            Encoding.ASCII.GetBytes(salt).CopyTo(finalHash, 0);
            hash.CopyTo(finalHash, SALT_SIZE);
            
            return Convert.ToBase64String(finalHash);
        }
        
        public static bool CheckHash(string password, string hash)
        {
            if (hash.Length < SALT_SIZE)
                return false;

            byte[] decodedHash = Convert.FromBase64String(hash);
            string salt = Encoding.ASCII.GetString(decodedHash, 0, SALT_SIZE);
            
            return hash == GenerateHash(password, salt);
        }
        
        public static string GetRandomString(int length)
        {
            byte[] buffer = new byte[length];
            Random random = new Random((int)DateTime.Now.Ticks);
            random.NextBytes(buffer);
            return Encoding.ASCII.GetString(buffer);
        }
        
        public static string GetRandomString(int length, string pool)
        {
            string result = String.Empty;
            Random random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < length; i ++)
                result += pool[random.Next(pool.Length)];
            return result;
        }
    }
}
