using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace Certs
{
    interface IGenerateRSAData
    {
        RSAData StrategyPattern();
        void GenerateP();
        void GenerateQ();
        void GenerateN();
        void GenerateNTotient();
        void GenerateDAndE();
    }
    class GenerateRSAData : IGenerateRSAData
    {
        RandomBigPrimes Primes = new RandomBigPrimes();
        RSAData nums = new RSAData();
        public void GenerateNTotient()
        {
            
            nums.NTosh = BigInteger.Multiply(BigInteger.Subtract(nums.P, 1), BigInteger.Subtract(nums.Q, 1));
        }

        public void GenerateP()
        {
            nums.P = GetBigInteger();
        }
        public void GenerateQ()
        {
            nums.Q = GetBigInteger();
        }
        public void GenerateDAndE()
        {
            nums.E = GetBigInteger();
            while(!CheckRelativePrimality(nums.E, nums.NTosh))
            {
                nums.E = GetBigInteger();
            }
            nums.D = ModInverse(nums.E, nums.NTosh);
        }

        public void GenerateN()
        {
            nums.N = BigInteger.Multiply(nums.P, nums.Q);
        }
        private bool CheckRelativePrimality(BigInteger num, BigInteger num2)
        {
            if (BigInteger.Remainder(num2, num) == 0 || BigInteger.Remainder(num, num2) == 0)
                return false;
            return true;
        }
        private long GetBigInteger()
        {
            return Primes.GetRandomPrime();
        }
        private BigInteger ModInverse(BigInteger a, BigInteger n)
        {
            BigInteger result;
            BigInteger k = 1;
            BigInteger temp, temp1;
            while (true)
            {
                temp = BigInteger.Multiply(k, n);
                temp1 = BigInteger.Add(1, temp);
                result = BigInteger.Divide(temp1, a);
                if (BigInteger.Remainder(result, 1) == 0) //integer
                {
                    return result;
                }
                else
                {
                    k = BigInteger.Add(1, k);
                }
            }
        }
        public RSAData StrategyPattern()
        {
            GenerateP();
            GenerateQ();
            GenerateN();
            GenerateNTotient();
            GenerateDAndE();
            return nums;
        }
    }


    class RSAData
    {
        public BigInteger N { get; set; }
        public BigInteger E { get; set; }
        public BigInteger D { get; set; }
        public BigInteger NTosh { get; set; }
        public BigInteger P { get; set; }
        public BigInteger Q { get; set; }
    }
}
