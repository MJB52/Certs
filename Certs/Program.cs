using Newtonsoft.Json;
using System;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Certs
{
    class Program
    {
        static void Main(string[] args) //Environment.GetCommandLineArgs
        {
            IGenerateRSAData genCert = new GenerateRSAData();
            var data = genCert.StrategyPattern();
            IRSA rsa = new RSA();
            var hash = Sha256.HashSha256("Hello World");
            Console.WriteLine(hash);
            var e = rsa.Encrypt(hash, data.E,data.N); // doesnt work right now idfk
            var test = new TestObj();
            test.thing = e;
            string ser = JsonConvert.SerializeObject(test);
            Console.WriteLine(ser);

            var d = rsa.Decrypt(ser, data.D,data.N); // doesnt work right now idfk
            var test2 = new TestObj();
            test2.thing = d;
            //test2.thing = test2.thing.Where(c => !c.Equals(0)).ToArray();
            ser = JsonConvert.SerializeObject(test2);
            Console.WriteLine(ser);
            //var e = rsa.Encrypt(hash, data.E, data.N); // doesnt work right now idfk
            //Console.WriteLine(e.ToString());
            //Console.WriteLine();
            //var d = rsa.Decrypt(e, data.D, data.N);
            //Console.WriteLine(d.ToString());
        }
    }
    public class TestObj
    {
        public string thing { get; set; }
    }
}
