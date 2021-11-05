namespace Alleles
{
    public class Wight
    {
        private int Dad = 0, Mom = 1;
        private DNA[] dna = new DNA[2];

        public Wight(DNA fromDad, DNA fromMom)
        {
            if (fromDad.Size() != fromMom.Size())
            {
                return;
            }
            for (int i = 0; i < fromDad.Size(); i++)
            {
                var gt = fromDad.Genes[i].Genotype;
                if (gt != fromMom.Genes[i].UpperGT() && gt != fromMom.Genes[i].LowerGT())
                {
                    Console.WriteLine($"Genotype {gt}와 {fromMom.Genes[i].Genotype}는 유전형질, 따라서 동일한 위치에 존재 불가");
                    return;
                }
            }

            dna[Dad] = fromDad;
            dna[Mom] = fromMom;
        }
        public string Genotype() {
            string str = "";
            for(int i = 0;i < dna[Dad].Size();i++) {
                //우-열 순서로 배열
                if(dna[Dad].Genes[i].isDominant()) {
                    str += dna[Dad].Genes[i].Genotype;
                    str += dna[Mom].Genes[i].Genotype;
                    continue;
                }
                str += dna[Mom].Genes[i].Genotype;
                str += dna[Dad].Genes[i].Genotype;

            }
            return str;
        }
        public string Phenotype() {
            var dna = Genotype();
            var pt = "|";
            for(int i = 0;i <= this.dna[Dad].Size();i+=2) {
                pt+= $"{dna[i].Phenotype()}|";
            }
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
                    switchedGT = dna[Convert.ToInt32(s)].Genes[i-1].Genotype;
                    for (int k = 0; k < countOfAll / possibleCount; k++)
                    {
                        gtForAll[(countOfAll/possibleCount)*j + k, i-1] = switchedGT;
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
        public int NumberOfGermDNACases() {
            return (int)Math.Pow(2, dna[Dad].Size());
        }
    }
}