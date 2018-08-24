using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace TownShip_Form.Kernal
{
    public class CZakaz
    {
        private string m_name;
        private readonly CSmartArray m_array;
        private bool m_isResolve;
        private bool m_isNew;

        public string GetName()
        {
            return m_name;
        }
        public CZakaz()
        {
            m_isNew = false;
            m_isResolve = false;
            m_array = new CSmartArray();
        }

        public CSmartArray GetSmartArray()
        {
            return m_array;
        }


        public void LoadValueFromSheet(Worksheet pSheet, int column)
        {
            m_name = pSheet.Cells[1, column].ToString();
            m_array.RemoveAll();
            int startRow = 2;
            {
                string value = pSheet.Cells[2, column].Text;
                if (value == "resolve")
                    m_isResolve = true;
                else if (value == "new")
                    m_isNew = true;

                if (m_isNew || m_isResolve)
                    startRow++;

            }
            for (int row = startRow;; row++)
            {
                string name = pSheet.Cells[row, column].Text;
                if (name.Length == 0)
                    break;
                string value = pSheet.Cells[row, column + 1].Text;
                m_array.Add(name, Convert.ToInt32(value));
            }
        }

        public bool IsResolve()
        {
            return m_isResolve;
        }

        public bool IsNew()
        {
            return m_isNew;
        }
    }
}
