using System;

namespace Alleles {

    public class Test {
        private bool self = false;
        private Wight f1, other;
        public Test(string[] args) {
            'R'.SetPhenotype("둥글다");
            'r'.SetPhenotype("주름지다");
            'Y'.SetPhenotype("황색");
            'y'.SetPhenotype("녹색");
            'T'.SetPhenotype("크다");
            't'.SetPhenotype("작다");

            var pDad = "RY";
            var pMom = "ry"; //RRYY + rryy 의 생식세포
            
            //.exe pDad pMom self
            //.exe pDad pMom otherDad otherMom
            pDad = args[0];
            pMom = args[1];
            if(args.Length == 3 && args[2] == "self") {
                self = true;
            }

            f1 = new Wight(new DNA(pDad), new DNA(pMom));
            other = f1.Clone();
            if(!self)
                other = new Wight(new DNA(args[2]), new DNA(args[3]));
        }
        public void Do() {
            Console.WriteLine($"F1\n유전형:\t{f1.Genotype()}\n표현형:\t{f1.Phenotype()}");
            Console.WriteLine("생성될 수 있는 생식세포 유전자형");
            int i = 1;
            foreach(var c in f1.CasesOfGermDNAs()) {
                Console.WriteLine($"{i}.\t{c.ToString()}\n=\t{c.Phenotype()}");
                i++;
            }
            
            Console.WriteLine($"F2 {f1.Genotype()} + {other.Genotype()}\n생성될 수 있는 {other.Genotype()}와 교배시 유전자형");
            var f2 = f1.ReproductWith(other);
            int j = 1;
            foreach(var c in f2) {
                Console.WriteLine($"{j}.\t{c.Value}");
                j++;
            }
            foreach(var c in DNAExtension.CountSamePhenotypes(f2)) {
                Console.WriteLine($"{c.Key} = {c.Value}");
            }
        }
    }
}