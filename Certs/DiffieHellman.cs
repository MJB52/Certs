using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Certs
{
    class DiffieHellman
    {
        //Order of Contents in file
        // Name
        // q
        // alpha
        // private key
        // public key
        // shared key
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Project2\\";
        RandomBigPrimes Primes = new RandomBigPrimes();
        string user;

        public DiffieHellman()
        {
            Console.WriteLine();
            Console.Write("What is your name: ");
            user = Console.ReadLine();

            DisplayMenu();
        }

        private void DisplayMenu()
        {
            Console.WriteLine("1) Make Request");
            Console.WriteLine("2) Check Request");
            Console.Write("Select Request: ");
            var request = Console.ReadLine();

            // User Choice
            if (request == "1")
                MakeRequest();
            else if (request == "2")
                CheckRequest();
        }

        private void MakeRequest()
        {
            // Created Storage for items
            List<string> list = new List<string>();

            var q = 18757;
            var alpha = 6;
            // Read Names
            var names = Directory.EnumerateDirectories(path);
            Console.WriteLine("Here are a list of users you could start a DH key exchange with: ");
            foreach (var thing in names)
            {
                var actualName = thing.Substring(thing.LastIndexOf('\\') + 1);
                if (actualName.ToUpper().Trim() != user.ToUpper().Trim())
                    Console.WriteLine(actualName);
            }
            Console.Write("Who would you like to share a key with: ");
            var name = Console.ReadLine();

            // Start adding items to be stored in the file
            list.Add(name);
            list.Add(q.ToString());
            list.Add(alpha.ToString());
            
            // Generate Keys
            var privateKey = Primes.GetRandomPrime();
            var publicKey = FastExponent(alpha, privateKey, q);//temp % q;

            list.Add(privateKey.ToString());
            list.Add(publicKey.ToString());

            File.WriteAllLines(path + "//" + name + "//" + name + "DH.txt", list);
            Console.WriteLine("Key stored and ready for " + name);
            Console.ReadLine();

            // Read info from other user
            var lines = File.ReadAllLines(path + "//" + name + "//" + name + "DH.txt");
            //Calculate shared key
            var shared = FastExponent(Int32.Parse(lines[4]), privateKey, q);

            list.Add(shared.ToString());
            list[0] = user;
            // Rewrite all lines to original user
            File.WriteAllLines(path + "//" + user + "//" + user + "DH.txt", list);

            Console.WriteLine("Diffie Hellman Completed");
            Console.ReadLine();

        }

        private void CheckRequest()
        {
            // Same style data
            List<string> list = new List<string>();
            // Reads info from original user and stores values into variables
            var lines = File.ReadAllLines(path + "//" + user + "//" + user + "DH.txt");
            list.Add(user);

            var q = lines[1];
            var alpha = lines[2];
            var friendsPublicKey = lines[4];
            list.Add(q);
            list.Add(alpha);
            
            // Generate Keys
            var privateKey = Primes.GetRandomPrime();
            var publicKey = FastExponent(Int32.Parse(alpha), privateKey, Int32.Parse(q));

            list.Add(privateKey.ToString());
            list.Add(publicKey.ToString());

            var shared = FastExponent(Int32.Parse(friendsPublicKey), privateKey, Int32.Parse(q));
            list.Add(shared.ToString());

            File.WriteAllLines(path + "//" + lines[0] + "//" + lines[0] + "DH.txt", list);

            Console.WriteLine(user + " has completed the exchange");
            Console.ReadLine();
        }

        public static long FastExponent(long b, long p, long m) //b^p%m=?
        {
            var bS = Convert.ToString(p, 2);
            long temp = b;
            for (int i = 1; i < bS.Length; i++)//bS.Length - 2; i >= 0; i--)
            {
                temp = temp * temp;
                if (bS[i] == '1')
                    temp = temp * b;
                temp = temp % m;
            }
            return temp;
        }
    }
}
