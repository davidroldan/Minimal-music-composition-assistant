using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicaMinimalista.Objects.Utils
{
    public class RNG
    {
        private static Random rand = new Random(Environment.TickCount);

        public static int generate()
        {
            return rand.Next();
        }

        public static int generate(int min, int max)
        {
            return (rand.Next() % (max - min + 1)) + min;
        }

        public static int generateModulo100()
        {
            return rand.Next() % 100;
        }

        public static bool generateBool()
        {
            return rand.Next() % 2 == 0;
        }

        //Box-muller algorithm
        /*public static double SampleGaussian(Random random, double mean, double stddev)
        {
            // The method requires sampling from a uniform random of (0,1]
            // but Random.NextDouble() returns a sample of [0,1).
            double x1 = 1 - random.NextDouble();
            double x2 = 1 - random.NextDouble();

            double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
            return y1 * stddev + mean;
        }*/
    }
}
