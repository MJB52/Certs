using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Certs
{
    class CertController
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Project2";
        string userPath;
        Guid CAGuid = Guid.NewGuid();
        GenerateCert _generateCert;
        RequestCert _requestCert;
        VerifyCertAndList _verify;
        FileIO fileStuff = new FileIO();
        RSAData data = new GenerateRSAData().StrategyPattern();
        string _userName;
        public CertController(string userName)
        {
            userPath = path +  $"\\{userName}";
            _userName = userName;
        }
        public void GenerateCert(string name, string pubKey, string n)
        {
            _generateCert = new GenerateCert(_userName, CAGuid, data.E.ToString(), data.N.ToString());
            var temp = _generateCert.CertGenny(name, pubKey, n);
            fileStuff.WriteToDir(name, JsonConvert.SerializeObject(temp), "Cert");
        }
        public void GetCert()
        {
            var users = Directory.EnumerateDirectories(userPath);
            Console.WriteLine("Here are a list of users you could go get a cert from: ");
            foreach(var thing in users)
            {
                Console.WriteLine(thing);
            }
            Console.Write("Enter the name of the person you would like a cert from: ");
            var choice = Console.ReadLine();
            _requestCert = new RequestCert(new CertRequest
            {
                Name = _userName,
                publicKey = data.D.ToString(),
                N = data.N.ToString()
            });
        }
        public void VerifyRevList()
        {

        }
        public void VerifyCert()
        {
            _verify = new VerifyCertAndList();
            var files = Directory.EnumerateFiles(userPath);
            List<string> certFiles = new List<string>();
            foreach (var thing in files)
            {
                if (thing.ToUpper().Contains("CERT"))
                    certFiles.Add(thing);
            }
            if (certFiles.Count != 0) {
                foreach (var thing in certFiles)
                {
                    var fText = File.ReadAllText(thing);
                    var file = JsonConvert.DeserializeObject<Certificate>(fText);
                    Console.WriteLine(file.SubjectName);
                }
                Console.Write("Enter the name of the cert you would like to verify or type all to verify all: ");
                var choice = Console.ReadLine();
                if(choice.ToUpper() == "ALL")
                    foreach(var thing in certFiles)
                    {
                        var fText = File.ReadAllText(thing);
                        var file = JsonConvert.DeserializeObject<Certificate>(fText);
                        _verify.VerifyCert(file);
                    }
                else
                {
                    foreach(var thing in certFiles)
                    {
                        var fText = File.ReadAllText(thing);
                        var file = JsonConvert.DeserializeObject<Certificate>(fText);
                        if(file.SubjectName.ToUpper() == choice.ToUpper())
                            _verify.VerifyCert(file);
                    }
                }
                
            }
        }

        public void HandleRequests()
        {
            var files = Directory.EnumerateFiles(userPath);
            List<string> certRequestFiles = new List<string>();
            foreach(var thing in files)
            {
                if (thing.ToUpper().Contains("CERTREQUEST"))
                    certRequestFiles.Add(thing);
            }
            if(certRequestFiles.Count != 0)
                foreach(var thing in certRequestFiles)
                {
                    var fText = File.ReadAllText(thing);
                    var request = JsonConvert.DeserializeObject<CertRequest>(fText);
                    GenerateCert(request.Name, request.publicKey, request.N);
                }
        }
    }
}
