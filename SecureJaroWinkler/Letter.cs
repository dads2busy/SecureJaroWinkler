using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecureJaroWinkler
{
    public class Letter
    {
        public string Pos { get; set;  }
        public string Ltr { get; set;  }

        public Letter( string posHash, string ltrHash )
        {
            Pos = posHash;
            Ltr = ltrHash;
        }
                
    }
}
