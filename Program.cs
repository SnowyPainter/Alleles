using System;

namespace Alleles
{
    class Program
    {
        static void Main(string[] args)
        {
            DNA a = new DNA(new char[]{'A', 'B', 'C'}), b = new DNA(new char[]{'a', 'b', 'c'});
            Wight c = new Wight(a, b);
            var d = c.CasesOfGermDNAs();
        }
    }

}