using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Experiment
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void HashFileTest()
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

        static void AsyncCopyTest()
        {
            Uri uri = new Uri(@"X:\Dictionary\MATDictionaryDataCompressor\JE.zip");

            var client = new WebClient();
            client.DownloadProgressChanged += (sender, e) =>
            {
                Console.WriteLine(e.ProgressPercentage);
            };

            client.DownloadFileTaskAsync(uri, @"C:\Temp\JE.zip").Wait();
        }
    }
}