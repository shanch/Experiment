using System;
namespace Experiment
{
    public class IntConverter
    {
        public static void Reverse(int input, ref int result)
        {
            Console.WriteLine("input={0},result={1}", input, result);

            int m = input % 10; // 5
            int d = input / 10; // 1234

            Console.WriteLine("m={0},d={1}", m, d);

            if (d > 0)
            {
                result += m * (int)Math.Pow(10, (int)Math.Log10(input));

                Reverse(d, ref result);
            }
            else
            {
                result += m;
            }
        }

    }
}
