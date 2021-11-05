namespace Alleles
{
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
        public DNA Clone()
        {
            return this;
        }
    }
}