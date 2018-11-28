using System;
using System.Numerics;

namespace Certs
{
    class Program
    {
        static void Main(string[] args) //Environment.GetCommandLineArgs
        {
            IRSA genCert = new RSA();
            var data = genCert.StrategyPattern();
            Console.WriteLine("e: "+data.E);
            Console.WriteLine("d: "+data.D);
            Console.WriteLine("n: "+data.N);
            Console.WriteLine("nt: "+data.NTosh);
            Console.WriteLine("p: "+data.P);
            Console.WriteLine("q: "+data.Q);
        }
    }
}
