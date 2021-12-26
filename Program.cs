using System;
using System.Text;

namespace Alleles // Note: actual namespace depends on the project name.
{
    public class Program
    {
        const string Parent = "AaBbCcDdEeFfGgHh";
        const int GEN = 100;
        private static void init()
        {
            foreach (var c in Parent)
            {
                c.SetPhenotype(new PhenotypeExecute
                {
                    Execute = (ref Wight sender) =>
                    {
                        //Nothing manifested
                    },
                    Execution = c.ToString()
                });
            }
        }
        public static void Main(string[] args)
        {
            init();
            Wight parent = new Wight(Parent);
            
            var result = parent.SelfReproductForFN(GEN, new Wight(Parent));
            if(result == null) {
                Console.WriteLine("Error result is null");
                return;
            }

            File.WriteAllText("a.csv", parent.ReproductLog.ToString());

        }
    }
}