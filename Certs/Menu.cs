using System;
using System.Collections.Generic;
using System.Text;

namespace Certs
{
    class Menu
    {
        //need one menu for general stuff then another for more specific rsa shit
        public Menu()
        {
            Console.WriteLine("To start, if you have not used this program when you enter your name it " +
                              "will create a directory under your name. If you have used this program before " +
                              "please enter your name and it will resume use of your previous directory." +
                              "\nPlease enter your name: ");
            var name = Console.ReadLine();
        }
        private void BeginMenu(string name)
        {
            Console.WriteLine($"Hello {name} would you like to perform DiffieHellman(1) or work with PKI certs(2).");
            var choice = Console.ReadKey().KeyChar;
            if(choice == '1')
            {
                DiffieHellmanMenu(name);
                return;
            }
            CertMenu(name);
        }
        private void DiffieHellmanMenu(string name)
        {

        }
        private void CertMenu(string name)
        {

        }
    }
}
