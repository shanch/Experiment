using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Experiment
{
    class AsyncCopyTest
    {
        static void Execute()
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
