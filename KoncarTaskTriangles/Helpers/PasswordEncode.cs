using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace KoncarTaskTriangles.Helpers
{
    public class PasswordEncode
    {
        public string Encoder(string pass)
        {
            string toHash = ":OSK:" + pass + ":OSK:";
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(toHash));
                foreach (Byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
