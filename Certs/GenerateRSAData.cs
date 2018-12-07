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
            nums.NTosh = (nums.P - 1) * (nums.Q - 1);
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
            nums.N = nums.P * nums.Q;
        }
        private bool CheckRelativePrimality(BigInteger num, long num2)
        {
            if (num2 % num == 0 || num % num2 == 0)
                return false;
            return true;
        }
        private long GetBigInteger()
        {
            return Primes.GetRandomPrime();
        }
        private long ModInverse(long a, long n)
        {
            long result;
            long k = 1;
            long temp;
            while (true)
            {
                temp = k * n;
                temp++;
                result = temp / a;
                if (result % 1 == 0) //integer
                {
                    return result;
                }
                else
                {
                    k++;
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
        public long N { get; set; }
        public long E { get; set; }
        public long D { get; set; }
        public long NTosh { get; set; }
        public long P { get; set; }
        public long Q { get; set; }
    }
}
