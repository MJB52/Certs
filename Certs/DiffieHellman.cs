using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
namespace Certs
{
    class DiffieHellman
    {
        RandomBigPrimes Primes = new RandomBigPrimes();
        public DiffieHellman(long SomeonesPubKey, long q, long alpha, string name) //if another user initiates conversation
        {
            DHData DH = new DHData
            {
                Q = q,
                Alpha = alpha
            };
            //gen priv key 
            //get pub key
            //gen shared key
            //write other users dir
            //write to this users directory
        }


    }
    class DHData
    {
        public long PubKey { get; set; }
        public long Q { get; set; }
        public long PrivKey { get; set; }
        public long Alpha { get; set; }
        public long SharedKey { get; set; }
    }
}
