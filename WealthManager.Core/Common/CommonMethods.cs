using System;
using System.Collections.Generic;
using System.Text;

namespace WealthManager.Core.Common
{
    public class CommonMethods
    {
        public static int GenerateRandomNo()
        {
            int min = 0000;
            int max = 9999;
            Random rand = new Random();
            return rand.Next(min, max);
        }
    }
}
