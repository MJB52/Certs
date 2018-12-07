using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Certs
{
    class RequestCert
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Project2";
        FileIO filestuff = new FileIO();
        public RequestCert(CertRequest request)
        {
            var users = Directory.EnumerateDirectories(path);
            Console.WriteLine("Here are a list of users you could go get a cert from: ");
            foreach (var thing in users)
            {
                Console.WriteLine(thing);
            }
            Console.Write("Enter the name of the person you would like a cert from: ");
            var choice = Console.ReadLine();
            var found = users.FirstOrDefault(c => c.ToLower() == choice.ToLower());
            while (found == null)
            {
                Console.WriteLine("Would you like to get a cert from any of these folks");
                choice = Console.ReadLine();
                found = users.FirstOrDefault(c => c.ToLower() == choice.ToLower());
            }
            SendRequest(request, choice);
        }
        private void SendRequest(CertRequest request, string chosenCA)
        {
            filestuff.WriteToDir(chosenCA, JsonConvert.SerializeObject(request), "CertRequest");
        }
    }
}
