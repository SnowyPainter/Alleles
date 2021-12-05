using System;
using System.Text;

namespace Alleles // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static string FormalOfLove(float a, float b, float c, float s)
        {
            return $"{a} * x^2 + {b} * abs(x) * y + {c} * y^2 = {s}";
        }
        private static void SetPhenotypeUpperLower(char c, string p)
        {
            Char.ToUpper(c).SetPhenotype("우성계수" + p);
            Char.ToLower(c).SetPhenotype("열성계수" + p);
        }
        private static string csvAppend(int fn, string c, string g, string p, string str)
        {
            str += $"{fn}, {c}, {g}, {p}\n";
            return str;
        }
        
        private static Dictionary<int,StringBuilder> sbdict = new Dictionary<int, StringBuilder>();
        public static void MakeAllKindOfDNA(int fn, string fd, string fm, int i, int max)
        {
            if (i > max || fn < 1) return;
            Wight w = new Wight(new DNA(fd), new DNA(fm));
            var cases = w.CasesOfGermDNAs();
            var j = 0;
            var str = "";
            foreach(var c in cases)
                str += $",{c.Genotype()}";
            foreach (var r in w.ReproductWith(w.Clone())) {
                //자가수분 제약 - 외부 환경
                /* if(r.Value.Phenotype().Contains("|우성계수크기|")) {
                    if(new Random().Next(0,3) >= 1) // 2/3
                        continue;
                } */

                str+= $",{r.Value}";
                MakeAllKindOfDNA(fn + 1, r.Key.Item1.Genotype(), r.Key.Item2.Genotype(), i + 1, max);
            }
            
            if(!sbdict.TryAdd(fn, new StringBuilder(str)))
                sbdict[fn].AppendLine(str);
            j++;
        }
        public static void Main(string[] args)
        {
            SetPhenotypeUpperLower('a', "x^2");
            SetPhenotypeUpperLower('b', "x");
            SetPhenotypeUpperLower('c', "y^2");
            SetPhenotypeUpperLower('s', "크기");
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
            string csvRaw = @"./program_result.csv";
            Console.WriteLine("재귀함수 호출 및 종료까지의 시간이 매우 많이, 오래 걸립니다.\n조금만 기다려주세요.");
            Console.WriteLine($"{csvRaw}에 파일을 저장하는 중 ... ");
            const string A = "ABCS", B = "abcs";
            
            const int GEN = 1;
            
            MakeAllKindOfDNA(1, A, B, 0, GEN);
            if(File.Exists(csvRaw)) {
                Console.WriteLine($"{csvRaw}가 존재하여 현재 날짜로 이름이 변경됩니다.");
                csvRaw = DateTime.Now.ToString("MMddyyyyHHmmss") + ".csv";
            }
            foreach(var s in sbdict)
                File.AppendAllText(csvRaw, $"{s.Key}{s.Value.ToString()}");
            Console.WriteLine($"{csvRaw}에 파일 저장이 완료되었습니다.");
            var lines = File.ReadLines(csvRaw);
            string lastline = lines.Last();
            string[] cols = lastline.Split(',');
            Console.WriteLine($"F{GEN+1} 단계까지 유전됨");
            int i = 1;
            //생식세포 DNA
            while(cols[i].Length == cols[1].Length) {
                //Console.WriteLine($"Germ\t{i} : {lastcols[i]}");
                i++;
            }
            Dictionary<string, int> dict = new Dictionary<string, int>();

            //외부환경 체크
            //int p = 0, m = 0;

            while(i < cols.Count()) {
                //if(cols[i].Phenotype().Contains("|우성계수크기|")) p++;
                //else m++;

                if(!dict.ContainsKey(cols[i]))
                    dict[cols[i]] = 1;
                else
                    dict[cols[i]]++;
                //Console.WriteLine($"Common DNAs {lastcols[i]}");
                i++;
            }

            Console.WriteLine($"|우성계수크기|의 표현형 개수 : {p} not {m}");

            Random random = new Random();
            
            var lineSplited = lines.ToList()[0].Split(',');
            //it must be last gneration row.
            var randDNA = dict.Keys.ToList()[random.Next(0, dict.Count())];

            Dictionary<char, float> coef = new Dictionary<char, float>();
            var literals = new char[] {'A', 'B', 'C', 'S'};
            var coefs = new char[5];
            var geno = randDNA.Genotype();
            i = 0;
            foreach(var l in literals) {
                var upper = Char.ToUpper(l);
                var lower = Char.ToLower(l);
                if(geno.Contains(upper)) {
                    coefs[i] = upper;
                    coef[coefs[i]] = initials[upper];
                }
                else if(geno.Contains(lower)) {
                    coefs[i] = lower;
                    coef[coefs[i]] = initials[lower];
                }
                i++;
            }
            Console.WriteLine($"자식세대 끝에서.. {randDNA}가 존재할 수 있음");
            Console.WriteLine($"{GEN+1}세대에 걸쳐 유전된 함수식은 아래와 같습니다.");
            Console.WriteLine(FormalOfLove(coef[coefs[0]], coef[coefs[1]], coef[coefs[2]], coef[coefs[3]]));

        }
    }
}