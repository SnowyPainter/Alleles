using System;

namespace Alleles {
    public static class Lab {
        public static void OperateGenerator() {
            
        }
        public static void ExecuteCalculator(string[] args) {
            foreach (var line in File.ReadAllLines(args[2]))
            {
                var c = line.Split(':');
                c[0][0].SetPhenotype(new PhenotypeExecute
                {
                    Execute = (ref Wight sender) =>
                    {
                        //Nothing manifested
                    },
                    Execution = c[1]
                });
            }
            
            Wight parent = new Wight(args[0]);
            
            var result = parent.ReproductWith(new Wight(args[1]));
            if(result == null) {
                Console.WriteLine("Error result is null");
                return;
            }
            
            Console.WriteLine(result.Count());
            foreach(var r in result) {
                Console.WriteLine(r.DNA());
            }
            foreach(var c in DNAExtension.CountSamePhenotypes(result)) {
                Console.WriteLine($"{c.Key} : {c.Value}");
            }
        }
    }
}