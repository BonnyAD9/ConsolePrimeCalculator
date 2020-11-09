using System;
using System.Collections.Generic;

namespace Primes
{
    class Step
    {
        public List<long> PrimeNumbers { get; private set; } = new List<long>();
        private bool hold = true;
        private long position = 0;

        private long solA = 2;
        private long solB = 3;

        public Step()
        {
            PrimeNumbers.Add(2);
        }

        public long Next()
        {
            if (!hold)
            {
                while (!TwoOne()) ;
            }
            else
            {
                solA = solB;
                hold = false;
            }
            PrimeNumbers.Add(solA);
            return solA;
        }

        private bool TwoOne()
        {
            position += 6;
            solA = position - 1;
            solB = position + 1;

            bool[] b = new bool[] { IsPrime(solA), IsPrime(solB) };
            switch (b[0])
            {
                case true when b[1] == true:
                    hold = true;
                    return true;
                case true when b[1] == false:
                    return true;
                case false when b[1] == true:
                    solA = solB;
                    return true;
                default:
                    return false;
            }
        }

        private bool IsPrime(long num)
        {
            double sqrt = Math.Sqrt(num);
            foreach (long p in PrimeNumbers)
            {
                if ((num % p) == 0)
                    return false;
                if (p >= sqrt)
                    return true;
            }
            return true;
        }
    }
}
