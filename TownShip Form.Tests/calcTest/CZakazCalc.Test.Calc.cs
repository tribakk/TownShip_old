using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownShip_Form.Kernal;

namespace TownShip_Form.Tests.calcFactory
{
    /// <summary>
    /// Summary description for CZakazCalc
    /// </summary>
    /// 
    [TestClass]
    public class CZakazCalc
    {
        public CZakazCalc()
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
        
        public static CSmartArray TestCalc(CFactory pFact, string name, int productCount = 1)
        {
            CSmartArray array = new CSmartArray();
            array.Add(name, productCount);
            pFact.SetInput(array);
            pFact.Calc();
            CSmartArray tempArray = pFact.GetResult();
            pFact.Refresh();
            return tempArray;
        }

        public class CFactoryTest
        {
            CSmartArray m_InputIngredients;
            CFactory m_pFactory;
            int m_FactoryProductCount = 0;
            public CFactoryTest(FactoryType type)
            {
                m_pFactory = FactoryFactory.CreateFactory(type);
            }
            public void Reset()
            {
                m_InputIngredients = new CSmartArray();
            }
            public void Add(string name, int count)
            {
                m_InputIngredients.Add(name, count);
            }
            public void TestProduct(string productName, int productCount = 1)
            {
                CSmartArray calcArray = CZakazCalc.TestCalc(m_pFactory, productName, productCount);
                Assert.IsTrue(calcArray.IsEqual(m_InputIngredients));
                m_FactoryProductCount++;
            }
            public void TestFactoryCount()
            {
                Assert.AreEqual(m_pFactory.GetInputArray().Count, m_FactoryProductCount);
            }
        }

        [TestMethod]
        public void TextExcludeWhatHave()
        {
            CFactory test1 = FactoryFactory.CreateFactory(FactoryType.ftBakery);
            CFactory test2 = FactoryFactory.CreateFactory(FactoryType.ftBakery);
            CSmartArray array1 = new CSmartArray();
            CSmartArray array2 = new CSmartArray();
            array1.Add("хлеб", 1);
            array2.Add("хлеб", 1);
            Assert.AreEqual(array1.GetCount(), 1);
            test1.SetInput(array1);
            Assert.AreEqual(array1.GetCount(), 0);
            Assert.AreEqual(array2.GetCount(), 1);
            test2.SetInput(array2);
            Assert.AreEqual(array2.GetCount(), 0);
            test1.ExcludeWhatHave(test2);
            Assert.AreEqual(test1.GetSmartInput().GetCount(), 0);
            Assert.AreEqual(test2.GetSmartInput().GetCount(), 1);
        }

        [TestMethod]
        public void TestExcludeWhatHave()
        {

        }

        [TestMethod]
        public void TestMilcFactory()
        {
            CMilkFactory pFact = new CMilkFactory();
            int count = pFact.GetInputArray().Count;
            int counter = 0;
            {
                CSmartArray correctArray = new CSmartArray();
                correctArray.Add("молоко", 1);
                CSmartArray calcArray = TestCalc(pFact, "сливки");
                Assert.IsTrue(calcArray.IsEqual(correctArray));
                counter++;
            }
            {
                CSmartArray correctArray = new CSmartArray();
                correctArray.Add("молоко", 2);
                CSmartArray calcArray = TestCalc(pFact, "сыр");
                Assert.IsTrue(calcArray.IsEqual(correctArray));
                counter++;
            }
            {
                CSmartArray correctArray = new CSmartArray();
                correctArray.Add("молоко", 3);
                CSmartArray calcArray = TestCalc(pFact, "масло");
                Assert.IsTrue(calcArray.IsEqual(correctArray));
                counter++;
            }
            {
                CSmartArray correctArray = new CSmartArray();
                correctArray.Add("молоко", 4);
                CSmartArray calcArray = TestCalc(pFact, "йогурт");
                Assert.IsTrue(calcArray.IsEqual(correctArray));
                counter++;
            }
        }
        [TestMethod]
        public void TestBackeryFactory()
        {
            CBakery pFact = new CBakery();
            int count = pFact.GetInputArray().Count;
            int counter = 0;
            {
                CSmartArray correctArray = new CSmartArray();
                correctArray.Add("пшеница", 2);
                CSmartArray calcArray = TestCalc(pFact, "хлеб");
                Assert.IsTrue(calcArray.IsEqual(correctArray));
                counter++;
            }
            {
                CSmartArray correctArray = new CSmartArray();
                correctArray.Add("пшеница", 2);
                correctArray.Add("яйцо", 2);
                CSmartArray calcArray = TestCalc(pFact, "печенье");
                Assert.IsTrue(calcArray.IsEqual(correctArray));
                counter++;
            }
            {
                CSmartArray correctArray = new CSmartArray();
                correctArray.Add("пшеница", 2);
                correctArray.Add("сахар", 1);
                correctArray.Add("яйцо", 3);
                CSmartArray calcArray = TestCalc(pFact, "бублик");
                Assert.IsTrue(calcArray.IsEqual(correctArray));
                counter++;
            }
            {
                CSmartArray correctArray = new CSmartArray();
                correctArray.Add("пшеница", 2);
                correctArray.Add("сыр", 1);
                correctArray.Add("томат", 2);
                CSmartArray calcArray = TestCalc(pFact, "пицца");
                Assert.IsTrue(calcArray.IsEqual(correctArray));
                counter++;
            }
            {
                CSmartArray correctArray = new CSmartArray();
                correctArray.Add("пшеница", 2);
                correctArray.Add("картофель", 2);
                correctArray.Add("яйцо", 4);
                CSmartArray calcArray = TestCalc(pFact, "картофельный хлеб");
                Assert.IsTrue(calcArray.IsEqual(correctArray));
                counter++;
            }
            {
                CSmartArray correctArray = new CSmartArray();
                correctArray.Add("пшеница", 2);
                correctArray.Add("банан", 3);
                correctArray.Add("яйцо", 2);
                correctArray.Add("масло", 1);
                CSmartArray calcArray = TestCalc(pFact, "банановый хлеб");
                Assert.IsTrue(calcArray.IsEqual(correctArray));
                counter++;
            }
            
            Assert.AreEqual(counter, count);
        }
        [TestMethod]
        public void TestMilcFactory2()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftMilk);
            test.Reset();
            test.Add("молоко", 1);
            test.TestProduct("сливки");

            test.Reset();
            test.Add("молоко", 2);
            test.TestProduct("сыр");

            test.Reset();
            test.Add("молоко", 3);
            test.TestProduct("масло");

            test.Reset();
            test.Add("молоко", 4);
            test.TestProduct("йогурт");

            test.TestFactoryCount();
        }

        [TestMethod]
        public void TestWeavingFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftWeaving);

            test.Reset();
            test.Add("хлопок", 2);
            test.TestProduct("хлопковая ткань");

            test.Reset();
            test.Add("шерсть", 2);
            test.TestProduct("пряжа");

            test.Reset();
            test.Add("шелк", 2);
            test.TestProduct("шелковая ткань");

            test.TestFactoryCount();
        }


        [TestMethod]
        public void TestSewingFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftSewing);

            test.Reset();
            test.Add("хлопковая ткань", 1);
            test.TestProduct("рубашка");

            test.Reset();
            test.Add("пряжа", 1);
            test.TestProduct("свитер");

            test.Reset();
            test.Add("хлопковая ткань", 1);
            test.Add("пряжа", 1);
            test.TestProduct("пальто");

            test.Reset();
            test.Add("хлопковая ткань", 1);
            test.Add("шелковая ткань", 1);
            test.TestProduct("шляпа");

            test.Reset();
            test.Add("пряжа", 1);
            test.Add("шелковая ткань", 1);
            test.TestProduct("платье");

            test.Reset();
            test.Add("хлопковая ткань", 1);
            test.Add("пряжа", 1);
            test.Add("шелковая ткань", 1);
            test.TestProduct("костюм");

            test.TestFactoryCount();
        }

        [TestMethod]
        public void TestSnackFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftSnack);

            test.Reset();
            test.Add("кукуруза", 2);
            test.TestProduct("попкорн");

            test.Reset();
            test.Add("кукуруза", 3);
            test.TestProduct("кукурузные чипсы");

            test.Reset();
            test.Add("пшеница", 2);
            test.Add("клубника", 2);
            test.TestProduct("гранола");

            test.Reset();
            test.Add("картофель", 2);
            test.TestProduct("чипсы");

            test.Reset();
            test.Add("оливки", 2);
            test.Add("виноград", 2);
            test.Add("сыр", 1);
            test.TestProduct("канопе");

            test.Reset();
            test.Add("бекон", 2);
            test.Add("сироп", 1);
            test.TestProduct("глазированный бекон");

            test.TestFactoryCount();
        }

        [TestMethod]
        public void TestFastFoodFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftFastFood);

            test.Reset();
            test.Add("молоко", 2);
            test.Add("клубника", 1);
            test.TestProduct("милкшейк");

            test.Reset();
            test.Add("хлеб", 2);
            test.Add("сыр", 1);
            test.Add("томат", 1);
            test.TestProduct("чизбургер");

            test.Reset();
            test.Add("хлеб", 1);
            test.Add("масло", 1);
            test.Add("клубника", 2);
            test.TestProduct("сэндвич");

            test.Reset();
            test.Add("картофель", 2);
            test.Add("сливки", 1);
            test.Add("клубника", 2);
            test.TestProduct("картошка фри");

            test.Reset();
            test.Add("картофель", 2);
            test.Add("бекон", 1);
            test.Add("сыр", 1);
            test.TestProduct("печеный картофель");

            test.TestFactoryCount();
        }

        [TestMethod]
        public void TestRubberFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftRubber);

            test.Reset();
            test.Add("каучук", 1);
            test.TestProduct("резина");

            test.Reset();
            test.Add("каучук", 2);
            test.TestProduct("пластик");

            test.Reset();
            test.Add("каучук", 3);
            test.TestProduct("клей");

            test.TestFactoryCount();
        }


        [TestMethod]
        public void TestSugarFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftSugar);

            test.Reset();
            test.Add("сахарный тросник", 1);
            test.TestProduct("сахар");

            test.Reset();
            test.Add("сахарный тросник", 2);
            test.TestProduct("сироп");

            test.Reset();
            test.Add("сахарный тросник", 3);
            test.TestProduct("карамель");

            test.Reset();
            test.Add("сахарный тросник", 1);
            test.Add("соты с медом", 1);
            test.TestProduct("медовая карамель");

            test.TestFactoryCount();
        }


        [TestMethod]
        public void TestPaperFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftPaper);

            test.Reset();
            test.Add("сосна", 1);
            test.TestProduct("бумага");

            test.Reset();
            test.Add("сосна", 2);
            test.TestProduct("бумажные полотенца");

            test.Reset();
            test.Add("сосна", 2);
            test.Add("резина", 1);
            test.TestProduct("обои");

            test.TestFactoryCount();
        }


        [TestMethod]
        public void TestIceCreamFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftIceCream);

            test.Reset();
            test.Add("молоко", 1);
            test.Add("сливки", 1);
            test.Add("сахар", 1);
            test.TestProduct("мороженое");

            test.Reset();
            test.Add("клубника", 2);
            test.Add("сахар", 1);
            test.TestProduct("фруктовый лед");

            test.Reset();
            test.Add("йогурт", 1);
            test.Add("сливки", 1);
            test.TestProduct("замороженный йогурт");

            test.Reset();
            test.Add("сосна", 1);
            test.Add("какао", 1);
            test.Add("сироп", 1);
            test.TestProduct("эскимо");

            test.TestFactoryCount();
        }

        [TestMethod]
        public void TestJamFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftJam);

            test.Reset();
            test.Add("клубника", 3);
            test.TestProduct("клубничное варенье");

            test.Reset();
            test.Add("персик", 3);
            test.TestProduct("персиковый конфитюр");

            test.Reset();
            test.Add("арбуз", 3);
            test.TestProduct("арбузный джем");

            test.Reset();
            test.Add("слива", 3);
            test.TestProduct("сливовое повидло");

            test.Reset();
            test.Add("виноград", 3);
            test.TestProduct("виноградное желе");

            test.TestFactoryCount();
        }

        [TestMethod]
        public void TestAnimalFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftAnimal);

            test.Reset();
            test.Add("корм для коров", 1);
            test.TestProduct("молоко", 1);
            test.Reset();
            test.Add("корм для коров", 1);
            test.TestProduct("молоко", 2);
            test.Reset();
            test.Add("корм для коров", 1);
            test.TestProduct("молоко", 3);
            test.Reset();
            test.Add("корм для коров", 2);
            test.TestProduct("молоко", 4);

            test.Reset();
            test.Add("корм для куриц", 1);
            test.TestProduct("яйцо", 1);
            test.Reset();
            test.Add("корм для куриц", 1);
            test.TestProduct("яйцо", 2);
            test.Reset();
            test.Add("корм для куриц", 1);
            test.TestProduct("яйцо", 3);
            test.Reset();
            test.Add("корм для куриц", 2);
            test.TestProduct("яйцо", 4);

            test.Reset();
            test.Add("корм для овец", 1);
            test.TestProduct("шерсть", 1);
            test.Reset();
            test.Add("корм для овец", 1);
            test.TestProduct("шерсть", 2);
            test.Reset();
            test.Add("корм для овец", 1);
            test.TestProduct("шерсть", 3);
            test.Reset();
            test.Add("корм для овец", 2);
            test.TestProduct("шерсть", 4);

            test.Reset();
            test.Add("корм для пчел", 1);
            test.TestProduct("соты с медом", 1);
            test.Reset();
            test.Add("корм для пчел", 1);
            test.TestProduct("соты с медом", 2);
            test.Reset();
            test.Add("корм для пчел", 1);
            test.TestProduct("соты с медом", 3);
            test.Reset();
            test.Add("корм для пчел", 2);
            test.TestProduct("соты с медом", 4);

            //test.TestFactoryCount();
        }

        [TestMethod]
        public void TestKormFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftKorm);

            test.Reset();
            test.Add("пшеница", 2);
            test.Add("кукуруза", 1);
            test.TestProduct("корм для коров");

            test.Reset();
            test.Add("пшеница", 2);
            test.Add("морковь", 1);
            test.TestProduct("корм для куриц");

            test.Reset();
            test.Add("морковь", 2);
            test.Add("кукуруза", 2);
            test.TestProduct("корм для овец");

            test.Reset();
            test.Add("пшеница", 3);
            test.Add("сахарный тросник", 1);
            test.TestProduct("корм для пчел");

            test.Reset();
            test.Add("пшеница", 2);
            test.Add("морковь", 2);
            test.Add("кукуруза", 1);
            test.TestProduct("корм для свиней");

            test.TestFactoryCount();
        }

        [TestMethod]
        public void TestPlasticFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftPlastic);

            test.Reset();
            test.Add("пластик", 1);
            test.TestProduct("пластиковая бутылка");

            test.Reset();
            test.Add("пластик", 1);
            test.Add("резина", 1);
            test.TestProduct("игрушка");

            test.Reset();
            test.Add("пластик", 1);
            test.Add("клей", 1);
            test.TestProduct("мяч");

            test.Reset();
            test.Add("резина", 2);
            test.Add("клей", 1);
            test.TestProduct("надувная лодка");

            test.TestFactoryCount();
        }

        [TestMethod]
        public void TestConfectioneryFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftConfectionery);

            test.Reset();
            test.Add("пшеница", 3);
            test.Add("сахар", 1);
            test.Add("яйцо", 4);
            test.TestProduct("кекс");

            test.Reset();
            test.Add("какао", 2);
            test.Add("сироп", 1);
            test.Add("масло", 1);
            test.TestProduct("шоколадный пирог");

            test.Reset();
            test.Add("сахар", 1);
            test.Add("яйцо", 5);
            test.Add("сливки", 1);
            test.TestProduct("пироженое");

            test.Reset();
            test.Add("бублик", 1);
            test.Add("карамель", 1);
            test.Add("какао", 1);
            test.TestProduct("пончик");

            test.Reset();
            test.Add("печенье", 1);
            test.Add("сыр", 1);
            test.Add("сироп", 1);
            test.Add("клубника", 2);
            test.TestProduct("чизкейк");

            test.Reset();
            test.Add("пшеница", 3);
            test.Add("соты с медом", 2);
            test.Add("яйцо", 1);
            test.TestProduct("медовый пряник");

            test.Reset();
            test.Add("пшеница", 3);
            test.Add("лайм", 2);
            test.Add("сахар", 1);
            test.Add("сливки", 1);
            test.TestProduct("лаймовый пирог");

            test.Reset();
            test.Add("кокос", 2);
            test.Add("яйцо", 2);
            test.Add("сахар", 1);
            test.TestProduct("кокосовые макаруны");


            test.TestFactoryCount();
        }

        [TestMethod]
        public void TestCandyFactory()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftCandy);

            test.Reset();
            test.Add("клубника", 3);
            test.Add("сироп", 1);
            test.TestProduct("драже");

            test.Reset();
            test.Add("карамель", 1);
            test.Add("масло", 1);
            test.TestProduct("ириски");

            test.TestFactoryCount();
        }
        [TestMethod]
        public void TestMexicanRestaurant()
        {
            CFactoryTest test = new CFactoryTest(FactoryType.ftMexican);

            test.Reset();
            test.Add("томат", 3);
            test.Add("перец", 3);
            test.Add("сахар", 1);
            test.TestProduct("соус чили");

            test.Reset();
            test.Add("пшеница", 2);
            test.Add("бекон", 2);
            test.Add("томат", 1);
            test.TestProduct("буррито");

            test.Reset();
            test.Add("кукурузные чипсы", 1);
            test.Add("сыр", 1);
            test.Add("перец", 2);
            test.TestProduct("начос");

            test.TestFactoryCount();
        }
        [TestMethod]
        public void TestFactory()
        {
            int count = Enum.GetNames(typeof(FactoryType)).Length;
            
            for (int i = 0; i < count; i++)
            {
                CFactory fact = FactoryFactory.CreateFactory((FactoryType)i);
                if (fact == null)
                    continue;
                bool good = fact.TestFactory();
                if (!good)
                    Assert.IsTrue(good);

            }
        }


    }
}