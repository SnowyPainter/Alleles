namespace Alleles
{
    public static class GeneExtension
    {
        private static Dictionary<char, string> phenoForgeno = new Dictionary<char, string>();
        public static char SetPhenotype(this char gene, string text)
        {
            if (!phenoForgeno.TryAdd(gene, text))
                phenoForgeno[gene] = text;
            return gene;
        }
        public static string? Phenotype(this char gene)
        {
            return phenoForgeno.GetValueOrDefault(gene);
        }
        public static string Phenotype(this string genotypes) {
            var pt = "|";
            for(int i = 0;i < genotypes.Length;i+=2) {
                pt+= $"{genotypes[i].Phenotype()}|";
            }
            return pt;
        }
        public static string ToAlignedGenes(DNA g, DNA g2) {
            string str = "";
            for(int i = 0;i < g.Size();i++) {
                //우-열 순서로 배열
                if(g.Genes[i].isDominant()) {
                    str += g.Genes[i].Genotype;
                    str += g2.Genes[i].Genotype;
                    continue;
                }
                str += g2.Genes[i].Genotype;
                str += g.Genes[i].Genotype;

            }
            return str;
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
        public bool isDominant() {
            return Char.IsUpper(Genotype);
        }
        public Gene(char g)
        {
            Genotype = g;
        }
    }
}