using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Numbers;

namespace Test_Numbers
{
    [TestClass]
    public class Test_Integer
    {
        [TestMethod]
        public void Check_ToString_SmallValues()
        {
            CNumber_Integer a = new CNumber_Integer(42);

            Assert.AreEqual("42", a.ToString(10));
            Assert.AreEqual("0b10.1010", a.ToString(2));
            Assert.AreEqual("0o52", a.ToString(8));
            Assert.AreEqual("0x2A", a.ToString(16));
            Assert.AreEqual("[17]28", a.ToString(17));
        }

        [TestMethod]
        public void Check_ToString_BigValues()
        {
            CNumber_Integer a = new CNumber_Integer(0x2345678);

            Assert.AreEqual("36.984.440", a.ToString(10));
            Assert.AreEqual("0o2.1505.3170", a.ToString(8));
            Assert.AreEqual("0x234.5678", a.ToString(16));
            Assert.AreEqual("[17]190.DEE5", a.ToString(17));
        }

        [TestMethod]
        public void Check_ToString_64bit()
        {
            long c = 0x40000000000;
            CNumber_Integer a = new CNumber_Integer(c);
            Assert.AreEqual(0x40000000000, c);

            Assert.AreEqual("4.398.046.511.104", a.ToString(10));
            Assert.AreEqual("0o100.0000.0000.0000", a.ToString(8));
            Assert.AreEqual("0x400.0000.0000", a.ToString(16));
            Assert.AreEqual("[17]231.818D.67B4", a.ToString(17));
        }

        [TestMethod]
        public void Check_Size()
        {
            CNumber_Integer a = new CNumber_Integer(42);
            Assert.AreEqual(1, a.size);

            CNumber_Integer b = new CNumber_Integer(0x400000000000);
            Assert.AreEqual(2, b.size);
        }

    }
}
