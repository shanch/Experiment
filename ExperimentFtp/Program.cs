using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentFtp
{
    class Program
    {
        static void Main(string[] args)
        {
            DoDownload().Wait();
        }

        static async Task DoDownload()
        {
            string uri = "ftp://wwwftp.wipo.int/text/142019/2019-14-JP.zip";
            //string uri = "ftp://wwwftp.wipo.int/biblio/142019/PCT_142019_xml.lst";
            //            string uri = "ftp://jptokyo1tsapf01/thshdic/je/dic_usr/Yugenkoshi02.jau";

            string downloadFile = "test.zip";
            try
            {
                using (MATWebClient wc = new MATWebClient())
                {
                    wc.Credentials = new NetworkCredential("derwent", "irt34ref");
                    wc.DownloadProgressChanged += (s, e) =>
                    {
                        Console.WriteLine($"downloaded {e.BytesReceived} of {e.TotalBytesToReceive} bytes. {e.ProgressPercentage} % complete...");
                    };
                    wc.DownloadFileCompleted += new AsyncCompletedEventHandler((s, e) =>
                    {
                        Console.WriteLine("DownloadFileCompleted");
                    });

                    await wc.DownloadFileTaskAsync(new Uri(uri), downloadFile);

                    Console.WriteLine("ダウンロード完了：" + downloadFile);
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.StackTrace);
            }
        }
    }

    public class MATWebClient : System.Net.WebClient
    {
        public int Timeout { get; set; } = 120 * 60 * 1000;

        protected override WebRequest GetWebRequest(Uri address)
        {
            var w = base.GetWebRequest(address);
            w.Timeout = Timeout;
            ((FtpWebRequest)w).KeepAlive = false;

            //Console.WriteLine(((FtpWebRequest)w).UsePassive);

            return w;
        }
    }

}
