using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Genetic {
    public class Division {
        public List<bool> division;

        public Division(int n) {
            division = new List<bool>(n);
            for (int i = 0; i < n; i++)
                division.Add(Program.RandomBool());
        }

        public Division(Division p1, Division p2) {
            division = new List<bool>();
            for (int i = 0; i < p1.division.Count; i++) {
                var parent = Program.RandomBool();
                if (Program.r.NextDouble() > 0.9) {
                    division.Add(Program.RandomBool());
                    continue;
                }
                if (parent)
                    division.Add(p1.division[i]);
                else
                    division.Add(p2.division[i]);
            }
        }
    }

    static class Program {
        public static int Fitness(Division d) {
            int count = 0;
            for (int i = 0; i < d.division.Count; i++)
                count += d.division[i] ? array[i] : -array[i];
            return Math.Abs(count);
        }

        public static List<Division> Iterate(List<Division> d)
        {
            var list = d.OrderBy(x => Fitness(x)).Take(d.Count / 2).ToList();
            var newPopulation = new List<Division>(list.Count * 2);

            for (int i = 0; i + 1 < list.Count(); i += 2) {
                var p1 = list[i];
                var p2 = list[i + 1];
                newPopulation.Add(new Division(p1, p2));
                newPopulation.Add(new Division(p1, p2));
                newPopulation.Add(p1);
                newPopulation.Add(p2);
            }

            return newPopulation;
        }

        public static Random r = new Random();

        public static bool RandomBool() {
            var res = r.Next(10000000) % 2 == 0 ? false : true;
            return res;
        }

        public static List<int> array;
        static void Main(string[] args) {
            array = new List<int> {
                57147,51000,13356,71124,98368,67225,49387,90851,66290,39572,38164,97006,15544,51436,89610,41522,27798,15528,16433,44290,47133,90226,26872,52251,41604,21268,9134,55783,70743,17562,79060,73980,70528,35680,91072,52030,23810,79500,1606,46364,76867,72136,71040,29216,96748,46416,40198,55906,57676,68589,78795,83462,50720,30962,31778,28645,94528,47830,98221,61664,73940,24396,66285,2970,81612,52500,26284,3380,51437,45359,20159,59399,45005,95422,19948,3212,3180,34384,31900,51110,10190,14088,95684,11395,8700,33276,1480,516,46252,51312,2254,5947,66084,37200,65309,21104,60984,10747,89270,42882
            };
            const int PopulationSize = 1000;
            const int PopulationCount = 1000;

            var population = new List<Division>();
            for (int i = 0; i < PopulationCount; i++)
                population.Add(new Division(array.Count));

            for (int i = 0; i < PopulationSize; i++)
                population = Iterate(population);

            var best = population.OrderBy(x => Fitness(x)).First();
            Console.WriteLine(Fitness(best));
            Console.WriteLine(string.Join(",", best.division));
        }
    }
}
