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
        static void ThreadMethod()
        {
            for(int i=0; i<10; i++)
            {
                Console.WriteLine("ThreadProc:{0}", i);

                Thread.Sleep(0);
            }
        }

        static void Main(string[] args)
        {
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.IsBackground = true;
            t.Start();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Main thread: Do some work");

                Thread.Sleep(0);
            }

            t.Join();
        }
    }
}
