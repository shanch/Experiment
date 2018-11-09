using System;
using Experiment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentTest
{
    [TestClass]
    public class DivideIntByIntTest
    {
        [TestMethod]
        public void TestUseDouble()
        {
            int result = 0;

            for (int i = 0; i < int.MaxValue; i++)
            {
                result = DivideIntByInt.UseDouble(7, 3);
            }

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestNoDouble()
        {
            int result = 0;

            for (int i = 0; i < int.MaxValue; i++)
            {
                result = DivideIntByInt.NoDouble(7, 3);
            }

            Assert.AreEqual(3, result);
        }

    }
}
