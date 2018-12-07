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
        int PrivKey;
        int N;
        IRSA RSA = new RSA();
        FileIO file = new FileIO();
        public GenerateCert(string caName, Guid id, int privKey, int n)
        {
            CAName = caName;
            CAId = id;
            PrivKey = privKey;
            N = n;
        }
        public Certificate CertGenny(string user, int pubkey, int n, List<String> chain)
        {
            CreateCert(user, pubkey, n, chain);
            HashCert();
            SignCert();
            WriteCert(user);
            return Cert;
        }
        private void CreateCert(string user, int pubKey, int n, List<string> chain)
        {
            Cert = new Certificate
            {
                Chain = chain,
                CertID = Guid.NewGuid(),
                IssuerName = CAName,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddHours(4),
                SubjectName = user,
                PubKey = pubKey.ToString(),
                N = n.ToString(),
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
            var signed = RSA.Encrypt(Cert.SignedCert, PrivKey, N);
            Cert.SignedCert = signed.ToString();
        }
        private void WriteCert(string user)
        {
            string serializedCert = JsonConvert.SerializeObject(Cert);
            //serialize cert again
            file.WriteToDir(user, serializedCert);
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
