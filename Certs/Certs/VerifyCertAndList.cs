using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Certs
{
    class VerifyCertAndList
    {
        public bool VerifyCert(Certificate cert)
        {
            var hash = cert.SignedCert;
            cert.SignedCert = string.Empty;
            try
            {
                string serializedJson = JsonConvert.SerializeObject(cert);

                var reHash = Sha256.HashSha256(serializedJson);

                if (hash != reHash)
                {
                    Console.WriteLine("Cert was altered.");
                    return false;
                }

                if (DateTime.Today < cert.StartDate)
                {
                    Console.WriteLine($"Cert is not valid until {cert.StartDate}. ");
                    return false;
                }
                if (DateTime.Today > cert.EndDate)
                {
                    Console.WriteLine($"Cert expired on {cert.EndDate}. ");
                    return false;
                }
                return true;
            }
            catch
            {
                Console.WriteLine("Cert was altered.");
                return false;
            }
        }
        public bool VerifyRevList(RevocationList list)
        {
            var hash = list.SignedHash;
            list.SignedHash = string.Empty;

            string serializedJson = JsonConvert.SerializeObject(list);
            var reHash = Sha256.HashSha256(serializedJson);
            if (hash != reHash)
            {
                Console.WriteLine("Revocation list was altered.");
                return false;
            }
            return true;
        }
    }
}
