using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace RudesWebapp.Controllers
{
    public class UserController
    {
        public UserController()
        {
           
        }
        [HttpPost]
        public String HashPassword(string password)

        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string passwordHash = Convert.ToBase64String(hashBytes);
            return passwordHash;
        }
        public Boolean ReturnPassword(string hashPassword, string inputPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, salt);
            byte[] hash = pbkdf2.GetBytes(20);

            // checking if they match
            Boolean match = true;
            for(int i=0;i<20;i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    match = false;
            }
            return match;
        }
    }
}
