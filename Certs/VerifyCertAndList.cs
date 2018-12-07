using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Certs
{
    class VerifyCertAndList
    {
        public void VerifyCert(string serializedCert)
        {
            var thing = JsonConvert.DeserializeObject<Certificate>(serializedCert);
            var hash = thing.SignedCert;
            thing.SignedCert = string.Empty;

            string serializedJson = JsonConvert.SerializeObject(thing);
            var reHash = Sha256.HashSha256(serializedJson);

            if (reHash == hash)
                Console.WriteLine("GoodShit");
            else
                Console.WriteLine("nah");
        }
    }
}
