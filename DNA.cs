namespace Alleles
{
    public static class DNAExtension {
        public static bool ValidateDNAs(DNA a, DNA b) {
            if (a.Size() != b.Size())
            {
                return false;
            }
            for (int i = 0; i < a.Size(); i++)
            {
                var gt = a.Genes[i].Genotype;
                if (gt != b.Genes[i].UpperGT() && gt != b.Genes[i].LowerGT())
                {
                    return false;
                }
            }
            return true;
        }
        public static Dictionary<string, int> CountSamePhenotypes(Dictionary<(DNA, DNA), string> reproducted)
        {
            Dictionary<string, int> phenoRatio = new Dictionary<string, int>();
            var f2 = reproducted;
            foreach (var item in f2)
            {
                var genotype = item.Value;

                if (!phenoRatio.TryAdd(item.Value.Phenotype(), 1))
                    phenoRatio[item.Value.Phenotype()]++;
            }
            return phenoRatio;
        }
        public static string Genotype(this string s)
        {
            var d = "";
            for (int i = 0; i < s.Length; i+=2)
            {
                d += s[i];
            }
            return d;
        }
        public static string RGenotype(this string s)
        {
            var d = "";
            for (int i = 1; i <= s.Length; i+=2)
            {
                d += s[i];
            }
            return d;
        }
    }

    public class DNA
    {
        public readonly List<Gene> Genes = new List<Gene>();
        
        public DNA() { }
        public DNA(char[] gts)
        {
            Set(gts);
        }
        public DNA(string gts) {
            Set(gts.ToCharArray());
        }
        public string Genotype()
        {
            string str = "";
            for (int i = 0; i < Genes.Count(); i++)
            {
                str += Genes[i].Genotype;
            }
            return str;
        }
        public string Phenotype()
        {
            var dna = Genotype();
            var pt = "|";
            for (int i = 0; i < this.Genes.Count();i++)
            {
                pt += $"{dna[i].Phenotype()}|";
            }
            return pt;
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
    }
}