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
        public string Encrypt(string message, BigInteger key, BigInteger n)
        {
            BigInteger[] m = new BigInteger[message.Length];
            string encryptMessage = string.Empty;
            for(int i = 0; i<message.Length; i++)
            {
                m[i] = FastExponentiation(message[i], key, n);
                encryptMessage += m[i].ToString().PadLeft(16,'0');
            }
            return encryptMessage;
        }

        public string Decrypt(string message, BigInteger key, BigInteger n)
        {
            BigInteger[] m = new BigInteger[message.Length];
            string decrypt = string.Empty;
            int count = 0;
            while(count < message.Length -16)
            {
                m[count] = FastExponentiation(BigInteger.Parse(message.Substring(count,count +16)), key, n);
                decrypt += m[count];
                Console.WriteLine(m[count]);
                count += 12;
            }
            return decrypt;
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
                if (temp.ToString().Length > 20)
                    Console.WriteLine("fuck");
            }
            return temp;
        }
        //private byte[] ConvertStringToNumeric(string message)
        //{
        //    return Encoding.ASCII.GetBytes(message);
        //}
        //private string ConvertNumericToString(string num)
        //{
        //    //Decoder d = Encoding.ASCII.GetDecoder();
        //    //char[] str = num.T
        //    ////d.GetChars(b,str, true);
        //    //d.GetChars(b, 0, b.Length, str, 0);
        //    //return new string(str);
        //    return Encoding.ASCII.GetString();
        //}
    }
}
