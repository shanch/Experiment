using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Experiment
{
    static class Program
    {
        static void Main(string[] args)
        {
            var result = 0;

            IntConverter.Reverse(12345, ref result);

            Console.WriteLine(result);

        }
    }
}
