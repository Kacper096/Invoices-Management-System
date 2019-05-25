using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Model
{
    public static class PasswordVerification
    {
        /// <summary>
        /// It's creates hashed password
        /// </summary>
        /// <param name="password">Gets your password.</param>
        /// <returns>Hashed password</returns>
        public static byte[] CreatePasswordHash(string password)
        {
            
            if(!string.IsNullOrEmpty(password))
            {
                string trimedPass = password.Trim();

                byte[] bytes = Encoding.ASCII.GetBytes(trimedPass);
                var sha1 = new SHA1CryptoServiceProvider();

                var hashedPass = sha1.ComputeHash(bytes);

                return hashedPass;
            }
            throw new ArgumentException("Hasło nie może być puste");
        }

        /// <summary>
        /// It's compares string pass with hashed pass.
        /// </summary>
        /// <param name="attemptedPass"></param>
        /// <param name="hash"></param>
        /// <returns>True if passswords are the same.</returns>
        public static bool CompareHashedPassword(string attemptedPass, byte[] hash)
        {
            string passHash = Convert.ToBase64String(hash);
            string attemptedHashPass = Convert.ToBase64String(CreatePasswordHash(attemptedPass));
            
            return passHash == attemptedHashPass;
        }

        /// <summary>
        /// It will return true if password is compatible with pattern.
        /// </summary>
        /// <param name="password">Password which you want to check.</param>
        /// <returns></returns>
        public static bool CanBeAPassword(string password)
        {
            //It should have minimum 8 letter and maximum 22. At least one upper letter, one lower letter, one digit, one special character
            Regex regex = new Regex(@"^(?=.*\p{Ll})(?=.*\p{Lu})(?=.*\p{N})(?=.*[@$!%*?&_-])[A-Za-z\d@$!%*?&_-]{8,22}$");

            return regex.IsMatch(password);
        }
    }
}
