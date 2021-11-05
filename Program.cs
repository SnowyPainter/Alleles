using System;

namespace Alleles
{
    class Program
    {
        static void Main(string[] args)
        {
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
            int i = 1;
            var c1 = creature.CasesOfGermDNAs();
            var c2 = c1;
            foreach(var g in c1) {
                int j = 1;
                Console.WriteLine($"{i} : {g.Genotype()} {g.Phenotype()}");
                foreach(var g2 in c2) {
                    Console.Write($"\t{j} : {g2.Genotype()} {g.Phenotype()} -> ");
                    var p = GeneExtension.ToAlignedGenes(g, g2);
                    if(!fs.TryAdd(p.Phenotype(), 1))
                        fs[p.Phenotype()]++;
                    Console.WriteLine($"{p} : {p.Phenotype()}");
                    j++;
                }
                i++;
            }

            foreach(var item in fs) {
                Console.WriteLine($"유전들 중 표현형 {item.Key}이 같은 것은 {item.Value}개");
            }
            

        }
    }

}