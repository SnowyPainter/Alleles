using System;

namespace Alleles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test functions");
            DNA a = new DNA(new char[]{'A', 'B', 'C'}), b = new DNA(new char[]{'a', 'b', 'c'});
            Chromo c = new Chromo(a, b);
            var d = c.GetGermCellDNA();
        }
    }
    public static class Error
    {
        public static string Caption = "";
        public static void Log(string msg)
        {
            Console.WriteLine($"[{Caption}] {msg}");
        }
    }
    public static class DNAExtension
    {
        private static Dictionary<char, string> phenoForgeno = new Dictionary<char, string>();
        public static void SetPhenotype(this Gene gene, string text)
        {
            if (!phenoForgeno.TryAdd(gene.Genotype, text))
                phenoForgeno[gene.Genotype] = text;
        }
        public static string? Phenotype(this Gene gene)
        {
            return phenoForgeno.GetValueOrDefault(gene.Genotype);
        }
    }
    public class Gene
    {
        public char Genotype { get; set; } //Uppercase, Lowercase contrast.
        public char UpperGT()
        {
            return Char.ToUpper(Genotype);
        }
        public char LowerGT()
        {
            return Char.ToLower(Genotype);
        }
        public string? Phenotype { get; set; } //must be same, just for display.
        public Gene(char g)
        {
            Genotype = g;
        }
    }
    class DNA
    {
        public readonly List<Gene> Genes = new List<Gene>();
        public DNA() {}
        public DNA(char[] gts) {
            Set(gts);
        }
        public void Set(char[] genotypes)
        {
            foreach (char gt in genotypes)
                Genes.Add(new Gene(gt));
        }
        public int Size()
        {
            return Genes.Count();
        }
        public DNA Clone()
        {
            return this;
        }
    }
    class Chromo
    {
        private int Dad = 0, Mom = 1;
        private DNA[] dna = new DNA[2];

        public Chromo(DNA fromDad, DNA fromMom)
        {
            Error.Caption = "Chromo Construct";
            if (fromDad.Size() != fromMom.Size())
            {
                Error.Log("Size != Size");
                return;
            }
            for (int i = 0; i < fromDad.Size(); i++)
            {
                var gt = fromDad.Genes[i].Genotype;
                if (gt != fromMom.Genes[i].UpperGT() && gt != fromMom.Genes[i].LowerGT())
                {
                    Error.Log($"Genotype {gt}와 {fromMom.Genes[i].Genotype}는 유전형질, 따라서 동일한 위치에 존재 불가");
                    return;
                }
            }

            dna[Dad] = fromDad;
            dna[Mom] = fromMom;
        }
        public DNA[] GetGermCellDNA()
        {
            int dnalen = dna[Dad].Size();
            int len = (int)Math.Pow(2, dnalen);
            DNA[] dnas = new DNA[len];
            char[,] gts = new char[len, dnalen];
            for (int i = dnalen; i > 0; i--)
            {
                int gtk = (int)Math.Pow(2, i);
                char switched; bool s = false;
                for (int j = 0; j < gtk; j++)
                {
                    switched = dna[Convert.ToInt32(s)].Genes[i-1].Genotype;
                    for (int k = 0; k < len / gtk; k++)
                    {
                        gts[(len/gtk)*j + k, i-1] = switched;
                    }
                    s = !s;
                }
            }
            for (int i = 0; i < len; i++)
            {
                dnas[i] = new DNA(GetRow(gts, i));
            }
            return dnas;
        }
        private char[] GetRow(char[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }

    class Creature
    { //2n chromos
        private Chromo[] chromos;
        public Creature(int chromos)
        {
            this.chromos = new Chromo[chromos];
        }
    }

}