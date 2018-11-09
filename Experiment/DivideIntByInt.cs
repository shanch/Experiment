using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment
{
    public class DivideIntByInt
    {
        public static int UseDouble(int a, int b)
        {
            return (int)Math.Ceiling((double)a / b);
        }

        public static int NoDouble(int a, int b)
        {
            return (a + b - 1) / b;
        }
    }
}
