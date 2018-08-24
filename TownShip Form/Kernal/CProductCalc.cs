using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownShip_Form.Kernal
{
    public class CProductCalc
    {
        private CSmartArray m_array = new CSmartArray();
        private CSmartArray m_alreadyHave = new CSmartArray();
        private readonly List<CFactory> m_factArray = new List<CFactory>();
	    public CProductCalc()
        {
            m_factArray.Add(new CMilkFactory());
            m_factArray.Add(new CBakery());
            m_factArray.Add(new CWeavingFactory());
            m_factArray.Add(new CSugarFactory());
            m_factArray.Add(new CRubberFactory());
            m_factArray.Add(new CPlasticFactory());
            m_factArray.Add(new CPaperFactory());
            m_factArray.Add(new CSewingFactory());
            m_factArray.Add(new CSnackFactory());
            m_factArray.Add(new CFastFoodFactory());
            m_factArray.Add(new CIceCreamFactory());
            m_factArray.Add(new CСonfectioneryFactory());
            m_factArray.Add(new CAnimalFactory());
            m_factArray.Add(new CKormFactory());
            m_factArray.Add(new CJamFactory());
            m_factArray.Add(new CCandyFactory());
            m_factArray.Add(new CMexicanRestaurant());

            //UtilsProduct.SetHaveProduct(m_AlreadyHave);
        }

        ~CProductCalc()
        {
            //int count = m_FactArray.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    delete m_FactArray[i];
            //}
        }
        public void Add(string name, int count)
        {
            m_array.Add(name, count);
        }
        public List<CFactory> GetFactoryList()
        {
            return m_factArray;
        }
        public void SetArray(CSmartArray array)
        {
            m_array = array;
        }
        public void SetAlreadyHave(CSmartArray array)
        {
            m_alreadyHave = array;
        }

        public CSmartArray GetAlreadyHave()
        {
            return m_alreadyHave.Copy();
        }

        public void Calc(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Calc();
            }
        }

        public CSmartArray GetArray()
        {
            return m_array.Copy();
        }
        public void Calc()
        {
            int count = m_factArray.Count;
            m_array.ExcludeWhatHave(m_alreadyHave);
            for (int i = 0; i < count; i++)
            {
                CFactory pFact = m_factArray[i];
                pFact.SetInput(m_array);
                pFact.Calc();
                pFact.UpdateResult(m_array);
                m_array.ExcludeWhatHave(m_alreadyHave);
            }
        }

        public string Print()
        {
            //OutputDebugString(_T("Информация по фабрикам:\n\r"));
            String result = "Информация по фабрикам:\n\r";
            int count = m_factArray.Count;
            for (int pr = 0; pr < (int)PriorityTag.three + 1; pr++)
            {
                for (int i = 0; i < count; i++)
                {
                    if (m_factArray[i].GetPriorety() == (PriorityTag)pr)
                        result += m_factArray[i].Print();
                }

            }
            result += Environment.NewLine + "Информация по ингридиентам:" + Environment.NewLine;
            result += m_array.Print();
            
            return result;
        }

        public void ExcludeWhatHave(CProductCalc pCalc)
        {
            for (int i = 0; i < m_factArray.Count; i++)
            {
                List<CFactory> pList = pCalc.GetFactoryList();
                m_factArray[i].ExcludeWhatHave(pList[i]);
            }
            m_array.ExcludeWhatHave(pCalc.m_array);
            //testString
        }
    };
}
