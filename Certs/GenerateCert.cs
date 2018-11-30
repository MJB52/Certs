using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Certs
{
    class GenerateCert
    {
        Certificate Cert;
        string CAName;
        Guid CAId;
        long PrivKey;
        BigInteger N;
        IRSA RSA = new RSA();
        FileIO file = new FileIO();
        public GenerateCert(string caName, Guid id, long privKey, BigInteger n)
        {
            CAName = caName;
            CAId = id;
            PrivKey = privKey;
            N = n;
        }
        public Certificate CertGenny(string user, long pubkey, BigInteger n, List<String> chain)
        {
            CreateCert(user, pubkey, n, chain);
            HashCert();
            SignCert();
            WriteCert(user);
            return Cert;
        }
        private void CreateCert(string user, long pubKey, BigInteger n, List<string> chain)
        {
            Cert = new Certificate{
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
            var hash = Sha256Algo.Hash(serializedJson);
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
        //getchain - build on chain as we create certs..if this cert has x chain next cert would x + 1 chain
        //verifychain
        //verifycert
        //modifycert
        //revokecert
        //createrevocationlist - one file with list of certs or maybe just certIds 
        //modifyrevocationlist
        //probably more idk

    }
}
