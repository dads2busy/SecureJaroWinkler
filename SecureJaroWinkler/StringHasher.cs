using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SecureJaroWinkler
{
    public class StringHasher
    {
        public StringHasher()
        {
        }

        static public List<Letter> getHashes(string sInput, byte[] salt)
        {
            List<Letter> sArrL = new List<Letter>();
            int sLen = sInput.Length;
            string ltr = "";
            string hash = "";
            string iHsh = "";
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Letter ltrHash = new Letter("","");
            Random rand = new Random();
            int rnd = 0;

            for(int i=0;i<sLen;i++)
            {

                rnd = rand.Next(0, i + 1);

                ltr = sInput[i].ToString().ToUpper();

                hash = ltr;// Hash.GetHash(ltr, Hash.HashType.MD5);

                iHsh = Hash.GetHMACSHA1(i.ToString(), salt).Substring(0,8); //i.ToString();

                ltrHash = new Letter(iHsh, hash);

                sArrL.Insert(rnd, ltrHash);

            }

            int l;
            if (sLen < 26)
            {
                l = 26 - sLen;
                
                

                for (int x = 0; x < l; x++)
                {
                    int r = rand.Next(0, sLen);
                    int ra = rand.Next(0, 25);
                    var rLtr = alpha[ra].ToString().ToUpper();
                    var rSlt = Salt.RandomSalt();
                    var rHash = rLtr; //Hash.GetHash(rLtr, Hash.HashType.MD5);
                    var rXHsh = Hash.GetHMACSHA1(r.ToString(), rSlt).Substring(0,8); //r.ToString();
                    var rLtrHsh = new Letter(rXHsh, rHash);
                    sArrL.Insert(r, rLtrHsh);
                }
            }

            return sArrL;
        }

        static public List<Letter> getFirstHashes(string sInput, byte[] fsalt)
        {
            var sfArrL = new List<Letter>();
            //var fsalt = new byte[4] { 1, 2, 3, 4 };
            var sfLen = sInput.Length;

            for (int j = 0; j < sfLen; j++)
            {
                var fltr = sInput[j].ToString();
                var fhash = Hash.GetHash(fltr, Hash.HashType.SHA1);
                var fiHsh = Hash.GetHMACSHA1(j.ToString(), fsalt);
                var fltrHash = new Letter(fiHsh, fhash);
                sfArrL.Add(fltrHash);
            }

            return sfArrL;
        }

    }
}
