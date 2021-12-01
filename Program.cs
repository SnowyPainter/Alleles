using System;
using System.Text;

namespace Alleles // Note: actual namespace depends on the project name.
{
    public class Program
    {
        private static StringBuilder sb = new StringBuilder();
        public static string FormalOfLove(float a, float b, float c, float s)
        {
            return $"{a} * x^2 + {b} * |x| * y + {c} * y^2 = {s}";
        }
        private static void SetPhenotypeUpperLower(char c, string p)
        {
            Char.ToUpper(c).SetPhenotype("우등계수 " + p);
            Char.ToLower(c).SetPhenotype("열등계수 " + p);
        }
        private static string csvAppend(int fn, string c, string g, string p, string str)
        {
            str += $"{fn}, {c}, {g}, {p}\n";
            return str;
        }
        // csv
        // F1 Cases ... result
        // F2 Cases ... result2
        public static void MakeAllKindOfDNA(int fn, string fd, string fm, int i, int max)
        {
            var str = "";
            if (i > max) return;
            Wight w = new Wight(new DNA(fd), new DNA(fm));
            var cases = w.CasesOfGermDNAs();
            var j = 0;
            str = $"{fn}";
            foreach(var c in cases)
                str += $",{c.Genotype()}";
            foreach (var r in w.ReproductWith(w.Clone()))
                str+= $",{r.Value}";
            
            sb.AppendLine(str);

            j++;
            foreach (var c in cases)
            {
                foreach(var C in cases) {
                    MakeAllKindOfDNA(fn + 1, C.Genotype(), c.Genotype(), i + 1, max);
                }
            }

        }
        public static void Main(string[] args)
        {
            SetPhenotypeUpperLower('a', "x^2");
            SetPhenotypeUpperLower('b', "x");
            SetPhenotypeUpperLower('c', "y^2");
            SetPhenotypeUpperLower('s', "크기");
            //p.Dad = {'A', 'B', 'C', 'S'}
            //p.Mom = {'a', 'b', 'c', 's'}
            Dictionary<char, float> initials = new Dictionary<char, float>() {
                {'A', 17},
                {'a', 2.6f},
                {'B', -16},
                {'b', -1.3f},
                {'C', 15},
                {'c', 6.5f},
                {'S', 100},
                {'s', 50}
            };
            string csvRaw = "./sheet.csv";
            Console.WriteLine("시간이 오래 걸립니다. 조금만 기다려주세요.");
            Console.WriteLine($"{csvRaw}에 파일을 저장하는 중 ... ");
            const string A = "ABCS", B = "abcs";
            MakeAllKindOfDNA(1, A, B, 0, 2);
            File.AppendAllText(csvRaw, sb.ToString());

            string lastline = File.ReadLines(csvRaw).Last();
            string[] lastcols = lastline.Split(',');
            Console.WriteLine($"F{lastcols[0]} 단계까지 유전");
            int i = 1;
            while(lastcols[i].Length == lastcols[1].Length) {
                Console.WriteLine($"Germ\t{i} : {lastcols[i]}");
                i++;
            }
            Dictionary<string, int> dict = new Dictionary<string, int>();
            while(i < lastcols.Count()) {
                if(!dict.ContainsKey(lastcols[i]))
                    dict[lastcols[i]] = 1;
                else
                    dict[lastcols[i]]++;
                Console.WriteLine($"Common DNAs {lastcols[i]}");
                i++;
            }
            var kp = dict.Max();
            var v = kp.Key;
            Console.WriteLine($"최종 분석 결과 : {v}");
            
        
            float a = 0, b = 0, c = 0, s = 0;
            if(v.Contains('A')) {
                a = initials['A'];
            } else if(v.Contains('a')) {
                a = initials['a'];
            }
            if(v.Contains('B')) {
                b = initials['B'];
            } else if(v.Contains('b')) {
                b = initials['b'];
            }
            if(v.Contains('C')) {
                c = initials['C'];
            } else if(v.Contains('c')) {
                c = initials['c'];
            }
            if(v.Contains('S')) {
                s = initials['S'];
            } else if(v.Contains('s')) {
                s = initials['s'];
            }

            Console.WriteLine($"The most commonly {v}");
            Console.WriteLine(FormalOfLove(a,b,c,s));

        }
    }
}