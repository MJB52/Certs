using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Certs
{
    class GenerateCert
    {
        Certificate Cert;
        string CAName;
        Guid CAId;
        RSAParameters PrivKey;
        FileIO file = new FileIO();
        public GenerateCert(string caName, Guid id, RSAParameters CAkey)
        {
            CAName = caName;
            CAId = id;
            PrivKey = CAkey;
        }
        public Certificate CertGenny(string user, RSAParameters pubkey)
        {
            CreateCert(user, pubkey);
            HashCert();
            WriteCert(user, "Cert");
            WriteCert(CAName, $"{CAName}Generated{user}Cert");
            return Cert;
        }
        private void CreateCert(string user, RSAParameters pubKey)
        {
            Cert = new Certificate
            {
                CertID = Guid.NewGuid(),
                IssuerName = CAName,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddHours(4),
                SubjectName = user,
                PubKey = pubKey,
                IssuerID = CAId
            };
        }
        private void HashCert()
        {
            string serializedJson = JsonConvert.SerializeObject(Cert);
            var hash = Sha256.HashSha256(serializedJson); //need to encrypt 
            Cert.SignedCert = RSA.Encrypt(Encoding.UTF8.GetBytes(hash), PrivKey);
        }
        private void WriteCert(string user, string type)
        {
            string serializedCert = JsonConvert.SerializeObject(Cert);
            //serialize cert again
            file.WriteToDir(user, serializedCert, type);
        }
        //read
        //verifychain
        //verifycert
        //modifycert
        //revokecert
        //createrevocationlist - one file with list of certs or maybe just certIds 
        //modifyrevocationlist
        //probably more idk

    }
}
