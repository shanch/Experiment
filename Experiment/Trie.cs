using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Experiment
{
    public class TrieTester
    {
        public static void DoTest()
        {
            Trie trie = new Trie(4);

            foreach (string f in Directory.EnumerateFiles(@"\\jptokyo1tsprc01\c$\AutoMAT\Data\A0207\Backup\SJIS", "*.sjis"))
            {
                Console.WriteLine(f);

                foreach (string line in File.ReadLines(f, Encoding.Default))
                {
                    //Console.WriteLine(line);
                    trie.ParseSentence(line);
                }

                //break;
            }

            //trie.PrinInfo();
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fileStr = new FileStream("ngram.bin", FileMode.Create))
            {
                formatter.Serialize(fileStr, trie);
            }

            Console.WriteLine("Completed serialize");

            Trie deserializedTrie;

            using (FileStream fileStr = new FileStream("ngram.bin", FileMode.Open))
            {
                deserializedTrie = formatter.Deserialize(fileStr) as Trie;
            }

            Console.WriteLine(deserializedTrie.Count("^あいう"));
            Console.WriteLine(deserializedTrie.Count("いう"));
            Console.WriteLine(deserializedTrie.Count("え$"));
        }
    }

    [Serializable]
    public class Trie
    {
        public int N { get; private set; }

        private CharNode rootNode;

        public Trie(int n)
        {
            N = n;
            rootNode = new CharNode();
        }

        public void ParseSentence(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return;

            s = "^" + s + "$";

            for (int i = 0; i < s.Length - 1; i++)
            {
                int len = N;
                if (i + N > s.Length)
                {
                    len = s.Length - i;
                }
                Put(s.Substring(i, len));
            }
        }

        public long Count(string s)
        {
            char[] ca = s.ToCharArray();

            CharNode cn = rootNode;

            for (int i = 0; i < ca.Length; i++)
            {
                if (cn.HasChild(ca[i]))
                {
                    if (i == ca.Length - 1)
                    {
                        return cn[ca[i]].Count;
                    }
                    cn = cn[ca[i]];
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }

        public void Put(string s)
        {
            char[] cs = s.ToCharArray();

            if (!rootNode.HasChild(cs[0]))
            {
                rootNode.AddChild(cs[0]);
            }

            CharNode cn = rootNode[cs[0]];
            cn.Count++;

            for (int i = 1; i < cs.Length; i++)
            {
                if (!cn.HasChild(cs[i]))
                {
                    cn.AddChild(cs[i]);
                }

                cn = cn[cs[i]];
                cn.Count++;
            }
        }

        public void PrinInfo()
        {
            PrinInfo(rootNode, 0);
        }

        public void PrinInfo(CharNode cn, int depth)
        {
            Console.Write(string.Join("", Enumerable.Repeat(" ", depth)));
            Console.WriteLine($"{cn.Value}=>{cn.Count}");

            if (!cn.HasChild())
            {
                return;
            }

            foreach (CharNode childNode in cn)
            {
                PrinInfo(childNode, depth + 1);
            }
        }
    }

    [Serializable]
    public class CharNode : IEnumerable<CharNode>
    {
        public char Value { get; private set; }

        public long Count { get; set; }

        private Dictionary<char, CharNode> childNodes;

        public CharNode this[char c]
        {
            get
            {
                return childNodes[c];
            }
        }

        public void AddChild(char c)
        {
            if (childNodes == null)
            {
                childNodes = new Dictionary<char, CharNode>();
            }
            childNodes.Add(c, new CharNode(c));
        }

        public CharNode()
        {
        }

        public CharNode(char c)
        {
            Value = c;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (CharNode cn in childNodes.Values)
            {
                yield return cn;
            }
        }

        public IEnumerator<CharNode> GetEnumerator()
        {
            foreach (CharNode cn in childNodes.Values)
            {
                yield return cn;
            }
        }

        public bool HasChild(char c)
        {
            if (childNodes == null)
            {
                return false;
            }
            return childNodes.ContainsKey(c);
        }

        public bool HasChild()
        {
            if (childNodes == null)
            {
                return false;
            }
            return childNodes.Count > 0;
        }
    }
}
