using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Certs
{
    class VerifyCertAndList
    {
        public bool VerifyCert(Certificate cert, RSAParameters pubKey)
        {
            try
            {
                var hash = RSA.Decrypt(cert.SignedCert, pubKey); //decrypt hash to compare
                cert.SignedCert = null;
                string serializedJson = JsonConvert.SerializeObject(cert);  //rehash

                var reHash = Sha256.HashSha256(serializedJson);

                if (hash != reHash)
                {
                    Console.WriteLine("Cert was altered.");
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
            try
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
            catch
            {
                Console.WriteLine("Revocation list was altered. ");
                return false;
            }

        }
    }
}
