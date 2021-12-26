namespace Alleles
{
    public static class GeneExtension
    {
        private static Dictionary<char, PhenotypeExecute> phenoForgeno = new Dictionary<char, PhenotypeExecute>();
        public static bool ValidateDictionary() {
            if(phenoForgeno.Count <= 0) return false;
            return true;
        }
        public static void Clear() {
            phenoForgeno = new Dictionary<char, PhenotypeExecute>();
        }
        public static void SetPhenotype(this char gene, PhenotypeExecute ep)
        {
            if (!phenoForgeno.TryAdd(gene, ep))
                phenoForgeno[gene] = ep;
        }
        public static PhenotypeExecute? Phenotype(this char gene)
        {
            return phenoForgeno.GetValueOrDefault(gene);
        }
        public static string PhenotypeString(this string genotypes) {
            var pt = "|";
            for(int i = 0;i < genotypes.Length;i+=2) {
                var p = genotypes[i].Phenotype();
                if(p != null) pt+= $"{p.Execution}|";
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