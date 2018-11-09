using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace Certs
{
    interface IRSA
    {
        string GeneratePandQ();
        BigInteger GenerateN();
        void GenerateNTotient();
        BigInteger GenerateE();
        BigInteger GenerateD();
    }
    class RSA : IRSA
    {
        public void GenerateNTotient()
        {
            throw new NotImplementedException();
        }

        public string GeneratePandQ()
        {
            return "";
        }

        public BigInteger GenerateD()
        {
            throw new NotImplementedException();
        }

        public BigInteger GenerateE()
        {
            throw new NotImplementedException();
        }

        public BigInteger GenerateN()
        {
            throw new NotImplementedException();
        }
        private bool CheckPrimality(BigInteger num)
        {
            for (int i = 0; i < num; i++)
                if (num % i == 0)
                    return false;
            return true;
        }
        private BigInteger GetBigInteger()
        {

        }
    }


    class RSAData
    {
        BigInteger N { get; set; }
        BigInteger E { get; set; }
        BigInteger D { get; set; }
    }
}
