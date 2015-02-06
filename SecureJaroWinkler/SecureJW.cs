using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SecureJaroWinkler
{
    class SecureJW
    {
        private double mWeightThreshold;
        private int mNumChars;
    
        public SecureJW(double weightThreshold, int numChars) {
            mNumChars = numChars;
            mWeightThreshold = weightThreshold;
        }

        public double Match(List<string> aR1, List<string> aR2)
        {
            bool[] matched1 = new bool[aR1.Count];
            bool[] matched2 = new bool[aR2.Count];

            int m = Math.Max(aR1.Count, aR2.Count);
            int searchRange = (m / 2) - 1;

            int numCommon = 0;
            for (int i = 0; i < aR1.Count; ++i)
            {
                int start = Math.Max(0, i - searchRange);
                int end = Math.Min(i + searchRange + 1, aR2.Count);
                for (int j = start; j < end; ++j)
                {
                    if (matched2[j]) continue;
                    if (aR1[i].Equals(aR2[j]))
                    {
                        matched1[i] = true;
                        matched2[j] = true;
                        ++numCommon;
                        break;
                    }
                }
            }
            if (numCommon == 0) return 0.0;

            int numHalfTransposed = 0;
            int k = 0;
            for (int i = 0; i < aR1.Count; ++i)
            {
                if (!matched1[i]) continue;
                while (!matched2[k]) ++k;
                if (aR1[i].Equals(aR2[k]))
                {

                }
                else
                {
                    ++numHalfTransposed;

                }
                ++k;
            }

            int numTransposed = numHalfTransposed / 2;

            double numCommonD = numCommon;
            double weight = (numCommonD / aR1.Count
                    + numCommonD / aR2.Count
                    + (numCommon - numTransposed) / numCommonD) / 3.0;

            if (weight <= mWeightThreshold) return weight;
            int max = Math.Min(mNumChars, Math.Min(aR1.Count, aR2.Count));
            int pos = 0;
            while (pos < max && aR1[pos].Equals(aR2[pos]))
                ++pos;
            if (pos == 0) return weight;
            return weight + 0.1 * pos * (1.0 - weight);
        }
    }
}
