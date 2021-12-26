using System;
using System.Text;

namespace Alleles
{
    public class Wight
    {
        private int Dad = 0, Mom = 1;
        private DNA[] dna = new DNA[2];

        public Wight(string dna) : this(new DNA(dna.Genotype()), new DNA(dna.RGenotype())) { }
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
        public string Genotype() => GeneExtension.ToAlignedGenes(dna[Dad], dna[Mom]);
        public List<PhenotypeExecute> Phenotype()
        {
            var dna = Genotype();

            var r = new List<PhenotypeExecute>();
            for (int i = 0; i < dna.Count(); i++)
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
        private Wight reproduct(DNA d1, DNA d2)
        {
            var r = new Wight(d1, d2);
            /* foreach (var exe in r.Phenotype())
            {
                exe.Execute(ref r);
            } */
            return r;
        }
        public string DNA()
        {
            return GeneExtension.ToAlignedGenes(dna[Dad], dna[Mom]);
        }
        public List<Wight> ReproductWith(Wight? creature)
        {
            if (creature == null || !DNAExtension.ValidateDNAs(dna[Dad], creature.dna[Dad]) || !DNAExtension.ValidateDNAs(dna[Mom], creature.dna[Mom]))
                return null;
            var reproducted = new List<Wight>();
            var c1 = CasesOfGermDNAs();
            var c2 = creature.CasesOfGermDNAs();
            foreach (var g in c1)
                foreach (var g2 in c2)
                    reproducted.Add(reproduct(g, g2));
            return reproducted;
        }
        private List<Tuple<Wight, DNA>>? creatures;
        public StringBuilder ReproductLog = new StringBuilder();
        private void recursionRep(int maxgen, Wight creature, Wight def, int current = 0)
        {
            if (current >= maxgen) return;
            var germs = creature.CasesOfGermDNAs();
            Random random = new Random();
            var married = def.CasesOfGermDNAs()[random.Next(0, germs.Count())];
            var marriedwith = germs[random.Next(0, germs.Count())];
            var rp = reproduct(marriedwith, married);
            ReproductLog.Append($"F{current+1}, {marriedwith.ToString()}, {married.ToString()}, {rp.DNA()}\n");
            if (rp == null) return;
            if(creatures != null) {
                creatures.Add(Tuple.Create(rp, married));
            }
            recursionRep(maxgen, rp, def, current + 1);

        }
        public List<Tuple<Wight, DNA>>? SelfReproductForFN(int n, Wight def)
        {
            creatures = new List<Tuple<Wight, DNA>>();
            ReproductLog.Append("Fn, A, B, A+B\n");
            var r = new Random();
            var c = def.CasesOfGermDNAs();
            recursionRep(n, reproduct(dna[Dad], dna[Mom]), def);
            var result = creatures;
            creatures = null;
            return result;
        }
    }
}