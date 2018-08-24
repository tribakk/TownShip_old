using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownShip_Form.Kernal;

namespace TownShip_Form.Tests.CProductCalcTest
{
    /// <summary>
    /// Summary description for CProductCalcTest
    /// </summary>
    [TestClass]
    public class CProductCalcTest
    {
        public CProductCalcTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestAdd()
        {
            CProductCalc testCalc = CProductCalcFactory.Create();
            testCalc.Add("хлеб", 1);
            Assert.AreEqual(testCalc.GetArray().GetCount(), 1);
        }

        [TestMethod]
        public void TestGetFactory()
        {
            CProductCalc testCalc = CProductCalcFactory.Create();
            Assert.AreNotEqual(testCalc.GetFactoryList(), null);
        }

        [TestMethod]
        public void TestSetArray()
        {
            CProductCalc testCalc = CProductCalcFactory.Create();
            CSmartArray testArray = CSmartArrayFactory.Create();
            testArray.Add(ProductTag.ptBublic, 1);
            testCalc.SetArray(testArray);
            Assert.AreEqual(testCalc.GetArray().GetTagCount(ProductTag.ptBublic), 1);
        }
        [TestMethod]
        public void TestSetAlreadyHave()
        {
            CProductCalc testCalc = CProductCalcFactory.Create();
            CSmartArray testArray = CSmartArrayFactory.Create();
            testArray.Add(ProductTag.ptBublic, 1);
            testCalc.SetAlreadyHave(testArray);
            Assert.AreEqual(testCalc.GetAlreadyHave().GetTagCount(ProductTag.ptBublic), 1);
        }
        [TestMethod]
        public void TestCalc()
        {
            CProductCalc testCalc = CProductCalcFactory.Create();
            CSmartArray testArray = CSmartArrayFactory.Create();
            testArray.Add(ProductTag.ptBublic, 1);
            testCalc.SetArray(testArray);

            CSmartArray ingridientArray = CSmartArrayFactory.Create();
            //должны получиться ингридиенты которые надо выращить и только они
            ingridientArray.Add("пшеница", 4);
            ingridientArray.Add("морковь", 1);
            ingridientArray.Add("сахарный тросник", 1);

            testCalc.Calc(5);
            Assert.IsTrue(testCalc.GetArray().IsEqual(ingridientArray));
        }

        [TestMethod]
        public void TestExcludeWhatHave()
        {
            CProductCalc testCalc1 = CProductCalcFactory.Create();
            CSmartArray testArray = CSmartArrayFactory.Create();
            testArray.Add(ProductTag.ptBublic, 1);
            testCalc1.SetArray(testArray);
            testCalc1.Calc(5);

            CSmartArray ingridientArray = CSmartArrayFactory.Create();
            ingridientArray.Add("пшеница", 4);
            ingridientArray.Add("морковь", 8);
            //ingridientArray.Add("сахарный тросник", 1);

            CProductCalc testCalc2 = CProductCalcFactory.Create();
            testCalc2.SetArray(ingridientArray);

            testCalc1.ExcludeWhatHave(testCalc2);
            Assert.AreEqual(testCalc1.GetArray().GetCount(), 1);
            Assert.AreEqual(testCalc1.GetArray().GetTagCount("сахарный тросник"), 1);

            Assert.AreEqual(testCalc2.GetArray().GetCount(), 1);
            Assert.AreEqual(testCalc2.GetArray().GetTagCount("морковь"), 7);
        }
    }
}
