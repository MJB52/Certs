using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Certs
{   
    interface IRSA
    {
        string Encrypt(string message, BigInteger key, BigInteger n);
        string Decrypt(string message, BigInteger key, BigInteger n);
    }
    class RSA : IRSA //TODO since all we do is hash the output from sha256, we only have to worry about hex values..basically simplify this so it does rsa on the whole thing
    {
        System.Globalization.NumberStyles _num = System.Globalization.NumberStyles.HexNumber;
        public string Encrypt(string message, BigInteger key, BigInteger n)
        {
            var hexNum = BigInteger.Parse(message, _num);
            return FastExponentiation(hexNum, key, n).ToString();
            
        }

        public string Decrypt(string message, BigInteger key, BigInteger n)
        {
            var hexNum = BigInteger.Parse(message, _num);
            return FastExponentiation(hexNum, key, n).ToString();
        }
        private BigInteger FastExponentiation(BigInteger baseNum, BigInteger exponent, BigInteger mod)
        {
            string exp = exponent.ToBinaryString();
            BigInteger temp = baseNum;
            for(int i = 1; i < exp.Length; i++)
            {
               BigInteger x = BigInteger.Multiply(temp, temp);
               if (exp[i] == '1')
                    x = BigInteger.Multiply(x, baseNum);
                temp = BigInteger.Remainder(x, mod);
            }
            return temp;
        }
    }
}
