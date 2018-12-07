using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Certs
{
    class CertController
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Project2";
        string userPath;
        Guid CAGuid;
        GenerateCert _generateCert;
        RequestCert _requestCert;
        VerifyCertAndList _verify;
        AddCertToRevList revList;
        FileIO fileStuff = new FileIO();
        RSAData data = new RSAData();
        string _userName;
        public CertController(string userName)
        {
            userPath = path +  $"\\{userName}";
            _userName = userName;
        }

        public void GetUserData()
        {
            var files = Directory.EnumerateFiles(userPath);
            var found = files.FirstOrDefault(c => c.ToUpper().Contains("USERDATA"));
            if (found != null)
            {
                var fText = File.ReadAllText(found);
                var file = JsonConvert.DeserializeObject<UserData>(fText);
                data.publicKey = file.pubKey;
                data.privateKey = file.privKey;
                CAGuid = file.CAGuid;
            }
            else
            {
                data = new GenerateRSAData().generateKeys();
                var userData = new UserData
                {
                    CAGuid = Guid.NewGuid(),
                    pubKey = data.publicKey,
                    privKey = data.privateKey
                };
                var serializedData = JsonConvert.SerializeObject(userData);
                CAGuid = userData.CAGuid;
                fileStuff.WriteToDir(_userName, serializedData, "UserData");
            }
        }
            

        public void GenerateCert(string name, RSAParameters pubKey)
        {
            _generateCert = new GenerateCert(_userName, CAGuid, data.privateKey);
            var temp = _generateCert.CertGenny(name, pubKey);
            fileStuff.WriteToDir(name, JsonConvert.SerializeObject(temp), "Cert");
        }
        public void GetCert()
        {
            _requestCert = new RequestCert(new CertRequest
            {
                Name = _userName,
                publicKey = data.publicKey
            });
        }
        public void VerifyRevList()
        {
            _verify = new VerifyCertAndList();
            var files = Directory.EnumerateFiles(userPath);
            foreach (var thing in files)
            {
                if (thing.ToUpper().EndsWith("REVLIST.TXT"))
                {
                    var fText = File.ReadAllText(thing);
                    var file = JsonConvert.DeserializeObject<RevocationList>(fText);
                    if (_verify.VerifyRevList(file))
                        Console.WriteLine("List was not mutated in anyway");
                }

            }
        }


        public void VerifyCert()
        {
            _verify = new VerifyCertAndList();
            var files = Directory.EnumerateFiles(userPath);
            List<string> certFiles = new List<string>();
            foreach (var thing in files)
            {
                if (thing.ToUpper().EndsWith("CERT.TXT"))
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
                        if (!_verify.VerifyCert(file, data.publicKey))
                            revList = new AddCertToRevList(_userName, file);
                        else
                            Console.WriteLine($"{file.CertID} is still valid. ");
                    }
                else
                {
                    foreach(var thing in certFiles)
                    {
                        var fText = File.ReadAllText(thing);
                        var file = JsonConvert.DeserializeObject<Certificate>(fText);
                        if (file.SubjectName.ToUpper() == choice.ToUpper())
                        {
                            if (!_verify.VerifyCert(file, data.publicKey))
                                revList = new AddCertToRevList(_userName, file);
                            else
                                Console.WriteLine($"{file.CertID} is still valid. ");
                        }
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
                    Console.WriteLine($"Request from {request.Name} to generate a certificate. Would you like to generate this cert? (y/n)");
                    if (Console.ReadKey().KeyChar == 'y')
                    {
                        GenerateCert(request.Name, request.publicKey);
                        File.Delete(thing);
                    }
                }
        }
    }
}
