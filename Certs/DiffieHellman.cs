using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Certs
{
    class DiffieHellman
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
        RandomBigPrimes Primes = new RandomBigPrimes();
        string user;

        public DiffieHellman() //if another user initiates conversation
        {
            Console.Write("What is your name: ");
            user = Console.ReadLine();

            DisplayMenu();

            //DHData DH = new DHData
            //{
            //    Q = q,
            //    Alpha = alpha
            //};


        }

        private void DisplayMenu()
        {
            Console.WriteLine("1) Make Request");
            Console.WriteLine("2) Check Request");
            Console.Write("Select Request: ");
            var request = Console.ReadLine();

            if (request == "1")
                MakeRequest();
            else if (request == "2")
                CheckRequest();
        }

        private void MakeRequest()
        {
            List<string> list = new List<string>();

            var q = Primes.GetRandomPrime();
            var alpha = Primes.GetRandomPrime();

            Console.Write("Who would you like to share a key with: ");
            var name = Console.ReadLine();

            list.Add(name);
            list.Add(q.ToString());
            list.Add(alpha.ToString());

            var privateKey = 10;//Primes.GetRandomPrime();
            var temp = Math.Pow(alpha, privateKey);
            var publicKey = temp % q;

            list.Add(privateKey.ToString());
            list.Add(publicKey.ToString());

            File.WriteAllLines(path + name + ".txt", list);
            Console.WriteLine("Key stored and ready for " + name);
            Console.ReadLine();

            var lines = File.ReadAllLines(path + user + ".txt");

            var shared = Math.Pow(Int32.Parse(lines[4]), privateKey);
            shared = shared % q;

            list.Add(shared.ToString());
            File.WriteAllLines(path + user + ".txt", list);

        }

        private void CheckRequest()
        {
            List<string> list = new List<string>();
            var lines = File.ReadAllLines(path + user + ".txt");
            list.Add(user);

            var q = lines[1];
            var alpha = lines[2];
            var friendsPublicKey = lines[4];
            list.Add(q);
            list.Add(alpha);

            var privateKey = Primes.GetRandomPrime();
            var publicKey = Math.Pow(Int32.Parse(alpha), privateKey);
            publicKey = publicKey % Int32.Parse(q);
            list.Add(privateKey.ToString());
            list.Add(publicKey.ToString());

            var shared = Math.Pow(Int32.Parse(friendsPublicKey), privateKey);
            shared = shared % Int32.Parse(q);
            list.Add(shared.ToString());

            File.WriteAllLines(path + lines[0] + ".txt", list);
        }
    }

    class DHData
    {
        public long PubKey { get; set; }
        public long Q { get; set; }
        public long PrivKey { get; set; }
        public long Alpha { get; set; }
        public long SharedKey { get; set; }
    }
}
