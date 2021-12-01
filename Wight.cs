using System;

namespace Alleles
{
    public class Wight
    {
        private int Dad = 0, Mom = 1;
        private DNA[] dna = new DNA[2];
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
            string str = "";
            for (int i = 0; i < dna[Dad].Size(); i++)
            {
                //우-열 순서로 배열
                if (dna[Dad].Genes[i].isDominant())
                {
                    str += dna[Dad].Genes[i].Genotype;
                    str += dna[Mom].Genes[i].Genotype;
                    continue;
                }
                str += dna[Mom].Genes[i].Genotype;
                str += dna[Dad].Genes[i].Genotype;

            }
            return str;
        }
        public string Phenotype()
        {
            var dna = Genotype();
            var pt = "|";
            for (int i = 0; i < this.dna[Dad].Size()*2; i += 2)
            {
                pt += $"{dna[i].Phenotype()}|";
            }
            if(pt.Length == 1) pt = "";
            return pt;
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
        public Dictionary<(DNA, DNA), string> ReproductWith(Wight creature)
        {
            if (!DNAExtension.ValidateDNAs(dna[Dad], creature.dna[Dad]) || !DNAExtension.ValidateDNAs(dna[Mom], creature.dna[Mom]))
            {
                return null;
            }

            var reproducted = new Dictionary<(DNA, DNA), string>();
            var c1 = CasesOfGermDNAs();
            var c2 = creature.CasesOfGermDNAs();
            foreach (var g in c1)
            {
                foreach (var g2 in c2)
                {
                    var p = GeneExtension.ToAlignedGenes(g, g2);
                    reproducted.TryAdd((g, g2), p);
                }
            }
            return reproducted;
        }
        //여러개 수가 있을때 최소 공배수 구하기

        //dna AaBb 주어짐
        //f2 표현형들의 비율이 나옴
        //어떤 dna와 교배시켜야 그 비율이 나오는가?
    }
}