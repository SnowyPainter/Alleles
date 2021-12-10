using System;
using System.Text;

namespace Alleles // Note: actual namespace depends on the project name.
{
    public class Program
    {
        private static readonly int GEN = 3;
        private static List<DNA>[] dnas = new List<DNA>[GEN];
        private static void r(int maxgen, DNA dna, int current = 0)
        {
            if (current >= maxgen) return;
            var germs = new Wight(dna.ToString()).CasesOfGermDNAs();
            foreach (var germ1 in germs)
            {
                foreach (var germ2 in germs)
                {
                    var rp = germ1.Reproduct(germ2);
                    dnas[current].Add(rp);
                    r(maxgen, rp, current + 1);
                }
            }

        }
        private static void init() {
            'A'.SetPhenotype("A");
            'a'.SetPhenotype("a");
            'B'.SetPhenotype("B");
            'b'.SetPhenotype("b");
        }
        public static void Main(string[] args)
        {
            init();
            string csvpath = @"./program_result.csv";
            Console.WriteLine($"{csvpath}에 파일을 저장하는 중 ... ");
            if (File.Exists(csvpath))
                File.Delete(csvpath);
            const string Parent = "AaBb";
            for (int i = 0; i < dnas.Length; i++)
                dnas[i] = new List<DNA>();

            r(GEN, new DNA(Parent));

            Console.WriteLine($"P\t{Parent}");
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < dnas.Length; i++)
            {
                var children = dnas[i];
                Console.WriteLine($"F{i + 1}\t{children.Count}");
                foreach(var count in children.CountPhenotypes()) 
                    Console.WriteLine($"{count.Key}\t{count.Value}");
                
                var elements = "";
                foreach (var child in children)
                {
                    elements += $",{child.ToString()}";
                }
                str.Append($"F{i + 1},{children.Count}{elements}\n");
            }
            File.WriteAllText(csvpath, str.ToString());
            Console.WriteLine($"{csvpath}에 파일 저장이 완료되었습니다.");
        }
    }
}