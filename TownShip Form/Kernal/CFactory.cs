using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownShip_Form.Kernal
{
    public enum FactoryType
    {
        ftField = 0,
        ftMilk = 1,
        ftBakery = 2,
        ftWeaving = 3,
        ftSugar = 4,
        ftRubber = 5,
        ftPlastic = 6,
        ftPaper = 7,
        ftSewing = 8,
        ftSnack = 9,
        ftFastFood = 10,
        ftIceCream = 11,
        ftConfectionery = 12,
        ftAnimal = 13,
        ftKorm = 14,
        ftJam = 15,
        ftCandy = 16,
        ftIsland = 17,
        ftFuture = 18,
        ftMexican = 19
    }
    public class CFactory
    {
        protected CAllProductSpisok spisok = new CAllProductSpisok();
        protected List<ProductTag> m_TagInputArray = new List<ProductTag>();
        protected List<int> m_InputCount;
        protected CSmartArray m_SmartInput;
        protected CSmartArray m_Result;
        protected PriorityTag m_Priority;
        protected FactoryType m_factoryType;

        public CFactory(FactoryType type)
        {
            m_factoryType = type;
        }

        public void Refresh()
        {
            m_InputCount = new List<int>();
            m_SmartInput = new CSmartArray();
            m_Result = new CSmartArray();

            int count = m_TagInputArray.Count();
            for (int i = 0; i < count; i++)
            {
                m_InputCount.Add(0);
            }
        }

        protected void Init()
        {
            Refresh();
        }

        public bool TestFactory()
        {
            List<ProductTag> testArray = new List<ProductTag>();
            for (int i = 0; i < spisok.GetCount(); i++)
            {
                ProductTagString tag = spisok.GetProductTagString(i);
                if (tag.m_FactoryType == m_factoryType)
                {
                    testArray.Add(tag.m_Tag);
                }
            }
            int testCount = testArray.Count;
            int inputCount = m_TagInputArray.Count;
            if (testCount != inputCount)
                return false;
            for (int i = 0; i<inputCount; i++)
            {
                ProductTag tag = m_TagInputArray[i];
                bool bFound = true;
                for (int j = 0; j<testCount; j++)
                {
                    if (testArray[j] == tag)
                    {
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                    return false;
            }
            return true;

        }
        virtual public void Calc()
        {
        }
        virtual public string GetFactName()
        {
            return "";
        }
    	virtual public void SetInput(CSmartArray Array)
        {
            int count = m_TagInputArray.Count;
            for (int i = 0; i < count; i++)
            {
                ProductTag tag = m_TagInputArray[i];
                int ProductCount = Array.GetTagCount(tag);
                m_InputCount[i] = ProductCount;
                m_SmartInput.Add(tag, ProductCount);
                Array.ClearTag(tag);
            }
        }

        public void ExcludeWhatHave(CFactory pFactory)
        {
            m_SmartInput.ExcludeWhatHave(pFactory.GetSmartInput().Copy());
        }
        public PriorityTag GetPriorety()
        {
            return m_Priority;
        }

        public void UpdateResult(CSmartArray arr)
        {
            if (arr == null)
                return;
            arr.Merge(m_Result);
            m_Result.RemoveAll();
        }
        public List<ProductTag> GetInputArray()
        {
            return m_TagInputArray;
        }

        public int GetTagCount(ProductTag tag)
        {
            return m_SmartInput.GetTagCount(tag);
        }

        public CSmartArray GetSmartInput()
        {
            return m_SmartInput;
        }

        public bool IsEmpty()
        {
            return m_SmartInput.IsEmpty();
        }


        void AddResult(String name, int count)
        {
            m_Result.Add(spisok.GetTag(name), count);
        }
        public CSmartArray GetResult()
        {
            return m_Result;
        }
        
        public String Print()
        {
            if (m_SmartInput.IsEmpty())
                return "";
            //String pr;
            String result = Environment.NewLine + GetFactName() + Environment.NewLine;
            result += m_SmartInput.Print();
            return result;
        }
    };

    public class CMilkFactory : CFactory
    {
        public CMilkFactory()
            : base(FactoryType.ftMilk)
        {
            m_TagInputArray.Add(spisok.GetTag(("сливки")));
            m_TagInputArray.Add(spisok.GetTag(("сыр")));
            m_TagInputArray.Add(spisok.GetTag(("масло")));
            m_TagInputArray.Add(spisok.GetTag(("йогурт")));
            Init();
            m_Priority = PriorityTag.one;
        }
        public override String GetFactName() 
        {
            return ("Молочная фабрика");
        }
        public override void Calc()
        {

            int count = 0;
            int counter = 0;
            
            {
                //сливки
                count = m_InputCount[counter];

                ProductTag tag = spisok.GetTag("молоко");
                m_Result.Add(spisok.GetTag(("молоко")), 1 * count);
                counter++;
            }
            
            {
                //сливки
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("молоко")), 2 * count);
                counter++;
            }
            {
                //сливки
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("молоко")), 3 * count);
                counter++;
            }
            {
                //сливки
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("молоко")), 4 * count);
                counter++;
            }
        }

    };

    public class CBakery : CFactory
    {
        public CBakery()
            : base(FactoryType.ftBakery)
        {
            m_TagInputArray.Add(spisok.GetTag(("хлеб")));
            m_TagInputArray.Add(spisok.GetTag(("печенье")));
            m_TagInputArray.Add(spisok.GetTag(("бублик")));
            m_TagInputArray.Add(spisok.GetTag(("пицца")));
            m_TagInputArray.Add(spisok.GetTag(("картофельный хлеб")));
            m_TagInputArray.Add(spisok.GetTag(("банановый хлеб")));
            m_Priority = PriorityTag.one;
            Init();
        }
        public override String GetFactName()
        {
            return ("пекарня");
        }

        public override void Calc()
        {
            int count = 0;
            int counter = 0;
            {
                //хлеб
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 2 * count);
                counter++;
            }
            {
                //печенье
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 2 * count);
                m_Result.Add(spisok.GetTag(("яйцо")), 2 * count);
                counter++;
            }
            {
                //бублик
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 2 * count);
                m_Result.Add(spisok.GetTag(("яйцо")), 3 * count);
                m_Result.Add(spisok.GetTag(("сахар")), 1 * count);
                counter++;
            }
            {
                //пицца
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 2 * count);
                m_Result.Add(spisok.GetTag(("сыр")), 1 * count);
                m_Result.Add(spisok.GetTag(("томат")), 2 * count);
                counter++;
            }

            {
                //картофельный хлеб
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 2 * count);
                m_Result.Add(spisok.GetTag(("картофель")), 2 * count);
                m_Result.Add(spisok.GetTag(("яйцо")), 4 * count);
                counter++;
            }

            {
                //банановый хлеб
                count = m_InputCount[5];
                m_Result.Add(spisok.GetTag(("пшеница")), 2 * count);
                m_Result.Add(spisok.GetTag(("банан")), 3 * count);
                m_Result.Add(spisok.GetTag(("яйцо")), 2 * count);
                m_Result.Add(spisok.GetTag(("масло")), 1 * count);
            }
        }

    };

    public class CWeavingFactory : CFactory
    {
        public CWeavingFactory()
            : base(FactoryType.ftWeaving)
        {
            m_TagInputArray.Add(spisok.GetTag(("хлопковая ткань")));
            m_TagInputArray.Add(spisok.GetTag(("пряжа")));
            m_TagInputArray.Add(spisok.GetTag(("шелковая ткань")));
            m_Priority = PriorityTag.one;
            Init();
        }
        public override String GetFactName()
        {
            return ("Ткацкая фабрика");
        }
        public override void Calc()
        {
            int count = 0;
            int counter = 0;
            {
                //хлопковая ткань
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("хлопок")), 2 * count);
                counter++;
            }
            {
                //пряжа
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("шерсть")), 2 * count);
                counter++;
            }
            {
                //шелковая ткань
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("шелк")), 2 * count);
                counter++;
            }
        }
    };

    public class CSewingFactory : CFactory
    {
        public CSewingFactory()
            : base(FactoryType.ftSewing)
        {
            m_TagInputArray.Add(spisok.GetTag(("рубашка")));
            m_TagInputArray.Add(spisok.GetTag(("свитер")));
            m_TagInputArray.Add(spisok.GetTag(("пальто")));
            m_TagInputArray.Add(spisok.GetTag(("шляпа")));
            m_TagInputArray.Add(spisok.GetTag(("платье")));
            m_TagInputArray.Add(spisok.GetTag(("костюм")));
            m_Priority = PriorityTag.two;
            Init();
        }
        public override String GetFactName()
        {
            return ("Швейная фабрика");
        }

        public override void Calc()
        {
            int count = 0;
            int counter = 0;
            {
                //рубашка
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("хлопковая ткань")), 1 * count);
                counter++;
            }
            {
                //свитер
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пряжа")), 1 * count);
                counter++;
            }
            {
                //пальто
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("хлопковая ткань")), 1 * count);
                m_Result.Add(spisok.GetTag(("пряжа")), 1 * count);
                counter++;
            }
            {
                //шляпа
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("хлопковая ткань")), 1 * count);
                m_Result.Add(spisok.GetTag(("шелковая ткань")), 1 * count);
                counter++;
            }
            {
                //платье
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пряжа")), 1 * count);
                m_Result.Add(spisok.GetTag(("шелковая ткань")), 1 * count);
                counter++;
            }
            {
                //костюм
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("хлопковая ткань")), 1 * count);
                m_Result.Add(spisok.GetTag(("пряжа")), 1 * count);
                m_Result.Add(spisok.GetTag(("шелковая ткань")), 1 * count);
                counter++;
            }
        }
    };

    public class CSnackFactory : CFactory
    {
        public CSnackFactory()
            : base(FactoryType.ftSnack)
        {
            m_TagInputArray.Add(spisok.GetTag(("попкорн")));
            m_TagInputArray.Add(spisok.GetTag(("кукурузные чипсы")));
            m_TagInputArray.Add(spisok.GetTag(("гранола")));
            m_TagInputArray.Add(spisok.GetTag(("чипсы")));
            m_TagInputArray.Add(spisok.GetTag(("канопе")));
            m_TagInputArray.Add(spisok.GetTag(("глазированный бекон")));
            m_Priority = PriorityTag.three;
            Init();
        }
        public override String GetFactName()
        {
            return ("Фабрика закусок");
        }
        public override void Calc()
        {
            int count = 0;
            int counter = 0;
            {
                //попкорн
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("кукуруза")), 2 * count);
                counter++;
            }
            {
                //кукурузные чипсы
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("кукуруза")), 3 * count);
                counter++;
            }
            {
                //гранола
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 2 * count);
                m_Result.Add(spisok.GetTag(("клубника")), 2 * count);
                counter++;
            }
            {
                //чипсы
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("картофель")), 2 * count);
                counter++;
            }
            {
                //канапе
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("оливки")), 2 * count);
                m_Result.Add(spisok.GetTag(("виноград")), 2 * count);
                m_Result.Add(spisok.GetTag(("сыр")), 1 * count);

                counter++;
            }
            {
                //канапе
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("бекон")), 2 * count);
                m_Result.Add(spisok.GetTag(("сироп")), 1 * count);

                counter++;
            }
        }
    };

    public class CFastFoodFactory : CFactory
    {
        public CFastFoodFactory()
            : base(FactoryType.ftFastFood)
        {
            m_TagInputArray.Add(spisok.GetTag(("милкшейк")));
            m_TagInputArray.Add(spisok.GetTag(("чизбургер")));
            m_TagInputArray.Add(spisok.GetTag(("сэндвич")));
            m_TagInputArray.Add(spisok.GetTag(("картошка фри")));
            m_TagInputArray.Add(spisok.GetTag(("печеный картофель")));
            m_Priority = PriorityTag.three;
            Init();
        }
        public override String GetFactName()
        {
            return ("Фабрика фастфуда");
        }
        public override void Calc()
        {
            int count = 0;
            int counter = 0;
            {
                //милкшейк
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("молоко")), 2 * count);
                m_Result.Add(spisok.GetTag(("клубника")), 1 * count);
                counter++;
            }
            {
                //чизбургер
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("хлеб")), 2 * count);
                m_Result.Add(spisok.GetTag(("сыр")), 1 * count);
                m_Result.Add(spisok.GetTag(("томат")), 1 * count);
                counter++;
            }
            {
                //сэндвич
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("хлеб")), 1 * count);
                m_Result.Add(spisok.GetTag(("масло")), 1 * count);
                m_Result.Add(spisok.GetTag(("клубника")), 2 * count);
                counter++;
            }
            {
                //картошка фри
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("картофель")), 2 * count);
                m_Result.Add(spisok.GetTag(("сливки")), 1 * count);
                m_Result.Add(spisok.GetTag(("томат")), 2 * count);
                counter++;
            }
            {
                //печеный картофель
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("картофель")), 2 * count);
                m_Result.Add(spisok.GetTag(("бекон")), 1 * count);
                m_Result.Add(spisok.GetTag(("сыр")), 1 * count);

                counter++;
            }
        }
    };

    public class CRubberFactory : CFactory
    {
        public CRubberFactory()
            : base(FactoryType.ftRubber)
        {
            m_TagInputArray.Add(spisok.GetTag(("резина")));
            m_TagInputArray.Add(spisok.GetTag(("пластик")));
            m_TagInputArray.Add(spisok.GetTag(("клей")));
            int count = m_TagInputArray.Count;
            m_Priority = PriorityTag.three;
            Init();
        }
        public override String GetFactName()
        {
            return ("Каучуковая фабрика");
        }
        public override void Calc()
        {

            int counter = 0;
            int count = 0;
            {
                //резина
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("каучук")), 1 * count);
                counter++;
            }
            {
                //пластик
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("каучук")), 2 * count);
                counter++;
            }
            {
                //клей
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("каучук")), 3 * count);
                counter++;
            }
        }
    };

    public class CSugarFactory : CFactory
    {
        public CSugarFactory()
            : base(FactoryType.ftSugar)
        {
            m_TagInputArray.Add(spisok.GetTag(("сахар")));
            m_TagInputArray.Add(spisok.GetTag(("сироп")));
            m_TagInputArray.Add(spisok.GetTag(("карамель")));
            m_TagInputArray.Add(spisok.GetTag(("медовая карамель")));
            m_Priority = PriorityTag.two;
            Init();
        }
        public override String GetFactName()
        {
            return ("Сахарная фабрика");
        }
        public override void Calc()
        {
            int counter = 0;
            int count = 0;
            {
                //сахар
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("сахарный тросник")), 1 * count);
                counter++;
            }
            {
                //сироп
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("сахарный тросник")), 2 * count);
                counter++;
            }
            {
                //карамель
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("сахарный тросник")), 3 * count);
                counter++;
            }
            {
                //медовая карамель
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag("соты с медом"), 1 * count);
                m_Result.Add(spisok.GetTag("сахарный тросник"), 1 * count);
                counter++;
            }
        }
    };

    public class CPaperFactory : CFactory
    {
        public CPaperFactory()
            : base(FactoryType.ftPaper)
        {
            m_TagInputArray.Add(spisok.GetTag(("бумага")));
            m_TagInputArray.Add(spisok.GetTag(("бумажные полотенца")));
            m_TagInputArray.Add(spisok.GetTag(("обои")));
            m_Priority = PriorityTag.two;
            Init();
        }
        public override String GetFactName()
        {
            return ("Бумажная фабрика");
        }
        public override void Calc()
        {
            int counter = 0;
            int count = 0;
            {
                //бумага
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("сосна")), 1 * count);
                counter++;
            }
            {
                //бумажные полотенца
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("сосна")), 2 * count);
                counter++;
            }
            {
                //обои
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("сосна")), 2 * count);
                m_Result.Add(spisok.GetTag(("резина")), 1 * count);
                counter++;
            }
        }
    };

    public class CIceCreamFactory : CFactory
    {
        public CIceCreamFactory()
            : base(FactoryType.ftIceCream)
        {
            m_TagInputArray.Add(spisok.GetTag(("мороженое")));
            m_TagInputArray.Add(spisok.GetTag(("фруктовый лед")));
            m_TagInputArray.Add(spisok.GetTag(("замороженный йогурт")));
            m_TagInputArray.Add(spisok.GetTag(("эскимо")));
            //m_TagInputArray.Add(spisok.GetTag(("ананасовый сорбет")));
            m_Priority = PriorityTag.three;
            Init();
        }
        public override String GetFactName()
        {
            return ("Фабрика мороженного");
        }
        public override void Calc()
        {
            int counter = 0;
            int count = 0;
            {
                //мороженное
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("молоко")), 1 * count);
                m_Result.Add(spisok.GetTag(("сливки")), 1 * count);
                m_Result.Add(spisok.GetTag(("сахар")), 1 * count);
                counter++;
            }
            {
                //фруктовый лед
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("клубника")), 2 * count);
                m_Result.Add(spisok.GetTag(("сахар")), 1 * count);
                counter++;
            }
            {
                //замороженный йогурт
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("йогурт")), 1 * count);
                m_Result.Add(spisok.GetTag(("сливки")), 1 * count);
                counter++;
            }
            {
                //эскимо
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("сироп")), 1 * count);
                m_Result.Add(spisok.GetTag(("какао")), 1 * count);
                m_Result.Add(spisok.GetTag(("сосна")), 1 * count);
                counter++;
            }
            //{
            //    //ананасовый сорбет
            //    count = m_InputCount[counter];

            //    counter++;
            //}
        }
    };

    public class CСonfectioneryFactory : CFactory
    {
        public CСonfectioneryFactory()
            : base(FactoryType.ftConfectionery)
        {
            m_TagInputArray.Add(spisok.GetTag(("кекс")));
            m_TagInputArray.Add(spisok.GetTag(("шоколадный пирог")));
            m_TagInputArray.Add(spisok.GetTag(("пироженое")));
            m_TagInputArray.Add(spisok.GetTag(("пончик")));
            m_TagInputArray.Add(spisok.GetTag(("чизкейк")));
            m_TagInputArray.Add(spisok.GetTag(("медовый пряник")));
            m_TagInputArray.Add(spisok.GetTag(("лаймовый пирог")));
            m_TagInputArray.Add(spisok.GetTag(("кокосовые макаруны")));
            m_Priority = PriorityTag.three;
            Init();
        }
        public override String GetFactName()
        {
            return ("Кондитерская");
        }
        public override void Calc()
        {
            int counter = 0;
            int count = 0;
            {
                //кекс
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 3 * count);
                m_Result.Add(spisok.GetTag(("сахар")), 1 * count);
                m_Result.Add(spisok.GetTag(("яйцо")), 4 * count);
                counter++;
            }
            {
                //шоколадный пирог
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("какао")), 2 * count);
                m_Result.Add(spisok.GetTag(("сироп")), 1 * count);
                m_Result.Add(spisok.GetTag(("масло")), 1 * count);
                counter++;
            }
            {
                //пироженое
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("сахар")), 1 * count);
                m_Result.Add(spisok.GetTag(("яйцо")), 5 * count);
                m_Result.Add(spisok.GetTag(("сливки")), 1 * count);
                counter++;
            }
            {
                //пончик
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("бублик")), 1 * count);
                m_Result.Add(spisok.GetTag(("карамель")), 1 * count);
                m_Result.Add(spisok.GetTag(("какао")), 1 * count);
                counter++;
            }
            {
                //чизкейк
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("печенье")), 1 * count);
                m_Result.Add(spisok.GetTag(("сыр")), 1 * count);
                m_Result.Add(spisok.GetTag(("сироп")), 1 * count);
                m_Result.Add(spisok.GetTag(("клубника")), 2 * count);
                counter++;
            }
            {
                //медовый пряник
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 3 * count);
                m_Result.Add(spisok.GetTag(("соты с медом")), 2 * count);
                m_Result.Add(spisok.GetTag(("яйцо")), 1 * count);
                counter++;
            }
            {
                //лаймовый пирог
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 3 * count);
                m_Result.Add(spisok.GetTag(("лайм")), 2 * count);
                m_Result.Add(spisok.GetTag(("сахар")), 1 * count);
                m_Result.Add(spisok.GetTag(("сливки")), 1 * count);
                counter++;
            }
            {
                //кокосовые макаруны
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("кокос")), 2 * count);
                m_Result.Add(spisok.GetTag(("яйцо")), 2 * count);
                m_Result.Add(spisok.GetTag(("сахар")), 1 * count);
                counter++;
            }
        }
    };


    public class CAnimalFactory : CFactory
    {
        public CAnimalFactory()
            : base(FactoryType.ftAnimal)
        {
            m_TagInputArray.Add(spisok.GetTag(("молоко")));
            m_TagInputArray.Add(spisok.GetTag(("яйцо")));
            m_TagInputArray.Add(spisok.GetTag(("шерсть")));
            m_TagInputArray.Add(spisok.GetTag(("соты с медом")));
            m_TagInputArray.Add(spisok.GetTag(("бекон")));
            m_Priority = PriorityTag.three;
            Init();
        }
        public override String GetFactName()
        {
            return ("Продукты животных");
        }

        public override void Calc()
        {
            int counter = 0;
            int count = 0;
            {
                //молоко
                count = m_InputCount[counter];
                count = (int)Math.Ceiling((float)count / 3.0);
                m_Result.Add(spisok.GetTag(("корм для коров")), 1 * count);
                counter++;
            }
            {
                //яйцо
                count = m_InputCount[counter];
                count = (int)Math.Ceiling((float)count / 3.0);
                m_Result.Add(spisok.GetTag(("корм для куриц")), 1 * count);
                counter++;
            }
            {
                //шерсть
                count = m_InputCount[counter];
                count = (int)Math.Ceiling((float)count / 3.0);
                m_Result.Add(spisok.GetTag(("корм для овец")), 1 * count);
                counter++;
            }
            {
                //соты с медом
                count = m_InputCount[counter];
                count = (int)Math.Ceiling((float)count / 3.0);
                m_Result.Add(spisok.GetTag(("корм для пчел")), 1 * count);
                counter++;
            }
            {
                //бекон
                count = m_InputCount[counter];
                count = (int)Math.Ceiling((float)count / 3.0);
                m_Result.Add(spisok.GetTag(("корм для свиней")), 1 * count);
                counter++;
            }
        }
    };
    public class CKormFactory : CFactory
    {
        public CKormFactory()
            : base(FactoryType.ftKorm)
        {
            m_TagInputArray.Add(spisok.GetTag(("корм для коров")));
            m_TagInputArray.Add(spisok.GetTag(("корм для куриц")));
            m_TagInputArray.Add(spisok.GetTag(("корм для овец")));
            m_TagInputArray.Add(spisok.GetTag(("корм для пчел")));
            m_TagInputArray.Add(spisok.GetTag(("корм для свиней")));
            m_Priority = PriorityTag.three;
            Init();
        }
        public override String GetFactName()
        {
            return ("Корма для животных");
        }
        public override void Calc()
        {
            int counter = 0;
            int count = 0;
            {
                //корм для коров
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 2 * count);
                m_Result.Add(spisok.GetTag(("кукуруза")), 1 * count);
                counter++;
            }
            {
                //корм для куриц
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 2 * count);
                m_Result.Add(spisok.GetTag(("морковь")), 1 * count);
                counter++;
            }
            {
                //корм для овец
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("кукуруза")), 2 * count);
                m_Result.Add(spisok.GetTag(("морковь")), 2 * count);
                counter++;
            }
            {
                //корм для пчел
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 3 * count);
                m_Result.Add(spisok.GetTag(("сахарный тросник")), 1 * count);
                counter++;
            }
            {
                //корм для свиней
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 2 * count);
                m_Result.Add(spisok.GetTag(("кукуруза")), 1 * count);
                m_Result.Add(spisok.GetTag(("морковь")), 1 * count);
                counter++;
            }
        }
    };

    public class CPlasticFactory : CFactory
    {
        public CPlasticFactory()
            : base(FactoryType.ftPlastic)
        {
            m_TagInputArray.Add(spisok.GetTag(("пластиковая бутылка")));
            m_TagInputArray.Add(spisok.GetTag(("игрушка")));
            m_TagInputArray.Add(spisok.GetTag(("мяч")));
            m_TagInputArray.Add(spisok.GetTag(("надувная лодка")));
            m_Priority = PriorityTag.three;
            Init();
        }
        public override String GetFactName()
        {
            return ("Фабрика пластмас");
        }
        public override void Calc()
        {
            int counter = 0;
            int count = 0;
            {
                //пластиковая бутылка
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пластик")), 1 * count);
                counter++;
            }
            {
                //игрушка
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пластик")), 1 * count);
                m_Result.Add(spisok.GetTag(("резина")), 1 * count);
                counter++;
            }
            {
                //мяч
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пластик")), 1 * count);
                m_Result.Add(spisok.GetTag(("клей")), 1 * count);
                counter++;
            }
            {
                //надувная лодка
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("резина")), 2 * count);
                m_Result.Add(spisok.GetTag(("клей")), 1 * count);
                counter++;
            }

        }
    };

    public class CJamFactory : CFactory
    {
        public CJamFactory()
            : base(FactoryType.ftJam)
        {
            m_TagInputArray.Add(spisok.GetTag(("клубничное варенье")));
            m_TagInputArray.Add(spisok.GetTag(("персиковый конфитюр")));
            m_TagInputArray.Add(spisok.GetTag(("арбузный джем")));
            m_TagInputArray.Add(spisok.GetTag(("сливовое повидло")));
            m_TagInputArray.Add(spisok.GetTag(("виноградное желе")));
            m_Priority = PriorityTag.three;
            Init();
        }
        public override String GetFactName()
        {
            return ("Фабрика джема");
        }
        public override void Calc()
        {
            int counter = 0;
            int count = 0;
            {
                //клубничное варенье
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("клубника")), 3 * count);
                counter++;
            }
            {
                //персиковый конфитюр
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("персик")), 3 * count);
                counter++;
            }
            {
                //арбузный джем
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("арбуз")), 3 * count);
                counter++;
            }
            {
                //сливовое повидло
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("слива")), 3 * count);
                counter++;
            }
            {
                //виноградное желе
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("виноград")), 3 * count);
                counter++;
            }

        }
    };


    public class CCandyFactory : CFactory
    {
        public CCandyFactory()
            : base(FactoryType.ftCandy)
        {
            m_TagInputArray.Add(spisok.GetTag(("драже")));
            m_TagInputArray.Add(spisok.GetTag(("ириски")));
            m_TagInputArray.Add(spisok.GetTag(("карамельная палочка")));
            m_TagInputArray.Add(spisok.GetTag(("шоколад")));
            m_TagInputArray.Add(spisok.GetTag(("леденец")));
            m_Priority = PriorityTag.three;
            Init();
        }
        public override String GetFactName()
        {
            return ("Конфетная фабрика");
        }
        public override void Calc()
        {
            int counter = 0;
            int count = 0;
            {
                //драже
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("клубника")), 3 * count);
                m_Result.Add(spisok.GetTag(("сироп")), 1 * count);
                counter++;
            }
            {
                //ириски
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("карамель")), 1 * count);
                m_Result.Add(spisok.GetTag(("масло")), 1 * count);
                counter++;
            }
            {
                //карамельная палочка
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("карамель")), 1 * count);
                m_Result.Add(spisok.GetTag(("сливки")), 1 * count);
                m_Result.Add(spisok.GetTag(("бумага")), 1 * count);
                counter++;
            }
            {
                //шоколад
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("какао")), 3 * count);
                m_Result.Add(spisok.GetTag(("сливки")), 1 * count);
                m_Result.Add(spisok.GetTag(("сахар")), 1 * count);
                counter++;
            }
            {
                //леденец
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("клубника")), 2 * count);
                m_Result.Add(spisok.GetTag(("сироп")), 1 * count);
                m_Result.Add(spisok.GetTag(("сосна")), 2 * count);
                counter++;
            }
        }
    };

    public class CMexicanRestaurant : CFactory
    {
        public CMexicanRestaurant()
            : base(FactoryType.ftMexican)
        {
            m_TagInputArray.Add(spisok.GetTag(("соус чили")));
            m_TagInputArray.Add(spisok.GetTag(("буррито")));
            m_TagInputArray.Add(spisok.GetTag(("начос")));
            m_Priority = PriorityTag.three;
            Init();
        }
        public override String GetFactName()
        {
            return ("Мексиканский ресторан");
        }
        public override void Calc()
        {
            int counter = 0;
            int count = 0;
            {
                //драже
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("томат")), 3 * count);
                m_Result.Add(spisok.GetTag(("перец")), 3 * count);
                m_Result.Add(spisok.GetTag(("сахар")), 1 * count);
                counter++;
            }
            {
                //ириски
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("пшеница")), 2 * count);
                m_Result.Add(spisok.GetTag(("бекон")), 2 * count);
                m_Result.Add(spisok.GetTag(("томат")), 1 * count);
                counter++;
            }

            {
                //ириски
                count = m_InputCount[counter];
                m_Result.Add(spisok.GetTag(("кукурузные чипсы")), 1 * count);
                m_Result.Add(spisok.GetTag(("сыр")), 1 * count);
                m_Result.Add(spisok.GetTag(("перец")), 2 * count);
                counter++;
            }
        }
    };
}
