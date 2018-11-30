using System;
using System.Numerics;

namespace Certs
{
    class Program
    {
        static void Main(string[] args) //Environment.GetCommandLineArgs
        {
            IGenerateRSAData genCert = new GenerateRSAData();
            var data = genCert.StrategyPattern();
            IRSA rsa = new RSA();
            var e = rsa.Encrypt("HiIamMike", data.E, data.N); // doesnt work right now idfk
            Console.WriteLine(e.ToString());
            var d = rsa.Decrypt(e, data.D, data.N);
            Console.WriteLine(d);
        }
    }
}
