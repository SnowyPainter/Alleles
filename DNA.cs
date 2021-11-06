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
    }

    public class DNA
    {
        public readonly List<Gene> Genes = new List<Gene>();
        public DNA() { }
        public DNA(char[] gts)
        {
            Set(gts);
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