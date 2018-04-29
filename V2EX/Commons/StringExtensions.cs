using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.Commons
{
    public static class StringExtensions
    {
        private static Dictionary<Char, int> dic = new Dictionary<char, int>
        {
            { 'A',11 }
        };
        public static char GetCharFromString(this string Word)
        {
            return 'A';
        }
    }
}
