using System;

namespace Alleles
{
    public class Wight
    {
        public int JustA, JustB;

        private int Dad = 0, Mom = 1;
        private DNA[] dna = new DNA[2];
        
        public Wight(string dna):this(new DNA(dna.Genotype()), new DNA(dna.RGenotype())) {
            
        }
        public Wight(DNA fromDad, DNA fromMom)
        {
            if (!DNAExtension.ValidateDNAs(fromDad, fromMom))
            {
                Console.WriteLine("Not a valid DNA");
                return;
            }

            dna[Dad] = fromDad;
            dna[Mom] = fromMom;
        }
        public Wight Clone() {
            return this;
        }
        public string Genotype()
        {
            return reproduct(dna[Dad], dna[Mom]).Genotype();
        }
        public List<IPhenotypeExecution> Phenotype()
        {
            var dna = Genotype();
            var r = new List<IPhenotypeExecution>();
            for (int i = 0; i < dna.Count();i++)
            {
                var v = dna[i].Phenotype();
                r.Add(v);
            }
            return r;
        }
        public DNA[] CasesOfGermDNAs()
        {
            int dnalen = dna[Dad].Size();
            int countOfAll = (int)Math.Pow(2, dnalen);
            DNA[] dnas = new DNA[countOfAll];
            char[,] gtForAll = new char[countOfAll, dnalen];
            for (int i = dnalen; i > 0; i--)
            {
                int possibleCount = (int)Math.Pow(2, i);
                char switchedGT; bool s = false;
                for (int j = 0; j < possibleCount; j++)
                {
                    switchedGT = dna[Convert.ToInt32(s)].Genes[i - 1].Genotype;
                    for (int k = 0; k < countOfAll / possibleCount; k++)
                    {
                        gtForAll[(countOfAll / possibleCount) * j + k, i - 1] = switchedGT;
                    }
                    s = !s;
                }
            }
            for (int i = 0; i < countOfAll; i++)
            {
                var r = Enumerable.Range(0, gtForAll.GetLength(1))
                    .Select(x => gtForAll[i, x])
                    .ToArray();
                dnas[i] = new DNA(r);
            }
            return dnas;
        }
        public int NumberOfGermDNACases()
        {
            return (int)Math.Pow(2, dna[Dad].Size());
        }
        public List<DNA> ReproductWith(Wight? creature)
        {
            if (creature == null || !DNAExtension.ValidateDNAs(dna[Dad], creature.dna[Dad]) || !DNAExtension.ValidateDNAs(dna[Mom], creature.dna[Mom]))
            {
                return null;
            }

            var reproducted = new List<DNA>();
            var c1 = CasesOfGermDNAs();
            var c2 = creature.CasesOfGermDNAs();
            foreach (var g in c1)
            {
                foreach (var g2 in c2)
                {
                    var p = GeneExtension.ToAlignedGenes(g, g2);
                    reproducted.Add(new DNA(p));
                }
            }
            return reproducted;
        }
        private static List<DNA>[] dnas;
        private DNA? reproduct(DNA d1, DNA d2) {
            return new DNA(GeneExtension.ToAlignedGenes(d1, d2));
        }
        private void recursionRep(int maxgen, DNA dna, int current = 0)
        {
            if (current >= maxgen) return;

            var germs = new Wight(dna.ToString()).CasesOfGermDNAs();
            
            foreach (var germ1 in germs)
            {
                foreach (var germ2 in germs)
                {
                    var rp = reproduct(germ1, germ2);
                    if(rp == null) return;
                    dnas[current].Add(rp);
                    recursionRep(maxgen, rp, current + 1);
                }
            }
        }
        public List<DNA>[] SelfReproductForFN(int n) {
            dnas = new List<DNA>[n];
            for(int i =  0;i < n;i++)
                dnas[i] = new List<DNA>();
            recursionRep(n, reproduct(dna[Dad],dna[Mom]));
            var result = dnas;
            
            dnas = null;
            return result;
        }
    }
}