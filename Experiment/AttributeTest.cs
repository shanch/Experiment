using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Experiment
{
    class AttributeTest
    {
        static void Run()
        {
            Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly a in asms)
            {
                Type[] ts = a.GetTypes();

                foreach (Type t in ts)
                {
                    ProcessAttribute processAttr = Attribute.GetCustomAttribute(t, typeof(ProcessAttribute)) as ProcessAttribute;

                    if (processAttr != null)
                    {
                        Console.WriteLine(t.ToString());
                        Console.WriteLine(processAttr.Name);
                        Console.WriteLine(processAttr.Description);
                    }

                    foreach(Attribute attr in Attribute.GetCustomAttributes(t, typeof(ParamAttribute)))
                    {
                        ParamAttribute paramAttr = attr as ParamAttribute;

                        if (paramAttr != null)
                        {
                            Console.WriteLine(paramAttr.Name);
                            Console.WriteLine(paramAttr.Type);
                        }
                    }

                    /*Console.WriteLine("名前:{0}", t.Name);
                    Console.WriteLine("名前空間:{0}", t.Namespace);
                    Console.WriteLine("完全限定名:{0}", t.FullName);
                    Console.WriteLine("このメンバを宣言するクラス:{0}", t.DeclaringType);
                    Console.WriteLine("直接の継承元:{0}", t.BaseType);
                    Console.WriteLine("属性:{0}", t.Attributes);
                    Console.WriteLine();*/
                }
            }

            //GetAllAuthors(typeof(AuthorTest));
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ProcessAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; set; }

        public ProcessAttribute(string name)
        {
            this.Name = name;
        }
    }

    public enum ParamType { str, file, folder };

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ParamAttribute : Attribute
    {
        public string Name { get; }
        public ParamType Type { get; set; }

        public ParamAttribute(string name)
        {
            this.Name = name;
        }
    }

    [Process("Procedure", Description= "Bad Rate")]
    [Param("pubdate", Type = ParamType.str)]
    [Param("postdir", Type = ParamType.folder)]
    class AuthorTest
    {
    }

}