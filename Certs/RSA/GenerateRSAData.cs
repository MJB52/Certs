using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Certs
{
    class GenerateRSAData
    {
        //generates keys by using a call to the system
        public RSAData generateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048)) //opens up a stream
            {
                rsa.PersistKeyInCsp = false;
                return new RSAData
                {
                    publicKey = rsa.ExportParameters(true),
                    privateKey = rsa.ExportParameters(false)
                };
            }
        }
    }

    //model for keeping track of rsaData
    class RSAData
    {
        public RSAParameters publicKey;
        public RSAParameters privateKey;
    }
}
