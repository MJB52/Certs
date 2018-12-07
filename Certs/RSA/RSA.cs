// RSA File
using System.Security.Cryptography;
using System.Text;

namespace Certs
{
    class RSA // IRSA //TODO since all we do is hash the output from sha256, we only have to worry about hex values..basically simplify this so it does rsa on the whole thing
    {
        public static string Decrypt(byte[] input, RSAParameters privateKey)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privateKey);
                decrypted = rsa.Decrypt(input, true);
            }
            return Encoding.UTF8.GetString(decrypted);

        }

        public static byte[] Encrypt(byte[] input, RSAParameters publicKey)
        {
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(publicKey);
                encrypted = rsa.Encrypt(input, true);
            }
            return encrypted;
        }
    }
}