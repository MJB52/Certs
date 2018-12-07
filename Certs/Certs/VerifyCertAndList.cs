using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Certs
{
    class VerifyCertAndList
    {
        public void VerifyCert(Certificate cert)
        {
            var hash = cert.SignedCert;
            cert.SignedCert = string.Empty;

            string serializedJson = JsonConvert.SerializeObject(cert);
            var reHash = Sha256.HashSha256(serializedJson);

            if (reHash == hash)
                Console.WriteLine("GoodShit");
            else
                Console.WriteLine("nah");
        }
    }
}
