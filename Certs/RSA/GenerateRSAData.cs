using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Certs
{
    class GenerateRSAData
    {
        public RSAData generateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                return new RSAData
                {
                    publicKey = rsa.ExportParameters(false),
                    privateKey = rsa.ExportParameters(true)
                };
            }
        }
    }


    class RSAData
    {
        public RSAParameters publicKey;
        public RSAParameters privateKey;
    }
}
