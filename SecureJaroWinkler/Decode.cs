using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SecureJaroWinkler
{
    public class Decode
    {
        static public List<String> CleanList(List<Letter> allLtrs, byte[] salt)
        {
            List<string> realLtrs = new List<string>();
            List<string> posHshs = new List<string>();

            for (int x = 0; x < 25; x++)
            {
                posHshs.Add(Hash.GetHMACSHA1(x.ToString(), salt).Substring(0,8));
            }

            foreach (string s in posHshs)
            {
                foreach (Letter l in allLtrs)
                {
                    if (l.Pos == s)
                    {
                        realLtrs.Add(l.Ltr);
                    }
                }
            }

            return realLtrs;
        }
    }
}
