using System;
using System.Text;

namespace Alleles // Note: actual namespace depends on the project name.
{
    public class Program
    {
        private static readonly int GEN = 10;
        private static Random random = new Random();
        private static List<DNA>[] dnas = new List<DNA>[GEN];
        private static DNA[] randomN(DNA[] dnas, int n) {
            DNA[] rands = new DNA[n];
            var m = dnas.Length;
            if(m < n) throw new Exception();

            for(int i =  0;i < n;i++) {
                rands[i] = dnas[random.Next(0,m)];
            }
            return rands;
        }   
        public static DNA? Reproduct(DNA d1, DNA d2) {
            //regulation
            string dna1 = d1.ToString(), dna2 = d2.ToString();
            
            

            return new DNA(GeneExtension.ToAlignedGenes(d1, d2));
        }
        private static void r(int maxgen, DNA dna, bool random = false, int rn = 4, int current = 0)
        {
            if (current >= maxgen) return;

            var germs = new Wight(dna.ToString()).CasesOfGermDNAs();
            if(random) germs = randomN(germs, rn);
            foreach (var germ1 in germs)
            {
                foreach (var germ2 in germs)
                {
                    var rp = Reproduct(germ1, germ2);
                    if(rp == null) return;
                    dnas[current].Add(rp);
                    r(maxgen, rp, random, rn, current + 1);
                }
            }
        }
        private static void init() {
            'A'.SetPhenotype("A");
            'a'.SetPhenotype("a");
            'B'.SetPhenotype("B");
            'b'.SetPhenotype("b");
            'C'.SetPhenotype("C");
            'c'.SetPhenotype("c");
            'D'.SetPhenotype("D");
            'd'.SetPhenotype("d");
        }
        public static void Main(string[] args)
        {
            init();
            string csvpath = @"./program_result.csv";
            Console.WriteLine($"{csvpath}에 파일을 저장하는 중 ... ");
            if (File.Exists(csvpath))
                File.Delete(csvpath);
            const string Parent = "AaBbCc";
            for (int i = 0; i < dnas.Length; i++)
                dnas[i] = new List<DNA>();

            r(GEN, new DNA(Parent));//, true, 4);

            Console.WriteLine($"P\t{Parent}");
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < dnas.Length; i++)
            {
                var children = dnas[i];
                Console.WriteLine($"F{i + 1}\t{children.Count}");
                KeyValuePair<string,int> pair = new KeyValuePair<string, int>();
                int max = 0;
                foreach(var count in children.CountPhenotypes()) {
                    if(count.Value > max) {
                        max = count.Value;
                        pair = count;
                    }
                }
                Console.WriteLine($"Most common {pair.Key} : {pair.Value}");
                
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