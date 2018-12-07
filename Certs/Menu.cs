using System;
using System.Collections.Generic;
using System.Text;

namespace Certs
{
    class Menu
    {
        CertController CC;
        FileIO fileStuff = new FileIO();
        //need one menu for general stuff then another for more specific rsa shit
        public Menu()
        {
            Console.WriteLine("To start, if you have not used this program when you enter your name it " +
                              "will create a directory under your name. If you have used this program before " +
                              "please enter your name and it will resume use of your previous directory.");
            BeginMenu();
        }
        private void BeginMenu()
        {
            Console.WriteLine("Would you like to perform DiffieHellman(1) or work with PKI certs(2).");
            var choice = Console.ReadKey().KeyChar;

            if (choice == '1')
            {
                DiffieHellmanMenu();
                return;
            }
            else if (choice == '2')
                CertMenu();
            else
                BeginMenu();
        }
        private void DiffieHellmanMenu()
        {
            DiffieHellman dh = new DiffieHellman();
        }
        private void CertMenu(string name = "")
        {
            fileStuff = new FileIO();
            Console.WriteLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Please enter your name: ");
                name = Console.ReadLine();
            }
            CC = new CertController(name);
            fileStuff.CreateDir(name);
            CC.GetUserData();
            Console.WriteLine("1. Detect a Forged Cert\n2. Detect Forged Rev List\n3. Get a Cert\n4. Check Requests" +
                "\n5. Logout \n6. Quit ");
            var choice = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (choice)
            {
                case '1':
                    {
                        CC.VerifyCert();
                        break;
                    }
                case '2':
                    {
                        CC.VerifyRevList();
                        break;
                    }
                case '3':
                    {
                        CC.GetCert();
                        break;
                    }
                case '4':
                    {
                        if (fileStuff.CheckRequests(name))
                            CC.HandleRequests();
                        else
                            Console.WriteLine("No requests!");
                        break;
                    }
                case '5':
                    {
                        BeginMenu();
                        break;
                    }
                case '6':
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        CertMenu(name);
                        break;
                    }
            }
            CertMenu(name);
        }
    }
}
