using MMC.Numbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNumber_Test
{
    
    
    /// <summary>
    ///Dies ist eine Testklasse für "CNumber_IntegerTest" und soll
    ///alle CNumber_IntegerTest Komponententests enthalten.
    ///</summary>
    [TestClass()]
    public class CNumber_IntegerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Testkontext auf, der Informationen
        ///über und Funktionalität für den aktuellen Testlauf bietet, oder legt diesen fest.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Zusätzliche Testattribute
        // 
        //Sie können beim Verfassen Ihrer Tests die folgenden zusätzlichen Attribute verwenden:
        //
        //Mit ClassInitialize führen Sie Code aus, bevor Sie den ersten Test in der Klasse ausführen.
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Mit ClassCleanup führen Sie Code aus, nachdem alle Tests in einer Klasse ausgeführt wurden.
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen.
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///Ein Test für "IsZero"
        ///</summary>
        [TestMethod()]
        public void IsZeroTest()
        {
            CNumber_Integer zero = new CNumber_Integer(0.0);
            Assert.IsTrue(zero.IsZero);
            CNumber_Integer one = new CNumber_Integer(1.0);
            Assert.IsFalse(one.IsZero);
        }

        /// <summary>
        ///Ein Test für "IsNegative"
        ///</summary>
        [TestMethod()]
        public void IsNegativeTest()
        {
            CNumber_Integer a = new CNumber_Integer(1.0);
            Assert.IsFalse(a.IsNegative);

            CNumber_Integer b = new CNumber_Integer(-1.0);
            Assert.IsTrue(b.IsNegative);
        }

        /// <summary>
        ///Ein Test für "AsInteger"
        ///</summary>
        [TestMethod()]
        public void AsIntegerTest()
        {
            CNumber_Integer a = new CNumber_Integer(1.0);
            Assert.AreEqual(a.AsInteger,1);
            CNumber_Integer b = new CNumber_Integer(-1.0);
            Assert.AreEqual(b.AsInteger, -1);
        }

        /// <summary>
        ///Ein Test für "Trim"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Calc.exe")]
        public void TrimTest()
        {
            CNumber_Integer_Accessor target = new CNumber_Integer_Accessor(); // TODO: Passenden Wert initialisieren
            target.Insert(0);
            target.Insert(2);
            target.Trim();
            Assert.AreEqual(target._Values.Count, 1);
        }

        /// <summary>
        ///Ein Test für "FromString"
        ///</summary>
        [TestMethod()]
        public void FromStringTest()
        {
            string Input = "0x1.0000.0000";
            CNumber_Integer_Accessor target = new CNumber_Integer_Accessor(Input);
            Assert.AreEqual(2,target._Values.Count);
            Assert.AreEqual("0x1.0000.0000", target.AsString);
        }

        /// <summary>
        ///Ein Test für "ToString"
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            CNumber_Integer_Accessor target = new CNumber_Integer_Accessor();
            string a = "0x1.0000.0000";
            target.FromString(a);
            string b = target.ToString(16);
            Assert.AreEqual(a, b);
        }

        /// <summary>
        ///Ein Test für "Subtraction"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Calc.exe")]
        public void SubtractionTest()
        {
            CNumber_Integer_Accessor target = new CNumber_Integer_Accessor();
            CNumber_Integer other = new CNumber_Integer(0.0);
            target.FromString("0x5");
            other.FromString("0x7");
            target.Subtraction(other);
            Assert.AreEqual("-0x2", target.AsString);

            target.FromString("0x1.0000.0005");
            other.FromString("0x7");
            target.Subtraction(other);
            Assert.AreEqual("0xFFFF.FFFE", target.AsString);

            target.FromString("0x1.0000.0005");
            other.FromString("0x1.0000.0007");
            target.Subtraction(other);
            Assert.AreEqual("-0x2", target.AsString);

            target.FromString("0x7");
            other.FromString("0x1.0000.0005");
            target.Subtraction(other);
            Assert.AreEqual("-0xFFFF.FFFE", target.AsString);

            target.FromString("0x1.0000.0000.0000.0005");
            other.FromString("0x1.0000.0007");
            target.Subtraction(other);
            Assert.AreEqual("0xFFFF.FFFE.FFFF.FFFE", target.AsString);
        }

        /// <summary>
        ///Ein Test für "ShiftRight"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Calc.exe")]
        public void ShiftRightTest()
        {
            CNumber_Integer_Accessor target = new CNumber_Integer_Accessor("0x1.0000.000F");
            uint actual = target.ShiftRight(4);
            Assert.AreEqual("0x1000.0000",target.AsString);
            Assert.AreEqual((uint) 0x0F, actual);
        }

        /// <summary>
        ///Ein Test für "ShiftLeft"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Calc.exe")]
        public void ShiftLeftTest()
        {
            CNumber_Integer_Accessor target = new CNumber_Integer_Accessor("0x1000.0000");
            uint actual = target.ShiftLeft(4);
            Assert.AreEqual("0x1.0000.0000", target.AsString);
            Assert.AreEqual((uint) 1, actual);

            target.FromString("0x1");
            actual = target.ShiftLeft(32);
            Assert.AreEqual("0x1.0000.0000", target.AsString);
            Assert.AreEqual((uint)0, actual);
        }

        /// <summary>
        ///Ein Test für "Not"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Calc.exe")]
        public void NotTest()
        {
            CNumber_Integer_Accessor target = new CNumber_Integer_Accessor(); // TODO: Passenden Wert initialisieren
            target.Not();
            Assert.Inconclusive("Eine Methode, die keinen Wert zurückgibt, kann nicht überprüft werden.");
        }

        /// <summary>
        ///Ein Test für "neg"
        ///</summary>
        [TestMethod()]
        public void negTest()
        {
            CNumber_Integer target = new CNumber_Integer(0.0); // TODO: Passenden Wert initialisieren
            CNumber expected = null; // TODO: Passenden Wert initialisieren
            CNumber actual;
            actual = target.neg();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "Multiply"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Calc.exe")]
        public void MultiplyTest()
        {
            CNumber_Integer_Accessor target = new CNumber_Integer_Accessor("0x7FFF.FFFF");
            target.Multiply(2);
            Assert.AreEqual("0xFFFF.FFFE", target.AsString);
        }

        /// <summary>
        ///Ein Test für "mul"
        ///</summary>
        [TestMethod()]
        public void mulTest()
        {
            CNumber_Integer target = new CNumber_Integer("0x7FFF.FFFF");
            CNumber_Integer two = new CNumber_Integer("0x2");
            target = (CNumber_Integer) target.mul(two);
            Assert.AreEqual("0xFFFF.FFFE",target.AsString);

            target = (CNumber_Integer)target.mul(two);
            Assert.AreEqual("0x1.FFFF.FFFC", target.AsString);
        }

        /// <summary>
        ///Ein Test für "Insert"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Calc.exe")]
        public void InsertTest()
        {
            CNumber_Integer_Accessor target = new CNumber_Integer_Accessor(); // TODO: Passenden Wert initialisieren
            uint Value = 0; // TODO: Passenden Wert initialisieren
            target.Insert(Value);
            Assert.Inconclusive("Eine Methode, die keinen Wert zurückgibt, kann nicht überprüft werden.");
        }

        /// <summary>
        ///Ein Test für "Equals"
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            CNumber_Integer a = new CNumber_Integer("0x1234.5678");
            CNumber_Integer b = new CNumber_Integer("0x1234.5678");
            Assert.IsTrue(a.Equals(b));
        }

        /// <summary>
        ///Ein Test für "Divide"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Calc.exe")]
        public void DivideTest()
        {
            CNumber_Integer_Accessor target = new CNumber_Integer_Accessor("0xFFFF.FFFF");
            uint rem = target.Divide(2);
            Assert.AreEqual((uint) 1, rem);
            Assert.AreEqual("0x7FFF.FFFF", target.AsString);
        }

        /// <summary>
        ///Ein Test für "Divide"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Calc.exe")]
        public void DivideTest2()
        {
            CNumber_Integer_Accessor a = new CNumber_Integer_Accessor("0x1234");
            CNumber_Integer b = new CNumber_Integer("0x12");
            CNumber_Integer rem = a.Divide(b);
            Assert.AreEqual("0x102", a.AsString);
            Assert.AreEqual("0x10", rem.AsString);
        }
                
        /// <summary>
        ///Ein Test für "div"
        ///</summary>
        [TestMethod()]
        public void divTest()
        {
            CNumber_Integer a = new CNumber_Integer("0x1234");
            CNumber_Integer b = new CNumber_Integer("0x12");
            CNumber_Integer c = (CNumber_Integer) a.div(b);
            Assert.AreEqual("0x102", c.AsString);
        }

        /// <summary>
        ///Ein Test für "rem"
        ///</summary>
        [TestMethod()]
        public void remTest()
        {
            CNumber_Integer a = new CNumber_Integer("0x1234");
            CNumber_Integer b = new CNumber_Integer("0x12");
            CNumber_Integer c = (CNumber_Integer)a.rem(b);
            Assert.AreEqual("0x10", c.AsString);
        }

        /// <summary>
        ///Ein Test für "Clone"
        ///</summary>
        [TestMethod()]
        public void CloneTest()
        {
            CNumber_Integer target = new CNumber_Integer(42.0);
            CNumber_Integer copy = (CNumber_Integer) target.Clone();

            Assert.IsTrue(target.Equals(copy));
        }

        /// <summary>
        ///Ein Test für "Addition"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Calc.exe")]
        public void AdditionTest1()
        {
            CNumber_Integer_Accessor target = new CNumber_Integer_Accessor();
            target.FromString("0xFFFFFFFE");
            target.Addition(7);
            Assert.AreEqual("0x1.0000.0005",target.AsString);
        }

        /// <summary>
        ///Ein Test für "Addition"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Calc.exe")]
        public void AdditionTest()
        {
            CNumber_Integer_Accessor target = new CNumber_Integer_Accessor();
            CNumber_Integer other = new CNumber_Integer(0.0);
            target.FromString("0x5");
            other.FromString("0x7");
            target.Addition(other);
            Assert.AreEqual("0xC", target.AsString);

            target.FromString("0xFFFFFFFE");
            other.FromString("0x7");
            target.Addition(other);
            Assert.AreEqual("0x1.0000.0005", target.AsString);

            target.FromString("0xFFFFFFFE");
            other.FromString("0x100000007");
            target.Addition(other);
            Assert.AreEqual("0x2.0000.0005", target.AsString);

            target.FromString("0x1FFFFFFFE");
            other.FromString("0x100000007");
            target.Addition(other);
            Assert.AreEqual("0x3.0000.0005", target.AsString);

            target.FromString("0xFFFFFFFFFFFFFFFE");
            other.FromString("0x7");
            target.Addition(other);
            Assert.AreEqual("0x1.0000.0000.0000.0005", target.AsString);

            target.FromString("0x1.FFFF.FFFF.FFFF.FFFE");
            other.FromString("0x7");
            target.Addition(other);
            Assert.AreEqual("0x2.0000.0000.0000.0005", target.AsString);

            target.FromString("0x7");
            other.FromString("0x1.FFFF.FFFF.FFFF.FFFE");
            target.Addition(other);
            Assert.AreEqual("0x2.0000.0000.0000.0005", target.AsString);

            target.FromString("0x1.FFFF.FFFF.FFFF.FFFE");
            other.FromString("0x1.0000.0007");
            target.Addition(other);
            Assert.AreEqual("0x2.0000.0001.0000.0005", target.AsString);

            target.FromString("0x1.FFFF.FFFE.FFFF.FFFE");
            other.FromString("0x1.0000.0007");
            target.Addition(other);
            Assert.AreEqual("0x2.0000.0000.0000.0005", target.AsString);
        }

        /// <summary>
        ///Ein Test für "pow"
        ///</summary>
        [TestMethod()]
        public void powTest()
        {
            CNumber_Integer target = new CNumber_Integer(0.0); // TODO: Passenden Wert initialisieren
            CNumber a = null; // TODO: Passenden Wert initialisieren
            CNumber expected = null; // TODO: Passenden Wert initialisieren
            CNumber actual;
            actual = target.pow(a);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }
    }
}
