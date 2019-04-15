using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Experiment
{
    class HashFileTest
    {
        static void Execute()
        {
            MD5 md5 = MD5.Create();

            using (ZipArchive archive = ZipFile.OpenRead(@"C:\Temp\JE2.zip"))
            {
                foreach (var e in archive.Entries)
                {
                    using (var stream = e.Open())
                    {
                        byte[] bs = md5.ComputeHash(stream);

                        foreach (byte b in bs)
                        {
                            Console.Write(b.ToString("x2"));
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
