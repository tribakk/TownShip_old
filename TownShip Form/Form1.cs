using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace TownShip_Form
{
    public partial class Form1 : Form
    {
        Kernal.CProductSetCollection m_TrainSet1 = new Kernal.CProductSetCollection();
        Kernal.CProductSetCollection m_TrainSet2 = new Kernal.CProductSetCollection();
        Kernal.CProductSetCollection m_TrainSet3 = new Kernal.CProductSetCollection();

        Kernal.CProductSetCollection m_PlaneSet1 = new Kernal.CProductSetCollection();
        Kernal.CProductSetCollection m_PlaneSet2 = new Kernal.CProductSetCollection();
        Kernal.CProductSetCollection m_PlaneSet3 = new Kernal.CProductSetCollection();

        Kernal.CZakazCollection m_ZakazSet = new Kernal.CZakazCollection();

        Kernal.CAllProductSpisok spisok = new Kernal.CAllProductSpisok();
        Kernal.CSmartArray m_AlreadyHave = new Kernal.CSmartArray();

        Kernal.CExcelInputHelper m_pCExcelInputHelper;

        int CalcCount = 7;

        void ShowResultForm(string result)
        {
            Form resultForm = new Form();
            System.Windows.Forms.TextBox resultBox = new System.Windows.Forms.TextBox();
            resultForm.Controls.Add(resultBox);
            resultBox.Dock = DockStyle.Fill;
            resultBox.Multiline = true;
            resultForm.Size = new Size(500, 500);
            resultForm.Show();
            resultBox.Text = result;
        }
        private void FillCombobox(Control contr)
        {
            ComboBox combo = contr as ComboBox;
            if (combo != null)
            {
                int count = spisok.GetCount();
                combo.Items.Clear();
                for (int i = 1; i <= count; i++) 
                {
                    combo.Items.Add(spisok.GetName((Kernal.ProductTag)i));
                }
            }
            else if (contr.Controls.Count>0)
            {
                for (int i =0;i<contr.Controls.Count;i++)
                {
                    FillCombobox(contr.Controls[i]);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            ZakazPanel.BackColor = Color.FromArgb(50, Color.Black);
            trainPanel.BackColor = Color.FromArgb(50, Color.Black);
            planePanel.BackColor = Color.FromArgb(50, Color.Black);
            FillCombobox(this);

            m_TrainSet1.InitByPanel(train1);
            m_TrainSet2.InitByPanel(train2);
            m_TrainSet3.InitByPanel(train3);

            m_PlaneSet1.InitByPanel(plane1);
            m_PlaneSet1.InitByPanel(plane2);
            m_PlaneSet1.InitByPanel(plane3);

            m_ZakazSet.Init(ZakazPanel);

        }
        private Kernal.CSmartArray GetAllZakazArray()
        {
            Kernal.CSmartArray array = m_ZakazSet.CalcArray();
            return array;
        }

        private Kernal.CSmartArray GetAllTrainArray()
        {
            Kernal.CSmartArray array = m_TrainSet1.CalcArray();
            array.Merge(m_TrainSet2.CalcArray());
            array.Merge(m_TrainSet3.CalcArray());
            return array;
        }


        private Kernal.CSmartArray GetAllPlaneArray()
        {
            Kernal.CSmartArray array = m_PlaneSet1.CalcArray();
            array.Merge(m_PlaneSet2.CalcArray());
            array.Merge(m_PlaneSet3.CalcArray());
            return array;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Kernal.CProductCalc calc = new Kernal.CProductCalc();
            Kernal.CSmartArray array = GetAllTrainArray();
            array.ExcludeWhatHave(m_AlreadyHave.Copy());
            calc.SetArray(array);
            calc.SetAlreadyHave(m_AlreadyHave.Copy());
            for (int i = 0; i < CalcCount; i++) calc.Calc();
            
            ShowResultForm(calc.Print());
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Kernal.CProductCalc ar = new Kernal.CProductCalc();
            Kernal.CSmartArray array = GetAllZakazArray();
            array.ExcludeWhatHave(m_AlreadyHave.Copy());
            ar.SetArray(array);
            ar.SetAlreadyHave(m_AlreadyHave.Copy());
            for (int i = 0; i < CalcCount; i++) ar.Calc();
            ShowResultForm(ar.Print());
        }

        private void planeCalc_Click(object sender, EventArgs e)
        {
            Kernal.CProductCalc calc = new Kernal.CProductCalc();
            Kernal.CSmartArray array = GetAllPlaneArray();
            array.ExcludeWhatHave(m_AlreadyHave.Copy());
            calc.SetArray(array);
            calc.SetAlreadyHave(m_AlreadyHave.Copy());
            for (int i = 0; i < CalcCount; i++) calc.Calc();

            ShowResultForm(calc.Print());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kernal.CSmartArray array = GetAllZakazArray();
            array.Merge(GetAllTrainArray());
            array.Merge(GetAllPlaneArray());
            array.ExcludeWhatHave(m_AlreadyHave.Copy());
            Kernal.CProductCalc calc = new Kernal.CProductCalc();
            calc.SetArray(array);
            calc.SetAlreadyHave(m_AlreadyHave.Copy());
            for (int i = 0; i < CalcCount; i++) calc.Calc();
            ShowResultForm(calc.Print());
            Kernal.CExportAllFactoryHelper helper = new Kernal.CExportAllFactoryHelper(calc.GetFactoryList());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Kernal.CAlreadyHaveForm form = new Kernal.CAlreadyHaveForm(m_AlreadyHave);
            form.ShowDialog();
            m_AlreadyHave = form.GetArray();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            m_pCExcelInputHelper = new Kernal.CExcelInputHelper(m_AlreadyHave);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            m_AlreadyHave = m_pCExcelInputHelper.GetArray();
        }
    }
}
