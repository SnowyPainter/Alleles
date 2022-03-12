using System;
using System.Linq;
using System.Text;

namespace Alleles // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string filepath = "phenotype.txt";
            Lab.OperateGenerator(filepath);
            StringBuilder sb = new StringBuilder();
            sb.Append("유전자, 유전병\n");
            Wight parent = new Wight("AaBbCcDdEeFfGg");

            int allCount = 0, diseaseCount = 0;
            const int diseaseMan = 5;
            
            var childDNAs = parent.CasesOfGermDNAs();
            foreach(var dna in childDNAs) {
                var child1 = dna;
                var siblings = parent.CasesOfGermDNAs();
                siblings = siblings.Where(dna => dna != child1).ToArray();

                foreach(var siblingDna in siblings) {
                    var child2 = siblingDna;

                    var mating = new Wight(child1, child2);
                    string phenotypes = "";
                    foreach(var p in mating.ManifestedPhenotype()) {
                        phenotypes += p.Execution + " ";
                    }
                    phenotypes = phenotypes.Replace("x ", "");
                    
                    phenotypes = phenotypes.Trim();
                    if(phenotypes.Count(c => c == ' ')-1 >= diseaseMan) diseaseCount++;

                    allCount++;
                    sb.Append($"{mating.ToString()}, {phenotypes}\n");
                }
            }
            const string path = @"C:\Users\snowy\Documents\Codes\Alleles\result.csv";
            if(File.Exists(path)) File.Delete(path);

            File.AppendAllText(path, sb.ToString());
            Console.WriteLine($"전체 카운트 : {allCount}");
            Console.WriteLine($"유전병이 {diseaseMan}개 이상 발현된 퍼센트 : {((float)diseaseCount/allCount)*100}%");
        }
    }
}