using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Certs
{   
    interface IRSA
    {
        BigInteger Encrypt(string message, long key, BigInteger q);
        string Decrypt(BigInteger message, long key, BigInteger q);
    }
    class RSA : IRSA
    {
        public BigInteger Encrypt(string message, long key, BigInteger n)
        {
            var num = ConvertStringToNumeric(message);
            var inBetween = BigInteger.Pow(num, key);
            return BigInteger.Remainder(inBetween, n);
        }
        public string Decrypt(BigInteger message, long key, BigInteger n)
        {
            var inBetween = BigInteger.Pow(message, int.Parse(key.ToString()));
            var decryptedNum = BigInteger.Remainder(inBetween, n);
            return ConvertNumericToString(decryptedNum);
        }
        private BigInteger ConvertStringToNumeric(string message)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(message);
            return new BigInteger(asciiBytes);
        }
        private string ConvertNumericToString(BigInteger num)
        {
            Decoder d = Encoding.ASCII.GetDecoder();
            var b = num.ToByteArray();
            char[] str = new char[10000];
            d.GetChars(b,str, true);
//            d.GetChars(b, 0, b.Length, str, 0);
            return new string(str);
        }
    }
}
