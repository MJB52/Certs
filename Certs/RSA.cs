using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Certs
{   
    interface IRSA
    {
        string Encrypt(string message, long key, long n);
        string Decrypt(string message, long key, long n);
    }
    class RSA : IRSA //TODO since all we do is hash the output from sha256, we only have to worry about hex values..basically simplify this so it does rsa on the whole thing
    {
        public string Decrypt(string message, long key, long n)
        {
            message = message.Substring(10);
            char[] ar = message.Substring(0, message.Length - 2).ToCharArray();
            int[] newAr = new int[ar.Length];
            for (int i = 0; i < newAr.Length; i++)
                newAr[i] = Convert.ToInt32(ar[i].ToString(), 16);
            String m = "";
            for (int i = 0; i < newAr.Length; i++)
            {
                m += FastExponent(newAr[i], key, n);
            }
            return m;

        }

        public string Encrypt(string message, long key, long n)
        {
            string hex = message;
            char[] ar = hex.ToCharArray();
            int[] newAr = new int[ar.Length];
            for (int i = 0; i < ar.Length; i++)
                newAr[i] = Convert.ToInt32(ar[i].ToString(),16);
            String c = "";
            for (int i = 0; i < newAr.Length; i++)
            {
               c += FastExponent(newAr[i], key, n);
            }
            return c;
        }

        public static long FastExponent(long b, long p, long m) //b^p%m=?
        {
            var bS = Convert.ToString(p, 2);
            long temp = b;
            for(int i = bS.Length - 2; i >= 0; i--)
            {
                temp = temp * temp;
                if (bS[i] == '1')
                    temp = temp * b;
                temp = temp % m;
            }
            return temp;
            //if (p == 0)
            //    return 1;
            //else if (p % 2 == 0)
            //    return BigInteger.Pow(BigMod(b, p / 2, m),2) % m;
            //else
            //    return ((b % m) * BigMod(b, p - 1, m)) % m;
        }
    }
}
