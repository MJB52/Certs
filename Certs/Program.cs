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

            var obj = JsonConvert.DeserializeObject<TestObj>(ser);
            //obj.thing = obj.thing.Where(c => !c.Equals(0)).ToArray();
            foreach (var thing in obj.thing)
                Console.Write(thing);

            var d = rsa.Decrypt(ser, data.D,data.N); // doesnt work right now idfk
            foreach (var thing in d)
                Console.Write(thing);
            var test2 = new TestObj();
            test2.thing = d;
            //test2.thing = test2.thing.Where(c => !c.Equals(0)).ToArray();
            ser = JsonConvert.SerializeObject(test);
            Console.WriteLine(ser);
            obj = JsonConvert.DeserializeObject<TestObj>(ser);
            foreach (var thing in obj.thing)
                Console.Write(Convert.ToByte(thing));
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
