using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace TownShip_Form.Kernal
{
    public class Utils
    {
        public static void Testing()
        {
            //Worksheet pSheet1;
            //Worksheet pSheet2;
        }

        public static Range GetRange(Worksheet pSheet, int startRow, int startColumn, int endRow, int endColumn)
        {
            Range pRange1 = pSheet.Cells[startRow, startColumn];
            Range pRange2 = pSheet.Cells[endRow, endColumn];
            return pSheet.Range[pRange1, pRange2];
        }
        public static void UpdateCell(Worksheet pSheet, int Row, int Column, string value, int weight = 5)
        {
            Range pRange = pSheet.Cells[Row, Column];
            pRange.Value = value;
            pRange.ColumnWidth = weight;
        }

        public static void WriteHeader(Worksheet pSheet, int row, int column)
        {
            UpdateCell(pSheet, row, column++, "Название", 20);
            UpdateCell(pSheet, row, column++, "Надо");
            UpdateCell(pSheet, row, column++, "Процесс");
            UpdateCell(pSheet, row, column++, "Готов");
            UpdateCell(pSheet, row, column++, "Осталось");
        }

        public static void WriteTagString(Worksheet pSheet, int row, int column, string name, int value)
        {
            UpdateCell(pSheet, row, column, name, 20);
            UpdateCell(pSheet, row, column + 1, value.ToString());
            UpdateCell(pSheet, row, column + 2, "");
            UpdateCell(pSheet, row, column + 3, "");
            UpdateCell(pSheet, row, column + 4, "");
            pSheet.Cells[row, column + 4].FormulaR1C1 = "=RC[-3]-RC[-2]-RC[-1]";
            //string formula = "=IF(RC[-1]=0,\"можно удалить\",\"\")";
            string formula = "=IF(AND(RC[-1]=0,RC[-3]=0),\"можно удалить\",\"\")";
            //string formula = "=SQRT(R1C1)";
            pSheet.Cells[row, column + 5].FormulaR1C1 = formula;

            Utils.DrawConditions(pSheet.Cells[row, column + 1], Color.CornflowerBlue);
            Utils.DrawConditions(pSheet.Cells[row, column + 2], Color.Khaki);
            Utils.DrawConditions(pSheet.Cells[row, column + 4], Color.DarkSeaGreen);
        }

        public static void DrawConditions(Range pRange, Color color)
        {
            FormatConditions pConditions = pRange.FormatConditions;
            Databar pCondition = pConditions.AddDatabar();
            pCondition.MaxPoint.Modify(XlConditionValueTypes.xlConditionValueNumber, 0.5);
            pCondition.MinPoint.Modify(XlConditionValueTypes.xlConditionValueNumber, 0);
            pCondition.BarColor.Color = color;
            pCondition.BarFillType = XlDataBarFillType.xlDataBarFillSolid;
        }
    }

    class CAllSourceWrite
    {
        CAllProductSpisok m_spisok = new CAllProductSpisok();

        public CAllSourceWrite(Worksheet pSheet, CSmartArray array)
        {
            Dictionary<ProductTag, int> map = array.GetMap();
            Dictionary<ProductTag, int>.KeyCollection.Enumerator e = map.Keys.GetEnumerator();
            const int startRow = 3;
            const int startColumn = 10;
            Utils.WriteHeader(pSheet, startRow, startColumn);

            int count = map.Keys.Count;
            for (int i = 0; i < count; i++)
            {
                e.MoveNext();
                ProductTag tag = e.Current;
                string name = m_spisok.GetName(tag);
                int value = map[tag];
                Utils.WriteTagString(pSheet, startRow + i, startColumn, name, value);
            }
        }
    }

    class CAllFactoryOneTable
    {
        CAllProductSpisok m_spisok = new CAllProductSpisok();
        public CAllFactoryOneTable(List<CFactory> factoryList, Worksheet pSheet)
        {
            int currentRow = 1;
            const int currentColumn = 1;
            for (int i = 0; i < factoryList.Count; i++)
            {
                currentRow += WriteFactory(pSheet, factoryList[i], currentRow, currentColumn);
            }
        }

        private int WriteFactory(Worksheet pSheet, CFactory pFactory, int row, int column)
        {
            List<ProductTag> input = pFactory.GetInputArray();
            int realCount = 0;
            string factoryName = pFactory.GetFactName();
            pSheet.Cells[row, column] = factoryName;
            Range pTitleRange = Utils.GetRange(pSheet, row, column, row, column + 4);
            pTitleRange.Merge();
            pTitleRange.Interior.Color = Color.Bisque;
            realCount++;
            for (int i = 0; i < input.Count; i++)
            {
                ProductTag tag = input[i];
                int value = pFactory.GetTagCount(tag);
                int currRow = row + realCount;
                string tagName = m_spisok.GetName(tag);
                
                if (value > 0)
                {
                    realCount++;
                    pSheet.Cells[currRow, column].ColumnWidth = 20;
                    Utils.WriteTagString(pSheet, currRow, column, tagName, value);
                }
            }

            return realCount;
        }
    }
    class CExcelInputHelper
    {
        Worksheet m_pSheet;
        Application m_pApp;
        CAllProductSpisok m_spisok = new CAllProductSpisok();
        public CExcelInputHelper(CSmartArray array)
        {
            //Application pApp = new Application();
            m_pApp = new Application();
            Workbook pBook = m_pApp.Workbooks.Add();
            m_pSheet = pBook.Sheets[1];

            m_pSheet.Cells[1, 1] = "наименование";
            m_pSheet.Cells[1, 2] = "количество";

            //int count = array.GetCount();
            int count = m_spisok.GetCount();
            for (int i = 0; i < count; i++) 
            {
                ProductTag tag = (ProductTag)(i + 1);
                m_pSheet.Cells[i + 2, 1] = m_spisok.GetName(tag);
                m_pSheet.Cells[i + 2, 2] = array.GetTagCount(tag);
            }
            Range pRange = m_pSheet.Range[m_pSheet.Cells[1, 1], m_pSheet.Cells[count + 1, 2]];
            //Range pColumn = m_pSheet.Columns["A:FF"];
            //pColumn.AutoFit();
            for (int i = (int)XlBordersIndex.xlEdgeLeft; i <= (int)XlBordersIndex.xlInsideHorizontal; i++) 
            {
                pRange.Borders[(XlBordersIndex)i].LineStyle = XlLineStyle.xlContinuous;
            }
            m_pApp.Visible = true;

        }
        public CSmartArray GetArray()
        {
            CSmartArray array = new CSmartArray();
            int count = m_spisok.GetCount();
            for (int i = 1; i < count;i++)
            {
                string name = m_pSheet.Cells[i + 1, 1].Text;
                string value = m_pSheet.Cells[i + 1, 2].Text;
                array.Add(name, Convert.ToInt32(value));
            }
            return array;
        }
        ~CExcelInputHelper()
        {
            m_pApp.Workbooks.Close();
        }
    }

    class CExportFactoryResultHelper
    {
        CAllProductSpisok m_spisok = new CAllProductSpisok();
        int m_startRow;
        int m_startColumn;
        CFactory m_pFactory;
        Worksheet m_pSheet;
        
        public CExportFactoryResultHelper(Worksheet pSheet, CFactory pFact, int Row, int Column)
        {
            m_pFactory = pFact;
            m_pSheet = pSheet;
            m_startRow = Row;
            m_startColumn = Column;
            UploadFactory();
            DefaultStyle();
            int delta = 1;
            Utils.DrawConditions(Utils.GetRange(pSheet, m_startRow + 1, m_startColumn + delta, m_startRow + 7, m_startColumn + delta), Color.CornflowerBlue);
            delta++;
            Utils.DrawConditions(Utils.GetRange(pSheet, m_startRow + 1, m_startColumn + delta, m_startRow + 7, m_startColumn + delta), Color.Khaki);
            delta++;
            //DrawConditions(GetRange(m_startRow + 1, m_startColumn + delta, m_startRow + 5, m_startColumn + delta), Color.DarkSeaGreen);
            delta++;
            Utils.DrawConditions(Utils.GetRange(pSheet, m_startRow + 1, m_startColumn + delta, m_startRow + 7, m_startColumn + delta), Color.DarkSeaGreen);
            WriteFormula();
        }
        

        private void WriteFormula()
        {
            for (int i = 0; i < 5; i++)
            {
                Range pRange = m_pSheet.Cells[m_startRow + 2 + i, m_startColumn + 4];
                pRange.FormulaR1C1 = "=RC[-3]-RC[-2]-RC[-1]";
            }
        }
        void UploadFactory()
        {
            List<ProductTag> input = m_pFactory.GetInputArray();

            for (int i = 0; i < input.Count; i++)
            {
                ProductTag tag = input[i];
                int value = m_pFactory.GetTagCount(tag);
                m_pSheet.Cells[m_startRow + 2 + i, m_startColumn] = m_spisok.GetName(tag);
                m_pSheet.Cells[m_startRow + 2 + i, m_startColumn + 1] = value.ToString();
            }
        }

        
        private void DefaultStyle()
        {
            Range FullRange = m_pSheet.Range[m_pSheet.Cells[m_startRow, m_startColumn], m_pSheet.Cells[m_startRow + 7, m_startColumn + 4]];
            for (int i = (int)XlBordersIndex.xlEdgeLeft; i <= (int)XlBordersIndex.xlInsideHorizontal; i++)
            {
                FullRange.Borders[(XlBordersIndex)i].LineStyle = XlLineStyle.xlContinuous;
            }
            FullRange = m_pSheet.Range[m_pSheet.Cells[m_startRow, m_startColumn], m_pSheet.Cells[m_startRow, m_startColumn + 4]];
            FullRange.Merge();
            FullRange.Value = m_pFactory.GetFactName();
            int delta = 0;
            m_pSheet.Cells[m_startRow, m_startColumn].ColumnWidth = 20;
            Utils.UpdateCell(m_pSheet, m_startRow + 1, m_startColumn + 1 + delta++, "Надо");
            Utils.UpdateCell(m_pSheet, m_startRow + 1, m_startColumn + 1 + delta++, "Процесс");
            Utils.UpdateCell(m_pSheet, m_startRow + 1, m_startColumn + 1 + delta++, "Готов");
            Utils.UpdateCell(m_pSheet, m_startRow + 1, m_startColumn + 1 + delta++, "Осталось");
        }

        

        void WriteConditionStyle()
        {

        }

        public int GetWidht()
        {
            return 6;
        }
        public int GetHeight()
        {
            return 8;
        }
    }

    class CExportAllFactoryHelper
    {
        CAllProductSpisok m_spisok = new CAllProductSpisok();
        List<CFactory> m_pFactoryList;
        private CSmartArray m_array;
        public CExportAllFactoryHelper(List<CFactory> pList)
        {
            m_pFactoryList = pList;
            Application pApp = new Application();
            Workbook pBook = pApp.Workbooks.Add();
            Worksheet pSheet = pBook.Sheets[1];
            pApp.Visible = true;
            
        }

        public CExportAllFactoryHelper(Worksheet pSheet, List<CFactory> pList, CSmartArray array)
        {
            m_pFactoryList = pList;
            m_array = array;
            ExportForExcel(pSheet);
        }
        public void ExportForExcel(Worksheet pSheet)
        {
            int factByLine = 4;
            int currRow = 2;
            int currColumn = 2;
            int maxColumn = 0;
            for (int i = 0; i < m_pFactoryList.Count; i++)
            {
                CFactory pFact = m_pFactoryList[i];
                CExportFactoryResultHelper helper = new CExportFactoryResultHelper(pSheet, pFact, currRow, currColumn);
                currColumn += (helper.GetWidht() + 0);
                maxColumn = Math.Max(maxColumn, currColumn);
                if ((i + 1) % factByLine == 0)
                {
                    currRow += (helper.GetHeight() + 1);
                    currColumn = 2;
                }
            }

            Dictionary<ProductTag, int> map = m_array.GetMap();
            Dictionary<ProductTag, int>.KeyCollection.Enumerator e = map.Keys.GetEnumerator();
            int startRow = 3;
            //maxColumn += 3;
            Utils.WriteHeader(pSheet, startRow, maxColumn);
            
            startRow++;
            int count = map.Keys.Count;
            for (int i = 0; i < count; i++)
            {
                e.MoveNext();
                ProductTag tag = e.Current;
                string name = m_spisok.GetName(tag);
                int value = map[tag];
                pSheet.Cells[startRow + i + 1, maxColumn] = name;
                pSheet.Cells[startRow + i + 1, maxColumn + 1] = value;
            }

            Range pRange = pSheet.Range[pSheet.Cells[startRow, maxColumn], pSheet.Cells[startRow + count, maxColumn + 3]];
            for (int i = (int)XlBordersIndex.xlEdgeLeft; i <= (int)XlBordersIndex.xlInsideHorizontal; i++)
            {
                pRange.Borders[(XlBordersIndex)i].LineStyle = XlLineStyle.xlContinuous;
            }
        }

        
    }

    class CSimpleFactoryExport
    {
        private readonly CAllProductSpisok m_spisok = new CAllProductSpisok();
        public CSimpleFactoryExport(Worksheet pSheet, List<CFactory> pList, CSmartArray array)
        {
            int row = 1;
            for (int i = 0; i < pList.Count; i++)
            {
                CFactory pFactory = pList[i];
                if (!pFactory.IsEmpty())
                {
                    Range pRange = pSheet.Range[pSheet.Cells[row, 1], pSheet.Cells[row, 2]];
                    pRange.Merge();
                    pRange.Value = pList[i].GetFactName();
                    row++;
                }
                ExportFactory(pSheet, pFactory, ref row);
            }

            Dictionary<ProductTag, int> map = array.GetMap();
            Dictionary<ProductTag, int>.KeyCollection.Enumerator e = map.Keys.GetEnumerator();
            int count = map.Keys.Count;
            pSheet.Range[pSheet.Cells[row, 1], pSheet.Cells[row, 2]].Merge();
            pSheet.Range[pSheet.Cells[row, 1], pSheet.Cells[row, 2]] = "Вырастить на поле";
            row++;
            for (int i = 0; i < count; i++)
            {
                e.MoveNext();
                ProductTag tag = e.Current;
                string name = m_spisok.GetName(tag);
                int value = map[tag];
                pSheet.Cells[row, 1] = name;
                pSheet.Cells[row, 2] = value;
                row++;
            }

            Range FullRange = pSheet.Range[pSheet.Cells[1, 1], pSheet.Cells[row - 1, 2]];
            for (int i = (int)XlBordersIndex.xlEdgeLeft; i <= (int)XlBordersIndex.xlInsideHorizontal; i++)
            {
                FullRange.Borders[(XlBordersIndex)i].LineStyle = XlLineStyle.xlContinuous;
            }
            pSheet.Columns[1].AutoFit();
        }

        private void ExportFactory(Worksheet pSheet, CFactory pFactory, ref int row)
        {
            List<ProductTag> input = pFactory.GetInputArray();
            for (int i = 0; i < input.Count; i++)
            {
                ProductTag tag = input[i];
                int value = pFactory.GetTagCount(tag);
                if (value > 0)
                {
                    pSheet.Cells[row, 1] = m_spisok.GetName(tag);
                    pSheet.Cells[row, 2] = value.ToString();
                    row++;
                }
            }
            
        }
    }
}
