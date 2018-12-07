using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Certs
{   
    interface IRSA
    {
        byte [] Encrypt(string message, BigInteger key, BigInteger n);
        byte [] Decrypt(string message, BigInteger key, BigInteger n);
    }
    class RSA : IRSA //TODO since all we do is hash the output from sha256, we only have to worry about hex values..basically simplify this so it does rsa on the whole thing
    {
        System.Globalization.NumberStyles _num = System.Globalization.NumberStyles.HexNumber;
        System.Security.Cryptography.RSA rsaInstance = System.Security.Cryptography.RSA.Create();
        System.Security.Cryptography.RSAParameters rsaParams = new System.Security.Cryptography.RSAParameters();
        public byte [] Encrypt(string message, BigInteger key, BigInteger n)
        {
            //rsaParams.Exponent = key;
            var result = Encoding.Unicode.GetBytes(message);
            rsaInstance.ImportParameters(rsaParams);
            return new byte[10];
        }

        public byte [] Decrypt(string message, BigInteger key, BigInteger n)
        {
            return new byte[10];
        }
    }
}
