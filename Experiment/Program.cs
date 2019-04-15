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
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Experiment
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> result = new List<int>();

            foreach (string file in Directory.EnumerateFiles(@"C:\AutoMAT\Data\B0627\Backup\XML"))
            {
                XDocument xdoc = XDocument.Load(file);

                var nsmgr = new XmlNamespaceManager(new NameTable());
                nsmgr.AddNamespace("jp", "http://www.jpo.go.jp");

                var pageNum = xdoc.XPathSelectElement("//jp:total-pages", nsmgr).Value;

                int lenOfText = 0;

                foreach(XText text in (IEnumerable)xdoc.XPathEvaluate("//*/text()"))
                {
                    lenOfText += text.Value.Length;
                }

                result.Add(lenOfText / int.Parse(pageNum));

//                break;
            }

            Console.WriteLine(result.Average());
            Console.WriteLine(result.Max());
            Console.WriteLine(result.Min());

        }


    }
}