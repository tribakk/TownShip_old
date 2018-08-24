using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace TownShip_Form.Kernal
{
    class CZakazCalc
    {
        private static readonly Application m_pApplication = new Application();
        private readonly Workbook m_pBook;
        private readonly List<CZakaz> m_zakazList = new List<CZakaz>();
        private readonly CSmartArray m_alreadyHave = new CSmartArray();
        private readonly CAllProductSpisok m_Spisok = new CAllProductSpisok();

        public CZakazCalc()
        {
            m_pBook = GetWorkbook("test.xlsx");
        }
        ~CZakazCalc()
        {
            try
            {
                m_pBook.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }
        private static Workbook GetWorkbook(string name = "")
        {
            Workbook pBook = null;
            m_pApplication.Visible = true;
            if (name == "")
            {
                pBook = m_pApplication.Workbooks.Add();
            }
            else
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                path = System.IO.Directory.GetParent(path).Parent.FullName;
                pBook = m_pApplication.Workbooks.Open(path + "\\excel\\" + name);
            }

            return pBook;
        }
        private Worksheet GetWorksheet(string sheetName, bool bThrow = true)
        {
            Worksheet pSheet = null;
            try
            {
                pSheet = (Worksheet)m_pBook.Worksheets.Item[sheetName];
            }
            catch
            {
                if (bThrow)
                    throw new ArgumentNullException("нет листа " + sheetName);
            }

            return pSheet;
        }
        private string GetUniqueSheetName(string name)
        {
            string uniqueName;
            for (int i = 1;; i++)
            {
                uniqueName = name + i.ToString();
                if (GetWorksheet(uniqueName, false) == null)
                    break;
            }

            return uniqueName;
        }
        public void UpdateZakazList()
        {
            Worksheet pSheet = GetWorksheet("Zakaz");
            m_zakazList.Clear();
            for (int column = 1; ; column += 2)
            {
                if (pSheet.Cells[1, column].Text == "")
                    break;

                CZakaz pZakaz = new CZakaz();
                pZakaz.LoadValueFromSheet(pSheet, column);
                m_zakazList.Add(pZakaz);
            }
        }

        public void UpdateAlreadyHaveList()
        {
            Worksheet pSheet = GetWorksheet("AlreadyHave");
            m_alreadyHave.RemoveAll();
            for (int column = 1;; column += 2)
            {
                if (pSheet.Cells[1, column].Text == "")
                    break;
                for (int row = 2;; row++)
                {
                    string name = pSheet.Cells[row, column].Text;
                    if (name == "")
                        break;
                    string value = pSheet.Cells[row, column + 1].Text;
                    m_alreadyHave.Add(name, Convert.ToInt32(value));
                }
            }
        }
        public void CreateEmptyAlreadyHaveSheet()
        {
            Worksheet pSheet = m_pBook.Worksheets.Add();
            pSheet.Name = GetUniqueSheetName("EmptyAlreadyHave");
            pSheet.Cells[1, 1] = "Наименование";
            pSheet.Cells[1, 2] = "Значение";
            int count = m_Spisok.GetCount();
            for (int i = 0; i < m_Spisok.GetCount(); i++)
            {
                ProductTag tag = (ProductTag) (i + 1);
                string tagName = m_Spisok.GetName(tag);
                pSheet.Cells[i + 2, 1] = tagName;
                pSheet.Cells[i + 2, 2] = 0;
            }
        }

        private void ExportToExcel(Worksheet pSheet, List<CFactory> pFactory, CSmartArray array)
        {
            CExportAllFactoryHelper excelHelper = new CExportAllFactoryHelper(pSheet, pFactory, array);
        }
        public void Calc()
        {
            List<CZakaz> simpleZakazes = new List<CZakaz>();
            List<CZakaz> newZakazes = new List<CZakaz>();
            List<CZakaz> resolveZakazes = new List<CZakaz>();

            UpdateZakazsList(resolveZakazes, newZakazes, simpleZakazes);

            CSmartArray calcArray = new CSmartArray();
            LoadZakazListToSmartArray(calcArray, simpleZakazes);

            CProductCalc productCalc = new CProductCalc();
            productCalc.SetArray(calcArray);
            productCalc.SetAlreadyHave(m_alreadyHave.Copy());
            productCalc.Calc(5);

            Worksheet pTotalSheet = m_pBook.Worksheets.Add();
            pTotalSheet.Name = GetUniqueSheetName("MainTotal");
            ExportToExcel(pTotalSheet, productCalc.GetFactoryList(), productCalc.GetArray());
            {
                Worksheet pOneTableSheet = m_pBook.Worksheets.Add();
                pOneTableSheet.Name = GetUniqueSheetName("OneTableTotal");
                Kernal.CAllFactoryOneTable allFactoryOneTable = new Kernal.CAllFactoryOneTable(productCalc.GetFactoryList(), pOneTableSheet);
                Kernal.CAllSourceWrite AllSourceWrite = new Kernal.CAllSourceWrite(pOneTableSheet, productCalc.GetArray());

            }

            if (newZakazes.Count > 0)
            {
                CProductCalc productCalc2 = GetProductCalcSimpleAndSecond(simpleZakazes, newZakazes);
                {
                    Worksheet pTotalSheet2 = m_pBook.Worksheets.Add();
                    pTotalSheet2.Name = GetUniqueSheetName("OtherTotal");
                    ExportToExcel(pTotalSheet2, productCalc2.GetFactoryList(), productCalc2.GetArray());
                }
                productCalc2.ExcludeWhatHave(productCalc);
                Worksheet pSimpleSheet = m_pBook.Worksheets.Add();
                pSimpleSheet.Name = GetUniqueSheetName("SimpleTotal");
                new CSimpleFactoryExport(pSimpleSheet, productCalc2.GetFactoryList(), productCalc2.GetArray());

            }
            if (resolveZakazes.Count > 0)
            {
                CProductCalc productCalc2 = GetProductCalcSimpleAndSecond(simpleZakazes, resolveZakazes);
                CSmartArray newAlreadyHave = productCalc.GetAlreadyHave();

                //вывести на другой лист значение newAlreadyHave
            }
        }
        private CProductCalc GetProductCalcSimpleAndSecond(List<CZakaz> simpleZakazes, List<CZakaz> newZakazes)
        {
            CSmartArray calcArray2 = new CSmartArray();
            LoadZakazListToSmartArray(calcArray2, simpleZakazes);
            LoadZakazListToSmartArray(calcArray2, newZakazes);

            CProductCalc productCalc2 = new CProductCalc();
            productCalc2.SetArray(calcArray2);
            productCalc2.SetAlreadyHave(m_alreadyHave.Copy());
            productCalc2.Calc(5);
            return productCalc2;
        }
        private void UpdateZakazsList(List<CZakaz> resolveZakazes, List<CZakaz> newZakazes, List<CZakaz> simpleZakazes)
        {
            for (int i = 0; i < m_zakazList.Count; i++)
            {
                CZakaz pZakaz = m_zakazList[i]/* ?? throw new ArgumentNullException("кривой заказ")*/;
                if (pZakaz.IsResolve())
                    resolveZakazes.Add((pZakaz));
                else if (pZakaz.IsNew())
                    newZakazes.Add(pZakaz);
                else
                    simpleZakazes.Add(pZakaz);
            }

            if ((resolveZakazes.Count * newZakazes.Count) != 0)
                throw new ArgumentNullException("есть Resolve и New заказы одновременно");
        }

        private void LoadZakazListToSmartArray(CSmartArray array, List<CZakaz> pList)
        {
            for (int i = 0; i < pList.Count; i++)
            {
                array.Merge(pList[i].GetSmartArray());
            }
        }
        public void WriteInfoAboutSourceToExcel()
        {
            CProductCalc productCalc = new CProductCalc();
            List<CFactory> factList = productCalc.GetFactoryList();
            Worksheet pSourceSheet = m_pBook.Worksheets.Add();
            pSourceSheet.Name = GetUniqueSheetName("FullSource");
            int currentRow = 1 + 1;
            for (int factNo = 0; factNo < factList.Count; factNo++)
            {
                Kernal.CFactory factory = factList[factNo];
                List<Kernal.ProductTag> tagInput = factory.GetInputArray();
                for (int i = 0; i < tagInput.Count; i++)
                {
                    Kernal.CSmartArray array = new Kernal.CSmartArray();
                    array.Add(tagInput[i], 1);
                    factory.SetInput(array);
                    factory.Calc();
                    Kernal.CSmartArray result = new Kernal.CSmartArray();
                    factory.UpdateResult(result);
                    Dictionary<ProductTag, int> map = result.GetMap();
                    int count = map.Count;
                    var en = map.Keys.GetEnumerator();
                    en.MoveNext();
                    for (int j = 0; j < count; j++)
                    {
                        ProductTag pTag = en.Current;
                        en.MoveNext();
                        int TagCount = result.GetTagCount(pTag);
                        Kernal.Utils.UpdateCell(pSourceSheet, currentRow, 1, m_Spisok.GetName(tagInput[i]), 20);
                        Kernal.Utils.UpdateCell(pSourceSheet, currentRow, 2, m_Spisok.GetName(pTag), 10);
                        Kernal.Utils.UpdateCell(pSourceSheet, currentRow, 3, TagCount.ToString());
                        currentRow++;
                    }
                }
            }
        }
        private struct ProductSourceInfo
        {
            public ProductTag m_ProductTag;
            public ProductTag m_SourceTag;
            public int m_Value;
            public ProductSourceInfo(ProductTag product, ProductTag source, int value)
            {
                m_ProductTag = product;
                m_SourceTag = source;
                m_Value = value;
            }
        }
        private struct ProductInfo
        {
            public ProductTag m_SourceTag;
            public int m_Value;
            public ProductInfo(ProductTag source, int value)
            {
                m_SourceTag = source;
                m_Value = value;
            }
        }

        private CFactory CreateFactory(Kernal.FactoryType type)
        {
            CFactory pFact = null;
            switch (type)
            {
                case FactoryType.ftAnimal:
                    pFact = new CAnimalFactory();
                    break;
                case FactoryType.ftBakery:
                    pFact = new CBakery();
                    break;
                case FactoryType.ftConfectionery:
                    pFact = new CСonfectioneryFactory();
                    break;
                case FactoryType.ftFastFood:
                    pFact = new CFastFoodFactory();
                    break;
                case FactoryType.ftIceCream:
                    pFact = new CIceCreamFactory();
                    break;
                case FactoryType.ftJam:
                    pFact = new CJamFactory();
                    break;
                case FactoryType.ftKorm:
                    pFact = new CKormFactory();
                    break;
                case FactoryType.ftMilk:
                    pFact = new CMilkFactory();
                    break;
                case FactoryType.ftPaper:
                    pFact = new CPaperFactory();
                    break;
                case FactoryType.ftPlastic:
                    pFact = new CPlasticFactory();
                    break;
                case FactoryType.ftSewing:
                    pFact = new CSewingFactory();
                    break;
                case FactoryType.ftSnack:
                    pFact = new CSnackFactory();
                    break;
                case FactoryType.ftSugar:
                    pFact = new CSugarFactory();
                    break;
                case FactoryType.ftWeaving:
                    pFact = new CWeavingFactory();
                    break;
                case FactoryType.ftRubber:
                    pFact = new CRubberFactory();
                    break;
                case FactoryType.ftCandy:
                    pFact = new CCandyFactory();
                    break;
                case FactoryType.ftMexican:
                    pFact = new CMexicanRestaurant();
                    break;

            }
            return pFact;
        }
        public void WriteInfoAboutFactroryToExcel()
        {
            List<ProductSourceInfo> PSInfo = new List<ProductSourceInfo>();
            CProductCalc productCalc = new CProductCalc();
            List<CFactory> factList = productCalc.GetFactoryList();
            //Worksheet pSourceSheet = m_pBook.Worksheets.Add();
            //pSourceSheet.Name = GetUniqueSheetName("FullSource");
            for (int factNo = 0; factNo < factList.Count; factNo++)
            {
                Kernal.CFactory factory = factList[factNo];
                List<Kernal.ProductTag> tagInput = factory.GetInputArray();
                for (int i = 0; i < tagInput.Count; i++)
                {
                    Kernal.CSmartArray array = new Kernal.CSmartArray();
                    array.Add(tagInput[i], 1);
                    factory.SetInput(array);
                    factory.Calc();
                    Kernal.CSmartArray result = new Kernal.CSmartArray();
                    factory.UpdateResult(result);
                    Dictionary<ProductTag, int> map = result.GetMap();
                    int count = map.Count;
                    var en = map.Keys.GetEnumerator();
                    en.MoveNext();
                    for (int j = 0; j < count; j++)
                    {
                        ProductTag pTag = en.Current;
                        en.MoveNext();
                        int TagCount = result.GetTagCount(pTag);
                        PSInfo.Add(new ProductSourceInfo(tagInput[i], pTag, TagCount));
                    }
                }
            }

            Dictionary<ProductTag, List<ProductInfo>> InfoArray =new Dictionary<ProductTag, List<ProductInfo>>();
            for (int i = 0;i< PSInfo.Count; i++)
            {
                ProductSourceInfo info = PSInfo[i];
                if (!InfoArray.ContainsKey(info.m_SourceTag))
                    InfoArray[info.m_SourceTag] = new List<ProductInfo>();
                InfoArray[info.m_SourceTag].Add(new ProductInfo(info.m_ProductTag, info.m_Value));
            }

            int FactCount = Enum.GetNames(typeof(FactoryType)).Length;
            
            for (int FactNo = 0; FactNo < FactCount; FactNo ++)
            {
                CFactory pFact = CreateFactory((FactoryType)FactNo);
                string FactName;
                if (pFact == null)
                {
                    if ((FactoryType)FactNo == FactoryType.ftField)
                        FactName = "поля";
                    else
                        continue;
                }
                else
                    FactName = pFact.GetFactName();

                Worksheet pSourceSheet = m_pBook.Worksheets.Add();
                pSourceSheet.Name = GetUniqueSheetName(FactName);
                int CurrentColumn = 1;
                var e = InfoArray.Keys.GetEnumerator();
                for (int i = 0; i < InfoArray.Count; i++)
                {
                    e.MoveNext();
                    ProductTag tag = e.Current;

                    if (m_Spisok.GetFactoryType(tag) == (FactoryType)FactNo)
                    {
                        List<ProductInfo> list = InfoArray[tag];
                        Kernal.Utils.UpdateCell(pSourceSheet, 1, CurrentColumn, m_Spisok.GetName(tag), 20);
                        Range pTitleRange = Utils.GetRange(pSourceSheet, 1, CurrentColumn, 1, CurrentColumn + 1);
                        pTitleRange.Merge();
                        for (int ProductNo = 0; ProductNo < list.Count; ProductNo++)
                        {
                            ProductInfo info = list[ProductNo];
                            Kernal.Utils.UpdateCell(pSourceSheet, ProductNo + 2, CurrentColumn, m_Spisok.GetName(info.m_SourceTag), 20);
                            Kernal.Utils.UpdateCell(pSourceSheet, ProductNo + 2, CurrentColumn + 1, info.m_Value.ToString(), 4);
                        }
                        Range pRange = Utils.GetRange(pSourceSheet, 1, CurrentColumn, list.Count + 1, CurrentColumn + 1);
                        for (int borderNo = (int)XlBordersIndex.xlEdgeLeft; borderNo <= (int)XlBordersIndex.xlInsideHorizontal; borderNo++)
                        {
                            pRange.Borders[(XlBordersIndex)borderNo].LineStyle = XlLineStyle.xlContinuous;
                        }
                        CurrentColumn += 2;
                    }
                }

            }

        }
    }
}
