using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Certs
{
    class GenerateCert
    {
        Certificate Cert;
        string CAName;
        Guid CAId;
        string PrivKey;
        string N;
        IRSA RSA = new RSA();
        FileIO file = new FileIO();
        public GenerateCert(string caName, Guid id, string CAkey, string CAN)
        {
            CAName = caName;
            CAId = id;
            PrivKey = CAkey;
            N = CAN;
        }
        public Certificate CertGenny(string user, string pubkey, string n)
        {
            CreateCert(user, pubkey, n);
            HashCert();
            //SignCert();
            WriteCert(user, "Cert");
            WriteCert(CAName, $"{CAName}Generated{user}Cert");
            return Cert;
        }
        private void CreateCert(string user, string pubKey, string n)
        {
            Cert = new Certificate
            {
                CertID = Guid.NewGuid(),
                IssuerName = CAName,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddHours(4),
                SubjectName = user,
                PubKey = pubKey,
                N = n,
                IssuerID = CAId
            };
        }
        private void HashCert()
        {
            string serializedJson = JsonConvert.SerializeObject(Cert);
            var hash = Sha256.HashSha256(serializedJson); //need to encrypt 
            Cert.SignedCert = hash;
        }
        private void SignCert()
        {
            var signed = RSA.Encrypt(Cert.SignedCert, Convert.ToInt64(PrivKey), Convert.ToInt64(N));
            Cert.SignedCert = signed.ToString();
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
