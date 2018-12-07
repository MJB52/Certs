using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Certs
{
    /// <summary>
    /// at this point a cert has been proven to be not trust worthy so it is added to the revlist
    /// </summary>
    class AddCertToRevList
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Project2";
        string _userName;
        FileIO file = new FileIO();
        public AddCertToRevList(string name)
        {
            _userName = name;
            path += $"\\{_userName}";
        }
        public AddCertToRevList(string name, Certificate badCert)
        {
            _userName = name;
            path += $"\\{_userName}";
            var files = Directory.EnumerateFiles(path);
            var found = files.FirstOrDefault(c => c.ToUpper().Contains("REVLIST"));
            if (found == null)
            {
                CreateRevList();
                files = Directory.EnumerateFiles(path);
                found = files.FirstOrDefault(c => c.ToUpper().Contains("REVLIST"));
            }
            var text = File.ReadAllText(found);
            var revList = JsonConvert.DeserializeObject<RevocationList>(text);
            var revCert = new RevokedCert //for revokation list we only need these fields
            {
                SubjectName = badCert.SubjectName,
                RevocationDate = DateTime.Today,
                SerialNo = badCert.CertID
            };
            revList.RevList.Add(revCert);
            revList.LastUpdated = DateTime.Today;
            revList.NextUpdate = DateTime.Today.AddDays(1);
            var hashed = Sha256.HashSha256(JsonConvert.SerializeObject(revList));
            revList.SignedHash = hashed;

            var data = JsonConvert.SerializeObject(revList);
            file.WriteToDir(_userName, data, "RevList");

        }
        public void GetUserRevList()
        {
            var files = Directory.EnumerateFiles(path);
            var found = files.FirstOrDefault(c => c.ToUpper().Contains("REVLIST"));
            if (found == null)
                CreateRevList();
            else
            {
                var text = File.ReadAllText(found);
                var revList = JsonConvert.DeserializeObject<RevocationList>(text);
                foreach(var thing in revList.RevList)
                {
                    Console.WriteLine(thing.SubjectName + thing.SerialNo + thing.RevocationDate);
                }
            }
        }

        private void CreateRevList()
        {
            var revList = new RevocationList {
                IssuerName = _userName,
                LastUpdated = DateTime.Today,
                NextUpdate = DateTime.Today.AddDays(1),
            };
            var hashed = Sha256.HashSha256(JsonConvert.SerializeObject(revList));
            revList.SignedHash = hashed; //hash revlist

            var data = JsonConvert.SerializeObject(revList);
            file.WriteToDir(_userName, data, "RevList");
        }
    }
}
