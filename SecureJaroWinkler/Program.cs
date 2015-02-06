using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Win32;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace SecureJaroWinkler
{
    class Program
    {
        
        
        static void Main(string[] args)
        {            
            //name in data source 1
            var str_1 = "MARTHA";
            //name in data source 2
            var str_2 = "MARHTA";

            //key sent to each data source
            var salt = Salt.RandomSalt(); //new byte[4] {1, 2, 3, 4};

            Stopwatch sw = new Stopwatch();
            sw = Stopwatch.StartNew();
            
            //get hashed list array of letter objects
            //letter object includes letter and position
            //letter objects are randomly inserted in the list array
            //random letters for names < 26 characters are randomly inserted in the list array
            var aList = StringHasher.getHashes(str_1, salt);
            var bList = StringHasher.getHashes(str_2, salt);
            var fstHash = sw.Elapsed;

            
            sw = Stopwatch.StartNew();
            //random letter insertions are eliminated
            //letters are arranged according to position property of letter object
            var a_rlLtrs = Decode.CleanList(aList, salt);
            var b_rlLtrs = Decode.CleanList(bList, salt);
            var scdHash = sw.Elapsed;

            sw = Stopwatch.StartNew();
            //names are compared using Jaro Winkler
            SecureJW sJW = new SecureJW(.7, 4);
            double match = sJW.Match(a_rlLtrs, b_rlLtrs);
            var jW = sw.Elapsed;


            //JSON-format serialized output for transport to Shaker
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string ser = jss.Serialize(aList);

            Console.WriteLine(ser);
            Console.WriteLine("--------------------------------------------");

            foreach (Letter l in aList)
            {
                Console.WriteLine(l.Pos.ToString() + ", " + l.Ltr);
            }

            Console.WriteLine("--------------------------------------------");

            foreach (string r in a_rlLtrs)
            {
                Console.WriteLine(r.ToString());
            }

            Console.WriteLine("--------------------------------------------");

            foreach (string r in b_rlLtrs)
            {
                Console.WriteLine(r.ToString());
            }

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Match: " + match);
            Console.WriteLine(fstHash);
            Console.WriteLine(scdHash);
            Console.WriteLine(jW);
        }
    }
}
