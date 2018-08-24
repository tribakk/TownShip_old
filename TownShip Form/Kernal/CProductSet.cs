using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TownShip_Form.Kernal
{
    public class CProductSet
    {
        ComboBox m_Combo;
        NumericUpDown m_Numeric;
        CAllProductSpisok spisok = new CAllProductSpisok();
        public CProductSet(ComboBox combo, NumericUpDown numeric)
        {
            m_Combo = combo;
            m_Numeric = numeric;
        }
        public string GetProductName()
        {
            return m_Combo.Text;
        }
        public ProductTag GetProductTag()
        {
            return spisok.GetTag(GetProductName());
        }
        public int GetValue()
        {
            return (int)m_Numeric.Value;
        }
        public void SetValue(ProductTag tag, int value)
        {
            m_Combo.Text = spisok.GetName(tag);
            m_Numeric.Value = value;
        }
    }
    public class CProductSetCollection
    {
        private List<CProductSet> m_prCollection = new List<CProductSet>();
        public CProductSetCollection()
        {

        }
        public void Add(CProductSet pSet)
        {
            m_prCollection.Add(pSet);
        }
        public void Add(string name, ComboBox combo, NumericUpDown numeric)
        {
            m_prCollection.Add(new CProductSet(combo, numeric));
        }
        public int GetCount()
        {
            return m_prCollection.Count;
        }
        public CProductSet GetItem(int i)
        {
            return m_prCollection[i];
        }

        public CSmartArray CalcArray()
        {
            CSmartArray array = new CSmartArray();
            for (int i = 0; i < m_prCollection.Count; i++)
            {
                array.Add(m_prCollection[i].GetProductTag(), m_prCollection[i].GetValue());
            }
            return array;
        }
        public void FillControls(CSmartArray array)
        {
            List<ProductTag> ptList = new List<ProductTag>();
            List<int> iList = new List<int>();
            if (array != null)
                array.FillArray(ptList, iList);
            int count = ptList.Count;
            for (int i = 0; i < count; i++)
            {
                m_prCollection[i].SetValue(ptList[i], iList[i]);
            }
            for (int i = count; i < m_prCollection.Count; i++) 
            {
                m_prCollection[i].SetValue(Kernal.ProductTag.ptNotFound, 0);
            }
        }
        public void InitByPanel(Panel pPanel)
        {
            int Count = 0;
            for (int i = 0;i < pPanel.Controls.Count; i++)
            {
                ComboBox combo = pPanel.Controls[i] as ComboBox;
                if (combo != null)
                {
                    string tag = combo.Tag as string;
                    if (tag != null)
                    {
                        int value = Convert.ToInt32(tag);
                        if (value > Count)
                            Count = value;
                    }
                }
            }
            //у нас значение начинаются с 1, поэтому count равен номеру последнего элемента
            List<ComboBox> comboList = new List<ComboBox>();
            List<NumericUpDown> numericList = new List<NumericUpDown>();
            for (int i =0;i<Count;i++)
            {
                comboList.Add(null);
                numericList.Add(null);
            }

            for (int i = 0; i < pPanel.Controls.Count; i++) 
            {
                Control contr = pPanel.Controls[i];
                ComboBox combo = contr as ComboBox;
                NumericUpDown numeric = contr as NumericUpDown;
                int number = 0;
                if (combo != null || numeric != null)
                {
                    string tag = contr.Tag as string;
                    if (tag != null)
                    {
                        number = Convert.ToInt32(tag);
                    }
                    number--; //начинаем считать с 1
                    if (combo != null)
                        comboList[number] = combo;
                    if (numeric != null)
                        numericList[number] = numeric;
                }
            }
            for (int i = 0; i < Count; i++)
            {
                Add("", comboList[i], numericList[i]);
            }
        }
    }
    public class CZakazCollection
    {
        Button ZakazButton;
        Button DeleteButton;
        Button SaveButton;
        ListBox ZakazList;

        List<CSmartArray> m_ZakazCollection = new List<CSmartArray>();
        CProductSetCollection m_prCollection = new CProductSetCollection();
        private void newZakaz(object sender, EventArgs e)
        {
            ZakazList.SelectedIndex = -1;
            m_prCollection.FillControls(null);
        }

        private void SelectedIndexChange(object sender, EventArgs e)
        {
            int sIndex = ZakazList.SelectedIndex;
            CSmartArray array = null;
            if (sIndex != -1)
            {
                array = m_ZakazCollection[ZakazList.SelectedIndex];
                
            }
            m_prCollection.FillControls(array);
        }
        private void RemoveZakaz(object sender, EventArgs e)
        {
            int sIndex = ZakazList.SelectedIndex;
            m_ZakazCollection.RemoveAt(sIndex);
            ZakazList.Items.RemoveAt(sIndex);
            ZakazList.SelectedIndex = -1;
        }
        private void SaveZakaz(object sender, EventArgs e)
        {
            CSmartArray array = m_prCollection.CalcArray();
            int sIndex = ZakazList.SelectedIndex;
            if (sIndex == -1)
            {
                m_ZakazCollection.Add(array);
                string name = "заказ " + (ZakazList.Items.Count + 1).ToString();
                ZakazList.Items.Add(name);
            }
            else
            {
                m_ZakazCollection[sIndex] = array;
            }
        }
        public void Init(Panel pPanel)
        {
            m_prCollection.InitByPanel(pPanel);
            for (int i = 0; i < pPanel.Controls.Count; i++) 
            {
                Control contr = pPanel.Controls[i];
                string tag = contr.Tag as string;
                if (tag == "ZalazCollection")
                    ZakazList = contr as ListBox;
                if (tag == "NewZakaz")
                    ZakazButton = contr as Button;
                if (tag == "RemoveZakaz")
                    DeleteButton = contr as Button;
                if (tag == "SaveButton")
                    SaveButton = contr as Button;
            }
            ZakazButton.Click += new System.EventHandler(this.newZakaz);
            ZakazList.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChange);
            DeleteButton.Click += new System.EventHandler(this.RemoveZakaz);
            SaveButton.Click += new System.EventHandler(this.SaveZakaz);
        }
        public CSmartArray CalcArray()
        {
            CSmartArray array = new CSmartArray();
            for (int i = 0; i < m_ZakazCollection.Count; i++)
            {
                array.Merge(m_ZakazCollection[i]);
            }
            return array;
        }

        //CProductSet m_CurrentZakaz;
    }
}
