using System;
using System.Text;

namespace Alleles // Note: actual namespace depends on the project name.
{
    public class Program
    {
        private static readonly int GEN = 1;

        private static void init() {
            'A'.SetPhenotype(new MyPhenoExec{Execute = (ref Wight sender)=> {
                (sender).JustA++;
            }, Execution = "PlusA"});
            'B'.SetPhenotype(new MyPhenoExec{Execute = (ref Wight sender)=> {
                (sender).JustB++;
            }, Execution = "PlusB"});
            'a'.SetPhenotype(new MyPhenoExec{Execute = (ref Wight sender)=> {
                (sender).JustA--;
            }, Execution = "MinusA"});
            'b'.SetPhenotype(new MyPhenoExec{Execute = (ref Wight sender)=> {
                (sender).JustB--;
            }, Execution = "MinusB"});
        }
        public static void Main(string[] args)
        {
            init();
            
            const string Parent = "AaBb";
            Wight parent = new Wight(Parent);
            
            foreach(var p in parent.Phenotype()) {
                Console.WriteLine(p.Execution);
                p.Execute(ref parent);
            }

            Console.WriteLine(parent.JustA);
            Console.WriteLine(parent.JustB);
        }
    }
}