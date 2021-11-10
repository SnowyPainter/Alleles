using System;

namespace Alleles
{
    class Program
    {
        static void Test() {
            'R'.SetPhenotype("둥근");
            'Y'.SetPhenotype("황색");
            'r'.SetPhenotype("주름진");
            'y'.SetPhenotype("녹색");
            DNA a = new DNA(new char[]{'R', 'Y'}), b = new DNA(new char[]{'r', 'y'});
            Wight creature = new Wight(a, b);
            Console.WriteLine($"유전형 : {creature.Genotype()} 표현형 : {creature.Phenotype()}");
            var n = creature.NumberOfGermDNACases();
            Console.WriteLine($"생식세포 DNA 구성 : {n}가지 경우가 공존");
            Console.WriteLine($"자가수분시 {n*n}가지");
            Dictionary<string, int> fs = new Dictionary<string, int>();
            
            var rp = creature.ReproductWith(creature.Clone());

            foreach(var item in rp) {
                Console.Write($"{item.Value} ");
                if(!fs.TryAdd(item.Value.Phenotype(), 1))
                    fs[item.Value.Phenotype()]++;
            }
            Console.Write("\n");
            foreach(var item in fs) {
                Console.WriteLine($"표현형이 {item.Key}로 일치하는 유전자는 {item.Value}개");
            }
        }
        static void Main(string[] args)
        {
            Test();
        }
    }

}