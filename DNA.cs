namespace Alleles
{
    public class DNA
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
}