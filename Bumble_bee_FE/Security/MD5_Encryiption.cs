using System.Security.Cryptography;
using System.Text;

namespace Bumble_bee_FE.Encryption
{
    public class MD5_Encryiption
    {
        public static string Encrypt(string str)
        {
            MD5 md5 = MD5.Create();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(str));
            byte[] result = md5.Hash;

            return Convert.ToBase64String(result);
        }
    }
}
