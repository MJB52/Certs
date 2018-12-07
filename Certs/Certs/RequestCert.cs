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
        //puts a request in a specific users inbox
        public RequestCert(CertRequest request)
        {
            var users = Directory.EnumerateDirectories(path);
            Console.WriteLine("Here are a list of users you could go get a cert from: ");
            foreach (var thing in users)
            {
                var actualName = thing.Substring(thing.LastIndexOf('\\') + 1);
                if (actualName.ToUpper().Trim() != request.Name.ToUpper().Trim())
                    Console.WriteLine(actualName);
            }
            string found = null;
            string choice = null;
            while (found == null) //ensures a user is actually selected
            {
                Console.Write("Enter the name of the person you would like a cert from: ");
                choice = Console.ReadLine();
                found = users.FirstOrDefault(c => c.Substring(c.LastIndexOf('\\')+1).ToLower() == choice.ToLower());
            }
            SendRequest(request, choice);
        }
        private void SendRequest(CertRequest request, string chosenCA)
        {
            filestuff.WriteToDir(chosenCA, request.Name, JsonConvert.SerializeObject(request), "CertRequest");
            Console.WriteLine($"Cert request sent to {chosenCA}. ");
        }
    }
}
