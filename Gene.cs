namespace Alleles
{
    public static class GeneExtension
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
}