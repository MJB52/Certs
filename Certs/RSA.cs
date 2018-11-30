using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Certs
{   
    interface IRSA
    {
        BigInteger Encrypt(string message, BigInteger key, BigInteger q);
        string Decrypt(BigInteger message, BigInteger key, BigInteger q);
    }
    class RSA : IRSA
    {
        public BigInteger Encrypt(string message, BigInteger key, BigInteger q)
        {

        }
        public string Decrypt(BigInteger message, BigInteger key, BigInteger q)
        {

        }
        private BigInteger ConvertStringToNumeric(string message)
        {
            string asciiMessage = string.Empty;
            for (int i = 0; i < message.Length; i++)
                asciiMessage += ((int)message[i]).ToString();
            return new BigInteger()
        }
        private string ConvertNumericToString(BigInteger num)
        {

        }
    }
}
