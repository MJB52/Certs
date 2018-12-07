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
        public string Decrypt(string message, BigInteger key, BigInteger n)
        {
            char[] ar = message.ToCharArray();
            int i = 0, j = 0;
            string c = "", dc = "";
            try
            {
                for (; i < ar.Length; i++)
                {
                    c = "";
                    for (j = i; ar[j] != '-'; j++)
                        c = c + ar[j];
                    i = j;
                    int xx = Convert.ToInt16(c);
                    dc = dc + ((char)BigMod(xx, key, n)).ToString();
                }
            }
            catch (Exception ex) { }
            return dc;
        }

        public string Encrypt(string message, BigInteger key, BigInteger n)
        {
            string hex = message;
            char[] ar = hex.ToCharArray();
            String c = "";
            for (int i = 0; i < ar.Length; i++)
            {
                if (c == "")
                    c = c + BigMod(ar[i], key, n);
                else
                    c = c + "-" + BigMod(ar[i], key, n);
            }
            return c;
        }

        public static BigInteger BigMod(BigInteger b, BigInteger p, BigInteger m) //b^p%m=?
        {
            if (p == 0)
                return 1;
            else if (p % 2 == 0)
                return BigInteger.Pow(BigMod(b, p / 2, m),2) % m;
            else
                return ((b % m) * BigMod(b, p - 1, m)) % m;
        }
    }
}
