using System;
using System.Collections.Generic;
using System.Text;

namespace BBlockchain.Helpers
{
    public class Util
    {
        public static string Zeros(int value)
        {

            string zero = "";
            for (int i = 0; i < value; i++)
            {
                zero = zero + "0";
            }
            return zero;
        }

    }
}
