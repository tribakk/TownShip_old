using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownShip_Form.Kernal;


namespace TownShip_Form.Tests.CSmartArrayTest
{
    /// <summary>
    /// Summary description for CSmartArray
    /// </summary>
    [TestClass]
    public class CSmartArrayTest
    {
        public CSmartArrayTest()
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
        public void AddByName()
        {
            CSmartArray testArray = CSmartArrayFactory.Create();
            testArray.Add("хлеб", 1);
            Assert.AreEqual(testArray.GetMap().Count, 1);
            testArray.Add("пшеница", 0);
            Assert.AreEqual(testArray.GetCount(), 1);
        }
        [TestMethod]
        public void AddByTag()
        {
            CSmartArray testArray = CSmartArrayFactory.Create();
            testArray.Add(ProductTag.prKormKurica, 1);
            Assert.AreEqual(testArray.GetMap().Count, 1);
        }
        [TestMethod]
        public void AddErrorInfo()
        {
            {
                CSmartArray testArray = CSmartArrayFactory.Create();
                testArray.Add(ProductTag.ptNotFound, 1);
                Assert.AreEqual(testArray.GetCount(), 0);
            }
            //{
            //    bool bException = false;
            //    CSmartArray testArray = CSmartArrayFactory.Create();
            //    try
            //    {
            //        testArray.Add("хлебб", 1);
            //    }
            //    catch
            //    {
            //        bException = true;
            //    }
            //    Assert.IsTrue(bException);
            //}
            //testArray.Add(null, 1);
            //testArray.Add((ProductTag)120000, 1);
            //testArray.Add("хлебб",1);
            //testArray.Add("Хлеб", 1);
            
            //Assert.AreEqual(testArray.GetMap().Count, 1);
        }
        [TestMethod]
        public void TestGetMap()
        {
            CSmartArray testArray = CSmartArrayFactory.Create();
            Assert.IsTrue(testArray.GetMap() != null);
        }
        [TestMethod]
        public void TestCopy()
        {
            CSmartArray testArray = CSmartArrayFactory.Create();
            testArray.Add("хлеб", 1);
            CSmartArray copyArray = testArray.Copy();
            Assert.AreEqual(copyArray.GetCount(), 1);
            Assert.AreEqual(copyArray.GetTagCount("хлеб"), 1);
        }

        [TestMethod]
        public void TestMerge()
        {
            CSmartArray testArray1 = CSmartArrayFactory.Create();
            CSmartArray testArray2 = CSmartArrayFactory.Create();
            testArray1.Add(ProductTag.prKormKurica, 3);
            testArray2.Add(ProductTag.prKormPchela, 2);
            testArray1.Merge(testArray2);
            Assert.AreEqual(testArray1.GetCount(), 2);
            Assert.AreEqual(testArray2.GetCount(), 1);
        }
        [TestMethod]
        public void TestClearTag()
        {
            CSmartArray testArray = CSmartArrayFactory.Create();
            testArray.Add(ProductTag.prKormKurica, 3);
            testArray.Add(ProductTag.ptArbuz, 2);
            testArray.ClearTag(ProductTag.prKormPchela);
            testArray.ClearTag(ProductTag.prKormKurica);
            Assert.AreEqual(testArray.GetCount(), 1);
        }
        [TestMethod]
        public void TestGetTagCountById()
        {
            CSmartArray testArray = CSmartArrayFactory.Create();
            testArray.Add(ProductTag.prKormKurica, 3);
            Assert.AreEqual(testArray.GetTagCount(ProductTag.prKormKurica), 3);
            testArray.Add(ProductTag.prKormKurica, 2);
            Assert.AreEqual(testArray.GetTagCount(ProductTag.prKormKurica), 5);

            testArray.Add(ProductTag.prKormPchela, 2);
            Assert.AreEqual(testArray.GetTagCount(ProductTag.prKormPchela), 2);
        }
        [TestMethod]
        public void TestGetTagCountByName()
        {
            CSmartArray testArray = CSmartArrayFactory.Create();
            testArray.Add("хлеб", 3);
            Assert.AreEqual(testArray.GetTagCount("хлеб"), 3);
            testArray.Add("хлеб", 2);
            Assert.AreEqual(testArray.GetTagCount("хлеб"), 5);

            testArray.Add("пшеница", 2);
            Assert.AreEqual(testArray.GetTagCount("пшеница"), 2);
        }
        [TestMethod]
        public void TestFillArray()
        {
            CSmartArray testArray = CSmartArrayFactory.Create();
            List<ProductTag> ptList = new List<ProductTag>();
            List<int> iList = new List<int>();

            testArray.Add(ProductTag.prKormKurica, 5);
            testArray.Add(ProductTag.prKormPchela, 8);

            testArray.FillArray(ptList, iList);
            Assert.AreEqual(ptList[0], ProductTag.prKormKurica);
            Assert.AreEqual(ptList[1], ProductTag.prKormPchela);

            Assert.AreEqual(iList[0], 5);
            Assert.AreEqual(iList[1], 8);
        }
        [TestMethod]
        public void TestExcludeWhatHave()
        {
            {
                CSmartArray testArray1 = CSmartArrayFactory.Create();
                CSmartArray testArray2 = CSmartArrayFactory.Create();

                testArray1.Add(ProductTag.prKormKurica, 3);
                testArray2.Add(ProductTag.prKormKurica, 2);

                testArray1.ExcludeWhatHave(testArray2);
                Assert.AreEqual(testArray1.GetTagCount(ProductTag.prKormKurica), 1);
                Assert.AreEqual(testArray2.GetTagCount(ProductTag.prKormKurica), 0);
            }

            {
                CSmartArray testArray1 = CSmartArrayFactory.Create();
                CSmartArray testArray2 = CSmartArrayFactory.Create();

                testArray1.Add(ProductTag.prKormKurica, 3);
                testArray2.Add(ProductTag.prKormKurica, 8);

                testArray1.ExcludeWhatHave(testArray2);
                Assert.AreEqual(testArray1.GetTagCount(ProductTag.prKormKurica), 0);
                Assert.AreEqual(testArray2.GetTagCount(ProductTag.prKormKurica), 5);
            }
        }

        [TestMethod]
        public void TestIsEqual()
        {
            {
                CSmartArray testArray1 = CSmartArrayFactory.Create();
                CSmartArray testArray2 = CSmartArrayFactory.Create();

                testArray1.Add(ProductTag.prKormKurica, 3);
                testArray2.Add(ProductTag.prKormKurica, 3);

                Assert.IsTrue(testArray1.IsEqual(testArray2));
            }

            {
                CSmartArray testArray1 = CSmartArrayFactory.Create();
                CSmartArray testArray2 = CSmartArrayFactory.Create();

                testArray1.Add(ProductTag.prKormKurica, 3);
                testArray2.Add(ProductTag.prKormKurica, 2);

                Assert.IsFalse(testArray1.IsEqual(testArray2));
            }

            {
                CSmartArray testArray1 = CSmartArrayFactory.Create();
                CSmartArray testArray2 = CSmartArrayFactory.Create();

                testArray1.Add(ProductTag.prKormKurica, 3);
                testArray2.Add(ProductTag.ptAnanasSorbet, 2);

                Assert.IsFalse(testArray1.IsEqual(testArray2));
            }
        }
        [TestMethod]
        public void TestIsEmpty()
        {
            CSmartArray testArray = CSmartArrayFactory.Create();
            Assert.IsTrue(testArray.IsEmpty());
        }
    }
}
