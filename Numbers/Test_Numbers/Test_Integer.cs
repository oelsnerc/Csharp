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
            Assert.AreEqual(6, b.size);
        }

        [TestMethod]
        public void Check_Multiplication_01()
        {
            CNumber_Integer three = new CNumber_Integer(3);

            CNumber_Integer a = new CNumber_Integer(42);
            CNumber_Integer b = (CNumber_Integer) a.mul(three);

            // check if the operdands are unchanged
            Assert.AreEqual(42, a.AsInteger);
            Assert.AreEqual(3, three.AsInteger);
            
            // check the result
            Assert.AreEqual(126, b.AsInteger);
            Assert.AreEqual(1, b.size);
        }

        [TestMethod]
        public void Check_Multiplication_02()
        {
            CNumber_Integer three = new CNumber_Integer(3);

            CNumber_Integer a = new CNumber_Integer(126);
            CNumber_Integer b = (CNumber_Integer)a.mul(three);

            Assert.AreEqual(378, b.AsInteger);
            Assert.AreEqual(2, b.size);
        }

        [TestMethod]
        public void Check_Multiplication_03()
        {
            CNumber_Integer a = new CNumber_Integer(378);
            CNumber_Integer b = (CNumber_Integer)a.mul(a);

            Assert.AreEqual(142884, b.AsInteger);
            Assert.AreEqual(3, b.size);
        }

        [TestMethod]
        public void ShiftLeft_01()
        {
            CNumber_Integer a = new CNumber_Integer(0xAB);
            uint overflow = a.ShiftLeft(4);

            Assert.AreEqual(0xAB0, a.AsInteger);
            Assert.AreEqual(2, a.size);
            Assert.AreEqual(0xAU, overflow);
        }

        [TestMethod]
        public void ShiftLeft_02()
        {
            CNumber_Integer a = new CNumber_Integer(0xABCD);
            uint overflow = a.ShiftLeft(4);

            Assert.AreEqual(0xABCD0, a.AsInteger);
            Assert.AreEqual(3, a.size);
            Assert.AreEqual(0xAU, overflow);
        }

        [TestMethod]
        public void ShiftLeft_03()
        {
            CNumber_Integer a = new CNumber_Integer(0xABCD);
            uint overflow = a.ShiftLeft(12);

            Assert.AreEqual(0xABCD000, a.AsInteger);
            Assert.AreEqual(4, a.size);
            Assert.AreEqual(0xAU, overflow);
        }

        [TestMethod]
        public void ShiftRight_01()
        {
            CNumber_Integer a = new CNumber_Integer(0xAB);
            uint underflow = a.ShiftRight(4);

            Assert.AreEqual(0xA, a.AsInteger);
            Assert.AreEqual(1, a.size);
            Assert.AreEqual(0xBU, underflow);
        }

        [TestMethod]
        public void ShiftRight_02()
        {
            CNumber_Integer a = new CNumber_Integer(0xABCD);
            uint underflow = a.ShiftRight(4);

            Assert.AreEqual(0xABC, a.AsInteger);
            Assert.AreEqual(2, a.size);
            Assert.AreEqual(0xDU, underflow);
        }

        [TestMethod]
        public void ShiftRight_03()
        {
            CNumber_Integer a = new CNumber_Integer(0xABCDEF);
            uint underflow = a.ShiftRight(12);

            Assert.AreEqual(0xABC, a.AsInteger);
            Assert.AreEqual(2, a.size);
            Assert.AreEqual(0xDU, underflow);
        }

        [TestMethod]
        public void ShiftRight_04()
        {
            CNumber_Integer a = new CNumber_Integer(0xABCDEF);
            uint underflow = a.ShiftRight(32);

            Assert.AreEqual(0, a.AsInteger);
            Assert.AreEqual(1, a.size);
            Assert.AreEqual(0xABU, underflow);
        }
    }
}
