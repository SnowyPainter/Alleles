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
        public List<PhenotypeExecute> ManifestedPhenotype()
        {
            var dna = Genotype();
            var r = new List<PhenotypeExecute>();
            for (int i = 0; i < dna.Count(); i++)
            {
                bool z = false;
                if(dna.Count() > i+1 && Char.ToLower(dna[i]) == Char.ToLower(dna[i+1])) {
                    var nextGene = dna[i+1];
                    z = Char.IsLower(dna[i]) && Char.IsLower(nextGene);
                }
                
                if (Char.IsUpper(dna[i]) || z)
                {
                    var v = dna[i].Phenotype();
                    r.Add(v);
                }
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
        private List<List<Wight>>? creatures;
        public StringBuilder ReproductLog = new StringBuilder();
        private void recursionRep(int maxgen, Wight creature, Wight def, int children_count, int current = 0)
        {
            if (current >= maxgen) return;
            var germs1 = creature.CasesOfGermDNAs();
            var germs2 = def.CasesOfGermDNAs();
            var children = new List<Wight>();
            Random random = new Random();
            string csv = "";
            DNA p1, p2;
            Console.WriteLine($"{current}");
            for (int i = 0; i < germs1.Count(); i++)
                Console.WriteLine($"{germs1[i].ToString()}");
            //생식세포 자신에게서 2개 추출하여 자가수분
            var r = random.Next(0, germs1.Count());
            var r2 = random.Next(0, germs1.Count());
            p1 = germs2[r];
            p2 = germs1[r2];
            var rp = reproduct(p1, p2);
            if (rp == null) return;
            if (creatures != null)
            {
                csv += $", {rp.DNA()}";
                children.Add(rp);
            }

            ReproductLog.Append(
                $"F{current + 1}, {p1.ToString()},{p2.ToString()}{csv}\n"
            );

            recursionRep(maxgen, rp, rp, children_count, current + 1);

        }
        public List<List<Wight>>? SelfReproductForFN(int n, int sameGenCount, Wight def)
        {
            creatures = new List<List<Wight>>();
            string str = "";
            for (int i = 0; i < sameGenCount; i++)
            {
                str += ", A+B";
            }
            ReproductLog.Append($"FN, A, B{str}\n");
            recursionRep(n, reproduct(dna[Dad], dna[Mom]), def, sameGenCount);
            var result = creatures;
            creatures = null;
            return result;
        }
    }
}