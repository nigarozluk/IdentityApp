using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnIdentityApp.Library.Functions
{
    public class PasswordHasher
    {
        public static string PasswordHash(string password)
        {
            var data = Encoding.ASCII.GetBytes(password);
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(data);
            ASCIIEncoding ascii = new ASCIIEncoding();
            var hashedPassword = ascii.GetString(md5data);
            return hashedPassword;
        }
    }
}
