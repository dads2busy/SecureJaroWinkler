using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SecureJaroWinkler
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = "Testing";
            var salt = new byte[4] {1, 2, 3, 4};

            var tick1 = System.Environment.TickCount;

            var aList = StringHasher.getHashes(str, salt);
            var tick2 = System.Environment.TickCount;
            
            var rlLtrs = Decode.CleanList(aList, salt);
            var tick3 = System.Environment.TickCount;
            
            var fList = StringHasher.getFirstHashes(str, salt);
            
            foreach (Letter l in aList)
            {
                Console.WriteLine(l.Pos.ToString() + ", " + l.LetterHash);
            }

            Console.WriteLine("--------------------------------------------");

            foreach (Letter r in rlLtrs)
            {
                Console.WriteLine(r.Pos.ToString() + ", " + r.LetterHash);
            }

            Console.WriteLine("--------------------------------------------");

            foreach (Letter f in fList)
            {
                Console.WriteLine(f.Pos.ToString() + ", " + f.LetterHash);
            }

            Console.WriteLine(tick1);
            Console.WriteLine(tick2);
            Console.WriteLine(tick3);
            
        }
    }
}
