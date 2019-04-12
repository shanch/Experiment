using NMeCab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment
{
    public class MecabRunner
    {
        public void Run()
        {
            try
            {
                string sentence = "ユーザが本明細書において提供れるような方法";

                MeCabParam param = new MeCabParam();
                param.DicDir = @"..\..\dic\ipadic";

                MeCabTagger t = MeCabTagger.Create(param);
                MeCabNode node = t.ParseToNode(sentence);
                while (node != null)
                {
                    if (node.CharType > 0)
                    {
                        Console.WriteLine(node.Surface + "\t" + node.Feature);
                    }
                    node = node.Next;
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.Read();
            }

        }
    }
}
